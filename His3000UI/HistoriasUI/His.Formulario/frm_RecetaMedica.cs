using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Formulario;
using His.Entidades;

namespace His.Formulario
{
    public partial class frm_RecetaMedica : Form
    {
        NegCertificadoMedico Certificado = new NegCertificadoMedico();
        NegQuirofano Quirofano = new NegQuirofano();
        internal static string hc; //Historial Clinico del paciente
        internal static string ate_codigo; //almacena el codigo del paciente elegido
        internal static string identificacion; //almacena el nro de cedula
        private static string tipo_ingreso; //contiene el tipo de ingreso del paciente
        private static DateTime edad;
        private static string genero;
        private static string nacionalidad;
        private static string empresa;
        private static string variable = "";
        private static string ate_numero;
        private static string indi;
        public frm_RecetaMedica()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            DateTime Hoy = DateTime.Now;
            USUARIOS objusuarios = NegUsuarios.RecuperaUsuario(His.Entidades.Clases.Sesion.codUsuario);
            try
            {
                string FullPath;
                FullPath = "C:\\Sic3000\\Iconos\\LogoEmpresa.png"; //se creo la ruta por el hecho de que cuando era dinamico tomaba del servidor y cuando trataba de buscar en maquinas locales este no encontraba
                if (TablaMedicamentos.Rows.Count > 0)
                {
                    if (TablaDiagnostico.Rows.Count > 0)
                    {
                        DatosReceta DReceta = new DatosReceta();
                        DataRow drReceta;
                        indi = "";
                        foreach (DataGridViewRow item in TablaMedicamentos.Rows)
                        {

                            drReceta = DReceta.Tables["Receta"].NewRow();
                            var letras = NumeroALetras(Convert.ToDouble(item.Cells[0].Value));
                            drReceta["Medicamentos"] = item.Cells[1].Value.ToString() + " #" + item.Cells[0].Value.ToString() + " (" + letras + ") ";
                            drReceta["Empresa"] = empresa;
                            drReceta["Servicio"] = tipo_ingreso;
                            drReceta["Paciente"] = txt_apellido1.Text + " " + txt_apellido2.Text + " " + txt_nombre1.Text + " " + txt_nombre2.Text;
                            drReceta["HC"] = txt_historiaclinica.Text;
                            drReceta["Identificacion"] = identificacion;
                            if(genero == "F")
                            {
                                drReceta["Sexo"] = "FEMENINO";
                            }
                            else
                            {
                                drReceta["Sexo"] = "MASCULINO";
                            }
                            drReceta["Edad"] = Hoy.Year - edad.Year;
                            drReceta["Meses"] = Hoy.Month + (12 - edad.Month);
                            drReceta["Nacionalidad"] = nacionalidad;
                            drReceta["Cie10"] = cie10texto.Text;
                            drReceta["Medico"] = objusuarios.APELLIDOS + " " + objusuarios.NOMBRES;
                            drReceta["Medico_Identificacion"] = objusuarios.IDENTIFICACION;
                            indi += item.Cells[1].Value.ToString() + ": " + item.Cells[2].Value.ToString() + "\r\n";
                            drReceta["Indicaciones"] = indi;
                            drReceta["Logo"] = Certificado.path();
                            DReceta.Tables["Receta"].Rows.Add(drReceta);
                        }
                        His.Formulario.frmReportes reporte = new His.Formulario.frmReportes(1, "Receta", DReceta);
                        reporte.Show();
                        this.Close();
                    }
                    else
                    {
                        errorProvider1.SetError(TablaDiagnostico, "Se deben agregar diagnostico(s) al paciente.");
                    }
                }
                else
                {
                    errorProvider1.SetError(TablaMedicamentos, "Se deben agregar indicación(es) al paciente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            frm_Ayuda_Certificado.id_usuario = Convert.ToString(His.Entidades.Clases.Sesion.codUsuario);
            frm_Ayuda_Certificado x = new frm_Ayuda_Certificado();
            x.ShowDialog();
            if(x.hc != "")
            {
                hc = x.hc;
                ate_codigo = x.ate_codigo;
                txt_historiaclinica.Text = hc;
                ayudaPacientes.Enabled = false;
            }
        }

        private void txt_historiaclinica_TextChanged(object sender, EventArgs e)
        {
            if (txt_historiaclinica.Text != "")
            {
                DataTable Tabla = new DataTable(); //Almacenara los nombres y apellidos del paciente por hc
                Tabla = Certificado.BuscarPaciente(ate_codigo);
                foreach (DataRow item in Tabla.Rows)
                {
                    txt_apellido1.Text = item[0].ToString();
                    txt_apellido2.Text = item[1].ToString();
                    txt_nombre1.Text = item[2].ToString();
                    txt_nombre2.Text = item[3].ToString();
                    tipo_ingreso = item[5].ToString();
                    identificacion = item[6].ToString();
                    edad = Convert.ToDateTime(item[7].ToString());
                    nacionalidad = item[8].ToString();
                    genero = item[9].ToString();
                    empresa = item[10].ToString();
                    ate_numero = item[11].ToString();
                }
            }
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            DateTime Hoy = DateTime.Now;
            if (txt_historiaclinica.Text != "")
            {
                errorProvider1.Clear();
                frm_AyudaProductosMedicina ayuda = new frm_AyudaProductosMedicina();
                ayuda.atencion = ate_numero;
                ayuda.paciente = txt_apellido1.Text + " " + txt_apellido2.Text + " " + txt_nombre1.Text + " " + txt_nombre2.Text;
                ayuda.edad = (Hoy.Year - edad.Year).ToString();
                ayuda.hc = hc;
                ayuda.codigoArea = 1;
                ayuda._Rubro = 1;
                ayuda.ate_codigo = ate_codigo;
                ayuda._CodigoConvenio = 145;
                ayuda._CodigoEmpresa = 146;
                ayuda.ShowDialog();
                TablaMedicamentos.DataSource = ayuda.dt;
            }
            else
            {
                errorProvider1.SetError(txt_historiaclinica, "Por favor elija el HC del Paciente");
            }
        }
        private bool BuscarItem(string searchValue, DataGridView grid)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(searchValue))
                {
                    return true;
                }
            }
            return false;
        }

