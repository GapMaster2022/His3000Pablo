using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using Recursos;
using System.IO;

namespace TarifariosUI
{
    public partial class frmBuscarHonorario : Form
    {
        private HIS3000BDEntities conexion;
        public frmBuscarHonorario()
        {
            InitializeComponent();
            conexion = new HIS3000BDEntities(ConexionEntidades.ConexionEDM); 
            
        }
        int codigo = 0;
        int codigoHonorario = 0;
        string fecFacMedDesde = String.Empty;
        string fecFacMedHasta = String.Empty;
        public void cargarAyuda(int cod)
        {
            codigo = cod;
            HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
            /*var honorariosQuery = (from a in contexto.TARIFARIOS_DETALLE
                                     join c in contexto.HONORARIOS_TARIFARIO_DETALLE on a.TAD_CODIGO equals c.TARIFARIOS_DETALLE.TAD_CODIGO
                                     join d in contexto.HONORARIOS_TARIFARIO on c.HOD_CODIGO equals d.HON_CODIGO
                                     join m in contexto.MEDICOS on d.MEDICOS.MED_CODIGO equals m.MED_CODIGO
                                     where m.MED_CODIGO == cod
                                     select new {a.TAD_CODIGO , a.TAD_REFERENCIA, a.TAD_DESCRIPCION, c.HOD_CANTIDAD,
                                                c.HOD_UVR, c.HOD_ANESTESIA, c.HOD_VALOR_UVR, c.HOD_VALOR_ANESTESIA, c.HOD_SUBTOTAL});

            */

            /*var honorariosQuery = (from t in contexto.HONORARIOS_TARIFARIO
                                   join m in contexto.MEDICOS on t.MEDICOS.MED_CODIGO equals m.MED_CODIGO
                                   join d in contexto.HONORARIOS_TARIFARIO_DETALLE on t.HON_CODIGO equals d.HONORARIOS_TARIFARIO.HON_CODIGO
                                   join td in contexto.TARIFARIOS_DETALLE on d.TARIFARIOS_DETALLE.TAD_CODIGO equals td.TAD_CODIGO
                                   where m.MED_CODIGO == cod
                                   orderby t.HON_FECHA descending
                                   select new {d.TARIFARIOS_DETALLE.TAD_CODIGO , td.TAD_REFERENCIA,
                                               d.HOD_DESCRIPCION, t.HON_FECHA, d.HOD_CANTIDAD, d.HOD_UVR,
                                               d.HOD_ANESTESIA, d.HOD_VALOR_UVR, d.HOD_VALOR_ANESTESIA, t.HON_TOTAL}
                );*/

            var honorariosQuery = (from t in contexto.HONORARIOS_TARIFARIO
                                   join a in contexto.ASEGURADORAS_EMPRESAS on t.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO
                                   where t.MEDICOS.MED_CODIGO == cod
                                   select new {
                                      CODIGO = t.HON_CODIGO, 
                                      ASEGURADORA = a.ASE_NOMBRE, 
                                      FECHA = t.HON_FECHA, 
                                      PACIENTE = t.HON_PACIENTE,
                                      HIS_CLINICA = t.HON_HISTORIA_CLINICA,
                                      TOTAL = t.HON_TOTAL
                                       }
                                   
                );
           
            
            //honorariosGrid.DataSource = honorariosQuery;
            uGridHonorarios.DataSource = honorariosQuery;
            //honorariosGrid.Columns["PACIENTE"].Width = 500;
            //honorariosGrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //honorariosGrid.AllowUserToOrderColumns = true;
            //honorariosGrid.Columns["CODIGO"].Width = 57;
            //honorariosGrid.Columns["ASEGURADORA"].Width = 150;
            //honorariosGrid.Columns["FECHA"].Width = 100;
            //honorariosGrid.Columns["PACIENTE"].Width = 240;
            //honorariosGrid.Columns["HIS_CLINICA"].Width = 100;
            //honorariosGrid.Columns["TOTAL"].Width = 100;

            //honorariosGrid.u

            //exportExcell();

        }

        //public void exportExcell()
        //{
        //    Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();

        //    xla.Visible = true;
        //    Microsoft.Office.Interop.Excel.Workbook wb = xla.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
        //    Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xla.ActiveSheet;
        //    int i = 1;
        //    int j = 1;
            
        //    string c = honorariosGrid.Rows[0].Cells[0].Value.ToString();
        //    for (i = 0; i < honorariosGrid.Rows.Count; i++)
        //    {
        //        for (j = 0; j < honorariosGrid.Columns.Count; j++)
        //        {
        //            ws.Cells[i + 1, j + 1] = honorariosGrid.Rows[i].Cells[j].Value.ToString().Trim();
                    
