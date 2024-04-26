using His.DatosReportes;
using His.Entidades;
using His.Formulario;
using His.Negocio;
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
            btnNuevo.Enabled = rbCedula.Checked; btnModificar.Enabled = !rbCedula.Checked; btnImprimir.Enabled = !rbCedula.Checked;
            actualizarGrid();
        }

        private void rbGenerados_CheckedChanged(object sender, EventArgs e)
        {
            btnNuevo.Enabled = !rbGenerados.Checked; btnModificar.Enabled = rbGenerados.Checked; btnImprimir.Enabled = rbGenerados.Checked;
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
                
                grid.DataSource = NegImagen.getAgendamientos(frango);
            }
            else if(rbGenerados.Checked)
            {
                grid.DataSource = NegImagen.getAgendamientosInformes(frango);
            }
           

           



        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (grid.Rows.Count > 0)
            {
                ImagenInforme x = new ImagenInforme(Convert.ToInt32(grid.ActiveRow.Cells["id"].Value));
                x.ShowDialog();
                //x.Show();
                actualizarGrid();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (grid.Rows.Count > 0)
            {
                ImprimirReportex(Convert.ToInt32(grid.ActiveRow.Cells["id"].Value));
            }
        }

        private void ImprimirReportex(int idImagenologia)
        {
            try
            {
                ReportesHistoriaClinica imagenL = new ReportesHistoriaClinica();
                imagenL.limpiarForm012();


                
                DataTable est = NegImagen.getForm012Estudios(idImagenologia);
                string estudios = "";
                int aux = 0;
                foreach (DataRow row in est.Rows)
                {
                    if (aux>0)
                        estudios += "   ,    ";
                    estudios += row["PRO_DESCRIPCION"].ToString();
                    aux++;
                }



                DataTable e = NegImagen.getForm012(idImagenologia);

                string[] x = new string[] {
                    Convert.ToString(e.Rows[0]["CLINICA"]),
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
                    Convert.ToString(e.Rows[0]["Radiologo"])
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
                ventana.Show();
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
    }
}
