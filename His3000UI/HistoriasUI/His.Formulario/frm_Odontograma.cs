using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Reportes;
using His.Negocio;
using Recursos;
using GeneralApp.ControlesWinForms;
using His.Parametros;
using His.General;
using His.Entidades.Clases;
using His.DatosReportes;
using His.Formulario;
using System.Reflection;
//using His.DatosReportes;

namespace His.Formulario
{
    public partial class frm_Odontograma : Form
    {
        public frm_Odontograma()
        {
            //InitializeComponent();
            //inicializar();
        }

        //private void inicializar()
        //{
        //    btnNuevo.Image = Archivo.imgBtnAdd2;
        //    btnGuardar.Image = Archivo.imgBtnGoneSave48;
        //    btnImprimir.Image = Archivo.imgBtnGonePrint48;
        //    btnSalir.Image = Archivo.imgBtnGoneExit48;
        //    btnCancelar.Image = Archivo.btnCancel16;
            

        //}

        //private void btnSalir_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        //private void ultraPanel1_PaintClient(object sender, PaintEventArgs e)
        //{

        //}

        //private void ayudaPacientes_Click(object sender, EventArgs e)
        //{
          
        //}

        //private void btnImprimir_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //recupero la informacion para el informe
        //        ReporteOdontologia reporte = new ReporteOdontologia();
        //        PACIENTES paciente = new PACIENTES(); 
        //        paciente = NegPacientes.RecuperarPacienteID(178618);

        //        reporte.ForEstablecimiento = Sesion.nomEmpresa;
        //        reporte.ForNombres = paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
        //        reporte.ForApellidos = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO;
        //        reporte.ForSexo = paciente.PAC_GENERO;
        //        reporte.ForNumeroHoja = 1;
        //        reporte.ForNumeroHistoria = Convert.ToString(paciente.PAC_HISTORIA_CLINICA);
                

        //        ReportesHistoriaClinica reporteOdontologia = new ReportesHistoriaClinica();
        //        reporteOdontologia.ingresarOdontologia(reporte);
        //        frmReportes ventana = new frmReportes(1, "anamnesis");
        //        ventana.Show();
        //    }
        //    catch (Exception err)
        //    { MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        //}        
    }
}
