using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades;
using System.IO;
using His.Formulario;

namespace His.Dietetica
{
    public partial class frmQuirofanoRegistro : Form
    {
        NegQuirofano Quirofano = new NegQuirofano();
        internal static string nom_paciente; //Variable que contiene el nombre del paciente.
        internal static string habitacion; //Variable que contiene la habitacion del paciente.
        internal static DateTime fecha_nacimiento; //Variable de contencion de fecha de nacimiento del paciente.
        internal static string cie_codigo; //Variable de contencion de codigo del procedimiento de la tabla cie10;
        internal static string cie_descripcion; //Variable de contencion del nombre del procedimiento, tabla cie10;
        internal static string cod_cirujano; //Variable que contiene el codigo del medico cirujano
        internal static string nom_cirujano; //Variable que contiene el nombre del medico cirujano
        internal static string cod_ayudante = ""; //Variable que contiene el codigo del medico(ayudante)
        internal static string nom_ayudante = ""; //Variable que contiene el nombre del medico(ayudante)
        internal static string cod_ayudantia = ""; //Variable que contiene el codigo del medico(ayudantia)
        internal static string nom_ayudantia = ""; //Variable que contiene el nombre del medico(ayudantia)
        internal static string cod_anestesiologo = ""; //Variable que contiene el codigo del anestesiologo
        internal static string nom_anestesiologo = ""; //Variable que contiene el nombredel anestesiologo internal static string cod_anestesiologo = ""; //Variable que contiene el codigo del anestesiologo
        internal static string cod_anestesiologo2 = ""; //Variable que contiene el codigo del anestesiologo
        internal static string nom_anestesiologo2 = ""; //Variable que contiene el nombredel anestesiologo
        internal static string cod_circulante = ""; //Variable que contiene el codigo del circulante
        internal static string nom_circulante; //Variable que contiene el nombre del circulante
        internal static string cod_instrumentista = ""; //Variable que contiene el codigo del instrumentista
        internal static string nom_instrumentista = ""; //Variable que contiene el nombre del instrumentista
        internal static string cod_patologo = ""; //Variable que contiene el codigo del patologo
        internal static string nom_patologo; //Vairiable que contiene el nombre del patologo
        private static bool valido; //Variable que permite el paso de guardar registro si este es verdadero, caso contrario no.
        private static string recuperacion; //variable que entrega en string el estado de recuperacion 1(si) y 0(no)
        private static string tip_ate; //Variable que contiene el estado de atencion 
        internal static string ate_codigo; //Variable que contiene el codigo de atencion del paciente
        internal static string pac_codigo; //Variable que contiene el codigo del paciente.
        private static string existe; //Valida si el usuario ya tiene registro, si es asi levanta la informacion, caso contrario debe ingresarla
        internal static bool Editar = false; //Modo Edidicion debe activarse unicamente cuando levante informacion
        public static string seguro, tratamiento, referido;
        private static bool EdicionInfo = false;
        public string procedimietnosOcupados = "";
        public int bodega = His.Entidades.Clases.Sesion.bodega; //Se cambia para que no sea la bodega de Quirofano como defecto desde el inicio // Mario // 05-12-2023
        public frmQuirofanoRegistro()
        {
            InitializeComponent();
        }

        public frmQuirofanoRegistro(int bodega)
        {
            InitializeComponent();



            this.bodega = bodega;
            if (bodega != Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()))
            {
                lblmedico.Text = "Médico*:";
                lblsala.Text = "Sala*:";
            }
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Esta Seguro de Salir sin Guardar?", "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void toolStripButtonEditar_Click(object sender, EventArgs e)
        {
            //Establecer lo que se va editar por el usuario
            toolStripButtonGuardar.Enabled = true;
            btnmodificar.Enabled = false;
            Desbloquear();
            EdicionInfo = true;
        }


