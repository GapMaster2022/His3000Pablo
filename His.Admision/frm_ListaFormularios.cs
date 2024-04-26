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
using His.General;
using His.Parametros;
using His.Formulario;

namespace His.Admision
{
    public partial class frm_ListaFormularios : Form
    {
        
        List<FORMULARIOS_HCU> formularios = new List<FORMULARIOS_HCU>();
        List<ATENCION_DETALLE_FORMULARIOS_HCU> formulariosAtencion = new List<ATENCION_DETALLE_FORMULARIOS_HCU>();
        ATENCIONES atencion = null;
        PACIENTES paciente = null;
        PACIENTES_DATOS_ADICIONALES datos = null;
        CIUDAD ciudad = null;
        PAIS pais = null;
        ESTADO_CIVIL estadocivil = null;
        ETNIA etnia = null;
        public frm_ListaFormularios()
        {
            formularios = NegFormulariosHCU.RecuperarFormulariosHCU();
            InitializeComponent();
            CargarDatos();
        }

        public frm_ListaFormularios(ATENCIONES a, PACIENTES p, PACIENTES_DATOS_ADICIONALES d)
        {
            formularios = NegFormulariosHCU.RecuperarFormulariosHCU();
            atencion = a;
            paciente = p;
            datos = d;
       
            estadocivil = NegEstadoCivil.RecuperaEstadoCivil().FirstOrDefault(e => e.EntityKey == d.ESTADO_CIVILReference.EntityKey);
            etnia = NegEtnias.ListaEtnias().FirstOrDefault(e => e.EntityKey == p.ETNIAReference.EntityKey);
            InitializeComponent();
            
        }

        private void CargarDatos()

        {
            try
            {
                grid.DataSource = formularios.Select(f => new { CODIGO = f.FH_CODIGO, NOMBRE = f.FH_NOMBRE}).ToList();
                grid.Columns["NOMBRE"].Width = 350;
                grid.Columns["CODIGO"].HeaderText = string.Empty;
                grid.Columns["CODIGO"].Width = 50;


                if (atencion==null)
                {
                    btnCrear.Enabled = false;
                }
                else
                {
                    CargarFormulariosAtencion(atencion.ATE_CODIGO);
                    //if (atencion.ATE_DIRECTORIO == null || atencion.ATE_DIRECTORIO == string.Empty)
                    //{
                    //    btnCrear.Enabled = false;
                    //}
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                if (e.InnerException != null)
                    MessageBox.Show(e.InnerException.Message);
            }
                
        }

        private void CargarFormulariosAtencion(int codAtencion)
        {
            formulariosAtencion = NegAtencionDetalleFormulariosHCU.listaFormulariosAtencion(codAtencion);
            foreach (ATENCION_DETALLE_FORMULARIOS_HCU acceso in formulariosAtencion)
            {
                foreach (DataGridViewRow dr in grid.Rows)
                {
                    int codigoForm = Convert.ToInt16(dr.Cells["CODIGO"].Value);
                    FORMULARIOS_HCU formul = formularios.FirstOrDefault(f => f.FH_CODIGO == codigoForm);
                    if (acceso.FORMULARIOS_HCUReference.EntityKey == formul.EntityKey)
                        dr.Cells[0].Value = true;
                }
            }
        }

        private void chk_select_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_select.Checked == true)
            {
                for (int i = 0; i < grid.Rows.Count; i++)
                    grid.Rows[i].Cells["estado"].Value = true;
            }
        }

