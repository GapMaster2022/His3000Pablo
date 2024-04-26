using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Recursos;
using System.IO;

namespace His.Admision
{
    public partial class frm_ExploradorHabitaciones : Form
    {

        public frm_ExploradorHabitaciones()
        {
            InitializeComponent();
            cargarRecursos();
            cargarTipoIngreso();
            
        }

        private void frm_ExploradorHabitaciones_Load(object sender, EventArgs e)
        {
            //ultraGridHabitaciones.DataSource = Negocio.NegHabitaciones.listaHabitacionesOcupadas(1);   

        }

        public void cargarRecursos()
        {
            //this.tssMedicos.Image  = Recursos.Archivo.btnOrganigrama;  
            //imagenes del menu principal
            toolStripButtonBuscar.Image = Archivo.imgBtnBuscar32;
            toolStripButtonActualizar.Image = Archivo.imgBtnRestart;
            toolStripButtonImprimir.Image = Archivo.imgBtnImprimir32;
            toolStripButtonSalir.Image = Archivo.imgBtnSalir32;
            toolStripButtonExportar.Image = Archivo.imgOfficeExcel;
            uBtnBuscarPaciente.Appearance.Image = Archivo.imgBtnBuscar32;
        }

        private void cargarTipoIngreso()
        {
            //cboTipoIngreso.DataSource = Negocio.NegTipoIngreso.ListaTipoIngreso();
            //cboTipoIngreso.DisplayMember = "TIP_DESCRIPCION";
            //cboTipoIngreso.ValueMember = "TIP_CODIGO";
            //cboTipoIngreso.AutoCompleteSource = AutoCompleteSource.ListItems;
            //cboTipoIngreso.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

       
        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                //if (chbTipoIngreso.Checked == true)
                //{
                //    if (cboTipoIngreso.SelectedText == "HOSPITALIZACION")                   
                //        ultraGridHabitaciones.DataSource = Negocio.NegPacientes.RecuperarPacientesAtenciones(dtpFiltroDesde.Value, dtpFiltroHasta.Value, "HOSPITALIZACION");                   
                //    else
                //    {
                //        if (cboTipoIngreso.SelectedText == "PREADMISION")                       
                //            ultraGridHabitaciones.DataSource = Negocio.NegPacientes.RecuperarPacientesAtenciones(dtpFiltroDesde.Value, dtpFiltroHasta.Value, "PREADMISION");                        
                //        else
                //        {
                //            if (cboTipoIngreso.SelectedText == "EMERGENCIA")                           
                //                ultraGridHabitaciones.DataSource = Negocio.NegPacientes.RecuperarPacientesAtenciones(dtpFiltroDesde.Value, dtpFiltroHasta.Value, "EMERGENCIA");                            
                //            else 
                //            {
                //                if (cboTipoIngreso.SelectedText == "HOSPITAL DEL DIA")                                
                //                    ultraGridHabitaciones.DataSource = Negocio.NegPacientes.RecuperarPacientesAtenciones(dtpFiltroDesde.Value, dtpFiltroHasta.Value, "HOSPITAL DEL DIA");                                
                //                else 
                //                {
                //                    ultraGridHabitaciones.DataSource = Negocio.NegPacientes.RecuperarPacientesAtenciones(dtpFiltroDesde.Value, dtpFiltroHasta.Value, "CONSULTA EXTERNA");
                //                }

                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    ultraGridHabitaciones.DataSource = Negocio.NegPacientes.RecuperarPacientesAtenciones(dtpFiltroDesde.Value, dtpFiltroHasta.Value);
                //}
                //chbTipoIngreso.Checked = false;                

                //}
                //DateTime fechaDesde = dtpFiltroDesde.Value
                //ultraGridPacientes.DataSource = Negocio.NegPacientes.RecuperarPacientesAtenciones(dtpFiltroDesde.Value, dtpFiltroHasta.Value);
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        
        private void toolStripButtonExportar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (ultraGridHabitaciones.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(ultraGridHabitaciones, PathExcel);
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

        private void ultraGridPacientes_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void dtpFiltroDesde_ValueChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void chbTipoIngreso_CheckedChanged(object sender, EventArgs e)
        {
            //if (chbTipoIngreso.Checked == true)           
            //    cboTipoIngreso.Enabled = true;            
            //else
            //    cboTipoIngreso.Enabled = false;
        }

        
    }
}
