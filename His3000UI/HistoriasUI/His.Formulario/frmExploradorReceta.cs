using His.Entidades;
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

namespace His.Formulario
{
    public partial class frmExploradorReceta : Form
    {
        public frmExploradorReceta()
        {
            InitializeComponent();
        }

        private void UltraGridRecetas_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = UltraGridRecetas.DisplayLayout.Bands[0];

            UltraGridRecetas.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            UltraGridRecetas.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            UltraGridRecetas.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            UltraGridRecetas.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            UltraGridRecetas.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            UltraGridRecetas.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            UltraGridRecetas.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            UltraGridRecetas.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

            //Caracteristicas de Filtro en la grilla
            UltraGridRecetas.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            UltraGridRecetas.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            UltraGridRecetas.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            UltraGridRecetas.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            UltraGridRecetas.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            UltraGridRecetas.DisplayLayout.UseFixedHeaders = true;

            //Dimension los registros
            UltraGridRecetas.DisplayLayout.Bands[0].Columns[0].Width = 60;
            UltraGridRecetas.DisplayLayout.Bands[0].Columns[1].Width = 150;
            UltraGridRecetas.DisplayLayout.Bands[0].Columns[2].Width = 80;
            UltraGridRecetas.DisplayLayout.Bands[0].Columns[3].Width = 60;
            UltraGridRecetas.DisplayLayout.Bands[0].Columns[4].Width = 60;
            UltraGridRecetas.DisplayLayout.Bands[0].Columns[5].Width = 350;

