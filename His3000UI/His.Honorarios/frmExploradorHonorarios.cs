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
using His.Parametros;
using Infragistics.Win.UltraWinGrid;
using System.IO;
using System.Windows.Forms.Integration;
using Infragistics.Win.UltraWinTabControl;
using His.Entidades.General;


namespace His.Honorarios
{
    public partial class frmExploradorHonorarios : Form
    {
    #region variables
        public static bool estadoInfMedico = true;
        //variables para los filtros
        private string porRecuperar = null;
        private string medCodigo = null;
        private string espCodigo = null;
        private string tihCodigo = null;
        private string timCodigo = null;
        private string medRecibeLlamada = null;
        private string fechaIniFacturaMedico = null;
        private string fechaFinFacturaMedico = null;
        private string honorariosCancelados = null;
        private string sinRetencion = null;
        private string forCodigo = null;
        private string tifCodigo = null; 
        private string lote = null;
        private string numeroControl = null;
        private string facturaClinica = null;
        private string FechaIniFacturaCliente = null;
        private string FechaFinFacturaCliente = null;
        private string pacienteReferido = null;
        private string pacienteFechaAlta = null;
        private string ateCodigo = null;
        private string pacCodigo = null;
        //variable de sesion
        private bool sesion = false;
        private bool nuevoRecuperacionHonorarios = false;
        private bool iniHonorariosPorPagar = false;
        private bool iniFacturasIngresadas = false;
        private bool iniNotasCreditoFormaPago = false;
        private bool iniRecuperacionHonorariosTemp = false;
        private string tipoTransferencia="multiple";
        private bool actualizando=false;
        //honorarios directos
        private bool honorariosDirectos = false;
        private bool pagosDirectos = false;
        //temporales
        private string formatoProdubanco;
        //control de loading
        ElementHost host;
        GeneralApp.ControlesWPF.LoadingAnimation loading;
        //generales
        //private List<UltraTab> tabPagesOcultas;
        private List<NOTAS_CREDITO_DEBITO> notasRetenciones;
        //listas
        List<HONORARIOS_MEDICOS_TRANSFERENCIAS> listaHonorariosMedicosTransferencias;
        List<MEDICOS> listaMedicos;
        List<HONORARIOS_VISTA> listaHonorarios;
    #endregion

    #region constructor
        public frmExploradorHonorarios()
        {
            InitializeComponent();
            //inicializo variables
            //tabPagesOcultas = new List<UltraTab>();
            notasRetenciones = new List<NOTAS_CREDITO_DEBITO>();
            listaMedicos = new List<MEDICOS>(); 
            //. cargo las opciones por defecto del explorador  
            cargarValoresPorDefecto();
            cargarRecursos();
            cargarDimensiones();
            cargarArbolMedicos("tipo_honorario");
            //cargo filtros

            //habilito la lista de todos los medicos
            //cargarInfMedico(null);
            //dbdgEstadoCuentas.DataSource = NegHonorariosMedicos.RecuperaHonorariosMedicos();

            //Cargar controles

            //Grid de facturas temporales
            //dbgrdCanceladasTemp.DataSource = null;
            //dbugrRecuperacionHonorariosTemp = null; 
  
        }
    #endregion

    #region metodos_generales

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
        }

