using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Formulario;
using Recursos;
using His.Negocio;
using His.Entidades;
using His.General; 

namespace His.AdminHistoriasClinicas
{
    public partial class txtNombrePaciente : Form
    {
        ATENCIONES atencion = new ATENCIONES();
        List<FORMULARIOS_HCU> formularios = new List<FORMULARIOS_HCU>();
        private Timer timerReloj;
        private int codigoAtencion;
        public txtNombrePaciente()
        {
            InitializeComponent();
        }

        public txtNombrePaciente(int parCodigoAtencion)
        {
            InitializeComponent();
            codigoAtencion = parCodigoAtencion;
        }
        
        private void ultraExplorerBar1_ItemClick(object sender, Infragistics.Win.UltraWinExplorerBar.ItemEventArgs e)
        {
            switch(e.Item.Key.ToString())
            {
                case "AdmisionAltaEgreso":
                    var frm = new frmReportes();
                    frm.parametro = codigoAtencion;
                    frm.reporte = "admision";
                    frm.ShowDialog(); 
                    break;
                case "Anamnesis":
                    frm_Anemnesis anemeasis = new frm_Anemnesis(codigoAtencion);
                    anemeasis.MdiParent = this;
                    anemeasis.Show();  
                    break;
                case "EvolucionPrescripciones":
                    frm_Evolucion evolucion = new frm_Evolucion(codigoAtencion,false);
                    evolucion.MdiParent = this;
                    evolucion.Show();  
                    break;
                case "Epicrisis":
                    frm_Epicrisis epicrisis = new frm_Epicrisis(codigoAtencion);
                    epicrisis.MdiParent = this;
                    epicrisis.Show();  
                    break;
                case "ProtocoloDeOperacion":
                    bool Resultado;
                    List<DtoProtocolos> Protocolos = new List<DtoProtocolos>();
                    //DataTable Protocolos = new DataTable();
                    Protocolos= NegAtenciones.RecuperarProtocolos(codigoAtencion);
                    dgrProtocolosOperacion.DataSource = Protocolos;

                    gbxProtocolos.Visible = true;

                    foreach (DataGridViewRow row in dgrProtocolosOperacion.Rows)
                    {
                        Resultado = NegAtenciones.VerificaProtocolos(codigoAtencion, Convert.ToInt32(row.Cells["Codigo"].Value));
                        if (Resultado == false)
                        {
                            row.DefaultCellStyle.BackColor = Color.Red;
                        }
                    }

                    //frm_Protocolo protocolo = new frm_Protocolo(codigoAtencion,1);
                    //protocolo.MdiParent = this;
                    //protocolo.Show();  



                    break;
                case "Emergencia":
                    frm_Emergencia emergencia = new frm_Emergencia();
                    emergencia.MdiParent = this;
                    emergencia.Show();  
                    break;
                case "Interconsulta":
                    frm_Interconsulta interconsulta = new frm_Interconsulta(codigoAtencion);
                    interconsulta.MdiParent = this;
                    interconsulta.Show();  
                    break;
                case "Laboratorio":
                    frm_LaboratorioClinico laboratorio = new frm_LaboratorioClinico();
                    laboratorio.MdiParent = this;
                    laboratorio.Show();
                    break;
                default:
                    break; 
            }

        }

        private void frmExploradorHC_Load(object sender, EventArgs e)
        {
            ultraExplorerBar1.ItemSettings.UseDefaultImage = Infragistics.Win.DefaultableBoolean.False;
            ultraExplorerBar1.ItemSettings.AppearancesLarge.Appearance.Image = Archivo.imgBtnGoneOpen48;
            //inicializo el reloj de la barra de Estado
            //timerReloj = new Timer();
            //timerReloj.Interval = TimeSpan.FromSeconds(1.0).Seconds;
            //timerReloj.Start();
            //timerReloj.Tick += new EventHandler(delegate(object s, EventArgs a)
            //{

            //    ultraLabelReloj.Text = "" + DateTime.Now.Hour + ":"
            //  + DateTime.Now.Minute + ":"
            //  + DateTime.Now.Second;


            //});
            //cargo la informacion del paciente
            cargarInfPaciente();
        }

