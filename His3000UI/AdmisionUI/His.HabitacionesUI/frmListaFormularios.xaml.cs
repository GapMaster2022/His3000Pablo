using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using His.Entidades;
using His.Negocio;

namespace His.HabitacionesUI
{
    /// <summary>
    /// Lógica de interacción para frmListaFormularios.xaml
    /// </summary>
    public partial class frmListaFormularios : Window
    {
        List<FORMULARIOS_HCU> formularios = new List<FORMULARIOS_HCU>();
        List<ATENCION_DETALLE_FORMULARIOS_HCU> formulariosAtencion = new List<ATENCION_DETALLE_FORMULARIOS_HCU>();
        List<ATENCION_DETALLE_FORMULARIOS_HCU> formulariosAtenciones = new List<ATENCION_DETALLE_FORMULARIOS_HCU>();

        ATENCIONES atencion = null;
        PACIENTES paciente = null;
        PACIENTES_DATOS_ADICIONALES datos = null;
        CIUDAD ciudad = null;
        PAIS pais = null;
        ESTADO_CIVIL estadocivil = null;
        ETNIA etnia = null;
        public frmListaFormularios()
        {
            InitializeComponent();
            formularios = NegFormulariosHCU.RecuperarFormulariosHCU();
            CargarDatos();
        }

        public frmListaFormularios(ATENCIONES a)
        {
            InitializeComponent();
            try
            {
                formularios = NegFormulariosHCU.RecuperarFormulariosHCU();
                PACIENTES p = NegPacientes.RecuperarPacienteID(a.PACIENTES.PAC_CODIGO);
                PACIENTES_DATOS_ADICIONALES d = NegPacienteDatosAdicionales.RecuperarDatosPacientesID(a.PACIENTES.PAC_CODIGO);
                atencion = a;
                paciente = p;
                datos = d;
                estadocivil = NegEstadoCivil.RecuperaEstadoCivil().FirstOrDefault(e => e.EntityKey == d.ESTADO_CIVILReference.EntityKey);
                etnia = NegEtnias.ListaEtnias().FirstOrDefault(e => e.EntityKey == p.ETNIAReference.EntityKey);

                CargarDatos();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void CargarDatos()
        {
            try
            {
                List<ListaFormularios> lista = formularios.Select(f => new ListaFormularios() { check = false, codigo = f.FH_CODIGO, nombre = f.FH_NOMBRE }).OrderBy(f => f.nombre).ToList();
                grid.DataSource = lista;


                //if (atencion == null)
                //{
                //    btnAdd.IsEnabled = false;
                //}
                //else
                //{
                //    CargarFormulariosAtencion(atencion.ATE_CODIGO);
                //    if (atencion.ATE_DIRECTORIO == null || atencion.ATE_DIRECTORIO == string.Empty)
                //    {
                //        btnAdd.IsEnabled = false;
                //    }
                //}
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
                foreach (var dr in grid.DataItems)
                {

                    //int codigoForm = Convert.ToInt16(dr.);
                    //FORMULARIOS_HCU formul = formularios.FirstOrDefault(f => f.FH_CODIGO == codigoForm);
                    //if (acceso.FORMULARIOS_HCUReference.EntityKey == formul.EntityKey)
                    //    dr.Cells[0].Value = true;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string nombrefor = "";
            int validarForm = 0;
            foreach (Infragistics.Windows.DataPresenter.DataRecord formulario in grid.Records)
            {
                if (Convert.ToBoolean(formulario.Cells[0].Value))
                //        MessageBox.Show("si");

                //    MessageBox.Show(item.Cells[1].Value.ToString());
                //    MessageBox.Show(item.Cells[2].Value.ToString());
                //}
                // if (grid.ActiveRecord != null)
                //    {
                //        if (!grid.ActiveRecord.HasChildren)
                //        {
                //            if (grid.ActiveDataItem != null)
                {
                    ListaFormularios item = (ListaFormularios)formulario.DataItem;
                    var codigoForm = item.codigo;
                    FORMULARIOS_HCU formul = formularios.FirstOrDefault(f => f.FH_CODIGO == codigoForm);
                    ATENCION_DETALLE_FORMULARIOS_HCU detalle = formulariosAtencion.FirstOrDefault(d => d.FORMULARIOS_HCUReference.EntityKey == formul.EntityKey);
                    List<ATENCION_DETALLE_FORMULARIOS_HCU> listas = NegAtencionDetalleFormulariosHCU.listaAtencionDetalleFormularios(atencion.ATE_CODIGO).OrderBy(f => f.FORMULARIOS_HCU.FH_CODIGO).ToList();
                    foreach (var items in listas)
                    {
                        string codigoFH = items.FORMULARIOS_HCU.FH_CODIGO.ToString();
                        nombrefor = items.FORMULARIOS_HCU.FH_NOMBRE.ToString();
                        if (codigoFH.Trim() == Convert.ToString(codigoForm).Trim())
                        {
                            validarForm++;

                        }
                    }
                    if (validarForm == 0 || codigoForm == 21)
                    {
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
                                    //case 2:
                                    //    #region Form. 002 CONSULTA EXTERNA
                                    //    form.setFormularioConsultaExterna(
                                    //        directorio + "\\" + formul.FH_DIRECTORIO,
                                    //        paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2,
                                    //        paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO,
                                    //        paciente.PAC_GENERO,
                                    //        atencion.ATE_EDAD_PACIENTE.ToString(),
                                    //        paciente.PAC_HISTORIA_CLINICA);

                                    //    #endregion
                                    //    break;
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
                            MessageBox.Show("Formulario añadido exitosamente", "Inf", MessageBoxButton.OK, MessageBoxImage.Information);

                        }
                    }
                    else
                        MessageBox.Show("El Formulario  ya fue añadido anteriormente", "Inf", MessageBoxButton.OK, MessageBoxImage.Information);

                }

                //}
            }
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void grid_DataValueChanged(object sender, Infragistics.Windows.DataPresenter.Events.DataValueChangedEventArgs e)
        {

        }

        private void grid_CellUpdated(object sender, Infragistics.Windows.DataPresenter.Events.CellUpdatedEventArgs e)
        {

        }

        private void grid_SelectedItemsChanging(object sender, Infragistics.Windows.DataPresenter.Events.SelectedItemsChangingEventArgs e)
        {
            if (grid.SelectedItems.Count() > 0)
                e.Handled = true;
        }

        private void grid_SelectedItemsChanged(object sender, Infragistics.Windows.DataPresenter.Events.SelectedItemsChangedEventArgs e)
        {
            if (grid.SelectedItems.Count() > 0)
                e.Handled = true;
        }

    }

    public class ListaFormularios
    {
        public bool check { get; set; }
        public int codigo { get; set; }
        public string nombre { get; set; }
    }
}
