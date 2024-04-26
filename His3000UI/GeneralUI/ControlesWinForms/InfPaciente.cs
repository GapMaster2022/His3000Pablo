using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades;
using His.General;

namespace GeneralApp.ControlesWinForms
{
    public partial class InfPaciente : UserControl
    {
        Int64 codigoAtencion;
        public InfPaciente(Int64 codAtencion)
        {
            InitializeComponent();
            codigoAtencion = codAtencion; 
        }

        private void ultraGroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void InfPaciente_Load(object sender, EventArgs e)
        {
            cargarInfPaciente();
        }

        private void cargarInfPaciente()
        {
            try
            {
                ATENCIONES atencion = NegAtenciones.RecuperarAtencionID(codigoAtencion);
                lblAtencion.Text = atencion.ATE_NUMERO_ATENCION.ToString(); 
                PACIENTES paciente = NegPacientes.RecuperarPacienteID(Convert.ToInt32(atencion.PACIENTES.PAC_CODIGO));

                lblInfPacienteNombre.Text = paciente.PAC_APELLIDO_PATERNO + " " +
                                      paciente.PAC_APELLIDO_MATERNO + " " +
                                      paciente.PAC_NOMBRE1 + " " +
                                      paciente.PAC_NOMBRE2;
                lblInfPacienteHCL.Text = paciente.PAC_HISTORIA_CLINICA;
                lblInfPacienteSexo.Text = paciente.PAC_GENERO;

                if (paciente.PAC_FECHA_NACIMIENTO != null)
                    lblInfPacienteEdad.Text = Funciones.CalcularEdad((DateTime)paciente.PAC_FECHA_NACIMIENTO).ToString();
                else
                    lblInfPacienteEdad.Text = "Desconocida";

                ASEGURADORAS_EMPRESAS aseguradora = NegAseguradoras.recuperaAseguradoraPorAtencion(codigoAtencion);
                if (aseguradora != null)
                    lblInfPacienteAseguradora.Text = aseguradora.ASE_NOMBRE;

                MEDICOS medico = NegMedicos.RecuperaMedicoId(atencion.MEDICOS.MED_CODIGO);
                lblInfPacienteMedico.Text = medico.MED_APELLIDO_PATERNO.Trim() + " " +
                    medico.MED_APELLIDO_MATERNO.Trim() + " " +
                    medico.MED_NOMBRE1.Trim() + " " + medico.MED_NOMBRE2.Trim();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }
    }
}