        private void frmQuirofanoRegistro_Load(object sender, EventArgs e)
        {
            MEDICOS med = new MEDICOS();
            USUARIOS user = new USUARIOS();
            CargarPacienteExistente();
            if (!Editar)
            {
                btnmodificar.Enabled = false;
            }
            else
            {
                btnmodificar.Enabled = true;
                toolStripButtonGuardar.Enabled = false;

                REGISTRO_QUIROFANO obj = new REGISTRO_QUIROFANO();
                obj = NegQuirofano.RecuperarRegistroQuirofano(ultimaAtencion.ATE_CODIGO);
                INTERVENCIONES_REGISTRO_QUIROFANO intervancion = new INTERVENCIONES_REGISTRO_QUIROFANO();
                intervancion = NegQuirofano.RecuperaRegistroIntervencionQuirofano(obj.id_registro_quirofano);
                cod_cirujano = Convert.ToString(obj.cirujano);
                med = NegMedicos.RecuperaMedicoId(obj.cirujano);
                nom_cirujano = med.MED_APELLIDO_PATERNO + " " + med.MED_APELLIDO_MATERNO + " " + med.MED_NOMBRE1 + " " + med.MED_NOMBRE2;
                txtcirujano.Text = nom_cirujano;
                if (intervancion != null)
                {
                    lblprocedimiento.Text = obj.intervencion;
                }
                cbanestesia.SelectedValue = obj.anestecia;
                if (obj.ayudante.ToString() != "")
                {
                    cod_ayudante = obj.ayudante.ToString();
                    med = NegMedicos.RecuperaMedicoId((int)obj.ayudante);
                    nom_ayudante = med.MED_APELLIDO_PATERNO + " " + med.MED_APELLIDO_MATERNO + " " + med.MED_NOMBRE1 + " " + med.MED_NOMBRE2;
                    txtayudante.Text = nom_ayudante;
                }
                if (obj.instrumentista.ToString() != "")
                {
                    cod_instrumentista = obj.instrumentista.ToString();
                    user = NegUsuarios.RecuperarUsuarioID((int)obj.instrumentista);
                    nom_instrumentista = user.APELLIDOS + " " + user.NOMBRES;
                    txtinstrumentista.Text = nom_instrumentista;
                }
                txthorainicio.Text = obj.hora_inicio.ToString();
                HoraFin.Text = obj.hora_fin.ToString();
                if (obj.recuperacion)
                {
                    ckbsi.Checked = true;
                    ckbno.Checked = false;
                }
                else
                {
                    ckbsi.Checked = false;
                    ckbno.Checked = true;
                }
                if (obj.tipo_Atencion)
                {
                    ckbProgramada.Checked = true;
                    ckbEmergencia.Checked = false;
                }
                else
                {
                    ckbProgramada.Checked = false;
                    ckbEmergencia.Checked = true;
                }
                if ((bool)obj.contaminada)
                {
                    chb_contaminadaSi.Checked = true;
                    chb_contaminadaNo.Checked = false;
                }
                else
                {
                    chb_contaminadaSi.Checked = false;
                    chb_contaminadaNo.Checked = true;
                }
                if (obj.circulante.ToString() != "")
                {
                    cod_circulante = obj.circulante.ToString();
                    user = NegUsuarios.RecuperarUsuarioID((int)obj.circulante);
                    nom_circulante = user.APELLIDOS + " " + user.NOMBRES;
                    txtcirculante.Text = nom_circulante;
                }
                if (obj.patologia.ToString() != "")
                {
                    cod_patologo = obj.patologia.ToString();
                    med = NegMedicos.RecuperaMedicoId((int)obj.patologia);
                    nom_patologo = med.MED_APELLIDO_PATERNO + " " + med.MED_APELLIDO_MATERNO + " " + med.MED_NOMBRE1 + " " + med.MED_NOMBRE2;
                    txtpatologia.Text = nom_patologo;
                    chb_patologiaNo.Checked = false;
                    chb_patologiaSi.Checked = true;
                }
                if (obj.anasteciologo.ToString() != "")
                {
                    cod_anestesiologo = obj.anasteciologo.ToString();
                    med = NegMedicos.RecuperaMedicoId((int)obj.anasteciologo);
                    nom_anestesiologo = med.MED_APELLIDO_PATERNO + " " + med.MED_APELLIDO_MATERNO + " " + med.MED_NOMBRE1 + " " + med.MED_NOMBRE2;
                    txtanestesiologo.Text = nom_anestesiologo;

                }
                if (obj.anasteciologoAyudante.ToString() != "")
                {
                    cod_anestesiologo2 = obj.anasteciologoAyudante.ToString();
                    med = NegMedicos.RecuperaMedicoId((int)obj.anasteciologoAyudante);
                    nom_anestesiologo2 = med.MED_APELLIDO_PATERNO + " " + med.MED_APELLIDO_MATERNO + " " + med.MED_NOMBRE1 + " " + med.MED_NOMBRE2;
                    textBox1.Text = nom_anestesiologo2;

                }
                if (obj.ayudantia.ToString() != "")

                {
                    cod_ayudantia = obj.ayudantia.ToString();
                    med = NegMedicos.RecuperaMedicoId((int)obj.ayudantia);
                    nom_ayudantia = med.MED_APELLIDO_PATERNO + " " + med.MED_APELLIDO_MATERNO + " " + med.MED_NOMBRE1 + " " + med.MED_NOMBRE2;
                    txtayudantia.Text = nom_ayudantia;

                }
                textBox2.Text = obj.observaciones;
                cmb_especialidadCirugia.SelectedValue = obj.especialidadCirugia;
                cbHabQuirofano.SelectedValue = obj.quirofano;

                TimeSpan Duracion;
                DateTime horainicio = Convert.ToDateTime(txthorainicio.Text);
                DateTime horafin = Convert.ToDateTime(HoraFin.Value);
                Duracion = horafin.Subtract(horainicio).Duration();
                txtduracion.Text = Convert.ToString(Duracion);
            }
        }