        private void btnañadir_Click(object sender, EventArgs e)
        {
            if (txt_historiaclinica.Text != "")
            {
                errorProvider1.Clear();
                if(TablaDiagnostico.Rows.Count <= 2)
                {
                    frm_BusquedaCIE10 x = new frm_BusquedaCIE10();
                    x.ShowDialog();


                    //frm_ImagenAyuda ayuda = new frm_ImagenAyuda("DIAGNOSTICOS");
                    //ayuda.ShowDialog();
                    if (x.codigo != string.Empty)
                    {
                        if (!BuscarItem(x.codigo, TablaDiagnostico))
                        {
                            this.TablaDiagnostico.Rows.Add(x.codigo, x.resultado);
                            variable += x.codigo + " " + x.resultado + "\r\n";
                            cie10texto.Text = variable;
                        }
                        else
                            MessageBox.Show("El item ya fue ingresado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No puede agregar más de 3 CIE10", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                errorProvider1.SetError(txt_historiaclinica, "Por favor elija el HC del Paciente");
            }
        }
        private static string NumeroALetras(double value)
        {
            string num2Text; value = Math.Truncate(value);
            if (value == 0) num2Text = "CERO";
            else if (value == 1) num2Text = "UNO";
            else if (value == 2) num2Text = "DOS";
            else if (value == 3) num2Text = "TRES";
            else if (value == 4) num2Text = "CUATRO";
            else if (value == 5) num2Text = "CINCO";
            else if (value == 6) num2Text = "SEIS";
            else if (value == 7) num2Text = "SIETE";
            else if (value == 8) num2Text = "OCHO";
            else if (value == 9) num2Text = "NUEVE";
            else if (value == 10) num2Text = "DIEZ";
            else if (value == 11) num2Text = "ONCE";
            else if (value == 12) num2Text = "DOCE";
            else if (value == 13) num2Text = "TRECE";
            else if (value == 14) num2Text = "CATORCE";
            else if (value == 15) num2Text = "QUINCE";
            else if (value < 20) num2Text = "DIECI" + NumeroALetras(value - 10);
            else if (value == 20) num2Text = "VEINTE";
            else if (value < 30) num2Text = "VEINTI" + NumeroALetras(value - 20);
            else if (value == 30) num2Text = "TREINTA";
            else if (value == 40) num2Text = "CUARENTA";
            else if (value == 50) num2Text = "CINCUENTA";
            else if (value == 60) num2Text = "SESENTA";
            else if (value == 70) num2Text = "SETENTA";
            else if (value == 80) num2Text = "OCHENTA";
            else if (value == 90) num2Text = "NOVENTA";
            else if (value < 100) num2Text = NumeroALetras(Math.Truncate(value / 10) * 10) + " Y " + NumeroALetras(value % 10);
            else if (value == 100) num2Text = "CIEN";
            else if (value < 200) num2Text = "CIENTO " + NumeroALetras(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) num2Text = NumeroALetras(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) num2Text = "QUINIENTOS";
            else if (value == 700) num2Text = "SETECIENTOS";
            else if (value == 900) num2Text = "NOVECIENTOS";
            else if (value < 1000) num2Text = NumeroALetras(Math.Truncate(value / 100) * 100) + " " + NumeroALetras(value % 100);
            else if (value == 1000) num2Text = "MIL";
            else if (value < 2000) num2Text = "MIL " + NumeroALetras(value % 1000);
            else if (value < 1000000)
            {
                num2Text = NumeroALetras(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0)
                {
                    num2Text = num2Text + " " + NumeroALetras(value % 1000);
                }
            }
            else if (value == 1000000)
            {
                num2Text = "UN MILLON";
            }
            else if (value < 2000000)
            {
                num2Text = "UN MILLON " + NumeroALetras(value % 1000000);
            }
            else if (value < 1000000000000)
            {
                num2Text = NumeroALetras(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0)
                {
                    num2Text = num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000) * 1000000);
                }
            }
            else if (value == 1000000000000) num2Text = "UN BILLON";
            else if (value < 2000000000000) num2Text = "UN BILLON " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            else
            {
                num2Text = NumeroALetras(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0)
                {
                    num2Text = num2Text + " " + NumeroALetras(value - Math.Truncate(value / 1000000000000) * 1000000000000);
                }
            }
            return num2Text;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ayudaPacientes.Enabled = true;
            Limpiar();
        }
        public void Limpiar()
        {
            TablaDiagnostico.Rows.Clear();
            TablaMedicamentos.DataSource = null;
            txt_historiaclinica.Text = "";
            txt_apellido1.Text = "";
            txt_apellido2.Text = "";
            txt_nombre1.Text = "";
            txt_nombre2.Text = "";
        }
    }
}