        //        }
        //    }
            
        //}

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        
        
        //private void honorariosGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
            
        //    if (honorariosGrid.Columns[e.ColumnIndex].Tag == null)
        //    {
        //        honorariosGrid.Columns[e.ColumnIndex].Tag = SortOrder.Ascending;
        //        honorariosGrid.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
        //    }
        //    else if (honorariosGrid.Columns[e.ColumnIndex].Tag.ToString() == SortOrder.Ascending.ToString())
        //    {
        //        honorariosGrid.Columns[e.ColumnIndex].Tag = SortOrder.Descending;
        //        honorariosGrid.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
        //    }
        //    else if (honorariosGrid.Columns[e.ColumnIndex].Tag.ToString() == SortOrder.Descending.ToString())
        //    {
        //        honorariosGrid.Columns[e.ColumnIndex].Tag = SortOrder.Ascending;
        //        honorariosGrid.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
        //    }
        //    string columna = honorariosGrid.Columns[e.ColumnIndex].DataPropertyName.Trim();
        //    string modo = honorariosGrid.Columns[e.ColumnIndex].Tag.ToString().ToLower();
        //    HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);

        //    #region SortFields

        //    #region ASE_NOMBRE
        //    if (columna == "ASEGURADORA")
        //    {
        //        if (modo == "ascending")
        //        {
        //            var honorariosQuery = (from t in contexto.HONORARIOS_TARIFARIO
        //                                   join a in contexto.ASEGURADORAS_EMPRESAS on t.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO
        //                                   where t.MEDICOS.MED_CODIGO == codigo
        //                                   orderby a.ASE_NOMBRE ascending
        //                                   select new
        //                                   {
        //                                       CODIGO = t.HON_CODIGO,
        //                                       ASEGURADORA = a.ASE_NOMBRE,
        //                                       FECHA = t.HON_FECHA,
        //                                       PACIENTE = t.HON_PACIENTE,
        //                                       HIS_CLINICA = t.HON_HISTORIA_CLINICA,
        //                                       TOTAL = t.HON_TOTAL
        //                                   }

        //               );

        //            honorariosGrid.DataSource = honorariosQuery;
        //            uGridHonorarios.DataSource = honorariosQuery;
        //        }
        //        else
        //        {
        //            var honorariosQuery = (from t in contexto.HONORARIOS_TARIFARIO
        //                                   join a in contexto.ASEGURADORAS_EMPRESAS on t.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO
        //                                   where t.MEDICOS.MED_CODIGO == codigo
        //                                   orderby a.ASE_NOMBRE descending
        //                                   select new
        //                                   {
        //                                       CODIGO = t.HON_CODIGO,
        //                                       ASEGURADORA = a.ASE_NOMBRE,
        //                                       FECHA = t.HON_FECHA,
        //                                       PACIENTE = t.HON_PACIENTE,
        //                                       HIS_CLINICA = t.HON_HISTORIA_CLINICA,
        //                                       TOTAL = t.HON_TOTAL
        //                                   }

        //                );

        //            honorariosGrid.DataSource = honorariosQuery;
        //            uGridHonorarios.DataSource = honorariosQuery;
        //        }
        //    }
        //    #endregion

        //    #region HON_CODIGO
        //    if (columna == "CODIGO")
        //    {
        //        if (modo == "ascending")
        //        {
        //            var honorariosQuery = (from t in contexto.HONORARIOS_TARIFARIO
        //                                   join a in contexto.ASEGURADORAS_EMPRESAS on t.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO
        //                                   where t.MEDICOS.MED_CODIGO == codigo
        //                                   orderby t.HON_CODIGO ascending
        //                                   select new
        //                                   {
        //                                       CODIGO = t.HON_CODIGO,
        //                                       ASEGURADORA = a.ASE_NOMBRE,
        //                                       FECHA = t.HON_FECHA,
        //                                       PACIENTE = t.HON_PACIENTE,
        //                                       HIS_CLINICA = t.HON_HISTORIA_CLINICA,
        //                                       TOTAL = t.HON_TOTAL
        //                                   }

        //               );

