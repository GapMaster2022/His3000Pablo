using System;
using System.Collections.Generic;
using System.ComponentModel;
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


namespace His.Formulario
{
    public partial class frm_EvolucionEnfermeras : Form
    {
        #region Variables
        private int atencionId;             //codigo de la atencion del paciente
        private bool mostrarInfPaciente;    //si se mostrara el panel con la informacion del paciente
        ATENCIONES atencion = null;
        PACIENTES paciente = null;
        HC_EVOLUCION evolucion = null;
        HC_EVOLUCION_DETALLE ultimaNota = null;
        List<HC_PRESCRIPCIONES> listaPrescripciones = new List<HC_PRESCRIPCIONES>();
        List<DtoAtenciones> atenciones = new List<DtoAtenciones>();
        bool formulario = false;
        bool band = true;
        private int posicion;
        private bool cursorDown;
        private Point[] puntos;
        private Pen lapiz;
        private Pen goma;
        private Bitmap bmp;
        private string accion;
        bool multilinea = true;

        #endregion

        #region Contructor

        public frm_EvolucionEnfermeras()
        {
            string mascara = string.Empty;

            for (int i = 0; i < GeneralPAR.TamNumAtencion; i++)
                mascara = mascara + "9";

            atenciones = NegAtenciones.atencionesActivas();

            InitializeComponent();
            //txtNumAtencion.Mask = mascara;
            inicializarForma();            
            deshabilitarCampos();
        }

        public frm_EvolucionEnfermeras(ATENCIONES nAtencion)
        {
            atencion = nAtencion;
            string mascara = string.Empty;

            for (int i = 0; i < GeneralPAR.TamNumAtencion; i++)
                mascara = mascara + "9";
            formulario = true;
            //atenciones = NegAtenciones.atencionesActivas();
            
            InitializeComponent();
            //txtNumAtencion.Mask = mascara;
            inicializarGridPrescripciones();
            inicializarForma();            
            deshabilitarCampos();
            cargarAtencion();
            
        }

        public frm_EvolucionEnfermeras(int codAtencion, bool parMostrarInfPaciente)
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
            inicializarForma();
            Inicializar();
            deshabilitarCampos();
            cargarAtencion();
            //variables para obtener la inf del paciente
            atencionId = codAtencion;
            mostrarInfPaciente = parMostrarInfPaciente;
        }

        #endregion

        #region Cargar Datos