        private void chkNinguno_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNinguno.Checked == true)
            {
                for (int i = 0; i < grid.Rows.Count; i++)
                    grid.Rows[i].Cells["estado"].Value = false;
            }
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (formulariosAtencion == null)
                formulariosAtencion = new List<ATENCION_DETALLE_FORMULARIOS_HCU>();

            foreach (DataGridViewRow dr in grid.Rows)
            {
                if (dr.Cells[0].Value != null)
                    if (dr.Cells[0].Value.ToString() == "True")
                    {
                        int codigoForm = Convert.ToInt16(dr.Cells["CODIGO"].Value);
                        FORMULARIOS_HCU formul = formularios.FirstOrDefault(f => f.FH_CODIGO == codigoForm);
                        ATENCION_DETALLE_FORMULARIOS_HCU detalle = formulariosAtencion.FirstOrDefault(d => d.FORMULARIOS_HCUReference.EntityKey == formul.EntityKey);

                        if (detalle == null)
                        {
                            //string directorio = Application.LocalUserAppDataPath + "\\" + Parametros.AdmisionParametros.getDirectorioPacientesFormularios();
                            //if (!System.IO.Directory.Exists(directorio))
                            //{
                            //    System.IO.Directory.CreateDirectory(directorio);
                            //}

                            detalle = new ATENCION_DETALLE_FORMULARIOS_HCU();
                            detalle.ADF_CODIGO = NegAtencionDetalleFormulariosHCU.MaxCodigo() + 1;
                            detalle.FORMULARIOS_HCUReference.EntityKey = formul.EntityKey;
                            detalle.ATENCIONESReference.EntityKey = atencion.EntityKey;
                            detalle.ADF_FECHA_INGRESO = DateTime.Now;
                            detalle.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;

                            //FtpClient ftp = new FtpClient();
                            //ftp.Login();
                            //ftp.ChangeDir(AdmisionParametros.getDirectorioMatrizFormularios());
                            
                            ////bajo el formulario como un archivo temporal
                            //ftp.Download(formul.FH_DIRECTORIO, directorio + "\\" + formul.FH_DIRECTORIO);

                            //abro el .xls y escribo la informacion del paciente

                            //FormularioFTP form = new FormularioFTP();

                            switch (codigoForm)
                            {
                                case 4:

                                    #region Form. 005 EVOLUCION Y PRESCRIPCIONES
                                    HC_EVOLUCION evolucion = new HC_EVOLUCION();
                                    evolucion.EVO_CODIGO = NegEvolucion.ultimoCodigo() + 1;
                                    evolucion.ATENCIONESReference.EntityKey = atencion.EntityKey;
                                    evolucion.EVO_FECHA_CREACION = DateTime.Now;
                                    evolucion.ID_USUARIO = Entidades.Clases.Sesion.codUsuario;
                                    evolucion.NOM_USUARIO = Entidades.Clases.Sesion.nomUsuario;
                                    evolucion.PAC_CODIGO = paciente.PAC_CODIGO;
                                    evolucion.EVO_ESTADO = true;
                                    NegEvolucion.crearEvolucion(evolucion);
                                    detalle.REF_CODIGO = evolucion.EVO_CODIGO;

                                    #endregion
                                    break;

                                #region ADMISION FORMULARIO
                                /*case 1:
                                    #region Form. 001 ADMISION Y ALTA EGRESO
                                    form.setFormularioAdmision(
                                    directorio + "\\" + formul.FH_DIRECTORIO,
                                    paciente.PAC_HISTORIA_CLINICA,
                                    paciente.PAC_APELLIDO_PATERNO,
                                    paciente.PAC_APELLIDO_MATERNO,
                                    paciente.PAC_NOMBRE1,
                                    paciente.PAC_NOMBRE2,
                                    paciente.PAC_IDENTIFICACION,
                                    datos.DAP_DIRECCION_DOMICILIO,
                                    datos.COD_SECTOR,
                                    datos.COD_PARROQUIA,
                                    datos.COD_CANTON,
                                    datos.COD_PROVINCIA,
                                    datos.DAP_TELEFONO1,
                                    paciente.PAC_FECHA_NACIMIENTO.ToString(),
                                    pais.NOMPAIS,
                                    paciente.PAC_NACIONALIDAD,
                                    etnia.E_NOMBRE,
                                    atencion.ATE_EDAD_PACIENTE.ToString(),
                                    paciente.PAC_GENERO,
                                    estadocivil.ESC_CODIGO.ToString(),
                                    datos.DAP_INSTRUCCION,
                                    atencion.ATE_FECHA_INGRESO.ToString(),
                                    datos.DAP_OCUPACION,
                                    datos.DAP_EMP_NOMBRE);

                                    #endregion
                                    break;*/
                                #endregion

                                #region CONSULTA EXTERNA FORMULARIO
                                /*case 2:
                                    #region Form. 002 CONSULTA EXTERNA
                                    form.setFormularioConsultaExterna(
                                        directorio + "\\" + formul.FH_DIRECTORIO,
                                        paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2,
                                        paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO,
                                        paciente.PAC_GENERO,
                                        atencion.ATE_EDAD_PACIENTE.ToString(),
                                        paciente.PAC_HISTORIA_CLINICA);

                                    #endregion
                                    break;*/
                                #endregion

                                #region ANAMNESIS FORMULARIO
                                
                               /* case 3:
                                    #region Form. 003 ANAMNESIS EXAMEN FISICO
                                    form.setFormularioAnamnesis(
                                    directorio + "\\" + formul.FH_DIRECTORIO,
                                    paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2,
                                    paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO,
                                    paciente.PAC_GENERO,
                                    paciente.PAC_HISTORIA_CLINICA);

                                    #endregion
                                    break;*/
                                #endregion
                                 
                                #region FORMULARIOS
                                /*

                                case 4:
                                    #region Form. 005 EVOLUCION Y PRESCRIPCIONES
                                    EVOLUCION evolucion = new EVOLUCION();
                                    evolucion.EVO_CODIGO = NegEvolucion.
                                    form.setFormularioEvolucion(
                                    directorio + "\\" + formul.FH_DIRECTORIO,
                                    paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2,
                                    paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO,
                                    paciente.PAC_GENERO,
                                    paciente.PAC_HISTORIA_CLINICA);

                                    #endregion
                                    break;

                                case 5:
                                    #region Form. 006 EPICRISIS
                                    form.setFormularioAnamnesis(
                                        directorio + "\\" + formul.FH_DIRECTORIO,
                                        paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2,
                                        paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO,
                                        paciente.PAC_GENERO,
                                        paciente.PAC_HISTORIA_CLINICA);

                                    #endregion
                                    break;

                                case 6:
                                    #region Form. 007 INTERCONSULTA
                                    form.setFormularioInterconsulta(
                                        directorio + "\\" + formul.FH_DIRECTORIO,
                                        paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2,
                                        paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO,
                                        paciente.PAC_GENERO,
                                        atencion.ATE_EDAD_PACIENTE.ToString(),
                                        paciente.PAC_HISTORIA_CLINICA);

                                    #endregion
                                    break;

                                case 7:
                                    #region Form. 008 EMERGENCIA
                                    form.setFormularioEmergencia(
                                        directorio + "\\" + formul.FH_DIRECTORIO,
                                        paciente.PAC_HISTORIA_CLINICA,
                                        paciente.PAC_APELLIDO_PATERNO,
                                        paciente.PAC_APELLIDO_MATERNO,
                                        paciente.PAC_NOMBRE1,
                                        paciente.PAC_NOMBRE2,
                                        paciente.PAC_IDENTIFICACION,
                                        datos.DAP_DIRECCION_DOMICILIO,
                                        datos.COD_SECTOR,
                                        datos.COD_PARROQUIA,
                                        datos.COD_CANTON,
                                        datos.COD_PROVINCIA,
                                        datos.DAP_TELEFONO1,
                                        paciente.PAC_FECHA_NACIMIENTO.ToString(),
                                        pais.NOMPAIS,
                                        paciente.PAC_NACIONALIDAD,
                                        etnia.E_NOMBRE,
                                        atencion.ATE_EDAD_PACIENTE.ToString(),
                                        paciente.PAC_GENERO,
                                        estadocivil.ESC_CODIGO.ToString(),
                                        datos.DAP_INSTRUCCION,
                                        atencion.ATE_FECHA_INGRESO.ToString(),
                                        datos.DAP_OCUPACION,
                                        datos.DAP_EMP_NOMBRE);

                                    #endregion
                                    break;

                                case 8:
                                    #region Form. 010 LABORATORIO CLINICO
                                    form.setFormularioLaboratorio(
                                        directorio + "\\" + formul.FH_DIRECTORIO,
                                        paciente.PAC_HISTORIA_CLINICA,
                                        paciente.PAC_APELLIDO_PATERNO,
                                        paciente.PAC_APELLIDO_MATERNO,
                                        paciente.PAC_NOMBRE1,
                                        paciente.PAC_NOMBRE2,
                                        paciente.PAC_IDENTIFICACION,
                                        atencion.ATE_EDAD_PACIENTE.ToString()
                                       );

                                    #endregion
                                    break;

                                case 9:
                                    #region Form. 012 IMAGENOLOGIA
                                    form.setFormularioImagenologia(
                                        directorio + "\\" + formul.FH_DIRECTORIO,
                                        paciente.PAC_HISTORIA_CLINICA,
                                        paciente.PAC_APELLIDO_PATERNO,
                                        paciente.PAC_APELLIDO_MATERNO,
                                        paciente.PAC_NOMBRE1,
                                        paciente.PAC_NOMBRE2,
                                        paciente.PAC_IDENTIFICACION,
                                        atencion.ATE_EDAD_PACIENTE.ToString()
                                       );

                                    #endregion
                                    break;

                                case 10:
                                    #region Form. 013 HISPATOLOGIA
                                    form.setFormularioHispatologia(
                                        directorio + "\\" + formul.FH_DIRECTORIO,
                                        paciente.PAC_HISTORIA_CLINICA,
                                        paciente.PAC_APELLIDO_PATERNO,
                                        paciente.PAC_APELLIDO_MATERNO,
                                        paciente.PAC_NOMBRE1,
                                        paciente.PAC_NOMBRE2,
                                        paciente.PAC_IDENTIFICACION,
                                        atencion.ATE_EDAD_PACIENTE.ToString()
                                       );

                                    #endregion
                                    break;

                                case 11:
                                    #region Form. 020 SIGNOS VITALES
                                    form.setFormularioSignosVitales(
                                        directorio + "\\" + formul.FH_DIRECTORIO,
                                        paciente.PAC_HISTORIA_CLINICA,
                                        paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2,
                                        paciente.PAC_APELLIDO_PATERNO+ " " +paciente.PAC_APELLIDO_MATERNO,
                                        paciente.PAC_GENERO
                                       );

                                    #endregion
                                    break;*/
                                #endregion
                            }

                            //subo el archivo al directorio de la atencion en el servidor
                            //ftp.ChangeDir(atencion.ATE_DIRECTORIO);
                            //ftp.Upload(directorio + "\\" + formul.FH_DIRECTORIO);
                            
                            //detalle.ADF_DIRECTORIO = ftp.RemotePath + formul.FH_DIRECTORIO;
                            NegAtencionDetalleFormulariosHCU.Crear(detalle);
                            CargarFormulariosAtencion(atencion.ATE_CODIGO);
                        }

                    }

            }
            MessageBox.Show("Formularios almacenados correctamente");
        }

        private void frm_ListaFormularios_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.grid.Columns[e.ColumnIndex].Name == "imprimir" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.grid.Rows[e.RowIndex].Cells["imprimir"] as DataGridViewButtonCell;
                Icon icoBorrar = new Icon(Recursos.Archivo.application_32x32 , 32, 32);
                
                e.Graphics.DrawIcon(icoBorrar, e.CellBounds.Left + 3, e.CellBounds.Top + 3);
                this.grid.Rows[e.RowIndex].Height = icoBorrar.Height + 10;
                this.grid.Columns[e.ColumnIndex].Width = icoBorrar.Width + 10;
                e.Handled = true;
            }
        }

        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            #region Imprimir Formularios
            try
            {
                if (this.grid.Columns[e.ColumnIndex].Name == "imprimir")
                {
                    int codigoForm = Convert.ToInt16(grid.CurrentRow.Cells["CODIGO"].Value);
                    FORMULARIOS_HCU formul = formularios.FirstOrDefault(f => f.FH_CODIGO == codigoForm);
                    ATENCION_DETALLE_FORMULARIOS_HCU detalle = formulariosAtencion.FirstOrDefault(d => d.FORMULARIOS_HCUReference.EntityKey == formul.EntityKey);

                    if (detalle!=null)
                    {
                        #region codigo comentado formularios .xls
                        //string directorio = Application.LocalUserAppDataPath + "\\" + Parametros.AdmisionParametros.getDirectorioPacientesFormularios();
                        //if (!System.IO.Directory.Exists(directorio))
                        //{
                        //    System.IO.Directory.CreateDirectory(directorio);
                        //}

                        
                        //FtpClient ftp = new FtpClient();
                        //ftp.Login();

                        //bajo el formulario como un archivo temporal: temp.xls
                        //ftp.Download(detalle.ADF_DIRECTORIO, directorio + "\\" + formul.FH_DIRECTORIO);

                        //DialogResult respuesta = new DialogResult();

                        //abro el .xls y escribo la informacion del paciente

                        //MyExcel excel = new MyExcel();

                        //if (excel.Open(directorio + "\\" + formul.FH_DIRECTORIO, 1))
                        //{
                        //    excel.Show();
                        //}

                        //System.Diagnostics.Process.Start(directorio + "\\" + formul.FH_DIRECTORIO);

                        //respuesta = MessageBox.Show("Desea guardar los cambios en el archivo al servidor?", "Subir Archivo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                        //if (respuesta == DialogResult.OK)
                        //{
                        //    //string direct = null;
                        //    //string[] directorioArchivo = detalle.ADF_DIRECTORIO.Trim().Split('/');
                        //    //for (int i = 0; i < directorioArchivo.Count() - 1; i++)
                        //    //{
                        //    //    if (directorioArchivo[i].ToString() != "")
                        //    //    {
                        //    //        if (direct != null)
                        //    //            direct= direct + "\\" + directorioArchivo[i].ToString();
                        //    //        else
                        //    //            direct = directorioArchivo[i].ToString();
                        //    //    }
                        //    //}
                        //    ftp.ChangeDir(atencion.ATE_DIRECTORIO);
                        //    ftp.Upload(directorio + "\\" + formul.FH_DIRECTORIO);
                        //}
                        #endregion

                        switch (codigoForm)
                        {
                            case 4:

                                #region Form. 005 EVOLUCION Y PRESCRIPCIONES
                                frm_Evolucion frm = new frm_Evolucion(atencion.ATE_CODIGO,true);
                                frm.Show();

                                #endregion
                                break;

                            #region ADMISION FORMULARIO
                            /*case 1:
                                    #region Form. 001 ADMISION Y ALTA EGRESO
                                    form.setFormularioAdmision(
                                    directorio + "\\" + formul.FH_DIRECTORIO,
                                    paciente.PAC_HISTORIA_CLINICA,
                                    paciente.PAC_APELLIDO_PATERNO,
                                    paciente.PAC_APELLIDO_MATERNO,
                                    paciente.PAC_NOMBRE1,
                                    paciente.PAC_NOMBRE2,
                                    paciente.PAC_IDENTIFICACION,
                                    datos.DAP_DIRECCION_DOMICILIO,
                                    datos.COD_SECTOR,
                                    datos.COD_PARROQUIA,
                                    datos.COD_CANTON,
                                    datos.COD_PROVINCIA,
                                    datos.DAP_TELEFONO1,
                                    paciente.PAC_FECHA_NACIMIENTO.ToString(),
                                    pais.NOMPAIS,
                                    paciente.PAC_NACIONALIDAD,
                                    etnia.E_NOMBRE,
                                    atencion.ATE_EDAD_PACIENTE.ToString(),
                                    paciente.PAC_GENERO,
                                    estadocivil.ESC_CODIGO.ToString(),
                                    datos.DAP_INSTRUCCION,
                                    atencion.ATE_FECHA_INGRESO.ToString(),
                                    datos.DAP_OCUPACION,
                                    datos.DAP_EMP_NOMBRE);

                                    #endregion
                                    break;*/
                            #endregion

                            #region CONSULTA EXTERNA FORMULARIO
                            /*case 2:
                                    #region Form. 002 CONSULTA EXTERNA
                                    form.setFormularioConsultaExterna(
                                        directorio + "\\" + formul.FH_DIRECTORIO,
                                        paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2,
                                        paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO,
                                        paciente.PAC_GENERO,
                                        atencion.ATE_EDAD_PACIENTE.ToString(),
                                        paciente.PAC_HISTORIA_CLINICA);

                                    #endregion
                                    break;*/
                            #endregion

                            #region ANAMNESIS FORMULARIO

                             case 3:
                                   

                                frm_Anemnesis frmA = new frm_Anemnesis(atencion.ATE_CODIGO,true);
                                    frmA.Show();

                                
                                    break;
                            #endregion

                            #region EPICRISIS
                            case 5:
                                    frm_Epicrisis frmE = new frm_Epicrisis(atencion.ATE_CODIGO);
                                    frmE.Show();
                                    break;
                            #endregion

                            #region INTERCONSULTA
                            case 6:
                                    frm_Interconsulta frmI = new frm_Interconsulta(atencion.ATE_CODIGO);
                                    frmI.Show();
                                    break;
                            #endregion

                            #region PROTOCOLO
                            case 21:
                                    frm_Protocolo frmP = new frm_Protocolo(atencion.ATE_CODIGO, detalle.ADF_CODIGO); // Aumento el codigo del protocolo / Giovanny Tapia /24/09/2012
                                    frmP.Show();
                                    break;
                            #endregion

                            #region FORMULARIOS
                            /*

                                case 4:
                                    #region Form. 005 EVOLUCION Y PRESCRIPCIONES
                                    EVOLUCION evolucion = new EVOLUCION();
                                    evolucion.EVO_CODIGO = NegEvolucion.
                                    form.setFormularioEvolucion(
                                    directorio + "\\" + formul.FH_DIRECTORIO,
                                    paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2,
                                    paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO,
                                    paciente.PAC_GENERO,
                                    paciente.PAC_HISTORIA_CLINICA);

                                    #endregion
                                    break;

                                case 5:
                                    #region Form. 006 EPICRISIS
                                    form.setFormularioAnamnesis(
                                        directorio + "\\" + formul.FH_DIRECTORIO,
                                        paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2,
                                        paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO,
                                        paciente.PAC_GENERO,
                                        paciente.PAC_HISTORIA_CLINICA);

                                    #endregion
                                    break;

                                case 6:
                                    #region Form. 007 INTERCONSULTA
                                    form.setFormularioInterconsulta(
                                        directorio + "\\" + formul.FH_DIRECTORIO,
                                        paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2,
                                        paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO,
                                        paciente.PAC_GENERO,
                                        atencion.ATE_EDAD_PACIENTE.ToString(),
                                        paciente.PAC_HISTORIA_CLINICA);

                                    #endregion
                                    break;

                                case 7:
                                    #region Form. 008 EMERGENCIA
                                    form.setFormularioEmergencia(
                                        directorio + "\\" + formul.FH_DIRECTORIO,
                                        paciente.PAC_HISTORIA_CLINICA,
                                        paciente.PAC_APELLIDO_PATERNO,
                                        paciente.PAC_APELLIDO_MATERNO,
                                        paciente.PAC_NOMBRE1,
                                        paciente.PAC_NOMBRE2,
                                        paciente.PAC_IDENTIFICACION,
                                        datos.DAP_DIRECCION_DOMICILIO,
                                        datos.COD_SECTOR,
                                        datos.COD_PARROQUIA,
                                        datos.COD_CANTON,
                                        datos.COD_PROVINCIA,
                                        datos.DAP_TELEFONO1,
                                        paciente.PAC_FECHA_NACIMIENTO.ToString(),
                                        pais.NOMPAIS,
                                        paciente.PAC_NACIONALIDAD,
                                        etnia.E_NOMBRE,
                                        atencion.ATE_EDAD_PACIENTE.ToString(),
                                        paciente.PAC_GENERO,
                                        estadocivil.ESC_CODIGO.ToString(),
                                        datos.DAP_INSTRUCCION,
                                        atencion.ATE_FECHA_INGRESO.ToString(),
                                        datos.DAP_OCUPACION,
                                        datos.DAP_EMP_NOMBRE);

                                    #endregion
                                    break;

                                case 8:
                                    #region Form. 010 LABORATORIO CLINICO
                                    form.setFormularioLaboratorio(
                                        directorio + "\\" + formul.FH_DIRECTORIO,
                                        paciente.PAC_HISTORIA_CLINICA,
                                        paciente.PAC_APELLIDO_PATERNO,
                                        paciente.PAC_APELLIDO_MATERNO,
                                        paciente.PAC_NOMBRE1,
                                        paciente.PAC_NOMBRE2,
                                        paciente.PAC_IDENTIFICACION,
                                        atencion.ATE_EDAD_PACIENTE.ToString()
                                       );

                                    #endregion
                                    break;

                                case 9:
                                    #region Form. 012 IMAGENOLOGIA
                                    form.setFormularioImagenologia(
                                        directorio + "\\" + formul.FH_DIRECTORIO,
                                        paciente.PAC_HISTORIA_CLINICA,
                                        paciente.PAC_APELLIDO_PATERNO,
                                        paciente.PAC_APELLIDO_MATERNO,
                                        paciente.PAC_NOMBRE1,
                                        paciente.PAC_NOMBRE2,
                                        paciente.PAC_IDENTIFICACION,
                                        atencion.ATE_EDAD_PACIENTE.ToString()
                                       );

                                    #endregion
                                    break;

                                case 10:
                                    #region Form. 013 HISPATOLOGIA
                                    form.setFormularioHispatologia(
                                        directorio + "\\" + formul.FH_DIRECTORIO,
                                        paciente.PAC_HISTORIA_CLINICA,
                                        paciente.PAC_APELLIDO_PATERNO,
                                        paciente.PAC_APELLIDO_MATERNO,
                                        paciente.PAC_NOMBRE1,
                                        paciente.PAC_NOMBRE2,
                                        paciente.PAC_IDENTIFICACION,
                                        atencion.ATE_EDAD_PACIENTE.ToString()
                                       );

                                    #endregion
                                    break;

                                case 11:
                                    #region Form. 020 SIGNOS VITALES
                                    form.setFormularioSignosVitales(
                                        directorio + "\\" + formul.FH_DIRECTORIO,
                                        paciente.PAC_HISTORIA_CLINICA,
                                        paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2,
                                        paciente.PAC_APELLIDO_PATERNO+ " " +paciente.PAC_APELLIDO_MATERNO,
                                        paciente.PAC_GENERO
                                       );

                                    #endregion
                                    break;*/
                            #endregion
                        }
                        

                    }
                    else
                    {
                        MessageBox.Show("Cree el formulario primero");
                    }
                }
            }
            catch (Exception ec)
            {
                MessageBox.Show("Error al modificar el honorario: " + ec.Message);
            }
            #endregion
        }


    }
}
