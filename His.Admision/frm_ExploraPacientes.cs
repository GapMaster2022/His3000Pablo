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
using Recursos;
using System.Drawing.Drawing2D;
using His.General;
using His.Parametros;
using Infragistics.Win.UltraWinGrid;
using System.IO;

namespace His.Admision
{
    public partial class frm_ExploraPacientes : Form
    {
        #region Variables
        ImageList iconImages;
        public string tipoc;
        public bool tipsec;
        private Int16 porcCobrar;
        //Variables de estado
        enum tipoActivo { ePaciente = 1, eMedicos, eTipoTratamiento, eTipoIngreso };
        enum tipoGrid { eRaiz = 1, eAtencion };
        private Int16 accion;
        private Int16 accionGrid;
        //Variables para filtros
        private string fechaCreacionIni;
        private string fechaCreacionFin;
        private string fechaIngresoIni;
        private string fechaIngresoFin;
        private string fechaAltaIni;
        private string fechaAltaFin;
        private string atencionActiva;
        private string codigoMedico;
        private string codigoAseguradoraEmpresa;
        //Variables contenedoras de las listas
        private List<PACIENTES_VISTA> pacientesVistaLista;

        #endregion

        #region Constructor
        public frm_ExploraPacientes()
        {
            InitializeComponent();
            //inicializo variables
            accion = (Int16)tipoActivo.ePaciente;
            //
            cargarRecursos();
            inicializarControles();
        }
        #endregion

        private void cargarListaPacientes()
        {

        }

        public void cargarRecursos()
        {
            //this.tssMedicos.Image  = Recursos.Archivo.btnOrganigrama;  
            //imagenes del menu principal
            //toolStripSplitButtonNuevo.Image = Archivo.imgBtnAdd2;
            toolStripButtonBuscar.Image = Archivo.imgBtnBuscar32;
            //toolStripButtonActualizar.Image = Archivo.imgBtnRestart;
            //toolStripButtonImprimir.Image = Archivo.imgBtnImprimir32;
            //toolStripButtonSalir.Image = Archivo.imgBtnSalir32;
            toolStripSplitButtonOrganizar.Image = Archivo.imgSptOrganizar;
            //toolStripButtonExportar.Image = Archivo.imgOfficeExcel;
            uBtnBuscarPaciente.Appearance.Image = Archivo.imgBtnBuscar32;
        }
        public void inicializarControles()
        {
            //Inicializo el imagenList
            iconImages = new System.Windows.Forms.ImageList();
            iconImages.ColorDepth = ColorDepth.Depth16Bit;
            iconImages.ImageSize = new Size(16, 16);
            iconImages.Images.Add(Archivo.icono);
            iconImages.Images.Add(Archivo.icoBtnOrganize);
            treeViewPacientes.ImageList = iconImages;

            //
            dtpFiltroDesde.Value = Convert.ToDateTime(String.Format("{0:g}", "01/" + DateTime.Now.Month + "/" + (DateTime.Now.Year).ToString()));
            dtpFiltroHasta.Value = DateTime.Now;

            //oculto controles no utilizados
            toolStripButtonImprimir.Visible = false;

            //
            cboEstadoAtencion.SelectedIndex = 1;
            //
            splitContainerPacientes.SplitterDistance = 200;

        }