        //            honorariosGrid.DataSource = honorariosQuery;
        //        }
        //        else
        //        {
        //            var honorariosQuery = (from t in contexto.HONORARIOS_TARIFARIO
        //                                   join a in contexto.ASEGURADORAS_EMPRESAS on t.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO
        //                                   where t.MEDICOS.MED_CODIGO == codigo
        //                                   orderby t.HON_CODIGO descending
        //                                   select new
        //                                   {
        //                                       CODIGO = t.HON_CODIGO,
        //                                       ASEGURADORA = a.ASE_NOMBRE,
        //                                       FECHA = t.HON_FECHA,
        //                                       PACIENTE = t.HON_PACIENTE,
        //                                       HIS_CLINICA = t.HON_HISTORIA_CLINICA,
        //                                       TOTAL = t.HON_TOTAL
        //                                   }
        //                );

        //            honorariosGrid.DataSource = honorariosQuery;
        //        }
        //    }
        //    #endregion

        //    #region HON_FECHA
        //    if (columna == "FECHA")
        //    {
        //        if (modo == "ascending")
        //        {
        //            var honorariosQuery = (from t in contexto.HONORARIOS_TARIFARIO
        //                                   join a in contexto.ASEGURADORAS_EMPRESAS on t.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO
        //                                   where t.MEDICOS.MED_CODIGO == codigo
        //                                   orderby t.HON_FECHA ascending
        //                                   select new
        //                                   {
        //                                       CODIGO = t.HON_CODIGO,
        //                                       ASEGURADORA = a.ASE_NOMBRE,
        //                                       FECHA = t.HON_FECHA,
        //                                       PACIENTE = t.HON_PACIENTE,
        //                                       HIS_CLINICA = t.HON_HISTORIA_CLINICA,
        //                                       TOTAL = t.HON_TOTAL
        //                                   }

        //               );

        //            honorariosGrid.DataSource = honorariosQuery;
        //        }
        //        else
        //        {
        //            var honorariosQuery = (from t in contexto.HONORARIOS_TARIFARIO
        //                                   join a in contexto.ASEGURADORAS_EMPRESAS on t.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO
        //                                   where t.MEDICOS.MED_CODIGO == codigo
        //                                   orderby t.HON_FECHA descending
        //                                   select new
        //                                   {
        //                                       CODIGO = t.HON_CODIGO,
        //                                       ASEGURADORA = a.ASE_NOMBRE,
        //                                       FECHA = t.HON_FECHA,
        //                                       PACIENTE = t.HON_PACIENTE,
        //                                       HIS_CLINICA = t.HON_HISTORIA_CLINICA,
        //                                       TOTAL = t.HON_TOTAL
        //                                   }

        //                );

        //            honorariosGrid.DataSource = honorariosQuery;
        //        }
        //    }
        //    #endregion

        //    #region HON_TOTAL
        //    if (columna == "TOTAL")
        //    {
        //        if (modo == "ascending")
        //        {
        //            var honorariosQuery = (from t in contexto.HONORARIOS_TARIFARIO
        //                                   join a in contexto.ASEGURADORAS_EMPRESAS on t.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO
        //                                   where t.MEDICOS.MED_CODIGO == codigo
        //                                   orderby t.HON_TOTAL ascending
        //                                   select new
        //                                   {
        //                                       CODIGO = t.HON_CODIGO,
        //                                       ASEGURADORA = a.ASE_NOMBRE,
        //                                       FECHA = t.HON_FECHA,
        //                                       PACIENTE = t.HON_PACIENTE,
        //                                       HIS_CLINICA = t.HON_HISTORIA_CLINICA,
        //                                       TOTAL = t.HON_TOTAL
        //                                   }

        //               );

        //            honorariosGrid.DataSource = honorariosQuery;
        //            uGridHonorarios.DataSource = honorariosQuery;
        //        }
        //        else
        //        {
        //            var honorariosQuery = (from t in contexto.HONORARIOS_TARIFARIO
        //                                   join a in contexto.ASEGURADORAS_EMPRESAS on t.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO
        //                                   where t.MEDICOS.MED_CODIGO == codigo
        //                                   orderby t.HON_TOTAL descending
        //                                   select new
        //                                   {
        //                                       CODIGO = t.HON_CODIGO,
        //                                       ASEGURADORA = a.ASE_NOMBRE,
        //                                       FECHA = t.HON_FECHA,
        //                                       PACIENTE = t.HON_PACIENTE,
        //                                       HIS_CLINICA = t.HON_HISTORIA_CLINICA,
        //                                       TOTAL = t.HON_TOTAL
        //                                   }

        //                );

        //            honorariosGrid.DataSource = honorariosQuery;
        //            uGridHonorarios.DataSource = honorariosQuery;
        //        }
        //    }
        //    #endregion