        //. Cargo los valores por defecto
        public void cargarValoresPorDefecto()
        {
            try
            {
                //Combobox
                cboEstadoDocumento.SelectedIndex = 0;

                //Fechas
       
                dtpHasta.Value = DateTime.Now.Date;
              
                //menus
                toolStripButtonGuardar.Enabled = false;
                toolStripSplitButtonNuevo.Enabled = true;

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);  
            }
        }
        //. Cargo las dimensiones por defecto de los paneles
        public void cargarDimensiones()
        {
            //panel de cancelacion honorarios medicos
            splitterCancelacion.Panel1Collapsed = true;
            splitterCancelacion.SplitterDistance = 80;
            //Panel1 del marco principal
            Marco.SplitterDistance = 80;
            Marco.Panel1MinSize = 30;
            //arbol de medicos
            MarcoCuerpo.Panel1Collapsed = false;
            MarcoCuerpo.SplitterDistance = 170;
            MarcoCuerpo.Panel1MinSize = 60;
            //marco de detalle
            marcoInfMedicos.Panel1Collapsed = true;
            //MarcoDetalle.Panel2Collapsed = true;  
            MarcoDetalle.SplitterDistance = 15;
            MarcoDetalle.Panel1MinSize = 15;
            //
            tableLayoutPanelCancelacionFac.ColumnStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanelCancelacionFac.ColumnStyles[0].Width = 100;
            tableLayoutPanelCancelacionFac.ColumnStyles[1].SizeType = SizeType.Percent;
            //
            btnPagosClinica.Width = 120;   
            //
            tableLayoutPanelDocumentosMedicos.RowStyles[0].SizeType = SizeType.Percent;
            tableLayoutPanelDocumentosMedicos.RowStyles[0].Height = 100;
            tableLayoutPanelDocumentosMedicos.RowStyles[1].SizeType = SizeType.Percent;
            tableLayoutPanelDocumentosMedicos.RowStyles[1].Height = 0;
        }
        public void cargarRecursos()
        {
            //this.tssMedicos.Image  = Recursos.Archivo.btnOrganigrama;  
            //imagenes del menu principal
            toolStripSplitButtonNuevo.Image = Archivo.imgBtnAdd2;
            toolStripButtonCancelar.Image = Archivo.imgBtnStop;
            toolStripButtonGuardar.Image = Archivo.imgBtnFloppy;
            toolStripButtonActualizar.Image = Archivo.imgBtnRestart;
            toolStripButtonExportar.Image = Archivo.imgOfficeExcel;  
            toolStripButtonImprimir.Image = Archivo.imgBtnImprimir32;
            toolStripButtonSalir.Image = Archivo.imgBtnSalir32;
            tssMedicos.Image = Archivo.imgSptOrganizar;
            //btnOcultarInfMedico.Appearance.Image = Archivo.imgBtnFlechaArriba16;

        }
        //. Cargo Inf del Medico en el cuadro de detalle de informacion medico
        private void cargarInfMedico(MEDICOS medico)
        {
            if (medico != null)
            {
                txtNombre.Text = medico.MED_NOMBRE1 + ' ' + medico.MED_NOMBRE2;
                txtDireccion.Text = medico.MED_DIRECCION;
                txtNumCuenta.Text = medico.MED_NUM_CUENTA;
                txtTelefonoCasa.Text = medico.MED_TELEFONO_CASA;
                txtTelefonoCelular.Text = medico.MED_TELEFONO_CELULAR;
                txtApellido.Text = medico.MED_APELLIDO_PATERNO + ' ' + medico.MED_APELLIDO_MATERNO;
                txtRuc.Text = medico.MED_RUC;
                txtTipoCuenta.Text = (medico.MED_TIPO_CUENTA == null) ? "" : ((medico.MED_TIPO_CUENTA == "C") ? "Corriente" : "Ahorros");
                txtTelefonoConsultorio.Text = medico.MED_TELEFONO_CONSULTORIO;
                pbAccionista.Visible = true;
                txtAutorizacionSri.Text = medico.MED_AUTORIZACION_SRI;
                txtValidezAutorizacion.Text = medico.MED_VALIDEZ_AUTORIZACION;
                txtFacturaInicial.Text = medico.MED_FACTURA_INICIAL;
                txtFacturaFinal.Text = medico.MED_FACTURA_FINAL;
            }
            else
            {
                txtNombre.Text = "";
                txtDireccion.Text = "";
                txtNumCuenta.Text = "";
                txtTelefonoCasa.Text = "";
                txtTelefonoCelular.Text = "";
                txtApellido.Text = "";
                txtRuc.Text = "";
                txtTipoCuenta.Text = "";
                txtTelefonoConsultorio.Text = "";
                pbAccionista.Visible = false;
                txtAutorizacionSri.Text = "";
                txtValidezAutorizacion.Text = "";
                txtFacturaInicial.Text = "";
                txtFacturaFinal.Text = "";
            }
        }

        //. Carga el grid con la información de todos los medicos
        private void cargarInfMedicos()
        {
            try
            {
                dbgrdListaMedicos.DataSource = NegMedicos.RecuperaMedicosFormulario().Select(m=>new {
                                    CODIGO=m.MED_CODIGO,
                                    MEDICO=(m.MED_APELLIDO_PATERNO + " "+m.MED_APELLIDO_MATERNO+" "+m.MED_NOMBRE1+" "+m.MED_NOMBRE2),
                                    CDM =m.MED_CODIGO_MEDICO,
                                    DIRECCION = m.MED_DIRECCION,
                                    DIRECCION_CONSULTORIO =m.MED_DIRECCION_CONSULTORIO,
                                    RUC=m.MED_RUC,
                                    TRASNFERENCIA=m.MED_CON_TRANSFERENCIA,
                                    EMAIL=m.MED_EMAIL,
                                    FECHA_NACIMIENTO=m.MED_FECHA_NACIMIENTO,
                                    FECHA_INGRESO=m.MED_FECHA,
                                    RECIBE_LLAMADA=m.MED_RECIBE_LLAMADA,
                                    TELEFONO_CASA=m.MED_TELEFONO_CASA,
                                    TELEFONO_CELULAR=m.MED_TELEFONO_CELULAR,
                                    TELEFONO_CONSULTORIO=m.MED_TELEFONO_CONSULTORIO
                        
                }).OrderBy(m=>m.MEDICO).ToList() ;
                //dbgrdListaMedicos.Columns["MED_CODIGO"].Visible = false;
                //dbgrdListaMedicos.Columns["RET_CODIGO"].Visible = false;
                //dbgrdListaMedicos.Columns["BAN_CODIGO"].Visible = false;
                //dbgrdListaMedicos.Columns["ESP_CODIGO"].Visible = false;
                //dbgrdListaMedicos.Columns["ESC_CODIGO"].Visible = false;
                //dbgrdListaMedicos.Columns["ID_USUARIO"].Visible = false;
                //dbgrdListaMedicos.Columns["TIM_CODIGO"].Visible = false;
                //dbgrdListaMedicos.Columns["MED_FECHA"].Visible = false;
                //dbgrdListaMedicos.Columns["RET_DESCRIPCION"].Visible = false;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error");
            }
        }

        //Cargo el arbol de medicos
        private void cargarArbolMedicos(string tipo)
        {
            //List<DtoMedicos> medicos = NegMedicos.RecuperaMedicosFormulario();
            TreeNode raiz = new TreeNode();
            raiz.Name = "0";
            raiz.Text = "Honorarios";
            raiz.Tag = "raiz";
            arbolMedicos.Nodes.Clear();   
            arbolMedicos.Nodes.Add(raiz);
            switch (tipo)
            {
                case "tipo_honorario":
                    listaMedicos = NegMedicos.listaMedicosIncTipoHonorario(); 
                    foreach (TIPO_HONORARIO item in NegMedicos.RecuperaTipoHonorario())
                    { 
                        TreeNode nodoTipoHonorario = new TreeNode();
                        nodoTipoHonorario.Name = item.TIH_CODIGO.ToString(); 
                        nodoTipoHonorario.Text = item.TIH_NOMBRE;
                        nodoTipoHonorario.Tag = tipo;
                        raiz.Nodes.Add(nodoTipoHonorario);
                        cargarMedicosPorTipo(tipo, item.TIH_CODIGO,nodoTipoHonorario);
                    }
                    break; 
                case "tipo_especialidad":
                    listaMedicos = NegMedicos.listaMedicosIncEspecialidadesMedicas().ToList(); 
                    foreach (ESPECIALIDADES_MEDICAS  item in NegMedicos.RecuperaTipoEspecialidad())
                    {
                        TreeNode nodoTipoEspecialidad = new TreeNode();
                        nodoTipoEspecialidad.Name=item.ESP_CODIGO.ToString(); 
                        nodoTipoEspecialidad.Text = item.ESP_NOMBRE;
                        nodoTipoEspecialidad.Tag =tipo;
                        raiz.Nodes.Add(nodoTipoEspecialidad);
                        cargarMedicosPorTipo(tipo, item.ESP_CODIGO, nodoTipoEspecialidad);
                    }
                    break; 
                case "tipo_medico":
                    listaMedicos = NegMedicos.listaMedicosIncTipoMedico(); 
                    foreach(TIPO_MEDICO item in NegMedicos.RecuperaTipoMedico())
                    {
                        TreeNode nodoTipoMedico = new TreeNode();
                        nodoTipoMedico.Name = item.TIM_CODIGO.ToString();   
                        nodoTipoMedico.Text = item.TIM_NOMBRE;
                        nodoTipoMedico.Tag =tipo;
                        raiz.Nodes.Add(nodoTipoMedico);
                        cargarMedicosPorTipo(tipo, item.TIM_CODIGO, nodoTipoMedico);
                    }
                    break;
                case "tipo_llamada":
                    //Sin llamada
                    TreeNode nodoSinllamada = new TreeNode();
                    nodoSinllamada.Name = "0";
                    nodoSinllamada.Text = "NO RECIBE LLAMADAS";
                    nodoSinllamada.Tag = tipo;
                    raiz.Nodes.Add(nodoSinllamada);
                    cargarMedicosPorTipo(tipo, 0, nodoSinllamada); 
                    //Con llamada
                    TreeNode nodoConllamada = new TreeNode();
                    nodoConllamada.Name = "1";
                    nodoConllamada.Text = "RECIBE LLAMADAS";
                    nodoConllamada.Tag = tipo;
                    raiz.Nodes.Add(nodoConllamada);
                    cargarMedicosPorTipo(tipo, 1, nodoConllamada); 
                    break; 
                default:
                    break; 
            }
        }

        //Cargo el arbol de medicos por especialidad
        private void cargarMedicosPorTipo(string tipo,int codigo,TreeNode nodo)
        {
            try
            {
            List<DtoMedicosCombo> medicos = new List<DtoMedicosCombo>() ;
            switch (tipo)
            {
                case "tipo_honorario":
                    medicos = (from m in listaMedicos
                               where m.TIPO_HONORARIO.TIH_CODIGO == codigo
                               select new DtoMedicosCombo{idMedico = m.MED_CODIGO.ToString(),
                                            medicoNombre = (m.MED_APELLIDO_PATERNO 
                                            + ' ' + m.MED_APELLIDO_MATERNO 
                                            + ' ' + m.MED_NOMBRE1 
                                            + ' ' + m.MED_NOMBRE2)}).OrderBy(m=>m.medicoNombre).ToList(); 
                    break;
                case "tipo_especialidad":
                    medicos = (from m in listaMedicos
                               where m.ESPECIALIDADES_MEDICAS.ESP_CODIGO == codigo
                               select new DtoMedicosCombo
                               {
                                   idMedico = m.MED_CODIGO.ToString(),
                                   medicoNombre = (m.MED_APELLIDO_PATERNO
                                   + ' ' + m.MED_APELLIDO_MATERNO
                                   + ' ' + m.MED_NOMBRE1
                                   + ' ' + m.MED_NOMBRE2)
                               }).OrderBy(m => m.medicoNombre).ToList(); 
                    break;
                case "tipo_medico":
                    medicos = (from m in listaMedicos
                               where m.TIPO_MEDICO.TIM_CODIGO == codigo
                               select new DtoMedicosCombo
                               {
                                   idMedico = m.MED_CODIGO.ToString(),
                                   medicoNombre = (m.MED_APELLIDO_PATERNO
                                   + ' ' + m.MED_APELLIDO_MATERNO
                                   + ' ' + m.MED_NOMBRE1
                                   + ' ' + m.MED_NOMBRE2)
                               }).OrderBy(m => m.medicoNombre).ToList(); 
                    break;
                case "tipo_llamada":
                    bool tipoLlamada = Convert.ToBoolean(codigo);
                    medicos = (from m in listaMedicos
                               where m.MED_RECIBE_LLAMADA == tipoLlamada
                               select new DtoMedicosCombo
                               {
                                   idMedico = m.MED_CODIGO.ToString(),
                                   medicoNombre = (m.MED_APELLIDO_PATERNO
                                   + ' ' + m.MED_APELLIDO_MATERNO
                                   + ' ' + m.MED_NOMBRE1
                                   + ' ' + m.MED_NOMBRE2)
                               }).OrderBy(m => m.medicoNombre).ToList(); 
                    break;
                default:
                    break;
            }
            
            foreach (var medico in medicos)
            {
                  TreeNode nodoMedico = new TreeNode();
                  nodoMedico.Name = medico.idMedico;
                   nodoMedico.Text = medico.medicoNombre;
                   nodoMedico.Tag = "medico";
                   nodo.Nodes.Add(nodoMedico);
            }
                }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);   
            }
        }

        //Activar Filtros de Acuerdo a la tarea a realizarse
        private void cargarFiltros(string tipoTarea)
        {
            //valores por defecto de los filtros de fechas
            optPorDefecto.Visible = false;
            optFacturaMedico.Visible = false;
            optFacturaClinica.Visible = false;
            optPorDefecto.Text = "Fecha Documento";
            Point ubicacionO = new Point();
            ubicacionO.X = 70;
            ubicacionO.Y = 15;
            optPorDefecto.Location = ubicacionO;

            switch (tipoTarea)
            {
                case "recuperacionCartera":
                    panelFiltroNumFactura.Visible = true; 
                    panelFiltroFechas.Visible = true;
                        optPorDefecto.Visible = false;
                        optFacturaMedico.Visible = true;
                        optFacturaClinica.Visible = true; 
                    panelFiltroFormaPago.Visible = true;
                    panelFiltroPaciente.Visible = true; 
                    break;
                case "listaRecuperacion":
                    panelFiltroNumFactura.Visible = false;
                    panelFiltroFechas.Visible = true;
                        optPorDefecto.Visible = true;
                        optFacturaMedico.Visible = false;
                        optFacturaClinica.Visible = false;
                    panelFiltroFormaPago.Visible = true;
                    panelFiltroPaciente.Visible = false;
                    break;
                case "cancelacionHonorarios":
                    panelFiltroNumFactura.Visible = false;
                    //armo el filtro de fechas
                    panelFiltroFechas.Visible = true;
                        optPorDefecto.Visible = false;
                        optFacturaMedico.Visible = true;
                        optFacturaClinica.Visible = false; 
                    panelFiltroFormaPago.Visible = true;
                    panelFiltroPaciente.Visible = false;
                    break;
                case "listaCancelaciones":
                    panelFiltroNumFactura.Visible = false;
                    //armo el filtro de fechas
                    panelFiltroFechas.Visible = true;
                    optPorDefecto.Visible = true;
                    optFacturaMedico.Visible = false;
                    optFacturaClinica.Visible = false;
                    panelFiltroFormaPago.Visible = true;
                    panelFiltroPaciente.Visible = false;
                    break;
                case "estadoCuentas":
                default:
                    panelFiltroNumFactura.Visible = true;
                    panelFiltroFechas.Visible = true;
                        optPorDefecto.Visible = true;
                        optFacturaMedico.Visible = false;
                        optFacturaClinica.Visible = false; 
                    panelFiltroFormaPago.Visible = true;
                    panelFiltroPaciente.Visible = true; 
                    break;
            }
        }

    #endregion

        #region filtros
        // FILTROS GENERALES
        
        //cargo el filtro de las formas de pago
        public void cargarTipoFormaPago(ComboBox combo)
        {
            try
            {
                List<TIPO_FORMA_PAGO> listaTipoFormaPago =  NegFormaPago.RecuperaTipoFormaPagos();
                foreach (TIPO_FORMA_PAGO tipoFormaPago in listaTipoFormaPago)
                {
                    combo.Items.Add(new KeyValuePair<int,string>(tipoFormaPago.TIF_CODIGO,tipoFormaPago.TIF_NOMBRE)); 
                }
                combo.DisplayMember = "Value";
                combo.ValueMember = "Key";
                combo.SelectedIndex = 0;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "err");
            }
        }
        //FACTURAS INGRESADAS
        //cargo el filtro de las formas de pago
        public void cargarFormaPago(ComboBox combo,Int16 tipoFormaPago)
        {
            try
            {
                combo.Items.Clear();
                cboFiltroFormaPago.Items.Add("Todas");
                List<FORMA_PAGO> listaFormaPago = NegFormaPago.RecuperaFormaPago(tipoFormaPago);
                foreach (FORMA_PAGO formaPago in listaFormaPago)
                {
                    combo.Items.Add(new KeyValuePair<int,string>(formaPago.FOR_CODIGO,formaPago.FOR_DESCRIPCION));
                }
                combo.DisplayMember = "Value";
                combo.ValueMember = "Key";
                combo.SelectedIndex = 0;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "err");
            }
        }
        #endregion

        #region metodos_ingreso_factura
        ///<summary>
        ///</summary> 
        //public void cargarFormatoFacturasIngresadas()
        //{
        //    //Añado titulos a las cabeceras
        //    facturasIngresadas.Columns["MED_CODIGO"].HeaderText = "CODIGO";
        //    facturasIngresadas.Columns["MED_NOMBRE_MEDICO"].HeaderText = "MEDICO";
        //    facturasIngresadas.Columns["HOM_FACTURA_MEDICO"].HeaderText = "FACTURA DEL MEDICO";
        //    facturasIngresadas.Columns["HOM_FACTURA_FECHA"].HeaderText = "FEC. FACTURA";
        //    facturasIngresadas.Columns["PAC_NOMBRE_PACIENTE"].HeaderText = "PACIENTE";
        //    facturasIngresadas.Columns["ATE_NUMERO_CONTROL"].HeaderText = "NUMERO DE CONTROL";
        //    facturasIngresadas.Columns["ATE_FACTURA_PACIENTE"].HeaderText = "FACTURA PACIENTE";
        //    facturasIngresadas.Columns["ATE_FACTURA_FECHA"].HeaderText = "FEC. FACTURA";
        //    facturasIngresadas.Columns["FOR_DESCRIPCION"].HeaderText = "FORMA DE PAGO";
        //    facturasIngresadas.Columns["HOM_LOTE"].HeaderText = "LOTE";
        //    facturasIngresadas.Columns["RET_CODIGO1"].HeaderText = "NUM. RETENCION";
        //    facturasIngresadas.Columns["HOM_VALOR_NETO"].HeaderText = "HONORARIO";
        //    facturasIngresadas.Columns["HOM_COMISION_CLINICA"].HeaderText = "COMISION CLINICA";
        //    facturasIngresadas.Columns["HOM_APORTE_LLAMADA"].HeaderText = "REFERIDO";
        //    facturasIngresadas.Columns["HOM_RETENCION"].HeaderText = "VALOR RETENCION";
        //    facturasIngresadas.Columns["HOM_VALOR_TOTAL"].HeaderText = "VALOR A PAGAR";
        //    facturasIngresadas.Columns["HOM_VALOR_PAGADO"].HeaderText = "VALOR RECUPERADO";
            
        //    //asigno el ancho por defecto de las columnas
        //    facturasIngresadas.Columns["columnaCheck"].Width = 40;
        //    facturasIngresadas.Columns["MED_CODIGO"].Width = 50;
        //    facturasIngresadas.Columns["MED_NOMBRE_MEDICO"].Width = 160;
        //    facturasIngresadas.Columns["HOM_FACTURA_MEDICO"].Width = 100;
        //    facturasIngresadas.Columns["HOM_FACTURA_FECHA"].Width = 70;
        //    facturasIngresadas.Columns["PAC_NOMBRE_PACIENTE"].Width = 160;
        //    facturasIngresadas.Columns["ATE_NUMERO_CONTROL"].Width = 80;
        //    facturasIngresadas.Columns["ATE_FACTURA_PACIENTE"].Width = 100;
        //    facturasIngresadas.Columns["ATE_FACTURA_FECHA"].Width = 70;
        //    facturasIngresadas.Columns["FOR_DESCRIPCION"].Width = 120;
        //    facturasIngresadas.Columns["HOM_LOTE"].Width = 80;
        //    facturasIngresadas.Columns["RET_CODIGO1"].Width = 100;
        //    facturasIngresadas.Columns["HOM_VALOR_NETO"].Width = 80;
        //    facturasIngresadas.Columns["HOM_COMISION_CLINICA"].Width = 70;
        //    facturasIngresadas.Columns["HOM_APORTE_LLAMADA"].Width = 70;
        //    facturasIngresadas.Columns["HOM_RETENCION"].Width = 70;
        //    facturasIngresadas.Columns["HOM_VALOR_TOTAL"].Width = 80;
        //    facturasIngresadas.Columns["HOM_VALOR_PAGADO"].Width = 80;

        //    //Alineo las columnas
        //    facturasIngresadas.Columns["MED_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    facturasIngresadas.Columns["MED_NOMBRE_MEDICO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    facturasIngresadas.Columns["HOM_FACTURA_MEDICO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    facturasIngresadas.Columns["HOM_FACTURA_FECHA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    facturasIngresadas.Columns["PAC_NOMBRE_PACIENTE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    facturasIngresadas.Columns["ATE_NUMERO_CONTROL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    facturasIngresadas.Columns["ATE_FACTURA_PACIENTE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    facturasIngresadas.Columns["ATE_FACTURA_FECHA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    facturasIngresadas.Columns["FOR_DESCRIPCION"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    facturasIngresadas.Columns["HOM_LOTE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    facturasIngresadas.Columns["RET_CODIGO1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    facturasIngresadas.Columns["HOM_VALOR_NETO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    facturasIngresadas.Columns["HOM_COMISION_CLINICA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    facturasIngresadas.Columns["HOM_APORTE_LLAMADA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    facturasIngresadas.Columns["HOM_RETENCION"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    facturasIngresadas.Columns["HOM_VALOR_TOTAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    facturasIngresadas.Columns["HOM_VALOR_PAGADO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        //    //Ordeno las columnas
        //    facturasIngresadas.Columns["columnaCheck"].DisplayIndex = 1;
        //    facturasIngresadas.Columns["MED_CODIGO"].DisplayIndex = 2;
        //    facturasIngresadas.Columns["MED_NOMBRE_MEDICO"].DisplayIndex = 3;
        //    facturasIngresadas.Columns["HOM_FACTURA_MEDICO"].DisplayIndex = 4;
        //    facturasIngresadas.Columns["HOM_FACTURA_FECHA"].DisplayIndex = 5;
        //    facturasIngresadas.Columns["PAC_NOMBRE_PACIENTE"].DisplayIndex = 6;
        //    facturasIngresadas.Columns["ATE_NUMERO_CONTROL"].DisplayIndex = 7;
        //    facturasIngresadas.Columns["ATE_FACTURA_PACIENTE"].DisplayIndex = 8;
        //    facturasIngresadas.Columns["ATE_FACTURA_FECHA"].DisplayIndex = 9;
        //    facturasIngresadas.Columns["FOR_DESCRIPCION"].DisplayIndex = 10;
        //    facturasIngresadas.Columns["HOM_LOTE"].DisplayIndex = 11;
        //    facturasIngresadas.Columns["HOM_FECHA_INGRESO"].DisplayIndex = 12;
        //    facturasIngresadas.Columns["HOM_VALOR_NETO"].DisplayIndex = 13;
        //    facturasIngresadas.Columns["RET_CODIGO1"].DisplayIndex = 14;
        //    facturasIngresadas.Columns["HOM_RETENCION"].DisplayIndex = 15;
        //    facturasIngresadas.Columns["HOM_COMISION_CLINICA"].DisplayIndex = 16;
        //    facturasIngresadas.Columns["HOM_APORTE_LLAMADA"].DisplayIndex = 17;
        //    facturasIngresadas.Columns["HOM_VALOR_TOTAL"].DisplayIndex = 18;
        //    facturasIngresadas.Columns["HOM_VALOR_PAGADO"].DisplayIndex = 19;


        //    //Color
        //    facturasIngresadas.Columns["MED_CODIGO"].DefaultCellStyle.BackColor = Color.LightCyan;
        //    facturasIngresadas.Columns["MED_NOMBRE_MEDICO"].DefaultCellStyle.BackColor = Color.LightCyan;
        //    facturasIngresadas.Columns["HOM_FACTURA_MEDICO"].DefaultCellStyle.BackColor = Color.LightCyan;
        //    facturasIngresadas.Columns["HOM_FACTURA_FECHA"].DefaultCellStyle.BackColor = Color.LightCyan;
        //    facturasIngresadas.Columns["PAC_NOMBRE_PACIENTE"].DefaultCellStyle.BackColor = Color.LightSteelBlue;
        //    facturasIngresadas.Columns["ATE_NUMERO_CONTROL"].DefaultCellStyle.BackColor = Color.LightSteelBlue;
        //    facturasIngresadas.Columns["ATE_FACTURA_PACIENTE"].DefaultCellStyle.BackColor = Color.LightSteelBlue;
        //    facturasIngresadas.Columns["ATE_FACTURA_FECHA"].DefaultCellStyle.BackColor = Color.LightSteelBlue;
        //    facturasIngresadas.Columns["FOR_DESCRIPCION"].DefaultCellStyle.BackColor = Color.LightSteelBlue;
        //    facturasIngresadas.Columns["HOM_LOTE"].DefaultCellStyle.BackColor = Color.LightSteelBlue;
        //    facturasIngresadas.Columns["HOM_VALOR_NETO"].DefaultCellStyle.BackColor = Color.WhiteSmoke;
        //    facturasIngresadas.Columns["RET_CODIGO1"].DefaultCellStyle.BackColor = Color.WhiteSmoke;
        //    facturasIngresadas.Columns["HOM_RETENCION"].DefaultCellStyle.BackColor = Color.WhiteSmoke;
        //    facturasIngresadas.Columns["HOM_COMISION_CLINICA"].DefaultCellStyle.BackColor = Color.WhiteSmoke;
        //    facturasIngresadas.Columns["HOM_APORTE_LLAMADA"].DefaultCellStyle.BackColor = Color.WhiteSmoke;
        //    facturasIngresadas.Columns["HOM_VALOR_TOTAL"].DefaultCellStyle.BackColor = Color.WhiteSmoke;
        //    facturasIngresadas.Columns["HOM_VALOR_PAGADO"].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            
        //    //Resalto columnas importantes
        //    //facturasIngresadas.Columns["MED_NOMBRE_MEDICO"].DefaultCellStyle.Font = new Font(facturasIngresadas.Font,FontStyle.Bold);
        //    facturasIngresadas.Columns["MED_NOMBRE_MEDICO"].DefaultCellStyle.ForeColor = Color.RoyalBlue;
        //    facturasIngresadas.Columns["PAC_NOMBRE_PACIENTE"].DefaultCellStyle.ForeColor = Color.RoyalBlue;


        //    //Oculto columnas 
        //    facturasIngresadas.Columns["HOM_CODIGO"].Visible = false;
        //    facturasIngresadas.Columns["ATE_CODIGO"].Visible = false;
        //    facturasIngresadas.Columns["FOR_CODIGO"].Visible = false;
        //    facturasIngresadas.Columns["ID_USUARIO"].Visible = false;
        //    facturasIngresadas.Columns["HOM_VALOR_CANCELADO"].Visible = false;
        //    facturasIngresadas.Columns["HOM_FECHA_INGRESO"].Visible = false;

        //    //marco como solo de lecturas todas las columnas excepto la de check
        //    foreach (DataGridViewColumn columna in facturasIngresadas.Columns)
        //    {
        //        columna.ReadOnly = (columna.Index > 0) ? true : false;  
        //    }
        //    //ingreso false como valor por defecto en el checkbox  
        //    //variables para la totalizacion
        //    Decimal tempTotalNeto = 0;
        //    Decimal tempTotalComision = 0;
        //    Decimal tempTotalReferido = 0;
        //    Decimal tempTotalValor = 0;
        //    Decimal tempTotalPagado = 0;

        //    foreach (DataGridViewRow item in facturasIngresadas.Rows)
        //    {
        //        item.Cells["columnaCheck"].Value = false;
        //        tempTotalNeto += Convert.ToDecimal(item.Cells["HOM_VALOR_NETO"].Value);
        //        tempTotalComision += Convert.ToDecimal(item.Cells["HOM_COMISION_CLINICA"].Value);
        //        tempTotalReferido += Convert.ToDecimal(item.Cells["HOM_APORTE_LLAMADA"].Value);
        //        tempTotalValor += Convert.ToDecimal(item.Cells["HOM_VALOR_TOTAL"].Value   ) ;
        //        tempTotalPagado += Convert.ToDecimal(item.Cells["HOM_VALOR_PAGADO"].Value);
        //    }
        //    txtTotalFacturacion.Text = tempTotalNeto.ToString();
        //    txtTotalComision.Text = tempTotalComision.ToString();
        //    txtTotalReferido.Text = tempTotalReferido.ToString();
        //    txtTotalAdeudado.Text = tempTotalValor.ToString();
        //    txtTotalPagado.Text = tempTotalPagado.ToString();   
        //}







        public void cargarFormatoRecuperacionMedicosDetalle()
        {
            //
            dbgrPagosMedicosDetalle.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;  

            dbgrPagosMedicosDetalle.Columns["PMD_CODIGO"].Visible = false;
            dbgrPagosMedicosDetalle.Columns["PAM_CODIGO"].Visible = false;

            //Añado titulos a las cabeceras
            dbgrPagosMedicosDetalle.Columns["NOMBRE_MEDICO"].HeaderText = "MEDICO";
            dbgrPagosMedicosDetalle.Columns["HOM_FACTURA_MEDICO"].HeaderText = "FACTURA MEDICO";
            dbgrPagosMedicosDetalle.Columns["HOM_FACTURA_FECHA"].HeaderText = "FECHA FACTURA";
            dbgrPagosMedicosDetalle.Columns["ATE_NUMERO_CONTROL"].HeaderText = "NUMERO DE CONTROL";
            dbgrPagosMedicosDetalle.Columns["ATE_FACTURA_PACIENTE"].HeaderText = "FACTURA PACIENTE";
            dbgrPagosMedicosDetalle.Columns["PMD_VALOR"].HeaderText = "VALOR DEL PAGO";
            

            //asigno el ancho por defecto de las columnas

            dbgrPagosMedicosDetalle.Columns["NOMBRE_MEDICO"].Width = 240;
            dbgrPagosMedicosDetalle.Columns["HOM_FACTURA_MEDICO"].Width = 140;
            dbgrPagosMedicosDetalle.Columns["HOM_FACTURA_FECHA"].Width = 100;
            dbgrPagosMedicosDetalle.Columns["ATE_NUMERO_CONTROL"].Width = 100;
            dbgrPagosMedicosDetalle.Columns["ATE_FACTURA_PACIENTE"].Width = 100;
            dbgrPagosMedicosDetalle.Columns["PMD_VALOR"].Width = 100;

            //Alineo las columnas
           
            dbgrPagosMedicosDetalle.Columns["NOMBRE_MEDICO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dbgrPagosMedicosDetalle.Columns["HOM_FACTURA_MEDICO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dbgrPagosMedicosDetalle.Columns["HOM_FACTURA_FECHA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dbgrPagosMedicosDetalle.Columns["ATE_NUMERO_CONTROL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dbgrPagosMedicosDetalle.Columns["ATE_FACTURA_PACIENTE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dbgrPagosMedicosDetalle.Columns["PMD_VALOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //Ordeno las columnas

            dbgrPagosMedicosDetalle.Columns["NOMBRE_MEDICO"].DisplayIndex = 0;
            dbgrPagosMedicosDetalle.Columns["HOM_FACTURA_MEDICO"].DisplayIndex = 1;
            dbgrPagosMedicosDetalle.Columns["HOM_FACTURA_FECHA"].DisplayIndex = 2;
            dbgrPagosMedicosDetalle.Columns["ATE_NUMERO_CONTROL"].DisplayIndex = 3;
            dbgrPagosMedicosDetalle.Columns["ATE_FACTURA_PACIENTE"].DisplayIndex = 4;
            dbgrPagosMedicosDetalle.Columns["PMD_VALOR"].DisplayIndex = 5;
        }

       
        //. Cargo el Grid de Facturas Ingresadas sin filtros
        public void cargarFacturasMedicos()
        {
            try
            {
                //facturasIngresadas.DataSource = NegHonorariosMedicos.RecuperaHonorariosMedicos();
                dbugrFacturasIngresadas.DataSource = NegHonorariosMedicos.RecuperaHonorariosMedicos();  
                //cargarFormatoFacturasIngresadas();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error");
            }
        }
        //. Cargo el Grid de Facturas Ingresadas por Medico
        public void cargarFacturasMedicos(string tipo,string porRecuperar,string medCodigo, string espCodigo, string tihCodigo, string timCodigo, string medRecibeLlamada, string fechaIniFacturaMedico,
            string FechaFinFacturaMedico, string honorariosCancelados, string forCodigo,string tifCodigo, string lote, string numeroControl, string facturaClinica,
            string FechaIniFacturaCliente, string FechaFinFacturaCliente, string pacienteReferido, string pacienteFechaAlta, string ateCodigo,string pacCodigo,UltraGrid dataGrid)
        {
            try
            {
                if(actualizando)
                    listaHonorarios  = NegHonorariosMedicos.RecuperarHonorariosMedicos(tipo,porRecuperar,medCodigo, espCodigo, tihCodigo, timCodigo, medRecibeLlamada, fechaIniFacturaMedico,
                        FechaFinFacturaMedico, honorariosCancelados,sinRetencion, forCodigo,tifCodigo, lote, numeroControl, facturaClinica, FechaIniFacturaCliente,
                        FechaFinFacturaCliente, pacienteReferido, pacienteFechaAlta, ateCodigo, pacCodigo);
                
                dataGrid.DataSource = listaHonorarios;

                
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error");
            }
        }
        //. Cargo el Grid de Facturas Ingresadas por Medico y paciente
        //public void cargarFacturasMedicos(int codigoMedico, int codigoPaciente)
        //{
        //    try
        //    {
        //        facturasIngresadas.DataSource = NegHonorariosMedicos.RecuperarHonorariosMedicos(null, null, null, null, null, null, null, null, null, null,
        //                null, null, null, null);
        //        cargarFormatoFacturasIngresadas();
        //    }
        //    catch (Exception err)
        //    {
        //        MessageBox.Show(err.Message, "Error");
        //    }
        //}
        //Buscar el nodo con check en el arbol
        private TreeNode buscarNodo(TreeNode arbol)
        {
            foreach (TreeNode nod in arbol.Nodes )
            {
                if (nod.Checked == true)
                {
                    return nod; 
                }
                if (nod.Nodes.Count > 0)
                {
                    TreeNode hijo =buscarNodo(nod); 
                    if (hijo != null )
                    {
                        return hijo; 
                    }
                }
            }
            return null;
        }


        //filtro las facturas ingresadas de los medicos
        private void filtrarFacturasIngresadasMedicos(TreeNode nodo)
        {
            try
            {
                //filtro de Estado de Cancelacion
                honorariosCancelados = null;
                porRecuperar = null;
                
                if (cboEstadoDocumento.SelectedIndex == 1)
                    honorariosCancelados = "1";
                else if (cboEstadoDocumento.SelectedIndex == 2)
                    honorariosCancelados = "0";
                else if (cboEstadoDocumento.SelectedIndex == 3)
                    porRecuperar = "0";
                else if (cboEstadoDocumento.SelectedIndex == 4)
                    porRecuperar = "1";

                //filtro los valores por recuperar
                if (nuevoRecuperacionHonorarios == true)
                {
                    porRecuperar = "1";
                }

                //filtro Numero de Control
                numeroControl = null;
                if (txtNumeroControl.Text.Trim() != "")
                {
                    numeroControl = txtNumeroControl.Text;  
                }
  
                //filtro Factura Clinica
                facturaClinica = null;
                if (txtFacturaClinica.Text.Trim() != "")
                {
                    facturaClinica = txtFacturaClinica.Text;
                }

                //filtro Fechas
                fechaIniFacturaMedico = null;
                fechaFinFacturaMedico = null;
                FechaIniFacturaCliente = null;
                FechaFinFacturaCliente = null;
             
                if (optFacturaMedico.Checked == true)
                {
                    fechaIniFacturaMedico = String.Format("{0:yyyy/MM/dd}",dtpDesde.Value);
                    fechaFinFacturaMedico = String.Format("{0:yyyy/MM/dd}", dtpHasta.Value);
                }
                else if (optFacturaClinica.Checked == true)
                {
                    FechaIniFacturaCliente = String.Format("{0:yyyy/MM/dd}", dtpDesde.Value);
                    FechaFinFacturaCliente = String.Format("{0:yyyy/MM/dd}", dtpHasta.Value);
                }
                //filtro tipo forma de pago
                tifCodigo = null; 
                if (cboFiltroTipoFormaPago.SelectedIndex > 0)
                {
                    KeyValuePair<int, string> tipoFormaPago = (KeyValuePair<int, string>)cboFiltroTipoFormaPago.SelectedItem;
                    tifCodigo  = tipoFormaPago.Key.ToString();
                }

                //filtro forma de pago
                forCodigo = null;
                if (cboFiltroFormaPago.Visible == true && cboFiltroFormaPago.SelectedIndex >0)
                {
                    KeyValuePair<int, string> formaPago = (KeyValuePair<int, string>)cboFiltroFormaPago.SelectedItem;
                    forCodigo = formaPago.Key.ToString();
                }

                //filtro por lote
                lote = null;
                if (txtLote.Visible == true && txtLote.Text.Trim()!="")
                {
                    lote = txtLote.Text;  
                }
  
                //filtro Pacientes
                pacCodigo = null;

                //Filtro medicos
                medCodigo = null;
                if (nodo.Tag.ToString() == "medico")
                {
                    //habilito la pantalla de informacion personal del medico
                    marcoInfMedicos.Panel2Collapsed = true;
                    MEDICOS medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(arbolMedicos.SelectedNode.Name));
                    //Cargo la informacion detallada del medico
                    cargarInfMedico(medico);
                    //cargo el filtro del medico
                    medCodigo = medico.MED_CODIGO.ToString(); 
                }

                //Raiz - no realiza filtro de la informacion
                if (nodo.Tag.ToString() == "raiz")
                {
                    //habilito la lista de todos los medicos
                    marcoInfMedicos.Panel1Collapsed = true;
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error al cargar la información del medico");
            }
        }

        //Cargo el grid de las facturas para cancelar
        public bool cargarFacturasParaCancelar()
        {
            try
            {
                List<HONORARIOS_VISTA> listaHonorarios = new List<HONORARIOS_VISTA>();
                int contador=0;
                foreach (UltraGridRow fila in dbugrFacturasIngresadas.Rows)
                { 
                    if (Convert.ToBoolean(fila.Cells["columnaCheck"].Value) == true)
                    {
                        if ((Convert.ToDecimal(fila.Cells["VALOR_POR_RECUPERAR"].Value) - Convert.ToDecimal(fila.Cells["HOM_VALOR_PAGADO"].Value)) > 0)
                        {
                            HONORARIOS_VISTA honorario = (HONORARIOS_VISTA)fila.ListObject;
                            listaHonorarios.Add(honorario);
                            contador++;   
                        }
                    }
                }

                if (contador == 0)
                {
                    MessageBox.Show("Por favor seleccione un honorario","Honorarios",MessageBoxButtons.OK,MessageBoxIcon.Information  );
                    return false;
                }

                
                dbugrRecuperacionHonorariosTemp.DataSource = listaHonorarios.Select(h => new { h.HOM_CODIGO,h.MED_NOMBRE_MEDICO, h.HOM_FACTURA_MEDICO,
                                h.HOM_VALOR_NETO,h.HOM_VALOR_PAGADO,h.HOM_COMISION_CLINICA, h.VALOR_POR_RECUPERAR,h.MED_CODIGO }).ToList();
                
                return true; 
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
        }

        //funcion para grabar una note de credito por el ingreso de una retencion
        private void grabarNotaDeCredito()
        { 

        }

        public void guardarFacturasCanceladas()
        {
            try
            {
                decimal valorPago = 0;
                decimal valorRecorte = 0;
                //foreach (DataGridViewRow item in dbgrdCanceladasTemp.Rows)
                foreach (UltraGridRow item in dbugrRecuperacionHonorariosTemp.Rows)
                {
                    valorPago = valorPago + Convert.ToDecimal(item.Cells["columnaRecuperado"].Value);
                    valorRecorte = valorRecorte + Convert.ToDecimal(item.Cells["columnaRecorte"].Text);
                    
                    if( Convert.ToDecimal(item.Cells["columnaRetencion"].Value)>0)
                    {
                        grabarNotaDeCredito();
                     }
                }
                PAGOS_FAC_MEDICOS pago = new PAGOS_FAC_MEDICOS();
                USUARIOS usuario = NegUsuarios.RecuperaUsuario(His.Entidades.Clases.Sesion.codUsuario);
                pago.USUARIOSReference.EntityKey = usuario.EntityKey;
                KeyValuePair<int, string> idFormaPago = (KeyValuePair<int, string>)cboCanFormaPago.SelectedItem;
                FORMA_PAGO forma = NegFormaPago.RecuperaFormaPagoID(Convert.ToInt16(idFormaPago.Key));
                pago.FORMA_PAGOReference.EntityKey    = forma.EntityKey ;
                pago.PAM_DESCRIPCION = rtbCanObsCancelacion.Text;
                pago.PAM_ESTADO = true;
                pago.PAM_FECHA = DateTime.Now;
                pago.PAM_VALOR = valorPago;
                pago.PAM_RECORTE = valorRecorte;
                pago.PAM_LOTE = txtCanLote.Text.ToString();
                pago.PAM_NUM_DOCUMENTO = txtCanNumeroPago.Text.ToString();
                pago.PAM_BANCO = txtCanBanco.Text.ToString();
                pago.PAM_FECHA_PAGO = dtpFechaPago.Value;

                NegPagosFacturasMedicos.CrearPagoFacturasMedicos(pago);
                foreach (UltraGridRow  item in dbugrRecuperacionHonorariosTemp.Rows) 
                {
                    if (Convert.ToInt64(item.Cells["HOM_CODIGO"].Value) > 0)
                    {
                        NegHonorariosMedicos negHonorarios = new NegHonorariosMedicos();
                        HONORARIOS_MEDICOS honorario = negHonorarios.RecuperaHonorariosMedicosID(Convert.ToInt64(item.Cells["HOM_CODIGO"].Value));
                        honorario.HOM_VALOR_PAGADO = honorario.HOM_VALOR_PAGADO + Convert.ToDecimal(item.Cells["columnaRecuperado"].Value);
                        honorario.HOM_RECORTE = Convert.ToDecimal(item.Cells["columnaRecorte"].Value);
                        //verifico que el honorario se haya recuperado por completo
                        if ((Convert.ToDecimal(item.Cells["HOM_VALOR_NETO"].Value) -
                            Convert.ToDecimal(item.Cells["HOM_COMISION_CLINICA"].Value) -
                            Convert.ToDecimal(item.Cells["columnaRecorte"].Value) -
                            Convert.ToDecimal(item.Cells["HOM_VALOR_PAGADO"].Value)) > 0)
                        {
                            honorario.HOM_ESTADO = 1;
                        }
                        else
                        {
                            honorario.HOM_ESTADO = 2;
                        }

                        negHonorarios.ActualizarHonorariosMedicos(honorario);
                        PAGOS_FAC_MEDICOS_DETALLE pagoDetalle = new PAGOS_FAC_MEDICOS_DETALLE();
                        pagoDetalle.HONORARIOS_MEDICOSReference.EntityKey = honorario.EntityKey;
                        pagoDetalle.PAGOS_FAC_MEDICOSReference.EntityKey = pago.EntityKey;
                        pagoDetalle.PMD_VALOR = Convert.ToDecimal(item.Cells["columnaRecuperado"].Value);
                        pagoDetalle.PMD_RECORTE = Convert.ToDecimal(item.Cells["columnaRecorte"].Value);
                        pagoDetalle.PMD_ESTADO = true;
                        NegPagosFacturasMedicos.CrearPagoFacturasMedicosDetalle(pagoDetalle);
                    }
                }
                //genero el reporte
                frmReportes frm = new frmReportes();
                frm.reporte = "rRecuperacionHonorarios1";
                frm.campo1 = pago.PAM_CODIGO.ToString();
                frm.Show();
            }
            catch (Exception  err)
            {
                MessageBox.Show(err.Message);  
            }
        }

    #endregion

        #region metodos pagos
        public void cargarGridPagos()
        {
            try
            {
                //declaro las variables para  los filtros
                string fechaDocIni = null;
                string fechaDocFin = null;
                string tipoFPcodigo = null;
                string fPcodigo = null;

                //filtro por fechas
                if (optPorDefecto.Checked == true)
                {
                    fechaDocFin = dtpHasta.Text;
                    fechaDocIni = dtpDesde.Text;
                }
                //filtro por tipo forma de pago
                if (cboFiltroTipoFormaPago.SelectedIndex > 0)
                {
                    KeyValuePair<int, string> tipoFormaPago = (KeyValuePair<int, string>)cboFiltroTipoFormaPago.SelectedItem;
                    tipoFPcodigo = tipoFormaPago.Key.ToString();
                    if (cboFiltroFormaPago.SelectedIndex >0)
                    {
                        KeyValuePair<int,string> formaPago  = (KeyValuePair<int,string>)cboFiltroFormaPago.SelectedItem;
                        fPcodigo = formaPago.Key.ToString();   
                    }
                }


                dbgrPagosFacMedicos.DataSource = NegPagosFacturasMedicos.RecuperarPagosFacturasMedicos(fechaDocIni, fechaDocFin, tipoFPcodigo, fPcodigo);
                cargarFormatoPagosIngresados();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error");
            }  
        }
        //public void cargarGridHonorariosTransferencias()
        //{
        //    try
        //    {
        //        dbugrHonorariosPorPagar.DataSource = NegHonorariosMedicos.RecuperarHonorariosMedicosPorTransferir();
        //    }
        //    catch (Exception err)
        //    {
        //        MessageBox.Show(err.Message, "Error");
        //    }  
        //}

        public void cargarFormatoPagosIngresados()
        {
            try {
                //Opciones por defecto de la grilla
                dbgrPagosFacMedicos.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                dbgrPagosFacMedicos.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                dbgrPagosFacMedicos.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                dbgrPagosFacMedicos.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center; 
                //Caracteristicas de Filtro en la grilla
                dbgrPagosFacMedicos.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                dbgrPagosFacMedicos.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                dbgrPagosFacMedicos.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                dbgrPagosFacMedicos.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
                //dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro";  
                dbgrPagosFacMedicos.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;  
                //Totalizo columnas
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Summaries.Clear();  
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Summaries.Add(Infragistics.Win.UltraWinGrid.SummaryType.Sum, dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_VALOR"]);
                //Añado la columna check
                if (sesion == false)
                {
                    dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns.Add("CHECKPAGO", "");
                    dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["CHECKPAGO"].DataType = typeof(bool);
                    dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["CHECKPAGO"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
                }

                //Edito el texto de las cabeceras
                //dbgrPagosMedicos.Columns["PAM_CODIGO"].HeaderText = "CODIGO";
                //dbgrPagosMedicos.Columns["PAM_NUM_DOCUMENTO"].HeaderText = "NUM. PAGO";
                //dbgrPagosMedicos.Columns["PAM_FECHA"].HeaderText = "FECHA REGISTRO";
                //dbgrPagosMedicos.Columns["PAM_FECHA_PAGO"].HeaderText = "FECHA PAGO";
                //dbgrPagosMedicos.Columns["PAM_DESCRIPCION"].HeaderText = "OBSERVACIONES";
                //dbgrPagosMedicos.Columns["FOR_DESCRIPCION"].HeaderText = "FORMA PAGO";
                //dbgrPagosMedicos.Columns["PAM_LOTE"].HeaderText = "LOTE";
                //dbgrPagosMedicos.Columns["PAM_BANCO"].HeaderText = "BANCO";
                //dbgrPagosMedicos.Columns["NOMBRE_USUARIO"].HeaderText = "RESPONSABLE";
                //dbgrPagosMedicos.Columns["PAM_VALOR"].HeaderText = "VALOR";

                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_CODIGO"].Header.Caption = "CODIGO";
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_NUM_DOCUMENTO"].Header.Caption = "NUM. PAGO";
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_FECHA"].Header.Caption = "FECHA REGISTRO";
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_FECHA_PAGO"].Header.Caption = "FECHA PAGO";
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_DESCRIPCION"].Header.Caption = "OBSERVACIONES";
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["FOR_DESCRIPCION"].Header.Caption = "FORMA PAGO";
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_LOTE"].Header.Caption = "LOTE";
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_BANCO"].Header.Caption = "BANCO";
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["NOMBRE_USUARIO"].Header.Caption = "RESPONSABLE";
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_VALOR"].Header.Caption = "VALOR";

                //Asigno un ancho predeterminado para las columnas del grid
                //dbgrPagosMedicos.Columns["pagosCheck"].Width = 30;
                //dbgrPagosMedicos.Columns["PAM_CODIGO"].Width = 50;
                //dbgrPagosMedicos.Columns["PAM_NUM_DOCUMENTO"].Width = 80;
                //dbgrPagosMedicos.Columns["PAM_FECHA"].Width = 120;
                //dbgrPagosMedicos.Columns["PAM_FECHA_PAGO"].Width = 70;
                //dbgrPagosMedicos.Columns["PAM_DESCRIPCION"].Width = 200;
                //dbgrPagosMedicos.Columns["FOR_DESCRIPCION"].Width = 120;
                //dbgrPagosMedicos.Columns["PAM_LOTE"].Width = 80;
                //dbgrPagosMedicos.Columns["PAM_BANCO"].Width = 110;
                //dbgrPagosMedicos.Columns["NOMBRE_USUARIO"].Width = 180;
                //dbgrPagosMedicos.Columns["PAM_VALOR"].Width = 70;

                //
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_CODIGO"].Width = 30;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_NUM_DOCUMENTO"].Width = 80;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_FECHA"].Width = 70;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_FECHA_PAGO"].Width = 70;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_DESCRIPCION"].Width = 200;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["FOR_DESCRIPCION"].Width = 120;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_LOTE"].Width = 80;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_BANCO"].Width = 110;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["NOMBRE_USUARIO"].Width = 180;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_VALOR"].Width = 100;
                //alineo las columas
                //dbgrPagosMedicos.Columns["PAM_CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dbgrPagosMedicos.Columns["PAM_NUM_DOCUMENTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dbgrPagosMedicos.Columns["PAM_FECHA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dbgrPagosMedicos.Columns["PAM_FECHA_PAGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dbgrPagosMedicos.Columns["PAM_DESCRIPCION"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dbgrPagosMedicos.Columns["FOR_DESCRIPCION"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dbgrPagosMedicos.Columns["PAM_LOTE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dbgrPagosMedicos.Columns["PAM_BANCO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dbgrPagosMedicos.Columns["NOMBRE_USUARIO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //dbgrPagosMedicos.Columns["PAM_VALOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_CODIGO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["FOR_DESCRIPCION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                //ordeno las Columnas
                //dbgrPagosMedicos.Columns["pagosCheck"].DisplayIndex = 1;
                //dbgrPagosMedicos.Columns["PAM_CODIGO"].DisplayIndex = 2;
                //dbgrPagosMedicos.Columns["PAM_NUM_DOCUMENTO"].DisplayIndex = 3;
                //dbgrPagosMedicos.Columns["PAM_FECHA"].DisplayIndex = 4;
                //dbgrPagosMedicos.Columns["PAM_FECHA_PAGO"].DisplayIndex = 5;
                //dbgrPagosMedicos.Columns["PAM_VALOR"].DisplayIndex = 6;
                //dbgrPagosMedicos.Columns["PAM_DESCRIPCION"].DisplayIndex = 7;
                //dbgrPagosMedicos.Columns["FOR_DESCRIPCION"].DisplayIndex = 8;
                //dbgrPagosMedicos.Columns["PAM_LOTE"].DisplayIndex = 9;
                //dbgrPagosMedicos.Columns["PAM_BANCO"].DisplayIndex = 10;
                //dbgrPagosMedicos.Columns["NOMBRE_USUARIO"].DisplayIndex = 11;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_CODIGO"].Header.VisiblePosition = 0;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_NUM_DOCUMENTO"].Header.VisiblePosition = 1;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_FECHA"].Header.VisiblePosition = 2;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_FECHA_PAGO"].Header.VisiblePosition = 3;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_VALOR"].Header.VisiblePosition = 4;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_DESCRIPCION"].Header.VisiblePosition = 5;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["FOR_DESCRIPCION"].Header.VisiblePosition = 6;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_LOTE"].Header.VisiblePosition = 7;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["PAM_BANCO"].Header.VisiblePosition = 8;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["NOMBRE_USUARIO"].Header.VisiblePosition = 9; 
                
                //oculto columnas
                //dbgrPagosMedicos.Columns["FOR_CODIGO"].Visible = false;
                //dbgrPagosMedicos.Columns["ID_USUARIO"].Visible = false;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["FOR_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["FOR_CODIGO"].Hidden = true;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["ID_USUARIO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["ID_USUARIO"].Hidden = true;
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);   
            }
        }
        #endregion

        #region metodos_transferencias
        public void guardarHonorariosCancelados()
        {
            try
            {
                //dbugrHonorariosPorPagar.DisplayLayout.Bands[0].Columns["MED_CODIGO"].SortIndicator = SortIndicator.Ascending;
                //usuario que ingreso el pago del honorario
                USUARIOS usuario = NegUsuarios.RecuperaUsuario(His.Entidades.Clases.Sesion.codUsuario);
                //
                TIPO_CANCELACION tipoPagoHonorario = (TIPO_CANCELACION)cboTipoTransferencia.SelectedItem; 
                //
                Int64 numeroDocumento=0;

                if(tipoPagoHonorario.TCA_CODIGO ==1)
                {
                    numeroDocumento = NegPagosHonorariosMedicos.RecuperarMaxNumeroTransferencia(); 
                }
                else
                {
                    numeroDocumento = Convert.ToInt64(txtTransCheque.Text);
                }
                Int32 codigoMedico = -1;
                decimal valorPago = 0;

                List<HONORARIOS_MEDICOS_TRANSFERENCIAS> listaDetalleTransferencias = new List<HONORARIOS_MEDICOS_TRANSFERENCIAS>();

                for (Int16 i = 0; i < dbugrHonorariosPorPagar.Rows.Count; i++)
                {
                    for (Int16 j = 0; j < dbugrHonorariosPorPagar.Rows[i].ChildBands[0].Rows.Count; j++)
                    {
                        if (Convert.ToBoolean(dbugrHonorariosPorPagar.Rows[i].ChildBands[0].Rows[j].Cells["CHECKTRANS"].Value) == true)
                        {
                            if (Convert.ToInt32(dbugrHonorariosPorPagar.Rows[i].ChildBands[0].Rows[j].Cells["MED_CODIGO"].Value) == codigoMedico)
                            {
                                valorPago = valorPago + Convert.ToDecimal(dbugrHonorariosPorPagar.Rows[i].ChildBands[0].Rows[j].Cells["VALOR_A_CANCELAR"].Value);
                                listaDetalleTransferencias.Add((HONORARIOS_MEDICOS_TRANSFERENCIAS)dbugrHonorariosPorPagar.Rows[i].ChildBands[0].Rows[j].ListObject);
                            }
                            else
                            {
                                if (codigoMedico != -1)
                                {
                                    guardarTransferenciaIndividual(listaDetalleTransferencias, numeroDocumento, valorPago, codigoMedico, tipoPagoHonorario, usuario);
                                    valorPago = Convert.ToDecimal(dbugrHonorariosPorPagar.Rows[i].ChildBands[0].Rows[j].Cells["VALOR_A_CANCELAR"].Value);
                                    listaDetalleTransferencias = new List<HONORARIOS_MEDICOS_TRANSFERENCIAS>();
                                    listaDetalleTransferencias.Add((HONORARIOS_MEDICOS_TRANSFERENCIAS)dbugrHonorariosPorPagar.Rows[i].ChildBands[0].Rows[j].ListObject);
                                    if (tipoPagoHonorario.TCA_CODIGO == 2)
                                        numeroDocumento++;

                                }
                                else
                                {
                                    codigoMedico = Convert.ToInt32(dbugrHonorariosPorPagar.Rows[i].ChildBands[0].Rows[j].Cells["MED_CODIGO"].Value);
                                    valorPago = valorPago + Convert.ToDecimal(dbugrHonorariosPorPagar.Rows[i].ChildBands[0].Rows[j].Cells["VALOR_A_CANCELAR"].Value);
                                    listaDetalleTransferencias.Add((HONORARIOS_MEDICOS_TRANSFERENCIAS)dbugrHonorariosPorPagar.Rows[i].ChildBands[0].Rows[j].ListObject);
                                }
                            }
                        }
                    }
                }
                //foreach (UltraGridRow item in dbugrHonorariosPorPagar.Rows)
                //{
                //    if ((bool)item.Cells["CHECKTRANS"].Value)
                //    {

                //    }
                    
                //}
                guardarTransferenciaIndividual(listaDetalleTransferencias, numeroDocumento, valorPago, codigoMedico, tipoPagoHonorario, usuario);
                if (tipoPagoHonorario.TCA_CODIGO == 1)
                {
                    frmArchivoBanco ventana = new frmArchivoBanco(formatoProdubanco);
                    ventana.Show();  
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public void guardarTransferenciaIndividual(List<HONORARIOS_MEDICOS_TRANSFERENCIAS> detalle,Int64 numeroDocumento,Decimal valorPago,Int32 codigoMedico,TIPO_CANCELACION tipoCancelacion, USUARIOS usuario)
        {
            try
            {
                MEDICOS medico = NegMedicos.RecuperaMedicoId(codigoMedico);
                CANCELACION_FAC_MEDICOS transferencia = new CANCELACION_FAC_MEDICOS();
                BANCOS banco = (BANCOS)cboTransBancos.SelectedItem;
                transferencia.CFM_BANCO = banco.BAN_NOMBRE;
                transferencia.CFM_CUENTA = txtCanCuenta.Text;
                transferencia.CFM_ESTADO = true;
                transferencia.CFM_FECHA = DateTime.Now;
                transferencia.CFM_NUMERO_DOCUMENTO = numeroDocumento;
                transferencia.CFM_OBSERVACION = rtbTransObservaciones.Text;
                transferencia.CFM_VALOR = valorPago;
                transferencia.MEDICOSReference.EntityKey = medico.EntityKey;
                transferencia.TIPO_CANCELACIONReference.EntityKey = tipoCancelacion.EntityKey;
                transferencia.USUARIOSReference.EntityKey = usuario.EntityKey;

                NegPagosHonorariosMedicos.CrearCancelacionHonorariosMedicos(transferencia);

                foreach (HONORARIOS_MEDICOS_TRANSFERENCIAS honorario in detalle)
                {
                    CANCELACION_FAC_MEDICOS_DETALLE transferenciaDetalle = new CANCELACION_FAC_MEDICOS_DETALLE();
                    NegHonorariosMedicos negHonorarios = new NegHonorariosMedicos();
                    HONORARIOS_MEDICOS honorarioMedico = negHonorarios.RecuperaHonorariosMedicosID(honorario.HOM_CODIGO);
                    honorarioMedico.HOM_VALOR_CANCELADO = honorarioMedico.HOM_VALOR_CANCELADO + honorario.VALOR_A_CANCELAR.Value;
                    negHonorarios.ActualizarHonorariosMedicos(honorarioMedico);

                    transferenciaDetalle.CANCELACION_FAC_MEDICOSReference.EntityKey = transferencia.EntityKey;
                    transferenciaDetalle.CFD_ESTADO = true;
                    transferenciaDetalle.CFD_VALOR = honorario.VALOR_A_CANCELAR.Value;
                    transferenciaDetalle.HONORARIOS_MEDICOSReference.EntityKey = honorarioMedico.EntityKey;
                    NegPagosHonorariosMedicos.CrearCancelacionHonorariosMedicosDetalle(transferenciaDetalle);
                }

                if (tipoCancelacion.TCA_CODIGO == 1)
                {
                    //variables
                    string nombreMedico;
                    string cuenta;
                    string deposito;
                    string fila;

                    nombreMedico = "P" + medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + "" + medico.MED_NOMBRE1
                                    + "" + medico.MED_NOMBRE2;
                    if(nombreMedico.Length>=41)   
                        nombreMedico = nombreMedico.Substring(1, 41); 
                    cuenta = String.Format("{0:00000000000}", medico.MED_CUENTA_CONTABLE.ToString());
                    deposito = valorPago.ToString("00000000000000");
                    fila = nombreMedico + Convert.ToChar(9) + cuenta + deposito + DateTime.Now.Year.ToString()+ DateTime.Now.Month.ToString()+DateTime.Now.Day.ToString() + "N1";
                    formatoProdubanco += fila + Convert.ToChar(13);
                }
                    else if(tipoCancelacion.TCA_CODIGO == 2)
                {
                    frmReportes frm = new frmReportes();
                    frm.reporte = "rCheque";
                    frm.campo1 = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + "" + medico.MED_NOMBRE1
                                    + "" + medico.MED_NOMBRE2;
                    frm.campo2 = valorPago.ToString(); 
                    frm.ShowDialog();
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);        
            }
        }

        #endregion

        #region eventos
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void cboEspecialidades_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void arbolMedicos_AfterSelect(object sender, TreeViewEventArgs e)
        {
            arbolMedicos.SelectedNode.Checked = true;

            // pestaña de facturas ingresadas
            if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["recuperacionCartera"])
            {
                filtrarFacturasIngresadasMedicos(arbolMedicos.SelectedNode);
                cargarFacturasMedicos(Parametros.HonorariosPAR.codigoTipoMovimientoHonorariosMedicos.ToString(), porRecuperar, medCodigo, espCodigo, tihCodigo, timCodigo, medRecibeLlamada, fechaIniFacturaMedico,
                        fechaFinFacturaMedico, honorariosCancelados, forCodigo, tifCodigo, lote, numeroControl, facturaClinica, FechaIniFacturaCliente,
                        FechaFinFacturaCliente, pacienteReferido, pacienteFechaAlta, ateCodigo, pacCodigo, dbugrFacturasIngresadas);

            }
                //pestaña de pagos realizados
            else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["listaRecuperacion"])
            {
                //cargarGridPagos();
            }
                //pestaña de valor por transferir
            else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["cancelacionHonorarios"])
            {
                //cargo el listado de honorarios medicos por cancelar
                if (arbolMedicos.SelectedNode.Tag.ToString() == "medico")
                    tipoTransferencia = "individual";
                else
                    tipoTransferencia = "multiple";
                //
                filtrarHonorariosPorTransferir(tipoTransferencia);
                dbugrdCancelacionHonorarios.DataSource = NegPagosHonorariosMedicos.RecuperarCancelacionHonorarioMedicos();
            }
            //pestaña de cancelaciones de Honorarios
            else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["listaCancelaciones"])
            {
                //
            }
                //pestaña de estado de cuentas
            else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["estadoCuentas"])
            {
                //
                filtrarFacturasIngresadasMedicos(arbolMedicos.SelectedNode);
                cargarFacturasMedicos(null, porRecuperar, medCodigo, espCodigo, tihCodigo, timCodigo, medRecibeLlamada, fechaIniFacturaMedico,
                        fechaFinFacturaMedico, honorariosCancelados, forCodigo, tifCodigo, lote, numeroControl, facturaClinica, FechaIniFacturaCliente,
                        FechaFinFacturaCliente, pacienteReferido, pacienteFechaAlta, ateCodigo, pacCodigo, dbugrEstadoCuentas);
            }

        }

        private void filtrarHonorariosPorTransferir(string tipo)
        {
            try {
                //variables de filtro
                string codigoMedico=null;
                string tipoFormaPagoId = null;
                string formaPagoId = null;
                string fechaFacturaIni = null;
                string fechaFacturaFin = null;
                bool conTransferencia = true;
                //Cargo Filtros
                //filtro los honorarios por medico
                if (tipo == "individual")
                    codigoMedico  = arbolMedicos.SelectedNode.Name;

                //filtro los honorarios por forma de pago
                if (cboFiltroTipoFormaPago.SelectedIndex > 0)
                {
                    KeyValuePair<int, string> tipoFormaPago = (KeyValuePair<int, string>)cboFiltroTipoFormaPago.SelectedItem;
                    tipoFormaPagoId = tipoFormaPago.Key.ToString();
                    if (cboFiltroFormaPago.SelectedIndex > 0)
                    {
                        KeyValuePair<int, string> formaPago = (KeyValuePair<int, string>)cboFiltroFormaPago.SelectedItem;
                        formaPagoId = formaPago.Key.ToString();
                    }
                }
                //fecha fatura medico
                if (optFacturaMedico.Checked == true)
                {
                    fechaFacturaFin = dtpHasta.Text;
                    fechaFacturaIni = dtpDesde.Text;
                }

                listaHonorariosMedicosTransferencias = NegHonorariosMedicos.RecuperarHonorariosMedicosPorTransferir(codigoMedico, tipoFormaPagoId, formaPagoId, fechaFacturaIni, fechaFacturaFin);
                if(cboTipoTransferencia.SelectedIndex >0)
                    conTransferencia = false; 

                var listaAgrupada = (from h in listaHonorariosMedicosTransferencias
                                     where h.MED_CON_TRANSFERENCIA == conTransferencia 
                                     group h by h.NOMBRE_MEDICO into grupo
                                     select new { MEDICO = grupo.Key, DETALLE = grupo }).ToList();
                dbugrHonorariosPorPagar.DataSource = listaAgrupada; 
                //dbugrHonorariosPorPagar.DataSource = NegHonorariosMedicos.RecuperarHonorariosMedicosPorTransferir(codigoMedico,tipoFormaPagoId, formaPagoId, fechaFacturaIni, fechaFacturaFin);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);   
            }
        }

        private void frmExploradorHonorarios_Load(object sender, EventArgs e)
        {

            //variable de sesion
            sesion = true;
            //cargo combos tipo de transferencia
            cboTipoTransferencia.DataSource = NegTipoCancelacion.RecuperarListaTipoCancelacion();
            cboTipoTransferencia.DisplayMember = "TCA_NOMBRE";
            cboTipoTransferencia.ValueMember = "TCA_CODIGO";
            //
            int deskHeight = Screen.PrimaryScreen.Bounds.Height;
            int deskWidth = Screen.PrimaryScreen.Bounds.Width;

            if (deskWidth >= 1024 && deskWidth < 1280) 
                flowLayoutPanelFiltros.BackgroundImage = Archivo.fondoA1x1024x73;
            else if (deskWidth >= 1280 && deskWidth < 1360)
                flowLayoutPanelFiltros.BackgroundImage = Archivo.fondoA1x1280x92;
            else if (deskWidth >= 1360 && deskWidth < 1600)
                flowLayoutPanelFiltros.BackgroundImage = Archivo.fondoA1x1360x97;


            //inicalizo el control que aloja al loading
            host = new ElementHost();
            
        }

        public void mostrarMensajeEspera()
        {
            GeneralApp.ControlesWPF.LoadingAnimation loading = new GeneralApp.ControlesWPF.LoadingAnimation(); 
            host.Dock = DockStyle.Fill;
            host.BackColorTransparent = true;
            host.Child = loading;
            flowLayoutPanelFiltros.Controls.Add(host);
            host.Show();
        }
        public void cerrarMensajeEspera()
        {
            host.Child = null ;  
        }

        //quito el check de los ToolStripMenuItem
        private void tssMedicos_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            foreach (ToolStripMenuItem item in tssMedicos.DropDownItems)
            {
                item.Checked = false;
            }
        }

        //. cargo el arbol de los medicos por tipo honorario
        private void tipoHonorarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cargarArbolMedicos("tipo_honorario");
        }
        //. cargo el arbol de los medicos por especialidad
        private void especialidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cargarArbolMedicos("tipo_especialidad");
        }
        //. cargo el arbol de los medicos por medico
        private void tipoMedicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cargarArbolMedicos("tipo_medico");
        }
        //. cargo el arbol de los medicos por llamada
        private void tipoLlamadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cargarArbolMedicos("tipo_llamada");
        }
     #endregion

        //private void btnOcultarInfMedico_Click(object sender, EventArgs e)
        //{
        //    if (MarcoDetalle.Panel2Collapsed == true)
        //    {
        //        MarcoDetalle.Panel2Collapsed = false;  
        //    }
        //    else if (MarcoDetalle.SplitterDistance > 15)
        //    {
        //        MarcoDetalle.SplitterDistance = 15;
        //    }
        //    else
        //    {
        //        MarcoDetalle.SplitterDistance = 160;
        //        MarcoDetalle.Panel2Collapsed = true;  
        //    }
            
        //}
        
        //Muestro el panel de cancelacion de las facturas medicos
        private void btnPagosClinica_Click(object sender, EventArgs e)
        {
            if (panelCanceladasTemp.Visible == false)
            {
                panelCanceladasTemp.Visible = true;
                if (cargarFacturasParaCancelar() == true)
                {
                    txtCanNumeroPago.Text ="";
                    txtCanBanco.Text ="";
                    txtCanLote.Text ="";
                    rtbCanObsCancelacion.Text = "";
                    tableLayoutPanelCancelacionFac.ColumnStyles[0].Width = 50;
                    tableLayoutPanelCancelacionFac.ColumnStyles[1].Width = 50;
                    panelCanceladasTemp.Visible = true;
                }
                else
                {
                    panelCanceladasTemp.Visible = false;
                }
            }
            else
            {
                tableLayoutPanelCancelacionFac.ColumnStyles[0].Width = 100;
                tableLayoutPanelCancelacionFac.ColumnStyles[1].Width = 0;
                panelCanceladasTemp.Visible = false;
            }
        }

        // Marco todos los checks de la Grilla de facturas ingresadas
        private void lnlbTodos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (UltraGridRow  item in dbugrFacturasIngresadas.Rows)
            {
                item.Cells["columnaCheck"].Value = true;
            }
        }
        // Desmarco todos los checks de la Grilla de facturas ingresadas
        private void lnlblNinguna_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (UltraGridRow item in dbugrFacturasIngresadas.Rows)
            {
                item.Cells["columnaCheck"].Value = false;  
            }
        }



        //. Permite crear un nuevo 
        private void toolStripButtonNuevo_Click(object sender, EventArgs e)
        {
            
        }

        private void rtbObsCancelacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        // Cargo la Forma de Pago de acuerdo al tipo
        private void cboCanTipoFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                KeyValuePair<int, string> item = (KeyValuePair<int, string>)cboCanTipoFormaPago.SelectedItem;
                cargarFormaPago(cboCanFormaPago, Convert.ToInt16(item.Key));
                Int16 codigo = (Int16)(item.Key);
                foreach (int cod in HonorariosPAR.getCodigoTarjetasCredito())
                {
                    if (codigo == cod)
                    {
                        txtCanLote.Enabled = true;
                        return; 
                    }
                }

                txtCanLote.Enabled = false;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);  
            }

        }
        // Cargo la Forma de Pago de acuerdo al tipo
        private void cboFiltroTipoFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(cboFiltroTipoFormaPago.SelectedIndex >0) 
                {
                    KeyValuePair<int, string> item = (KeyValuePair<int, string>)cboFiltroTipoFormaPago.SelectedItem;
                    Int16 codigo = (Int16)(item.Key);
                    cboFiltroFormaPago.Visible = true;
                    lbFiltroFormaPago.Visible = true;
                    //cboFiltroFormaPago.Items.Add("Todas");
                    cargarFormaPago(cboFiltroFormaPago, codigo);
                    //cboCanTipoFormaPago.SelectedIndex = cboFiltroTipoFormaPago.SelectedIndex - 1; 

                    foreach (int cod in HonorariosPAR.getCodigoTarjetasCredito())
                    {
                        if (codigo == cod)
                        {
                            lblLote.Visible = true;
                            txtLote.Visible = true;
                            return;
                        }
                    }

                    lblLote.Visible = false;
                    txtLote.Visible = false;

                }
                else
                {
                    cboFiltroFormaPago.Visible = false;
                    lbFiltroFormaPago.Visible = false;
                    lblLote.Visible = false;
                    txtLote.Visible = false;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message,"error");   
            }
        }

        //Guardo los cambios en la pestaña activa
        private void toolStripButtonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //pestaña de ingreso de facturas
                if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["recuperacionCartera"])
                {
                    if (tableLayoutPanelCancelacionFac.ColumnStyles[1].Width > 0)
                    {
                        guardarFacturasCanceladas();
                        tableLayoutPanelCancelacionFac.ColumnStyles[0].Width = 100;
                        tableLayoutPanelCancelacionFac.ColumnStyles[1].Width = 0;
                        dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["columnaCheck"].Hidden = true;
                        dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["VALOR_POR_RECUPERAR"].Hidden = true;
                        dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["VALOR_POR_RECUPERAR"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                        panelCanceladasTemp.Visible = false;
                        MessageBox.Show("La información se guardo existosamente", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        nuevoRecuperacionHonorarios = false;
                    }
                }
                //pestaña de ingreso de facturas
                else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["cancelacionHonorarios"])
                {

                    guardarHonorariosCancelados();
                    List<HONORARIOS_MEDICOS_TRANSFERENCIAS> listaHonrariosMedicos = NegHonorariosMedicos.RecuperarHonorariosMedicosPorTransferir();
                    var listaAgrupada = (from h in listaHonrariosMedicos 
                                        group h by h.NOMBRE_MEDICO into grupo
                                        select grupo).ToList();
                    dbugrHonorariosPorPagar.DataSource = listaAgrupada; 
                    txtCanCuenta.Text = "";
                    txtTransCheque.Text = "";
                    rtbTransObservaciones.Text = "";
                    txtTransCheque.Text = "";
                    dbugrdCancelacionHonorarios.DataSource = NegPagosHonorariosMedicos.RecuperarCancelacionHonorarioMedicos();
                    MessageBox.Show("La información se guardo existosamente", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //ejecuto el evento cancelar para reiniciar los controles
                toolStripButtonCancelar_Click(null, null);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error  );
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                e.Handled = true;
        }


        //private void dbgrdCanceladasTemp_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    Decimal sum = 0;
        //    if (tableLayoutPanelCancelacionFac.ColumnStyles[1].Width > 0)
        //    {
        //        if (e.ColumnIndex  == 0)
        //        {
        //            for (int i = 0; i < this.dbgrdCanceladasTemp.RowCount; i++)
        //            {
        //                Decimal cellValue = 0;
        //                Decimal.TryParse(this.dbgrdCanceladasTemp[0, i].FormattedValue.ToString(), out cellValue);
        //                //
        //                if (cellValue == 0)
        //                    dbgrdCanceladasTemp[0, i].Value = 0;
        //                //
        //                sum += cellValue;
        //            }

        //            txtTotalCanceladas.Text = sum.ToString();
        //        }
        //    }
        //}

        private void lnlbMedicos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Si el panel de informacion personalizada del medico esta cerrada
            if (marcoInfMedicos.Panel1Collapsed == true)
            {
                MarcoDetalle.Panel2Collapsed = true;
                cargarInfMedicos();
            }
            else {
                MarcoDetalle.Panel2Collapsed = false;
                MarcoDetalle.SplitterDistance = 160;
                MarcoDetalle.Panel1MinSize = 15;
            }
        }

        private void arbolMedicos_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (arbolMedicos.SelectedNode != null)
                {
                    if (arbolMedicos.SelectedNode != e.Node)
                    {
                        arbolMedicos.SelectedNode = e.Node ;
                    }
                }
                else
                {
                    arbolMedicos.SelectedNode = e.Node;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);  
            }
        }

        private void arbolMedicos_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (arbolMedicos.SelectedNode!=null)
                arbolMedicos.SelectedNode.Checked =false;   
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                actualizando = true;
                if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["recuperacionCartera"])
                {
                    if (arbolMedicos.Nodes[0].Checked == true)
                    {
                        filtrarFacturasIngresadasMedicos(arbolMedicos.Nodes[0]);
                    }
                    else
                    {
                        TreeNode nodo = buscarNodo(arbolMedicos.Nodes[0]);
                        if (nodo != null)
                            filtrarFacturasIngresadasMedicos(nodo);
                        else
                            filtrarFacturasIngresadasMedicos(arbolMedicos.Nodes[0]);
                    }
                    cargarFacturasMedicos(Parametros.HonorariosPAR.codigoTipoMovimientoHonorariosMedicos.ToString(), porRecuperar, medCodigo, espCodigo, tihCodigo, timCodigo, medRecibeLlamada, fechaIniFacturaMedico,
                        fechaFinFacturaMedico, honorariosCancelados, forCodigo, tifCodigo, lote, numeroControl, facturaClinica, FechaIniFacturaCliente,
                        FechaFinFacturaCliente, pacienteReferido, pacienteFechaAlta, ateCodigo, pacCodigo, dbugrFacturasIngresadas);
                }
                //pestaña de pagos realizados
                else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["listaRecuperacion"])
                {
                    cargarGridPagos();
                }
                //pestaña de valor por transferir
                else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["cancelacionHonorarios"])
                {
                    if (arbolMedicos.SelectedNode != null)
                    {
                        //cargo el listado de honorarios medicos por cancelar
                        if (arbolMedicos.SelectedNode.Tag.ToString() == "medico")
                            tipoTransferencia = "individual";
                        else
                            tipoTransferencia = "multiple";
                    }
                    //
                    filtrarHonorariosPorTransferir(tipoTransferencia);
                }
                //pestaña de honorarios cancelados
                else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["listaCancelaciones"])
                {
                    dbugrdCancelacionHonorarios.DataSource = NegPagosHonorariosMedicos.RecuperarCancelacionHonorarioMedicos();
                }
                //pestaña de estado de cuentas
                else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["estadoCuentas"])
                {
                    if (arbolMedicos.Nodes[0].Checked == true)
                    {
                        filtrarFacturasIngresadasMedicos(arbolMedicos.Nodes[0]);
                    }
                    else
                    {
                        TreeNode nodo = buscarNodo(arbolMedicos.Nodes[0]);
                        if (nodo != null)
                            filtrarFacturasIngresadasMedicos(nodo);
                        else
                            filtrarFacturasIngresadasMedicos(arbolMedicos.Nodes[0]);
                    }
                    cargarFacturasMedicos(null, porRecuperar, medCodigo, espCodigo, tihCodigo, timCodigo, medRecibeLlamada, fechaIniFacturaMedico,
                        fechaFinFacturaMedico, honorariosCancelados, forCodigo, tifCodigo, lote, numeroControl, facturaClinica, FechaIniFacturaCliente,
                        FechaFinFacturaCliente, pacienteReferido, pacienteFechaAlta, ateCodigo, pacCodigo, dbugrEstadoCuentas);
                }
                actualizando = false;
            }
            catch (Exception err)
            {
                actualizando = false;
                MessageBox.Show(err.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);  
            }

        }

        private void txtFilFacturaClinica_TextChanged(object sender, EventArgs e)
        {

        }

        private void grpFiltroFechas_Enter(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void grpModo_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkTransDetalle_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void cboCanFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            KeyValuePair<int, string> item = (KeyValuePair<int, string>)cboCanFormaPago.SelectedItem;
            Int16 codigo = (Int16)(item.Key);
            if (codigo == HonorariosPAR.getCodigoFormaPagoCheque())
            {
                txtCanBanco.Enabled = true; 
            }
            else
            {
                txtCanBanco.Enabled = false; 
            }
        }

        //actualizo la aplicacion de acuerdo a la tarea a realizarse 
        private void tabHonorariosDetalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["recuperacionCartera"])
            {
                cargarFiltros("recuperacionCartera");
            }
            else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["listaRecuperacion"])
            {
                cargarFiltros("listaRecuperacion");
            }
            else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["cancelacionHonorarios"])
            {
                cargarFiltros("cancelacionHonorarios");
            }
            else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["listaCancelaciones"])
            {
                cargarFiltros("listaCancelaciones");
            }
            else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["estadoCuentas"])
            {
                cargarFiltros("estadoCuentas");
            }

        }



        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cboTipoTransferencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listaHonorariosMedicosTransferencias != null)
            {
                bool conTransferencia = true;
                if (cboTipoTransferencia.SelectedIndex == 1)
                {
                    conTransferencia = false;
                    var listaAgrupada = (from h in listaHonorariosMedicosTransferencias
                                         where h.MED_CON_TRANSFERENCIA == conTransferencia
                                         group h by h.NOMBRE_MEDICO into grupo
                                         select new { MEDICO = grupo.Key, DETALLE = grupo }).ToList();
                    dbugrHonorariosPorPagar.DataSource = listaAgrupada;
                    txtTransCheque.Enabled = true;
                    txtTransCheque.Enabled = true;
                }
                else
                {
                    var listaAgrupada = (from h in listaHonorariosMedicosTransferencias
                                         where h.MED_CON_TRANSFERENCIA == conTransferencia
                                         group h by h.NOMBRE_MEDICO into grupo
                                         select new { MEDICO = grupo.Key, DETALLE = grupo }).ToList();
                    dbugrHonorariosPorPagar.DataSource = listaAgrupada;
                    txtTransCheque.Enabled = false;
                    txtTransCheque.Enabled = false;
                }
            }
        }

        private void dbgrPagosFacMedicos_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dbgrPagosFacMedicos.ActiveRow != null)
                {
                    Int64 codigo = Convert.ToInt64(dbgrPagosFacMedicos.ActiveRow.Cells["PAM_CODIGO"].Text);
                    dbgrPagosMedicosDetalle.DataSource = NegPagosFacturasMedicos.RecuperarPagosFacturasMedicosDetalle(codigo);
                    cargarFormatoRecuperacionMedicosDetalle();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dbugrEstadoCuentas_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
        //    UltraGridBand bandUno = dbugrEstadoCuentas.DisplayLayout.Bands[0];


         

        //    dbugrEstadoCuentas.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
        //    dbugrEstadoCuentas.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
        //    dbugrEstadoCuentas.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
        //    dbugrEstadoCuentas.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
        //    bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
        //    //Caracteristicas de Filtro en la grilla
        //    dbugrEstadoCuentas.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
        //    dbugrEstadoCuentas.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
        //    dbugrEstadoCuentas.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
        //    dbugrEstadoCuentas.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
        //    dbugrEstadoCuentas.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
        //    //
        //    dbugrEstadoCuentas.DisplayLayout.UseFixedHeaders = true;
        //    //Cambio la apariencia de las sumas
        //    bandUno.Summaries.Clear();
        //    bandUno.SummaryFooterCaption = "Totales: ";
        //    bandUno.Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
        //    bandUno.Override.SummaryFooterCaptionAppearance.BackColor = Color.Silver;
        //    bandUno.Override.SummaryFooterCaptionAppearance.ForeColor = Color.LightYellow;
        //    //totalizo las columnas
        //    SummarySettings sumHonorarios = bandUno.Summaries.Add("Honorarios", SummaryType.Sum, bandUno.Columns["HOM_VALOR_NETO"]);
        //    //sumHonorarios.DisplayFormat = "Tot = {0:#####.00}";
        //    sumHonorarios.DisplayFormat = "{0:#####.00}";
        //    sumHonorarios.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    SummarySettings sumComision = bandUno.Summaries.Add("Comision", SummaryType.Sum, bandUno.Columns["HOM_COMISION_CLINICA"]);
        //    //sumComision.DisplayFormat = "Tot = {0:#####.00}";
        //    sumComision.DisplayFormat = "{0:#####.00}";
        //    sumComision.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    SummarySettings sumReferido = bandUno.Summaries.Add("Referido", SummaryType.Sum, bandUno.Columns["HOM_APORTE_LLAMADA"]);
        //    //sumReferido.DisplayFormat = "Tot = {0:#####.00}";
        //    sumReferido.DisplayFormat = "{0:#####.00}";
        //    sumReferido.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

        //    SummarySettings sumRetenido = bandUno.Summaries.Add("Retenido", SummaryType.Sum, bandUno.Columns["HOM_RETENCION"]);
        //    sumRetenido.DisplayFormat = "{0:#####.00}";
        //    sumRetenido.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

        //    SummarySettings sumValorPagar = bandUno.Summaries.Add("Valor a pagar", SummaryType.Sum, bandUno.Columns["HOM_VALOR_TOTAL"]);
        //    sumValorPagar.DisplayFormat = "{0:#####.00}";
        //    sumValorPagar.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

        //    SummarySettings sumValorRecuperado = bandUno.Summaries.Add("Valor Recuperado", SummaryType.Sum, bandUno.Columns["HOM_VALOR_PAGADO"]);
        //    sumValorRecuperado.DisplayFormat = "{0:#####.00}";
        //    sumValorRecuperado.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

        //    SummarySettings sumValorPorRecuperar = bandUno.Summaries.Add("Valor Por Recuperar", SummaryType.Sum, bandUno.Columns["VALOR_POR_RECUPERAR"]);
        //    sumValorPorRecuperar.DisplayFormat = "{0:#####.00}";
        //    sumValorPorRecuperar.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

        //    SummarySettings sumValorCancelado = bandUno.Summaries.Add("Valor Cancelado", SummaryType.Sum, bandUno.Columns["HOM_VALOR_CANCELADO"]);
        //    sumValorCancelado.DisplayFormat = "{0:#####.00}";
        //    sumValorCancelado.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

        //    SummarySettings sumValorSALDO = bandUno.Summaries.Add("SALDO", SummaryType.Sum, bandUno.Columns["VALOR_RECUPERADO"]);
        //    sumValorSALDO.DisplayFormat = "{0:#####.00}";
        //    sumValorSALDO.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

        //    SummarySettings sumValorPORPAGAR = bandUno.Summaries.Add("SALDO POR PAGAR", SummaryType.Sum, bandUno.Columns["DIFERENCIA"]);
        //    sumValorPORPAGAR.DisplayFormat = "{0:#####.00}";
        //    sumValorPORPAGAR.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

     
        //    //Cambio el nombre de las cabeceras
        //    bandUno.Columns["MED_CODIGO"].Header.Caption = "CODIGO";
        //    bandUno.Columns["MED_NOMBRE_MEDICO"].Header.Caption = "MEDICO";
        //    bandUno.Columns["HOM_FACTURA_MEDICO"].Header.Caption = "FACTURA DEL MEDICO";
        //    bandUno.Columns["HOM_FACTURA_FECHA"].Header.Caption = "FEC. FACTURA";
        //    bandUno.Columns["PAC_NOMBRE_PACIENTE"].Header.Caption = "PACIENTE";
        //    bandUno.Columns["ATE_NUMERO_CONTROL"].Header.Caption = "NUMERO DE CONTROL";
        //    bandUno.Columns["ATE_FACTURA_PACIENTE"].Header.Caption = "FACTURA PACIENTE";
        //    bandUno.Columns["ATE_FACTURA_FECHA"].Header.Caption = "FEC. FACTURA";
        //    bandUno.Columns["FOR_DESCRIPCION"].Header.Caption = "FORMA DE PAGO";
        //    bandUno.Columns["HOM_LOTE"].Header.Caption = "LOTE";
        //    bandUno.Columns["RET_CODIGO1"].Header.Caption = "NUM. RETENCION";
        //    bandUno.Columns["HOM_VALOR_NETO"].Header.Caption = "HONORARIO";
        //    bandUno.Columns["HOM_COMISION_CLINICA"].Header.Caption = "COMISION CLINICA";
        //    bandUno.Columns["HOM_APORTE_LLAMADA"].Header.Caption = "REFERIDO";
        //    bandUno.Columns["HOM_RETENCION"].Header.Caption = "VALOR RETENCION";
        //    bandUno.Columns["HOM_VALOR_TOTAL"].Header.Caption = "VALOR A PAGAR";
        //    bandUno.Columns["HOM_VALOR_PAGADO"].Header.Caption = "VALOR RECUPERADO";
        //    bandUno.Columns["VALOR_POR_RECUPERAR"].Header.Caption = "SALDO";
        //    bandUno.Columns["HOM_VALOR_CANCELADO"].Header.Caption = "VALOR CANCELADO";
        //    bandUno.Columns["HOM_OBSERVACION"].Header.Caption = "OBSERVACION";
        //    bandUno.Columns["VALOR_RECUPERADO"].Header.Caption = "SALDO";
        //    bandUno.Columns["DIFERENCIA"].Header.Caption = "SALDO POR PAGAR";

        //    //modifico el ancho por defecto de las columnas
        //    bandUno.Columns["MED_CODIGO"].Width = 50;
        //    bandUno.Columns["MED_NOMBRE_MEDICO"].Width = 200;
        //    bandUno.Columns["HOM_FACTURA_MEDICO"].Width = 100;
        //    bandUno.Columns["HOM_FACTURA_FECHA"].Width = 70;
        //    bandUno.Columns["PAC_NOMBRE_PACIENTE"].Width = 200;
        //    bandUno.Columns["ATE_NUMERO_CONTROL"].Width = 80;
        //    bandUno.Columns["ATE_FACTURA_PACIENTE"].Width = 100;
        //    bandUno.Columns["ATE_FACTURA_FECHA"].Width = 70;
        //    bandUno.Columns["FOR_DESCRIPCION"].Width = 120;
        //    bandUno.Columns["HOM_LOTE"].Width = 80;
        //    bandUno.Columns["RET_CODIGO1"].Width = 100;
        //    bandUno.Columns["HOM_VALOR_NETO"].Width = 100;
        //    bandUno.Columns["HOM_COMISION_CLINICA"].Width = 100;
        //    bandUno.Columns["HOM_APORTE_LLAMADA"].Width = 100;
        //    bandUno.Columns["HOM_RETENCION"].Width = 100;
        //    bandUno.Columns["HOM_VALOR_TOTAL"].Width = 100;
        //    bandUno.Columns["HOM_VALOR_PAGADO"].Width = 100;
        //    bandUno.Columns["VALOR_POR_RECUPERAR"].Width = 100;
        //      bandUno.Columns["HOM_VALOR_CANCELADO"].Width = 100;
        //    bandUno.Columns["HOM_OBSERVACION"].Width = 250;
        //    bandUno.Columns["VALOR_RECUPERADO"].Width = 100;
        //    bandUno.Columns["DIFERENCIA"].Width = 100; 

        //    //alineo las columnas
        //    bandUno.Columns["MED_CODIGO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    bandUno.Columns["MED_NOMBRE_MEDICO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
        //    bandUno.Columns["HOM_FACTURA_MEDICO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    bandUno.Columns["HOM_FACTURA_FECHA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
        //    bandUno.Columns["PAC_NOMBRE_PACIENTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
        //    bandUno.Columns["ATE_NUMERO_CONTROL"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    bandUno.Columns["ATE_FACTURA_PACIENTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    bandUno.Columns["ATE_FACTURA_FECHA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
        //    bandUno.Columns["FOR_DESCRIPCION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
        //    bandUno.Columns["HOM_LOTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    bandUno.Columns["RET_CODIGO1"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
        //    bandUno.Columns["HOM_VALOR_NETO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    bandUno.Columns["HOM_COMISION_CLINICA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    bandUno.Columns["HOM_APORTE_LLAMADA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    bandUno.Columns["HOM_RETENCION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    bandUno.Columns["HOM_VALOR_TOTAL"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    bandUno.Columns["HOM_VALOR_PAGADO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    bandUno.Columns["VALOR_POR_RECUPERAR"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //      bandUno.Columns["HOM_VALOR_CANCELADO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    bandUno.Columns["HOM_OBSERVACION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    bandUno.Columns["VALOR_RECUPERADO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    bandUno.Columns["DIFERENCIA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
        //    //ordeno las columnas
        //    bandUno.Columns["MED_CODIGO"].Header.VisiblePosition = 1;
        //    bandUno.Columns["MED_NOMBRE_MEDICO"].Header.VisiblePosition = 2;
        //    bandUno.Columns["HOM_FACTURA_MEDICO"].Header.VisiblePosition = 3;
        //    bandUno.Columns["HOM_FACTURA_FECHA"].Header.VisiblePosition = 4;
        //    bandUno.Columns["PAC_NOMBRE_PACIENTE"].Header.VisiblePosition = 5;
        //    bandUno.Columns["ATE_NUMERO_CONTROL"].Header.VisiblePosition = 6;
        //    bandUno.Columns["ATE_FACTURA_PACIENTE"].Header.VisiblePosition = 7;
        //    bandUno.Columns["ATE_FACTURA_FECHA"].Header.VisiblePosition = 8;
        //    bandUno.Columns["FOR_DESCRIPCION"].Header.VisiblePosition = 9;
        //    bandUno.Columns["HOM_LOTE"].Header.VisiblePosition = 10;
        //    bandUno.Columns["RET_CODIGO1"].Header.VisiblePosition = 11;
        //    bandUno.Columns["HOM_VALOR_NETO"].Header.VisiblePosition = 12;
        //    bandUno.Columns["HOM_COMISION_CLINICA"].Header.VisiblePosition = 13;
        //    bandUno.Columns["HOM_APORTE_LLAMADA"].Header.VisiblePosition = 14;
        //    bandUno.Columns["HOM_RETENCION"].Header.VisiblePosition = 15;
        //    bandUno.Columns["HOM_VALOR_TOTAL"].Header.VisiblePosition = 16;
        //    bandUno.Columns["VALOR_RECUPERADO"].Header.VisiblePosition = 17;
        //    bandUno.Columns["VALOR_POR_RECUPERAR"].Header.VisiblePosition = 18;
        //     bandUno.Columns["HOM_VALOR_PAGADO"].Header.VisiblePosition = 19;
        //    bandUno.Columns["HOM_VALOR_CANCELADO"].Header.VisiblePosition = 20;
        //    bandUno.Columns["HOM_OBSERVACION"].Header.VisiblePosition = 22;
         
        //    bandUno.Columns["DIFERENCIA"].Header.VisiblePosition = 21;

        //    //Cambio el color de las columnas
        //    string[] infMedico = new string[4] { "MED_CODIGO", "MED_NOMBRE_MEDICO", "HOM_FACTURA_MEDICO", "HOM_FACTURA_FECHA" };
        //    string[] infPaciente = new string[6] { "PAC_NOMBRE_PACIENTE", "ATE_NUMERO_CONTROL", "ATE_FACTURA_PACIENTE", "ATE_FACTURA_FECHA", "FOR_DESCRIPCION", "HOM_LOTE" };
        //    string[] infHonorarios = new string[12] { "RET_CODIGO1", "HOM_VALOR_NETO", "HOM_COMISION_CLINICA", "HOM_APORTE_LLAMADA", "HOM_RETENCION", "HOM_VALOR_TOTAL", "HOM_VALOR_PAGADO", "VALOR_POR_RECUPERAR", "HOM_RECORTE", "HOM_OBSERVACION", "DIFERENCIA", "VALOR_RECUPERADO"};

        //    foreach (string item in infMedico)
        //    {
        //        //bandUno.Columns["MED_NOMBRE_MEDICO"].CellAppearance.AlphaLevel = 125;
        //        bandUno.Columns[item].CellAppearance.BackColor2 = Color.White;
        //        bandUno.Columns[item].CellAppearance.BackColor = Color.Silver;
        //        //bandUno.Columns[item].CellAppearance.BackColor = Color.DarkGray;
        //        bandUno.Columns[item].CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
        //    }
        //    foreach (string item in infPaciente)
        //    {
        //        //bandUno.Columns[item].CellAppearance.BackColor2 = Color.LightCyan;
        //        bandUno.Columns[item].CellAppearance.BackColor = Color.LightCyan;
        //        //bandUno.Columns[item].CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
        //    }
        //    foreach (string item in infHonorarios)
        //    {
        //        bandUno.Columns[item].CellAppearance.BackColor = Color.LightSteelBlue;
        //        //bandUno.Columns[item].CellAppearance.BackColor = Color.SlateGray;
        //        //bandUno.Columns[item].CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
        //    }

        //    //
        //    bandUno.Columns["MED_CODIGO"].Header.Fixed = true;
        //    bandUno.Columns["MED_NOMBRE_MEDICO"].Header.Fixed = true;
        //    //Oculto columnas 
        //    bandUno.Columns["VALOR_POR_RECUPERAR"].Hidden = true;
        //    bandUno.Columns["HOM_CODIGO"].Hidden = true;
        //    bandUno.Columns["ATE_CODIGO"].Hidden = true;
        //    bandUno.Columns["FOR_CODIGO"].Hidden = true;
        //    bandUno.Columns["ID_USUARIO"].Hidden = true;
        //    bandUno.Columns["HOM_FECHA_INGRESO"].Hidden = true;
        //    bandUno.Columns["MED_RUC"].Hidden = true;

        //    //excluyó columnas no visibles de la seleccion
        //    bandUno.Columns["HOM_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
        //    bandUno.Columns["ATE_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
        //    bandUno.Columns["FOR_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
        //    bandUno.Columns["ID_USUARIO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
        //    bandUno.Columns["HOM_FECHA_INGRESO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
        //    bandUno.Columns["MED_RUC"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
        //
            UltraGridBand bandUno = dbugrEstadoCuentas.DisplayLayout.Bands[0];

            dbugrEstadoCuentas.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            dbugrEstadoCuentas.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
            dbugrEstadoCuentas.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            dbugrEstadoCuentas.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            //Caracteristicas de Filtro en la grilla
            dbugrEstadoCuentas.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            dbugrEstadoCuentas.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            dbugrEstadoCuentas.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            dbugrEstadoCuentas.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
            dbugrEstadoCuentas.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
            //
            dbugrEstadoCuentas.DisplayLayout.UseFixedHeaders = true;
            //Cambio la apariencia de las sumas
            bandUno.Summaries.Clear();
            bandUno.SummaryFooterCaption = "Totales: ";
            bandUno.Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
            bandUno.Override.SummaryFooterCaptionAppearance.BackColor = Color.Silver;
            bandUno.Override.SummaryFooterCaptionAppearance.ForeColor = Color.LightYellow;
            //totalizo las columnas
            SummarySettings sumHonorarios = bandUno.Summaries.Add("Honorarios", SummaryType.Sum, bandUno.Columns["HOM_VALOR_NETO"]);
            //sumHonorarios.DisplayFormat = "Tot = {0:#####.00}";
            sumHonorarios.DisplayFormat = "{0:#####.00}";
            sumHonorarios.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
            SummarySettings sumComision = bandUno.Summaries.Add("Comision", SummaryType.Sum, bandUno.Columns["HOM_COMISION_CLINICA"]);
            //sumComision.DisplayFormat = "Tot = {0:#####.00}";
            sumComision.DisplayFormat = "{0:#####.00}";
            sumComision.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
            SummarySettings sumReferido = bandUno.Summaries.Add("Referido", SummaryType.Sum, bandUno.Columns["HOM_APORTE_LLAMADA"]);
            //sumReferido.DisplayFormat = "Tot = {0:#####.00}";
            sumReferido.DisplayFormat = "{0:#####.00}";
            sumReferido.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            SummarySettings sumRetenido = bandUno.Summaries.Add("Retenido", SummaryType.Sum, bandUno.Columns["HOM_RETENCION"]);
            sumRetenido.DisplayFormat = "{0:#####.00}";
            sumRetenido.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            SummarySettings sumValorPagar = bandUno.Summaries.Add("Valor a pagar", SummaryType.Sum, bandUno.Columns["HOM_VALOR_TOTAL"]);
            sumValorPagar.DisplayFormat = "{0:#####.00}";
            sumValorPagar.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            SummarySettings sumValorRecuperado = bandUno.Summaries.Add("Valor Recuperado", SummaryType.Sum, bandUno.Columns["HOM_VALOR_PAGADO"]);
            sumValorRecuperado.DisplayFormat = "{0:#####.00}";
            sumValorRecuperado.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            SummarySettings sumValorPorRecuperar = bandUno.Summaries.Add("Valor Por Recuperar", SummaryType.Sum, bandUno.Columns["VALOR_POR_RECUPERAR"]);
            sumValorPorRecuperar.DisplayFormat = "{0:#####.00}";
            sumValorPorRecuperar.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

            SummarySettings sumValorCancelado = bandUno.Summaries.Add("Valor Cancelado", SummaryType.Sum, bandUno.Columns["HOM_VALOR_CANCELADO"]);
            sumValorCancelado.DisplayFormat = "{0:#####.00}";
            sumValorCancelado.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;


            //Cambio el nombre de las cabeceras
            bandUno.Columns["MED_CODIGO"].Header.Caption = "CODIGO";
            bandUno.Columns["MED_NOMBRE_MEDICO"].Header.Caption = "MEDICO";
            bandUno.Columns["HOM_FACTURA_MEDICO"].Header.Caption = "FACTURA DEL MEDICO";
            bandUno.Columns["HOM_FACTURA_FECHA"].Header.Caption = "FEC. FACTURA";
            bandUno.Columns["PAC_NOMBRE_PACIENTE"].Header.Caption = "PACIENTE";
            bandUno.Columns["ATE_NUMERO_CONTROL"].Header.Caption = "NUMERO DE CONTROL";
            bandUno.Columns["ATE_FACTURA_PACIENTE"].Header.Caption = "FACTURA PACIENTE";
            bandUno.Columns["ATE_FACTURA_FECHA"].Header.Caption = "FEC. FACTURA";
            bandUno.Columns["FOR_DESCRIPCION"].Header.Caption = "FORMA DE PAGO";
            bandUno.Columns["HOM_LOTE"].Header.Caption = "LOTE";
            bandUno.Columns["RET_CODIGO1"].Header.Caption = "NUM. RETENCION";
            bandUno.Columns["HOM_VALOR_NETO"].Header.Caption = "HONORARIO";
            bandUno.Columns["HOM_COMISION_CLINICA"].Header.Caption = "COMISION";
            bandUno.Columns["HOM_APORTE_LLAMADA"].Header.Caption = "REFERIDO";
            bandUno.Columns["HOM_RETENCION"].Header.Caption = "RETENCION";
            bandUno.Columns["HOM_VALOR_TOTAL"].Header.Caption = "PAGAR";
            bandUno.Columns["HOM_VALOR_PAGADO"].Header.Caption = "RECUPERADO";
            bandUno.Columns["VALOR_POR_RECUPERAR"].Header.Caption = "RECUPERAR";
            bandUno.Columns["HOM_VALOR_CANCELADO"].Header.Caption = "CANCELADO";
            bandUno.Columns["HOM_OBSERVACION"].Header.Caption = "OBSERVACION";

            //modifico el ancho por defecto de las columnas
            bandUno.Columns["MED_CODIGO"].Width = 50;
            bandUno.Columns["MED_NOMBRE_MEDICO"].Width = 200;
            bandUno.Columns["HOM_FACTURA_MEDICO"].Width = 100;
            bandUno.Columns["HOM_FACTURA_FECHA"].Width = 70;
            bandUno.Columns["PAC_NOMBRE_PACIENTE"].Width = 200;
            bandUno.Columns["ATE_NUMERO_CONTROL"].Width = 80;
            bandUno.Columns["ATE_FACTURA_PACIENTE"].Width = 100;
            bandUno.Columns["ATE_FACTURA_FECHA"].Width = 70;
            bandUno.Columns["FOR_DESCRIPCION"].Width = 120;
            bandUno.Columns["HOM_LOTE"].Width = 80;
            bandUno.Columns["RET_CODIGO1"].Width = 100;
            bandUno.Columns["HOM_VALOR_NETO"].Width = 100;
            bandUno.Columns["HOM_COMISION_CLINICA"].Width = 100;
            bandUno.Columns["HOM_APORTE_LLAMADA"].Width = 100;
            bandUno.Columns["HOM_RETENCION"].Width = 100;
            bandUno.Columns["HOM_VALOR_TOTAL"].Width = 100;
            bandUno.Columns["HOM_VALOR_PAGADO"].Width = 100;
            bandUno.Columns["VALOR_POR_RECUPERAR"].Width = 100;
            bandUno.Columns["HOM_VALOR_CANCELADO"].Width = 100;
            bandUno.Columns["HOM_OBSERVACION"].Width = 250;

            //alineo las columnas
            bandUno.Columns["MED_CODIGO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["MED_NOMBRE_MEDICO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            bandUno.Columns["HOM_FACTURA_MEDICO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["HOM_FACTURA_FECHA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            bandUno.Columns["PAC_NOMBRE_PACIENTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            bandUno.Columns["ATE_NUMERO_CONTROL"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["ATE_FACTURA_PACIENTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["ATE_FACTURA_FECHA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            bandUno.Columns["FOR_DESCRIPCION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            bandUno.Columns["HOM_LOTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["RET_CODIGO1"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            bandUno.Columns["HOM_VALOR_NETO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["HOM_COMISION_CLINICA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["HOM_APORTE_LLAMADA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["HOM_RETENCION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["HOM_VALOR_TOTAL"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["HOM_VALOR_PAGADO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["VALOR_POR_RECUPERAR"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["HOM_VALOR_CANCELADO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            bandUno.Columns["HOM_OBSERVACION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

            //ordeno las columnas
            bandUno.Columns["MED_CODIGO"].Header.VisiblePosition = 1;
            bandUno.Columns["MED_NOMBRE_MEDICO"].Header.VisiblePosition = 2;
            bandUno.Columns["HOM_FACTURA_MEDICO"].Header.VisiblePosition = 3;
            bandUno.Columns["HOM_FACTURA_FECHA"].Header.VisiblePosition = 4;
            bandUno.Columns["PAC_NOMBRE_PACIENTE"].Header.VisiblePosition = 5;
            bandUno.Columns["ATE_NUMERO_CONTROL"].Header.VisiblePosition = 6;
            bandUno.Columns["ATE_FACTURA_PACIENTE"].Header.VisiblePosition = 7;
            bandUno.Columns["ATE_FACTURA_FECHA"].Header.VisiblePosition = 8;
            bandUno.Columns["FOR_DESCRIPCION"].Header.VisiblePosition = 9;
            bandUno.Columns["HOM_LOTE"].Header.VisiblePosition = 10;
            bandUno.Columns["RET_CODIGO1"].Header.VisiblePosition = 11;
            bandUno.Columns["HOM_VALOR_NETO"].Header.VisiblePosition = 12;
            bandUno.Columns["HOM_COMISION_CLINICA"].Header.VisiblePosition = 14;
            bandUno.Columns["HOM_APORTE_LLAMADA"].Header.VisiblePosition = 15;
            bandUno.Columns["HOM_RETENCION"].Header.VisiblePosition = 16;
            bandUno.Columns["HOM_VALOR_TOTAL"].Header.VisiblePosition = 17;
            bandUno.Columns["VALOR_POR_RECUPERAR"].Header.VisiblePosition = 18;
            bandUno.Columns["HOM_VALOR_PAGADO"].Header.VisiblePosition = 19;
            bandUno.Columns["HOM_VALOR_CANCELADO"].Header.VisiblePosition = 13;
            bandUno.Columns["HOM_OBSERVACION"].Header.VisiblePosition = 20;

            //Cambio el color de las columnas
            string[] infMedico = new string[4] { "MED_CODIGO", "MED_NOMBRE_MEDICO", "HOM_FACTURA_MEDICO", "HOM_FACTURA_FECHA" };
            string[] infPaciente = new string[6] { "PAC_NOMBRE_PACIENTE", "ATE_NUMERO_CONTROL", "ATE_FACTURA_PACIENTE", "ATE_FACTURA_FECHA", "FOR_DESCRIPCION", "HOM_LOTE" };
            string[] infHonorarios = new string[11] { "RET_CODIGO1", "HOM_VALOR_NETO", "HOM_COMISION_CLINICA", "HOM_APORTE_LLAMADA", "HOM_RETENCION", "HOM_VALOR_TOTAL", "HOM_VALOR_PAGADO", "VALOR_POR_RECUPERAR", "HOM_RECORTE", "HOM_OBSERVACION", "HOM_VALOR_CANCELADO" };

            foreach (string item in infMedico)
            {
                //bandUno.Columns["MED_NOMBRE_MEDICO"].CellAppearance.AlphaLevel = 125;
                bandUno.Columns[item].CellAppearance.BackColor2 = Color.White;
                bandUno.Columns[item].CellAppearance.BackColor = Color.Silver;
                //bandUno.Columns[item].CellAppearance.BackColor = Color.DarkGray;
                bandUno.Columns[item].CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            }
            foreach (string item in infPaciente)
            {
                //bandUno.Columns[item].CellAppearance.BackColor2 = Color.LightCyan;
                bandUno.Columns[item].CellAppearance.BackColor = Color.LightCyan;
                //bandUno.Columns[item].CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            }
            foreach (string item in infHonorarios)
            {
                bandUno.Columns[item].CellAppearance.BackColor = Color.LightSteelBlue;
                //bandUno.Columns[item].CellAppearance.BackColor = Color.SlateGray;
                //bandUno.Columns[item].CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            }

            //
            bandUno.Columns["MED_CODIGO"].Header.Fixed = true;
            bandUno.Columns["MED_NOMBRE_MEDICO"].Header.Fixed = true;
            //Oculto columnas 
            bandUno.Columns["HOM_CODIGO"].Hidden = true;
            bandUno.Columns["HOM_OBSERVACION"].Hidden = true;
            bandUno.Columns["TMO_CODIGO"].Hidden = true;
            bandUno.Columns["ATE_CODIGO"].Hidden = true;
            bandUno.Columns["FOR_CODIGO"].Hidden = true;
            bandUno.Columns["ID_USUARIO"].Hidden = true;
            bandUno.Columns["HOM_FECHA_INGRESO"].Hidden = true;
            bandUno.Columns["MED_RUC"].Hidden = true;
            bandUno.Columns["DIFERENCIA"].Hidden = true;
            bandUno.Columns["VALOR_RECUPERADO"].Hidden = true;
            //excluyó columnas no visibles de la seleccion
            bandUno.Columns["HOM_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
            bandUno.Columns["ATE_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
            bandUno.Columns["FOR_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
            bandUno.Columns["ID_USUARIO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
            bandUno.Columns["HOM_FECHA_INGRESO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
            bandUno.Columns["MED_RUC"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
        
        }

        private void panelFiltroNumFactura_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dbugrHonorariosPorPagar_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                //Opciones por defecto de la grilla
                //dbugrHonorariosPorPagar.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                dbugrHonorariosPorPagar.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
                //dbugrHonorariosPorPagar.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                //dbugrHonorariosPorPagar.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                //dbugrHonorariosPorPagar.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[0].Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                //Caracteristicas de Filtro en la grilla
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
                //dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro";  
                //dbugrHonorariosPorPagar.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //Totalizo columnas
                dbugrHonorariosPorPagar.DisplayLayout.Bands[0].Summaries.Clear();
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Summaries.Clear();
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].SummaryFooterCaption = "Totales: ";
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Override.SummaryFooterCaptionAppearance.BackColor = Color.Silver;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Override.SummaryFooterCaptionAppearance.ForeColor = Color.LightYellow;
                //dbugrHonorariosPorPagar.DisplayLayout.Bands[0].Summaries.Add(Infragistics.Win.UltraWinGrid.SummaryType.Sum, dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["HOM_VALOR_NETO"]);
                //edito las columnas

                //añado las sumatorias a las columnas
                SummarySettings sumValorHonorario = dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Summaries.Add("Valor Honorario", SummaryType.Sum, dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_NETO"]);
                sumValorHonorario.DisplayFormat = "{0:#####.00}";
                sumValorHonorario.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                SummarySettings sumValorarPorPagar = dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Summaries.Add("Valor a Pagar", SummaryType.Sum, dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_TOTAL"]);
                sumValorarPorPagar.DisplayFormat = "{0:#####.00}";
                sumValorarPorPagar.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                SummarySettings sumValorTransferido = dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Summaries.Add("Valor Transferido", SummaryType.Sum, dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_CANCELADO"]);
                sumValorTransferido.DisplayFormat = "{0:#####.00}";
                sumValorTransferido.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                SummarySettings sumValorarPorTransferir = dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Summaries.Add("Valor por transferir", SummaryType.Sum, dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["VALOR_A_CANCELAR"]);
                sumValorarPorTransferir.DisplayFormat = "{0:#####.00}";
                sumValorarPorTransferir.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["NOMBRE_MEDICO"].Header.Caption = "MEDICO";
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_FACTURA_MEDICO"].Header.Caption = "FACTURA";
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_FACTURA_FECHA"].Header.Caption = "FEC. FACTURA";
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_NETO"].Header.Caption = "HONORARIO";
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_TOTAL"].Header.Caption = "VALOR CON DESCUENTOS";
                ////dbugrHonorariosPorPagar.DisplayLayout.Bands[0].Columns["HOM_VALOR_PAGADO"].Header.Caption ="VALOR PAGADO"; 
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_CANCELADO"].Header.Caption = "VALOR TRANSFERIDO";
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["VALOR_A_CANCELAR"].Header.Caption = "VALOR A TRANSFERIR";

                //cambio a no editables los campos de consulta
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["NOMBRE_MEDICO"].CellActivation = Activation.NoEdit;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_FACTURA_MEDICO"].CellActivation = Activation.NoEdit;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_FACTURA_FECHA"].CellActivation = Activation.NoEdit;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_NETO"].CellActivation = Activation.NoEdit;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_TOTAL"].CellActivation = Activation.NoEdit;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_CANCELADO"].CellActivation = Activation.NoEdit;


                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["NOMBRE_MEDICO"].Hidden = true;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_PAGADO"].Hidden = true;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_CODIGO"].Hidden = true;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["MED_CODIGO"].Hidden = true;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["FOR_CODIGO"].Hidden = true;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["MED_CON_TRANSFERENCIA"].Hidden = true;

                //Añado la columna check
                //if (iniHonorariosPorPagar == false)
                if(!dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns.Exists("CHECKTRANS")) 
                {
                    dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns.Add("CHECKTRANS", "");
                    dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["CHECKTRANS"].DataType = typeof(bool);
                    dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["CHECKTRANS"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
                    iniHonorariosPorPagar = true;
                }
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["CHECKTRANS"].Header.VisiblePosition = 0;

                for (Int16 i = 0; i < dbugrHonorariosPorPagar.Rows.Count; i++)
                {
                    for (Int16 j = 0; j < dbugrHonorariosPorPagar.Rows[i].ChildBands[0].Rows.Count; j++)
                    {
                        dbugrHonorariosPorPagar.Rows[i].ChildBands[0].Rows[j].Cells["CHECKTRANS"].Value = true;
                    }
                }
                    
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);       
            }
        }

        private void panelIndividual_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripSplitButtonNuevo_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSplitButtonNuevo_ButtonClick(object sender, EventArgs e)
        {
            //if (arbolMedicos.SelectedNode != null)
            //{
            //    if (arbolMedicos.SelectedNode.Tag.ToString() == "raiz")
            //    {

            //        if (MessageBox.Show("Desea crear un nuevo medico?", "Nuevo Medico", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //        {
            //            Form medicos = new frm_Medicos();
            //            medicos.ShowDialog();
            //        }
            //    }
            //    else if (arbolMedicos.SelectedNode.Tag.ToString() == "tipo_honorario")
            //    {
            //        if (MessageBox.Show("Desea crear un nuevo Tipo de Honorario?", "Nuevo Tipo de Honorario", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        {
            //            Form tipoHonorario = new frm_TipoHonorario();
            //            tipoHonorario.ShowDialog();
            //        }

            //    }
            //    else if (arbolMedicos.SelectedNode.Tag.ToString() == "tipo_especialidad")
            //    {
            //        if (MessageBox.Show("Desea crear una nueva Especialidad?", "Nueva Especialidad", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        {
            //            Form tipoEspecialidad = new frm_Especialidades();
            //            tipoEspecialidad.ShowDialog();
            //        }
            //    }
            //    //else if (arbolMedicos.SelectedNode.Tag == "tipo_medico")
            //    //{
            //    //    if (MessageBox.Show("Desea crear un nuevo Tipo de Medico?", "Nuevo Tipo de Medico", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //    //    { 

            //    //    }
            //    //}
            //    else if (arbolMedicos.SelectedNode.Tag.ToString() == "medico")
            //    {
            //        if (MessageBox.Show("Desea ingresar una nueva factura del medico?", "Nueva Factura", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        {
            //            Form factura = new frm_IngresoHonorarios();
            //            factura.ShowDialog();
            //        }
            //    }
            //} 
        }

        private void dbugrFacturasIngresadas_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                if (iniFacturasIngresadas == false)
                {
                    UltraGridBand bandUno = dbugrFacturasIngresadas.DisplayLayout.Bands[0];
                    
                    //Opciones por defecto de la grilla

                    //dbugrFacturasIngresadas.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                    dbugrFacturasIngresadas.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                    dbugrFacturasIngresadas.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                    dbugrFacturasIngresadas.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                    bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    //Caracteristicas de Filtro en la grilla
                    dbugrFacturasIngresadas.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                    dbugrFacturasIngresadas.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                    dbugrFacturasIngresadas.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                    dbugrFacturasIngresadas.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
                    //dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro"; 
                    dbugrFacturasIngresadas.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                    //
                    dbugrFacturasIngresadas.DisplayLayout.UseFixedHeaders = true;
                    //Cambio la apariencia de las sumas
                    bandUno.Summaries.Clear();
                    bandUno.SummaryFooterCaption = "Totales: ";
                    bandUno.Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                    bandUno.Override.SummaryFooterCaptionAppearance.BackColor = Color.Silver;
                    bandUno.Override.SummaryFooterCaptionAppearance.ForeColor = Color.LightYellow;
                    //totalizo las columnas
                    SummarySettings sumHonorarios = bandUno.Summaries.Add("Honorarios", SummaryType.Sum, bandUno.Columns["HOM_VALOR_NETO"]);
                    //sumHonorarios.DisplayFormat = "Tot = {0:#####.00}";
                    sumHonorarios.DisplayFormat = "{0:#####.00}";
                    sumHonorarios.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    SummarySettings sumComision = bandUno.Summaries.Add("Comision", SummaryType.Sum, bandUno.Columns["HOM_COMISION_CLINICA"]);
                    //sumComision.DisplayFormat = "Tot = {0:#####.00}";
                    sumComision.DisplayFormat = "{0:#####.00}";
                    sumComision.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    SummarySettings sumReferido = bandUno.Summaries.Add("Referido", SummaryType.Sum, bandUno.Columns["HOM_APORTE_LLAMADA"]);
                    //sumReferido.DisplayFormat = "Tot = {0:#####.00}";
                    sumReferido.DisplayFormat = "{0:#####.00}";
                    sumReferido.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    SummarySettings sumRetenido = bandUno.Summaries.Add("Retenido", SummaryType.Sum, bandUno.Columns["HOM_RETENCION"]);
                    sumRetenido.DisplayFormat = "{0:#####.00}";
                    sumRetenido.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    SummarySettings sumValorPagar = bandUno.Summaries.Add("Valor a pagar", SummaryType.Sum, bandUno.Columns["HOM_VALOR_TOTAL"]);
                    sumValorPagar.DisplayFormat = "{0:#####.00}";
                    sumValorPagar.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    SummarySettings sumValorRecuperado = bandUno.Summaries.Add("Valor Recuperado", SummaryType.Sum, bandUno.Columns["HOM_VALOR_PAGADO"]);
                    sumValorRecuperado.DisplayFormat = "{0:#####.00}";
                    sumValorRecuperado.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    SummarySettings sumValorPorRecuperar = bandUno.Summaries.Add("Valor Por Recuperar", SummaryType.Sum, bandUno.Columns["VALOR_POR_RECUPERAR"]);
                    sumValorPorRecuperar.DisplayFormat = "{0:#####.00}";
                    sumValorPorRecuperar.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    SummarySettings sumValorCancelado = bandUno.Summaries.Add("Valor Cancelado", SummaryType.Sum, bandUno.Columns["HOM_VALOR_CANCELADO"]);
                    sumValorCancelado.DisplayFormat = "{0:#####.00}";
                    sumValorCancelado.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    //Añado la columna check
                    bandUno.Columns.Add("columnaCheck", "");
                    bandUno.Columns["columnaCheck"].DataType = typeof(bool);
                    bandUno.Columns["columnaCheck"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
                    bandUno.Columns["columnaCheck"].Hidden = true; 

                    //Cambio el nombre de las cabeceras
                    bandUno.Columns["MED_CODIGO"].Header.Caption = "CODIGO";
                    bandUno.Columns["MED_NOMBRE_MEDICO"].Header.Caption = "MEDICO";
                    bandUno.Columns["HOM_FACTURA_MEDICO"].Header.Caption = "FACTURA DEL MEDICO";
                    bandUno.Columns["HOM_FACTURA_FECHA"].Header.Caption = "FEC. FACTURA";
                    bandUno.Columns["PAC_NOMBRE_PACIENTE"].Header.Caption = "PACIENTE";
                    bandUno.Columns["ATE_NUMERO_CONTROL"].Header.Caption = "NUMERO DE CONTROL";
                    bandUno.Columns["ATE_FACTURA_PACIENTE"].Header.Caption = "FACTURA PACIENTE";
                    bandUno.Columns["ATE_FACTURA_FECHA"].Header.Caption = "FEC. FACTURA";
                    bandUno.Columns["FOR_DESCRIPCION"].Header.Caption = "FORMA DE PAGO";
                    bandUno.Columns["HOM_LOTE"].Header.Caption = "LOTE";
                    bandUno.Columns["RET_CODIGO1"].Header.Caption = "NUM. RETENCION";
                    bandUno.Columns["HOM_VALOR_NETO"].Header.Caption = "HONORARIO";
                    bandUno.Columns["HOM_COMISION_CLINICA"].Header.Caption = "COMISION CLINICA";
                    bandUno.Columns["HOM_APORTE_LLAMADA"].Header.Caption = "REFERIDO";
                    bandUno.Columns["HOM_RETENCION"].Header.Caption = "VALOR RETENCION";
                    bandUno.Columns["HOM_VALOR_TOTAL"].Header.Caption = "VALOR A PAGAR";
                    bandUno.Columns["HOM_VALOR_PAGADO"].Header.Caption = "VALOR RECUPERADO";
                    bandUno.Columns["HOM_RECORTE"].Header.Caption = "RECORTE";
                    bandUno.Columns["VALOR_POR_RECUPERAR"].Header.Caption = "VALOR POR RECUPERAR";
                    //bandUno.Columns["HOM_VALOR_CANCELADO"].Header.Caption = "VALOR CANCELADO";
                    dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["HOM_OBSERVACION"].Header.Caption = "OBSERVACION";

                    //resalto columnas importantes
                    bandUno.Columns["HOM_VALOR_NETO"].CellAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                    bandUno.Columns["HOM_VALOR_PAGADO"].CellAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                    bandUno.Columns["VALOR_POR_RECUPERAR"].CellAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;

                    //Cambio el a no editables a las columnas
                    bandUno.Columns["MED_CODIGO"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["MED_NOMBRE_MEDICO"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_FACTURA_MEDICO"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_FACTURA_FECHA"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["PAC_NOMBRE_PACIENTE"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["ATE_NUMERO_CONTROL"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["ATE_FACTURA_PACIENTE"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["ATE_FACTURA_FECHA"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["FOR_DESCRIPCION"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_LOTE"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["RET_CODIGO1"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_VALOR_NETO"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_COMISION_CLINICA"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_APORTE_LLAMADA"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_RETENCION"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_VALOR_TOTAL"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_VALOR_PAGADO"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["HOM_RECORTE"].CellActivation = Activation.NoEdit;
                    bandUno.Columns["VALOR_POR_RECUPERAR"].CellActivation = Activation.NoEdit;
                    //bandUno.Columns["HOM_VALOR_CANCELADO"].CellActivation = Activation.NoEdit;
                    dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["HOM_OBSERVACION"].CellActivation = Activation.NoEdit;

                    //modifico el ancho por defecto de las columnas
                    bandUno.Columns["columnaCheck"].Width = 40;
                    bandUno.Columns["MED_CODIGO"].Width = 50;
                    bandUno.Columns["MED_NOMBRE_MEDICO"].Width = 200;
                    bandUno.Columns["HOM_FACTURA_MEDICO"].Width = 100;
                    bandUno.Columns["HOM_FACTURA_FECHA"].Width = 70;
                    bandUno.Columns["PAC_NOMBRE_PACIENTE"].Width = 200;
                    bandUno.Columns["ATE_NUMERO_CONTROL"].Width = 80;
                    bandUno.Columns["ATE_FACTURA_PACIENTE"].Width = 100;
                    bandUno.Columns["ATE_FACTURA_FECHA"].Width = 70;
                    bandUno.Columns["FOR_DESCRIPCION"].Width = 120;
                    bandUno.Columns["HOM_LOTE"].Width = 80;
                    bandUno.Columns["RET_CODIGO1"].Width = 100;
                    bandUno.Columns["HOM_VALOR_NETO"].Width = 100;
                    bandUno.Columns["HOM_COMISION_CLINICA"].Width = 100;
                    bandUno.Columns["HOM_APORTE_LLAMADA"].Width = 100;
                    bandUno.Columns["HOM_RETENCION"].Width = 100;
                    bandUno.Columns["HOM_VALOR_TOTAL"].Width = 100;
                    bandUno.Columns["HOM_VALOR_PAGADO"].Width = 100;
                    bandUno.Columns["HOM_RECORTE"].Width = 100;
                    bandUno.Columns["VALOR_POR_RECUPERAR"].Width = 100;
                    //bandUno.Columns["HOM_VALOR_CANCELADO"].Width = 100;
                    dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["HOM_OBSERVACION"].Width = 250;

                    //alineo las columnas
                    bandUno.Columns["MED_CODIGO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["MED_NOMBRE_MEDICO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    bandUno.Columns["HOM_FACTURA_MEDICO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_FACTURA_FECHA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    bandUno.Columns["PAC_NOMBRE_PACIENTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    bandUno.Columns["ATE_NUMERO_CONTROL"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["ATE_FACTURA_PACIENTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["ATE_FACTURA_FECHA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    bandUno.Columns["FOR_DESCRIPCION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    bandUno.Columns["HOM_LOTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["RET_CODIGO1"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    bandUno.Columns["HOM_VALOR_NETO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_COMISION_CLINICA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_APORTE_LLAMADA"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_RETENCION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_VALOR_TOTAL"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_VALOR_PAGADO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["HOM_RECORTE"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    bandUno.Columns["VALOR_POR_RECUPERAR"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    //bandUno.Columns["HOM_VALOR_CANCELADO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                    dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["HOM_OBSERVACION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

                    //ordeno las columnas
                    bandUno.Columns["columnaCheck"].Header.VisiblePosition = 0;
                    bandUno.Columns["MED_CODIGO"].Header.VisiblePosition = 1;
                    bandUno.Columns["MED_NOMBRE_MEDICO"].Header.VisiblePosition = 2;
                    bandUno.Columns["HOM_FACTURA_MEDICO"].Header.VisiblePosition = 3;
                    bandUno.Columns["HOM_FACTURA_FECHA"].Header.VisiblePosition = 4;
                    bandUno.Columns["PAC_NOMBRE_PACIENTE"].Header.VisiblePosition = 5;
                    bandUno.Columns["ATE_NUMERO_CONTROL"].Header.VisiblePosition = 6;
                    bandUno.Columns["ATE_FACTURA_PACIENTE"].Header.VisiblePosition = 7;
                    bandUno.Columns["ATE_FACTURA_FECHA"].Header.VisiblePosition = 8;
                    bandUno.Columns["FOR_DESCRIPCION"].Header.VisiblePosition = 9;
                    bandUno.Columns["HOM_LOTE"].Header.VisiblePosition = 10;
                    bandUno.Columns["RET_CODIGO1"].Header.VisiblePosition = 11;
                    bandUno.Columns["HOM_VALOR_NETO"].Header.VisiblePosition = 12;
                    bandUno.Columns["HOM_COMISION_CLINICA"].Header.VisiblePosition = 13;
                    bandUno.Columns["HOM_APORTE_LLAMADA"].Header.VisiblePosition = 14;
                    bandUno.Columns["HOM_RETENCION"].Header.VisiblePosition = 15;
                    bandUno.Columns["HOM_VALOR_TOTAL"].Header.VisiblePosition = 16;
                    bandUno.Columns["VALOR_POR_RECUPERAR"].Header.VisiblePosition = 17;
                    bandUno.Columns["HOM_RECORTE"].Header.VisiblePosition = 18;
                    bandUno.Columns["HOM_VALOR_PAGADO"].Header.VisiblePosition = 19;
                    //bandUno.Columns["HOM_VALOR_CANCELADO"].Header.VisiblePosition = 19;
                    dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["HOM_OBSERVACION"].Header.VisiblePosition = 20;

                    //
                    bandUno.Columns["columnaCheck"].Header.Fixed = true;  
                    bandUno.Columns["MED_CODIGO"].Header.Fixed = true;  
                    bandUno.Columns["MED_NOMBRE_MEDICO"].Header.Fixed = true;  
                    //Oculto columnas 
                    bandUno.Columns["HOM_CODIGO"].Hidden = true;
                    bandUno.Columns["ATE_CODIGO"].Hidden = true;
                    bandUno.Columns["FOR_CODIGO"].Hidden = true;
                    bandUno.Columns["ID_USUARIO"].Hidden = true;
                    bandUno.Columns["HOM_FECHA_INGRESO"].Hidden = true;
                    bandUno.Columns["MED_RUC"].Hidden = true;
                    bandUno.Columns["HOM_VALOR_CANCELADO"].Hidden = true;
                    bandUno.Columns["TMO_NOMBRE"].Hidden = true;
                    bandUno.Columns["TMO_CODIGO"].Hidden = true;
                    
                    //Cambio el color de las columnas
                    string[]  infMedico = new string[4] { "MED_CODIGO", "MED_NOMBRE_MEDICO", "HOM_FACTURA_MEDICO", "HOM_FACTURA_FECHA" };
                    string[] infPaciente = new string[6] { "PAC_NOMBRE_PACIENTE", "ATE_NUMERO_CONTROL", "ATE_FACTURA_PACIENTE", "ATE_FACTURA_FECHA", "FOR_DESCRIPCION", "HOM_LOTE" };
                    string[] infHonorarios = new string[10] { "RET_CODIGO1", "HOM_VALOR_NETO", "HOM_COMISION_CLINICA", "HOM_APORTE_LLAMADA", "HOM_RETENCION", "HOM_VALOR_TOTAL", "HOM_VALOR_PAGADO", "VALOR_POR_RECUPERAR", "HOM_RECORTE", "HOM_OBSERVACION" };

                    foreach (string item in infMedico)
                    {
                        //bandUno.Columns["MED_NOMBRE_MEDICO"].CellAppearance.AlphaLevel = 125;
                        bandUno.Columns[item].CellAppearance.BackColor2 = Color.White;
                        bandUno.Columns[item].CellAppearance.BackColor = Color.Silver;
                        //bandUno.Columns[item].CellAppearance.BackColor = Color.DarkGray;
                        bandUno.Columns[item].CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
                    }
                    foreach (string item in infPaciente)
                    {
                        //bandUno.Columns[item].CellAppearance.BackColor2 = Color.LightCyan;
                        bandUno.Columns[item].CellAppearance.BackColor = Color.LightCyan;
                        //bandUno.Columns[item].CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
                    }
                    foreach (string item in infHonorarios)
                    {
                        bandUno.Columns[item].CellAppearance.BackColor = Color.LightSteelBlue;
                        //bandUno.Columns[item].CellAppearance.BackColor = Color.SlateGray;
                        //bandUno.Columns[item].CellAppearance.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
                    }
                    //excluyó columnas no visibles de la seleccion
                    bandUno.Columns["HOM_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["ATE_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["FOR_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["ID_USUARIO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    ////bandUno.Columns["HOM_VALOR_CANCELADO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["HOM_FECHA_INGRESO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["MED_RUC"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["HOM_VALOR_CANCELADO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["TMO_NOMBRE"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;
                    bandUno.Columns["TMO_CODIGO"].ExcludeFromColumnChooser = Infragistics.Win.UltraWinGrid.ExcludeFromColumnChooser.True;

                    iniFacturasIngresadas = true;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);  
            }
        }

        private void ToolStripMenuItemPagoRecuperado_Click(object sender, EventArgs e)
        {
             try {
      
                nuevoRecuperacionHonorarios = true;
                toolStripButtonGuardar.Enabled = true;
                toolStripSplitButtonNuevo.Enabled = false; 

                //oculto las demas pestañas
                tabHonorariosDetalle.Tabs["listaRecuperacion"].Enabled =false;
                tabHonorariosDetalle.Tabs["cancelacionHonorarios"].Enabled =false;
                tabHonorariosDetalle.Tabs["listaCancelaciones"].Enabled =false;
                tabHonorariosDetalle.Tabs["estadoCuentas"].Enabled = false;
                 //oculto el arbol de medicos
                MarcoCuerpo.Panel1Collapsed = true;
                 //cargo los valores por recuperar
                if (arbolMedicos.Nodes[0].Checked == true)
                {
                    filtrarFacturasIngresadasMedicos(arbolMedicos.Nodes[0]);
                }
                else
                {
                    TreeNode nodo = buscarNodo(arbolMedicos.Nodes[0]);
                    if (nodo != null)
                        filtrarFacturasIngresadasMedicos(nodo);

                    else
                        filtrarFacturasIngresadasMedicos(arbolMedicos.Nodes[0]);

                }
                cargarFacturasMedicos(Parametros.HonorariosPAR.codigoTipoMovimientoHonorariosMedicos.ToString(), porRecuperar, medCodigo, espCodigo, tihCodigo, timCodigo, medRecibeLlamada, fechaIniFacturaMedico,
                    fechaFinFacturaMedico, honorariosCancelados, forCodigo, tifCodigo, lote, numeroControl, facturaClinica, FechaIniFacturaCliente,
                    FechaFinFacturaCliente, pacienteReferido, pacienteFechaAlta, ateCodigo, pacCodigo, dbugrFacturasIngresadas);

                //activo los controles para un nuevo pago
            
                lnlbTodos.Enabled = true;
                lnlblNinguna.Enabled = true;
                 //muestro el total de honorarios por recuperar

                tableLayoutPanelDocumentosMedicos.RowStyles[1].SizeType = SizeType.Absolute;
                tableLayoutPanelDocumentosMedicos.RowStyles[1].Height =35;

                if(dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["columnaCheck"]!=null)
                    dbugrFacturasIngresadas.DisplayLayout.Bands[0].Columns["columnaCheck"].Hidden = false;

                btnPagosClinica.Enabled = true;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);   
            }

        }

        private void tableLayoutPanelDocumentosMedicos_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboFiltroFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                //KeyValuePair<int, string> item = (KeyValuePair<int, string>)cboFiltroTipoFormaPago.SelectedItem;
                //Int16 codigoTipo = (Int16)(item.Key);
                //foreach (int cod in HonorariosPAR.getCodigoTarjetasCredito())
                //{
                //    if (codigoTipo == cod)
                //    {
                //        KeyValuePair<int, string> idFormaPago = (KeyValuePair<int, string>)cboFiltroFormaPago.SelectedItem;
                //        Int16 codigoFormaPago = (Int16)idFormaPago.Key;  
                //        dbugrNCFormaPago.DataSource = NegNotaCreditoDebito.RecuperarNotasCreditoInternasPorFormaPago(codigoFormaPago).Select(n => new { n.MED_CODIGO1, n.NOT_RAZON_SOCIAL, n.NOT_NUMERO, n.NOT_DOCUMENTO_AFECTADO, n.NOT_MOTIVO_MODIFICACION, n.NOT_VALOR, n.TID_CODIGO }).ToList() ;
                //        if (dbugrNCFormaPago.Rows.Count > 0)
                //        {
                //            tableLayoutPanelDocumentosMedicos.RowStyles[0].SizeType = SizeType.Percent;
                //            tableLayoutPanelDocumentosMedicos.RowStyles[0].Height = 70;
                //            tableLayoutPanelDocumentosMedicos.RowStyles[1].SizeType = SizeType.Percent;
                //            tableLayoutPanelDocumentosMedicos.RowStyles[1].Height = 30;
                //            return;
                //        }
                //    }
                //}
                //tableLayoutPanelDocumentosMedicos.RowStyles[0].SizeType = SizeType.Percent;
                //tableLayoutPanelDocumentosMedicos.RowStyles[0].Height = 100;
                //tableLayoutPanelDocumentosMedicos.RowStyles[1].SizeType = SizeType.Percent;
                //tableLayoutPanelDocumentosMedicos.RowStyles[1].Height = 0;
                
                //En el caso de que la forma de pago sea por Honorarios Directos.

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);      
            }
        }

        //private void dbugrNCFormaPago_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        //{

        //        if (iniNotasCreditoFormaPago == false)
        //        {
        //            UltraGridBand bandUno = dbugrNCFormaPago.DisplayLayout.Bands[0];
                    
        //            //Opciones por defecto de la grilla

        //            //dbugrNCFormaPago.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
        //            //dbugrNCFormaPago.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
        //            dbugrNCFormaPago.DisplayLayout.GroupByBox.Hidden = true;
        //            //dbugrNCFormaPago.DisplayLayout.Override.GroupByColumnsHidden = Infragistics.Win.DefaultableBoolean.True;
        //            dbugrNCFormaPago.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
        //            dbugrNCFormaPago.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
        //            bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                    
        //            //Añado la columna check
        //            bandUno.Columns.Add("columnaCheck", "");
        //            bandUno.Columns["columnaCheck"].DataType = typeof(bool);
        //            bandUno.Columns["columnaCheck"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
        //            //bandUno.Columns["columnaCheck"].Hidden = true; 

        //            //Cambio el nombre de las cabeceras
        //            bandUno.Columns["MED_CODIGO1"].Header.Caption = "CODIGO";
        //            bandUno.Columns["NOT_RAZON_SOCIAL"].Header.Caption = "MEDICO";
        //            bandUno.Columns["NOT_NUMERO"].Header.Caption = "NUM. DE LA NOTA";
        //            bandUno.Columns["NOT_DOCUMENTO_AFECTADO"].Header.Caption = "FACT. AFECTADA";
        //            bandUno.Columns["NOT_MOTIVO_MODIFICACION"].Header.Caption = "OBSERVACIONES";
        //            bandUno.Columns["NOT_VALOR"].Header.Caption = "VALOR";

        //            //modifico el ancho por defecto de las columnas
        //            bandUno.Columns["columnaCheck"].Width = 40;
        //            bandUno.Columns["MED_CODIGO1"].Width = 50;
        //            bandUno.Columns["NOT_RAZON_SOCIAL"].Width = 260;
        //            bandUno.Columns["NOT_NUMERO"].Width = 100;
        //            bandUno.Columns["NOT_DOCUMENTO_AFECTADO"].Width =100;
        //            bandUno.Columns["NOT_MOTIVO_MODIFICACION"].Width = 250;
        //            bandUno.Columns["NOT_VALOR"].Width = 80;

        //            //alineo las columnas
        //            bandUno.Columns["MED_CODIGO1"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
        //            bandUno.Columns["NOT_RAZON_SOCIAL"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
        //            bandUno.Columns["NOT_NUMERO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
        //            bandUno.Columns["NOT_DOCUMENTO_AFECTADO"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
        //            bandUno.Columns["NOT_MOTIVO_MODIFICACION"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
        //            bandUno.Columns["NOT_VALOR"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;

        //            //ordeno las columnas
        //            bandUno.Columns["columnaCheck"].Header.VisiblePosition = 0;
        //            bandUno.Columns["MED_CODIGO1"].Header.VisiblePosition = 1;
        //            bandUno.Columns["NOT_RAZON_SOCIAL"].Header.VisiblePosition = 2;
        //            bandUno.Columns["NOT_NUMERO"].Header.VisiblePosition = 3;
        //            bandUno.Columns["NOT_DOCUMENTO_AFECTADO"].Header.VisiblePosition = 4;
        //            bandUno.Columns["NOT_MOTIVO_MODIFICACION"].Header.VisiblePosition = 5;
        //            bandUno.Columns["NOT_VALOR"].Header.VisiblePosition = 6;

        //            //Oculto columnas 
        //            bandUno.Columns["TID_CODIGO"].Hidden = true;
                   
        //            iniNotasCreditoFormaPago = true;
        //    }
        //}

        private void ultraDataSourcePorRecuperar_CellDataRequested(object sender, Infragistics.Win.UltraWinDataSource.CellDataRequestedEventArgs e)
        {

        }



        private void cboPagTipoTransferencia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboTransBancos_SelectedIndexChanged(object sender, EventArgs e)
        {
            BANCOS  banco = (BANCOS) cboTransBancos.SelectedItem;
            txtCanCuenta.Text = banco.BAN_CUENTA_CONTABLE; 
        }

        private void ToolStripMenuItemPagoHonorarios_Click(object sender, EventArgs e)
        {
            try
            {
                toolStripButtonGuardar.Enabled = true;
                //oculto las demas pestañas
                tabHonorariosDetalle.Tabs["recuperacionCartera"].Enabled = false;
                tabHonorariosDetalle.Tabs["listaRecuperacion"].Enabled = false;
                tabHonorariosDetalle.Tabs["listaCancelaciones"].Enabled = false;
                tabHonorariosDetalle.Tabs["estadoCuentas"].Enabled = false;
                //oculto el arbol de medicos
                MarcoCuerpo.Panel1Collapsed = true;
                //
                splitterCancelacion.SplitterDistance = 66;
                splitterCancelacion.Panel1Collapsed = false; 
                //tabHonorariosDetalle.SelectedIndex = 2;
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);       
            }
        }

        //private void medicoToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
        //    if (MessageBox.Show("Desea crear un nuevo medico?", "Nuevo Medico", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //    {
        //        Form medicos = new frm_Medicos();
        //        medicos.ShowDialog();
        //    }
        //}

        private void especialidadMedicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea crear una nueva Especialidad?", "Nueva Especialidad", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Form tipoEspecialidad = new frm_Especialidades();
                tipoEspecialidad.ShowDialog();
            }
        }

        private void tipoHonorarioToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea crear un nuevo Tipo de Honorario?", "Nuevo Tipo de Honorario", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Form tipoHonorario = new frm_TipoHonorario();
                tipoHonorario.ShowDialog();
            }
        }

        private void dbugrRecuperacionHonorariosTemp_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            if (iniRecuperacionHonorariosTemp == false)
            {
                //Añado la columna check
                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns.Add("columnaRecuperado", "VALOR POR RECUPERAR");
                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["columnaRecuperado"].DataType = typeof(Decimal);
                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["columnaRecuperado"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Currency;
                

                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns.Add("columnaRecorte", "RECORTE");
                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["columnaRecorte"].DataType = typeof(Decimal);
                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["columnaRecorte"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Currency;

                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns.Add("columnaRetencion", "RETENCION");
                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["columnaRetencion"].DataType = typeof(Decimal);
                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["columnaRetencion"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Currency;

                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["MED_NOMBRE_MEDICO"].Header.Caption = "MEDICO";
                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["HOM_FACTURA_MEDICO"].Header.Caption = "FACTURA";
                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["HOM_VALOR_NETO"].Header.Caption = "HONORARIO";

                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["MED_NOMBRE_MEDICO"].Width = 170;
                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["HOM_FACTURA_MEDICO"].Width = 100;
                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["HOM_VALOR_NETO"].Width =80;
                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["columnaRecuperado"].Width = 80;
               

                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["HOM_CODIGO"].Hidden = true;
                //dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["MED_CODIGO"].Hidden = true;
                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["VALOR_POR_RECUPERAR"].Hidden = true;
                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["MED_CODIGO"].Hidden = true;
                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["HOM_COMISION_CLINICA"].Hidden = true;
                dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["HOM_VALOR_PAGADO"].Hidden = true;

                //Aumento las sumas
                SummarySettings sumValorHonorario = dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Summaries.Add("Valor Honorario", SummaryType.Sum, dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["HOM_VALOR_NETO"]);
                sumValorHonorario.DisplayFormat = "{0:#####.00}";
                sumValorHonorario.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                SummarySettings sumValorPorRecuperar = dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Summaries.Add("Valor por Recuperar", SummaryType.Sum, dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["columnaRecuperado"]);
                sumValorPorRecuperar.DisplayFormat = "{0:#####.00}";
                sumValorPorRecuperar.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                SummarySettings sumValorRecorte = dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Summaries.Add("Valor del Recorte", SummaryType.Sum, dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["columnaRecorte"]);
                sumValorRecorte.DisplayFormat = "{0:#####.00}";
                sumValorRecorte.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                iniRecuperacionHonorariosTemp = true;
            }
            foreach (UltraGridRow fila in dbugrRecuperacionHonorariosTemp.Rows)
            {
                fila.Cells["columnaRecorte"].Value = 0.00;
                fila.Cells["columnaRecuperado"].Value = fila.Cells["VALOR_POR_RECUPERAR"].Value;
            }

        }


        private void cboFiltroTipoFormaPago_Enter(object sender, EventArgs e)
        {
            if (cboFiltroTipoFormaPago.Items.Count == 1)
            {
                cargarTipoFormaPago(cboFiltroTipoFormaPago);
                //cargarTipoFormaPago(cboCanTipoFormaPago); ;
            }
        }

        private void cboCanTipoFormaPago_Enter(object sender, EventArgs e)
        {
            if (cboCanTipoFormaPago.Items.Count == 0)
            {
                cargarTipoFormaPago(cboCanTipoFormaPago); ;
            }
        }

        private void dbugrEstadoCuentas_InitializeGroupByRow(object sender, InitializeGroupByRowEventArgs e)
        {
            e.Row.ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
            e.Row.Expanded = true;
        }

        private void toolStripMenuItemHonorarioDirecto_Click(object sender, EventArgs e)
        {
            toolStripButtonGuardar.Enabled = true;
            //si el combo de tipo y formas de pago no esta lleno
            if (cboFiltroTipoFormaPago.Items.Count == 1)
            {
                cargarTipoFormaPago(cboFiltroTipoFormaPago);
                cargarTipoFormaPago(cboCanTipoFormaPago); 
            }
            for (int i = 1; i < cboFiltroFormaPago.Items.Count-1; i++)
            {
                KeyValuePair<int, string> item = (KeyValuePair<int, string>)cboFiltroFormaPago.Items[i] ;
                Int16 codigoFormaPago = (Int16)(item.Key);
                if (codigoFormaPago == HonorariosPAR.getCodigoFPHonorarioDirecto())
                {
                    cboFiltroFormaPago.SelectedIndex = i;
                    honorariosDirectos = true;
                    btnPagosClinica.Text = "Cancelar Honorarios Directos";
                    toolStripButtonActualizar_Click(null, null);
                    return;
                }
            }
        }

        private void cboTransBancos_Enter(object sender, EventArgs e)
        {
            //Combo de Bancos
            if (cboTransBancos.Items.Count == 0)
            {
                cboTransBancos.DataSource = NegBancos.RecuperaBancos();
                cboTransBancos.DisplayMember = "BAN_NOMBRE";
                cboTransBancos.ValueMember = "BAN_CODIGO";
            }
        }



        private void dbugrHonorariosPorPagar_InitializeGroupByRow(object sender, InitializeGroupByRowEventArgs e)
        {
            e.Row.ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
            e.Row.Expanded = true;
        }

        private void pnlInfMedico_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dbugrRecuperacionHonorariosTemp_AfterCellUpdate(object sender, CellEventArgs e)
        {
            try
            {
                if (dbugrRecuperacionHonorariosTemp.ActiveRow != null)
                {
                    if (e.Cell.Column == dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["columnaRecorte"])
                    {
                        Int64 codigoHonorarios = Convert.ToInt64(dbugrRecuperacionHonorariosTemp.ActiveRow.Cells["HOM_CODIGO"].Text);
                        if (codigoHonorarios > 0)
                        {
                            decimal recorte = Convert.ToDecimal(e.Cell.Value);
                            decimal honorario = Convert.ToDecimal(dbugrRecuperacionHonorariosTemp.Rows[e.Cell.Row.Index].Cells["HOM_VALOR_NETO"].Value);
                            decimal recuperado = Convert.ToDecimal(dbugrRecuperacionHonorariosTemp.Rows[e.Cell.Row.Index].Cells["VALOR_POR_RECUPERAR"].Value);

                            if (recorte <= recuperado)
                            {
                                recuperado = recuperado - recorte;
                                dbugrRecuperacionHonorariosTemp.Rows[e.Cell.Row.Index].Cells["columnaRecuperado"].Value = recuperado;
                            }
                            else
                                e.Cell.Value = e.Cell.OriginalValue;
                        }
                    }
                }
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message); 
            }

            //if (e.Cell.Column == dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["VALOR_POR_RECUPERAR"])
            //{
            //    MessageBox.Show(e.Cell.Value.ToString());
            //}

        }

        private void toolStripButtonImprimir_Click(object sender, EventArgs e)
        {
            mostrarMensajeEspera(); 
            if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["recuperacionCartera"])
            {

            }
            else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["listaRecuperacion"])
            {

                if (dbgrPagosFacMedicos.ActiveRow != null)
                {
                    frmReportes frm = new frmReportes();
                    frm.reporte = "rRecuperacionHonorarios1";
                    frm.campo1 = dbgrPagosFacMedicos.ActiveRow.Cells["PAM_CODIGO"].Text ;
                    frm.Show();
                }
            }
            else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["cancelacionHonorarios"])
            {
                
            }
            else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["listaCancelaciones"])
            {
                
            }
            else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["estadoCuentas"])
            {
                
            }
            cerrarMensajeEspera();
        }

        private void lblNumeroControl_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                //. cargo las opciones por defecto del explorador  
                cargarValoresPorDefecto();
                cargarRecursos();
                cargarDimensiones();

                if (tabHonorariosDetalle.Tabs[0].Key == "recuperacionCartera")
                {
                    tableLayoutPanelDocumentosMedicos.RowStyles[0].SizeType = SizeType.Percent;
                    tableLayoutPanelDocumentosMedicos.RowStyles[0].Height = 100;
                    tableLayoutPanelDocumentosMedicos.RowStyles[1].SizeType = SizeType.Percent;
                    tableLayoutPanelDocumentosMedicos.RowStyles[1].Height = 0;
                }
                //desbloqueo tabs
                foreach (UltraTab tab in tabHonorariosDetalle.Tabs)
                {
                    if (tab.Enabled == false)
                        tab.Enabled = true;
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);    
            }

        }

        private void optFacturaMedico_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButtonExportar_Click(object sender, EventArgs e)
        {
            try
            {
                
                CreateExcel(FindSavePath());
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {
                
                this.Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// Crea el archivo de Excel usando el directorio q elige el usuario
        /// </summary>
        /// <param name="myFilepath"></param>
        private void CreateExcel(String myFilepath)
        {
            try
            {
                if (myFilepath != null)
                {

                    if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["recuperacionCartera"])
                        this.ultraGridExcelExporter1.Export(dbugrFacturasIngresadas, myFilepath);
                    else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["listaRecuperacion"])
                        this.ultraGridExcelExporter1.Export(dbgrPagosFacMedicos, myFilepath);
                    else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["cancelacionHonorarios"])
                        this.ultraGridExcelExporter1.Export(dbugrHonorariosPorPagar, myFilepath);
                    else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["listaCancelaciones"])
                        this.ultraGridExcelExporter1.Export(dbugrdCancelacionHonorarios, myFilepath);
                    else if (tabHonorariosDetalle.SelectedTab == tabHonorariosDetalle.Tabs["estadoCuentas"])
                        this.ultraGridExcelExporter1.Export(dbugrEstadoCuentas, myFilepath);
                    
                    MessageBox.Show("Se termino de exportar el grid en el archivo " + myFilepath);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        private void dbgrPagosFacMedicos_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }

        // Marco todos los checks de la Grilla de cancelacion de facturas
        private void lnlbTodosCan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (UltraGridRow item in dbugrHonorariosPorPagar.Rows)
            {
                item.Cells["columnaCheck"].Value = true;
            }
        }

        // Marco todos los checks de la Grilla de cancelacion de facturas
        private void lnlblNingunaCan_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            foreach (UltraGridRow item in dbugrHonorariosPorPagar.Rows)
            {
                item.Cells["columnaCheck"].Value = false;
            }
        }

        private void dbugrRecuperacionHonorariosTemp_KeyUp(object sender, KeyEventArgs e)
        {
            //var grid = (UltraGrid)sender;

            if (e.KeyCode == Keys.Enter)
            {
                // Go down one row
                dbugrRecuperacionHonorariosTemp.PerformAction(UltraGridAction.BelowCell);
                dbugrRecuperacionHonorariosTemp.PerformAction(UltraGridAction.EnterEditMode);
                //grid.PerformAction(UltraGridAction.BelowCell);
            }

        }

        private void dbugrRecuperacionHonorariosTemp_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
        {
            try
            {
                if (dbugrRecuperacionHonorariosTemp.ActiveRow != null)
                {
                    if (e.Cell.Column == dbugrRecuperacionHonorariosTemp.DisplayLayout.Bands[0].Columns["columnaRetencion"])
                    {
                        int codigoHonorarios = Convert.ToInt32(dbugrRecuperacionHonorariosTemp.ActiveRow.Cells["MED_CODIGO"].Text);
                        if (codigoHonorarios != Parametros.HonorariosPAR.CodigoMedicoClinica)
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);  
            }
        }

        private void txtCanNumeroPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void dtpFechaPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cboCanTipoFormaPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtCanBanco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void cboCanFormaPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtCanLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void rtbCanObsCancelacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void dbugrFacturasIngresadas_CellChange(object sender, CellEventArgs e)
        {
            try
            {
                //index de la columna checkbox
                if (e.Cell.Column.Index == 29)
                {
                    if (Convert.ToBoolean(dbugrFacturasIngresadas.ActiveRow.Cells[29].Value) == false)
                    {
                        txtTotalPorRecuperar.Text = (Convert.ToDecimal(txtTotalPorRecuperar.Text) + Convert.ToDecimal(dbugrFacturasIngresadas.ActiveRow.Cells["VALOR_POR_RECUPERAR"].Value)).ToString();
                        dbugrFacturasIngresadas.PerformAction(UltraGridAction.BelowCell);
                    }
                    else
                    {
                        txtTotalPorRecuperar.Text = (Convert.ToDecimal(txtTotalPorRecuperar.Text) - Convert.ToDecimal(dbugrFacturasIngresadas.ActiveRow.Cells["VALOR_POR_RECUPERAR"].Value)).ToString();
                        dbugrFacturasIngresadas.PerformAction(UltraGridAction.BelowCell);
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);    
            }
        }

        private void ultraGroupBoxDatosCancelacion_Click(object sender, EventArgs e)
        {

        }

        private void txtTransCheque_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboTransBancos_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cboTransBancos_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            BANCOS banco = (BANCOS)cboTransBancos.SelectedItem;
            txtCanCuenta.Text = banco.BAN_CUENTA_CONTABLE;
        }

        private void cboTransBancos_Enter_1(object sender, EventArgs e)
        {
            //Combo de Bancos
            if (cboTransBancos.Items.Count == 0)
            {
                cboTransBancos.DataSource = NegBancos.RecuperaBancos();
                cboTransBancos.DisplayMember = "BAN_NOMBRE";
                cboTransBancos.ValueMember = "BAN_CODIGO";
            }
        }

        private void dbugrHonorariosPorPagar_InitializeLayout_1(object sender, InitializeLayoutEventArgs e)
        {

            try
            {
                //Opciones por defecto de la grilla
                //dbugrHonorariosPorPagar.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                dbugrHonorariosPorPagar.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
                //dbugrHonorariosPorPagar.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                //dbugrHonorariosPorPagar.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                //dbugrHonorariosPorPagar.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[0].Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                //Caracteristicas de Filtro en la grilla
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.RowAndCell;
                //dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro";  
                //dbugrHonorariosPorPagar.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //Totalizo columnas
                dbugrHonorariosPorPagar.DisplayLayout.Bands[0].Summaries.Clear();
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Summaries.Clear();
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].SummaryFooterCaption = "Totales: ";
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Override.SummaryFooterCaptionAppearance.BackColor = Color.Silver;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Override.SummaryFooterCaptionAppearance.ForeColor = Color.LightYellow;
                //dbugrHonorariosPorPagar.DisplayLayout.Bands[0].Summaries.Add(Infragistics.Win.UltraWinGrid.SummaryType.Sum, dbgrPagosFacMedicos.DisplayLayout.Bands[0].Columns["HOM_VALOR_NETO"]);
                //edito las columnas

                //añado las sumatorias a las columnas
                SummarySettings sumValorHonorario = dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Summaries.Add("Valor Honorario", SummaryType.Sum, dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_NETO"]);
                sumValorHonorario.DisplayFormat = "{0:#####.00}";
                sumValorHonorario.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                SummarySettings sumValorarPorPagar = dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Summaries.Add("Valor a Pagar", SummaryType.Sum, dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_TOTAL"]);
                sumValorarPorPagar.DisplayFormat = "{0:#####.00}";
                sumValorarPorPagar.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                SummarySettings sumValorTransferido = dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Summaries.Add("Valor Transferido", SummaryType.Sum, dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_CANCELADO"]);
                sumValorTransferido.DisplayFormat = "{0:#####.00}";
                sumValorTransferido.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                SummarySettings sumValorarPorTransferir = dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Summaries.Add("Valor por transferir", SummaryType.Sum, dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["VALOR_A_CANCELAR"]);
                sumValorarPorTransferir.DisplayFormat = "{0:#####.00}";
                sumValorarPorTransferir.Appearance.TextHAlign = Infragistics.Win.HAlign.Right;

                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["NOMBRE_MEDICO"].Header.Caption = "MEDICO";
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_FACTURA_MEDICO"].Header.Caption = "FACTURA";
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_FACTURA_FECHA"].Header.Caption = "FEC. FACTURA";
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_NETO"].Header.Caption = "HONORARIO";
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_TOTAL"].Header.Caption = "VALOR CON DESCUENTOS";
                ////dbugrHonorariosPorPagar.DisplayLayout.Bands[0].Columns["HOM_VALOR_PAGADO"].Header.Caption ="VALOR PAGADO"; 
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_CANCELADO"].Header.Caption = "VALOR TRANSFERIDO";
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["VALOR_A_CANCELAR"].Header.Caption = "VALOR A TRANSFERIR";

                //cambio a no editables los campos de consulta
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["NOMBRE_MEDICO"].CellActivation = Activation.NoEdit;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_FACTURA_MEDICO"].CellActivation = Activation.NoEdit;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_FACTURA_FECHA"].CellActivation = Activation.NoEdit;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_NETO"].CellActivation = Activation.NoEdit;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_TOTAL"].CellActivation = Activation.NoEdit;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_CANCELADO"].CellActivation = Activation.NoEdit;


                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["NOMBRE_MEDICO"].Hidden = true;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_VALOR_PAGADO"].Hidden = true;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["HOM_CODIGO"].Hidden = true;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["MED_CODIGO"].Hidden = true;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["FOR_CODIGO"].Hidden = true;
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["MED_CON_TRANSFERENCIA"].Hidden = true;

                //Añado la columna check
                //if (iniHonorariosPorPagar == false)
                if (!dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns.Exists("CHECKTRANS"))
                {
                    dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns.Add("CHECKTRANS", "");
                    dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["CHECKTRANS"].DataType = typeof(bool);
                    dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["CHECKTRANS"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
                    iniHonorariosPorPagar = true;
                }
                dbugrHonorariosPorPagar.DisplayLayout.Bands[1].Columns["CHECKTRANS"].Header.VisiblePosition = 0;

                for (Int16 i = 0; i < dbugrHonorariosPorPagar.Rows.Count; i++)
                {
                    for (Int16 j = 0; j < dbugrHonorariosPorPagar.Rows[i].ChildBands[0].Rows.Count; j++)
                    {
                        dbugrHonorariosPorPagar.Rows[i].ChildBands[0].Rows[j].Cells["CHECKTRANS"].Value = true;
                    }
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dbugrHonorariosPorPagar_InitializeGroupByRow_1(object sender, InitializeGroupByRowEventArgs e)
        {
            e.Row.ExpansionIndicator = Infragistics.Win.UltraWinGrid.ShowExpansionIndicator.Never;
            e.Row.Expanded = true;
        }




  }


}