        #region Eventos
        private void frm_ExploraPacientes_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            //cargo arbol paciente
            tipoc = "Pacientes";
            //ultraTreePacientes.DataSource = pacientesVistaLista;
            //ultraTreePacientes.DataMember = "NOMBRE"; 
            accion = (Int16)tipoActivo.ePaciente;
            //cargarArbol(accion);
            //cargo aseguragoras y empresas
            cargarAseguradoraEmpresa();
            //inicilizo controles
            fechaCreacionIni = String.Format("{0:yyyy-MM-dd }", dtpFiltroDesde.Value) + " 00:00:01";
            fechaCreacionFin = String.Format("{0:yyyy-MM-dd }", dtpFiltroHasta.Value) + " 23:59:59";
            splitContainerDetalle.Panel2Collapsed = true;

        }
        private void cargarAseguradoraEmpresa()
        {
            if (cboAseguradoras.Items.Count == 0)
            {
                ASEGURADORAS_EMPRESAS aseguradora = new ASEGURADORAS_EMPRESAS();
                aseguradora.ASE_CODIGO = -1;
                aseguradora.ASE_NOMBRE = " Ninguno ";
                List<ASEGURADORAS_EMPRESAS> listaAseguradoras = new List<ASEGURADORAS_EMPRESAS>();
                listaAseguradoras = NegAseguradoras.ListaEmpresas();
                listaAseguradoras.Add(aseguradora);
                cboAseguradoras.DataSource = listaAseguradoras.OrderBy(a => a.ASE_NOMBRE).ToList();
                cboAseguradoras.DisplayMember = "ASE_NOMBRE";
                cboAseguradoras.ValueMember = "ASE_CODIGO";
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void rbn_medicos_CheckedChanged(object sender, EventArgs e)
        {
            tipoc = "Medico";
            cargarArbol(accion);
            var pacientes = NegMedicos.MedicosdeAtenciones().OrderBy(c => c.MED_NOMBRE1).OrderBy(o => o.MED_APELLIDO_MATERNO).OrderBy(d => d.MED_APELLIDO_PATERNO).ToList();
            ultraGridAtenciones.DataSource = pacientes;

        }
        #endregion

        #region Metodos Privados

        private void cargarArbol(Int16 tipo)
        {
            try
            {
                //cargo el arbol de acuerdo a la opcion activa
                treeViewPacientes.Nodes.Clear();
                TreeNode raiz = new TreeNode();
                raiz.Name = "0";
                raiz.Text = "Todos";
                raiz.Tag = "raiz";
                treeViewPacientes.Nodes.Add(raiz);
                switch (tipo)
                {
                    case (Int16)tipoActivo.ePaciente:
                        var listaResumen = (from p in pacientesVistaLista
                                            select new { p.NOMBRE, p.PAC_CODIGO }).Distinct();
                        foreach (var item in listaResumen)
                        {
                            TreeNode nodoTipoHonorario = new TreeNode();
                            nodoTipoHonorario.Name = item.PAC_CODIGO.ToString();
                            nodoTipoHonorario.Text = item.NOMBRE;
                            nodoTipoHonorario.Tag = tipo;
                            raiz.Nodes.Add(nodoTipoHonorario);
                            //cargarDatosPaciente("Pacientes", item.PAC_CODIGO, nodoTipoHonorario);
                        }
                        break;
                    case (Int16)tipoActivo.eMedicos:

                        var medicos = NegMedicos.listaMedicosTratantes();
                        Int16 tipoIngreso = (Int16)tipoActivo.ePaciente;

                        foreach (var g in medicos)
                        {
                            TreeNode nodoTipoHonorario = new TreeNode();
                            nodoTipoHonorario.Text = g.MED_APELLIDO_PATERNO + " " + g.MED_APELLIDO_MATERNO + " " + g.MED_NOMBRE1 + " " + g.MED_NOMBRE2;
                            nodoTipoHonorario.Name = g.MED_CODIGO.ToString();
                            nodoTipoHonorario.Tag = tipo;

                            //var pacientes = NegPacientes.RecuperarPacientesLista(fechaCreacionIni, fechaCreacionFin,
                            //    fechaIngresoIni, fechaIngresoFin, fechaAltaIni, fechaAltaFin, atencionActiva, g.MED_CODIGO.ToString(), codigoAseguradoraEmpresa);
                            //foreach (var p in pacientes)
                            //{
                            //    TreeNode paciente = new TreeNode();
                            //    paciente.Name = p.PAC_CODIGO.ToString();
                            //    paciente.Text = p.NOMBRE;
                            //    paciente.ForeColor = Color.RoyalBlue;
                            //    paciente.Tag = tipoIngreso;
                            //    nodoTipoHonorario.Nodes.Add(paciente);
                            //}

                            raiz.Nodes.Add(nodoTipoHonorario);
                        }
                        //foreach (DtoMedicos item in NegMedicos.MedicosdeAtenciones().OrderBy(c=>c.MED_NOMBRE1).OrderBy(o=>o.MED_APELLIDO_MATERNO).OrderBy(d=>d.MED_APELLIDO_PATERNO))
                        //{
                        //    TreeNode nodoTipoMedico = new TreeNode();
                        //    nodoTipoMedico.Name = item.MED_CODIGO.ToString();
                        //    nodoTipoMedico.Text = item.MED_APELLIDO_PATERNO+ " " + item.MED_APELLIDO_MATERNO + " " + item.MED_NOMBRE1 + item.MED_NOMBRE2;
                        //    nodoTipoMedico.Tag = tipo;
                        //    raiz.Nodes.Add(nodoTipoMedico);
                        //    cargarDatosPaciente("Medico", item.MED_CODIGO, nodoTipoMedico);
                        //}

                        break;
                    case (Int16)tipoActivo.eTipoTratamiento:
                        var tipoTratamientoLista = NegTipoTratamiento.RecuperaTipoTratamiento().Select(t => new { t.TIA_CODIGO, t.TIA_DESCRIPCION }).ToList();
                        foreach (var tipoTratamiento in tipoTratamientoLista)
                        {
                            TreeNode nodoTipoMedico = new TreeNode();
                            nodoTipoMedico.Name = tipoTratamiento.TIA_CODIGO.ToString();
                            nodoTipoMedico.Text = tipoTratamiento.TIA_DESCRIPCION.ToString();
                            nodoTipoMedico.Tag = tipo;
                            nodoTipoMedico.ForeColor = Color.Chocolate;
                            raiz.Nodes.Add(nodoTipoMedico);
                            cargarDatosPaciente("TipoTratamiento", tipoTratamiento.TIA_CODIGO, nodoTipoMedico);
                        }
                        break;
                    //case "tipo_llamada":
                    //    //Sin llamada
                    //    TreeNode nodoSinllamada = new TreeNode();
                    //    nodoSinllamada.Name = "0";
                    //    nodoSinllamada.Text = "NO RECIBE LLAMADAS";
                    //    nodoSinllamada.Tag = tipo;
                    //    raiz.Nodes.Add(nodoSinllamada);
                    //    cargarDatosPaciente(tipo, 0, nodoSinllamada);
                    //    //Con llamada
                    //    TreeNode nodoConllamada = new TreeNode();
                    //    nodoConllamada.Name = "1";
                    //    nodoConllamada.Text = "RECIBE LLAMADAS";
                    //    nodoConllamada.Tag = tipo;
                    //    raiz.Nodes.Add(nodoConllamada);
                    //    cargarDatosPaciente(tipo, 1, nodoConllamada);
                    //    break;
                    default:
                        break;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
        //Cargo el arbol de medicos por especialidad
        private void cargarDatosPaciente(string tipo, int codigo, TreeNode nodo)
        {
            //List<MEDICOS> medicos;
            //medicos = NegMedicos.listaMedicos(codigo, tipo);
            if (tipo == "Pacientes" || tipsec == true)
            {
                List<ATENCIONES> atenciones = NegAtenciones.listaAtenciones().Where(cod => cod.PACIENTES.PAC_CODIGO == codigo).ToList();
                foreach (var aten in atenciones)
                {
                    TreeNode nodoMedico = new TreeNode();
                    nodoMedico.Name = aten.ATE_CODIGO.ToString();
                    nodoMedico.Text = aten.ATE_FECHA_INGRESO.ToString() + ' ' + aten.ATE_DIAGNOSTICO_FINAL;
                    nodoMedico.Tag = "atencion";
                    nodo.Nodes.Add(nodoMedico);
                }
            }
            else if (tipo == "Medico")
            {
                List<DtoPacientesInfo> atenciones = NegPacientes.ListaPacientesInfo("2").Where(cod => cod.MED_CODIGO == codigo).ToList();
                foreach (var aten in atenciones)
                {
                    TreeNode nodoMedico = new TreeNode();
                    nodoMedico.Name = aten.PAC_CODIGO.ToString();
                    nodoMedico.Text = aten.PAC_APELLIDO_PATERNO + " " + aten.PAC_APELLIDO_MATERNO + " " + aten.PAC_NOMBRE1 + " " + aten.PAC_NOMBRE2;
                    nodoMedico.Tag = tipoActivo.ePaciente;
                    nodo.Nodes.Add(nodoMedico);
                    tipsec = true;
                    cargarDatosPaciente("Pacientes", aten.PAC_CODIGO, nodoMedico);
                    tipsec = false;
                }
            }
            else if (tipo == "TipoTratamiento")
            {
                var atenciones = NegPacientes.RecuperarPacientesLista((Int16)codigo);
                foreach (var aten in atenciones)
                {
                    TreeNode nodoMedico = new TreeNode();
                    nodoMedico.Name = aten.PAC_CODIGO.ToString();
                    nodoMedico.Text = aten.PAC_APELLIDO_PATERNO + " " + aten.PAC_APELLIDO_MATERNO + " " + aten.PAC_NOMBRE1 + " " + aten.PAC_NOMBRE2;
                    nodoMedico.Tag = tipoActivo.ePaciente;
                    nodo.Nodes.Add(nodoMedico);
                    tipsec = true;
                    cargarDatosPaciente(tipo, aten.PAC_CODIGO, nodoMedico);
                    tipsec = false;
                }
            }
        }
        #endregion

        private void rbn_tiposervicios_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pacientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pacientesVistaLista != null)
            {
                accion = (Int16)tipoActivo.ePaciente;
                cargarArbol(accion);
            }
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void medicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pacientesVistaLista != null)
            {
                accion = (Int16)tipoActivo.eMedicos;
                cargarArbol(accion);
                //var pacientes = NegMedicos.MedicosdeAtenciones().OrderBy(c => c.MED_NOMBRE1).OrderBy(o => o.MED_APELLIDO_MATERNO).OrderBy(d => d.MED_APELLIDO_PATERNO).ToList();
                //panelFiltros2.DataSource = pacientes;
                //panelFiltros.DataSource = pacientes;
                //panelFiltros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                //camposGrid();
            }
        }

        private void tvw_datos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                treeViewPacientes.SelectedNode.ImageIndex = 0;
                //treeViewPacientes.SelectedNode.BackColor = Color.MediumAquamarine;

                if (treeViewPacientes.SelectedNode.Tag != null)
                {
                    if (treeViewPacientes.SelectedNode.Tag.ToString() == "raiz")
                    {
                        accionGrid = (Int16)tipoGrid.eRaiz;
                        //activarFiltros();
                        if (accion == (Int16)tipoActivo.ePaciente)
                        {
                            //cargo el grid de info de pacientes
                            ultraGridPacientes.DataSource = pacientesVistaLista;
                        }
                        splitContainerDetalle.Panel2Collapsed = true;
                        return;
                    }
                    if ((Int16)treeViewPacientes.SelectedNode.Tag == (Int16)tipoActivo.ePaciente)
                    {
                        accionGrid = (Int16)tipoGrid.eAtencion;
                        int codigoPaciente = Convert.ToInt32(treeViewPacientes.SelectedNode.Name);
                        CargarAtencionesPaciente(codigoPaciente);
                        splitContainerDetalle.Panel1Collapsed = true;
                    }
                    if ((Int16)treeViewPacientes.SelectedNode.Tag == (Int16)tipoActivo.eMedicos)
                    {
                        accionGrid = (Int16)tipoGrid.eRaiz;
                        int codigoMedico = Convert.ToInt32(treeViewPacientes.SelectedNode.Name);
                        var pacientes = NegPacientes.RecuperarPacientesLista(fechaCreacionIni, fechaCreacionFin,
                            fechaIngresoIni, fechaIngresoFin, fechaAltaIni, fechaAltaFin, atencionActiva, codigoMedico.ToString(), codigoAseguradoraEmpresa);
                        foreach (var p in pacientes)
                        {
                            TreeNode paciente = new TreeNode();
                            paciente.Name = p.PAC_CODIGO.ToString();
                            paciente.Text = p.NOMBRE;
                            paciente.ForeColor = Color.RoyalBlue;
                            paciente.Tag = (Int16)tipoActivo.ePaciente;
                            treeViewPacientes.SelectedNode.Nodes.Add(paciente);
                        }
                        //CargarAtencionesPaciente(codigoPaciente);
                        splitContainerDetalle.Panel2Collapsed = true;
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //cargo las atenciones de un determinado paciente
        private void CargarAtencionesPaciente(int keyPaciente)
        {
            List<DtoAtenciones> dataAtenciones = NegAtenciones.RecuperarAtencionesPaciente(keyPaciente);

            ultraGridAtenciones.DataSource = dataAtenciones;
            //panelFiltros.DataSource = dataAtenciones;
            //
            UltraGridBand panelFiltros = ultraGridAtenciones.DisplayLayout.Bands[0];
            panelFiltros.Columns["ATE_NUMERO_ATENCION"].Header.Caption = "N° ATENCION";
            panelFiltros.Columns["ATE_NUMERO_CONTROL"].Header.Caption = "N° CONTROL";
            panelFiltros.Columns["ATE_FACTURA_PACIENTE"].Header.Caption = "N° FACTURA";
            panelFiltros.Columns["ATE_FACTURA_FECHA"].Header.Caption = "FECHA DE LA FACTURA";
            panelFiltros.Columns["PAC_DIRECCION"].Header.Caption = "DIRECCION PACIENTE";
            panelFiltros.Columns["PAC_TELEFONO"].Header.Caption = "TELF. PACIENTE";
            panelFiltros.Columns["HAB_NUMERO"].Header.Caption = "N° HABITACION";
            panelFiltros.Columns["ATE_REFERIDO"].Header.Caption = "REFERIDO";
            panelFiltros.Columns["ATE_FECHA_ALTA"].Header.Caption = "FECHA DE ALTA";
            panelFiltros.Columns["ATE_FECHA_INGRESO"].Header.Caption = "FECHA DE INGRESO";
            panelFiltros.Columns["ATE_ESTADO"].Header.Caption = "ACTIVO";

            panelFiltros.Columns["ATE_CODIGO"].Hidden = true;
            panelFiltros.Columns["ATE_FECHA"].Hidden = true;
            panelFiltros.Columns["PAC_CODIGO"].Hidden = true;
            panelFiltros.Columns["CAJ_CODIGO"].Hidden = true;
            panelFiltros.Columns["PAC_NOMBRE"].Hidden = true;
            panelFiltros.Columns["PAC_NOMBRE2"].Hidden = true;
            panelFiltros.Columns["PAC_APELLIDO_PATERNO"].Hidden = true;
            panelFiltros.Columns["PAC_APELLIDO_MATERNO"].Hidden = true;
            panelFiltros.Columns["PAC_HCL"].Hidden = true;
            panelFiltros.Columns["PAC_CEDULA"].Hidden = true;
            panelFiltros.Columns["HAB_CODIGO"].Hidden = true;
            panelFiltros.Columns["HAB_CODIGO"].Hidden = true;
            panelFiltros.Columns["DAP_CODIGO"].Hidden = true;
            panelFiltros.Columns["ENTITYID"].Hidden = true;
            panelFiltros.Columns["ENTITYSETNAME"].Hidden = true;

            panelFiltros.Columns["ATE_NUMERO_ATENCION"].Header.VisiblePosition = 1;
            panelFiltros.Columns["ATE_NUMERO_CONTROL"].Header.VisiblePosition = 2;
            panelFiltros.Columns["HAB_NUMERO"].Header.VisiblePosition = 3;
            panelFiltros.Columns["ATE_FECHA_INGRESO"].Header.VisiblePosition = 4;
            panelFiltros.Columns["ATE_FECHA_ALTA"].Header.VisiblePosition = 5;
            panelFiltros.Columns["ATE_FACTURA_PACIENTE"].Header.VisiblePosition = 6;
            panelFiltros.Columns["ATE_FACTURA_FECHA"].Header.VisiblePosition = 7;
            panelFiltros.Columns["PAC_DIRECCION"].Header.VisiblePosition = 8;
            panelFiltros.Columns["PAC_TELEFONO"].Header.VisiblePosition = 9;
            panelFiltros.Columns["ATE_REFERIDO"].Header.VisiblePosition = 10;
            panelFiltros.Columns["ATE_ESTADO"].Header.VisiblePosition = 11;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dptFiltroHasta_ValueChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            if (this.txtHistoriaClinica.Text != "")
            {
                activarFiltrosHistoria();
            }
            else
            {
                activarFiltros();
            }

            //cargo el grid de info de pacientes
            ultraGridAtenciones.DataSource = pacientesVistaLista;
            //panelFiltros.DataSource = pacientesVistaLista;
            cargarArbol(accion);
        }

        private void cargarFormulario(ATENCION_DETALLE_FORMULARIOS_HCU atencion)
        {
            try
            {
                //compruebo que existe el directorio
                if (!System.IO.Directory.Exists(Application.LocalUserAppDataPath + "\\" + Parametros.GeneralPAR.getDirectorioPacientesFormularios()))
                {
                    System.IO.Directory.CreateDirectory(Application.LocalUserAppDataPath + "\\" + Parametros.GeneralPAR.getDirectorioPacientesFormularios());
                }

                //FtpClient clienteFTP = new FtpClient("10.10.1.15","gap","Gapgr2010");
                //clienteFTP.Download(atencion.ADF_DIRECTORIO, Application.LocalUserAppDataPath + "\\" + Parametros.GeneralPAR.getDirectorioPacientesFormularios()+"\\" + atencion.FORMULARIOS_HCU.FH_DIRECTORIO.Trim());
                //System.Diagnostics.Process.Start(Application.LocalUserAppDataPath + "\\" + Parametros.GeneralPAR.getDirectorioPacientesFormularios()+"\\" + atencion.FORMULARIOS_HCU.FH_DIRECTORIO.Trim());
                //DialogResult respuesta = new DialogResult();

                //respuesta = MessageBox.Show("Desea guardar los cambios en el archivo al servidor?", "Subir Archivo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                //if (respuesta == DialogResult.OK)
                //{
                //    string directorio=null;
                //    string[]  directorioArchivo = atencion.ADF_DIRECTORIO.Trim().Split('/');
                //    for (int i = 0; i < directorioArchivo.Count() - 1; i++)
                //    {
                //        if (directorioArchivo[i].ToString() != "")
                //        {
                //            if(directorio!=null ) 
                //                directorio = directorio + "\\" + directorioArchivo[i].ToString();
                //            else
                //                directorio = directorioArchivo[i].ToString();
                //        }
                //    }
                //    clienteFTP.ChangeDir(directorio);
                //    clienteFTP.Upload(Application.LocalUserAppDataPath + "\\" + Parametros.GeneralPAR.getDirectorioPacientesFormularios()+"\\" + atencion.FORMULARIOS_HCU.FH_DIRECTORIO.Trim());
                //}
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void addFormulario(FORMULARIOS_HCU formulario, int codigoAtencion, int codigoPaciente)
        {
            try
            {
                //
                ATENCIONES atencion = NegAtenciones.AtencionID(codigoAtencion);
                PACIENTES paciente = NegPacientes.RecuperarPacienteID(codigoPaciente);
                PACIENTES_DATOS_ADICIONALES datos = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPacienteID(codigoPaciente);
                //CIUDAD ciudad = NegCiudad.ListaCiudades().FirstOrDefault(c => c.EntityKey == paciente.CIUDADReference.EntityKey);
                //PAIS pais = NegPais.RecuperaPaises().FirstOrDefault(pa => pa.EntityKey == ciudad.PAISReference.EntityKey);
                ESTADO_CIVIL estadocivil = NegEstadoCivil.RecuperaEstadoCivil().FirstOrDefault(e => e.EntityKey == datos.ESTADO_CIVILReference.EntityKey);
                ETNIA etnia = NegEtnias.ListaEtnias().FirstOrDefault(e => e.EntityKey == paciente.ETNIAReference.EntityKey);
                //asigno los directorios a utilizar
                string directorioLocal = Application.LocalUserAppDataPath + "\\" + Parametros.GeneralPAR.getDirectorioMatrizFormularios();
                string formularioPadre = GeneralPAR.getDirectorioMatrizFormularios() + "\\" + formulario.FH_DIRECTORIO;
                string formularioLocal = directorioLocal + "\\" + formulario.FH_DIRECTORIO;
                UltraGridRow fila = ultraGridAtenciones.ActiveRow;
                string numeroHistoria = fila.Cells["PAC_HCL"].Value.ToString();
                string numeroAtencion = fila.Cells["ATE_NUMERO_ATENCION"].Value.ToString();
                string directorioRemoto = GeneralPAR.getDirectorioPacientesFormularios() + "\\" + numeroHistoria.Trim() + "\\" + numeroAtencion.Trim();
                //compruebo que existe el directorio
                if (!System.IO.Directory.Exists(directorioLocal))
                {
                    System.IO.Directory.CreateDirectory(directorioLocal);
                }

                //

                //bajo el archivo al directorio local para su edicion
                FtpClient clienteFTP = new FtpClient();
                clienteFTP.Login();
                clienteFTP.Download(formularioPadre, formularioLocal);

                //grabo los datos en cada formulario
                FormularioFTP form = new FormularioFTP();
                switch (formulario.FH_CODIGO)
                {
                    case 1:
                        #region Form. 001 ADMISION Y ALTA EGRESO
                        form.setFormularioAdmision(
                        formularioLocal,
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
                        "Ecuador",
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

                    case 2:
                        #region Form. 002 CONSULTA EXTERNA
                        form.setFormularioConsultaExterna(
                            formularioLocal,
                            paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2,
                            paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO,
                            paciente.PAC_GENERO,
                            atencion.ATE_EDAD_PACIENTE.ToString(),
                            paciente.PAC_HISTORIA_CLINICA);

                        #endregion
                        break;

                    case 3:
                        #region Form. 003 ANAMNESIS EXAMEN FISICO
                        form.setFormularioAnamnesis(
                        formularioLocal,
                        paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2,
                        paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO,
                        paciente.PAC_GENERO,
                        paciente.PAC_HISTORIA_CLINICA);

                        #endregion
                        break;

                    case 4:
                        #region Form. 005 EVOLUCION Y PRESCRIPCIONES
                        form.setFormularioEvolucion(
                        formularioLocal,
                        paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2,
                        paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO,
                        paciente.PAC_GENERO,
                        paciente.PAC_HISTORIA_CLINICA);

                        #endregion
                        break;

                    case 5:
                        #region Form. 006 EPICRISIS
                        form.setFormularioAnamnesis(
                            formularioLocal,
                            paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2,
                            paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO,
                            paciente.PAC_GENERO,
                            paciente.PAC_HISTORIA_CLINICA);

                        #endregion
                        break;

                    case 6:
                        #region Form. 007 INTERCONSULTA
                        form.setFormularioInterconsulta(
                            formularioLocal,
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
                            formularioLocal,
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
                            "Ecuador",
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
                            formularioLocal,
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
                            formularioLocal,
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
                            formularioLocal,
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
                            formularioLocal,
                            paciente.PAC_HISTORIA_CLINICA,
                            paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2,
                            paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO,
                            paciente.PAC_GENERO
                           );

                        #endregion
                        break;
                }

                ////
                ATENCION_DETALLE_FORMULARIOS_HCU detalle = new ATENCION_DETALLE_FORMULARIOS_HCU();
                detalle.ADF_CODIGO = NegAtencionDetalleFormulariosHCU.MaxCodigo() + 1;
                detalle.FORMULARIOS_HCUReference.EntityKey = formulario.EntityKey;
                detalle.ATENCIONESReference.EntityKey = atencion.EntityKey;
                detalle.ADF_FECHA_INGRESO = DateTime.Now;


                //subo el archivo al directorio de la atencion en el servidor
                clienteFTP.ChangeDir(directorioRemoto);
                clienteFTP.Upload(formularioLocal);

                //detalle.ADF_DIRECTORIO = clienteFTP.RemotePath + formulario.FH_DIRECTORIO;
                NegAtencionDetalleFormulariosHCU.Crear(detalle);

                ////

                // respuesta = MessageBox.Show("Desea guardar los cambios en el archivo al servidor?", "Subir Archivo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void actualizarListaFormularios()
        {
            try
            {
                formulariosToolStripMenuItem.DropDownItems.Clear();
                int codigoAtencion = Convert.ToInt32(ultraGridAtenciones.ActiveRow.Cells["ATE_CODIGO"].Value.ToString());
                int codigoPaciente = Convert.ToInt32(ultraGridAtenciones.ActiveRow.Cells["PAC_CODIGO"].Value.ToString());
                List<ATENCION_DETALLE_FORMULARIOS_HCU> listaDetalleFormularios = NegAtencionDetalleFormulariosHCU.listaAtencionDetalleFormularios(codigoAtencion).OrderBy(f => f.FORMULARIOS_HCU.FH_NOMBRE).ToList();
                List<FORMULARIOS_HCU> listaFormularios = NegFormulariosHCU.RecuperarFormulariosHCU().OrderBy(f => f.FH_NOMBRE).ToList();

                ToolStripMenuItem add = new ToolStripMenuItem();
                add.Text = "Añadir";
                add.Image = Archivo.imgBtnAdd;
                //add.Click += delegate {aniadirFormulario(); };
                foreach (var itemFormulario in listaFormularios)
                {
                    ToolStripMenuItem formulario = new ToolStripMenuItem();
                    formulario.Text = itemFormulario.FH_NOMBRE;
                    formulario.Image = Archivo.imgOfficeExcel;
                    formulario.Tag = itemFormulario;
                    formulario.Click += delegate { addFormulario((FORMULARIOS_HCU)formulario.Tag, codigoAtencion, codigoPaciente); };
                    add.DropDownItems.Add(formulario);
                }

                formulariosToolStripMenuItem.DropDownItems.Add(add);
                foreach (var item in listaDetalleFormularios)
                {
                    ToolStripMenuItem formulario = new ToolStripMenuItem();

                    formulario.Text = item.FORMULARIOS_HCU.FH_NOMBRE;
                    formulario.Image = Archivo.imgOfficeExcel;
                    formulario.Tag = item;
                    formulario.Click += delegate { cargarFormulario((ATENCION_DETALLE_FORMULARIOS_HCU)formulario.Tag); };
                    formulariosToolStripMenuItem.DropDownItems.Add(formulario);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void activarFiltros()
        {
            try
            {
                //Filtro por Fechas
                fechaCreacionIni = null;
                fechaCreacionFin = null;
                fechaIngresoIni = null;
                fechaIngresoFin = null;
                fechaAltaIni = null;
                fechaAltaFin = null;
                if (rbtCreacionPaciente.Checked == true)
                {
                    fechaCreacionIni = String.Format("{0:yyyy-MM-dd}", dtpFiltroDesde.Value) + " 00:00:01";
                    fechaCreacionFin = String.Format("{0:yyyy-MM-dd}", dtpFiltroHasta.Value) + " 23:59:59";
                }
                else if (rbtIngresoPaciente.Checked == true)
                {
                    fechaIngresoIni = String.Format("{0:yyyy/MM/dd}", dtpFiltroDesde.Value);
                    fechaIngresoFin = String.Format("{0:yyyy/MM/dd}", dtpFiltroHasta.Value) + " 23:59:59";
                }
                else if (rbtAltaPaciente.Checked == true)
                {
                    fechaAltaIni = String.Format("{0:yyyy/MM/dd}", dtpFiltroDesde.Value);
                    fechaAltaFin = String.Format("{0:yyyy-MM-dd}", dtpFiltroHasta.Value) + " 23:59:59";
                }

                //Estado Atencion
                atencionActiva = null;
                if (cboEstadoAtencion.SelectedIndex == 1)
                {
                    atencionActiva = "true";
                }
                else if (cboEstadoAtencion.SelectedIndex == 2)
                {
                    atencionActiva = "false";
                }

                //aseguradora
                codigoAseguradoraEmpresa = null;
                if (cboAseguradoras.SelectedIndex > 0)
                {
                    ASEGURADORAS_EMPRESAS aseguradora = (ASEGURADORAS_EMPRESAS)cboAseguradoras.SelectedItem;
                    codigoAseguradoraEmpresa = aseguradora.ASE_CODIGO.ToString();
                }

                pacientesVistaLista = NegPacientes.RecuperarPacientesLista(fechaCreacionIni, fechaCreacionFin,
                fechaIngresoIni, fechaIngresoFin, fechaAltaIni, fechaAltaFin, atencionActiva, codigoMedico, codigoAseguradoraEmpresa);

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void activarFiltrosHistoria()
        {
            try
            {

                pacientesVistaLista = NegPacientes.recuperarPacientePorHistoria(this.txtHistoriaClinica.Text);


            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void cboEstadoAtencion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tipoTratamientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pacientesVistaLista != null)
            {
                accion = (Int16)tipoActivo.eTipoTratamiento;
                cargarArbol(accion);
            }
        }

        private void treeViewPacientes_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                TreeViewHitTestInfo hit = treeViewPacientes.HitTest(e.X, e.Y);
            }
        }

        private void atencionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Int16)treeViewPacientes.SelectedNode.Tag == (Int16)tipoActivo.ePaciente)
                {
                    int codigo = Convert.ToInt32(treeViewPacientes.SelectedNode.Name.ToString());

                    PACIENTES_VISTA paciente = pacientesVistaLista.FirstOrDefault(p => p.PAC_CODIGO == codigo);
                    frm_Admision ventana = new frm_Admision();
                    ventana.Show();
                    ventana.CargarPaciente(paciente.PAC_HISTORIA_CLINICA,1);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void toolStripMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripSplitButtonOrganizar_ButtonClick(object sender, EventArgs e)
        {

        }

        private void detalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int codigoPaciente = Convert.ToInt32(ultraGridAtenciones.ActiveRow.Cells["PAC_CODIGO"].Value.ToString());
                string numeroAtencion = ultraGridAtenciones.ActiveRow.Cells["ATE_NUMERO_ATENCION"].Value.ToString();

                PACIENTES_VISTA paciente = pacientesVistaLista.FirstOrDefault(p => p.PAC_CODIGO == codigoPaciente);
                frm_Admision ventana = new frm_Admision();
                ventana.setCampoHistoriaClinica(paciente.PAC_HISTORIA_CLINICA);
                //ventana.CargarAtencion(numeroAtencion);
                ventana.CargarPaciente(paciente.PAC_HISTORIA_CLINICA, 1);
                ventana.MdiParent = this.MdiParent;
                ventana.Show();
            }
            catch (Exception err)
            { MessageBox.Show(err.Message); }
        }

        private void microfilmDeHCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UltraGridRow fila = ultraGridAtenciones.ActiveRow;
            string paciente = fila.Cells["PAC_APELLIDO_PATERNO"].Value + " " + fila.Cells["PAC_APELLIDO_MATERNO"].Value + " " + fila.Cells["PAC_NOMBRE"].Value + " " + fila.Cells["PAC_NOMBRE2"].Value;
            string apellido = fila.Cells["PAC_APELLIDO_PATERNO"].Value.ToString();
            string numeroAtencion = fila.Cells["ATE_NUMERO_ATENCION"].Value.ToString();
            string numeroHistoria = fila.Cells["PAC_HCL"].Value.ToString();
            int codPaciente = Convert.ToInt32(fila.Cells["PAC_CODIGO"].Value);
            int codAtencion = Convert.ToInt32(fila.Cells["ATE_CODIGO"].Value);

            //var expArchivos = new GeneralApp.ControlesWinForms.ClienteFTPVista(Convert.ToInt32(numeroHistoria));
            //string strDirectorio = apellido + "/" + numeroHistoria + "/" + codAtencion;
            //expArchivos.CarpetaServidor = strDirectorio;
            //expArchivos.Modo = "Microfilms";
            //expArchivos.ShowDialog();




            DataTable x=  NegDietetica.getDataTable("PathLocalHC");
            
            string strDirectorio =   numeroHistoria + "//" + codAtencion + "//";
            if(x.Rows.Count > 0)
            {
                string strPathGeneral = x.Rows[0][0].ToString();

                var expArchivos = new GeneralApp.ControlesWinForms.ExploradorLocal(strDirectorio, strPathGeneral, paciente, codAtencion.ToString());
                expArchivos.ShowDialog();
            }
            else
            {
                MessageBox.Show("No se encontro datos para cargar microfilm.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //UltraGridRow fila = ultraGridAtenciones.ActiveRow;
            //string paciente = fila.Cells["PAC_APELLIDO_PATERNO"].Value + " " + fila.Cells["PAC_APELLIDO_MATERNO"].Value + " " + fila.Cells["PAC_NOMBRE"].Value + " " + fila.Cells["PAC_NOMBRE2"].Value;
            //string apellido = fila.Cells["PAC_APELLIDO_PATERNO"].Value.ToString();
            //string numeroAtencion = fila.Cells["ATE_NUMERO_ATENCION"].Value.ToString();
            //string numeroHistoria = fila.Cells["PAC_HCL"].Value.ToString();
            //int codPaciente = Convert.ToInt32(fila.Cells["PAC_CODIGO"].Value);
            //int codAtencion = Convert.ToInt32(fila.Cells["ATE_CODIGO"].Value);

            //var expArchivos = new GeneralApp.ControlesWinForms.ClienteFTPVista(Convert.ToInt32(numeroHistoria));
            //string strDirectorio = apellido + "/" + numeroHistoria + "/" + codAtencion;
            //expArchivos.CarpetaServidor = strDirectorio;
            //expArchivos.Modo = "Microfilms";
            //expArchivos.ShowDialog();

            //frmExploradorMicrofilms ventana = new frmExploradorMicrofilms(paciente, numeroAtencion, numeroHistoria, codPaciente, codAtencion);
            //ventana.lblPaciente = fila.Cells["PAC_APELLIDO_PATERNO"] + " " + fila.Cells["PAC_APELLIDO_MATERNO"] + " " + fila.Cells["PAC_NOMBRE"] + " " + fila.Cells["PAC_NOMBRE2"];   
            //ventana.ShowDialog();   
        }

        private void historiaClinicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string ateCodigo = ultraGridAtenciones.ActiveRow.Cells["ATE_CODIGO"].Value.ToString();
                int codigoAtencion = Convert.ToInt32(ateCodigo);
                His.AdminHistoriasClinicas.txtNombrePaciente exploradorHC = new His.AdminHistoriasClinicas.txtNombrePaciente(codigoAtencion);
                exploradorHC.Show();
                His.Formulario.frm_Evolucion evolucion = new His.Formulario.frm_Evolucion(codigoAtencion, false);
                evolucion.MdiParent = exploradorHC;
                evolucion.Show(); 
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void treeViewPacientes_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.ImageIndex = 1;
        }

        private void cboAseguradoras_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panelFiltros2_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ultraGridAtenciones.DisplayLayout.Bands[0];

            ultraGridAtenciones.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            ultraGridAtenciones.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            ultraGridAtenciones.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            ultraGridAtenciones.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            ultraGridAtenciones.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            ultraGridAtenciones.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            ultraGridAtenciones.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            //Caracteristicas de Filtro en la grilla
            ultraGridAtenciones.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ultraGridAtenciones.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ultraGridAtenciones.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ultraGridAtenciones.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            ultraGridAtenciones.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            ultraGridAtenciones.DisplayLayout.UseFixedHeaders = true;
        }

        private void panelFiltros2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (accionGrid == (Int16)tipoGrid.eRaiz)
                {
                    ultraGridAtenciones.ContextMenuStrip = null;
                }
                else if (accionGrid == (Int16)tipoGrid.eAtencion)
                {
                    ultraGridAtenciones.ContextMenuStrip = atencionContextMenuStrip;
                    //actualizarListaFormularios();
                }
            }
        }

        private void ultraGridPacientes_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            try
            {
                UltraGridBand bandUno = ultraGridPacientes.DisplayLayout.Bands[0];

                ultraGridPacientes.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                ultraGridPacientes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                ultraGridPacientes.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                ultraGridPacientes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                ultraGridPacientes.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                ultraGridPacientes.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                ultraGridPacientes.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Caracteristicas de Filtro en la grilla
                ultraGridPacientes.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                ultraGridPacientes.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                ultraGridPacientes.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                ultraGridPacientes.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                ultraGridPacientes.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                ultraGridPacientes.DisplayLayout.UseFixedHeaders = true;

                if (accion == (Int16)tipoActivo.ePaciente)
                {
                    //cambio el texto de las cabeceras
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_HISTORIA_CLINICA"].Header.Caption = "Historia Clínica";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["NOMBRE"].Header.Caption = "Nombre";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_FECHA_CREACION"].Header.Caption = "Ingreso al Sistema";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_NACIONALIDAD"].Header.Caption = "Nacionalidad";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["EDAD"].Header.Caption = "Edad";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["GENERO"].Header.Caption = "Genero";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_ETNIA"].Header.Caption = "Etnia";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_GRUPOSANQUINEO"].Header.Caption = "Grupo Sanguíneo";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_IDENTIFICACION"].Header.Caption = "Identificación";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_EMAIL"].Header.Caption = "E mail";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_DIRECCION_DOMICILIO"].Header.Caption = "Dirección ";
                    //dbgrPacientes.Columns["ZONA"].HeaderText = "Sector";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_TELEFONO1"].Header.Caption = "Teléfono 1";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_TELEFONO2"].Header.Caption = "Teléfono 2";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_OCUPACION"].Header.Caption = "Ocupación";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_INSTRUCCION"].Header.Caption = "Instrucción";

                    //asigno un ancho por defecto para las columnas
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_HISTORIA_CLINICA"].Width = 60;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["NOMBRE"].Width = 220;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_FECHA_CREACION"].Width = 120;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_NACIONALIDAD"].Width = 120;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["EDAD"].Width = 60;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["GENERO"].Width = 80;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_ETNIA"].Width = 120;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_GRUPOSANQUINEO"].Width = 60;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_IDENTIFICACION"].Width = 140;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_EMAIL"].Width = 200;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_DIRECCION_DOMICILIO"].Width = 300;
                    //dbgrPacientes.Columns["ZONA"].Width = 200;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_TELEFONO1"].Width = 140;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_TELEFONO2"].Width = 140;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_OCUPACION"].Width = 160;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_INSTRUCCION"].Width = 160;

