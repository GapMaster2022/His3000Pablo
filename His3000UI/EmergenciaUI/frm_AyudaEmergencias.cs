using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Negocio;
using His.Entidades.Clases;
using Recursos;
using His.General;
using System.Reflection;
using His.Parametros;
using His.Formulario;
using Infragistics.Win.UltraWinGrid;



namespace His.Emergencia
{
    public partial class frm_AyudaEmergencias : Form
    {
        //List<HC_EMERGENCIA> listaEmergencias = new List<HC_EMERGENCIA>();
        List<HC_EMERGENCIA> lista = new List<HC_EMERGENCIA>();
        public HC_EMERGENCIA nuevaEmergenciaBuscada ;
        PACIENTES paciente = new PACIENTES();
        DataTable dtCatalogoEmergencias = new DataTable();
        public int numeroEmergencia;

        int codigoPaciente;
        string nombrePaciente;
        
        DataTable dtCatalogoEmergencia = new DataTable();
       
        public frm_AyudaEmergencias(int codigoPaciente, string nombrePaciente)
        {
            InitializeComponent();
            this.codigoPaciente = codigoPaciente;
            this.nombrePaciente = nombrePaciente;
            cbx_Filtrar.SelectedIndex = 0;
            buscar(codigoPaciente, nombrePaciente);            
        }

        private void buscar(int codigoPaciente,string nombrePaciente)
        {
            try
            {                
                dtCatalogoEmergencias = new DataTable();
                dataGridViewEmergencias.DataSource = dtCatalogoEmergencias;
                lista = NegHcEmergencia.RecuperarHcEmergencias(codigoPaciente);
                txt_Paciente.Text = nombrePaciente;   
                if (cbx_Filtrar.SelectedItem != null)
                {
                    if (cbx_Filtrar.SelectedItem.Equals("10"))
                    {
                        if (lista.Count > 10)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                HC_EMERGENCIA emergencia = new HC_EMERGENCIA();
                                emergencia = lista.ElementAt(i);
                                paciente = emergencia.PACIENTES;
                                cargarEmergencias(emergencia);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < lista.Count; i++)
                            {
                                HC_EMERGENCIA emergencia = new HC_EMERGENCIA();
                                emergencia = lista.ElementAt(i);
                                paciente = emergencia.PACIENTES;
                                cargarEmergencias(emergencia);
                            }
                        }
                    }
                    else
                    {
                        dataGridViewEmergencias.DataSource = null;
                        for (int i = 0; i < lista.Count; i++)
                        {
                            HC_EMERGENCIA emergencia = new HC_EMERGENCIA();
                            emergencia = lista.ElementAt(i);
                            paciente = emergencia.PACIENTES;
                            cargarEmergencias(emergencia);
                        }
                    }
                }            
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cargarEmergencias(HC_EMERGENCIA emergenciaNueva)
        {
            try
            {               
                DataRow drCatalogoEmergencias;
                dataGridViewEmergencias.DataSource = dtCatalogoEmergencias;
                dataGridViewEmergencias.DataSource = dtCatalogoEmergencias;
                if (dataGridViewEmergencias.Rows.Count == 0)
                {
                    dtCatalogoEmergencias.Columns.Add("EMERGENCIAS", Type.GetType("System.String"));
                    dtCatalogoEmergencias.Columns.Add("FECHA FUM", Type.GetType("System.String"));
                    dtCatalogoEmergencias.Columns.Add("FECHA INICIO", Type.GetType("System.String"));
                    //dtCatalogoEmergencias.Columns.Add("OBSERVACIONES", Type.GetType("System.String"));
                }
                if (dataGridViewEmergencias.Rows.Count >= 1)
                {
                    drCatalogoEmergencias = dtCatalogoEmergencias.NewRow();
                    drCatalogoEmergencias["EMERGENCIAS"] = emergenciaNueva.COD_EMERGENCIA;
                    drCatalogoEmergencias["FECHA FUM"] = emergenciaNueva.EME_FUM;
                    drCatalogoEmergencias["FECHA INICIO"] = emergenciaNueva.EME_FECHA_INICIO;                    
                    dtCatalogoEmergencias.Rows.Add(drCatalogoEmergencias);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }  
        

        public void retornarEmergencia(int idCodigo)
        {
            for (int i = 0; i < lista.Count; i++ )
            {
                HC_EMERGENCIA emergencia = new HC_EMERGENCIA();
                emergencia = lista.ElementAt(i);
                if(emergencia.COD_EMERGENCIA == idCodigo)
                {
                    nuevaEmergenciaBuscada = new HC_EMERGENCIA();
                    nuevaEmergenciaBuscada = emergencia;
                }
            }
        }

        private void dataGridViewEmergencias_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if(dataGridViewEmergencias.CurrentRow.Cells[0] !=  null) 
                {
                    int codigo = Convert.ToInt32(dataGridViewEmergencias.CurrentCell.Value.ToString());
                    numeroEmergencia = codigo;
                    retornarEmergencia(numeroEmergencia);
                    //MessageBox.Show("# :" + numeroEmergencia);
                    this.Close();
                }          
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Seleccione el Número de Emergencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
           buscar(codigoPaciente,nombrePaciente);
        }

        private void frm_AyudaEmergencias_Load(object sender, EventArgs e)
        {

        }
    }
}