        //    #region HON_PACIENTE
        //    if (columna == "PACIENTE")
        //    {
        //        if (modo == "ascending")
        //        {
        //            var honorariosQuery = (from t in contexto.HONORARIOS_TARIFARIO
        //                                   join a in contexto.ASEGURADORAS_EMPRESAS on t.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO
        //                                   where t.MEDICOS.MED_CODIGO == codigo
        //                                   orderby t.HON_PACIENTE ascending
        //                                   select new
        //                                   {
        //                                       CODIGO = t.HON_CODIGO,
        //                                       ASEGURADORA = a.ASE_NOMBRE,
        //                                       FECHA = t.HON_FECHA,
        //                                       PACIENTE = t.HON_PACIENTE,
        //                                       HIS_CLINICA = t.HON_HISTORIA_CLINICA,
        //                                       TOTAL = t.HON_TOTAL
        //                                   }

        //               );

        //            honorariosGrid.DataSource = honorariosQuery;
        //            uGridHonorarios.DataSource = honorariosQuery;
        //        }
        //        else
        //        {
        //            var honorariosQuery = (from t in contexto.HONORARIOS_TARIFARIO
        //                                   join a in contexto.ASEGURADORAS_EMPRESAS on t.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO
        //                                   where t.MEDICOS.MED_CODIGO == codigo
        //                                   orderby t.HON_PACIENTE descending
        //                                   select new
        //                                   {
        //                                       CODIGO = t.HON_CODIGO,
        //                                       ASEGURADORA = a.ASE_NOMBRE,
        //                                       FECHA = t.HON_FECHA,
        //                                       PACIENTE = t.HON_PACIENTE,
        //                                       HIS_CLINICA = t.HON_HISTORIA_CLINICA,
        //                                       TOTAL = t.HON_TOTAL
        //                                   }
        //                );

        //            honorariosGrid.DataSource = honorariosQuery;
        //            uGridHonorarios.DataSource = honorariosQuery;
        //        }
        //    }
        //    #endregion

        //    #region HON_HISTORIA_CLINICA
        //    if (columna == "HIS_CLINICA")
        //    {
        //        if (modo == "ascending")
        //        {
        //            var honorariosQuery = (from t in contexto.HONORARIOS_TARIFARIO
        //                                   join a in contexto.ASEGURADORAS_EMPRESAS on t.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO
        //                                   where t.MEDICOS.MED_CODIGO == codigo
        //                                   orderby t.HON_HISTORIA_CLINICA ascending
        //                                   select new
        //                                   {
        //                                       CODIGO = t.HON_CODIGO,
        //                                       ASEGURADORA = a.ASE_NOMBRE,
        //                                       FECHA = t.HON_FECHA,
        //                                       PACIENTE = t.HON_PACIENTE,
        //                                       HIS_CLINICA = t.HON_HISTORIA_CLINICA,
        //                                       TOTAL = t.HON_TOTAL
        //                                   }

        //               );

        //            //honorariosGrid.DataSource = honorariosQuery;
        //            uGridHonorarios.DataSource = honorariosQuery;
        //        }
        //        else
        //        {
        //            var honorariosQuery = (from t in contexto.HONORARIOS_TARIFARIO
        //                                   join a in contexto.ASEGURADORAS_EMPRESAS on t.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO
        //                                   where t.MEDICOS.MED_CODIGO == codigo
        //                                   orderby t.HON_HISTORIA_CLINICA descending
        //                                   select new
        //                                   {
        //                                       CODIGO = t.HON_CODIGO,
        //                                       ASEGURADORA = a.ASE_NOMBRE,
        //                                       FECHA = t.HON_FECHA,
        //                                       PACIENTE = t.HON_PACIENTE,
        //                                       HIS_CLINICA = t.HON_HISTORIA_CLINICA,
        //                                       TOTAL = t.HON_TOTAL
        //                                   }

        //                );

        //            honorariosGrid.DataSource = honorariosQuery;
        //            uGridHonorarios.DataSource = honorariosQuery;
        //        }
        //    }
        //    #endregion

        //    #endregion

        //    //honorariosGrid.Columns["PACIENTE"].Width = 250;
        //}

        //private void honorariosGrid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    codigoHonorario = Convert.ToInt32(honorariosGrid.CurrentRow.Cells["CODIGO"].Value.ToString());
        //    frmReportes reporte = new frmReportes(codigoHonorario);
        //    reporte.Show();
        //    codigoHonorario = 0;
        //}

        //private void honorariosGrid_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    codigoHonorario = Convert.ToInt32(honorariosGrid.CurrentRow.Cells["CODIGO"].Value.ToString());
        //}