                    //alineo el texto de las columnas
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_HISTORIA_CLINICA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["NOMBRE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Left;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_FECHA_CREACION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_NACIONALIDAD"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["EDAD"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["GENERO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_ETNIA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_GRUPOSANQUINEO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_IDENTIFICACION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_EMAIL"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_DIRECCION_DOMICILIO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    //dbgrPacientes.Columns["ZONA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_TELEFONO1"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_TELEFONO2"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_OCUPACION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_INSTRUCCION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                    //cambio el orden en que se presentaran las columnas
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_HISTORIA_CLINICA"].Header.VisiblePosition = 1;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["NOMBRE"].Header.VisiblePosition = 2;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_FECHA_CREACION"].Header.VisiblePosition = 3;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_NACIONALIDAD"].Header.VisiblePosition = 4;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["EDAD"].Header.VisiblePosition = 5;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["GENERO"].Header.VisiblePosition = 6;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_ETNIA"].Header.VisiblePosition = 7;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_GRUPOSANQUINEO"].Header.VisiblePosition = 8;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_IDENTIFICACION"].Header.VisiblePosition = 9;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_EMAIL"].Header.VisiblePosition = 10;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_DIRECCION_DOMICILIO"].Header.VisiblePosition = 11;
                    //dbgrPacientes.Columns["ZONA"].DisplayIndex = 12;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_TELEFONO1"].Header.VisiblePosition = 13;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_TELEFONO2"].Header.VisiblePosition = 14;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_OCUPACION"].Header.VisiblePosition = 15;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["DAP_INSTRUCCION"].Header.VisiblePosition = 16;

                    //oculto columnas 
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_CODIGO"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["PAC_TIPO_IDENTIFICACION"].Hidden = true;
                    //panelFiltros2.DisplayLayout.Bands[0].Columns["CODCIUDAD"].Hidden = true;


                }
                else if (accion == (Int16)tipoActivo.eMedicos)
                {
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_CODIGO"].Header.Caption = "CODIGO";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_NOMBRE1"].Header.Caption = "NOMBRES";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_NOMBRE2"].Header.Caption = "";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_APELLIDO_PATERNO"].Header.Caption = "APELLIDOS";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_APELLIDO_MATERNO"].Header.Caption = "";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["ESP_NOMBRE"].Header.Caption = "ESPECIALIDAD";
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_RUC"].Header.Caption = "RUC";

                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_DIRECCION"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_TELEFONO_CASA"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_TELEFONO_CONSULTORIO"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_TELEFONO_CELULAR"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_ESTADO"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_DIRECCION_CONSULTORIO"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["ESP_CODIGO"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["ID_USUARIO"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["BAN_CODIGO"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["TIH_CODIGO"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_FECHA_MODIFICACION"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_EMAIL"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["TIM_CODIGO"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_FECHA"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_GENERO"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["ESC_CODIGO"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_NUM_CUENTA"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_TIPO_CUENTA"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_CUENTA_CONTABLE"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_FECHA_NACIMIENTO"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_AUTORIZACION_SRI"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_VALIDEZ_AUTORIZACION"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["RET_CODIGO"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["RET_DESCRIPCION"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["RET_PORCENTAJE"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_FACTURA_INICIAL"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_FACTURA_FINAL"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_CON_TRANSFERENCIA"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_RECIBE_LLAMADA"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["MED_CODIGO_MEDICO"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["ENTITYSETNAME"].Hidden = true;
                    ultraGridPacientes.DisplayLayout.Bands[0].Columns["ENTITYID"].Hidden = true;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void ultraTabControlPacientes_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {

        }

        private void toolStripButtonExportar_Click(object sender, EventArgs e)
        {
            try
            {
                string PathExcel = FindSavePath();
                if (PathExcel != null)
                {
                    if (ultraGridPacientes.CanFocus == true)
                        this.ultraGridExcelExporter1.Export(ultraGridPacientes, PathExcel);
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + PathExcel);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally
            { this.Cursor = Cursors.Default; }
        }

        /// <summary>
        /// Busca el directorio donde se guarda el archivo de excel
        /// </summary>
        /// <returns>retorna el directorio</returns>
        private String FindSavePath()
        {
            Stream myStream;
            string myFilepath = null;
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "excel files (*.xls)|*.xls";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        myFilepath = saveFileDialog1.FileName;
                        myStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myFilepath;
        }

        private void lblInfListaPacientes_Click(object sender, EventArgs e)
        {

        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            frm_AyudaPacientes form = new frm_AyudaPacientes();
            form.campoPadre = txtHistoriaClinica;
            form.ShowDialog();
            form.Dispose();
            txtHistoriaClinica.Text = txtHistoriaClinica.Text.Trim();
        }

        private void txtHistoriaClinica_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }

        private void pacienteContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {

        }

        private void grid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = ultraGridAtenciones.DisplayLayout.Bands[0];

            ultraGridAtenciones.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
            //grid.DisplayLayout.Override.Allow

            ultraGridAtenciones.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            ultraGridAtenciones.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            ultraGridAtenciones.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            ultraGridAtenciones.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            ultraGridAtenciones.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            ultraGridAtenciones.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            //Caracteristicas de Filtro en la grilla
            ultraGridAtenciones.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ultraGridAtenciones.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ultraGridAtenciones.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ultraGridAtenciones.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            ultraGridAtenciones.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            ultraGridAtenciones.DisplayLayout.UseFixedHeaders = true;

        }

        private void ultraGridAtenciones_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (accionGrid == (Int16)tipoGrid.eRaiz)
                {
                    ultraGridAtenciones.ContextMenuStrip = null;
                }
                else if (accionGrid == (Int16)tipoGrid.eAtencion)
                {
                    ultraGridAtenciones.ContextMenuStrip = atencionContextMenuStrip;
                    //actualizarListaFormularios();
                }
            }

        }
    }



}