            UltraGridRecetas.DisplayLayout.Bands[0].Columns["ate_codigo"].Hidden = true;
        }
        public void CargarRecetas()
        {
            try
            {
                UltraGridRecetas.DataSource = NegCertificadoMedico.ExploradorRecetas(dtpFiltroDesde.Value, dtpFiltroHasta.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarRecetas();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmExploradorReceta_Load(object sender, EventArgs e)
        {
            //Primero obtenemos el día actual
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);

            dtpFiltroDesde.Value = oPrimerDiaDelMes;
            dtpFiltroHasta.Value = oUltimoDiaDelMes;
            CargarRecetas();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string PathExcel = FindSavePath();
            //    if (PathExcel != null)
            //    {
            //        this.ultraGridExcelExporter1.Export(UltraGridRecetas, PathExcel);
            //        MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
            //    }
            //}
            //catch (Exception ex)
            //{ MessageBox.Show(ex.Message); }
            //finally
            //{ this.Cursor = Cursors.Default; }
        }
        private String FindSavePath()
        {
            Stream myStream;
            string myFilepath = null;
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xlsx)|*.xlsx";
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

        private void btnimprimir_Click(object sender, EventArgs e)
        {
            try
            {
                UltraGridRow fila = UltraGridRecetas.ActiveRow;
                Imprimir(Convert.ToInt64(fila.Cells["ate_codigo"].Value.ToString()),Convert.ToInt64(fila.Cells["Codigo"].Value));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void Imprimir(Int64 ate_codgo, Int64 RM_CODIGO)
        {
            DSRecetaPasteur newreceta = new DSRecetaPasteur();

            DataRow row;
            EMPRESA emp = NegEmpresa.RecuperaEmpresa();

            RECETA_MEDICA receta = NegCertificadoMedico.RecuperaReceta(ate_codgo,RM_CODIGO);
            List<RECETA_DIAGNOSTICO> diagnostico = NegCertificadoMedico.RecuperaDiagnostico(receta.RM_CODIGO);
            List<RECETA_MEDICAMENTOS> medica = NegCertificadoMedico.RecuperarMedicamentos(receta.RM_CODIGO);
            ATENCIONES ultimaAtencion = NegAtenciones.RecuperarAtencionID(ate_codgo);
            PACIENTES paciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codgo));
            MEDICOS medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(receta.MED_CODIGO));
            TIPO_CONSULTA consulta = new TIPO_CONSULTA();
            TIPO_INGRESO ingreso = new TIPO_INGRESO();
            
            ingreso = NegTipoIngreso.FiltrarPorId(Convert.ToInt16(receta.TIP_CODIGO));


            var now = DateTime.Now;
            var birthday = paciente.PAC_FECHA_NACIMIENTO.Value;
            var yearsOld = now - birthday;

            int years = (int)(yearsOld.TotalDays / 365.25);
            int months = (int)(((yearsOld.TotalDays / 365.25) - years) * 12);

            TimeSpan age = now - birthday;
            DateTime totalTime = new DateTime(age.Ticks);

            row = newreceta.Tables["RecetaPasteur"].NewRow();
            SUCURSALES sucursal = NegEmpresa.RecuperaEmpresaID(1);
            if (ingreso.TIP_CODIGO == 10)
            {
                row["Logo"] = NegUtilitarios.RutaLogo("Mushuñan");
                row["Empresa"] = sucursal.SUC_NOMBRE;
                row["Direccion"] = sucursal.SUC_DIRECCION;
                row["Telefonos"] = sucursal.SUC_TELEFONO;
                row["Fecha"] = "Sangolqui, " + Convert.ToDateTime(receta.RM_FECHA).ToString("dddd") + ", " + Convert.ToDateTime(receta.RM_FECHA).Day.ToString() + " de " + Convert.ToDateTime(receta.RM_FECHA).ToString("MMMM") + " de " + Convert.ToDateTime(receta.RM_FECHA).ToString("yyyy");
            }
            else
            {
                row["Logo"] = NegUtilitarios.RutaLogo("GENERAL");
                row["Empresa"] = His.Entidades.Clases.Sesion.nomEmpresa;
                row["Direccion"] = emp.EMP_DIRECCION;
                row["Telefonos"] = emp.EMP_TELEFONO;
                row["Fecha"] = "Quito, DM, " + Convert.ToDateTime(receta.RM_FECHA).ToString("dddd") + ", " + Convert.ToDateTime(receta.RM_FECHA).Day.ToString() + " de " + Convert.ToDateTime(receta.RM_FECHA).ToString("MMMM") + " de " + Convert.ToDateTime(receta.RM_FECHA).ToString("yyyy");
            }
            row["Paciente"] = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO + " " + paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
            row["Identificacion"] = paciente.PAC_IDENTIFICACION;
            row["Edad"] = years + " AÑOS, " + months + " MESES, " + totalTime.Day + " DIAS";
            row["Procedencia"] = ingreso.TIP_DESCRIPCION;
            row["Alergias"] = receta.RM_ALERGIAS;
            row["Medico"] = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
            if (medico.MED_RUC.Length <= 10)
                row["Medico_Ruc"] = medico.MED_RUC;
            else
            {
                string te = medico.MED_RUC;
                te = te.Substring(0, 10);
                row["Medico_Ruc"] = te;
            }
            row["Medico_Telefono"] = receta.MED_TELEFONO;
            row["Alarma"] = receta.RM_SIGNO;
            row["Cita"] = receta.RM_CITA;
            row["Farmacos"] = receta.RM_FARMACOS;
            row["Numero"] = receta.RM_CODIGO;
            newreceta.Tables["RecetaPasteur"].Rows.Add(row);

            foreach (var item in diagnostico)
            {
                CIE10 cie = NegCIE10.RecuperarCIE10(item.CIE_CODIGO);
                row = newreceta.Tables["Diagnostico"].NewRow();
                row["Diagnosticos"] = cie.CIE_DESCRIPCION;
                row["Cie10"] = item.CIE_CODIGO;
                row["Alergias"] = receta.RM_ALERGIAS;
                newreceta.Tables["Diagnostico"].Rows.Add(row);
            }

            foreach (var item in medica)
            {
                DataTable TablaP = NegProducto.RecuperarProductoSic(item.CODPRO);
                row = newreceta.Tables["Medicamentos"].NewRow();
                if (TablaP.Rows.Count > 0)
                    row["Medicamento"] = TablaP.Rows[0]["despro"].ToString();
                else
                {
                    row["Medicamento"] = item.RMD_DESCRIPCION;
                }
                row["Via"] = item.RMD_ADMINISTRACION;
                row["Presentacion"] = item.RMD_PRESENTACION;
                row["Concentracion"] = item.RMD_CONCENTRACION;
                row["Cantidad"] = item.RMD_CANTIDAD + " (" + Dia_En_Palabras(item.RMD_CANTIDAD.ToString()) + ")";
                row["Indicacion"] = item.RMD_INDICACIONES;
                //if (chkCita.Checked)
                //{
                if (receta.TC_CONSULTA == 0)
                {
                    row["Consulta"] = "";
                    row["Cita"] = "";
                }
                else
                {
                    TIPO_CONSULTA Lconsulta = NegCertificadoMedico.RecuperarConsulta((int)receta.TC_CONSULTA);
                    row["Consulta"] = Lconsulta.TC_DESCRIPCION;
                    row["Cita"] = Convert.ToDateTime(receta.RM_CITA).ToString("dd/MM/yyyy HH:mm");
                }

                //}
                //else
                //{
                //    if (receta.TC_CONSULTA == 0)
                //    {
                //        row["Consulta"] = "";
                //        row["Cita"] = "";
                //    }
                //}
                row["Alarma"] = receta.RM_SIGNO;
                row["Numero"] = receta.RM_CODIGO;
                row["Farmacos"] = receta.RM_FARMACOS;
                row["Paciente"] = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO + " " + paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                row["Identificacion"] = paciente.PAC_IDENTIFICACION;
                row["Edad"] = years + " AÑOS, " + months + " MESES, " + totalTime.Day + " DIAS";
                if (ingreso.TIP_CODIGO == 10)
                    row["Logo"] = NegUtilitarios.RutaLogo("Mushuñan");
                else
                    row["Logo"] = NegUtilitarios.RutaLogo("GENERAL");
                row["Comercial"] = item.RMD_COMERCIAL;

                newreceta.Tables["Medicamentos"].Rows.Add(row);
            }

            His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "RecetaPasteur", newreceta);
            reporte.Show();
        }
        public string Dia_En_Palabras(string dia)
        {
            string num2Text = ""; int value = Convert.ToInt32(dia);
            if (value == 0) num2Text = "CERO";
            else if (value == 1) num2Text = "UN";
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

        private void btnanular_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Para inhabilitar una receta medica necesita la autorización de Dirección Médica", "HIS3000", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                UltraGridRow fila = this.UltraGridRecetas.ActiveRow;
                frmAnulaCertificado frm = new frmAnulaCertificado(Convert.ToInt32(fila.Cells["Codigo"].Value.ToString()), Convert.ToString(fila.Cells["Medico"].Value.ToString()), "", false);
                frm.ShowDialog();
                CargarRecetas();
            }
        }
    }
}