        private void frmBuscarHonorario_Load(object sender, EventArgs e)
        {
            btnImprimir.Image = Archivo.imgBtnGonePrint48;
            btnExportarExcel.Image = Archivo.imgOfficeExcel;
            btnBuscarHonorarios.Image = Archivo.imgBtnSearch;
        }

        private void btnBuscarHonorarios_Click(object sender, EventArgs e)
        {
            try
            {
                fecFacMedDesde = String.Format("{0:yyyy/MM/dd}", dtpDesde.Value);
                fecFacMedHasta = String.Format("{0:yyyy/MM/dd}", dtpHasta.Value);


                HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);

                var honorariosQuery = (from t in contexto.HONORARIOS_TARIFARIO
                                       join a in contexto.ASEGURADORAS_EMPRESAS on t.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO
                                       where t.MEDICOS.MED_CODIGO == codigo
                                       && t.HON_FECHA > dtpDesde.Value
                                       && t.HON_FECHA < dtpHasta.Value
                                       select new
                                       {
                                           CODIGO = t.HON_CODIGO,
                                           ASEGURADORA = a.ASE_NOMBRE,
                                           FECHA = t.HON_FECHA,
                                           PACIENTE = t.HON_PACIENTE,
                                           HIS_CLINICA = t.HON_HISTORIA_CLINICA,
                                           TOTAL = t.HON_TOTAL
                                       }
                    );


                //honorariosGrid.DataSource = honorariosQuery;
                uGridHonorarios.DataSource = honorariosQuery;
            }
            catch (Exception err){MessageBox.Show(err.Message);   }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //exportExcell();
            frmReportes reporte = new frmReportes(codigoHonorario);
            reporte.Show();
            //frmReportes reporte = new frmReportes(codigo);
            //reporte.Show();  
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (uGridHonorarios.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(uGridHonorarios, PathExcel);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
        }

        /// <summary>
        /// Busca el directorio donde se guarda el archivo de excel
        /// </summary>
        /// <returns>retorna el directorio</returns>
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

        private void uGridHonorarios_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            uGridHonorarios.DisplayLayout.Bands[0].Columns[0].Header.Caption = "CODIGO";
            uGridHonorarios.DisplayLayout.Bands[0].Columns[1].Header.Caption = "ASEGURADORA";
            uGridHonorarios.DisplayLayout.Bands[0].Columns[2].Header.Caption = "FECHA";
            uGridHonorarios.DisplayLayout.Bands[0].Columns[3].Header.Caption = "PACIENTE";
            uGridHonorarios.DisplayLayout.Bands[0].Columns[4].Header.Caption = "H. CLINICA";
            uGridHonorarios.DisplayLayout.Bands[0].Columns[5].Header.Caption = "TOTAL";

            uGridHonorarios.DisplayLayout.Bands[0].Columns[0].Width = 80;
            uGridHonorarios.DisplayLayout.Bands[0].Columns[1].Width = 180;
            uGridHonorarios.DisplayLayout.Bands[0].Columns[2].Width = 80;
            uGridHonorarios.DisplayLayout.Bands[0].Columns[3].Width = 200;
            uGridHonorarios.DisplayLayout.Bands[0].Columns[4].Width = 80;
            uGridHonorarios.DisplayLayout.Bands[0].Columns[5].Width = 80;
        }
    }
}
/* var honorariosQuery = (from t in contexto.HONORARIOS_TARIFARIO
                                       join m in contexto.MEDICOS on t.MEDICOS.MED_CODIGO equals m.MED_CODIGO
                                       join d in contexto.HONORARIOS_TARIFARIO_DETALLE on t.HON_CODIGO equals d.HONORARIOS_TARIFARIO.HON_CODIGO
                                       join td in contexto.TARIFARIOS_DETALLE on d.TARIFARIOS_DETALLE.TAD_CODIGO equals td.TAD_CODIGO
                                       where m.MED_CODIGO == codigo
                                       orderby campo descending
                                       select new
                                       {
                                           d.TARIFARIOS_DETALLE.TAD_CODIGO,
                                           td.TAD_REFERENCIA,
                                           d.HOD_DESCRIPCION,
                                           t.HON_FECHA,
                                           d.HOD_CANTIDAD,
                                           d.HOD_UVR,
                                           d.HOD_ANESTESIA,
                                           d.HOD_VALOR_UVR,
                                           d.HOD_VALOR_ANESTESIA,
                                           t.HON_TOTAL
                                       }*/