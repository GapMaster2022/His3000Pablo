using His.Formulario;
using His.Negocio;
using His.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace His.Honorarios
{
    public partial class frm_CertificadoPresentacion : Form
    {
        internal static string hc; //Historial Clinico del paciente
        internal static string atencion;
        public bool Pacientes = false;
        NegCertificadoMedico Certificado = new NegCertificadoMedico();
        ATENCIONES atencionActual = new ATENCIONES();
        PACIENTES paciente = new PACIENTES();
        MEDICOS medico = new MEDICOS();
        public bool abre = true;
        public frm_CertificadoPresentacion()
        {
            InitializeComponent();
            MEDICOS med = new MEDICOS();
            med = NegMedicos.RecuperaMedicoIdUsuario(His.Entidades.Clases.Sesion.codUsuario);
            var val = med.USUARIOSReference.EntityKey.EntityKeyValues[0].Value;
            if (Convert.ToInt32(val) == 1)
            {
                MessageBox.Show("Ud. No tiene acceso a generar certificados medicos ya que no esta registrado como un usuario medico", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                abre = false;
                this.Close();
                return;
            }
        }

        private void ultraGroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            frm_Ayuda_Certificado.id_usuario = Convert.ToString(His.Entidades.Clases.Sesion.codUsuario);
            frm_Ayuda_Certificado x = new frm_Ayuda_Certificado();
            if (Pacientes)
                x.emergencia = true;
            x.ShowDialog();
            x.FormClosed += X_FormClosed;
            CargarTipoContingencia();
            cmbTipos.SelectedIndex = -1;
            hc = x.hc;
            atencion = x.ate_codigo;
            if (hc.Trim() != "")
            {
                CERTIFICADO_PRESENTACION cert = new CERTIFICADO_PRESENTACION();
                cert = NegCertificadoMedico.RecuperarCertificadoPresentacion(Convert.ToInt64(x.ate_codigo), Entidades.Clases.Sesion.codUsuario);
                if (cert == null)
                {
                    txt_historiaclinica.Text = hc.Trim();
                    ultraGroupBox1.Enabled = true;
                    if (txt_historiaclinica.Text != "")
                    {

                        atencionActual = NegAtenciones.RecuepraAtencionNumeroAtencion2(x.ate_codigo);
                        paciente = NegPacientes.RecuperarPacienteID(x.hc);
                        try
                        {
                            var fec = Convert.ToString(atencionActual.ATE_FECHA_ALTA.Value) ?? "";
                            txtfechaalta.Text = atencionActual.ATE_FECHA_ALTA.ToString();
                        }
                        catch
                        {
                            txtfechaalta.Text = Convert.ToString(DateTime.Now);
                            //MessageBox.Show("No se puede generar un certificado de atencion si el paciente esta con atencion activa", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            //txt_historiaclinica.Text = "";
                            //return;
                        }

                        txt_apellido1.Text = paciente.PAC_APELLIDO_PATERNO;
                        txt_apellido2.Text = paciente.PAC_APELLIDO_MATERNO;
                        txt_nombre1.Text = paciente.PAC_NOMBRE1;
                        txt_nombre2.Text = paciente.PAC_NOMBRE2;
                        TIPO_INGRESO tip = NegAtenciones.RecuperaTipoIngreso(x.ate_codigo);
                        txt_Atencion.Text = tip.TIP_DESCRIPCION;
                        medico = NegMedicos.RecuperaMedicoIdUsuario(Entidades.Clases.Sesion.codUsuario);
                        textBox1.Text = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO + ' ' + medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                        txtfechaingreso.Text = atencionActual.ATE_FECHA_INGRESO.ToString();

                        ultraGroupBox1.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Paciente Cuenta con Certificado medico elaborado por ud.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                }


            }
            else
            {
                limpiar();
            }
        }

        public void CargarTipoContingencia()
        {
            List<TIPO_INGRESO> tipoIngreso = new List<TIPO_INGRESO>();
            tipoIngreso = NegTipoIngreso.ListaTipoIngreso();
            cmbTipos.DataSource = tipoIngreso;
            cmbTipos.ValueMember = "TIP_CODIGO";
            cmbTipos.DisplayMember = "TIP_DESCRIPCION";
            cmbTipos.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbTipos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

        }

        private void X_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (hc != null)
            {
                txt_historiaclinica.Text = hc;
                ultraGroupBox1.Enabled = true;
                //Tratamiento.Enabled = true;
                ayudaPacientes.Enabled = false;
            }
            else
            {
                limpiar();
            }
        }

        public void limpiar()
        {
            txt_apellido1.Text = "";
            txt_apellido2.Text = "";
            txt_nombre1.Text = "";
            txt_nombre2.Text = "";
            txt_historiaclinica.Text = "";
            txtfechaalta.Text = "";
            txtfechaingreso.Text = "";
            textBox1.Text = "";
            txt_Atencion.Text = "";
        }

        private void txt_historiaclinica_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (txt_historiaclinica.Text == "")
            {
                MessageBox.Show("Seleccione un paciente y llene la informacion para poder generar el Certificado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            //if (cmbTipos.Text == "")
            //{
            //    MessageBox.Show("Escoja un tipo de atencion", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}
            CERTIFICADO_PRESENTACION cert = new CERTIFICADO_PRESENTACION();
            cert.hc = Convert.ToInt64(txt_historiaclinica.Text);
            cert.ate_codigo = Convert.ToInt64(atencionActual.ATE_NUMERO_ATENCION);
            cert.apellido1 = txt_apellido1.Text;
            cert.apellido2 = txt_apellido2.Text;
            cert.nombre1 = txt_nombre1.Text;
            cert.nombre2 = txt_nombre2.Text;
            cert.fechaIngreso = Convert.ToDateTime(txtfechaingreso.Text);
            cert.fechaAlta = Convert.ToDateTime(txtfechaalta.Text);
            cert.medico = Entidades.Clases.Sesion.codUsuario;
            cert.tipo = txt_Atencion.Text;
            cert.cedula = paciente.PAC_IDENTIFICACION;

            if (NegCertificadoMedico.GuardaCertificadoPresentacion(cert))
            {
                string path = "";
                int ingreso = NegTipoIngreso.RecuperarporAtencion(Convert.ToInt64(cert.ate_codigo));
                switch (ingreso)
                {
                    case 10:
                        path = NegUtilitarios.RutaLogo("Mushuñan");
                        break;
                    case 12:
                        path = NegUtilitarios.RutaLogo("BrigadaMedica");
                        break;
                    default:
                        path = Certificado.path();
                        break;
                }
                CerificadoPrecentacionDatos CM = new CerificadoPrecentacionDatos();
                CERTIFICADO_PRESENTACION certPre = new CERTIFICADO_PRESENTACION();
                DataTable espMedicas = NegMedicos.RecuperaEspecialidadMed(Convert.ToInt32(medico.MED_CODIGO));
                string especialidadesMed = espMedicas.Rows[0][0].ToString();
                certPre = NegCertificadoMedico.RecuperarCertificadoPresentacion(cert.ate_codigo, cert.medico);
                DataRow drCertificado;
                drCertificado = CM.Tables["Certificado"].NewRow();
                drCertificado["nombre"] = cert.apellido1 + " " + cert.apellido2 + " " + cert.nombre1 + " " + cert.nombre2;
                drCertificado["cedula"] = cert.cedula;
                drCertificado["fechaIngreso"] = cert.fechaIngreso.ToString().Substring(0, 16) +" "+ Fecha_En_Palabra(Convert.ToDateTime(cert.fechaIngreso.ToString()).ToShortDateString());
                drCertificado["fechaAlta"] = cert.fechaAlta.ToString().Substring(0, 16) +" "+ Fecha_En_Palabra(Convert.ToDateTime(cert.fechaAlta.ToString()).ToShortDateString());
                drCertificado["consulta"] = cert.tipo;
                drCertificado["medico"] = textBox1.Text;
                drCertificado["imagen"] = path;
                drCertificado["id"] = certPre.id;
                drCertificado["hc"] = cert.hc;
                drCertificado["identificacion"] = medico.MED_RUC.Substring(0, 10);
                drCertificado["especialidad"] = especialidadesMed;
                drCertificado["email"] = medico.MED_EMAIL;
                drCertificado["telefono"] = medico.MED_TELEFONO_CONSULTORIO;
                drCertificado["fechaEmision"] = cert.fechaAlta.ToString().Substring(0, 16) + " " + Fecha_En_Palabra(Convert.ToDateTime(cert.fechaAlta.ToString()).ToShortDateString());//Fecha_Actual_En_Palabra(Convert.ToDateTime(cert.fechaAlta.ToString()).ToShortDateString());
                CM.Tables["Certificado"].Rows.Add(drCertificado);

                His.Formulario.frmReportes myreport = new His.Formulario.frmReportes(1, "CertificadoPresentacion", CM);
                myreport.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Algo ocurrio al guardar la informacion vuelva a intentar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void txtfechaalta_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        public string Fecha_Actual_En_Palabra(string fecha)
        {
            string fechaprueba;
            CultureInfo cul = new CultureInfo("es");
            DateTime FI = Convert.ToDateTime(fecha, cul);
            if (Convert.ToInt32(FI.ToString("dd")) == 1)
            {
                fechaprueba = FI.ToString("dddd") + " 1 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToInt32(FI.ToString("dd")) == 2)
            {
                fechaprueba = FI.ToString("dddd") + " 2  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 3)
            {
                fechaprueba = FI.ToString("dddd") + " 3  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 4)
            {
                fechaprueba = FI.ToString("dddd") + " 4  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 5)
            {
                fechaprueba = FI.ToString("dddd") + " 5  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 6)
            {
                fechaprueba = FI.ToString("dddd") + " 6  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 7)
            {
                fechaprueba = FI.ToString("dddd") + " 7  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 8)
            {
                fechaprueba = FI.ToString("dddd") + " 8  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 9)
            {
                fechaprueba = FI.ToString("dddd") + " 9  de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 10)
            {
                fechaprueba = FI.ToString("dddd") + " 10 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 11)
            {
                fechaprueba = FI.ToString("dddd") + " 11 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 12)
            {
                fechaprueba = FI.ToString("dddd") + " 12 (DOCE) de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 13)
            {
                fechaprueba = FI.ToString("dddd") + " 13 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 14)
            {
                fechaprueba = FI.ToString("dddd") + " 14 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 15)
            {
                fechaprueba = FI.ToString("dddd") + " 15 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 16)
            {
                fechaprueba = FI.ToString("dddd") + " 16 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 17)
            {
                fechaprueba = FI.ToString("dddd") + " 17 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 18)
            {
                fechaprueba = FI.ToString("dddd") + " 18 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 19)
            {
                fechaprueba = FI.ToString("dddd") + " 19 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 20)
            {
                fechaprueba = FI.ToString("dddd") + " 20 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 21)
            {
                fechaprueba = FI.ToString("dddd") + " 21 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 22)
            {
                fechaprueba = FI.ToString("dddd") + " 22 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 23)
            {
                fechaprueba = FI.ToString("dddd") + " 23 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 24)
            {
                fechaprueba = FI.ToString("dddd") + " 24 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 25)
            {
                fechaprueba = FI.ToString("dddd") + " 25 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 26)
            {
                fechaprueba = FI.ToString("dddd") + " 26 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 27)
            {
                fechaprueba = FI.ToString("dddd") + " 27 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 28)
            {
                fechaprueba = FI.ToString("dddd") + " 28 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 29)
            {
                fechaprueba = FI.ToString("dddd") + " 29 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 30)
            {
                fechaprueba = FI.ToString("dddd") + " 30 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            if (Convert.ToUInt32(FI.ToString("dd")) == 31)
            {
                fechaprueba = FI.ToString("dddd") + " 31 de " + FI.ToString("MMMM") + " del " + FI.ToString("yyyy");
                return fechaprueba;
            }
            else
            {
                return "";
            }
        }

        #region Fecha en Palabras
        public string Fecha_En_Palabra(string fecha)
        {
            string fechaprueba;
            CultureInfo cul = new CultureInfo("es");
            DateTime FI = Convert.ToDateTime(fecha, cul);
            string diaEnPalabras = ConvertirDiaEnPalabras(FI.Day);

            string anioEnPalabras = ConvertirAnioEnPalabras(FI.Year);

            fechaprueba = "(" + diaEnPalabras + " de " + FI.ToString("MMMM") + " del " + anioEnPalabras + ")";
            return fechaprueba;

        }

        private string ConvertirDiaEnPalabras(int dia)
        {
            string[] numeros = {"", "Primero", "Dos", "Tres", "Cuatro", "Cinco", "Seis", "Siete", "Ocho", "Nueve",
                        "Diez", "Once", "Doce", "Trece", "Catorce", "Quince", "Dieciseis", "Diecisiete",
                        "Dieciocho", "Diecinueve", "Veinti", "Veintiuno", "Veintidos", "Veintitres",
                        "Veinticuatro", "Veinticinco", "Veintiseis", "Veintisiete", "Veintiocho", "Veintinueve", "Treinta", "Treinta y uno"};
            return numeros[dia];
        }

        // Función para convertir el año a palabras (solo para los años hasta el 2999)
        private string ConvertirAnioEnPalabras(int anio)
        {
            string[] unidades = { "", "un", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve" };
            string[] decenas = { "", "diez", "veinti", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa" };
            string[] especiales = { "diez", "once", "doce", "trece", "catorce", "quince", "dieciseis", "diecisiete", "dieciocho", "diecinueve" };

            if (anio >= 1000 && anio <= 2999)
            {
                int unidad = anio % 10;
                int decena = (anio / 10) % 10;
                int centena = (anio / 100) % 10;
                int millar = (anio / 1000) % 10;

                string anioEnPalabras = "";

                if (millar > 0)
                    anioEnPalabras += unidades[millar] + " mil ";

                if (centena > 0)
                    anioEnPalabras += unidades[centena] + " cientos ";

                if (decena > 0)
                {
                    if (decena == 1)
                        anioEnPalabras += especiales[unidad];
                    else
                    {
                        if (unidad > 0)
                            anioEnPalabras += decenas[decena] +  unidades[unidad];
                        else
                            anioEnPalabras += decenas[decena];
                    }
                }
                else
                {
                    if (unidad > 0)
                        anioEnPalabras += unidades[unidad];
                }

                return anioEnPalabras;
            }
            else
            {
                return "Año no soportado";
            }
        }
        #endregion
    }
}
