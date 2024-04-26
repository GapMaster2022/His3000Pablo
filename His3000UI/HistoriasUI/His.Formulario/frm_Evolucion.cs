using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.DatosReportes;
using His.Entidades;
using His.Entidades.Reportes;
using His.Negocio;
using His.Parametros;
using Infragistics.Win.UltraWinGrid;
using His.Entidades.Clases;
using GeneralApp.ControlesWinForms;
using System.IO;
using System.Speech.Recognition;
using System.Diagnostics;

namespace His.Formulario
{
    public partial class frm_Evolucion : Form
    {
        #region Variables
        private Int64 atencionId;             //codigo de la atencion del paciente
        private bool mostrarInfPaciente;    //si se mostrara el panel con la informacion del paciente
        ATENCIONES atencion = null;
        PACIENTES paciente = null;
        HC_EVOLUCION evolucion = null;
        HC_EVOLUCION_DETALLE ultimaNota = null;
        bool psqui = false;
        EVOLUCIONDETALLE ultimaNota1; //prueba porque el ultimanota no sirve y nunca es utilizada
        List<HC_PRESCRIPCIONES> listaPrescripciones = new List<HC_PRESCRIPCIONES>();
        List<DtoAtenciones> atenciones = new List<DtoAtenciones>();
        MEDICOS medico = null;
        MaskedTextBox codMedico;
        bool formulario = false;
        bool band = true;
        private int posicion;
        private bool cursorDown;
        private Point[] puntos;
        private Pen lapiz;
        private Pen goma;
        private Bitmap bmp;
        private string accion;
        int validaUsuario = 0;
        bool multilinea = true;
        int ingresanuevomedico = 1;
        //SpeechRecognitionEngine escucha = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("es-ES"));
        int grabaEvolucion = 1;
        int grabaIndicaciones = 1;
        int grabaFarmacos = 1;
        private static int aux = 0;
        TextBox txt_Atencion = new TextBox();
        public int LineCount { get; set; }

        private bool Editar = false; // prueba para ver si edita la evolucion
        public Int64 evd_codigo = 0;
        private Int64 cod_Evolucion1; //hago global el codigo para cuando quieran levantar informacion, como cambiar de nota de evolucion este permita editar
        public bool subsecuente;
        public string medicosEvolucion = "";


        #endregion