        private void cargarInfPaciente()
        {
            try
            {
                atencion = NegAtenciones.RecuperarAtencionID(codigoAtencion);
                PACIENTES  paciente = NegPacientes.RecuperarPacienteID(atencion.PACIENTES.PAC_CODIGO);
                
                   lblInfPacienteNombre.Text = paciente.PAC_APELLIDO_PATERNO + " " +
                                         paciente.PAC_APELLIDO_MATERNO + " " +
                                         paciente.PAC_NOMBRE1 + " " +
                                         paciente.PAC_NOMBRE2;
                   lblInfPacienteHCL.Text = paciente.PAC_HISTORIA_CLINICA;
                   lblInfPacienteSexo.Text = paciente.PAC_GENERO;

                   if(paciente.PAC_FECHA_NACIMIENTO!=null)
                        lblInfPacienteEdad.Text = Funciones.CalcularEdad((DateTime)paciente.PAC_FECHA_NACIMIENTO).ToString();
                   else
                        lblInfPacienteEdad.Text = "Desconocida";

                ASEGURADORAS_EMPRESAS aseguradora = NegAseguradoras.recuperaAseguradoraPorAtencion(codigoAtencion); 
                    if (aseguradora != null)
                        lblInfPacienteAseguradora.Text = aseguradora.ASE_NOMBRE;
                 
                MEDICOS medico = NegMedicos.RecuperaMedicoId(codigoAtencion);
                if (medico != null)
                {
                    lblInfPacienteMedico.Text = medico.MED_APELLIDO_PATERNO.Trim() + " " +
                        medico.MED_APELLIDO_MATERNO.Trim() + " " +
                        medico.MED_NOMBRE1.Trim() + " " + medico.MED_NOMBRE2.Trim();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message );  
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label45_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            gbxProtocolos.Visible = false;
        }


        private void dgrProtocolosOperacion_DoubleClick(object sender, EventArgs e)
        {
            Int32 Atencion=0;
            Int32 Protocolo=0;

            //Atencion = Convert.ToInt16(this.dgrProtocolosOperacion[Convert.ToInt32(dgrProtocolosOperacion.CurrentRow.Index), 1].Value);
            //Protocolo = Convert.ToInt16(this.dgrProtocolosOperacion[Convert.ToInt32(dgrProtocolosOperacion.CurrentRow.Index), 0].Value);

            Protocolo = Convert.ToInt32(dgrProtocolosOperacion.CurrentRow.Cells["Codigo"].Value);
            Atencion = Convert.ToInt32(dgrProtocolosOperacion.CurrentRow.Cells["CodigoAtencion"].Value);

            frm_Protocolo protocolo = new frm_Protocolo(Atencion, Protocolo);
            protocolo.MdiParent = this;
            gbxProtocolos.Visible = false;
            protocolo.Show();  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ATENCION_DETALLE_FORMULARIOS_HCU detalle = new ATENCION_DETALLE_FORMULARIOS_HCU();
            formularios = NegFormulariosHCU.RecuperarFormulariosHCU();
            FORMULARIOS_HCU formul = formularios.FirstOrDefault(f => f.FH_CODIGO == 21);
            detalle.ADF_CODIGO = NegAtencionDetalleFormulariosHCU.MaxCodigo() + 1;
            detalle.ATENCIONESReference.EntityKey = atencion.EntityKey;
            detalle.REF_CODIGO = 0;
            detalle.FORMULARIOS_HCUReference.EntityKey = formul.EntityKey;
            detalle.ADF_FECHA_INGRESO = DateTime.Now;
            detalle.ADF_ESTADO = true;
            detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;

            NegAtencionDetalleFormulariosHCU.Crear(detalle);

            this.gbxProtocolos.Visible = false;

            frm_Protocolo protocolo = new frm_Protocolo(codigoAtencion, detalle.ADF_CODIGO);
            protocolo.MdiParent = this;
            protocolo.Show(); 
        }

    }
}

