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
using Outlook = Microsoft.Office.Interop.Outlook;

namespace His.ConsultaExterna
{
    public partial class frm_ExploradorCitaMedica : Form
    {
        public frm_ExploradorCitaMedica()
        {
            InitializeComponent();
        }
        public void CargarAgendamientos()
        {
            DateTime fi = monthCalendar1.SelectionRange.Start;
            DateTime ff = monthCalendar1.SelectionRange.End;
            //ff = ff.AddDays(1);

            grid.DataSource = NegConsultaExterna.ViewAgendamiento(Convert.ToDateTime(dtpDesde.Value), Convert.ToDateTime(dtpHasta.Value));
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarAgendamientos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_ExploradorCitaMedica_Load(object sender, EventArgs e)
        {
            monthCalendar1.SelectionRange.Start = DateTime.Now;
            dtpDesde.Value = DateTime.Now;
            monthCalendar1.SelectionRange.End = (DateTime.Now).AddDays(30);
            dtpHasta.Value = (DateTime.Now).AddDays(30);
            dtpDesde.Value = Convert.ToDateTime(dtpDesde.Value.ToString("yyyy'-'MM'-'dd'T'00':'00':'00"));
            dtpHasta.Value = Convert.ToDateTime(dtpHasta.Value.ToString("yyyy'-'MM'-'dd'T'23':'59':'59"));
            CargarAgendamientos();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                dtpDesde.Value = monthCalendar1.SelectionRange.Start;
                dtpHasta.Value = monthCalendar1.SelectionRange.End;
                CargarAgendamientos();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("No puede elegir fecha final como inicial", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                monthCalendar1.SelectionRange.Start = DateTime.Now;
                dtpDesde.Value = DateTime.Now;
                monthCalendar1.SelectionRange.End = (DateTime.Now).AddDays(30);
                dtpHasta.Value = (DateTime.Now).AddDays(30);
                dtpDesde.Value = Convert.ToDateTime(dtpDesde.Value.ToString("yyyy'-'MM'-'dd'T'00':'00':'00"));
                dtpHasta.Value = Convert.ToDateTime(dtpHasta.Value.ToString("yyyy'-'MM'-'dd'T'23':'59':'59"));
            }
        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            monthCalendar1.SetSelectionRange(dtpDesde.Value, dtpHasta.Value);
            dtpHasta.MinDate = dtpDesde.Value;
            CargarAgendamientos();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            monthCalendar1.SetSelectionRange(dtpDesde.Value, dtpHasta.Value);
            dtpDesde.MaxDate = dtpHasta.Value;
            CargarAgendamientos();
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

            UltraGridBand bandUno = grid.DisplayLayout.Bands[0];

            grid.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            grid.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            grid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            grid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            grid.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            grid.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            grid.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            grid.DisplayLayout.Override.DefaultRowHeight = 20; //Para el modo tablet

            //Caracteristicas de Filtro en la grilla
            grid.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            grid.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            grid.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            grid.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            grid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            grid.DisplayLayout.UseFixedHeaders = true;

            //Dimension los registros
            grid.DisplayLayout.Bands[0].Columns["Fecha Cita Medica"].Width = 150;
            grid.DisplayLayout.Bands[0].Columns["Hora Cita Medica"].Width = 150;
            grid.DisplayLayout.Bands[0].Columns["Paciente"].Width = 400;
            grid.DisplayLayout.Bands[0].Columns["Identificacion"].Width = 150;
            grid.DisplayLayout.Bands[0].Columns["Direccion"].Width = 400;
            grid.DisplayLayout.Bands[0].Columns["Telefono"].Width = 150;
            grid.DisplayLayout.Bands[0].Columns["Celular"].Width = 150;
            grid.DisplayLayout.Bands[0].Columns["Email"].Width = 250;
            grid.DisplayLayout.Bands[0].Columns["Medico"].Width = 400;
            grid.DisplayLayout.Bands[0].Columns["Especialidad"].Width = 150;
            grid.DisplayLayout.Bands[0].Columns["Email Medico"].Width = 250;
            grid.DisplayLayout.Bands[0].Columns["Motivo de la Consulta"].Width = 300;
            grid.DisplayLayout.Bands[0].Columns["Observaciones"].Width = 300;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if(grid.Selected.Rows.Count == 1)
            {
                UltraGridRow fila = grid.ActiveRow;
                DateTime fechaCita = Convert.ToDateTime(fila.Cells["Fecha Cita Medica"].Value.ToString());
                string[] hora = fila.Cells["Hora Cita Medica"].Value.ToString().Split('-');
                DateTime horaCita = Convert.ToDateTime(hora[0].Trim());
                if(fechaCita.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("No puede cancelar una cita ya concluida.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if(fechaCita.Date >= DateTime.Now.Date)
                {
                    if (horaCita.Hour < DateTime.Now.Hour && fechaCita.Date < DateTime.Now.Date)
                    {
                        MessageBox.Show("No puede cancelar una cita ya concluida", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if(MessageBox.Show("Esta seguro de eliminar cita medica del paciente: " + fila.Cells["Paciente"].Value.ToString().Trim() + "?",
                    "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (NegConsultaExterna.EliminarCita(Convert.ToInt64(fila.Cells["Codigo"].Value.ToString())))
                    {
                        MessageBox.Show("La cita ha sido eliminada.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        EnviarCorreoCancelacion(fila.Cells["Paciente"].Value.ToString(), fila.Cells["Medico"].Value.ToString(), fila.Cells["Email"].Value.ToString(), fila.Cells["Email Medico"].Value.ToString());
                        CargarAgendamientos();
                    }
                    else
                        MessageBox.Show("No se ha podido eliminar cita medica. Intente mas tarde.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void EnviarCorreoCancelacion(string paciente, string medico, string emailpaciente,
            string emailmedico)
        {
            EMPRESA empresa = NegEmpresa.RecuperaEmpresa();
            Outlook._Application olApp = new Outlook.Application();
            Outlook.NameSpace mapiNS = olApp.GetNamespace("MAPI");
            string profile = "";
            mapiNS.Logon(profile, null, null, null);

            if (!EnviaEmailOutlook(emailpaciente, emailmedico, empresa.EMP_NOMBRE + ", Cita Cancelada", "Su cita con el medico: " + medico + ", del paciente: " + paciente + " ha sido cancelada." + "\r\nCita cancelada por: " + His.Entidades.Clases.Sesion.nomUsuario))
                MessageBox.Show("Enviar Mensaje Manualmente Y Solicite Soporte Tecnico Outlook Fuera de Línea", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static Boolean EnviaEmailOutlook(string mailPaciente, string mailDr, string mailSubject, string mailContent)
        {
            try
            {
                var oApp = new Outlook.Application();

                Outlook.NameSpace ns = oApp.GetNamespace("MAPI");
                var f = ns.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);

                System.Threading.Thread.Sleep(1000);

                var mailItem = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
                mailItem.Subject = mailSubject;
                mailItem.HTMLBody = mailContent;
                mailItem.To = mailPaciente;
                mailItem.CC = mailDr;
                mailItem.Send();

            }
            catch
            {
                return false;
            }

            return true;
        }
        private void btnDirectorio_Click(object sender, EventArgs e)
        {
            frm_AyudaMedicos x = new frm_AyudaMedicos("999");
            x.ShowDialog();
        }
    }
}