        #region Contructor
        internal static bool Eliminar; //recibe true si el login se hizo correctamente y podra eliminar la evolucion
        private int evocodigo; //recibe el codigo de evlucion para la eliminacion
        public bool subSecuente = false;
        public frm_Evolucion(bool _subSecuente = false, Int64 ateCodigo = 0)
        {
            InitializeComponent();
            if (Sesion.codUsuario == 1)
            {
                MEDICOS med = NegMedicos.recuperarMedico(0);
                txtMedico.Text = med.MED_APELLIDO_PATERNO.Trim() + " " + med.MED_APELLIDO_MATERNO.Trim()
                         + " " + med.MED_NOMBRE1.Trim() + " " + med.MED_NOMBRE2.Trim() + " Usuario Administrador";
                txtCodMedico.Text = med.MED_CODIGO.ToString();
            }
            else
            {
                MEDICOS med = NegMedicos.recuperarMedicoID_Usuario(Sesion.codUsuario);
                if (med != null)
                {
                    txtMedico.Text = med.MED_APELLIDO_PATERNO.Trim() + " " + med.MED_APELLIDO_MATERNO.Trim()
                         + " " + med.MED_NOMBRE1.Trim() + " " + med.MED_NOMBRE2.Trim();
                    txtCodMedico.Text = med.MED_CODIGO.ToString();
                }
                else
                {
                    MessageBox.Show("El Usuario no tiene aun credenciales de médico", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }

            }
            if (!_subSecuente)
            {
                frm_AyudaPacientes form = new frm_AyudaPacientes();
                form.campoAtencion = txt_Atencion;
                //form.campoPadre = txt_historiaclinica;
                form.ShowDialog();
                if (txt_Atencion.Text != "")
                {
                    Int64 codAtencion = Convert.ToInt64(txt_Atencion.Text);
                    atencion = NegAtenciones.AtencionID(codAtencion);
                    string mascara = string.Empty;

                    for (int i = 0; i < GeneralPAR.TamNumAtencion; i++)
                        mascara = mascara + "9";
                    formulario = true;
                    inicializarForma();
                    Inicializar();
                    deshabilitarCampos();
                    cargarAtencion();

                    //variables para obtener la inf del paciente
                    atencionId = codAtencion;
                    mostrarInfPaciente = true;
                }
                else
                {
                    inicializarForma();
                    Inicializar();
                    deshabilitarCampos();
                    btnNuevo.Enabled = false;
                    cerrar = true;
                }
            }
            else
            {
                subSecuente = _subSecuente;
                atencion = NegAtenciones.AtencionID(ateCodigo);

                if (subSecuente)
                {
                    DataTable signos = new DataTable();
                    signos = NegConsultaExterna.RecuperaSignos(ateCodigo);
                }

                string mascara = string.Empty;

                for (int i = 0; i < GeneralPAR.TamNumAtencion; i++)
                    mascara = mascara + "9";
                formulario = true;
                inicializarForma();
                Inicializar();
                deshabilitarCampos();

                cargarAtencion();

                //variables para obtener la inf del paciente
                atencionId = ateCodigo;
                mostrarInfPaciente = true;
                //btn_Ayuda.Visible = true; // se habilita el boton de ayuda para todas las consultas // 14/04/2023 //Mario
                btn_Microfilms.Visible = true;
            }

        }

        public bool cerrar = false;

        public frm_Evolucion(ATENCIONES nAtencion)
        {
            atencion = nAtencion;
            string mascara = string.Empty;

            for (int i = 0; i < GeneralPAR.TamNumAtencion; i++)
                mascara = mascara + "9";
            formulario = true;
            //atenciones = NegAtenciones.atencionesActivas();

            InitializeComponent();
            //txtNumAtencion.Mask = mascara;
            //inicializarGridPrescripciones();
            inicializarForma();
            deshabilitarCampos();
            cargarAtencion();


            ////aqui se agregara el estado de la cuenta que depende del ate_codigo
            ////NegAtenciones atenciones = new NegAtenciones();
            ////string estado = atenciones.EstadoCuenta(Convert.ToString(nAtencion));

            ////if (estado == "1")
            ////{
            ////    Dejo como esaba si fuese activo
            ////}
            ////else
            ////{
            ////    Bloquear();
            ////}

            ValidaEnfermeria();


        }
        public void ValidaEnfermeria()
        {
            //CAmbios Edgar 20210303 Requerimientos de la pasteur por Alex
            if (His.Entidades.Clases.Sesion.codDepartamento == 6)
            {
                btnEditar.Enabled = false;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                btnImprimir.Enabled = false;
            }
        }
        public void Bloquear()
        {
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = false;
            btnGrabar.Enabled = false;
            btnEliminar.Enabled = false;
            btnEditar.Enabled = false;
            btnCancelar.Enabled = false;
        }
        public void Desbloquear()
        {

        }

        public frm_Evolucion(int codAtencion, bool parMostrarInfPaciente) //aqui se ingresa a evolucion desde habitacion
        {

            atencion = NegAtenciones.AtencionID(codAtencion);
            string mascara = string.Empty;

            for (int i = 0; i < GeneralPAR.TamNumAtencion; i++)
                mascara = mascara + "9";
            formulario = true;
            //atenciones = NegAtenciones.atencionesActivas();

            InitializeComponent();
            //txtNumAtencion.Mask = mascara;
            //inicializarGridPrescripciones();
            //if (MessageBox.Show("¿Desea abrir una evolucion de PSIQUIATRIA?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //{
            //    psqui = true;
            //    utcEvolucion.Tabs["datos"].Visible = false;
            //    utcEvolucion.Tabs["psiquiatria"].Visible = true;
            //    utcEvolucion.SelectedTab = utcEvolucion.Tabs["psiquiatria"];
            //    SendKeys.SendWait("{TAB}");
            //}
            //else
            //{
            //    utcEvolucion.Tabs["datos"].Visible = true;
            //    utcEvolucion.Tabs["psiquiatria"].Visible = false;
            //    utcEvolucion.SelectedTab = utcEvolucion.Tabs["datos"];
            //    SendKeys.SendWait("{TAB}");
            //}
            inicializarForma();
            //Inicializar();
            deshabilitarCampos();
            cargarAtencion();
            //variables para obtener la inf del paciente
            atencionId = codAtencion;
            mostrarInfPaciente = parMostrarInfPaciente;
            btnEditar.Enabled = false;
            //btnEliminar.Enabled = true;
            //Aqui se valida el estado de cuenta del paciente con referencia al ate_codigo de la atencion
            NegAtenciones atenciones = new NegAtenciones();
            string estado = atenciones.EstadoCuenta(Convert.ToString(codAtencion));


            if (estado == "1")
            {
                utcEvolucion.Enabled = true;
                if (btnGuardar.Enabled == true)
                {
                    fechaInicio.Enabled = true;
                    fechaFin.Enabled = true;
                    ckbUCI.Enabled = true;
                }
                //dtpFechaNota.Enabled = true;
            }
            else
            {
                List<PERFILES> perfilUsuario = new NegPerfil().RecuperarPerfil(Sesion.codUsuario);
                bool valido = false;
                //if (estado != "1")// por cambio a seguridades // Mario Valencia 21/12/2023 // cambio en seguridades.
                //{
                //    foreach (var item in perfilUsuario)
                //    {
                //        if (item.ID_PERFIL == 31) //validara con codigo
                //        {
                //            if (item.DESCRIPCION.Contains("HCS")) //valida contra la descripcion
                //            {
                //                valido = true;
                //                break;
                //            }
                //        }
                //        else
                //        {
                //            if (item.DESCRIPCION.Contains("HCS")) //solo valida contra la descripcion
                //            {
                //                valido = true;
                //                break;
                //            }
                //        }
                //    }
                //    if (!valido)
                //        Bloquear();
                //}
                if (estado != "1")
                {
                    foreach (var item in perfilUsuario)
                    {
                        List<ACCESO_OPCIONES> accop = NegUtilitarios.ListaAccesoOpcionesPorPerfil(item.ID_PERFIL, 4);
                        foreach (var items in accop)
                        {
                            if (items.ID_ACCESO == 42000) // Mario Valencia 21/12/2023 // cambio en seguridades.
                            {
                                valido = true;
                                break;
                            }
                            else
                                valido = true;
                        }
                    }
                    if (!valido)
                        Bloquear();
                }
            }
        }

        #endregion

        #region Cargar Datos

        public void Actualizar()
        {
            inicializarForma();
            Inicializar();
            deshabilitarCampos();
            cargarAtencion();
            //variables para obtener la inf del paciente
        }

        private void inicializarForma()
        {
            //inicializarGridPrescripciones();
            txt_notaEvolucion.Scrollbars = ScrollBars.Both;
            btnNuevo.Image = Recursos.Archivo.imgBtnAdd2;
            btnEditar.Image = Recursos.Archivo.imgBtnRestart;
            btnImprimir.Image = Recursos.Archivo.imgBtnGonePrint48;
            btnGuardar.Image = Recursos.Archivo.imgBtnGoneSave48;
            btnEliminar.Image = Recursos.Archivo.imgDelete;
            btnCancelar.Image = Recursos.Archivo.imgCancelar1;
            ultraPictureBox1.Image = Recursos.Archivo.F1;
            ultraPictureBox1.Visible = false;
            DataTable notas = new DataTable();
            //notas = NegAtenciones.RecuperaNotasEvolucion();
            cmbNotas.DataSource = NegAtenciones.RecuperaNotasEvolucion();
            cmbNotas.DisplayMember = "DETALLE_NOTA_EVOLUCION";
            cmbNotas.ValueMember = "ID_NOTA_EVOLUCION";
            if (subSecuente)
            {
                cmbNotas.SelectedValue = 8;
                BloquearCombo();
            }
            //txtNumAtencion.ReadOnly = true;
            //txt_pacHCL.ReadOnly = true;
            //txt_pacNombre.ReadOnly = true;
            txtMedico.ReadOnly = true;
            //txtSexo.ReadOnly = true;
            //txtEdad.ReadOnly = true;

            //txtNumAtencion.BackColor = Color.White;
            //txt_pacHCL.BackColor = Color.White;
            //txtSexo.BackColor = Color.White;
            //txtEdad.BackColor = Color.White;
        }

        private void Inicializar()
        {
            accion = "dibujar";
            puntos = new Point[0];

            //Creo el lapiz y la goma y defino el tipo de linea
            lapiz = new Pen(Color.Black, 1);
            lapiz.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            lapiz.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            lapiz.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
            goma = new Pen(Color.White, 10);
            goma.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            goma.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            goma.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;

            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //creo el cursor del lapiz a partir de una imagen que tengo como recurso
            CrearLapiz();
        }

        private void cargarAtencion(string numAtencion)
        {
            try
            {
                atencion = NegAtenciones.RecuperarAtencionPorNumero(numAtencion);
                gridNotasEvolucion.DataSource = null;
                //gridPrescripciones.Rows.Clear();
                txtindicaciones.Text = "";

                if (atencion != null)
                {
                    paciente = NegPacientes.recuperarPacientePorAtencion(atencion.ATE_CODIGO);
                    //txt_pacNombre.Text = paciente.PAC_APELLIDO_PATERNO + " " +
                    //                     paciente.PAC_APELLIDO_MATERNO + " " +
                    //                     paciente.PAC_NOMBRE1 + " " +
                    //                     paciente.PAC_NOMBRE2;
                    //txt_pacHCL.Text = paciente.PAC_HISTORIA_CLINICA;

                    evolucion = NegEvolucion.recuperarEvolucionPorAtencion(atencion.ATE_CODIGO);
                    cod_Evolucion1 = evolucion.EVO_CODIGO;
                    cargarUltimaNota(evolucion.EVO_CODIGO);
                    cargarNotasEvolucion(evolucion.EVO_CODIGO);

                }
                else
                {
                    //txt_pacNombre.Text = string.Empty;
                    //txt_pacHCL.Text = string.Empty;
                    paciente = null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cargarAtencion()
        {
            try
            {
                gridNotasEvolucion.DataSource = null;
                //gridPrescripciones.Rows.Clear();
                txtindicaciones.Text = "";
                evolucion = NegEvolucion.recuperarEvolucionPorAtencion(atencion.ATE_CODIGO);

                if (evolucion != null)
                {
                    cargarNotasEvolucion(evolucion.EVO_CODIGO);
                    cargarUltimaNota(evolucion.EVO_CODIGO);
                    btnImprimir.Enabled = true;
                }
                if (validaUsuario == 0)
                {


                    //txtNumAtencion.Text = atencion.ATE_NUMERO_ATENCION;
                    //    paciente = NegPacientes.recuperarPacientePorAtencion(atencion.ATE_CODIGO);
                    //    txt_pacNombre.Text = paciente.PAC_APELLIDO_PATERNO + " " +
                    //                         paciente.PAC_APELLIDO_MATERNO + " " +
                    //                         paciente.PAC_NOMBRE1 + " " +
                    //                         paciente.PAC_NOMBRE2;
                    //    txt_pacHCL.Text = paciente.PAC_HISTORIA_CLINICA;
                    //    txtEdad.Text = (DateTime.Now.Year - paciente.PAC_FECHA_NACIMIENTO.Value.Year).ToString();
                    //    txtSexo.Text = paciente.PAC_GENERO;

                    if (evolucion != null)
                    {
                        cargarUltimaNota(evolucion.EVO_CODIGO);
                        cargarNotasEvolucion(evolucion.EVO_CODIGO);
                        CargaMedicosEvolucion(evolucion.EVO_CODIGO);
                        habilitarCampos();
                        btnImprimir.Enabled = true;
                    }
                    if (evolucion == null)
                    {
                        band = false;
                        btnEditar.Enabled = false;
                        btnImprimir.Enabled = false;
                        lblUsuario.Text = "USUARIO: " + Sesion.nomUsuario;
                        ultimaNota1 = null;
                        habilitarCampos2();
                        limpiarCampos();
                        btnBuscaMedico.Enabled = false;
                        btnlimpiarmedicos.Enabled = true;
                        if (!subSecuente)
                        {
                            btnBuscaMedico.Enabled = true;
                            cmbNotas.Enabled = true;
                            txt_notaEvolucion.Text = "NOTA DE INGRESO";
                            txtMedico.Enabled = true;
                        }
                        txt_notaEvolucion.Enabled = true;
                        //gridPrescripciones.Enabled = true;
                        txtindicaciones.Enabled = true;
                        //gridPrescripciones.ReadOnly = false;
                        btnImprimirIndividual.Enabled = false;
                        txtindicaciones.ReadOnly = false;
                    }

                }

                else
                {
                    txt_notaEvolucion.Text = string.Empty;
                    txt_notaEvolucion.ReadOnly = false;
                    txtMedico.Text = Sesion.nomUsuario;
                    //gridPrescripciones.AllowUserToAddRows = true;
                    //gridPrescripciones.Rows.Clear();
                    txtindicaciones.Text = "";
                    btnEditar.Enabled = false;
                    //btnImprimir.Enabled = false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void cargarUltimaNota(int codEvolucion)
        {
            try
            {
                ultimaNota1 = new EVOLUCIONDETALLE();
                ultimaNota1 = NegEvolucionDetalle.ultimaNotaEvolucion(codEvolucion);
                USUARIOS user = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
                lblUsuario.Text = "USUARIO: " + Sesion.nomUsuario;
                if (Sesion.codUsuario == user.ID_USUARIO)
                {
                    if (ultimaNota1 != null && ultimaNota1.evd_fecha != Convert.ToDateTime("01/01/0001 00:00:00"))
                    {
                        txt_notaEvolucion.Text = ultimaNota1.evd_descripcion;
                        //USUARIOS user = NegUsuarios.RecuperaUsuario((Int16)ultimaNota.ID_USUARIO);
                        txtMedico.Text = user.NOMBRES + " " + user.APELLIDOS;
                        dtpFechaNota.Value = (DateTime)ultimaNota1.evd_fecha;
                        lblImpresionNotas.Text = ultimaNota1.nom_usuario;
                        txtCodigo2.Text = Convert.ToString(ultimaNota1.med_codigo);
                        cargarPrescripciones(ultimaNota1.evd_codigo);
                        fechaInicio.Value = ultimaNota1.fechInicio;
                        fechaFin.Value = ultimaNota1.fechaFin;
                        cod_Evolucion1 = codEvolucion;
                        evd_codigo = ultimaNota1.evd_codigo; //cambios Edgar
                        if (Sesion.codUsuario == user.ID_USUARIO)
                        {
                            txt_notaEvolucion.ReadOnly = false;
                            //gridPrescripciones.AllowUserToAddRows = true;
                            btnEditar.Enabled = true;
                        }
                        else
                        {
                            txt_notaEvolucion.ReadOnly = true;
                            //gridPrescripciones.AllowUserToAddRows = false;
                        }
                        //DataTable ds = new DataTable();
                        //ds = NegPrescripciones.RegresaImagen(codEvolucion);
                        //byte[] m_imagen = (byte[])ds.Rows[0]["EVO_IMAGEN"];
                        //MemoryStream m_MemoryStream = new MemoryStream(m_imagen);
                        //pictureBox1.Image = Image.FromStream(m_MemoryStream);


                    }
                    else
                    {
                        txt_notaEvolucion.Text = string.Empty;
                        txt_notaEvolucion.ReadOnly = false;
                        txtMedico.Text = Sesion.nomUsuario;
                        //gridPrescripciones.AllowUserToAddRows = true;
                        //gridPrescripciones.Rows.Clear();
                        txtindicaciones.Text = "";
                    }
                }
                else
                {
                    txt_notaEvolucion.Text = string.Empty;
                    txt_notaEvolucion.ReadOnly = false;
                    txtMedico.Text = Sesion.nomUsuario;
                    //gridPrescripciones.AllowUserToAddRows = true;
                    //gridPrescripciones.Rows.Clear();
                    txtindicaciones.Text = "";
                    btnEditar.Enabled = false;
                    btnImprimir.Enabled = false;
                    validaUsuario = 1;
                }

                var totalHoras = (DateTime.Now - dtpFechaNota.Value).TotalHours;

                if (totalHoras <= 12)
                {
                    btnEditar.Enabled = true;
                }
                else
                    btnEditar.Enabled = false;
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CargaMedicosEvolucion(int codnota)
        {
            try
            {
                band = true;
                gridMedicosEvolucion.Rows.Clear();
                gridMedicosEvolucion.AllowUserToAddRows = true;
                DataTable medicosrecuperados = new DataTable();
                medicosrecuperados = NegAtenciones.RecuperaMedicosEvolucion(evolucion.EVO_CODIGO);
                for (int i = 0; i <= medicosrecuperados.Rows.Count - 1; i++)
                {
                    DataGridViewRow nuevafila = (DataGridViewRow)gridMedicosEvolucion.Rows[0].Clone();
                    nuevafila.Cells[0].Value = medicosrecuperados.Rows[i]["MÉDICO"].ToString();
                    nuevafila.Cells[1].Value = medicosrecuperados.Rows[i]["FECHA EVOLUCIÓN"].ToString();
                    gridMedicosEvolucion.Rows.Add(nuevafila);
                }
                gridMedicosEvolucion.ReadOnly = true;
                band = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio al cargar los medicos tratantes.\r\nConsulte con el Administrador.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Console.WriteLine(ex.Message);
            }
        }

        public void cargarPrescripciones(Int64 codNota)
        {
            try
            {
                band = true;
                //gridPrescripciones.Rows.Clear();
                //gridPrescripciones.AllowUserToAddRows = true;
                txtindicaciones.Text = "";

                listaPrescripciones = NegPrescripciones.listaPrescripciones(codNota);

                foreach (HC_PRESCRIPCIONES item in listaPrescripciones)
                {
                    txtindicaciones.Text += item.PRES_FARMACOTERAPIA_INDICACIONES + "\r\n";
                    pres_codigo.Text = item.PRES_CODIGO.ToString();
                }

                band = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void cargarNotasEvolucion(Int64 codEvolucion)
        {
            gridNotasEvolucion.DataSource = NegEvolucionDetalle.listaNotasEvolucion(codEvolucion);

        }

        #endregion

        #region Otros

        private void habilitarCampos()
        {
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnNuevo.Enabled = true;
            btnEliminar.Enabled = false;
            txt_notaEvolucion.ReadOnly = true;
            //gridPrescripciones.ReadOnly = true;
            txtindicaciones.ReadOnly = true;
            //btnBuscaMedico.Enabled = true;
            //btnlimpiarmedicos.Enabled = true;
            fechaInicio.Enabled = true;
            fechaFin.Enabled = true;


        }
        private void habilitarCampos2()
        {
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnImprimir.Enabled = true;
            fechaInicio.Enabled = true;
            fechaFin.Enabled = true;
            txt_notaEvolucion.ReadOnly = false; //aqui estaba deshabilitado cambios Edgar
            txt_notaEvolucion.Enabled = true;
            //gridPrescripciones.Enabled = true;
            txtindicaciones.Enabled = true;
            //gridPrescripciones.ReadOnly = false; //aqui estaba deshabiliado cambios Edgar
            txtindicaciones.ReadOnly = false;
            btnBuscaMedico.Enabled = true;
            btnlimpiarmedicos.Enabled = true;
            cmbNotas.Enabled = false;
        }

        private void deshabilitarCampos()
        {
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnNuevo.Enabled = true;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            txt_notaEvolucion.ReadOnly = true;
            //gridPrescripciones.ReadOnly = true;
            txtindicaciones.ReadOnly = true;
            btnBuscaMedico.Enabled = false;
            btnlimpiarmedicos.Enabled = false;
            chbSubjetivo.Enabled = true;
            chbSubjetivo.Checked = false;
            chbObjetivo.Checked = false;
            chbObjetivo.Enabled = true;
            chbAnalisis.Enabled = true;
            chbAnalisis.Checked = false;
            chbPlan.Enabled = true;
            chbPlan.Checked = false;
            chbExamenes.Checked = false;
            chbExamenes.Enabled = true;
            groupBox1.Enabled = true;
            fechaFin.Enabled = false;
            fechaInicio.Enabled = false;
        }

        private bool validarFormulario()
        {
            error.Clear();
            bool band = true;
            if (ultimaNota1 == null)
            {
                if (txt_notaEvolucion.Text.ToString() == string.Empty)
                {
                    AgregarError(txt_notaEvolucion);
                    band = false;
                }
            }
            DateTime f1 = fechaInicio.Value;
            DateTime f2 = fechaFin.Value;
            if (f1 == f2)
            {
                AgregarError(fechaInicio);
                AgregarError(fechaFin);
                band = false;
            }
            if (f1.Year == f2.Year && f1.Month == f2.Month && f1.Day == f2.Day &&
    f1.Hour == f2.Hour && f1.Minute == f2.Minute && f1.Second == f2.Second)
            {
                AgregarError(fechaInicio);
                AgregarError(fechaFin);
                band = false;
            }
            if (ckbUCI.Checked == true)
            {
                if (txtindicaciones.Text != "INDICACIONES: " || txtindicaciones.Text != "")
                {
                    band = true;
                }
                else
                {
                    band = false;
                }
            }
            else
            {
                if (txtindicaciones.Text == "")
                {
                    AgregarError(txtindicaciones);
                    band = false;
                }
                //if (gridPrescripciones.Rows.Count >= 0)
                //{
                //    if (gridPrescripciones.Rows[0].Cells["PRES_FARMACOTERAPIA_INDICACIONES"].Value != null)
                //    {
                //        for (Int16 i = 0; i < gridPrescripciones.Rows.Count - 1; i++)
                //        {
                //            DataGridViewRow fila = gridPrescripciones.Rows[i];

                //            if (fila.Cells["PRES_FARMACOTERAPIA_INDICACIONES"].Value == null)
                //            {
                //                AgregarError(gridPrescripciones);
                //                band = false;
                //            }

                //        }
                //    }
                //    else
                //    {
                //        AgregarError(gridPrescripciones);
                //        band = false;
                //    }
                //}
            }

            //gridPrescripciones.CurrentRow.Cells[3].Selected = true;

            return band;
        }

        private void AgregarError(Control control)
        {
            error.SetError(control, "Campo Requerido");

        }

        private void limpiarCampos()
        {
            error.Clear();
            txt_notaEvolucion.ReadOnly = false;
            txtMedico.Text = Sesion.nomUsuario;
            dtpFechaNota.Value = DateTime.Now;
            //gridPrescripciones.Rows.Clear();
            txtindicaciones.Text = "";
            //gridPrescripciones.AllowUserToAddRows = true;

            chbSubjetivo.Checked = false;
            chbObjetivo.Checked = false;
            chbAnalisis.Checked = false;
            chbPlan.Checked = false;
            chbExamenes.Checked = false;

            chbSubjetivo.Enabled = true;
            chbObjetivo.Enabled = true;
            chbAnalisis.Enabled = true;
            chbPlan.Enabled = true;
            chbExamenes.Enabled = true;
            fechaInicio.Value = DateTime.Now;
            fechaFin.Value = DateTime.Now;
            if (!subSecuente)
            {
                txt_notaEvolucion.Text = string.Empty;
                lblImpresionNotas.Text = "Nota de Evolución sin Medico/s.";
            }
            else
            {
                lblImpresionNotas.Text = Sesion.nomUsuario;
                MEDICOS medico = NegMedicos.recuperarMedicoID_Usuario(Sesion.codUsuario);
                txtCodigo2.Text = Convert.ToString(medico.MED_CODIGO);
            }
        }

        #endregion

        #region CRUD
        int imagenlista = 0;
        private void GuardarDatos()
        {
            try
            {
                if (evolucion == null)
                {
                    FORMULARIOS_HCU formul = NegFormulariosHCU.RecuperarFormularioID(4);
                    ATENCION_DETALLE_FORMULARIOS_HCU detalle = new ATENCION_DETALLE_FORMULARIOS_HCU();
                    detalle.ADF_CODIGO = NegAtencionDetalleFormulariosHCU.MaxCodigo() + 1;
                    detalle.FORMULARIOS_HCUReference.EntityKey = formul.EntityKey;
                    detalle.ATENCIONESReference.EntityKey = atencion.EntityKey;
                    detalle.ADF_FECHA_INGRESO = DateTime.Now;
                    if (cmbNotas.Text.Trim() == "NOTA SUBSECUENTE")
                    {
                        txtCodMedico.Text = Sesion.codUsuario.ToString();
                    }
                    if (psqui)
                        detalle.ID_USUARIO = Sesion.codMedico;
                    else
                        detalle.ID_USUARIO = Convert.ToInt16(txtCodMedico.Text);

                    evolucion = new HC_EVOLUCION();
                    evolucion.EVO_CODIGO = NegEvolucion.ultimoCodigo() + 1;
                    evolucion.ATENCIONESReference.EntityKey = atencion.EntityKey;
                    evolucion.EVO_FECHA_CREACION = DateTime.Now;
                    evolucion.ID_USUARIO = Entidades.Clases.Sesion.codUsuario;
                    //evolucion.NOM_USUARIO = Entidades.Clases.Sesion.nomUsuario;
                    //evolucion.ID_USUARIO = Convert.ToInt16(txtCodMedico.Text);
                    evolucion.NOM_USUARIO = lblImpresionNotas.Text + "\r\nRESPONSABLE: " + Sesion.nomUsuario;
                    evolucion.PAC_CODIGO = NegPacientes.recuperarPacientePorAtencion(atencion.ATE_CODIGO).PAC_CODIGO;
                    evolucion.EVO_ESTADO = true;
                    NegEvolucion.crearEvolucion(evolucion);
                    detalle.REF_CODIGO = evolucion.EVO_CODIGO;

                    //cod_Evolucion1 = evolucion.EVO_CODIGO;
                    NegAtencionDetalleFormulariosHCU.Crear(detalle);
                }

                if (ultimaNota1 == null && Editar == false)
                {
                    ultimaNota = new HC_EVOLUCION_DETALLE();
                    ultimaNota.EVD_CODIGO = NegEvolucionDetalle.ultimoCodigo() + 1;
                    ultimaNota.EVD_FECHA = DateTime.Now;
                    ultimaNota.EVD_DESCRIPCION = txt_notaEvolucion.Text;
                    ultimaNota.HC_EVOLUCIONReference.EntityKey = evolucion.EntityKey;
                    ultimaNota.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    //ultimaNota.NOM_USUARIO = His.Entidades.Clases.Sesion.nomUsuario;
                    //ultimaNota.ID_USUARIO = Convert.ToInt16(txtCodMedico.Text);
                    ultimaNota.NOM_USUARIO = lblImpresionNotas.Text;
                    ultimaNota.FECHA_INICIO = fechaInicio.Value;
                    ultimaNota.FECHA_FIN = fechaFin.Value;
                    ultimaNota.MED_TRATANTE = Convert.ToInt32(txtCodigo2.Text);
                    NegEvolucionDetalle.crearEvolucionDetalle(ultimaNota);

                }
                else
                {
                    //ultimaNota1.evd_descripcion = txt_notaEvolucion.Text;
                    NegEvolucionDetalle.editarNotaEvolucionMedica(txt_notaEvolucion.Text, fechaInicio.Value, fechaFin.Value, evd_codigo.ToString(), lblImpresionNotas.Text, txtCodigo2.Text);//cod_Evolucion1.ToString()); //Cambios Edgar 20201201

                    //se cambio la consulta ya ULTIMANOTA no hacia nada, y daba error al momento de editar.
                }
                int codPresc = 0;
                //if (ckbUCI.Checked) AQUI UCI
                //{
                //    gridPrescripciones.Rows[0].Cells["PRES_FARMACOTERAPIA_INDICACIONES"].Value = txtindicaciones.Text;
                //    gridPrescripciones.Rows.Add();
                //    gridPrescripciones.Refresh();
                //}
                HC_PRESCRIPCIONES nuevaPrescripcion = new HC_PRESCRIPCIONES();

                if (pres_codigo.Text == "vacio")
                {
                    nuevaPrescripcion.PRES_CODIGO = NegPrescripciones.ultimoCodigo() + 1;
                    nuevaPrescripcion.PRES_FARMACOTERAPIA_INDICACIONES = txtindicaciones.Text;
                    nuevaPrescripcion.PRES_FECHA = DateTime.Now;
                    nuevaPrescripcion.PRES_ESTADO = false;
                    nuevaPrescripcion.HC_EVOLUCION_DETALLEReference.EntityKey = ultimaNota.EntityKey;
                    NegPrescripciones.crearPrescripcion(nuevaPrescripcion);
                }
                else
                {
                    codPresc = Convert.ToInt32(pres_codigo.Text);
                    nuevaPrescripcion = NegPrescripciones.recuperarPrescripcionID(codPresc);
                    NegPrescripciones pres = new NegPrescripciones();
                    pres.EditarIndicaciones(codPresc.ToString(), txtindicaciones.Text);
                }
                try
                {
                    MemoryStream m_MemoryStream = new MemoryStream();
                    //pictureBox1.Image.Save(m_MemoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    //byte[] m_imagen = m_MemoryStream.ToArray();
                    //NegPrescripciones.IngresaImagen(atencion.ATE_CODIGO, m_imagen);
                    //imagenlista = 1;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error al tratar de guardar imagen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Informacion almacenada correctamente", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                medicosEvolucion = "";
                txt_notaEvolucion.ReadOnly = true;
                //gridPrescripciones.ReadOnly = true;
                txtindicaciones.ReadOnly = true;
                //cargarNotasEvolucion(evolucion.EVO_CODIGO);
                //cod_Evolucion1 = evolucion.EVO_CODIGO;
                //cargarPrescripciones(cod_Evolucion1);
                btnImprimirIndividual.Enabled = true;
                MessageBox.Show("Imprima Si Desea Seguir En La Misma EVOLUCIÓN", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion

        #region Eventos sobres los botones


        private void btnNuevaNotaEvolucion_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevaPrescripcion_Click(object sender, EventArgs e)
        {
            habilitarCampos2();
        }

        #endregion

        private void txtNumAtencion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 && formulario == false)
            {
                frm_Ayudas frm = new frm_Ayudas(atenciones);
                //frm.campoPadre2 = txtNumAtencion;
                //frm.columnabuscada = "";
                frm.bandCampo = true;
                frm.ShowDialog();

                if (frm.campoPadre2.Text != string.Empty)
                    cargarAtencion(frm.campoPadre2.Text.Trim().ToString());

            }
        }

        private void ultraGrid2_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void gridNotasEvolucion_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {

                gridNotasEvolucion.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                gridNotasEvolucion.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;

                gridNotasEvolucion.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
                gridNotasEvolucion.DisplayLayout.Bands[0].Columns["EVD_DESCRIPCION"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_FARMACOTERAPIA_INDICACIONES"].CellMultiLine = Infragistics.Win.DefaultableBoolean.True;

                gridNotasEvolucion.DisplayLayout.Override.RowSizing = RowSizing.AutoFixed;
                gridNotasEvolucion.DisplayLayout.Override.DefaultRowHeight = 20;
                gridNotasEvolucion.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;



                gridNotasEvolucion.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded1;

                gridNotasEvolucion.DisplayLayout.Bands[0].Override.CellAppearance.BackColor = Color.LightCyan;
                gridNotasEvolucion.DisplayLayout.Bands[0].Override.CellAppearance.BackColor2 = Color.Azure;
                gridNotasEvolucion.DisplayLayout.Bands[0].Override.CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;



                gridNotasEvolucion.DisplayLayout.Bands[0].Columns["EVD_CODIGO"].Hidden = true;
                gridNotasEvolucion.DisplayLayout.Bands[0].Columns["ID_USUARIO"].Hidden = true;
                gridNotasEvolucion.DisplayLayout.Bands[0].Columns["HC_EVOLUCION"].Hidden = true;

                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_CODIGO"].Hidden = true;
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["ID_USUARIO"].Hidden = true;
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["HC_EVOLUCION_DETALLE"].Hidden = true;
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_FECHA"].Hidden = true;

                var gridColumn1 = gridNotasEvolucion.DisplayLayout.Bands[0].Columns["FECHA_INICIO"];
                gridNotasEvolucion.DisplayLayout.Bands[0].Columns["EVD_FECHA"].Header.Caption = "FECHA";
                gridColumn1.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DateTime;
                var gridColumn2 = gridNotasEvolucion.DisplayLayout.Bands[0].Columns["FECHA_FIN"];
                gridColumn2.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DateTime;
                gridNotasEvolucion.DisplayLayout.Bands[0].Columns["EVD_DESCRIPCION"].Header.Caption = "NOTA DE EVOLUCION";
                gridNotasEvolucion.DisplayLayout.Bands[0].Columns["NOM_USUARIO"].Header.Caption = "USUARIO";

                gridNotasEvolucion.DisplayLayout.Bands[0].Columns["EVD_FECHA"].CellActivation = Activation.NoEdit;
                gridNotasEvolucion.DisplayLayout.Bands[0].Columns["EVD_DESCRIPCION"].CellActivation = Activation.NoEdit;
                gridNotasEvolucion.DisplayLayout.Bands[0].Columns["NOM_USUARIO"].CellActivation = Activation.NoEdit;

                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_FARMACOTERAPIA_INDICACIONES"].Header.Caption = "INDICACIONES";
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_FARMACOS_INSUMOS"].Header.Caption = "FARMACOS";
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["NOM_USUARIO"].Header.Caption = "USUARIO";
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_ESTADO"].Header.Caption = "ESTADO";
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_FECHA_ADMINISTRACION"].Header.Caption = "FECHA ADMIN";

                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_FARMACOTERAPIA_INDICACIONES"].CellActivation = Activation.NoEdit;
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_FARMACOS_INSUMOS"].CellActivation = Activation.NoEdit;
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["NOM_USUARIO"].CellActivation = Activation.NoEdit;
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_ESTADO"].CellActivation = Activation.NoEdit;
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_FECHA_ADMINISTRACION"].CellActivation = Activation.NoEdit;

                //gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_FECHA"].Header.VisiblePosition = 1;
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_FARMACOTERAPIA_INDICACIONES"].Header.VisiblePosition = 1;
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_FARMACOS_INSUMOS"].Header.VisiblePosition = 2;
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["NOM_USUARIO"].Header.VisiblePosition = 3;
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_FECHA_ADMINISTRACION"].Header.VisiblePosition = 4;
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_ESTADO"].Header.VisiblePosition = 5;

                //gridNotasEvolucion.DisplayLayout.Bands[0].Columns["NOM_USUARIO"].Width = 100;
                gridNotasEvolucion.DisplayLayout.Bands[0].Columns["NOM_USUARIO"].MaxWidth = 200;
                gridNotasEvolucion.DisplayLayout.Bands[0].Columns["NOM_USUARIO"].MinWidth = 200;

                gridNotasEvolucion.DisplayLayout.Bands[0].Columns["EVD_FECHA"].MaxWidth = 70;
                gridNotasEvolucion.DisplayLayout.Bands[0].Columns["EVD_FECHA"].MinWidth = 70;

                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["NOM_USUARIO"].MaxWidth = 200;
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["NOM_USUARIO"].MinWidth = 200;

                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_ESTADO"].MaxWidth = 20;
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_ESTADO"].MinWidth = 20;

                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_FECHA"].MaxWidth = 130;
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_FECHA"].MinWidth = 130;

                //gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_FECHA_ADMINISTRACION"].CellActivation = Activation.NoEdit;

                gridNotasEvolucion.DisplayLayout.Bands[0].Columns["EVD_FECHA"].LockedWidth = true;
                gridNotasEvolucion.DisplayLayout.Bands[1].Columns["PRES_FARMACOTERAPIA_INDICACIONES"].Width = 300;
                gridNotasEvolucion.DisplayLayout.Rows.ExpandAll(false);

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridNotasEvolucion_InitializeGroupByRow(object sender, InitializeGroupByRowEventArgs e)
        {
            e.Row.ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
            e.Row.Expanded = true;
        }

        private void ImprimirReporte(string accion)
        {
            try
            {
                paciente = NegPacientes.recuperarPacientePorAtencion(atencion.ATE_CODIGO);
                ReporteEvolucion reporte = new ReporteEvolucion();
                DataTable responsable = new DataTable();
                NotasEvolucionMedicos notas = new NotasEvolucionMedicos();
                DataRow drnotas;
                drnotas = notas.Tables["NotasEvolucionMedico"].NewRow();
                reporte.FOR_ESTABLECIMIENTO = "";
                reporte.FOR_NOMBRE = paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                reporte.FOR_APELLIDO = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO;
                reporte.FOR_SEXO = paciente.PAC_GENERO;
                reporte.FOR_HISTORIA = paciente.PAC_HISTORIA_CLINICA;

                ReportesHistoriaClinica reporteEpicrisis2 = new ReportesHistoriaClinica();
                reporteEpicrisis2.ingresarEvolucion(reporte);
                // NOTA DE EVOLUCIÓN
                ReporteEvolucionDetalle reporteDetalle = new ReporteEvolucionDetalle();
                List<ReporteEvolucionDetalle> listaDetalleEvolucion = new List<ReporteEvolucionDetalle>();
                for (int i = 0; i < gridNotasEvolucion.Rows.Count; i++)
                {
                    List<ReporteEvolucionDetalle> listaDetalle = new List<ReporteEvolucionDetalle>();
                    int lon = gridNotasEvolucion.Rows[i].Cells[4].Value.ToString().Length;
                    ReporteEvolucionDetalle datosDetalle = new ReporteEvolucionDetalle();
                    datosDetalle.DETE_FECHA = Convert.ToString(gridNotasEvolucion.Rows[i].Cells[3].Value.ToString());
                    datosDetalle.DETE_HORA = (Convert.ToString(gridNotasEvolucion.Rows[i].Cells[3].Value.ToString())).Substring(11, 5);
                    string cadena = gridNotasEvolucion.Rows[i].Cells[4].Value.ToString();
                    int cont = 0;
                    posicion = 0;
                    datosDetalle.DETE_NORAS_EVOLUCION = cadena;
                    listaDetalle.Add(datosDetalle);
                    datosDetalle = new ReporteEvolucionDetalle();
                    //for(int j = 0 ; j< lon; j++)
                    //{
                    //    if(cadena.Length > 2)
                    //    {
                    //        datosDetalle.DETE_NORAS_EVOLUCION = validarTexto(gridNotasEvolucion.Rows[i].Cells[4].Value.ToString(), posicion);
                    //        cadena = gridNotasEvolucion.Rows[i].Cells[4].Value.ToString().Substring(posicion);
                    //        listaDetalle.Add(datosDetalle);
                    //    }else
                    //    {
                    //        j = lon;
                    //        datosDetalle.DETE_NORAS_EVOLUCION = validarTexto(gridNotasEvolucion.Rows[i].Cells[4].Value.ToString(), posicion);
                    //        listaDetalle.Add(datosDetalle);
                    //    }
                    //    datosDetalle = new ReporteEvolucionDetalle();
                    //}
                    listaPrescripciones = NegPrescripciones.listaPrescripciones(Convert.ToInt32(gridNotasEvolucion.Rows[i].Cells[0].Value.ToString()));
                    if (listaDetalle.Count > listaPrescripciones.Count)
                    {
                        reporteDetalle = new ReporteEvolucionDetalle();
                        HC_PRESCRIPCIONES prescripciones = new HC_PRESCRIPCIONES();

                        for (int k = 0; k < listaPrescripciones.Count; k++)
                        {
                            cont += 1;
                            prescripciones = listaPrescripciones.ElementAt(k);
                            reporteDetalle = listaDetalle.ElementAt(k);
                            reporteDetalle.DETE_FARMACOS = prescripciones.PRES_FARMACOTERAPIA_INDICACIONES +
                                "\r\n" + prescripciones.PRES_FARMACOS_INSUMOS;
                            reporteDetalle.DETE_ADMIN_FARM = prescripciones.PRES_FARMACOS_INSUMOS;
                            string prueba = reporteDetalle.DETE_FARMACOS;
                        }
                    }
                    else
                    {
                        reporteDetalle = new ReporteEvolucionDetalle();
                        HC_PRESCRIPCIONES prescripciones = new HC_PRESCRIPCIONES();
                        int indice = 0;
                        for (int k = 0; k < listaDetalle.Count; k++)
                        {
                            cont = +1;
                            prescripciones = listaPrescripciones.ElementAt(k);
                            reporteDetalle = listaDetalle.ElementAt(k);
                            reporteDetalle.DETE_FARMACOS = prescripciones.PRES_FARMACOTERAPIA_INDICACIONES +
                                "\r\n" + prescripciones.PRES_FARMACOS_INSUMOS;
                            reporteDetalle.DETE_ADMIN_FARM = prescripciones.PRES_FARMACOS_INSUMOS;
                            indice = k;
                        }
                        int contaux = 0;
                        for (int j = indice + 1; j < listaPrescripciones.Count; j++)
                        {
                            contaux += 1;
                            reporteDetalle = new ReporteEvolucionDetalle();
                            prescripciones = new HC_PRESCRIPCIONES();
                            prescripciones = listaPrescripciones.ElementAt(j);
                            reporteDetalle.DETE_FECHA = "";
                            reporteDetalle.DETE_HORA = "";
                            reporteDetalle.DETE_NORAS_EVOLUCION = "";
                            reporteDetalle.DETE_PROFESIONAL = "";
                            reporteDetalle.DETE_FARMACOS = prescripciones.PRES_FARMACOTERAPIA_INDICACIONES +
                                "\r\n" + prescripciones.PRES_FARMACOS_INSUMOS;
                            reporteDetalle.DETE_ADMIN_FARM = prescripciones.PRES_FARMACOS_INSUMOS;
                            listaDetalle.Add(reporteDetalle);
                        }
                    }

                    datosDetalle = new ReporteEvolucionDetalle();
                    try
                    {
                        string resp = txt_notaEvolucion.Text;
                        responsable = NegPacientes.RecuperaResponsable(Convert.ToInt64(paciente.PAC_CODIGO), resp);
                        reporteDetalle.DETE_FARMACOS += "\r\n\r\n"
                            + gridNotasEvolucion.Rows[i].Cells["NOM_USUARIO"].Value.ToString() +
                            "\r\nResponsable: " + responsable.Rows[0]["NOMBRE"].ToString();
                        //datosDetalle.DETE_FARMACOS = Convert.ToString(gridNotasEvolucion.Rows[i].Cells[2].Value.ToString()) + 
                        //    "\r\n" + responsable.Rows[0]["NOMBRE"].ToString();
                    }
                    catch (Exception ex)
                    {
                        datosDetalle.DETE_FARMACOS = "";
                    }

                    listaDetalle.Add(datosDetalle);
                    datosDetalle = new ReporteEvolucionDetalle();
                    listaDetalle.Add(datosDetalle);
                    for (int n = 0; n < listaDetalle.Count; n++)
                    {
                        listaDetalleEvolucion.Add(listaDetalle.ElementAt(n));
                    }

                }
                int fila = 0;
                foreach (ReporteEvolucionDetalle reporDet in listaDetalleEvolucion)
                {
                    reporteDetalle = new ReporteEvolucionDetalle();
                    reporteDetalle.DETE_FECHA = reporDet.DETE_FECHA;
                    reporteDetalle.DETE_HORA = reporDet.DETE_HORA;
                    reporteDetalle.DETE_NORAS_EVOLUCION = reporDet.DETE_NORAS_EVOLUCION;
                    reporteDetalle.DETE_PROFESIONAL = reporDet.DETE_PROFESIONAL;
                    reporteDetalle.DETE_FARMACOS = reporDet.DETE_FARMACOS;
                    reporteDetalle.DETE_ADMIN_FARM = reporDet.DETE_ADMIN_FARM;
                    ReportesHistoriaClinica reporteDetalleE = new ReportesHistoriaClinica();
                    reporteDetalleE.ingresarEvolucionDetalle(reporteDetalle);
                    fila++;
                    if (fila == 42)
                    {
                        fila = 0;
                    }
                }
                if (fila < 42)
                {
                    for (int j = fila; j < 42; j++)
                    {
                        reporteDetalle = new ReporteEvolucionDetalle();
                        ReportesHistoriaClinica reporteDetalleE = new ReportesHistoriaClinica();
                        reporteDetalleE.ingresarEvolucionDetalle(reporteDetalle);
                    }
                }



                frmReportes ventana = new frmReportes(1, "evolucion");
                //ventana.Show();

                if (accion.Equals("reporte"))
                    ventana.Show();
                else
                {
                    CrearCarpetas_Srvidor("evolucion");

                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void CrearCarpetas_Srvidor(string modo_formulario)
        {
            try
            {
                His.DatosReportes.Datos.GenerarPdf pdf = new His.DatosReportes.Datos.GenerarPdf();
                pdf.reporte = modo_formulario;
                pdf.campo1 = atencion.ATE_CODIGO.ToString();
                pdf.nuemro_atencion = atencion.ATE_NUMERO_ATENCION.ToString();
                pdf.clinica = paciente.PAC_HISTORIA_CLINICA.ToString();
                pdf.generar();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string validarTexto(string campo, int indice)
        {
            string cadena = "";
            string texto = campo.Substring(indice, (campo.Length - indice));
            int cont = 0;
            for (int i = indice; i < campo.Length; i++)
            {
                if ((posicion < campo.Length - 2) && (cont <= 55))
                {
                    if (!(campo.Substring(i, 2) == "\r\n"))
                    {
                        cadena = cadena + campo.Substring(i, 1);
                        cont++;
                        posicion++;
                    }
                    else
                    {
                        posicion = i + 2;
                        return cadena;
                    }
                }
                else
                {
                    //int pos = CampoValidado(campo, cadena);
                    return cadena;
                }
                if ((posicion + 2 == campo.Length))
                {
                    cadena = cadena + "" + campo.Substring(posicion, 2);
                }
            }
            return cadena;
        }


        private int CampoValidado(string texto, string cadena)
        {
            //string cadena = texto.Substring(posicion,50);
            int indice = (cadena.Length);
            for (int i = cadena.Length; i > (cadena.Length - indice); i--)
            {
                if (((texto.Substring(i - 1, 1)) == " "))
                {
                    indice = i;
                    return indice;
                }
            }
            return indice;
        }

        private void btnNuevo_ButtonClick(object sender, EventArgs e)
        {

        }

        private void txtNumAtencion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_pacHCL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_pacNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void dtpFechaNota_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtMedico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txt_notaEvolucion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void frm_Evolucion_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == GeneralPAR.TeclaNuevo)
            {
                btnNuevo.PerformClick();
            }
            if (e.KeyCode == GeneralPAR.TeclaEditar)
            {
                btnEditar.PerformClick();
            }
            //if (e.KeyCode == GeneralPAR.TeclaEliminar)
            //{
            //    btnEliminar.PerformClick();
            //}
            if (e.KeyCode == GeneralPAR.TeclaGuardar)
            {
                btnGuardar.PerformClick();
            }
            if (e.KeyCode == GeneralPAR.TeclaCancelar)
            {
                btnCancelar.PerformClick();
            }
        }

        private void frm_Evolucion_SizeChanged(object sender, EventArgs e)
        {

        }

        private void frm_Evolucion_Load(object sender, System.EventArgs e)
        {

            // Traduzco los mensajes al espa¤ol en la ventana de correcci¢n ortogr fica
            Infragistics.Shared.ResourceCustomizer rc = new Infragistics.Shared.ResourceCustomizer();

            rc = Infragistics.Win.UltraWinSpellChecker.Resources.Customizer;
            rc.SetCustomizedString("LS_SpellCheckForm", "Ortograf¡a");
            rc.SetCustomizedString("LS_SpellCheckForm_btChange", "&Cambiar");
            rc.SetCustomizedString("LS_SpellCheckForm_btChangeAll", "Cam&biar Todas");
            rc.SetCustomizedString("LS_SpellCheckForm_btClose_1", "Cancelar");
            rc.SetCustomizedString("LS_SpellCheckForm_btClose_2", "Cerrar");
            rc.SetCustomizedString("LS_SpellCheckForm_btIgnoreAll", "Omitir toda&s");
            rc.SetCustomizedString("LS_SpellCheckForm_btIgnoreOnce_1", "Om&itir una vez");
            rc.SetCustomizedString("LS_SpellCheckForm_btIgnoreOnce_2", "&Reanudar");
            rc.SetCustomizedString("LS_SpellCheckForm_btAddToDictionary", "Ag&regar");
            rc.SetCustomizedString("LS_SpellCheckForm_btUndo", "&Deshacer");
            rc.SetCustomizedString("LS_SpellCheckForm_lbErrorsFound", "Se han encontrado errores");
            rc.SetCustomizedString("LS_SpellCheckForm_lbChangeTo", "Cambiar a:");
            rc.SetCustomizedString("LS_SpellCheckForm_lbSuggestions", "Sugerencias:");

            //cargo el diccionario
            //ultraSpellCheckerEvolucion.Dictionary = Application.StartupPath + "\\Recursos\\es-spanish-v2-whole.dict";

            //cargo inf del paciente
            if (mostrarInfPaciente == true)
            {
                //Añado el panel con la informaciòn del paciente
                InfPaciente infPaciente = new InfPaciente(atencionId);
                panelInfPaciente.Controls.Add(infPaciente);
                //cambio las dimenciones de los paneles
                panelInfPaciente.Size = new Size(panelInfPaciente.Width, 110);
                //pantab1.Top = 125;
            }
            if (txtindicaciones.Text == "")
            {
                NegPacientes p = new NegPacientes();
                DataTable Tabla = new DataTable();
                try
                {
                    Tabla = p.LetrasEvolucion();
                    foreach (DataRow item in Tabla.Rows)
                    {
                        txtindicaciones.Text += item[0].ToString().Trim() + "\r\n";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void gridPrescripciones_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

        }



        private void txtSexo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void pantab1_Click(object sender, EventArgs e)
        {

        }

        //codigo para dibujar Pablo Rocha 07/05/2014
        private void agregarPunto(Point punto)
        {
            //agrega los puntos obtenidos al arreglo
            Point[] temp = new Point[puntos.Length + 1];
            puntos.CopyTo(temp, 0);
            puntos = temp;
            puntos[puntos.Length - 1] = punto;
        }

        private void MDibujar()
        {
            //Dibuja
            if (puntos.Length > 1)
            {
                //Aqui usamos dos Graphics, uno que dibuja directamente en el pictureBox y
                //otro que dibuja en la imagen.
                //Esto ayuda a que el dibujo sea rapido ya que si dibujamos solo la imagen
                //y la vamos colocando en el pictureBox puede ser mas lento.
                Graphics g1 = pictureBox1.CreateGraphics();
                Graphics g2 = Graphics.FromImage(bmp);
                g1.DrawLines(lapiz, puntos);
                g2.DrawLines(lapiz, puntos);
                g1.Dispose();
                g2.Dispose();
            }
        }

        private void MBorrar()
        {
            //Borra, es lo mismo que dibujar solo que el objeto Pen es otro
            if (puntos.Length > 1)
            {
                Graphics g1 = pictureBox1.CreateGraphics();
                Graphics g2 = Graphics.FromImage(bmp);
                g1.DrawLines(goma, puntos);
                g2.DrawLines(goma, puntos);
                g1.Dispose();
                g2.Dispose();
            }
        }

        private void CrearGoma()
        {
            //este metodo lo que hace es crear un cursor para la goma a partir de dos
            //elipses, de esta manera el cursor queda como un circulo
            int diametroG = Convert.ToInt32(goma.Width);
            Bitmap Goma = new Bitmap(diametroG, diametroG);

            Graphics gGoma = Graphics.FromImage(Goma);
            gGoma.FillRectangle(Brushes.Magenta, 0, 0, diametroG, diametroG);
            gGoma.FillEllipse(Brushes.White, 0, 0, diametroG - 1, diametroG - 1);
            gGoma.DrawEllipse(new Pen(Color.Black, 1), 0, 0, diametroG - 1, diametroG - 1);
            Goma.MakeTransparent(Color.Magenta);
            gGoma.Dispose();

            IntPtr intprCursorGoma = Goma.GetHicon();
            pictureBox1.Cursor = new Cursor(intprCursorGoma);

        }

        private void CrearLapiz()
        {
            //este metodo lo que hace es crear un cursor para el lapiz a partir de dos
            //elipses, de esta manera el cursor queda como una tiza
            int diametroG = Convert.ToInt32(goma.Width);
            Bitmap Goma = new Bitmap(diametroG, diametroG);

            Graphics gGoma = Graphics.FromImage(Goma);
            gGoma.FillRectangle(Brushes.Magenta, 0, 0, diametroG, diametroG);
            gGoma.FillEllipse(Brushes.White, 0, 0, diametroG + 5, diametroG - 7);
            gGoma.DrawEllipse(new Pen(Color.Black, 1), 0, 0, diametroG + 5, diametroG - 7);
            Goma.MakeTransparent(Color.Magenta);
            gGoma.Dispose();

            IntPtr intprCursorGoma = Goma.GetHicon();
            pictureBox1.Cursor = new Cursor(intprCursorGoma);

        }

        private void Unir(Bitmap fondo)
        {
            //Este metodo une un fondo con lo que se ha dibujado.
            //esto es necesario ya que lo que dibujamos se dibuja sobre un fondo transparente
            //y si no lo unimos puede traer problemas al salvar la imagen o al cambiarle el tamaño
            //al area de dibujo.
            Graphics g = Graphics.FromImage(fondo);
            g.DrawImage(bmp, 0, 0);
            bmp = new Bitmap(fondo);
            g.Dispose();
            fondo.Dispose();
        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            cursorDown = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel4.Text = "X: " + e.X.ToString() + " Y: " + e.Y.ToString();
            if (cursorDown)
            {
                agregarPunto(new Point(e.X, e.Y));
                if (accion == "dibujar")
                {
                    MDibujar();
                }
                if (accion == "borrar")
                {
                    MBorrar();
                }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            cursorDown = false;

            //Aqui reinicializamos a puntos para que no se unan las lineas
            //al volver a dibujar
            puntos = new Point[0];

            //Marcamos como transparente en la imagen donde estamos dibujando todo aquello que sea
            //del color de fondo, esto es necesario para que al cambiar de fondo no se vea lo que borramos
            bmp.MakeTransparent(pictureBox1.BackColor);

            //ponemos la imagen dibujada como fondo
            //esto lo hago aqui y no mientras se dibuja para que el trabajo de dibujar sea mas rapido.
            pictureBox1.Image = bmp;
        }

        private void salvarDibujoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap fondo = new Bitmap(bmp.Width, bmp.Height);
                Graphics g = Graphics.FromImage(fondo);
                g.FillRectangle(new SolidBrush(pictureBox1.BackColor), 0, 0, bmp.Width, bmp.Height);
                g.Dispose();
                Unir(fondo);
                bmp.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            toolStripButton3.Checked = false;
            toolStripButton2.Checked = true;
            CrearLapiz();
            accion = "dibujar";
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            toolStripButton2.Checked = false;
            toolStripButton3.Checked = true;
            CrearGoma();
            accion = "borrar";

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap btemp = new Bitmap(15, 15);
                Graphics gtemp = Graphics.FromImage(btemp);
                gtemp.FillRectangle(new SolidBrush(colorDialog1.Color), 0, 0, 16, 16);
                toolStripButton4.Image = new Bitmap(btemp);
                gtemp.Dispose();
                btemp.Dispose();
                lapiz.Color = colorDialog1.Color;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap btemp = new Bitmap(15, 15);
                Graphics gtemp = Graphics.FromImage(btemp);
                gtemp.FillRectangle(new SolidBrush(colorDialog1.Color), 0, 0, 16, 16);
                toolStripButton5.Image = new Bitmap(btemp);
                gtemp.Dispose();
                btemp.Dispose();
                pictureBox1.BackColor = colorDialog1.Color;
                goma.Color = colorDialog1.Color;
            }
        }

        private void frm_Evolucion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Add)
            {
                if (accion == "dibujar")
                {
                    if (lapiz.Width < 100)
                    {
                        lapiz.Width++;
                    }
                }
                if (accion == "borrar")
                {
                    if (goma.Width < 100)
                    {
                        goma.Width++;
                        CrearGoma();
                    }
                }
            }

            if (e.KeyCode == Keys.Subtract)
            {
                if (accion == "dibujar")
                {
                    if (lapiz.Width > 1)
                    {
                        lapiz.Width--;
                    }
                }
                if (accion == "borrar")
                {
                    if (goma.Width > 10)
                    {
                        goma.Width--;
                        CrearGoma();
                    }
                }
            }

            toolStripStatusLabel2.Text = "Grosor Linea: " + lapiz.Width.ToString();
            toolStripStatusLabel3.Text = "Grosor Goma: " + goma.Width.ToString();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de crear nueva nota de evolución?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ultimaNota1 = null;
                habilitarCampos2();
                limpiarCampos();
                btnBuscaMedico.Enabled = true;
                cmbNotas.Enabled = true;
                btnlimpiarmedicos.Enabled = true;
                txt_notaEvolucion.Text = "NOTA DE INGRESO";
                txtMedico.Enabled = true;
                txt_notaEvolucion.Enabled = true;
                //gridPrescripciones.Enabled = true;
                txtindicaciones.Enabled = true;
                //gridPrescripciones.ReadOnly = false;
                btnImprimirIndividual.Enabled = false;
                txtindicaciones.ReadOnly = false;
                utcEvolucion.SelectedTab = utcEvolucion.Tabs["datos"];
                SendKeys.SendWait("{TAB}");
                chbSubjetivo.Enabled = true;
                chbObjetivo.Enabled = true;
                chbAnalisis.Enabled = true;
                chbPlan.Enabled = true;
                chbExamenes.Enabled = true;
                fechaInicio.Enabled = true;
                fechaFin.Enabled = true;
                Editar = false;
                ckbUCI.Enabled = true;
                pres_codigo.Text = "vacio";
                if (txtindicaciones.Text == "")
                {
                    NegPacientes p = new NegPacientes();
                    DataTable Tabla = new DataTable();
                    try
                    {
                        Tabla = p.LetrasEvolucion();
                        foreach (DataRow item in Tabla.Rows)
                        {
                            txtindicaciones.Text += item[0].ToString().Trim() + "\r\n";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                //if (this.gridPrescripciones.Rows.Count <= 1)
                //{
                //    this.gridPrescripciones.Rows.Clear();
                //    NegPacientes p = new NegPacientes();
                //    DataTable Tabla = new DataTable();
                //    try
                //    {
                //        Tabla = p.LetrasEvolucion();
                //        foreach (DataRow item in Tabla.Rows)
                //        {
                //            this.gridPrescripciones.Rows.Add(null, "", "", item[0].ToString().Trim(), "", DateTime.Now, false);
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message);
                //    }
                //}
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            DateTime startTime = dtpFechaNota.Value;

            DateTime endTime = DateTime.Now;

            TimeSpan span = endTime.Subtract(startTime);
            int hora = DateTime.Now.Hour - dtpFechaNota.Value.Hour;

            Editar = true;
            Int64 dia = NegUtilitarios.validaDiasEpicrisis(66);
            if (NegParametros.EditarEvolucion())
            {
                if (span.Days <= dia)
                {
                    habilitarCampos2();
                    ingresanuevomedico = 0;
                }
                else
                {
                    MessageBox.Show("No se puede editar despues de " + span.TotalHours + " horas.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                habilitarCampos2();
                ingresanuevomedico = 0;
            }

            //if (NegParametros.EditarEvolucion()) // se cambia el parametro estaba quemado a 24 horas por que no hay un parametro definido correctamente // Mario Valencia 20/12/2023 
            //{
            //    if (span.TotalHours <= 24)
            //    {
            //        habilitarCampos2();
            //        ingresanuevomedico = 0;
            //    }
            //    else
            //    {
            //        MessageBox.Show("No se puede editar despues de 24 horas.", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    }
            //}
            //else
            //{
            //    habilitarCampos2();
            //    ingresanuevomedico = 0;
            //}

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {

                if (psqui)
                {
                    DateTime Hoy = DateTime.Now;
                    fechaFin.Value = Hoy;
                    MEDICOS med = new MEDICOS();
                    med = NegMedicos.recuperarMedico(Sesion.codMedico);
                    if (med != null)
                        lblImpresionNotas.Text = med.MED_APELLIDO_PATERNO + " " + med.MED_APELLIDO_MATERNO + " " + med.MED_NOMBRE1 + " " + med.MED_NOMBRE2;
                    else
                        lblImpresionNotas.Text = Sesion.nomUsuario;
                    txt_notaEvolucion.Text = "DATOS DE RELEVANCIA: \r\n" + txtDatosRelevancia.Text + "\r\n\r\n" + "EXPLORACIÓN FÍSICA: \r\n" + txtExploracionFisica.Text + "\r\n\r\n" + "EXAMEN MENTAL: \r\n" + txtExamenMental.Text + "\r\n\r\n" + "ANALISIS DEL CASO: \r\n" + txtAnalisisCaso.Text + "\r\n\r\n" + "TRATAMIENTO: \r\n" + txtTratamiento.Text;
                    txtindicaciones.Text = "EVOLUCIÓN DE PSIQUIATRIA.\r\n";
                }
                if (validarFormulario())
                {
                    if (lblImpresionNotas.Text != "Nota de Evolución sin Medico/s.")
                    {
                        if (fechaInicio.Value < fechaFin.Value)
                        {
                            txt_notaEvolucion.Focus();
                            lblFecha.Focus();
                            GuardarDatos();
                            deshabilitarCampos();
                            // ImprimirReporte("pdf");
                            btnImprimir.Enabled = true;
                            //cmbNotas.Enabled = true;
                            Editar = false;
                            cargarNotasEvolucion(cod_Evolucion1);
                            frm_Evolucion_Load(sender, e);
                            Actualizar();
                            btnEditar.Enabled = false;
                            ckbUCI.Checked = false;
                            ckbUCI.Enabled = false;
                            cargarAtencion();
                            aux = 0;
                        }
                        else
                        {
                            MessageBox.Show("La Fecha Fin de digitación no puede ser menor.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            fechaFin.Value = DateTime.Now;
                        }
                    }
                    else
                        MessageBox.Show("Debe Ingresar Por Lo Menos Un Médico Para Grabar Nota", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Informacion Incompleta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Hubo un error en la coneccion con el servidor, espere unos segundos y vuelva a guardar la información.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Si Continua Va Perder Todos Los Datos,\r\n¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                cargarAtencion();
                deshabilitarCampos();
                cmbNotas.Enabled = false;
                if (evolucion == null)
                {
                    btnEditar.Enabled = false;
                    btnImprimir.Enabled = false;
                    cmbNotas.Enabled = false;
                    btnBuscaMedico.Enabled = false;
                    btnlimpiarmedicos.Enabled = false;
                    aux = 0;

                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirIndividual();
            btnEditar.Enabled = false;

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            NegEvolucionDetalle evolucion = new NegEvolucionDetalle();
            if (His.Entidades.Clases.Sesion.codDepartamento == 5)
            {
                if (MessageBox.Show("¿Está seguro de eliminar la evolución seleccionada?", "HIS3000",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    try
                    {
                        frmLogin x = new frmLogin();
                        x.ShowDialog();
                        if (Eliminar == true)
                        {
                            string observacion = Microsoft.VisualBasic.Interaction.InputBox(
                                "INDICAR LAS RAZONES PARA ELIMINAR LA EVOLUCION Nro " + evocodigo + "",
                                "HIS3000",
                                "SE ELIMINA POR: ");
                            evolucion.EliminarEvolucion(observacion.ToUpper(), His.Entidades.Clases.Sesion.codUsuario, atencionId, evocodigo, evd_codigo);
                            MessageBox.Show("Evolucion Eliminada Correctamente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("No tiene Permiso Suficiente, Consulte con el Director Medico", "HIS3000",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnBuscaMedico_Click(object sender, EventArgs e)
        {
            List<MEDICOS> medicos = NegMedicos.listaMedicos();
            medicos = NegMedicos.listaMedicosIncTipoMedico();
            if (ingresanuevomedico == 1)
            {
                lblImpresionNotas.Text = "";
                ingresanuevomedico = 0;
            }
            string mediconuevo = "";
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(medicos, "MEDICOS", "CODIGO");
            //ayuda.campoPadre = txt;
            ayuda.ShowDialog();

            if (ayuda.campoPadre.Text != string.Empty)
            {
                medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                txtMedico2.Text = medico.MED_APELLIDO_PATERNO.Trim() + " " + medico.MED_APELLIDO_MATERNO.Trim()
                    + " " + medico.MED_NOMBRE1.Trim() + " " + medico.MED_NOMBRE2.Trim();
                //agregarMedico(medico);
                txtCodigo2.Text = medico.MED_CODIGO.ToString();

            }
            if (aux == 0)
            {
                lblImpresionNotas.Text = "";
                aux = 1;
            }
            String[] validador = lblImpresionNotas.Text.Split(',', '\r', '\n');
            foreach (var item in validador)
            {
                string separador = item;
                if (separador != "")
                    if (separador != "\r\n")
                        separador = separador.Substring(6, separador.Length - 6);
                if (separador == txtMedico2.Text)
                {
                    MessageBox.Show("El médico ya ha sido agregado, intente con otro.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            mediconuevo = "Dr/a: " + txtMedico2.Text + ",\r\n";
            medicosEvolucion = medico.MED_CODIGO + ",";
            lblImpresionNotas.Text = mediconuevo;
        }
        //private void agregarMedico(MEDICOS medicoTratante)
        //{

        //        lblImpresionNotas.Text += "Dr/a: " + medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
        //            + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim() + ",\r\n";
        //        //txtCodMedico.Text = medicoTratante.MED_CODIGO.ToString();



        //}

        private void cmbNotas_SelectedIndexChanged(object sender, EventArgs e)
        {
            BloquearCombo();
        }

        public void BloquearCombo()
        {
            limpiarCampos();
            txt_notaEvolucion.Focus();
            txt_notaEvolucion.Text = cmbNotas.Text.TrimEnd() + ": \r\n\r\n";
            cmbNotas.Enabled = false;
            txt_notaEvolucion.Select(txt_notaEvolucion.Text.Length, 0);

            if (txtindicaciones.Text == "")
            {
                NegPacientes p = new NegPacientes();
                DataTable Tabla = new DataTable();
                try
                {
                    Tabla = p.LetrasEvolucion();
                    foreach (DataRow item in Tabla.Rows)
                    {
                        txtindicaciones.Text += item[0].ToString().Trim() + "\r\n";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //if (this.gridPrescripciones.Rows.Count <= 1)
            //{
            //    this.gridPrescripciones.Rows.Clear();
            //    NegPacientes p = new NegPacientes();
            //    DataTable Tabla = new DataTable();
            //    try
            //    {
            //        Tabla = p.LetrasEvolucion();
            //        foreach (DataRow item in Tabla.Rows)
            //        {
            //            this.gridPrescripciones.Rows.Add(null, "", "", item[0].ToString().Trim(), "", DateTime.Now, false);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
            //if (cmbNotas.Text.TrimEnd() == "NOTA DE EVOLUCIÓN")
            //{
            groupBox1.Enabled = true;
            chbSubjetivo.Enabled = true;
            chbObjetivo.Enabled = true;
            chbAnalisis.Enabled = true;
            chbPlan.Enabled = true;
            chbExamenes.Enabled = true;
            //}
            //gridPrescripciones.ReadOnly = false;
            txtindicaciones.ReadOnly = false;
        }

        private void gridPrescripciones_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;

            if (dgv.CurrentCell.ColumnIndex == 3 || dgv.CurrentCell.ColumnIndex == 4)
            {
                TextBox tb = ((TextBox)e.Control);
                tb.CharacterCasing = CharacterCasing.Upper;
            }
        }


        //IMPRIME NOTAS POR SEPARADO PABLO ROCHA 31-05-2020

        //private void ImprimirSeparado()
        //{
        //    try
        //    {
        //        //Cambios 20201221 Edgar
        //        HC_EVOLUCION objEvolucion = new HC_EVOLUCION();
        //        objEvolucion = NegEvolucion.recuperarEvolucionPorAtencion(atencion.ATE_CODIGO);
        //        List<HC_EVOLUCION_DETALLE> lista = new List<HC_EVOLUCION_DETALLE>();
        //        lista = NegEvolucionDetalle.RecuperaEvolucion(objEvolucion.EVO_CODIGO);
        //        ReporteEvolucion reporte = new ReporteEvolucion();
        //        NotasEvolucionMedicos notas = new NotasEvolucionMedicos();
        //        DataRow drnotas;

        //        drnotas = notas.Tables["NotasEvolucionMedico"].NewRow();
        //        drnotas["nombrePaciente"] = paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
        //        reporte.FOR_APELLIDO = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO;
        //        reporte.FOR_SEXO = paciente.PAC_GENERO;
        //        reporte.FOR_HISTORIA = paciente.PAC_HISTORIA_CLINICA;
        //        ReportesHistoriaClinica reporteEpicrisis2 = new ReportesHistoriaClinica();
        //        reporteEpicrisis2.ingresarEvolucion(reporte);
        //        foreach (var item in lista)
        //        {
        //            HC_EVOLUCION_DETALLE objEvoDet = new HC_EVOLUCION_DETALLE();
        //            objEvoDet = NegEvolucionDetalle.RecuperaEvolucionDetalle(objEvolucion.EVO_CODIGO);
        //            List<HC_PRESCRIPCIONES> listaPrescripcion = new List<HC_PRESCRIPCIONES>();
        //            listaPrescripcion = NegEvolucionDetalle.RecuperaEvoPrescripciones(objEvoDet.EVD_CODIGO);

        //            for (int i = 0; i < gridNotasEvolucion.Rows.Count; i++)
        //            {
        //                List<ReporteEvolucionDetalle> listaDetalle = new List<ReporteEvolucionDetalle>();
        //                int lon = gridNotasEvolucion.Rows[i].Cells[4].Value.ToString().Length;
        //                ReporteEvolucionDetalle datosDetalle = new ReporteEvolucionDetalle();
        //                datosDetalle.DETE_FECHA = Convert.ToString(gridNotasEvolucion.Rows[i].Cells[3].Value.ToString());
        //                datosDetalle.DETE_HORA = (Convert.ToString(gridNotasEvolucion.Rows[i].Cells[3].Value.ToString())).Substring(11, 5);
        //                string cadena = gridNotasEvolucion.Rows[i].Cells[4].Value.ToString();
        //                int cont = 0;
        //                posicion = 0;
        //                //#region 1
        //                for (int j = 0; j < lon; j++)
        //                {
        //                    if (cadena.Length > 2)
        //                    {
        //                        datosDetalle.DETE_NORAS_EVOLUCION = validarTexto(gridNotasEvolucion.Rows[i].Cells[4].Value.ToString(), posicion);
        //                        cadena = gridNotasEvolucion.Rows[i].Cells[4].Value.ToString().Substring(posicion);
        //                        listaDetalle.Add(datosDetalle);
        //                    }
        //                    else
        //                    {
        //                        j = lon;
        //                        datosDetalle.DETE_NORAS_EVOLUCION = validarTexto(gridNotasEvolucion.Rows[i].Cells[4].Value.ToString(), posicion);
        //                        listaDetalle.Add(datosDetalle);
        //                    }
        //                    datosDetalle = new ReporteEvolucionDetalle();
        //                }
        //                //listaPrescripciones = NegPrescripciones.listaPrescripciones(Convert.ToInt32(gridNotasEvolucion.Rows[i].Cells[0].Value.ToString()));
        //                //if (listaDetalle.Count > listaPrescripciones.Count)
        //                //{
        //                //    reporteDetalle = new ReporteEvolucionDetalle();
        //                //    HC_PRESCRIPCIONES prescripciones = new HC_PRESCRIPCIONES();

        //                //    for (int k = 0; k < listaPrescripciones.Count; k++)
        //                //    {
        //                //        cont += 1;
        //                //        prescripciones = listaPrescripciones.ElementAt(k);
        //                //        reporteDetalle = listaDetalle.ElementAt(k);
        //                //        reporteDetalle.DETE_FARMACOS = prescripciones.PRES_FARMACOTERAPIA_INDICACIONES +
        //                //            "\r\n" + prescripciones.PRES_FARMACOS_INSUMOS;
        //                //        reporteDetalle.DETE_ADMIN_FARM = prescripciones.PRES_FARMACOS_INSUMOS;
        //                //    }
        //                //}
        //                //else
        //                //{
        //                //    reporteDetalle = new ReporteEvolucionDetalle();
        //                //    HC_PRESCRIPCIONES prescripciones = new HC_PRESCRIPCIONES();
        //                //    int indice = 0;
        //                //    for (int k = 0; k < listaDetalle.Count; k++)
        //                //    {
        //                //        cont = +1;
        //                //        prescripciones = listaPrescripciones.ElementAt(k);
        //                //        reporteDetalle = listaDetalle.ElementAt(k);
        //                //        reporteDetalle.DETE_FARMACOS = prescripciones.PRES_FARMACOTERAPIA_INDICACIONES +
        //                //            "\r\n" + prescripciones.PRES_FARMACOS_INSUMOS;
        //                //        reporteDetalle.DETE_ADMIN_FARM = prescripciones.PRES_FARMACOS_INSUMOS;
        //                //        indice = k;
        //                //    }
        //                //    int contaux = 0;
        //                //    for (int j = indice + 1; j < listaPrescripciones.Count; j++)
        //                //    {
        //                //        contaux += 1;
        //                //        reporteDetalle = new ReporteEvolucionDetalle();
        //                //        prescripciones = new HC_PRESCRIPCIONES();
        //                //        prescripciones = listaPrescripciones.ElementAt(j);
        //                //        reporteDetalle.DETE_FECHA = "";
        //                //        reporteDetalle.DETE_HORA = "";
        //                //        reporteDetalle.DETE_NORAS_EVOLUCION = "";
        //                //        reporteDetalle.DETE_PROFESIONAL = "";
        //                //        reporteDetalle.DETE_FARMACOS = prescripciones.PRES_FARMACOTERAPIA_INDICACIONES +
        //                //            "\r\n" + prescripciones.PRES_FARMACOS_INSUMOS;
        //                //        reporteDetalle.DETE_ADMIN_FARM = prescripciones.PRES_FARMACOS_INSUMOS;
        //                //        listaDetalle.Add(reporteDetalle);
        //                //    }
        //                //}

        //                //datosDetalle = new ReporteEvolucionDetalle();
        //                //try
        //                //{
        //                //    string resp = txt_notaEvolucion.Text;
        //                //    responsable = NegPacientes.RecuperaResponsable(Convert.ToInt64(paciente.PAC_CODIGO), resp);
        //                //    reporteDetalle.DETE_FARMACOS += "\r\n\r\n"
        //                //        + gridNotasEvolucion.Rows[i].Cells["NOM_USUARIO"].Value.ToString() +
        //                //        "\r\nResponsable: " + responsable.Rows[0]["NOMBRE"].ToString();
        //                //    //datosDetalle.DETE_FARMACOS = Convert.ToString(gridNotasEvolucion.Rows[i].Cells[2].Value.ToString()) + 
        //                //    //    "\r\n" + responsable.Rows[0]["NOMBRE"].ToString();
        //                //}
        //                //catch (Exception ex)
        //                //{
        //                //    datosDetalle.DETE_FARMACOS = "";
        //                //}

        //                //listaDetalle.Add(datosDetalle);
        //                //datosDetalle = new ReporteEvolucionDetalle();
        //                //listaDetalle.Add(datosDetalle);
        //                //for (int n = 0; n < listaDetalle.Count; n++)
        //                //{
        //                //    listaDetalleEvolucion.Add(listaDetalle.ElementAt(n));
        //                //}

        //            }
        //        }


        //        //    paciente = NegPacientes.recuperarPacientePorAtencion(atencion.ATE_CODIGO);
        //        //    ReporteEvolucion reporte = new ReporteEvolucion();
        //        //    NotasEvolucionMedicos notas = new NotasEvolucionMedicos();
        //        //    DataRow drnotas;
        //        //    DataTable responsable = new DataTable();
        //        //    drnotas = notas.Tables["NotasEvolucionMedico"].NewRow();
        //        //    reporte.FOR_ESTABLECIMIENTO = "";
        //        //    drnotas["nombrePaciente"] = paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
        //        //    reporte.FOR_APELLIDO = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO;
        //        //    reporte.FOR_SEXO = paciente.PAC_GENERO;
        //        //    reporte.FOR_HISTORIA = paciente.PAC_HISTORIA_CLINICA;
        //        //    ReportesHistoriaClinica reporteEpicrisis2 = new ReportesHistoriaClinica();
        //        //    reporteEpicrisis2.ingresarEvolucion(reporte);
        //        //    // NOTA DE EVOLUCIÓN
        //        //    ReporteEvolucionDetalle reporteDetalle = new ReporteEvolucionDetalle();
        //        //    List<ReporteEvolucionDetalle> listaDetalleEvolucion = new List<ReporteEvolucionDetalle>();
        //        //    for (int i = 0; i < gridNotasEvolucion.Rows.Count; i++)
        //        //    {
        //        //        List<ReporteEvolucionDetalle> listaDetalle = new List<ReporteEvolucionDetalle>();
        //        //        int lon = gridNotasEvolucion.Rows[i].Cells[4].Value.ToString().Length;
        //        //        ReporteEvolucionDetalle datosDetalle = new ReporteEvolucionDetalle();
        //        //        datosDetalle.DETE_FECHA = Convert.ToString(gridNotasEvolucion.Rows[i].Cells[3].Value.ToString());
        //        //        datosDetalle.DETE_HORA = (Convert.ToString(gridNotasEvolucion.Rows[i].Cells[3].Value.ToString())).Substring(11, 5);
        //        //        string cadena = gridNotasEvolucion.Rows[i].Cells[4].Value.ToString();
        //        //        int cont = 0;
        //        //        posicion = 0;
        //        //        #region 1
        //        //        for (int j = 0; j < lon; j++)
        //        //        {
        //        //            if (cadena.Length > 2)
        //        //            {
        //        //                datosDetalle.DETE_NORAS_EVOLUCION = validarTexto(gridNotasEvolucion.Rows[i].Cells[4].Value.ToString(), posicion);
        //        //                cadena = gridNotasEvolucion.Rows[i].Cells[4].Value.ToString().Substring(posicion);
        //        //                listaDetalle.Add(datosDetalle);
        //        //            }
        //        //            else
        //        //            {
        //        //                j = lon;
        //        //                datosDetalle.DETE_NORAS_EVOLUCION = validarTexto(gridNotasEvolucion.Rows[i].Cells[4].Value.ToString(), posicion);
        //        //                listaDetalle.Add(datosDetalle);
        //        //            }
        //        //            datosDetalle = new ReporteEvolucionDetalle();
        //        //        }
        //        //        listaPrescripciones = NegPrescripciones.listaPrescripciones(Convert.ToInt32(gridNotasEvolucion.Rows[i].Cells[0].Value.ToString()));
        //        //        if (listaDetalle.Count > listaPrescripciones.Count)
        //        //        {
        //        //            reporteDetalle = new ReporteEvolucionDetalle();
        //        //            HC_PRESCRIPCIONES prescripciones = new HC_PRESCRIPCIONES();

        //        //            for (int k = 0; k < listaPrescripciones.Count; k++)
        //        //            {
        //        //                cont += 1;
        //        //                prescripciones = listaPrescripciones.ElementAt(k);
        //        //                reporteDetalle = listaDetalle.ElementAt(k);
        //        //                reporteDetalle.DETE_FARMACOS = prescripciones.PRES_FARMACOTERAPIA_INDICACIONES +
        //        //                    "\r\n" + prescripciones.PRES_FARMACOS_INSUMOS;
        //        //                reporteDetalle.DETE_ADMIN_FARM = prescripciones.PRES_FARMACOS_INSUMOS;
        //        //            }
        //        //        }
        //        //        else
        //        //        {
        //        //            reporteDetalle = new ReporteEvolucionDetalle();
        //        //            HC_PRESCRIPCIONES prescripciones = new HC_PRESCRIPCIONES();
        //        //            int indice = 0;
        //        //            for (int k = 0; k < listaDetalle.Count; k++)
        //        //            {
        //        //                cont = +1;
        //        //                prescripciones = listaPrescripciones.ElementAt(k);
        //        //                reporteDetalle = listaDetalle.ElementAt(k);
        //        //                reporteDetalle.DETE_FARMACOS = prescripciones.PRES_FARMACOTERAPIA_INDICACIONES +
        //        //                    "\r\n" + prescripciones.PRES_FARMACOS_INSUMOS;
        //        //                reporteDetalle.DETE_ADMIN_FARM = prescripciones.PRES_FARMACOS_INSUMOS;
        //        //                indice = k;
        //        //            }
        //        //            int contaux = 0;
        //        //            for (int j = indice + 1; j < listaPrescripciones.Count; j++)
        //        //            {
        //        //                contaux += 1;
        //        //                reporteDetalle = new ReporteEvolucionDetalle();
        //        //                prescripciones = new HC_PRESCRIPCIONES();
        //        //                prescripciones = listaPrescripciones.ElementAt(j);
        //        //                reporteDetalle.DETE_FECHA = "";
        //        //                reporteDetalle.DETE_HORA = "";
        //        //                reporteDetalle.DETE_NORAS_EVOLUCION = "";
        //        //                reporteDetalle.DETE_PROFESIONAL = "";
        //        //                reporteDetalle.DETE_FARMACOS = prescripciones.PRES_FARMACOTERAPIA_INDICACIONES +
        //        //                    "\r\n" + prescripciones.PRES_FARMACOS_INSUMOS;
        //        //                reporteDetalle.DETE_ADMIN_FARM = prescripciones.PRES_FARMACOS_INSUMOS;
        //        //                listaDetalle.Add(reporteDetalle);
        //        //            }
        //        //        }

        //        //        datosDetalle = new ReporteEvolucionDetalle();
        //        //        try
        //        //        {
        //        //            string resp = txt_notaEvolucion.Text;
        //        //            responsable = NegPacientes.RecuperaResponsable(Convert.ToInt64(paciente.PAC_CODIGO), resp);
        //        //            reporteDetalle.DETE_FARMACOS += "\r\n\r\n"
        //        //                + gridNotasEvolucion.Rows[i].Cells["NOM_USUARIO"].Value.ToString() +
        //        //                "\r\nResponsable: " + responsable.Rows[0]["NOMBRE"].ToString();
        //        //            //datosDetalle.DETE_FARMACOS = Convert.ToString(gridNotasEvolucion.Rows[i].Cells[2].Value.ToString()) + 
        //        //            //    "\r\n" + responsable.Rows[0]["NOMBRE"].ToString();
        //        //        }
        //        //        catch (Exception ex)
        //        //        {
        //        //            datosDetalle.DETE_FARMACOS = "";
        //        //        }

        //        //        listaDetalle.Add(datosDetalle);
        //        //        datosDetalle = new ReporteEvolucionDetalle();
        //        //        listaDetalle.Add(datosDetalle);
        //        //        for (int n = 0; n < listaDetalle.Count; n++)
        //        //        {
        //        //            listaDetalleEvolucion.Add(listaDetalle.ElementAt(n));
        //        //        }

        //        //    }
        //        //    int fila = 0;
        //        //    foreach (ReporteEvolucionDetalle reporDet in listaDetalleEvolucion)
        //        //    {
        //        //        reporteDetalle = new ReporteEvolucionDetalle();
        //        //        reporteDetalle.DETE_FECHA = reporDet.DETE_FECHA;
        //        //        reporteDetalle.DETE_HORA = reporDet.DETE_HORA;
        //        //        reporteDetalle.DETE_NORAS_EVOLUCION = reporDet.DETE_NORAS_EVOLUCION;
        //        //        reporteDetalle.DETE_PROFESIONAL = reporDet.DETE_PROFESIONAL;
        //        //        reporteDetalle.DETE_FARMACOS = reporDet.DETE_FARMACOS;
        //        //        reporteDetalle.DETE_ADMIN_FARM = reporDet.DETE_ADMIN_FARM;
        //        //        ReportesHistoriaClinica reporteDetalleE = new ReportesHistoriaClinica();
        //        //        reporteDetalleE.ingresarEvolucionDetalle(reporteDetalle);
        //        //        fila++;
        //        //        if (fila == 42)
        //        //        {
        //        //            fila = 0;
        //        //        }
        //        //    }
        //        //    if (fila < 42)
        //        //    {
        //        //        for (int j = fila; j < 42; j++)
        //        //        {
        //        //            reporteDetalle = new ReporteEvolucionDetalle();
        //        //            ReportesHistoriaClinica reporteDetalleE = new ReportesHistoriaClinica();
        //        //            reporteDetalleE.ingresarEvolucionDetalle(reporteDetalle);
        //        //        }
        //        //    }



        //        //    frmReportes ventana = new frmReportes(1, "evolucion");
        //        //    //ventana.Show();

        //        //    if (accion.Equals("reporte"))
        //        //        ventana.Show();
        //        //    else
        //        //    {
        //        //        CrearCarpetas_Srvidor("evolucion");

        //        //    }

        //    }
        //    catch (Exception er)
        //    {
        //        MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    //#endregion 1

        //}

        public byte[] ImageToByteArray(string imagepath)
        {

            FileStream fs;

            fs = new FileStream(imagepath, FileMode.Open, FileAccess.Read);

            //a byte array to read the image

            byte[] picbyte = new byte[fs.Length];

            fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));

            fs.Close();

            return picbyte;
        }
        private void ImprimirIndividual()
        {
            try
            {
                //Cambios 20201221 Edgar
                HC_EVOLUCION objEvolucion = new HC_EVOLUCION();
                objEvolucion = NegEvolucion.recuperarEvolucionPorAtencion(atencion.ATE_CODIGO);
                List<HC_EVOLUCION_DETALLE> lista = new List<HC_EVOLUCION_DETALLE>();
                lista = NegEvolucionDetalle.RecuperaEvolucion(objEvolucion.EVO_CODIGO);
                NegCertificadoMedico C = new NegCertificadoMedico();
                paciente = NegPacientes.recuperarPacientePorAtencion(atencion.ATE_CODIGO);
                NotasEvolucionMedicos notas = new NotasEvolucionMedicos();

                foreach (var item in lista)
                {
                    DataRow drnotas;
                    drnotas = notas.Tables["NotasEvolucionMedico"].NewRow();
                    drnotas["nombrePaciente"] = paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                    drnotas["apellidoPaciente"] = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO;
                    drnotas["sexoPaciente"] = paciente.PAC_GENERO;
                    if (!NegParametros.ParametroFormularios())
                        drnotas["historiaClinica"] = paciente.PAC_HISTORIA_CLINICA;
                    else
                        drnotas["historiaClinica"] = paciente.PAC_IDENTIFICACION;
                    drnotas["notaEvolucion"] = cmbNotas.Text;
                    drnotas["Logo"] = C.path();
                    drnotas["especialistaNota"] = Sesion.nomEmpresa;
                    char condicional = ' ';
                    HC_EVOLUCION_DETALLE objEvoDet = new HC_EVOLUCION_DETALLE();
                    objEvoDet = NegEvolucionDetalle.RecuperaEvolucionDetalle(item.EVD_CODIGO);
                    List<HC_PRESCRIPCIONES> listaPrescripcion = new List<HC_PRESCRIPCIONES>();
                    listaPrescripcion = NegEvolucionDetalle.RecuperaEvoPrescripciones(objEvoDet.EVD_CODIGO);
                    NegEvolucionDetalle evo = new NegEvolucionDetalle();
                    //llenado con la fecha y la hora de la tabla de evolucion detalle
                    DataTable FechaHora = new DataTable();
                    FechaHora = evo.FechaHoraEvolucion(item.EVD_CODIGO);
                    string fechainicio = FechaHora.Rows[0][0].ToString();
                    string fechafin = FechaHora.Rows[0][1].ToString();
                    string[] fechaseparada = fechainicio.Split(condicional);
                    string[] fechafinseparada = fechafin.Split(condicional);
                    drnotas["fechaNota"] = fechaseparada[0];
                    DateTime horainicio = Convert.ToDateTime(fechafinseparada[1]);
                    DateTime horafin = Convert.ToDateTime(fechaseparada[1]);
                    drnotas["horaNota"] = horainicio.ToShortTimeString();
                    drnotas["horafinNota"] = horafin.ToShortTimeString();
                    drnotas["detalleNota"] = item.EVD_DESCRIPCION;
                    drnotas["fechafinNota"] = fechafinseparada[0];
                    USUARIOS user = NegEvolucionDetalle.RecuperarUsuario(Convert.ToInt32(item.ID_USUARIO));
                    drnotas["responsableNota"] = "\r\nMEDICO(S): " + item.NOM_USUARIO + "\r\nRESPONSABLE:" + user.APELLIDOS + " " + user.NOMBRES;
                    foreach (var i in listaPrescripcion)
                    {
                        drnotas["detallefarmacosNota"] += i.PRES_FARMACOTERAPIA_INDICACIONES + "\r\n";
                    }
                    notas.Tables["NotasEvolucionMedico"].Rows.Add(drnotas);
                }
                DataRow cabecera;
                cabecera = notas.Tables["Encabezado"].NewRow();
                cabecera["institucion"] = Sesion.nomEmpresa;
                cabecera["unicodigo"] = "-";
                cabecera["establecimiento"] = Sesion.nomEmpresa;
                cabecera["hc"] = paciente.PAC_IDENTIFICACION;
                cabecera["narchivo"] = paciente.PAC_HISTORIA_CLINICA + "-" + atencion.ATE_NUMERO_ATENCION;
                cabecera["papellido"] = paciente.PAC_APELLIDO_PATERNO;
                cabecera["sapellido"] = paciente.PAC_APELLIDO_MATERNO;
                cabecera["pnombre"] = paciente.PAC_NOMBRE1;
                cabecera["snombre"] = paciente.PAC_NOMBRE2;
                cabecera["fnacimiento"] = paciente.PAC_FECHA_NACIMIENTO;
                if (paciente.PAC_GENERO == "M")
                    cabecera["sexo"] = "HOMBRE";
                else
                    cabecera["sexo"] = "MUJER";

                cabecera["edad"] = NegUtilitarios.EdadCalculada(Convert.ToDateTime(paciente.PAC_FECHA_NACIMIENTO), (DateTime)objEvolucion.EVO_FECHA_CREACION);
                cabecera["x"] = "-";
                notas.Tables["Encabezado"].Rows.Add(cabecera);
                frmReportes ventana = new frmReportes(1, "evolucion", notas);
                ventana.Show();
                //NotasIndividualesMedicos reporte = new NotasIndividualesMedicos();

                //reporte.SetDataSource(notas);
                //CrystalDecisions.Windows.Forms.CrystalReportViewer vista = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                //vista.ReportSource = reporte;
                //vista.PrintReport();
                //cmbNotas.Enabled = true;
                //btnImprimirIndividual.Enabled = false;
                //btnReimprime.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnImprimirIndividual_Click(object sender, EventArgs e)
        {
            try
            {
                //Cambios 20201221 Edgar
                HC_EVOLUCION objEvolucion = new HC_EVOLUCION();
                objEvolucion = NegEvolucion.recuperarEvolucionPorAtencion(atencion.ATE_CODIGO);
                List<HC_EVOLUCION_DETALLE> lista = new List<HC_EVOLUCION_DETALLE>();
                lista = NegEvolucionDetalle.RecuperaEvolucion(objEvolucion.EVO_CODIGO);

                paciente = NegPacientes.recuperarPacientePorAtencion(atencion.ATE_CODIGO);
                NotasEvolucionMedicos notas = new NotasEvolucionMedicos();


                foreach (var item in lista)
                {
                    DataRow drnotas;
                    drnotas = notas.Tables["NotasEvolucionMedico"].NewRow();
                    drnotas["nombrePaciente"] = paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                    drnotas["apellidoPaciente"] = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO;
                    drnotas["sexoPaciente"] = paciente.PAC_GENERO;
                    drnotas["historiaClinica"] = paciente.PAC_HISTORIA_CLINICA;
                    drnotas["notaEvolucion"] = cmbNotas.Text;
                    char condicional = ' ';
                    HC_EVOLUCION_DETALLE objEvoDet = new HC_EVOLUCION_DETALLE();
                    objEvoDet = NegEvolucionDetalle.RecuperaEvolucionDetalle(item.EVD_CODIGO);
                    List<HC_PRESCRIPCIONES> listaPrescripcion = new List<HC_PRESCRIPCIONES>();
                    listaPrescripcion = NegEvolucionDetalle.RecuperaEvoPrescripciones(objEvoDet.EVD_CODIGO);
                    string fecha = Convert.ToString(item.EVD_FECHA);
                    string[] fechaseparada = fecha.Split(condicional);
                    drnotas["fechaNota"] = fechaseparada[0];
                    drnotas["horaNota"] = fechaseparada[1];
                    drnotas["detalleNota"] = item.EVD_DESCRIPCION;
                    drnotas["responsableNota"] = item.NOM_USUARIO + "\r\nRESPONSABLE:" + His.Entidades.Clases.Sesion.nomUsuario;
                    foreach (var i in listaPrescripcion)
                    {
                        drnotas["detallefarmacosNota"] += i.PRES_FARMACOTERAPIA_INDICACIONES + "\r\n";
                    }
                    notas.Tables["NotasEvolucionMedico"].Rows.Add(drnotas);
                }
                NotasIndividualesMedicos reporte = new NotasIndividualesMedicos();

                reporte.SetDataSource(notas);
                CrystalDecisions.Windows.Forms.CrystalReportViewer vista = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
                vista.ReportSource = reporte;
                vista.PrintReport();
                cmbNotas.Enabled = true;
                btnImprimirIndividual.Enabled = false;
                btnReimprime.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void gridNotasEvolucion_ClickCell(object sender, ClickCellEventArgs e)
        {

        }

        private void btnReimprime_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está Seguro/a De Copiar Nota De Evolución?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ultimaNota1 = null;
                btnReimprime.Visible = false;
                btnBuscaMedico.Enabled = true;
                habilitarCampos2();
                txt_notaEvolucion.Enabled = true;
                //gridPrescripciones.Enabled = true;
                txtindicaciones.Enabled = true;
                btnImprimirIndividual.Enabled = false;
                dtpFechaNota.Value = DateTime.Now;
                //gridPrescripciones.AllowUserToAddRows = true;
                lblcopia.Visible = true;
            }
        }

        private void gridNotasEvolucion_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Usted va a replazar la información Actual \r\n¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //gridPrescripciones.Visible = true;
                    //txtindicaciones.Visible = false;
                    lblImpresionNotas.Text = "";
                    //gridPrescripciones.Rows.Clear();
                    txtindicaciones.Text = "";
                    //gridPrescripciones.Rows.Add();
                    txtMedico.Text = gridNotasEvolucion.ActiveRow.Cells["NOM_USUARIO"].Value.ToString();
                    lblImpresionNotas.Text = gridNotasEvolucion.ActiveRow.Cells["NOM_USUARIO"].Value.ToString();
                    txt_notaEvolucion.Text = gridNotasEvolucion.ActiveRow.Cells["EVD_DESCRIPCION"].Value.ToString();

                    string fecha = gridNotasEvolucion.ActiveRow.Cells["EVD_FECHA"].Value.ToString();
                    lblFecha.Text = fecha;
                    DataTable cargaFarmacos = new DataTable();
                    cargaFarmacos = NegPacientes.RecuperaFarmacos(fecha, txtMedico.Text, txt_notaEvolucion.Text);
                    for (int i = 0; i <= cargaFarmacos.Rows.Count - 1; i++)
                    {
                        try
                        {
                            txtindicaciones.Text += cargaFarmacos.Rows[i]["PRES_FARMACOTERAPIA_INDICACIONES"].ToString() + "\r\n";
                            //gridPrescripciones.Rows[i].Cells[3].Value = cargaFarmacos.Rows[i]["PRES_FARMACOTERAPIA_INDICACIONES"].ToString();
                            //gridPrescripciones.Rows[i].Cells[4].Value = cargaFarmacos.Rows[i]["PRES_FARMACOS_INSUMOS"].ToString();
                            //gridPrescripciones.Rows[i].Cells[5].Value = cargaFarmacos.Rows[i]["PRES_FECHA_ADMINISTRACION"].ToString();
                            cod_Evolucion1 = Convert.ToInt32(cargaFarmacos.Rows[i]["EVD_CODIGO"].ToString());
                            evd_codigo = Convert.ToInt64(cargaFarmacos.Rows[i]["EVD_CODIGO"].ToString());
                            //gridPrescripciones.Rows[i].Cells[0].Value = cargaFarmacos.Rows[i]["PRES_CODIGO"].ToString();
                            pres_codigo.Text = cargaFarmacos.Rows[i]["PRES_CODIGO"].ToString();
                            evocodigo = Convert.ToInt32(cargaFarmacos.Rows[i]["EVO_CODIGO"].ToString());
                            //gridPrescripciones.Rows[i].Cells[6]. = cargaFarmacos.Rows[i]["a"].ToString();
                            //gridPrescripciones.Rows.Add();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    //cambios edgar 20201204
                    ultimaNota1 = NegEvolucionDetalle.ultimaNotaEvolucion(cod_Evolucion1);
                    //----------------------------------------------------------------------
                    txtMedico.Enabled = false;
                    txt_notaEvolucion.Enabled = false;
                    //gridPrescripciones.Enabled = false;
                    txtindicaciones.Enabled = false;
                    btnImprimirIndividual.Enabled = true;
                    //gridPrescripciones.AllowUserToAddRows = false;
                    btnBuscaMedico.Enabled = false;
                    btnlimpiarmedicos.Enabled = false;
                    btnReimprime.Visible = true;
                    btnEditar.Enabled = true;
                    btnEliminar.Enabled = true;
                    MessageBox.Show("Nota De EVOLUCIÓN lista para reimprimir", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    utcEvolucion.SelectedTab = utcEvolucion.Tabs["datos"];
                    SendKeys.SendWait("{TAB}");
                    ValidaEnfermeria();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nota De Evolución No Se Puede Imprimir", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void gridPrescripciones_Paint(object sender, PaintEventArgs e)
        //{
        //    if (multilinea == true)
        //    {
        //        multilinea = false;
        //        this.gridPrescripciones.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
        //    }
        //}

        //private void gridPrescripciones_Scroll(object sender, ScrollEventArgs e)
        //{
        //    this.gridPrescripciones.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);

        //}

        //private void gridPrescripciones_RowEnter(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        //{
        //    this.gridPrescripciones.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
        //    multilinea = true;
        //    //gridPrescripciones.Rows.Add();
        //}

        //private void gridMedicosEvolucion_Paint(object sender, PaintEventArgs e)
        //{
        //    this.gridPrescripciones.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
        //    gridMedicosEvolucion.ReadOnly = true;
        //}

        SpeechRecognitionEngine escucha = new SpeechRecognitionEngine();
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Va A Iniciar Reconocimiento De Voz\r\n¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    txt_notaEvolucion.Focus();
                    txt_notaEvolucion.Select(txt_notaEvolucion.Text.Length, 0);
                    btnGrabar.Enabled = false;
                    btnParar.Enabled = true;
                    progressBar1.Visible = true;
                    escucha.SetInputToDefaultAudioDevice();
                    escucha.LoadGrammar(new DictationGrammar());
                    escucha.SpeechRecognized += reconocedor;
                    escucha.RecognizeAsync(RecognizeMode.Multiple);
                    //escucha.AudioLevelUpdated += NivelAudio;
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Micrófono En Uso", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reconocedor(object sender, SpeechRecognizedEventArgs e)
        {

            txt_notaEvolucion.Text = e.Result.Text;
            ////int pos = gridPrescripciones.CurrentRow.Index;

            //foreach (RecognizedWordUnit palabra in e.Result.Words)
            //{
            //    if (palabra.Text == "Evolucion")
            //    {
            //        grabaEvolucion = 1;
            //        grabaIndicaciones = 0;
            //        grabaFarmacos = 0;
            //    }
            //    else if (palabra.Text == "Indicaciones")
            //    {
            //        grabaEvolucion = 0;
            //        grabaIndicaciones = 1;
            //        grabaFarmacos = 0;
            //    }
            //    else if (palabra.Text == "Farmacos")
            //    {
            //        grabaEvolucion = 0;
            //        grabaIndicaciones = 0;
            //        grabaFarmacos = 1;
            //    }

            //    if (palabra.Text == "coma")
            //    {
            //        txt_notaEvolucion.Text += ",";
            //    }
            //    else if (palabra.Text == "punto")
            //    {
            //        txt_notaEvolucion.Text += ".";
            //    }
            //    else if (palabra.Text == "punto y coma")
            //    {
            //        txt_notaEvolucion.Text += ";";
            //    }
            //    else if (palabra.Text == "dos puntos")
            //    {
            //        txt_notaEvolucion.Text += ":";
            //    }
            //    else if (palabra.Text == "punto final")
            //    {
            //        txt_notaEvolucion.Text += ".\r\n";
            //    }

            //    //if (grabaEvolucion == 1)
            //    txt_notaEvolucion.Text += " " + palabra.Text;
            //    //else if (grabaIndicaciones == 1)
            //    //    //gridPrescripciones.Rows[pos].Cells["INDICACIONES"].Value = " " + palabra.Text;
            //    //else if (grabaFarmacos == 1)
            //    //gridPrescripciones.Rows[pos].Cells["FARMACOS"].Value = " " + palabra.Text;
            //}
        }
        public void NivelAudio(object sender, AudioLevelUpdatedEventArgs e)
        {
            int audio = e.AudioLevel;
            progressBar1.Value = audio;
        }

        private void btnParar_Click(object sender, EventArgs e)
        {
            escucha.RecognizeAsyncStop();
            //MessageBox.Show("Micrófono Apagado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //btnGrabar.Enabled = true;
            //btnParar.Enabled = false;
            //progressBar1.Visible = false;
        }

        private void chbSubjetivo_Click(object sender, EventArgs e)
        {
            txt_notaEvolucion.Focus();
            chbSubjetivo.Enabled = false;
            txt_notaEvolucion.Text += "\r\n\r\n" + cmbNotas.Text.TrimEnd() + ": SUBJETIVO\r\n\r\n";
            txt_notaEvolucion.Select(txt_notaEvolucion.Text.Length, 0);
        }

        private void chbObjetivo_Click(object sender, EventArgs e)
        {
            txt_notaEvolucion.Focus();
            chbObjetivo.Enabled = false;
            txt_notaEvolucion.Text += "\r\n\r\n" + cmbNotas.Text.TrimEnd() + ": OBJETIVO\r\n\r\n";
            txt_notaEvolucion.Select(txt_notaEvolucion.Text.Length, 0);
        }

        private void chbAnalisis_Click(object sender, EventArgs e)
        {
            txt_notaEvolucion.Focus();
            chbAnalisis.Enabled = false;
            txt_notaEvolucion.Text += "\r\n\r\n" + cmbNotas.Text.TrimEnd() + ": ANÁLISIS\r\n\r\n";
            txt_notaEvolucion.Select(txt_notaEvolucion.Text.Length, 0);
        }

        private void chbPlan_Click(object sender, EventArgs e)
        {
            txt_notaEvolucion.Focus();
            chbPlan.Enabled = false;
            txt_notaEvolucion.Text += "\r\n\r\n" + cmbNotas.Text.TrimEnd() + ": PLAN\r\n\r\n";
            txt_notaEvolucion.Select(txt_notaEvolucion.Text.Length, 0);
        }

        private void ckbUCI_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbUCI.Checked == false)
            {
                //this.gridPrescripciones.Rows.Clear();
                NegPacientes p = new NegPacientes();
                DataTable Tabla = new DataTable();
                try
                {
                    Tabla = p.LetrasEvolucion();
                    txtindicaciones.Text = "";
                    foreach (DataRow item in Tabla.Rows)
                    {
                        txtindicaciones.Text += item[0].ToString().Trim() + "\r\n";
                    }
                    //foreach (DataRow item in Tabla.Rows)
                    //{
                    //    this.gridPrescripciones.Rows.Add(null, "", "", item[0].ToString().Trim(), "", DateTime.Now, false);
                    //}
                    //txtindicaciones.Visible = false;
                    //gridPrescripciones.Visible = true;
                    //txtindicaciones.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                if (MessageBox.Show("¿Está seguro de Eliminar Información Actual?", "HIS3000",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    //gridPrescripciones.Rows.Clear();
                    //gridPrescripciones.Visible = false;
                    txtindicaciones.Visible = true;
                    txtindicaciones.Text = "INDICACIONES: ";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de Eliminar todos los médicos?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                == DialogResult.Yes)
            {
                ingresanuevomedico = 1;
                lblImpresionNotas.Text = "Nota de Evolución sin Medico/s.";
                medicosEvolucion = "";
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFirmaElectronica_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"C:\Program Files\FirmaEC\firmador.exe");
            }
            catch
            {
                MessageBox.Show("La aplicacion para firmar no se encuentra instalada o tiene un problema pida soporte del departamento de sistemas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void fechaInicio_ValueChanged(object sender, EventArgs e)
        {
            if (fechaInicio.Value.Date > DateTime.Now.Date)
            {
                fechaInicio.Value = DateTime.Now;
            }
        }

        private void fechaFin_ValueChanged(object sender, EventArgs e)
        {
            if (fechaFin.Value.Date > DateTime.Now.Date)
            {
                fechaFin.Value = DateTime.Now;
            }
        }

        private void btn_Ayuda_Click(object sender, EventArgs e)
        {
            paciente = NegPacientes.recuperarPacientePorAtencion(atencion.ATE_CODIGO);
            //frm_AyudaPacientes ayuda = new frm_AyudaPacientes(true, true);
            //ayuda.HC = paciente.PAC_CODIGO;
            //ayuda.ShowDialog();
            His.Admision.frm_ExploradorFormularios frm = new Admision.frm_ExploradorFormularios();
            frm.FiltroHC = Convert.ToInt64(paciente.PAC_HISTORIA_CLINICA);
            frm._ayudaSubsecuentes = true;
            frm.ShowDialog();
        }

        private void btn_Microfilms_Click(object sender, EventArgs e)
        {
            paciente = NegPacientes.recuperarPacientePorAtencion(atencion.ATE_CODIGO);
            frm_AyudaPacientes ayuda = new frm_AyudaPacientes(true, true, true);
            ayuda.HC = paciente.PAC_CODIGO;
            ayuda.ShowDialog();
        }

        private void chbExamenes_Click(object sender, EventArgs e)
        {
            txt_notaEvolucion.Focus();
            chbExamenes.Enabled = false;
            txt_notaEvolucion.Text += "\r\n\r\n" + cmbNotas.Text.TrimEnd() + ": EXAMENES\r\n\r\n";
            txt_notaEvolucion.Select(txt_notaEvolucion.Text.Length, 0);
        }
    }
}