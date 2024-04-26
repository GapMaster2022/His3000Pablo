using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using System.IO;
using His.Entidades;
using System.Data;
using Recursos;


namespace His.Emergencia
{
    public partial class frm_InformeMorbilidad : Form
    {
        List<DtoHcEmergencias> listaEmergencias;
        public frm_InformeMorbilidad()
        {
            InitializeComponent();
            cargarDatos();
            btn_Imprimir.Image = Archivo.imgBtnImprimir32;
            btn_Salir.Image = Archivo.imgBtnSalir32;
            
        }

        private void frm_InformeMorbilidad_Load(object sender, EventArgs e)
        {            
        
        }

        private void cargarDatos() 
        {
            cb_Morbilidad.Items.Add("Diagnóstico");//0
            cb_Morbilidad.Items.Add("Diagnóstico en Base de ICD");//1
            cb_Morbilidad.Items.Add("Distribución Convenio MSP");//2
            cb_Morbilidad.Items.Add("Distribución Forma de Arribo");//3
            cb_Morbilidad.Items.Add("Distribución Horario de Atención");//4
            cb_Morbilidad.Items.Add("Distribución por Edad");//5
            cb_Morbilidad.Items.Add("Distribución por Médico");//6
            cb_Morbilidad.Items.Add("Distribución por Seguros Privados");//7
            cb_Morbilidad.Items.Add("Distribución por Sexo");//8
            cb_Morbilidad.Items.Add("Estancia Hospitalaria"); //9
            cb_Morbilidad.Items.Add("Grupos Etáreos");//10
            cb_Morbilidad.Items.Add("Horario de Ingresos");//11
            cb_Morbilidad.Items.Add("Ingresos Críticos (Traige del Paciente)");//12
            cb_Morbilidad.Items.Add("Ingreso por Diagnóstico");//13
            cb_Morbilidad.Items.Add("Ingresos por Especialidad");//14
            cb_Morbilidad.Items.Add("Pronóstico");//15
                       
          }