        ATENCIONES ultimaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));
        PACIENTES datosPaciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));
        public void CargarPacienteExistente()
        {
            HABITACIONES _hab = NegHabitaciones.RecuperarHabitacionId(Convert.ToInt16(ultimaAtencion.HABITACIONESReference.EntityKey.EntityKeyValues[0].Value));
            //existe = Quirofano.ExisteProcedimiento(ate_codigo, pac_codigo, cie_codigo);
            //if (existe == "")
            //{
            var now = DateTime.Now;
            var birthday = datosPaciente.PAC_FECHA_NACIMIENTO;
            var yearsOld = now - birthday;

            int years = Convert.ToInt32(((TimeSpan)(yearsOld)).TotalDays / 365.25);
            int months = Convert.ToInt32((((TimeSpan)(yearsOld)).TotalDays / 365.25) - years) * 12;
            //Editar = true;
            DateTime Fecha = DateTime.Now;
            lblfecha.Text = Convert.ToString(Fecha.ToShortDateString());
            lblpaciente.Text = datosPaciente.PAC_APELLIDO_PATERNO + " " + datosPaciente.PAC_APELLIDO_MATERNO + " " + datosPaciente.PAC_NOMBRE1 + " " + datosPaciente.PAC_NOMBRE2;
            lblHC.Text = datosPaciente.PAC_HISTORIA_CLINICA.ToString();
            lblhabitacion.Text = _hab.hab_Numero;
            CargarHabitacionesQuirofano();
            cbHabQuirofano.SelectedValue = _hab.hab_Codigo;
            lbledad.Text = years.ToString() + " años";
            lblprocedimiento.Text = cie_descripcion;
            //txthorainicio.Text = Fecha.ToLongTimeString();
            txthorainicio.Text = "";
            HoraFin.Text = "";
            CargarTipoAnestesia();
            CargarEspCirugia();

            //}
            //else
            //{
            //    var now = DateTime.Now;
            //    var birthday = datosPaciente.PAC_FECHA_NACIMIENTO;
            //    var yearsOld = now - birthday;

            //    int years = Convert.ToInt32(((TimeSpan)(yearsOld)).TotalDays / 365.25);
            //    int months = Convert.ToInt32((((TimeSpan)(yearsOld)).TotalDays / 365.25) - years) * 12;
            //    //int months = (int)(((yearsOld.TotalDays / 365.25) - years) * 12);


            //    Editar = false;
            //    DataTable TablaRegistro = new DataTable();
            //    DateTime Fecha = DateTime.Now;
            //    bool recu;
            //    string ate;
            //    TablaRegistro = Quirofano.RegistroPaciente(ate_codigo, pac_codigo, cie_codigo);
            //    CargarHabitacionesQuirofano();
            //    CargarTipoAnestesia();
            //    Bloquear();
            //    foreach (DataRow item in TablaRegistro.Rows)
            //    {
            //        cbHabQuirofano.SelectedValue = item[0].ToString();
            //        txtcirujano.Text = Convert.ToString(item[1]);
            //        cod_cirujano = item["CODCIRUJANO"].ToString();
            //        txtayudante.Text = Convert.ToString(item[2]);
            //        cod_ayudante = item["CODAYUDANTE"].ToString();
            //        txtayudantia.Text = Convert.ToString(item[3]);
            //        cod_ayudantia = item["CODAYUDANTIA"].ToString();
            //        cbanestesia.SelectedValue = item[4].ToString();
            //        recu = Convert.ToBoolean(item[5]);
            //        if (recu == true)
            //        {
            //            ckbsi.Checked = true;
            //            ckbno.Checked = false;
            //        }
            //        else
            //        {
            //            ckbno.Checked = true;
            //            ckbsi.Checked = false;
            //        }
            //        txtanestesiologo.Text = Convert.ToString(item[6]);
            //        cod_anestesiologo = item["CODANESTESIOLOGO"].ToString();
            //        txthorainicio.Text = Convert.ToString(item[7]);
            //        txtcirculante.Text = Convert.ToString(item[8]);
            //        cod_circulante = item["CODCIRCULANTE"].ToString();
            //        txtinstrumentista.Text = Convert.ToString(item[9]);
            //        cod_instrumentista = item["CODINSTRUMENTISTA"].ToString();
            //        txtpatologia.Text = Convert.ToString(item[10]);
            //        cod_patologo = item["CODPATOLOGO"].ToString();
            //        ate = Convert.ToString(item[11]);
            //        if (ate == "PROGRAMADA")
            //        {
            //            ckbProgramada.Checked = true;
            //        }
            //        if (ate == "EMERGENCIA")
            //        {
            //            ckbEmergencia.Checked = true;
            //        }
            //        lblprocedimiento.Text = cie_descripcion;
            //        lblpaciente.Text = datosPaciente.PAC_APELLIDO_PATERNO + " " + datosPaciente.PAC_APELLIDO_MATERNO + " " + datosPaciente.PAC_NOMBRE1 + " " + datosPaciente.PAC_NOMBRE2;
            //        lblhabitacion.Text = _hab.hab_Numero;
            //        cbHabQuirofano.SelectedValue = _hab.hab_Codigo;
            //        lbledad.Text = years.ToString() + " años";
            //        lblfecha.Text = item[13].ToString();
            //    }
            //}
        }
        public void CargarTipoAnestesia()
        {
            cbanestesia.DataSource = Quirofano.MostrarAnestesia();
            cbanestesia.DisplayMember = "DESCRIPCION";
            cbanestesia.ValueMember = "CODIGO";
            cbanestesia.SelectedIndex = -1;
        }
        public void CargarEspCirugia()
        {
            cmb_especialidadCirugia.DataSource = Quirofano.MostrarEspCirugia();
            cmb_especialidadCirugia.DisplayMember = "detalle";
            cmb_especialidadCirugia.ValueMember = "id";
            cmb_especialidadCirugia.SelectedIndex = -1;
        }
        public void CargarHabitacionesQuirofano()
        {
            if (bodega != Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()))
            {
                cbHabQuirofano.DataSource = NegQuirofano.GastroHabitacion();
                cbHabQuirofano.DisplayMember = "HABITACION";
                cbHabQuirofano.ValueMember = "CODIGO";
            }
            else
            {
                cbHabQuirofano.DataSource = Quirofano.QuirofanoHabitacion();
                cbHabQuirofano.DisplayMember = "HABITACION";
                cbHabQuirofano.ValueMember = "CODIGO";
            }
        }

        private void btncirujano_Click(object sender, EventArgs e)
        {
            //if (bodega != 19)
            //{
            frmQuirofanoAyudaRegistro.medico = true;
            frmQuirofanoAyudaRegistro x = new frmQuirofanoAyudaRegistro();
            x.Show();
            x.FormClosed += X_FormClosed;
            //}
            //else
            //{
            //    frmQuirofanoAyudaRegistro.gastro = true;
            //    frmQuirofanoAyudaRegistro x = new frmQuirofanoAyudaRegistro();
            //    x.Show();
            //    x.FormClosed += X_FormClosed;
            //}
        }

        private void X_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (frmQuirofanoAyudaRegistro.medico == true)
            {
                frmQuirofanoAyudaRegistro.medico = false;
                txtcirujano.Text = nom_cirujano;
            }
            if (frmQuirofanoAyudaRegistro.ayudante == true)
            {
                frmQuirofanoAyudaRegistro.ayudante = false;
                txtayudante.Text = nom_ayudante;
            }
            if (frmQuirofanoAyudaRegistro.ayudantia == true)
            {
                frmQuirofanoAyudaRegistro.ayudantia = false;
                txtayudantia.Text = nom_ayudantia;
            }
            if (frmQuirofanoAyudaRegistro.anestesiologo == true)
            {
                frmQuirofanoAyudaRegistro.anestesiologo = false;
                txtanestesiologo.Text = nom_anestesiologo;
            }
            if (frmQuirofanoAyudaRegistro.anestesiologo2 == true)
            {
                frmQuirofanoAyudaRegistro.anestesiologo2 = false;
                textBox1.Text = nom_anestesiologo2;
            }
            if (frmQuirofanoAyudaRegistro.circulante == true)
            {
                frmQuirofanoAyudaRegistro.circulante = false;
                txtcirculante.Text = nom_circulante;
            }
            if (frmQuirofanoAyudaRegistro.instrumentista == true)
            {
                frmQuirofanoAyudaRegistro.instrumentista = false;
                txtinstrumentista.Text = nom_instrumentista;
            }
            if (frmQuirofanoAyudaRegistro.patologo == true)
            {
                frmQuirofanoAyudaRegistro.patologo = false;
                txtpatologia.Text = nom_patologo;
            }
            if (frmQuirofanoAyudaRegistro.gastro == true)
            {
                frmQuirofanoAyudaRegistro.gastro = false;
                txtcirujano.Text = nom_cirujano;
            }
        }

        private void btnayudante_Click(object sender, EventArgs e)
        {
            frmQuirofanoAyudaRegistro.ayudante = true;
            frmQuirofanoAyudaRegistro x = new frmQuirofanoAyudaRegistro();
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void btnayudantia_Click(object sender, EventArgs e)
        {
            frmQuirofanoAyudaRegistro.ayudantia = true;
            frmQuirofanoAyudaRegistro x = new frmQuirofanoAyudaRegistro();
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void btnanestesiologo_Click(object sender, EventArgs e)
        {
            frmQuirofanoAyudaRegistro.anestesiologo = true;
            frmQuirofanoAyudaRegistro x = new frmQuirofanoAyudaRegistro();
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void btncirculante_Click(object sender, EventArgs e)
        {
            frmQuirofanoAyudaRegistro.circulante = true;
            frmQuirofanoAyudaRegistro x = new frmQuirofanoAyudaRegistro();
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void btninstrumentista_Click(object sender, EventArgs e)
        {
            frmQuirofanoAyudaRegistro.instrumentista = true;
            frmQuirofanoAyudaRegistro x = new frmQuirofanoAyudaRegistro();
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void ckbProgramada_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbProgramada.Checked == true)
            {
                ckbEmergencia.Enabled = false;
                tip_ate = "PROGRAMADA";
            }
            else
            {
                ckbEmergencia.Enabled = true;
            }
        }

        private void ckbEmergencia_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbEmergencia.Checked == true)
            {
                ckbProgramada.Enabled = false;
                tip_ate = "EMERGENCIA";
            }
            else
            {
                ckbProgramada.Enabled = true;
            }
        }

        private void btnpatologia_Click(object sender, EventArgs e)
        {
            frmQuirofanoAyudaRegistro.patologo = true;
            frmQuirofanoAyudaRegistro x = new frmQuirofanoAyudaRegistro();
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbsi.Checked == true)
            {
                ckbno.Enabled = false;
                recuperacion = "1";
            }
            else
            {
                ckbno.Enabled = true;
            }
        }

        private void ckbno_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbno.Checked == true)
            {
                ckbsi.Enabled = false;
                recuperacion = "0";
            }
            else
            {
                ckbsi.Enabled = true;
            }
        }

        private void txthorafinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }
        private static void OnlyNumber(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789:" + Convert.ToChar(8);
            }
            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txthorainicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }

        private void HoraFin_Leave(object sender, EventArgs e)
        {
            TimeSpan Duracion;
            DateTime horainicio = Convert.ToDateTime(txthorainicio.Text);
            DateTime horafin = Convert.ToDateTime(HoraFin.Value);
            Duracion = horafin.Subtract(horainicio).Duration();
            txtduracion.Text = Convert.ToString(Duracion);
        }

        private void HoraFin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TimeSpan Duracion;
                DateTime horainicio = Convert.ToDateTime(txthorainicio.Text);
                DateTime horafin = Convert.ToDateTime(HoraFin.Value);
                Duracion = horafin.Subtract(horainicio).Duration();
                txtduracion.Text = Convert.ToString(Duracion);
            }
        }

        private void toolStripButtonGuardar_Click(object sender, EventArgs e)
        {
            ValidarVacio();
            DateTime fecha = DateTime.Now;

            ATENCIONES atencionActual = new ATENCIONES();
            PACIENTES pacienteActual = new PACIENTES();

            atencionActual = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(ate_codigo));
            pacienteActual = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(ate_codigo));
            if (valido && !EdicionInfo)
            {
                try
                {
                    REGISTRO_QUIROFANO obj = new REGISTRO_QUIROFANO();
                    obj.ate_codigo = ultimaAtencion.ATE_CODIGO;
                    obj.fecha_registro = DateTime.Now;
                    obj.cirujano = Convert.ToInt32(cod_cirujano);
                    obj.anestecia = Convert.ToInt32(cbanestesia.SelectedValue.ToString());
                    if (cod_ayudante != "")
                        obj.ayudante = Convert.ToInt32(cod_ayudante);
                    obj.instrumentista = Convert.ToInt32(cod_instrumentista);
                    obj.hora_inicio = TimeSpan.Parse(txthorainicio.Text);
                    obj.hora_fin = TimeSpan.Parse(HoraFin.Text);
                    if (ckbsi.Checked)
                        obj.recuperacion = true;
                    else
                        obj.recuperacion = false;
                    if (cod_circulante != "")
                        obj.circulante = Convert.ToInt32(cod_circulante);
                    if (cod_patologo != "")
                        obj.patologia = Convert.ToInt32(cod_patologo);
                    if (cod_ayudantia != "")
                        obj.ayudantia = Convert.ToInt32(cod_ayudantia);
                    obj.anasteciologo = Convert.ToInt32(cod_anestesiologo);
                    obj.intervencion = lblprocedimiento.Text;
                    if (cod_anestesiologo2 != "")
                        obj.anasteciologoAyudante = Convert.ToInt32(cod_anestesiologo2);
                    obj.observaciones = textBox2.Text;
                    obj.especialidadCirugia = Convert.ToInt32(cmb_especialidadCirugia.SelectedValue);
                    if (ckbProgramada.Checked)
                    {
                        obj.tipo_Atencion = true;
                    }
                    else
                    {
                        obj.tipo_Atencion = false;

                    }
                    if (chb_contaminadaSi.Checked)
                    {
                        obj.contaminada = true;
                    }
                    else
                    {
                        obj.contaminada = false;
                    }
                    obj.estado = true;
                    obj.quirofano = Convert.ToInt32(cbHabQuirofano.SelectedValue);
                    obj.bodega = bodega;

                    string[] codigos = procedimietnosOcupados.Split(';');
                    List<INTERVENCIONES_REGISTRO_QUIROFANO> lista = new List<INTERVENCIONES_REGISTRO_QUIROFANO>();
                    INTERVENCIONES_REGISTRO_QUIROFANO obj1 = new INTERVENCIONES_REGISTRO_QUIROFANO();

                    Int64 codigo = NegQuirofano.GuardaRegistroQuirofano(obj);

                    if (codigo > 0)
                    {

                        foreach (var item in codigos)
                        {
                            if (item != "")
                            {
                                obj1.id_registro_quirofano = codigo;
                                obj1.cie_10 = Convert.ToInt64(item);
                                obj1.general = bodega;
                                NegQuirofano.GuardaRegistroIntervencionQuirofano(obj1);
                            }
                        }

                        //  Quirofano.PedidoPaciente(cie_codigo, datosPaciente.PAC_CODIGO.ToString(), ate_codigo, Convert.ToString(fecha));
                        //  Quirofano.AgregarRegistro(Convert.ToString(cbHabQuirofano.SelectedValue), cod_cirujano, cod_ayudante, cod_ayudantia, Convert.ToString(cbanestesia.SelectedValue),
                        //recuperacion, cod_anestesiologo, txthorainicio.Text, HoraFin.Text, txtduracion.Text, cod_circulante, cod_instrumentista, cod_patologo, tip_ate, ate_codigo, datosPaciente.PAC_CODIGO.ToString(), cie_codigo);
                        EdicionInfo = false;
                        this.Close();
                        MessageBox.Show("¡Los Datos han sido Agregados Correctamente!", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        //UltraGridRow Fila = UltraGridPacientes.ActiveRow;

                        ////Envio datos a otro formulario pedido paciente
                        //frmQuirofanoPedidoPaciente.medico = atencionActual.MEDICOS.MED_APELLIDO_PATERNO + " " +
                        //atencionActual.MEDICOS.MED_APELLIDO_MATERNO + " " + atencionActual.MEDICOS.MED_NOMBRE1 + " " + atencionActual.MEDICOS.MED_NOMBRE2;
                        //frmQuirofanoPedidoPaciente.seguro = seguro;
                        //frmQuirofanoPedidoPaciente.tipo = tratamiento;
                        //frmQuirofanoPedidoPaciente.genero = pacienteActual.PAC_GENERO;
                        //frmQuirofanoPedidoPaciente.referido = referido;
                        //frmQuirofanoPedidoPaciente.nombrepaciente = lblpaciente.Text;
                        //frmQuirofanoPedidoPaciente.ate_codigo = ate_codigo;
                        //frmQuirofanoPedidoPaciente.pac_codigo = datosPaciente.PAC_CODIGO.ToString();

                        ////Abrir el formulario
                        //frmQuirofanoPedidoPaciente x = new frmQuirofanoPedidoPaciente(bodega);
                        //x.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("¡Algo Ocurrio al Agregar los Datos al Paciente!\r\nMás detalle: " + ex.InnerException, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else if (valido && EdicionInfo)
            {
                try
                {
                    REGISTRO_QUIROFANO obj = new REGISTRO_QUIROFANO();
                    obj.ate_codigo = ultimaAtencion.ATE_CODIGO;
                    obj.fecha_registro = DateTime.Now;
                    obj.cirujano = Convert.ToInt32(cod_cirujano);
                    obj.anestecia = Convert.ToInt32(cbanestesia.SelectedValue.ToString());
                    if (cod_ayudante != "")
                        obj.ayudante = Convert.ToInt32(cod_ayudante);
                    obj.instrumentista = Convert.ToInt32(cod_instrumentista);
                    obj.hora_inicio = TimeSpan.Parse(txthorainicio.Text);
                    obj.hora_fin = TimeSpan.Parse(HoraFin.Text);
                    if (ckbsi.Checked)
                        obj.recuperacion = true;
                    else
                        obj.recuperacion = false;
                    if (cod_circulante != "")
                        obj.circulante = Convert.ToInt32(cod_circulante);
                    if (cod_patologo != "")
                        obj.patologia = Convert.ToInt32(cod_patologo);
                    if (cod_ayudantia != "")
                        obj.ayudantia = Convert.ToInt32(cod_ayudantia);
                    obj.anasteciologo = Convert.ToInt32(cod_anestesiologo);
                    obj.intervencion = lblprocedimiento.Text;
                    if (cod_anestesiologo2 != "")
                        obj.anasteciologoAyudante = Convert.ToInt32(cod_anestesiologo2);
                    obj.observaciones = textBox2.Text;
                    obj.especialidadCirugia = Convert.ToInt32(cmb_especialidadCirugia.SelectedValue);
                    if (ckbProgramada.Checked)
                    {
                        obj.tipo_Atencion = true;
                    }
                    else
                    {
                        obj.tipo_Atencion = false;

                    }
                    if (chb_contaminadaSi.Checked)
                    {
                        obj.contaminada = true;
                    }
                    else
                    {
                        obj.contaminada = false;
                    }
                    obj.estado = true;
                    obj.quirofano = Convert.ToInt32(cbHabQuirofano.SelectedValue);
                    obj.bodega = bodega;

                    string[] codigos = procedimietnosOcupados.Split(';');
                    List<INTERVENCIONES_REGISTRO_QUIROFANO> lista = new List<INTERVENCIONES_REGISTRO_QUIROFANO>();
                    INTERVENCIONES_REGISTRO_QUIROFANO obj1 = new INTERVENCIONES_REGISTRO_QUIROFANO();

                    Int64 codigo = NegQuirofano.GuardaRegistroQuirofano(obj, EdicionInfo);

                    if (codigo > 0)
                    {
                        //ESTA PARTE SE OCUPA PARA LA MODIFICACIÓN 
                        foreach (var item in codigos)
                        {
                            if (item != "")
                            {
                                obj1.id_registro_quirofano = codigo;
                                obj1.cie_10 = Convert.ToInt64(item);
                                obj1.general = bodega;
                                NegQuirofano.GuardaRegistroIntervencionQuirofano(obj1, EdicionInfo);
                                EdicionInfo = false;
                            }
                        }
                        EdicionInfo = false;
                        this.Close();
                        MessageBox.Show("¡Los Datos han sido Actualizados Correctamente!", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                catch (Exception ex)
                {
                    EdicionInfo = false;
                    MessageBox.Show("¡Algo Ocurrio al Agregar los Datos al Paciente!\r\nMás detalle: " + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        public void ValidarVacio()
        {
            valido = true;
            errorProvider1.Clear();
            if (txtcirujano.Text == "")
            {
                errorProvider1.SetError(txtcirujano, "¡Campo Obligatorio!");
                valido = false;
            }
            if (cbHabQuirofano.SelectedIndex < 0)
            {
                errorProvider1.SetError(cbHabQuirofano, "Debe elegir " + lblsala.Text);
                valido = false;
            }
            if (cmb_especialidadCirugia.Text == "")
            {
                errorProvider1.SetError(cmb_especialidadCirugia, "¡Campo Obligatorio!");
                valido = false;
            }
            if (cbanestesia.Text == "")
            {
                errorProvider1.SetError(cbanestesia, "¡Campo Obligatorio!");
                valido = false;
            }
            if (txtanestesiologo.Text == "")
            {
                errorProvider1.SetError(txtanestesiologo, "¡Campo Obligatorio!");
                valido = false;
            }
            if (!chb_contaminadaSi.Checked && !chb_contaminadaNo.Checked)
            {
                errorProvider1.SetError(label16, "¡Campo Obligatorio!");
                valido = false;
            }
            if (txtinstrumentista.Text == "")
            {
                errorProvider1.SetError(txtinstrumentista, "¡Campo Obligatorio!");
                valido = false;
            }
            if (lblprocedimiento.Text == "")
            {
                errorProvider1.SetError(lblprocedimiento, "¡Campo Obligatorio!");
                valido = false;
            }
            if (chb_patologiaSi.Checked)
            {
                if (txtpatologia.Text == "")
                {
                    errorProvider1.SetError(txtpatologia, "¡Campo Obligatorio!");
                    valido = false;

                }
            }
            if (ckbEmergencia.Checked == false && ckbProgramada.Checked == false)
            {
                errorProvider1.SetError(lblatencion, "¡Campo Obligatorio!");
                valido = false;
            }
            if (ckbsi.Checked == false && ckbno.Checked == false)
            {
                errorProvider1.SetError(lblrecuperacion, "¡Campo Obligatorio!");
                valido = false;
            }
            if (!txthorainicio.Checked)
            {
                errorProvider1.SetError(txthorainicio, "¡Debe activar la casilla!");
                valido = false;
            }
            if (!HoraFin.Checked)
            {
                errorProvider1.SetError(HoraFin, "¡Debe activar la casilla!");
                valido = false;
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de eliminar este procedimiento? \r\nUna vez eliminado no podra recuperar la información."
               + "\r\n¿Desea continuar..?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                NegQuirofano Quir = new NegQuirofano();
                string cerrado = Quir.ProcedimientoCerrado(ate_codigo, pac_codigo, cie_codigo);
                if (cerrado == "")
                {
                    NegQuirofano.EliminarRegistro(Convert.ToInt64(ate_codigo), Convert.ToInt32(cie_codigo));
                    MessageBox.Show("Datos eliminados correctamente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se puede eliminar un registro que ya ha sido cerrado.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnTarifario_Click(object sender, EventArgs e)
        {
            frm_AyudaGeneral x;
            if (His.Entidades.Clases.Sesion.bodega == Convert.ToInt32(NegParametros.ParametroBodegaQuirofano()))
            {
                x = new frm_AyudaGeneral(true);
            }
            else
            {
                x = new frm_AyudaGeneral(false, true);
            }

            x.tarifario = true;
            x.ShowDialog();
            if (lblprocedimiento.Text == "")
            {
                lblprocedimiento.Text = x.resultado;
                procedimietnosOcupados = x.codigo;
                procedimietnosOcupados += ";";
            }
            else
            {
                lblprocedimiento.Text += " + ";
                lblprocedimiento.Text += x.resultado;
                procedimietnosOcupados += x.codigo;
                procedimietnosOcupados += ";";
            }


        }

        private void btnSuprimir_Click(object sender, EventArgs e)
        {
            lblprocedimiento.Text = "";
        }

        private void txthorainicio_Leave(object sender, EventArgs e)
        {
            TimeSpan Duracion;
            DateTime horainicio = Convert.ToDateTime(txthorainicio.Text);
            DateTime horafin = Convert.ToDateTime(HoraFin.Value);
            Duracion = horafin.Subtract(horainicio).Duration();
            txtduracion.Text = Convert.ToString(Duracion);
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            frmQuirofanoAyudaRegistro.anestesiologo2 = true;
            frmQuirofanoAyudaRegistro x = new frmQuirofanoAyudaRegistro();
            x.Show();
            x.FormClosed += X_FormClosed;
        }

        private void chb_contaminadaNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_contaminadaNo.Checked)
            {
                chb_contaminadaSi.Checked = false;
            }
        }

        private void chb_contaminadaSi_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_contaminadaSi.Checked)
            {
                chb_contaminadaNo.Checked = false;
            }
        }

        private void chb_patologiaNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_patologiaNo.Checked)
            {
                btnpatologia.Visible = false;
                chb_patologiaSi.Checked = false;
                txtpatologia.Text = "";
                cod_patologo = "";
            }
        }

        private void chb_patologiaSi_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_patologiaSi.Checked)
            {
                btnpatologia.Visible = true;
                chb_patologiaNo.Checked = false;
            }
        }

        private void cbanestesia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbanestesia.SelectedIndex == 1)
            {
                txtanestesiologo.Text = nom_cirujano;
                cod_anestesiologo = cod_cirujano;
            }
        }

        public void Bloquear()
        {
            btncirujano.Enabled = false;
            btnayudante.Enabled = false;
            btnayudantia.Enabled = false;
            cbHabQuirofano.Enabled = false;
            cbanestesia.Enabled = false;
            ckbsi.Enabled = false;
            ckbno.Enabled = false;
            btnanestesiologo.Enabled = false;
            btncirculante.Enabled = false;
            btninstrumentista.Enabled = false;
            btnpatologia.Enabled = false;
            ckbProgramada.Enabled = false;
            ckbEmergencia.Enabled = false;
        }
        public void Desbloquear()
        {
            btncirujano.Enabled = true;
            btnayudante.Enabled = true;
            btnayudantia.Enabled = true;
            cbHabQuirofano.Enabled = true;
            cbanestesia.Enabled = true;
            ckbsi.Enabled = true;
            ckbno.Enabled = true;
            btnanestesiologo.Enabled = true;
            btncirculante.Enabled = true;
            btninstrumentista.Enabled = true;
            btnpatologia.Enabled = true;
            ckbProgramada.Enabled = true;
            ckbEmergencia.Enabled = true;
        }
    }
}
