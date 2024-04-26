using His.DatosReportes;
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

namespace His.Admision
{
    public partial class frmCensoDiarioSxE : Form
    {
        public frmCensoDiarioSxE()
        {
            InitializeComponent();
        }

        private void frmCensoDiarioSxE_Load(object sender, EventArgs e)
        {
            refrescar();
        }
        private void refrescar()
        {
            //DataTable x = (NegHabitaciones.sp_HabitacionesCenso(1));
            //x.Columns.Add("Tipo", typeof(String));

            //foreach (DataRow row in x.Rows)
            //{
            //    row["Tipo"] = "HOSPITALIZACION";
            //}
            //DataTable y = (NegHabitaciones.sp_HabitacionesCenso(2));
            //y.Columns.Add("Tipo", typeof(String));
            //foreach (DataRow row in y.Rows)
            //{
            //    row["Tipo"] = "EMERGENCIA";
            //}
            DataTable z = (NegHabitaciones.sp_HabitacionesCenso(3));
            z.Columns.Add("Tipo", typeof(String));
            foreach (DataRow row in z.Rows)
            {
                row["Tipo"] = "OTROS";
            }
            //x.Merge(y);
            //x.Merge(z);
            ugrdHistorial.DataSource = z;

            UltraGridBand band = this.ugrdHistorial.DisplayLayout.Bands[0];
            band.Columns["CodigoHabitacion"].Hidden = true;

        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            refrescar();
        }

        private void toolStripButtonBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (ugrdHistorial.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(ugrdHistorial, PathExcel);
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //ReportesHistoriaClinica msa = new ReportesHistoriaClinica();
            //msa.DeleteTable("CensoDiario");

            //UltraGridBand band = this.ugrdHistorial.DisplayLayout.Bands[0];
            //foreach (UltraGridRow row in band.GetRowEnumerator(GridRowType.DataRow))
            //{
            //    string[] x = new string[] { Convert.ToString(row.Cells[0].Value).Trim(),
            //                                Convert.ToString(row.Cells[1].Value).Trim(),
            //                                Convert.ToString(row.Cells[2].Value).Trim(),
            //                                Convert.ToString(row.Cells[3].Value).Trim(),
            //                                Convert.ToString(row.Cells[4].Value).Trim(),
            //                                Convert.ToString(row.Cells[5].Value).Trim(),
            //                                Convert.ToString(row.Cells[6].Value).Trim(),
            //                                Convert.ToString(row.Cells[7].Value).Trim(),
            //                                Convert.ToString(row.Cells[8].Value).Trim(),
            //                                Convert.ToString(row.Cells[9].Value).Trim(),
            //                                Convert.ToString(row.Cells[10].Value).Trim(),
            //                                Convert.ToString(row.Cells[11].Value).Trim(),
            //                                Convert.ToString(row.Cells[12].Value).Trim(),
            //                                Convert.ToString(row.Cells[13].Value).Trim(),
            //                                Convert.ToString(row.Cells[14].Value).Trim(),
            //                                Convert.ToString(row.Cells[15].Value).Trim(),
            //                                Convert.ToString(row.Cells[16].Value).Trim(),
            //                                Convert.ToString(row.Cells[17].Value).Trim()
            //                                 };
            //    ReportesHistoriaClinica msa2 = new ReportesHistoriaClinica();
            //    msa2.InsertTable("CensoDiario", x);
            //}

            //frmReportes ventana = new frmReportes();
            //ventana.reporte = "CensoDiario";
            //ventana.Show();


            DSCensoDiaria censo = new DSCensoDiaria();
            DataRow dr;

            NegCertificadoMedico c = new NegCertificadoMedico();
            foreach (UltraGridRow item in ugrdHistorial.Rows)
            {
                dr = censo.Tables["Censo"].NewRow();
                dr["Paciente"] = item.Cells["NombrePaciente"].Value.ToString();
                dr["Identificacion"] = item.Cells["Cedula"].Value.ToString();
                dr["Hc"] = item.Cells["HistoriaClincia"].Value.ToString();
                dr["Hab"] = item.Cells["NumeroHabitacion"].Value.ToString();
                dr["Medico"] = item.Cells["MedicoTratante"].Value.ToString();
                dr["Nacimiento"] = item.Cells["FechaNacimiento"].Value.ToString();
                dr["Ingreso"] = item.Cells["FechaIngreso"].Value.ToString();
                if (item.Cells["Referido"].Value.ToString() == "PRIVADO")
                    dr["Ref"] = "P";
                else
                    dr["Ref"] = "H";
                dr["Seguro"] = item.Cells["Aseguradora"].Value.ToString();
                dr["Tipo"] = item.Cells["Tipo"].Value.ToString();
                dr["Logo"] = c.path();
                dr["Total"] = 0;
                dr["Area"] = item.Cells["Area"].Value.ToString();
                dr["TotalGeneral"] = ugrdHistorial.Rows.Count;

                censo.Tables["Censo"].Rows.Add(dr);
            }
            frmReportes xs = new frmReportes(1, "Censo", censo);
            xs.Show();
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ugrdHistorial_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            //Caracteristicas de Filtro en la grilla
            ugrdHistorial.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ugrdHistorial.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ugrdHistorial.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ugrdHistorial.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
            //dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro";  
            ugrdHistorial.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
        }
    }
}