        private void btn_Consultar_Click(object sender, EventArgs e)
        {
            try
            {
                //dtpFiltroDesde.Value = Convert.ToDateTime(String.Format("{0:g}", "01/" + DateTime.Now.Month + "/" + (DateTime.Now.Year).ToString()));
                //dtpFiltroHasta.Value = DateTime.Now;
                listaEmergencias = new List<DtoHcEmergencias>();
                listaEmergencias = NegHcEmergencia.RecuperarHcEmergenciasPorFechas(dtpFiltroDesde.Value,dtpFiltroHasta.Value);  
                                  
                ultraGridPacientes.DataSource = listaEmergencias;                     
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
        }
        
        private void frm_InformeMorbilidad_Load_1(object sender, EventArgs e)
        {
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {
        }

        private void ultraSplitter1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Hola");            
        }

        private void btn_ConsultaMorb_Click(object sender, EventArgs e)
        {
            try
            {                
                if (cb_Morbilidad.SelectedIndex != -1)
                {
                    //ultraChartEmergencias.Visible = true;
                    //ultraGridPacientes.Visible = false;
                    if (cb_Morbilidad.SelectedIndex == 0)
                    {
                        lblNombreReporte.Text = "Ingreso por Diagnóstico";
                        var listaCli = (from hc in listaEmergencias
                                      where (hc.clinicas >= 0)
                                      group hc by hc.clinicas into u
                                      select new { estado = "Esp. Clinicas", cantidad = u.Count() }).ToList();
                        var listaQui= (from hc in listaEmergencias
                                      where (hc.quirur >= 0)
                                      group hc by hc.quirur into em
                                      select new { estado = "Esp. Quirúrgicas", cantidad = em.Count() }).ToList();
                        var listaOtras = (from hc in listaEmergencias
                                      where (hc.otrasEsp >= 0)
                                      group hc by hc.otrasEsp into c
                                      select new { estado = "Otras Esp.", cantidad = c.Count() }).ToList();                        
                        var lista = listaCli.Union(listaQui).Union(listaOtras);
                        if (lista.Count() > 0)
                            ultraChartEmergencias.DataSource = lista.ToList();
                    }
                    if (cb_Morbilidad.SelectedIndex == 5)
                    {
                        lblNombreReporte.Text = "Descripción por Edad";
                        var lista1 = (from hc in listaEmergencias
                                      where (hc.edad <= 20)
                                      group hc by hc.edad into e1
                                      select new { estado = "< 20", cantidad = e1.Count() }).ToList();
                        var lista2 = (from hc in listaEmergencias
                                      where (hc.edad >= 20 || hc.edad <= 39)
                                      group hc by hc.edad into e2
                                      select new { estado = "20 - 39", cantidad = e2.Count() }).ToList();
                        var lista3 = (from hc in listaEmergencias
                                      where (hc.edad >= 40 || hc.edad <= 59)
                                      group hc by hc.edad into e3
                                      select new { estado = "40 - 59", cantidad = e3.Count() }).ToList();
                        var lista4 = (from hc in listaEmergencias
                                      where (hc.edad >= 60 || hc.edad <= 79)
                                      group hc by hc.edad into e4
                                      select new { estado = "60 - 79", cantidad = e4.Count() }).ToList();
                        var lista5 = (from hc in listaEmergencias
                                      where (hc.edad >= 80)
                                      group hc by hc.edad into e5
                                      select new { estado = "> 80", cantidad = e5.Count() }).ToList();
                        var lista = lista1.Union(lista2).Union(lista3).Union(lista4).Union(lista5);
                        if (lista.Count() > 0)
                            ultraChartEmergencias.DataSource = lista.ToList();
                    }
                    if (cb_Morbilidad.SelectedIndex == 8)
                    {
                        lblNombreReporte.Text = "Distribución por Sexo";
                        var listam = (from hc in listaEmergencias
                                      where (hc.genero == "M")
                                      group hc by hc.genero into g1
                                      select new { genero = "Masculino", cantidad = g1.Count() }).ToList();
                        var listaf = (from hc in listaEmergencias
                                      where (hc.genero == "F")
                                      group hc by hc.genero into g2
                                      select new { genero = "Femenino", cantidad = g2.Count() }).ToList();
                        int total = listam.Count() + listaf.Count();
                        var lista = listam.Union(listaf);
                        if (lista.Count() > 0)
                            ultraChartEmergencias.DataSource = lista.ToList();
                            //ultraChartEmergencias.DataSource = total.ToString();
                            
                    }
                    if (cb_Morbilidad.SelectedIndex == 12)
                    {
                        lblNombreReporte.Text = "Ingresos Críticos";
                        var listaU = (from hc in listaEmergencias
                                      where hc.urgente.Equals(true)
                                      group hc by hc.urgente into u
                                      select new { estado = "No Urgente", cantidad = u.Count() }).ToList();
                        var listaE = (from hc in listaEmergencias
                                      where hc.emergente.Equals(true)
                                      group hc by hc.emergente into em
                                      select new { estado = "Emergente", cantidad = em.Count() }).ToList();
                        var listaC = (from hc in listaEmergencias
                                      where hc.critico.Equals(true)
                                      group hc by hc.critico into c
                                      select new { estado = "Critico", cantidad = c.Count() }).ToList();
                        var listaO = (from hc in listaEmergencias
                                      where hc.otras != string.Empty
                                      group hc by hc.otras into o
                                      select new { estado = "Otros", cantidad = o.Count() }).ToList();
                        var lista = listaU.Union(listaE).Union(listaC).Union(listaO);
                        if (lista.Count() > 0)
                            ultraChartEmergencias.DataSource = lista.ToList();
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un Tipo de Reporte");
                    cb_Morbilidad.Focus();
                }               
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error al cargar la datos");
            }
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ultraChartEmergencias_ChartDataClicked(object sender, Infragistics.UltraChart.Shared.Events.ChartDataEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}