        private void inicializarForma()
        {
            inicializarGridPrescripciones();
            txt_notaEvolucion.Scrollbars = ScrollBars.Both;
            btnNuevo.Image = Recursos.Archivo.imgBtnAdd2;
            btnEditar.Image = Recursos.Archivo.imgBtnRestart;
            btnImprimir.Image = Recursos.Archivo.imgBtnGonePrint48;
            btnGuardar.Image = Recursos.Archivo.imgBtnGoneSave48;
            btnEliminar.Image = Recursos.Archivo.imgDelete;
            btnCancelar.Image = Recursos.Archivo.imgCancelar1;
            ultraPictureBox1.Image = Recursos.Archivo.F1;
            ultraPictureBox1.Visible = false;


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
            lapiz = new Pen(Color.Black, 5);
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
                gridPrescripciones.Rows.Clear();

                if (atencion != null)
                {
                    paciente = NegPacientes.recuperarPacientePorAtencion(atencion.ATE_CODIGO);
                    //txt_pacNombre.Text = paciente.PAC_APELLIDO_PATERNO + " " +
                    //                     paciente.PAC_APELLIDO_MATERNO + " " +
                    //                     paciente.PAC_NOMBRE1 + " " +
                    //                     paciente.PAC_NOMBRE2;
                    //txt_pacHCL.Text = paciente.PAC_HISTORIA_CLINICA;

                    evolucion = NegEvolucion.recuperarEvolucionPorAtencion(atencion.ATE_CODIGO);
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
                gridPrescripciones.Rows.Clear();
                DataTable verificaEvolucionEnfermeria = new DataTable();
                verificaEvolucionEnfermeria = NegEvolucionDetalle.verificaEvolucionEnfermeria(atencion.ATE_CODIGO);
                if (verificaEvolucionEnfermeria.Rows.Count > 0)
                {
                    cargarUltimaNota(Convert.ToInt16(verificaEvolucionEnfermeria.Rows[0]["EVO_CODIGO"].ToString()));
                    cargarNotasEvolucion(Convert.ToInt16(verificaEvolucionEnfermeria.Rows[0]["EVO_CODIGO"].ToString()));
                    habilitarCampos();
                    btnImprimir.Enabled = true;
                }
                if (evolucion == null)
                {
                    band = false;
                    btnEditar.Enabled = false;
                    btnImprimir.Enabled = false;
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
                DataTable recuperaevdCodigo = new DataTable();
                recuperaevdCodigo = NegEvolucionDetalle.RecuperaEvdCodigo(codEvolucion);                
                if (recuperaevdCodigo != null)
                {
                    txt_notaEvolucion.Text = recuperaevdCodigo.Rows[0]["EVD_DESCRIPCION"].ToString();                    
                    txtMedico.Text = recuperaevdCodigo.Rows[0]["NOM_USUARIO"].ToString();
                    USUARIOS user = NegUsuarios.RecuperaUsuario(Convert.ToInt16(recuperaevdCodigo.Rows[0]["ID_USUARIO"].ToString()));
                    dtpFechaNota.Value = Convert.ToDateTime(recuperaevdCodigo.Rows[0]["EVD_FECHA"].ToString());

                    cargarPrescripciones(Convert.ToInt64(recuperaevdCodigo.Rows[0]["EVD_CODIGO"].ToString()));

                    if (Sesion.codUsuario == user.ID_USUARIO)
                    {
                        txt_notaEvolucion.ReadOnly = false;
                        gridPrescripciones.AllowUserToAddRows = true;
                    }
                    else
                    {
                        txt_notaEvolucion.ReadOnly = true;
                        gridPrescripciones.AllowUserToAddRows = false;
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
                    gridPrescripciones.AllowUserToAddRows = true;
                    gridPrescripciones.Rows.Clear();
                }
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //try
            //{
            //    ultimaNota = NegEvolucionDetalle.ultimaNotaEvolucion(codEvolucion);

            //    if (ultimaNota != null)
            //    {
            //        txt_notaEvolucion.Text = ultimaNota.EVD_DESCRIPCION;
            //        USUARIOS user = NegUsuarios.RecuperaUsuario((Int16)ultimaNota.ID_USUARIO);
            //        txtMedico.Text = user.NOMBRES + " " + user.APELLIDOS;
            //        dtpFechaNota.Value = (DateTime)ultimaNota.EVD_FECHA;

            //        cargarPrescripciones(ultimaNota.EVD_CODIGO);

            //        if (Sesion.codUsuario == user.ID_USUARIO)
            //        {
            //            txt_notaEvolucion.ReadOnly = false;
            //            gridPrescripciones.AllowUserToAddRows = true;
            //        }
            //        else
            //        {
            //            txt_notaEvolucion.ReadOnly = true;
            //            gridPrescripciones.AllowUserToAddRows = false;
            //        }
                       

                    
            //    }
            //    else
            //    {
            //        txt_notaEvolucion.Text = string.Empty;
            //        txt_notaEvolucion.ReadOnly = false;
            //        txtMedico.Text = Sesion.nomUsuario;
            //        gridPrescripciones.AllowUserToAddRows = true;
            //        gridPrescripciones.Rows.Clear();
            //    }
            //}
            //catch (Exception r)
            //{
            //    MessageBox.Show(r.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

        }

        public void cargarPrescripciones(Int64 codNota)
        {
            try
            {
                band = true;
                gridPrescripciones.Rows.Clear();
                gridPrescripciones.AllowUserToAddRows = true;

                DataTable prescripciones = new DataTable();
                prescripciones = NegEvolucionDetalle.RecuperaPrescripciones(codNota);
                for (int i = 0; i < prescripciones.Rows.Count; i++)
                {
                    gridPrescripciones.Rows.Add();
                    gridPrescripciones.Rows[i].Cells[3].Value = prescripciones.Rows[i]["PRES_FARMACOTERAPIA_INDICACIONES"].ToString();
                    gridPrescripciones.Rows[i].Cells[4].Value = prescripciones.Rows[i]["PRES_FARMACOS_INSUMOS"].ToString();
                    gridPrescripciones.Rows[i].Cells[5].Value = Convert.ToDateTime(prescripciones.Rows[i]["PRES_FECHA"].ToString());
                    gridPrescripciones.Rows[i].Cells[6].Value = (prescripciones.Rows[i]["PRES_ESTADO"].ToString());
                    
                }
                txt_notaEvolucion.ReadOnly = true;
                band = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public void cargarNotasEvolucion(int codEvolucion)
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
            btnEditar.Enabled = true;
            btnEliminar.Enabled = false;
        }
        private void habilitarCampos2()
        {
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            txt_notaEvolucion.ReadOnly = false;
        }

        private void deshabilitarCampos()
        {
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnNuevo.Enabled = true;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            txt_notaEvolucion.ReadOnly = true;

        }

        private bool validarFormulario()
        {
            bool band = true;
            if (ultimaNota == null)
            {
                if (txt_notaEvolucion.Text.ToString() == string.Empty)
                {
                    AgregarError(txt_notaEvolucion);
                    band = false;
                }
            }

            if (gridPrescripciones.Rows.Count >= 0)
            {
                //if (gridPrescripciones.Rows[0].Cells["PRES_FARMACOTERAPIA_INDICACIONES"].Value != null)
                //{
                //    for (Int16 i = 0; i < gridPrescripciones.Rows.Count - 1; i++)
                //    {
                //        DataGridViewRow fila = gridPrescripciones.Rows[i];

                //        if (fila.Cells["PRES_FARMACOTERAPIA_INDICACIONES"].Value == null)
                //        {
                //            AgregarError(gridPrescripciones);
                //            band = false;
                //        }

                //    }
                //}
                //else
                //{
                //    AgregarError(gridPrescripciones);
                //    band = false;
                //}
            }
            //gridPrescripciones.CurrentRow.Cells[3].Selected = true;

            return band;
            //bool band = true;
            //if (ultimaNota == null)
            //{
            //    if (txt_notaEvolucion.Text.ToString() == string.Empty)
            //    {
            //        AgregarError(txt_notaEvolucion);
            //        band = false;
            //    }
            //}

            //if (gridPrescripciones.Rows.Count > 1)
            //{
            //    for (Int16 i = 0; i < gridPrescripciones.Rows.Count - 1;i++ )
            //    {
            //        DataGridViewRow fila = gridPrescripciones.Rows[i];

            //        if (fila.Cells["PRES_FARMACOTERAPIA_INDICACIONES"].Value == null)
            //        {
            //            AgregarError(gridPrescripciones);
            //            band = false;
            //        }

            //    }
            //}

            //return band;
        }

        private void AgregarError(Control control)
        {
            error.SetError(control, "Campo Requerido");
            
        }

        private void limpiarCampos()
        {
            txt_notaEvolucion.Text = string.Empty;
            txt_notaEvolucion.ReadOnly = false;
            txtMedico.Text = Sesion.nomUsuario;
            dtpFechaNota.Value = DateTime.Now;
            gridPrescripciones.Rows.Clear();
            gridPrescripciones.AllowUserToAddRows = true;
            txt_notaEvolucion.ReadOnly = false;
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
                    detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;

                    evolucion = new HC_EVOLUCION();
                    evolucion.EVO_CODIGO = NegEvolucion.ultimoCodigo() + 1;
                    evolucion.ATENCIONESReference.EntityKey = atencion.EntityKey;
                    evolucion.EVO_FECHA_CREACION = DateTime.Now;
                    evolucion.ID_USUARIO = Entidades.Clases.Sesion.codUsuario;
                    evolucion.NOM_USUARIO = Entidades.Clases.Sesion.nomUsuario;
                    evolucion.PAC_CODIGO = NegPacientes.recuperarPacientePorAtencion(atencion.ATE_CODIGO).PAC_CODIGO;
                    evolucion.EVO_ESTADO = true;
                    NegEvolucion.crearEvolucion(evolucion);
                    detalle.REF_CODIGO = evolucion.EVO_CODIGO;
                    NegAtencionDetalleFormulariosHCU.Crear(detalle);
                }

                if (ultimaNota == null)
                {
                    ultimaNota = new HC_EVOLUCION_DETALLE();
                    ultimaNota.EVD_CODIGO = NegEvolucionDetalle.ultimoCodigo() + 1;
                    ultimaNota.EVD_FECHA = DateTime.Now;
                    ultimaNota.EVD_DESCRIPCION = txt_notaEvolucion.Text;
                    ultimaNota.HC_EVOLUCIONReference.EntityKey = evolucion.EntityKey;
                    ultimaNota.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    ultimaNota.NOM_USUARIO = His.Entidades.Clases.Sesion.nomUsuario;
                    ultimaNota.FECHA_INICIO = DateTime.Now;
                    ultimaNota.FECHA_FIN = DateTime.Now;
                    ultimaNota.MED_TRATANTE = Sesion.codUsuario;
                    NegEvolucionDetalle.crearEvolucionDetalle(ultimaNota);
                }
                else
                {
                    ultimaNota.EVD_DESCRIPCION = txt_notaEvolucion.Text;
                    NegEvolucionDetalle.editarNotaEvolucion(ultimaNota, DateTime.Now, DateTime.Now);
                }
                int codPresc = 0;
                for (Int16 i = 0; i < gridPrescripciones.Rows.Count - 1; i++)
                {
                    DataGridViewRow fila = gridPrescripciones.Rows[i];
                    HC_PRESCRIPCIONES nuevaPrescripcion = new HC_PRESCRIPCIONES();

                    if (fila.Cells["PRES_CODIGO"].Value == null)
                    {
                        nuevaPrescripcion.PRES_CODIGO = NegPrescripciones.ultimoCodigo() + 1;

                        if (fila.Cells["PRES_FARMACOTERAPIA_INDICACIONES"].Value != null)
                            nuevaPrescripcion.PRES_FARMACOTERAPIA_INDICACIONES = fila.Cells["PRES_FARMACOTERAPIA_INDICACIONES"].Value.ToString();


                        nuevaPrescripcion.PRES_FECHA = DateTime.Now;
                        if (fila.Cells["PRES_ESTADO"].Value == null)
                            nuevaPrescripcion.PRES_ESTADO = false;
                        else
                            nuevaPrescripcion.PRES_ESTADO = Convert.ToBoolean(fila.Cells["PRES_ESTADO"].Value);

                        nuevaPrescripcion.HC_EVOLUCION_DETALLEReference.EntityKey = ultimaNota.EntityKey;

                        if (nuevaPrescripcion.PRES_ESTADO == true)
                        {
                            nuevaPrescripcion.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            nuevaPrescripcion.NOM_USUARIO = Entidades.Clases.Sesion.nomUsuario;
                            nuevaPrescripcion.PRES_FARMACOS_INSUMOS = fila.Cells["PRES_FARMACOS_INSUMOS"].Value.ToString();
                            nuevaPrescripcion.PRES_FECHA_ADMINISTRACION = Convert.ToDateTime(fila.Cells["PRES_FECHA"].Value.ToString());
                        }
                        NegPrescripciones.crearPrescripcion(nuevaPrescripcion);
                    }
                    else
                    {
                        codPresc = (Int32)fila.Cells["PRES_CODIGO"].Value;
                        nuevaPrescripcion = NegPrescripciones.recuperarPrescripcionID(codPresc);

                        if (fila.Cells["PRES_ESTADO"].Value == null)
                            nuevaPrescripcion.PRES_ESTADO = false;
                        else
                            nuevaPrescripcion.PRES_ESTADO = Convert.ToBoolean(fila.Cells["PRES_ESTADO"].Value);

                        if (nuevaPrescripcion.PRES_ESTADO == true)
                        {
                            nuevaPrescripcion.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                            nuevaPrescripcion.NOM_USUARIO = Entidades.Clases.Sesion.nomUsuario;
                            nuevaPrescripcion.PRES_FARMACOS_INSUMOS = fila.Cells["PRES_FARMACOS_INSUMOS"].Value.ToString();
                            nuevaPrescripcion.PRES_FECHA_ADMINISTRACION = Convert.ToDateTime(fila.Cells["PRES_FECHA"].Value.ToString());
                        }

                        NegPrescripciones.editarPrescripcion(nuevaPrescripcion);

                    }
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
                MessageBox.Show("Informacion almacenada correctamente", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cargarNotasEvolucion(evolucion.EVO_CODIGO);
                cargarPrescripciones(ultimaNota.EVD_CODIGO);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //try
            //{
            //    if (evolucion == null)
            //    {
            //        FORMULARIOS_HCU formul = NegFormulariosHCU.RecuperarFormularioID(4);
            //        ATENCION_DETALLE_FORMULARIOS_HCU detalle = new ATENCION_DETALLE_FORMULARIOS_HCU();
            //        detalle.ADF_CODIGO = NegAtencionDetalleFormulariosHCU.MaxCodigo() + 1;
            //        detalle.FORMULARIOS_HCUReference.EntityKey = formul.EntityKey;
            //        detalle.ATENCIONESReference.EntityKey = atencion.EntityKey;
            //        detalle.ADF_FECHA_INGRESO = DateTime.Now;
            //        detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;

            //        evolucion = new HC_EVOLUCION();
            //        evolucion.EVO_CODIGO = NegEvolucion.ultimoCodigo() + 1;
            //        evolucion.ATENCIONESReference.EntityKey = atencion.EntityKey;
            //        evolucion.EVO_FECHA_CREACION = DateTime.Now;
            //        evolucion.ID_USUARIO = Entidades.Clases.Sesion.codUsuario;
            //        evolucion.NOM_USUARIO = Entidades.Clases.Sesion.nomUsuario;
            //        evolucion.PAC_CODIGO = NegPacientes.recuperarPacientePorAtencion(atencion.ATE_CODIGO).PAC_CODIGO;
            //        evolucion.EVO_ESTADO = true;
            //        NegEvolucion.crearEvolucion(evolucion);
            //        detalle.REF_CODIGO = evolucion.EVO_CODIGO;
            //        NegAtencionDetalleFormulariosHCU.Crear(detalle);
            //    }

            //    if (ultimaNota == null)
            //    {
            //        ultimaNota = new HC_EVOLUCION_DETALLE();
            //        ultimaNota.EVD_CODIGO = NegEvolucionDetalle.ultimoCodigo() + 1;
            //        ultimaNota.EVD_FECHA = DateTime.Now;
            //        ultimaNota.EVD_DESCRIPCION = txt_notaEvolucion.Text;
            //        ultimaNota.HC_EVOLUCIONReference.EntityKey = evolucion.EntityKey;
            //        ultimaNota.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
            //        ultimaNota.NOM_USUARIO = His.Entidades.Clases.Sesion.nomUsuario;
            //        NegEvolucionDetalle.crearEvolucionDetalle(ultimaNota);
            //    }
            //    else
            //    {
            //        ultimaNota.EVD_DESCRIPCION = txt_notaEvolucion.Text;
            //        NegEvolucionDetalle.editarNotaEvolucion(ultimaNota);
            //    }

            //    for (Int16 i = 0; i < gridPrescripciones.Rows.Count - 1; i++)
            //    {
            //        DataGridViewRow fila = gridPrescripciones.Rows[i];
            //            HC_PRESCRIPCIONES nuevaPrescripcion = new HC_PRESCRIPCIONES();

            //            if (fila.Cells["PRES_CODIGO"].Value == null)
            //            {
            //                nuevaPrescripcion.PRES_CODIGO = NegPrescripciones.ultimoCodigo() + 1;

            //                if (fila.Cells["PRES_FARMACOTERAPIA_INDICACIONES"].Value!=null)
            //                    nuevaPrescripcion.PRES_FARMACOTERAPIA_INDICACIONES = fila.Cells["PRES_FARMACOTERAPIA_INDICACIONES"].Value.ToString();


            //                nuevaPrescripcion.PRES_FECHA = DateTime.Now;
            //                if (fila.Cells["PRES_ESTADO"].Value == null)
            //                    nuevaPrescripcion.PRES_ESTADO = false;
            //                else
            //                    nuevaPrescripcion.PRES_ESTADO = Convert.ToBoolean(fila.Cells["PRES_ESTADO"].Value);
                            
            //                nuevaPrescripcion.HC_EVOLUCION_DETALLEReference.EntityKey = ultimaNota.EntityKey;

            //                if (nuevaPrescripcion.PRES_ESTADO == true)
            //                {
            //                    nuevaPrescripcion.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
            //                    nuevaPrescripcion.NOM_USUARIO = Entidades.Clases.Sesion.nomUsuario;
            //                    nuevaPrescripcion.PRES_FARMACOS_INSUMOS = fila.Cells["PRES_FARMACOS_INSUMOS"].Value.ToString();
            //                    nuevaPrescripcion.PRES_FECHA_ADMINISTRACION = Convert.ToDateTime(fila.Cells["PRES_FECHA"].Value.ToString());
            //                }
            //                NegPrescripciones.crearPrescripcion(nuevaPrescripcion);
            //            }
            //            else
            //            {
            //                int codPresc = (Int32)fila.Cells["PRES_CODIGO"].Value;
            //                nuevaPrescripcion = NegPrescripciones.recuperarPrescripcionID(codPresc);

            //                if (fila.Cells["PRES_ESTADO"].Value == null)
            //                    nuevaPrescripcion.PRES_ESTADO = false;
            //                else
            //                    nuevaPrescripcion.PRES_ESTADO = Convert.ToBoolean(fila.Cells["PRES_ESTADO"].Value);

            //                if (nuevaPrescripcion.PRES_ESTADO == true)
            //                {
            //                    nuevaPrescripcion.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
            //                    nuevaPrescripcion.NOM_USUARIO = Entidades.Clases.Sesion.nomUsuario;
            //                    nuevaPrescripcion.PRES_FARMACOS_INSUMOS = fila.Cells["PRES_FARMACOS_INSUMOS"].Value.ToString();
            //                    nuevaPrescripcion.PRES_FECHA_ADMINISTRACION = Convert.ToDateTime(fila.Cells["PRES_FECHA"].Value.ToString());
            //                }

            //                NegPrescripciones.editarPrescripcion(nuevaPrescripcion);
            //            }
            //        }

            //        MessageBox.Show("Informacion almacenada correctamente","Confirmación",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        cargarNotasEvolucion(evolucion.EVO_CODIGO);
            //        cargarPrescripciones(ultimaNota.EVD_CODIGO);
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //}
        }

        private void GuardaDatosEnfermeria()
        {
            try
            {
                DataTable verificaEvolucionEnfermeria = new DataTable();
                DataTable recuperaevdCodigo = new DataTable();
                verificaEvolucionEnfermeria = NegEvolucionDetalle.verificaEvolucionEnfermeria(atencion.ATE_CODIGO);
                int verifica1 = 1, verifica2 = 0;
                if (verificaEvolucionEnfermeria.Rows.Count == 0)
                {
                    verifica1 = NegEvolucionDetalle.GuardaEvolucionEnfermeria(atencion.ATE_CODIGO, NegPacientes.recuperarPacientePorAtencion(atencion.ATE_CODIGO).PAC_CODIGO, Sesion.codUsuario, Sesion.nomUsuario);
                    verificaEvolucionEnfermeria = NegEvolucionDetalle.verificaEvolucionEnfermeria(atencion.ATE_CODIGO);
                }
                if(verifica1==1)
                {
                    verifica2 = NegEvolucionDetalle.GuardaEvolucionEnfermeriaDetalle(Convert.ToInt16(verificaEvolucionEnfermeria.Rows[0]["EVO_CODIGO"].ToString()), Sesion.codUsuario, Sesion.nomUsuario, txt_notaEvolucion.Text);
                    recuperaevdCodigo = NegEvolucionDetalle.RecuperaEvdCodigo(Convert.ToInt16(verificaEvolucionEnfermeria.Rows[0]["EVO_CODIGO"].ToString()));
                }
                else
                {
                    MessageBox.Show("No Se Guardo La Nota De Enfermeria", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //if (verifica2 == 1)
                //{
                //    for (int i = 0; i < gridPrescripciones.Rows.Count - 1; i++)
                //    {
                //        NegEvolucionDetalle.GrabaEvolucionEnfermeriaPrescripciones(Convert.ToInt16(recuperaevdCodigo.Rows[0]["EVD_CODIGO"].ToString()), gridPrescripciones.Rows[i].Cells[3].Value.ToString(), gridPrescripciones.Rows[i].Cells[4].Value.ToString(), Convert.ToBoolean(gridPrescripciones.Rows[i].Cells[6].Value), gridPrescripciones.Rows[i].Cells[5].Value.ToString());
                //    }
                //}
                MessageBox.Show("Información Guardada Con Éxito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Se Guardo La Nota De Enfermeria", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void inicializarGridPrescripciones()
        {
            gridPrescripciones.EditMode = DataGridViewEditMode.EditOnKeystroke;
            PRES_FARMACOTERAPIA_INDICACIONES.SortMode = DataGridViewColumnSortMode.NotSortable;
            PRES_FARMACOS_INSUMOS.SortMode = DataGridViewColumnSortMode.NotSortable;
            PRES_FECHA.SortMode = DataGridViewColumnSortMode.NotSortable;
            PRES_ESTADO.SortMode = DataGridViewColumnSortMode.NotSortable;
            PRES_CODIGO.Visible = false;
            ID_USUARIO.Visible = false;
            NOM_USUARIO.Visible = false;
            PRES_ESTADO.Width = 20;
            PRES_FECHA.Width = 130;
            PRES_FARMACOTERAPIA_INDICACIONES.Width = 350;
            PRES_FARMACOS_INSUMOS.Width = 200;

        }

        private void txtNumAtencion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 && formulario==false)
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
            
            gridNotasEvolucion.DisplayLayout.Bands[0].Columns["EVD_FECHA"].Header.Caption = "FECHA";
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

                reporte.FOR_ESTABLECIMIENTO = "";
                reporte.FOR_NOMBRE = paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                reporte.FOR_APELLIDO = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO;
                reporte.FOR_SEXO = paciente.PAC_GENERO;
                if (!NegParametros.ParametroFormularios())
                    reporte.FOR_HISTORIA = paciente.PAC_HISTORIA_CLINICA;
                else
                    reporte.FOR_HISTORIA = paciente.PAC_IDENTIFICACION;
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
                    for(int j = 0 ; j< lon; j++)
                    {
                        if(cadena.Length > 2)
                        {
                            datosDetalle.DETE_NORAS_EVOLUCION = validarTexto(gridNotasEvolucion.Rows[i].Cells[4].Value.ToString(), posicion);
                            cadena = gridNotasEvolucion.Rows[i].Cells[4].Value.ToString().Substring(posicion);
                            listaDetalle.Add(datosDetalle);
                        }else
                        {
                            j = lon;
                            datosDetalle.DETE_NORAS_EVOLUCION = validarTexto(gridNotasEvolucion.Rows[i].Cells[4].Value.ToString(), posicion);
                            listaDetalle.Add(datosDetalle);
                        }
                        datosDetalle = new ReporteEvolucionDetalle();
                    }
                    listaPrescripciones = NegPrescripciones.listaPrescripciones(Convert.ToInt32(gridNotasEvolucion.Rows[i].Cells[0].Value.ToString()));
                    if (listaDetalle.Count > listaPrescripciones.Count)
                    {
                        reporteDetalle = new ReporteEvolucionDetalle();
                        HC_PRESCRIPCIONES prescripciones = new HC_PRESCRIPCIONES();
                        
                        for(int k = 0 ; k< listaPrescripciones.Count;k++)
                        {
                            prescripciones = listaPrescripciones.ElementAt(k);
                            reporteDetalle = listaDetalle.ElementAt(k);
                            reporteDetalle.DETE_FARMACOS = prescripciones.PRES_FARMACOTERAPIA_INDICACIONES;
                            reporteDetalle.DETE_ADMIN_FARM = prescripciones.PRES_FARMACOS_INSUMOS;
                        }
                    }else
                    {
                        reporteDetalle = new ReporteEvolucionDetalle();
                        HC_PRESCRIPCIONES prescripciones = new HC_PRESCRIPCIONES();
                        int indice = 0;
                        for (int k = 0; k < listaDetalle.Count; k++)
                        {
                            prescripciones = listaPrescripciones.ElementAt(k);
                            reporteDetalle = listaDetalle.ElementAt(k);
                            reporteDetalle.DETE_FARMACOS = prescripciones.PRES_FARMACOTERAPIA_INDICACIONES;
                            reporteDetalle.DETE_ADMIN_FARM = prescripciones.PRES_FARMACOS_INSUMOS;
                            indice = k;
                        }
                        for (int j = indice+1; j < listaPrescripciones.Count; j++)
                        {
                            reporteDetalle = new ReporteEvolucionDetalle();
                            prescripciones = new HC_PRESCRIPCIONES();
                            prescripciones = listaPrescripciones.ElementAt(j);
                            reporteDetalle.DETE_FECHA = "";
                            reporteDetalle.DETE_HORA = "";
                            reporteDetalle.DETE_NORAS_EVOLUCION = "";
                            reporteDetalle.DETE_PROFESIONAL = "";
                            reporteDetalle.DETE_FARMACOS = prescripciones.PRES_FARMACOTERAPIA_INDICACIONES;
                            reporteDetalle.DETE_ADMIN_FARM = prescripciones.PRES_FARMACOS_INSUMOS;
                            listaDetalle.Add(reporteDetalle);
                        }
                    }
                    datosDetalle = new ReporteEvolucionDetalle();
                    datosDetalle.DETE_FARMACOS = "Dr./a." + Convert.ToString(gridNotasEvolucion.Rows[i].Cells[2].Value.ToString());
                    listaDetalle.Add(datosDetalle);
                    datosDetalle = new ReporteEvolucionDetalle();
                    listaDetalle.Add(datosDetalle);
                    for (int n = 0; n < listaDetalle.Count; n++ )
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
                    if(fila== 42)
                    {
                        fila = 0;
                    }
                }
                if(fila<42)
                {
                    for(int j = fila; j< 42 ;j++)
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
                if ((posicion < campo.Length-2) && (cont <= 55))
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
                if (((texto.Substring(i-1, 1)) == " "))
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

        private void txt_notaEvolucionEnfermeras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void frm_EvolucionEnfermeras_KeyUp(object sender, KeyEventArgs e)
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

        private void frm_EvolucionEnfermeras_SizeChanged(object sender, EventArgs e)
        {
            
        }
        
        private void frm_EvolucionEnfermeras_Load(object sender, System.EventArgs e)
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
            ultraSpellCheckerEvolucion.Dictionary = Application.StartupPath + "\\Recursos\\es-spanish-v2-whole.dict";

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
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void gridPrescripciones_CellContentClick(object sender, System.DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            try
            {
                if (band == false)
                {
                    if (e.ColumnIndex == gridPrescripciones.Columns["PRES_ESTADO"].Index)
                    {
                        DataGridViewCheckBoxCell chkCell = (DataGridViewCheckBoxCell)gridPrescripciones.Rows[e.RowIndex].Cells["PRES_ESTADO"];
                        if (chkCell.Value != null)
                        {
                            if ((bool)chkCell.Value == true)
                            {
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FARMACOS_INSUMOS"].ReadOnly = false;
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].ReadOnly = false;
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].Value = DateTime.Now;
                            }
                            else
                            {
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FARMACOS_INSUMOS"].ReadOnly = false;
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FARMACOS_INSUMOS"].Value = string.Empty;
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].ReadOnly = true;
                                gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].Value = null;
                            }
                        }
                        else
                        {
                            gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FARMACOS_INSUMOS"].ReadOnly = false;
                            gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FARMACOS_INSUMOS"].Value = string.Empty;
                            gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].ReadOnly = true;
                            gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].Value = null;
                        }

                    }
                    else
                    {
                        gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FARMACOS_INSUMOS"].ReadOnly = false;
                        gridPrescripciones.Rows[e.RowIndex].Cells["PRES_FECHA"].ReadOnly = false;
                    }
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridViewRow fila = gridPrescripciones.Rows[e.RowIndex];
            if (fila.Cells["PRES_CODIGO"].Value == null)
            {
                //fila.Cells["NOM_USUARIO"].Value = Sesion.nomUsuario;
                //fila.Cells["NOM_USUARIO"].ReadOnly = true;

                //fila.Cells["PRES_FECHA"].Value = DateTime.Now;
                fila.Cells["PRES_FECHA"].ReadOnly = true;
                fila.Cells["PRES_FARMACOS_INSUMOS"].ReadOnly = true;
            }
            else
            {
                fila.Cells["PRES_FECHA"].ReadOnly = false;
            }
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
        
        private void frm_EvolucionEnfermeras_KeyDown(object sender, KeyEventArgs e)
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
            ultimaNota = null;
            habilitarCampos2();
            limpiarCampos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            habilitarCampos2();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validarFormulario())
            {
                //GuardarDatos();
                GuardaDatosEnfermeria();
                deshabilitarCampos();
                // ImprimirReporte("pdf");
            }
            else
            {
                MessageBox.Show("Informacion Incompleta", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cargarAtencion();
            deshabilitarCampos();
            if (evolucion == null)
            {
                btnEditar.Enabled = false;
                btnImprimir.Enabled = false;
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ImprimirReporte("reporte");
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

        private void gridPrescripciones_Paint(object sender, PaintEventArgs e)
        {
            if (multilinea == true)
            {
                multilinea = false;
                this.gridPrescripciones.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
            }
        }

        private void gridPrescripciones_Scroll(object sender, ScrollEventArgs e)
        {
            this.gridPrescripciones.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);

        }

        private void gridPrescripciones_RowEnter(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            this.gridPrescripciones.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
            multilinea = true;
        }
    }
}