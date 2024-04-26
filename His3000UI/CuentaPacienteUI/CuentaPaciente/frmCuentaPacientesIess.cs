using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Formulario;
using His.Negocio;
using His.Parametros;
using Infragistics.Win.UltraWinGrid;
using Recursos;
using frm_Ayudas = His.Admision.frm_Ayudas;

namespace CuentaPaciente
{
    public partial class frmCuentaPacientesIess : Form
    {
        #region Declaracion de Variables
        public ListView listaDatos = null;
        public PACIENTES paciente;
        public PACIENTES_DATOS_ADICIONALES datos;
        ATENCIONES atencion = null;
        public int codigoAtencion;
        public List<PRODUCTO> listaProductosSolicitados;
        private byte codigoEstacion;
        public PRODUCTO producto;
        private string direccion;
        List<DtoCuentaCargas> listaCuentaCargas = new List<DtoCuentaCargas>();
        List<DtoDetalleCuentaPaciente> listaCuenta = new List<DtoDetalleCuentaPaciente>();
        ATENCIONES_DETALLE_SEGUROS atdSeguros = new ATENCIONES_DETALLE_SEGUROS();
        CUENTAS_PACIENTES cuentaPacientes = new CUENTAS_PACIENTES();
        CUENTAS_PACIENTES cuentaModificada = null;
        List<HC_CATALOGO_SUBNIVEL> listaCatSub;
        PEDIDOS_AREAS pedidosArea = new PEDIDOS_AREAS();
        private Boolean accionCuenta = false;
        private TARIFARIOS_DETALLE tarifarioHI;
        private TARIFARIOS_DETALLE tarifarioH;
        decimal unidadesUvr;
        decimal precioUnitario;
        private MEDICOS medico;
        private List<ATENCION_DETALLE_CATEGORIAS> detalleCategorias = null;
        private List<ASEGURADORAS_EMPRESAS> aseguradoras = new List<ASEGURADORAS_EMPRESAS>();
        private ATENCION_DETALLE_CATEGORIAS atencionDCategoria;
        Int32 NumeroFila = 0;
        Infragistics.Win.UltraWinGrid.UltraGridRow gridRow;

        #endregion

        #region Contructor
        public frmCuentaPacientesIess()
        {
            InitializeComponent();
            HabilitarBotones(true, false, false, true, true);

            //ultraTabControlCuenta.Enabled = true;
        }
        #endregion
        private void cargarProductos()
        {

            btnGuardar.Enabled = false;
            List<CUENTAS_PACIENTES> listaCuentasPacientes = new List<CUENTAS_PACIENTES>();
            ugrdCuenta.DataSource = null;
            try
            {
                if (txtAtencion.Text.Trim() != string.Empty || txtAtencion.Text.Trim() != "")
                {
                    pedidosArea = (PEDIDOS_AREAS)cmb_Areas.SelectedItem;
                    if (pedidosArea != null)
                    {
                        listaCuentasPacientes =
                            NegCuentasPacientes.RecuperarCuentaArea2(Convert.ToInt32(txtAtencion.Text.Trim()), Convert.ToInt32(pedidosArea.DIV_CODIGO));
                        if (listaCuentasPacientes.Count > 0)
                        {
                            ugrdCuenta.DataSource = listaCuentasPacientes;
                            UltraGridBand bandUno = ugrdCuenta.DisplayLayout.Bands[0];
                            SummarySettings sumTarifa = bandUno.Summaries.Add("CUE_VALOR", SummaryType.Sum,
                                                                              bandUno.Columns["CUE_VALOR"]);
                            if (NumeroFila != 0)
                            {
                                ugrdCuenta.Rows[NumeroFila].Activate();
                            }
                        }
                    }
                }

                //cargo los valores por defecto de la grilla
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }
        public void CargarPaciente(int codigo)
        {
            //paciente = new DtoPacientes();
            paciente = NegPacientes.RecuperarPacienteID(codigo);

            if (paciente != null)
            {

                txtPacienteNombre.Text = paciente.PAC_NOMBRE1;
                txtPacienteNombre2.Text = paciente.PAC_NOMBRE2;
                txtPacienteApellidoPaterno.Text = paciente.PAC_APELLIDO_PATERNO;
                txtPacienteApellidoMaterno.Text = paciente.PAC_APELLIDO_MATERNO;
                txtPacienteHCL.Text = paciente.PAC_HISTORIA_CLINICA;
                txtPacienteCedula.Text = paciente.PAC_IDENTIFICACION;
                datos = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(paciente.PAC_CODIGO);
                txtPacienteDireccion.Text = datos.DAP_DIRECCION_DOMICILIO;
                txtPacienteTelf.Text = datos.DAP_TELEFONO1;
                //CargarAtencion();
                CargarDatos();
                limpiarCampos();
            }
            else
            {
                txtPacienteNombre.Text = string.Empty;
                txtPacienteNombre2.Text = string.Empty;
                txtPacienteApellidoPaterno.Text = string.Empty;
                txtPacienteApellidoMaterno.Text = string.Empty;
                txtPacienteDireccion.Text = string.Empty;
                txtPacienteHCL.Text = string.Empty;
                txtPacienteTelf.Text = string.Empty;
                txtPacienteCedula.Text = string.Empty;
                datos = null;
                limpiarCampos();
            }

            //cargarFiltros();
        }

        private void CargarAtencion()
        {

            try
            {
                atencion = NegAtenciones.AtencionID(Convert.ToInt32(txtAtencion.Text.Trim()));
                if (atencion != null)
                {
                    atdSeguros = NegAtencionDetalleSeguros.RecuAtencionesDetalleSeguros(atencion.ATE_CODIGO);
                    if (atdSeguros != null)
                    {

                        //txt_nombreTitular.Text = atdSeguros.ADS_ASEGURADO_NOMBRE;
                        //txt_CedulaTitular.Text = atdSeguros.ADS_ASEGURADO_CEDULA;
                        //txt_CiudadTitular.Text = atdSeguros.ADS_ASEGURADO_CIUDAD;
                        //txt_TelefonoTitular.Text = atdSeguros.ADS_ASEGURADO_TELEFONO;
                        //txt_DireccionTitular.Text = atdSeguros.ADS_ASEGURADO_DIRECCION;
                    }
                    else
                    {
                        //txt_nombreTitular.Text = string.Empty;
                        //txt_CedulaTitular.Text = string.Empty;
                        //txt_CiudadTitular.Text = string.Empty;
                        //txt_TelefonoTitular.Text = string.Empty;
                        //txt_DireccionTitular.Text = string.Empty;
                    }
                    CargarConvenio();
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); }

        }

        private void CargarConvenio()
        {
            //ATENCION_DETALLE_CATEGORIAS
            List<ASEGURADORAS_EMPRESAS> listaAseguradoras = new List<ASEGURADORAS_EMPRESAS>();
            detalleCategorias = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasAtencion(atencion.ATE_CODIGO);
            Int16 CategoriaId = 0; // Variable para cargar la Aseguradora (IESS/SOAT) / Giovanny Tapia /30/08/2012
            if (detalleCategorias != null)
            {
                foreach (ATENCION_DETALLE_CATEGORIAS detalle in detalleCategorias)
                {
                    if (detalle != null)
                    {
                        if ((Convert.ToInt32(detalle.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value) == 21) || (Convert.ToInt32(detalle.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value) == 125) || (Convert.ToInt32(detalle.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value) == 54) || (Convert.ToInt32(detalle.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value) == 62)) // Convenio 21=IESS /125 soat (Aumento 125 para que se cargue aseguradoras SOAT )(Aumento 54 para que se cargue aseguradoras MINISTERIO DE SALUD ) /Giovanny Tapia / 26/11/2012
                        {
                            CategoriaId = (Convert.ToInt16(detalle.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value));
                            txtConvenio.Text =
                                //((CATEGORIAS_CONVENIOS) (NegCategorias.RecuperaCategoriaID(21))).CAT_NOMBRE;                     
                                ((CATEGORIAS_CONVENIOS)(NegCategorias.RecuperaCategoriaID(CategoriaId))).CAT_NOMBRE; //Busco el nombre de la aseguradora apartir del codigo / Giovanny Tapia / 30/08/2012

                            ultraTabControlCuenta.Enabled = true;
                            break;
                        }
                        else
                        { //++++++++++++++++++++++++++++++++++++++++
                            //antes Edgar 20210105
                            //txtConvenio.Text = String.Empty;
                            //ultraTabControlCuenta.Enabled = false;
                            //+++++++++++++++++++++++++++++++++++++++
                            txtConvenio.Text =
                               //((CATEGORIAS_CONVENIOS) (NegCategorias.RecuperaCategoriaID(21))).CAT_NOMBRE;                     
                               ((CATEGORIAS_CONVENIOS)(NegCategorias.RecuperaCategoriaID(CategoriaId))).CAT_NOMBRE;
                            ultraTabControlCuenta.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                txtConvenio.Text = String.Empty;
                ultraTabControlCuenta.Enabled = false;
            }
        }


        #region Cargar Datos  public void cargarFiltros()


        public void CargarDatos()
        {
            ultraTabControlCuenta.Enabled = true;
            cmb_Areas.DataSource = NegPedidos.recuperarListaAreas().OrderBy(a => a.PEA_NOMBRE).ToList();
            cmb_Areas.ValueMember = "PEA_CODIGO";
            cmb_Areas.DisplayMember = "PEA_NOMBRE";
            cmb_Areas.SelectedIndex = 0;
            listaCatSub = NegHcCatalogoSubNivel.RecuperarSubNivel(40);
            //cmb_Parentesco.DataSource = listaCatSub;
            //cmb_Parentesco.DisplayMember = "CA_DESCRIPCION";
            //cmb_Parentesco.ValueMember = "CA_CODIGO";
            //cmb_Parentesco.AutoCompleteSource = AutoCompleteSource.ListItems;
            //cmb_Parentesco.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCantidad.Text = "1";
            ArchivoIni archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
            byte codigoEstacion = Convert.ToByte(archivo.IniReadValue("Pedidos", "estacion"));
        }

        public void HabilitarBotones(Boolean editar, Boolean guardar, Boolean cancelar, Boolean salir, Boolean cerrar)
        {
            btnActualizar.Enabled = editar;
            btnGuardar.Enabled = guardar;
            btnCancelar.Enabled = cancelar;
            btnSalir.Enabled = salir;
            //btnEditar.Enable = editar;
            btnCerrar.Enabled = cerrar;
            gpbDatos.Enabled = true;
        }

        public void GuardarDatos()
        {
            if (!ValidarCampos())
            {
                if (ugrdCuenta.Rows.Count > 0)
                {
                    List<CUENTAS_PACIENTES> listaCuentas = new List<CUENTAS_PACIENTES>();

                }
            }
        }

        public bool ValidarCampos()
        {
            try
            {
                erroresAtencion.Clear();
                bool validoCampo = true;
                if (ugrdCuenta.Rows.Count <= 0)
                {
                    AgregarError(ugrdCuenta);
                    validoCampo = false;
                }
                return validoCampo;
            }
            catch (Exception err) { MessageBox.Show(err.Message); return false; }
        }

        private void AgregarError(Control control)
        {
            erroresAtencion.SetError(control, "Campo Requerido");
        }


        private void limpiarCampos()
        {
            cmb_Areas.Enabled = true;
            cuentaModificada = null;
            txt_Codigo.Text = string.Empty;
            txtCodigoSic.Text = string.Empty;
            txt_Nombre.Text = string.Empty;
            txtValorNeto.Text = "0.00";
            txtCantidad.Text = "1";
            txtTotal.Text = "0.00";

            if (chkNumeroControl.Checked != true)
            {
                txtCControl.Text = string.Empty;
            }


            txtCodMedico.Text = string.Empty;
            txtNombreMedico.Text = string.Empty;
            precioUnitario = 0;
            txtPorcentaje.Text = "100";
            rdb_SinIva.Checked = true;
            rdb_ConIva.Enabled = false;
            rdb_SinIva.Enabled = false;

            cmbTipoMedico.SelectedIndex = -1;

        }


        #endregion

        #region Eventos
        //public void cargarFiltros()
        //{
        //    HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM); 
        //    var tarifarioQuery = from t in contexto.TARIFARIOS.Include("ESPECIALIDADES_TARIFARIOS")
        //                         orderby t.TAR_CODIGO
        //                         select t;
        //    try
        //    {
        //        // lleno la lista de tarifarios
        //        this.tarifarioList.DataSource = tarifarioQuery;
        //        this.tarifarioList.DisplayMember = "TAR_NOMBRE";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    ultraGrid.DataSource = null;
        //    ultraGrid.ClearUndoHistory();
        //    ultraGrid.DataMember = null;
        //}




        //public void cargarAseguradoras(Int16 codigoTarifario)
        //{
        //    try
        //    {
        //        HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
        //        var aseguradorasQuery = (from a in contexto.ASEGURADORAS_EMPRESAS
        //                                 join c in contexto.CONVENIOS_TARIFARIOS on a.ASE_CODIGO equals c.ASEGURADORAS_EMPRESAS.ASE_CODIGO
        //                                 join e in contexto.ESPECIALIDADES_TARIFARIOS on c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO equals e.EST_CODIGO
        //                                 where a.ASE_ESTADO == true && e.TARIFARIOS.TAR_CODIGO == codigoTarifario
        //                                 select a).Distinct().OrderBy(a => a.ASE_NOMBRE).ToList();
        //        this.aseguradoraList.DataSource = null;
        //        this.aseguradoraList.DataSource = aseguradorasQuery;
        //        this.aseguradoraList.DisplayMember = "ASE_NOMBRE";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void txtCodigoPaciente_TextChanged(object sender, EventArgs e)
        {

            btnActualizar.Enabled = true;

            btnCerrar.Enabled = true;
            try
            {
                if (Microsoft.VisualBasic.Information.IsNumeric(txtCodigoPaciente.Text) == false)
                    txtCodigoPaciente.Text = string.Empty;

                if (txtCodigoPaciente.Text != string.Empty)
                {
                    CargarPaciente(Convert.ToInt32(txtCodigoPaciente.Text.ToString()));

                    //if ((bool)txtCodigoPaciente.Tag)
                    //    cargarDatosPorPaciente(Convert.ToInt32(txtCodigoPaciente.Text.ToString()));
                }
                else
                {
                    paciente = null;
                    txtPacienteNombre.Text = string.Empty;
                    txtPacienteNombre2.Text = string.Empty;
                    txtPacienteApellidoPaterno.Text = string.Empty;
                    txtPacienteApellidoMaterno.Text = string.Empty;
                    txtPacienteDireccion.Text = string.Empty;
                    txtPacienteHCL.Text = string.Empty;
                    txtPacienteTelf.Text = string.Empty;
                    txtPacienteCedula.Text = string.Empty;

                    txtPacienteNombre.ReadOnly = false;
                    txtPacienteNombre2.ReadOnly = false;
                    txtPacienteApellidoPaterno.ReadOnly = false;
                    txtPacienteApellidoMaterno.ReadOnly = false;
                    txtPacienteDireccion.ReadOnly = false;
                    txtPacienteHCL.ReadOnly = false;
                    txtPacienteTelf.ReadOnly = false;
                    txtPacienteCedula.ReadOnly = false;

                }
            }
            catch (Exception err)
            {

            }
        }

        private void txtAtencion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Microsoft.VisualBasic.Information.IsNumeric(txtAtencion.Text) == false)
                    txtAtencion.Text = string.Empty;

                if (txtAtencion.Text != string.Empty)
                {
                    CargarAtencion();
                    cargarProductos();
                }
                else
                {


                }
            }
            catch (Exception err)
            {

            }

        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            try
            {
                string a = "";
                frmAyudaPacientesIess frm = new frmAyudaPacientesIess();
                frm.campoPadre = txtCodigoPaciente;
                frm.campoAtencion = txtAtencion;
                frm.campoHabitacion = txtHabitacion;
                frm.txtfactura = txtFactura;
                frm.txtNumControl = txtNumControl;
                frm.fechafacturacion = dateTimePickerFechaFactura;
                frm.fechaingreso = dateTimePickerFechaIngreso;
                frm.fechaalta = dateTimePickerFechaAlta;
                frm.ShowDialog();
                HabilitarBotones(true, true, true, true, true);
                if (txtAtencion.Text != "")
                    NumeroFila = 0;
                cargarProductos();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.InnerException.Message);
            }
        }


        private void tarifarioList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TARIFARIOS tarifario = (TARIFARIOS)this.tarifarioList.SelectedItem;
            //cargarEspecialidadesTarifario(tarifario);
            //cargarAseguradoras(tarifario.TAR_CODIGO);
            //if (this.Visible == true)
            //{
            //    //cargarDetalleTarifario();
            //}
        }

        private void btnSolicitar_Click(object sender, EventArgs e)
        {

        }

        private void cmb_Areas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmb_Areas_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                cmb_Rubros.DataSource = null;
                if (cmb_Areas.SelectedItem != null)
                {

                    if (this.chkNumeroControl.Checked == true)
                    {
                        this.chkNumeroControl.Checked = false;
                    }

                    limpiarCampos();
                    PEDIDOS_AREAS areaP = (PEDIDOS_AREAS)cmb_Areas.SelectedItem;
                    List<RUBROS> listaRubros = NegRubros.recuperarRubros(Convert.ToInt32(areaP.DIV_CODIGO));
                    if (areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoHonorarios)
                        cmb_Rubros.DataSource = listaRubros.OrderByDescending(pa => pa.RUB_NOMBRE.Trim()).ToList();
                    else
                        cmb_Rubros.DataSource = listaRubros.OrderBy(pa => pa.RUB_NOMBRE.Trim()).ToList();
                    if (areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoLaboratorioP || areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoHonorarios)
                    {
                        txtCodMedico.Text = string.Empty;
                        txtNombreMedico.Text = string.Empty;
                        btnMedico.Enabled = true;
                        txtCodMedico.Enabled = true;
                        this.cmbTipoMedico.Enabled = true;
                    }
                    else
                    {
                        txtCodMedico.Text = string.Empty;
                        txtNombreMedico.Text = string.Empty;
                        btnMedico.Enabled = false;
                        txtCodMedico.Enabled = false;
                        this.cmbTipoMedico.Enabled = false;
                        if (areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoOtrosP || areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
                        {
                            rdb_ConIva.Enabled = true;
                            rdb_SinIva.Enabled = true;
                        }
                    }

                    cmb_Rubros.DisplayMember = "RUB_NOMBRE".Trim();
                    cmb_Rubros.ValueMember = "RUB_CODIGO";
                    NumeroFila = 0;
                    cargarProductos();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void cmb_Rubros_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    cmb_Rubros.DataSource = null;
            //    if (cmb_Rubros.SelectedItem != null)
            //    {
            //        RUBROS rubroC = (RUBROS)cmb_Rubros.SelectedItem;
            //        if (rubroC.RUB_CODIGO == His.Parametros.CuentasPacientes.RubroOtrosP || rubroC.RUB_CODIGO == His.Parametros.CuentasPacientes.RubroSuministros) 
            //        {
            //        }
            //    }
            //}
            //catch (Exception err)
            //{
            //    MessageBox.Show(err.Message);
            //}
        }

        //private void btnExaminar_Click(object sender, EventArgs e)
        //{
        //    openFileDialogo.InitialDirectory = "c:\\";
        //    openFileDialogo.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
        //    openFileDialogo.FilterIndex = 2;
        //    openFileDialogo.RestoreDirectory = true;

        //    if (openFileDialogo.ShowDialog() == DialogResult.OK)
        //    {
        //        txtDirArchivo.Text = openFileDialogo.FileName;

        //    }
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    ugrdCuenta.DataSource = null;
        //    try
        //    {
        //        var listaFilas = ImportarArchivoDelimitado(txtDirArchivo.Text.Trim(), new char[] { ';' });

        //        ugrdCuenta.DataSource = listaFilas.Select(c => new CUENTAS_PACIENTES()
        //        {
        //            ATE_CODIGO = Convert.ToInt32(txtAtencion.Text),
        //            CUE_FECHA = Convert.ToDateTime(c[0]),
        //            PRO_CODIGO = (c[1]).ToString(),
        //            CUE_DETALLE = (c[2]).ToString(),
        //            CUE_VALOR_UNITARIO = Convert.ToDecimal(c[3]),
        //            CUE_CANTIDAD = Convert.ToByte(c[4]),
        //            CUE_VALOR = Convert.ToDecimal(c[5]),
        //            CUE_IVA = Convert.ToDecimal(0.00),
        //            CUE_ESTADO = 1,
        //            CUE_NUM_FAC = "0",
        //            RUB_CODIGO =
        //                ((RUBROS)(cmb_Rubros.SelectedItem)).
        //                RUB_CODIGO,
        //            PED_CODIGO =
        //                ((PEDIDOS_AREAS)(cmb_Areas.SelectedItem))
        //                .
        //                PEA_CODIGO,
        //            ID_USUARIO =
        //                His.Entidades.Clases.Sesion.codUsuario,
        //            CAT_CODIGO = 0
        //        }).ToList();
        //        UltraGridBand bandUno = ugrdCuenta.DisplayLayout.Bands[0];
        //        SummarySettings sumTarifa = bandUno.Summaries.Add("CUE_VALOR", SummaryType.Sum,
        //                                                          bandUno.Columns["CUE_VALOR"]);
        //        MessageBox.Show("Archivo cargado  exitosamente");
        //        btnGuardar.Enabled = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("No se pudo  cargar  archivo ");
        //        btnGuardar.Enabled = false;
        //    }
        //    //SummarySettings sumTarifa = ultraGrid.DisplayLayout.Bands[Num_banda].Summaries.Add("NOMBRE_COLUMNA", SummaryType.Sum, bandUno.Columns["NOMBRE_COLUMNA"]);
        //}

        public List<string[]> ImportarArchivoDelimitado(string archivo, char[] separador)
        {
            var listaFilas = new List<string[]>();
            if (txtDirArchivo.Text.Trim() != string.Empty)
            {
                try
                {
                    using (var file = new StreamReader(archivo))
                    {
                        string linea;
                        while ((linea = file.ReadLine()) != null)
                        {
                            if (linea.Trim().Length > 0)
                            {
                                string[] columns = linea.Split(separador, StringSplitOptions.RemoveEmptyEntries);
                                listaFilas.Add(columns);
                            }
                        }


                    }

                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message + "Formato de archivo incorrecto");
                }
            }
            return listaFilas;
        }

        private void ultraGrid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            if (!ugrdCuenta.DisplayLayout.Bands[0].Columns.Exists("CHECKTRANS"))
            {
                ugrdCuenta.DisplayLayout.Bands[0].Columns.Add("CHECKTRANS", "");
                ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].DataType = typeof(bool);
                ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
                ugrdCuenta.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;

            }
            ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].Header.VisiblePosition = 0;
            ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].CellActivation = Activation.AllowEdit;
            ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].DefaultCellValue = true;

            UltraGridBand bandUno = ugrdCuenta.DisplayLayout.Bands[0];
            //ugrdCuenta.DisplayLayout.ViewStyle = ViewStyle.SingleBand;

            //ugrdCuenta.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            //ugrdCuenta.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
            //ugrdCuenta.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            //bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

            //bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            //bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

            //ugrdCuenta.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
            //ugrdCuenta.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
            //ugrdCuenta.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

            //Caracteristicas de Filtro en la grilla
            ugrdCuenta.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
            ugrdCuenta.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
            ugrdCuenta.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
            ugrdCuenta.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
            ugrdCuenta.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

            ugrdCuenta.DisplayLayout.UseFixedHeaders = true;

            bandUno.Columns["CUE_CODIGO"].Header.Caption = "CODIGO";
            bandUno.Columns["ATE_CODIGO"].Header.Caption = "ATENCION";
            bandUno.Columns["CUE_FECHA"].Header.Caption = "FECHA";
            bandUno.Columns["PRO_CODIGO"].Header.Caption = "CODIGO_PRO";
            bandUno.Columns["CUE_DETALLE"].Header.Caption = "DESCRIPCION";
            bandUno.Columns["CUE_VALOR_UNITARIO"].Header.Caption = "TOTAL_UNITARIO";
            bandUno.Columns["CUE_CANTIDAD"].Header.Caption = "CANTIDAD";
            bandUno.Columns["CUE_VALOR"].Header.Caption = "TOTAL";
            bandUno.Columns["CUE_IVA"].Header.Caption = "IVA";
            bandUno.Columns["CUE_ESTADO"].Header.Caption = "ESTADO";
            bandUno.Columns["CUE_NUM_FAC"].Header.Caption = "N. FACTURA";
            bandUno.Columns["RUB_CODIGO"].Header.Caption = "RUBRO";
            bandUno.Columns["PED_CODIGO"].Header.Caption = "PEDIDO";
            bandUno.Columns["ID_USUARIO"].Header.Caption = "USUARIO";
            bandUno.Columns["CAT_CODIGO"].Header.Caption = "CATALOGO";
            bandUno.Columns["PRO_CODIGO_BARRAS"].Header.Caption = "CODIGO";
            bandUno.Columns["CUE_NUM_CONTROL"].Header.Caption = "NUM_CONTROL";
            bandUno.Columns["Id_Tipo_Medico"].Header.Caption = "Id_Tipo_Medico";

            bandUno.Columns["CUE_CODIGO"].Width = 80;
            bandUno.Columns["ATE_CODIGO"].Width = 80;
            bandUno.Columns["CUE_FECHA"].Width = 80;
            bandUno.Columns["PRO_CODIGO"].Width = 80;
            bandUno.Columns["CUE_DETALLE"].Width = 280;
            bandUno.Columns["CUE_VALOR_UNITARIO"].Width = 80;
            bandUno.Columns["CUE_CANTIDAD"].Width = 80;
            bandUno.Columns["CUE_VALOR"].Width = 80;
            bandUno.Columns["CUE_IVA"].Width = 50;
            bandUno.Columns["CUE_ESTADO"].Width = 80;
            bandUno.Columns["CUE_NUM_FAC"].Width = 80;
            bandUno.Columns["RUB_CODIGO"].Width = 80;
            bandUno.Columns["PED_CODIGO"].Width = 80;
            bandUno.Columns["ID_USUARIO"].Width = 80;
            bandUno.Columns["CAT_CODIGO"].Width = 80;
            bandUno.Columns["PRO_CODIGO_BARRAS"].Width = 80;
            bandUno.Columns["CUE_NUM_CONTROL"].Width = 80;
            bandUno.Columns["Id_Tipo_Medico"].Width = 80;

            bandUno.Columns["CUE_CODIGO"].Hidden = true;
            bandUno.Columns["ATE_CODIGO"].Hidden = false;
            bandUno.Columns["CUE_FECHA"].Hidden = false;
            bandUno.Columns["PRO_CODIGO"].Hidden = false;
            bandUno.Columns["CUE_DETALLE"].Hidden = false;
            bandUno.Columns["CUE_VALOR_UNITARIO"].Hidden = false;
            bandUno.Columns["CUE_CANTIDAD"].Hidden = false;
            bandUno.Columns["CUE_VALOR"].Hidden = false;
            bandUno.Columns["CUE_IVA"].Hidden = false;
            bandUno.Columns["CUE_ESTADO"].Hidden = true;
            bandUno.Columns["CUE_NUM_FAC"].Hidden = false;
            bandUno.Columns["RUB_CODIGO"].Hidden = true;
            bandUno.Columns["PED_CODIGO"].Hidden = true;
            bandUno.Columns["ID_USUARIO"].Hidden = true;
            bandUno.Columns["CAT_CODIGO"].Hidden = true;
            bandUno.Columns["PRO_CODIGO_BARRAS"].Hidden = false;
            bandUno.Columns["CUE_NUM_CONTROL"].Hidden = false;
            bandUno.Columns["Id_Tipo_Medico"].Hidden = true;

            //ordeno las columnas
            bandUno.Columns["CUE_CODIGO"].Header.VisiblePosition = 0;
            bandUno.Columns["ATE_CODIGO"].Header.VisiblePosition = 1;
            bandUno.Columns["CUE_FECHA"].Header.VisiblePosition = 2;
            bandUno.Columns["PRO_CODIGO_BARRAS"].Header.VisiblePosition = 3;
            bandUno.Columns["CUE_DETALLE"].Header.VisiblePosition = 4;
            bandUno.Columns["CUE_VALOR_UNITARIO"].Header.VisiblePosition = 5;
            bandUno.Columns["CUE_CANTIDAD"].Header.VisiblePosition = 6;
            bandUno.Columns["CUE_VALOR"].Header.VisiblePosition = 7;
            bandUno.Columns["CUE_IVA"].Header.VisiblePosition = 8;
            bandUno.Columns["CUE_NUM_CONTROL"].Header.VisiblePosition = 9;
            bandUno.Columns["CUE_ESTADO"].Header.VisiblePosition = 10;
            bandUno.Columns["CUE_NUM_FAC"].Header.VisiblePosition = 11;
            bandUno.Columns["RUB_CODIGO"].Header.VisiblePosition = 12;
            bandUno.Columns["PED_CODIGO"].Header.VisiblePosition = 13;
            bandUno.Columns["ID_USUARIO"].Header.VisiblePosition = 14;
            bandUno.Columns["CAT_CODIGO"].Header.VisiblePosition = 15;
            bandUno.Columns["PRO_CODIGO"].Header.VisiblePosition = 16;
            //bandUno.Columns["Id_Tipo_Medico"].Header.VisiblePosition = 16;   
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (this.accionCuenta == true)
            {
                atencion.ATE_FECHA_ALTA = dateTimePickerFechaAlta.Value;
                NegAtenciones.EditarAtencionAdmision(atencion,2);
                NegCuentasPacientes.ActualizarFechaCuentas(atencion.ATE_CODIGO);
                MessageBox.Show("Atención Actualizada  Exitosamente");
                this.dateTimePickerFechaAlta.Enabled = false;
                this.accionCuenta = false;
            }
            else
            {
                if (ValidarCampos())
                {
                    RUBROS rubroM = new RUBROS();
                    RUBROS rubroS = new RUBROS();
                    if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
                    {
                        rubroM = (RUBROS)(cmb_Rubros.SelectedItem);
                        cmb_Rubros.SelectedIndex = 1;
                        rubroS = (RUBROS)(cmb_Rubros.SelectedItem);
                        cmb_Rubros.SelectedIndex = 0;
                    }
                    foreach (UltraGridRow cuenta in ugrdCuenta.Rows)
                    {
                        cuentaPacientes = new CUENTAS_PACIENTES();
                        cuentaPacientes.ATE_CODIGO = Convert.ToInt32(cuenta.Cells[1].Value);
                        cuentaPacientes.CUE_FECHA = Convert.ToDateTime(cuenta.Cells[2].Value);
                        cuentaPacientes.PRO_CODIGO_BARRAS = (cuenta.Cells[15].Value).ToString();
                        cuentaPacientes.CUE_DETALLE = (cuenta.Cells[3].Value).ToString();
                        cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(cuenta.Cells[14].Value);
                        cuentaPacientes.CUE_CANTIDAD = Convert.ToByte(cuenta.Cells[8].Value);
                        cuentaPacientes.CUE_VALOR = Convert.ToDecimal(cuentaPacientes.CUE_VALOR_UNITARIO * cuentaPacientes.CUE_CANTIDAD);
                        cuentaPacientes.CUE_ESTADO = Convert.ToInt32(cuenta.Cells[6].Value);
                        cuentaPacientes.CUE_NUM_FAC = (cuenta.Cells[7].Value).ToString();
                        cuentaPacientes.CUE_NUM_CONTROL = (cuenta.Cells[16].Value).ToString();
                        cuentaPacientes.PRO_CODIGO = "0";
                        cuentaPacientes.MED_CODIGO = 0;
                        if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
                        {
                            if ((cuenta.Cells[15].Value).ToString().Substring(0, 1).Equals("2"))
                            {
                                cuentaPacientes.RUB_CODIGO = rubroS.RUB_CODIGO;
                                //cambio hr 23052016
                                //string ivaTotal = Convert.ToString(Convert.ToDouble(cuentaPacientes.CUE_VALOR) * 0.12);
                                string ivaTotal = Convert.ToString(Convert.ToDouble(cuentaPacientes.CUE_VALOR) * 0.14);
                                cuentaPacientes.CUE_IVA = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
                                //cuentaPacientes.CUE_IVA = Math.Round(Convert.ToDecimal((cuentaPacientes.CUE_VALOR) * Convert.ToDecimal(0.12)),3);
                            }
                            else
                            {
                                cuentaPacientes.RUB_CODIGO = rubroM.RUB_CODIGO;
                                cuentaPacientes.CUE_IVA = 0;
                            }
                        }
                        else
                        {
                            cuentaPacientes.RUB_CODIGO = Convert.ToInt16(cuenta.Cells[9].Value);
                            cuentaPacientes.CUE_IVA = 0;
                        }
                        cuentaPacientes.PED_CODIGO = Convert.ToInt32(cuenta.Cells[10].Value);
                        cuentaPacientes.ID_USUARIO = Convert.ToInt16(cuenta.Cells[11].Value);
                        cuentaPacientes.CAT_CODIGO = Convert.ToInt16(cuenta.Cells[12].Value);
                        NegCuentasPacientes.CrearCuenta(cuentaPacientes);
                    }
                    //NegCuentasPacientes.CrearCuenta(cuentaPacientes);
                    CalcularIvaSuministros(Convert.ToInt32(txtAtencion.Text.Trim()));
                    MessageBox.Show("Datos Almacenados Correctamente");

                    txtDirArchivo.Text = string.Empty;
                    //ultraTabControlCuenta.Enabled = false;
                    cargarProductos();
                }

            }

            HabilitarBotones(true, false, false, true, true);
        }


        private void CalcularIvaSuministros(int codigoAtencion)
        {
            CUENTAS_PACIENTES cuentaIva = new CUENTAS_PACIENTES();
            cuentaIva = NegCuentasPacientes.RecuperarCuentasIvaS(codigoAtencion, His.Parametros.CuentasPacientes.CodigoSuministros);
            List<CUENTAS_PACIENTES> listaCuentaRubro = NegCuentasPacientes.RecuperarCuentasRubros(codigoAtencion,
                                                                          His.Parametros.CuentasPacientes.
                                                                              RubroSuministros);
            List<CUENTAS_PACIENTES> listaCuentaRubroO = NegCuentasPacientes.RecuperarCuentasRubros(codigoAtencion,
                                                                          His.Parametros.CuentasPacientes.RubroOtrosP);
            if (listaCuentaRubro.Count > 0)
            {
                Decimal iva = 0;
                CUENTAS_PACIENTES cuentas = new CUENTAS_PACIENTES();
                for (int i = 0; i < listaCuentaRubro.Count; i++)
                {
                    cuentas = listaCuentaRubro.ElementAt(i);
                    if (cuentas.CUE_ESTADO == 1)
                    {
                        iva = iva + Convert.ToDecimal(cuentas.CUE_VALOR);
                    }
                }
                if (listaCuentaRubroO.Count > 0)
                {
                    for (int i = 0; i < listaCuentaRubroO.Count; i++)
                    {
                        cuentas = listaCuentaRubroO.ElementAt(i);
                        if (cuentas.CUE_IVA > 0)
                        {
                            if (cuentas.CUE_ESTADO == 1)
                            {
                                iva = iva + Convert.ToDecimal(cuentas.CUE_VALOR);
                            }
                        }
                    }
                }
                if (cuentaIva == null)
                {
                    cuentaPacientes = new CUENTAS_PACIENTES();
                    cuentaPacientes.ATE_CODIGO = codigoAtencion;
                    if (atencion.ATE_FECHA_INGRESO < atencion.ATE_FECHA_ALTA)
                        cuentaPacientes.CUE_FECHA = atencion.ATE_FECHA_ALTA;
                    else
                        cuentaPacientes.CUE_FECHA = atencion.ATE_FECHA_INGRESO;
                    cuentaPacientes.PRO_CODIGO_BARRAS = His.Parametros.CuentasPacientes.CodigoSuministros;
                    cuentaPacientes.CUE_DETALLE = "IVA 14% SUMINISTROS";
                    //cambio hr 23052016
                    //string ivaTotal = Convert.ToString(Convert.ToDouble(iva) * 0.12);
                    string ivaTotal = Convert.ToString(Convert.ToDouble(iva) * 0.14);
                    cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
                    cuentaPacientes.CUE_CANTIDAD = 1;
                    cuentaPacientes.CUE_VALOR = cuentaPacientes.CUE_VALOR_UNITARIO;
                    //(iva * Convert.ToDecimal(0.12));
                    cuentaPacientes.CUE_ESTADO = 1;
                    cuentaPacientes.CUE_NUM_FAC = "0";
                    cuentaPacientes.CUE_NUM_CONTROL = "0";
                    cuentaPacientes.PRO_CODIGO = "0";
                    cuentaPacientes.RUB_CODIGO = 26;
                    cuentaPacientes.CUE_IVA = 0;
                    cuentaPacientes.PED_CODIGO = Convert.ToInt32(His.Parametros.CuentasPacientes.CodigoServicosI);
                    cuentaPacientes.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    cuentaPacientes.CAT_CODIGO = 0;
                    cuentaPacientes.MED_CODIGO = 0; //----->> Para la relacion con los medicos. / Giovanny Tapia / 05/09/2012
                    NegCuentasPacientes.CrearCuenta(cuentaPacientes);
                }
                else
                {
                    if (atencion.ATE_FECHA_INGRESO < atencion.ATE_FECHA_ALTA)
                        cuentaIva.CUE_FECHA = atencion.ATE_FECHA_ALTA;
                    else
                        cuentaIva.CUE_FECHA = atencion.ATE_FECHA_INGRESO;
                    //cambio hr 23052016
                    ///string ivaTotal = Convert.ToString(Convert.ToDouble(iva) * 0.12);
                    string ivaTotal = Convert.ToString(Convert.ToDouble(iva) * 0.14);
                    //cuentaIva.CUE_VALOR = (iva * Convert.ToDecimal(0.12));
                    cuentaIva.CUE_VALOR = (iva * Convert.ToDecimal(0.14));
                    cuentaIva.CUE_VALOR_UNITARIO = cuentaIva.CUE_VALOR;
                    cuentaIva.MED_CODIGO = 0; //----->> Para la relacion con los medicos. / Giovanny Tapia / 05/09/2012
                    NegCuentasPacientes.ModificarCuenta(cuentaIva);

                }
            }

        }

        private void ActualizaFechaCuentas()
        {

            List<CUENTAS_PACIENTES> listaCuentasP = new List<CUENTAS_PACIENTES>();
            listaCuentasP = NegCuentasPacientes.RecuperarCuenta(Convert.ToInt32(this.txtAtencion.Text.Trim()));



        }

        //private void CalcularIvaOtrosP(int codigoAtencion)
        //{
        //    CUENTAS_PACIENTES cuentaIva = new CUENTAS_PACIENTES();
        //    cuentaIva = NegCuentasPacientes.RecuperarCuentasIvaS(codigoAtencion, His.Parametros.CuentasPacientes.CodigoSuministros);
        //    List<CUENTAS_PACIENTES> listaCuentaRubro = NegCuentasPacientes.RecuperarCuentasRubros(codigoAtencion,
        //                                                                  His.Parametros.CuentasPacientes.RubroOtrosP);            if (listaCuentaRubro.Count > 0)
        //    {
        //        Decimal iva = 0;
        //        CUENTAS_PACIENTES cuentas = new CUENTAS_PACIENTES();
        //        for (int i = 0; i < listaCuentaRubro.Count; i++)
        //        {
        //            cuentas = listaCuentaRubro.ElementAt(i);
        //            if (cuentas.CUE_IVA > 0)
        //            {
        //                iva = iva + Convert.ToDecimal(cuentas.CUE_VALOR);
        //            }
        //        }
        //        if (cuentaIva == null)
        //        {
        //            cuentaPacientes = new CUENTAS_PACIENTES();
        //            cuentaPacientes.ATE_CODIGO = codigoAtencion;
        //            if (atencion.ATE_FECHA_INGRESO < atencion.ATE_FECHA_ALTA)
        //                cuentaPacientes.CUE_FECHA = atencion.ATE_FECHA_ALTA;
        //            else
        //                cuentaPacientes.CUE_FECHA = atencion.ATE_FECHA_INGRESO;
        //            cuentaPacientes.PRO_CODIGO_BARRAS = His.Parametros.CuentasPacientes.CodigoSuministros;
        //            cuentaPacientes.CUE_DETALLE = "IVA 12% SUMINISTROS";
        //            cuentaPacientes.CUE_VALOR_UNITARIO = (iva * Convert.ToDecimal(0.12));
        //            cuentaPacientes.CUE_CANTIDAD = 1;
        //            cuentaPacientes.CUE_VALOR = (iva * Convert.ToDecimal(0.12));
        //            cuentaPacientes.CUE_ESTADO = 1;
        //            cuentaPacientes.CUE_NUM_FAC = "0";
        //            cuentaPacientes.CUE_NUM_CONTROL = "0";
        //            cuentaPacientes.PRO_CODIGO = "0";
        //            cuentaPacientes.RUB_CODIGO = 26;
        //            cuentaPacientes.CUE_IVA = 0;
        //            cuentaPacientes.PED_CODIGO = Convert.ToInt32(His.Parametros.CuentasPacientes.CodigoServicosI);
        //            cuentaPacientes.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
        //            cuentaPacientes.CAT_CODIGO = 0;
        //            NegCuentasPacientes.CrearCuenta(cuentaPacientes);
        //        }
        //        else
        //        {
        //            if (atencion.ATE_FECHA_INGRESO < atencion.ATE_FECHA_ALTA)
        //                cuentaIva.CUE_FECHA = atencion.ATE_FECHA_ALTA;
        //            else
        //                cuentaIva.CUE_FECHA = atencion.ATE_FECHA_INGRESO;
        //            cuentaIva.CUE_VALOR = (iva * Convert.ToDecimal(0.12));
        //            cuentaIva.CUE_VALOR_UNITARIO = (iva * Convert.ToDecimal(0.12));
        //            NegCuentasPacientes.ModificarCuenta(cuentaIva);
        //        }
        //    }

        //}


        #endregion

        private void btnSolicitar_Click_1(object sender, EventArgs e)
        {
            DataGridView listas = new DataGridView();
            RUBROS rubro = (RUBROS)(cmb_Rubros.SelectedItem);
            PEDIDOS_AREAS pedidosAreas = (PEDIDOS_AREAS)(cmb_Areas.SelectedItem);
            if (pedidosAreas.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoHonorarios || pedidosAreas.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoImagen || pedidosAreas.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoServicosI)
            {
                frmAyudaTarifarios frm = new frmAyudaTarifarios(txtCodMedico.Text.Trim(), dateTimePickerFechaAlta);
                frm.ShowDialog();
                listas = frm.lista;
                if (listas != null)
                {
                    btnGuardar.Enabled = true;
                    for (Int16 i = 0; i <= listas.RowCount - 2; i++)
                    {
                        DataGridViewRow fila = listas.Rows[i];


                        string porcentaje = fila.Cells["PORCENTAJE"].Value.ToString();

                        string cantidad = fila.Cells["VCANTIDAD"].Value.ToString();
                        decimal valorAux = Convert.ToDecimal(fila.Cells["VALORUNITARIO"].Value.ToString());
                        double aux = (double)valorAux;
                        valorAux = (decimal)aux;
                        string referencia = fila.Cells["CODIGO"].Value.ToString(); ;
                        string detalle = fila.Cells["NOMBRE"].Value.ToString(); ;
                        cuentaPacientes = new CUENTAS_PACIENTES();
                        cuentaPacientes.ATE_CODIGO = Convert.ToInt32(txtAtencion.Text.Trim());
                        cuentaPacientes.CUE_FECHA = Convert.ToDateTime(fila.Cells["FECHAS"].Value.ToString());
                        cuentaPacientes.PRO_CODIGO_BARRAS = referencia.Trim();
                        cuentaPacientes.CUE_DETALLE = detalle.Trim();
                        if (porcentaje.Trim() == "1.5")
                            cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(Convert.ToDecimal(fila.Cells["TOTAL"].Value.ToString()));
                        else
                            cuentaPacientes.CUE_VALOR_UNITARIO = valorAux;
                        cuentaPacientes.CUE_CANTIDAD = Convert.ToByte(cantidad);
                        if (porcentaje.Trim() == "1.5")
                            cuentaPacientes.CUE_VALOR = Convert.ToDecimal(Convert.ToDecimal(fila.Cells["TOTAL"].Value.ToString()));
                        else
                        {
                            valorAux = (Convert.ToDecimal(Convert.ToDecimal(fila.Cells["VALORUNITARIO"].Value.ToString())) * (Convert.ToInt16(fila.Cells["PORCENTAJE"].Value.ToString()) / 100));
                            cuentaPacientes.CUE_VALOR =
                                Decimal.Round(
                                    Convert.ToInt16(cantidad) *
                                    Decimal.Round(Convert.ToDecimal(Decimal.Round(valorAux, 3)), 3), 3);
                        }
                        cuentaPacientes.CUE_IVA = 0;
                        cuentaPacientes.PED_CODIGO = pedidosArea.DIV_CODIGO;
                        cuentaPacientes.RUB_CODIGO = rubro.RUB_CODIGO;
                        cuentaPacientes.CUE_ESTADO = 1;
                        cuentaPacientes.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                        cuentaPacientes.CAT_CODIGO = 0;
                        cuentaPacientes.PRO_CODIGO = "0";
                        cuentaPacientes.CUE_NUM_CONTROL = "0";
                        cuentaPacientes.CUE_NUM_FAC = "0";
                        cuentaPacientes.MED_CODIGO = Convert.ToInt16(fila.Cells["MEDICOS"].Value.ToString());
                        NegCuentasPacientes.CrearCuenta(cuentaPacientes);
                        ugrdCuenta.DataSource = null;

                    }
                }

                //if (frm.descripcion != "0")
                //{


                //    btnGuardar.Enabled = true;

                //    string cantidad = Convert.ToString(frm.cantidad);
                //    decimal valorAux = Convert.ToDecimal(frm.valorU);
                //    double aux = (double)valorAux;
                //    valorAux = (decimal)aux;
                //    string referencia = frm.referencia;
                //    string detalle = frm.descripcion;
                //    cuentaPacientes = new CUENTAS_PACIENTES();
                //    cuentaPacientes.ATE_CODIGO = Convert.ToInt32(txtAtencion.Text.Trim());
                //    cuentaPacientes.CUE_FECHA = frm.fecha.Value;
                //    cuentaPacientes.PRO_CODIGO_BARRAS = referencia.Trim();
                //    cuentaPacientes.CUE_DETALLE = detalle.Trim();
                //    cuentaPacientes.CUE_VALOR_UNITARIO = valorAux;
                //    cuentaPacientes.CUE_CANTIDAD = Convert.ToByte(cantidad);

                //    valorAux = (Convert.ToDecimal(frm.valorU) * (frm.porcentajeFacturar / 100));
                //    cuentaPacientes.CUE_VALOR =
                //        Decimal.Round(
                //            Convert.ToInt16(cantidad) *
                //            Decimal.Round(Convert.ToDecimal(Decimal.Round(valorAux, 2)), 2), 2);
                //    cuentaPacientes.CUE_IVA = 0;
                //    cuentaPacientes.PED_CODIGO = pedidosArea.DIV_CODIGO;
                //    cuentaPacientes.RUB_CODIGO = rubro.RUB_CODIGO;
                //    cuentaPacientes.CUE_ESTADO = 1;
                //    cuentaPacientes.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                //    cuentaPacientes.CAT_CODIGO = 0;
                //    cuentaPacientes.PRO_CODIGO = "0";
                //    cuentaPacientes.CUE_NUM_CONTROL = "0";
                //    cuentaPacientes.CUE_NUM_FAC = "0";
                //    NegCuentasPacientes.CrearCuenta(cuentaPacientes);
                //    ugrdCuenta.DataSource = null;

                cargarProductos();
            }


            else
            {
                //frmAyudaProductos form = new frmAyudaProductos((PEDIDOS_AREAS)(cmb_Areas.SelectedItem));
                //form.ShowDialog();
                //listaProductosSolicitados = form.listaProductosSolicitados;
                //guardarProductos(listaProductosSolicitados);
                //cargarDetalleFactura(listaCuenta);
            }
        }
        public void guardarProductos(List<PRODUCTO> listaProductosSolicitados)
        {
            RUBROS rubroM = new RUBROS();
            RUBROS rubroS = new RUBROS();
            if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
            {
                rubroM = (RUBROS)(cmb_Rubros.SelectedItem);
                cmb_Rubros.SelectedIndex = 1;
                rubroS = (RUBROS)(cmb_Rubros.SelectedItem);
                cmb_Rubros.SelectedIndex = 0;
            }

            foreach (var solicitud in listaProductosSolicitados)
            {

                cuentaPacientes = new CUENTAS_PACIENTES();
                cuentaPacientes.ATE_CODIGO = Convert.ToInt32(txtAtencion.Text.Trim());
                cuentaPacientes.CUE_FECHA = DateTime.Now.Date;
                cuentaPacientes.PRO_CODIGO = solicitud.PRO_CODIGO.ToString();
                cuentaPacientes.CUE_DETALLE = solicitud.PRO_DESCRIPCION.ToString();
                cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(solicitud.PRO_PRECIO);
                cuentaPacientes.CUE_CANTIDAD = Convert.ToByte(solicitud.PRO_CANTIDAD);
                cuentaPacientes.CUE_VALOR = (solicitud.PRO_PRECIO * ((solicitud.PRO_IVA / 100) + 1) * solicitud.PRO_CANTIDAD);
                cuentaPacientes.CUE_ESTADO = 1;
                cuentaPacientes.CUE_NUM_FAC = "0";
                cuentaPacientes.RUB_CODIGO = Convert.ToInt16(cmb_Areas.SelectedValue.ToString());
                //cambio hr 23052016
                //string ivaTotal = Convert.ToString(Convert.ToDouble(cuentaPacientes.CUE_VALOR) * 0.12);
                string ivaTotal = Convert.ToString(Convert.ToDouble(cuentaPacientes.CUE_VALOR) * 0.14);

                cuentaPacientes.CUE_IVA = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
                //cuentaPacientes.CUE_IVA = Convert.ToDecimal(cuentaPacientes.CUE_VALOR) * Convert.ToDecimal(0.12);

                cuentaPacientes.PED_CODIGO = NegPedidos.ultimoCodigoPedidos() + 1;
                cuentaPacientes.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                cuentaPacientes.CAT_CODIGO = 0;
                NegCuentasPacientes.CrearCuenta(cuentaPacientes);
                ugrdCuenta.DataSource = null;
            }
            cargarProductos();

        }

        /// <summary>
        /// Leer Productos de la Bade de Datos de la tabla Productos
        /// </summary>
        public void CargarProducto()
        {
            //dtpFechaPedido.Value = DateTime.Now;
            PRODUCTO producto = NegProducto.RecuperarProductoID(Convert.ToInt32(txt_Codigo.Text.Trim()));
            if (producto != null)
            {
                txt_Codigo.Text = (producto.PRO_CODIGO).ToString();
                txt_Nombre.Text = producto.PRO_DESCRIPCION;
                //txtValorNeto.Text = Convert.ToString(producto.PRO_PRECIO);
                txtValorNeto.Text = String.Format("{0:0.000}", producto.PRO_PRECIO);
                txtCodigoSic.Text = (producto.PRO_CODIGO_BARRAS).ToString();
                txtCodigoSic.Enabled = false;
            }
            else
            {
                txtCodigoSic.Enabled = true;
                txt_Codigo.Text = string.Empty;
                txtCodigoSic.Text = "0";
                txt_Nombre.Text = string.Empty;
                txtValorNeto.Text = "0.00";
                txtCantidad.Text = "1";
                txtTotal.Text = "0.00";
            }
        }

        public void CargarTarifarioIess()
        {
            //dtpFechaPedido.Value = DateTime.Now;
            TARIFARIO_IESS tarifarioI = NegTarifario.RecuperarTarifarioIess(Convert.ToInt32(txt_Codigo.Text.Trim()));
            if (tarifarioI != null)
            {
                txt_Codigo.Text = (tarifarioI.COD_IESS).ToString();
                txt_Nombre.Text = tarifarioI.NOM_EXA;
                txtValorNeto.Text = String.Format("{0:0.000}", tarifarioI.PR_LAB_NV3);
                txtCodigoSic.Text = (tarifarioI.COD_IESS).ToString();
                txtCodigoSic.Enabled = false;
                txt_Nombre.Focus();
            }
            else
            {
                txtCodigoSic.Enabled = true;
                txt_Codigo.Text = string.Empty;
                txtCodigoSic.Text = "0";
                txt_Nombre.Text = string.Empty;
                txtValorNeto.Text = "0.00";
                txtCantidad.Text = "1";
                txtTotal.Text = "0.00";
                txt_Codigo.Focus();
            }
        }


        public void CargarTarifarioHonorarios()
        {
            TARIFARIOS_DETALLE tarifarioH = NegTarifario.RecuperarTarifarioHono(txt_Codigo.Text.Trim());
            if (tarifarioH != null)
            {
                txt_Codigo.Text = (tarifarioH.TAD_REFERENCIA).ToString();
                if (((RUBROS)cmb_Rubros.SelectedItem).RUB_CODIGO == His.Parametros.CuentasPacientes.RubroAnestesia)
                    txt_Nombre.Text = tarifarioH.TAD_DESCRIPCION + (((RUBROS)cmb_Rubros.SelectedItem)).RUB_NOMBRE;
                else
                    txt_Nombre.Text = tarifarioH.TAD_DESCRIPCION;
                txtCodigoSic.Text = (tarifarioH.TAD_REFERENCIA).ToString();
                calcularValor(tarifarioH.TAD_CODIGO);
                        txtCodigoSic.Enabled = false;
                txt_Nombre.Focus();
                if (cmb_Areas.Text == "HONORARIOS MEDICOS")
                {
                    if (cmb_Rubros.Text == "HONORARIOS MEDICOS")
                        txtValorNeto.Text = (tarifarioH.TAD_UVR).ToString();
                    else
                        txtValorNeto.Text = (tarifarioH.TAD_ANESTESIA).ToString();
                }
                else
                    txtValorNeto.Text = (tarifarioH.TAD_UVR).ToString();
            }
            else
            {
                txtCodigoSic.Enabled = true;
                txt_Codigo.Text = string.Empty;
                txtCodigoSic.Text = "0";
                txt_Nombre.Text = string.Empty;
                txtValorNeto.Text = "0.00";
                txtCantidad.Text = "1";
                txtTotal.Text = "0.00";
                txt_Codigo.Focus();
            }
        }


        private void calcularValor(Int64 codigoTarifarioH)
        {
            HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
            tarifarioH = contexto.TARIFARIOS_DETALLE.Include("ESPECIALIDADES_TARIFARIOS").FirstOrDefault(
                                                        t => t.TAD_CODIGO == codigoTarifarioH);
            Int64 codigo = tarifarioH.ESPECIALIDADES_TARIFARIOS.EST_CODIGO;
            addHonorarioDetalle(codigo, Convert.ToDouble(tarifarioH.TAD_UVR));
            tarifarioHI = contexto.TARIFARIOS_DETALLE.Include("ESPECIALIDADES_TARIFARIOS").FirstOrDefault(t => t.TAD_PADRE == codigoTarifarioH);
        }

        private void addHonorarioDetalle(Int64 codigoEspecialidad, double uvr)
        {
            try
            {
                HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
                CONVENIOS_TARIFARIOS convenios = contexto.CONVENIOS_TARIFARIOS.FirstOrDefault(
                                                c => c.ASEGURADORAS_EMPRESAS.ASE_CODIGO == 13
                                                    && c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO == codigoEspecialidad);
                double costoU = 0;
                //decimal Val1 = 0;
                //string ValorCostoU="";
                if (convenios != null)
                {
                    //Val1 = Convert.ToDecimal(Math.Round((Convert.ToDouble(convenios.CON_VALOR_UVR) * uvr), 3));
                    //ValorCostoU = String.Format("{0:0.000}",Val1);
                    costoU = His.General.CalculosCuentas.CalcularRedondeo(String.Format("{0:0.000}", Math.Round((Convert.ToDouble(convenios.CON_VALOR_UVR) * uvr), 3)));
                    //costoU = His.General.CalculosCuentas.CalcularRedondeo("10.5556");
                }
                else
                {
                    MessageBox.Show("No existe un Convenio", MessageBoxIcon.Information.ToString());
                    return;
                }
                unidadesUvr = Convert.ToDecimal(costoU) * Convert.ToInt32(txtCantidad.Text);
                //txtValorNeto.Text = Convert.ToString(Math.Round(unidadesUvr, 3));
                txtValorNeto.Text = String.Format("{0:0.000}", Math.Round(unidadesUvr, 3));
                //txtPrecio.Text = txtValorNeto.Text;
                txt_Nombre.Focus();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GuardarProducto()
        {
            RUBROS rubroM = new RUBROS();
            RUBROS rubroS = new RUBROS();
            Boolean accion = false;
            if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
            {
                rubroM = (RUBROS)(cmb_Rubros.SelectedItem);
                cmb_Rubros.SelectedIndex = 1;
                rubroS = (RUBROS)(cmb_Rubros.SelectedItem);
                cmb_Rubros.SelectedIndex = 0;
            }
            if (cuentaModificada == null)
            {

                cuentaPacientes = new CUENTAS_PACIENTES();
                cuentaPacientes.ATE_CODIGO = Convert.ToInt32(txtAtencion.Text);
                cuentaPacientes.CUE_FECHA = dtpFechaPedido.Value;
                cuentaPacientes.PRO_CODIGO = "0";
                cuentaPacientes.CUE_DETALLE = (txt_Nombre.Text).ToString();
                //cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(precioUnitario); ' Debe almacenar el valor que muestra en la caja de texto / Giovanny Tapia / 12/12/2012 / NOTA: Requerimiento Sra. Vicky
                cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(txtTotal.Text) / Convert.ToDecimal(txtCantidad.Text);
                cuentaPacientes.CUE_CANTIDAD = Convert.ToDecimal(txtCantidad.Text);
                cuentaPacientes.CUE_VALOR = Convert.ToDecimal(txtTotal.Text);
                cuentaPacientes.CUE_ESTADO = 1;
                cuentaPacientes.CUE_NUM_CONTROL = txtCControl.Text;
                cuentaPacientes.CUE_NUM_FAC = "0";
                cuentaPacientes.PRO_CODIGO_BARRAS = txtCodigoSic.Text.Trim();
                if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
                {
                    //if ((txtCodigoSic.Text).ToString().Substring(0, 1).Equals("2"))Corrijo para que no tome el codigo sino el iva del producto David Mantilla
                    string aux = txtCodigoSic.Text.Trim();
                    aux = Convert.ToString(NegFactura.RecuperaInformacionIVA(aux));
                    //cambio hr 23052016
                    //if (aux == "12")
                    if (aux == "14")
                    {
                        cuentaPacientes.RUB_CODIGO = rubroS.RUB_CODIGO;
                        //cambio hr 23052016
                        //string ivaTotal = Convert.ToString(Convert.ToDouble(cuentaPacientes.CUE_VALOR) * 0.12);
                        string ivaTotal = Convert.ToString(Convert.ToDouble(cuentaPacientes.CUE_VALOR) * 0.14);
                        cuentaPacientes.CUE_IVA = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
                        //cuentaPacientes.CUE_IVA = Convert.ToDecimal(cuentaPacientes.CUE_VALOR) * Convert.ToDecimal(0.12);
                    }
                    else
                    {
                        cuentaPacientes.RUB_CODIGO = rubroM.RUB_CODIGO;
                        cuentaPacientes.CUE_IVA = 0;
                    }
                }
                else
                {
                    cuentaPacientes.RUB_CODIGO = Convert.ToInt16(((RUBROS)(cmb_Rubros.SelectedItem)).RUB_CODIGO);
                    if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoOtrosP)
                    {
                        accion = true;
                        if (rdb_ConIva.Checked == true)
                        {
                            //cambio hr 23052016
                            //string ivaTotal = Convert.ToString(Convert.ToDouble(txtTotal.Text) * 0.12);
                            string ivaTotal = Convert.ToString(Convert.ToDouble(txtTotal.Text) * 0.14);
                            cuentaPacientes.CUE_IVA = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
                            //cuentaPacientes.CUE_IVA = Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(0.12);
                        }
                        else
                            cuentaPacientes.CUE_IVA = 0;
                    }
                    else
                        cuentaPacientes.CUE_IVA = 0;
                }
                cuentaPacientes.PED_CODIGO = Convert.ToInt32(((PEDIDOS_AREAS)(cmb_Areas.SelectedItem)).DIV_CODIGO);
                cuentaPacientes.ID_USUARIO = Convert.ToInt16(His.Entidades.Clases.Sesion.codUsuario);
                cuentaPacientes.CAT_CODIGO = 0;
                if (txtCodMedico.Text.Trim() != string.Empty)
                {
                    cuentaPacientes.MED_CODIGO = Convert.ToInt32(txtCodMedico.Text.Trim());
                    cuentaPacientes.Id_Tipo_Medico = Convert.ToInt32(this.cmbTipoMedico.SelectedValue.ToString());
                }

                else // Añado el else para que cuando no se ingrese el medico en la DB se inserte el valor 0(Ninguno) / Giovanny Tapia / 09/08/2012
                {
                    cuentaPacientes.MED_CODIGO = 0;
                    cuentaPacientes.Id_Tipo_Medico = 0;
                }

                NegCuentasPacientes.CrearCuenta(cuentaPacientes);
            }
            else
            {
                cuentaModificada.CUE_FECHA = dtpFechaPedido.Value;
                cuentaModificada.PRO_CODIGO = "0";
                cuentaModificada.CUE_DETALLE = (txt_Nombre.Text).ToString();
                cuentaModificada.CUE_VALOR_UNITARIO = Convert.ToDecimal(txtTotal.Text) / Convert.ToDecimal(txtCantidad.Text);//Cambio pára verificar archivo plano doris 
                cuentaModificada.CUE_CANTIDAD = Convert.ToDecimal(txtCantidad.Text);
                cuentaModificada.CUE_VALOR = Convert.ToDecimal(txtTotal.Text);
                cuentaModificada.CUE_NUM_CONTROL = txtCControl.Text;
                cuentaModificada.PRO_CODIGO_BARRAS = txtCodigoSic.Text.Trim();
                if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
                {
                    if ((txtCodigoSic.Text).ToString().Substring(0, 1).Equals("2"))
                    {
                        cuentaModificada.RUB_CODIGO = rubroS.RUB_CODIGO;
                        //cambio hr 23052016
                        //string ivaTotal = Convert.ToString(Convert.ToDouble(cuentaModificada.CUE_VALOR) * 0.12);
                        string ivaTotal = Convert.ToString(Convert.ToDouble(cuentaModificada.CUE_VALOR) * 0.14);
                        cuentaModificada.CUE_IVA = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
                        //cuentaModificada.CUE_IVA = Convert.ToDecimal(cuentaModificada.CUE_VALOR) * Convert.ToDecimal(0.12);
                    }
                    else
                    {
                        cuentaModificada.RUB_CODIGO = rubroM.RUB_CODIGO;
                        cuentaModificada.CUE_IVA = 0;
                    }
                }
                else
                {
                    //cuentaPacientes.RUB_CODIGO = Convert.ToInt16(((RUBROS)(cmb_Rubros.SelectedItem)).RUB_CODIGO);
                    if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoOtrosP)
                    {
                        accion = true;
                        if (rdb_ConIva.Checked == true)
                        {
                            //cambio hr 23052016
                            //string ivaTotal = Convert.ToString(Convert.ToDouble(cuentaModificada.CUE_VALOR) * 0.12);
                            string ivaTotal = Convert.ToString(Convert.ToDouble(cuentaModificada.CUE_VALOR) * 0.14);
                            cuentaModificada.CUE_IVA = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
                            //cuentaModificada.CUE_IVA = Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(0.12);
                        }
                        else
                            cuentaModificada.CUE_IVA = 0;
                    }
                    else
                        cuentaModificada.CUE_IVA = 0;
                }
                if (txtCodMedico.Text.Trim() != string.Empty)
                {
                    cuentaModificada.MED_CODIGO = Convert.ToInt32(txtCodMedico.Text.Trim());
                    cuentaModificada.Id_Tipo_Medico = Convert.ToInt32(this.cmbTipoMedico.SelectedValue.ToString());

                }
                else // Añado el else para que cuando no se ingrese el medico en la DB se inserte el valor 0(Ninguno) / Giovanny Tapia / 09/08/2012
                {
                    cuentaModificada.MED_CODIGO = 0;
                    cuentaModificada.Id_Tipo_Medico = 0;
                }

                NegCuentasPacientes.ModificarCuenta(cuentaModificada);
            }
            if (accion == true)
                CalcularIvaSuministros(Convert.ToInt32(txtAtencion.Text));
            limpiarCampos();
            cargarProductos();
        }

        public void calcularValoresFactura()
        {
            try
            {
                if (txtValorNeto.Text != string.Empty && txtPorcentaje.Text != string.Empty)
                {
                    double porcentaje = Math.Round((Convert.ToDouble(txtPorcentaje.Text) / 100), 2);
                    //txtTotal.Text = ' Cambio el formato de los decimales a tres / Giovanny Tapia / 14/12/2012
                    //Convert.ToString(((Convert.ToDouble(txtCantidad.Text)) *
                    //(Convert.ToDouble(txtValorNeto.Text.ToString()))) * porcentaje);

                    txtTotal.Text =
                    String.Format("{0:0.000}", ((Convert.ToDouble(txtCantidad.Text)) *
                    (Convert.ToDouble(txtValorNeto.Text.ToString()))) * porcentaje);

                    if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO != His.Parametros.CuentasPacientes.CodigoFarmacia)
                    {
                        if (txtCantidad.Text == "1")
                        {
                            precioUnitario = Convert.ToDecimal(txtTotal.Text);
                        }
                        else
                        {
                            precioUnitario = Math.Round(Convert.ToDecimal(txtTotal.Text) / Convert.ToDecimal(txtCantidad.Text.Trim()), 2);
                        }
                    }
                    else
                    {
                        precioUnitario = Math.Round(Convert.ToDecimal(txtTotal.Text) / Convert.ToDecimal(txtCantidad.Text.Trim()), 2);
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }


        public void calcularValoresFacturaIva()
        {
            try
            {
                if (txtValorNeto.Text != string.Empty && txtPorcentaje.Text != string.Empty)
                {
                    double porcentaje = Math.Round((Convert.ToDouble(txtPorcentaje.Text) / 100), 2);
                    string TotalC = Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) *
                                         (Convert.ToDouble(txtValorNeto.Text.ToString()))) * porcentaje);
                    txtTotal.Text = Convert.ToString(Convert.ToInt32(txtCantidad.Text) * (
                                         Convert.ToDouble(txtValorNeto.Text.ToString()) + Convert.ToDouble(TotalC)));
                    precioUnitario = Math.Round(Convert.ToDecimal(txtTotal.Text) / Convert.ToInt32(txtCantidad.Text.Trim()), 2);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void frmCuentaPacientesIess_Load(object sender, EventArgs e)
        {
            NumeroFila = 0;

            DataTable dt = new DataTable();

            dt = NegCuentasPacientes.CargaTipoMedico();

            cmbTipoMedico.DataSource = dt;
            cmbTipoMedico.ValueMember = "Id_Tipo_Medico";
            cmbTipoMedico.DisplayMember = "Descripcion_Tipo_Medico";


        }

        private void bindingNavigatorPositionItem_Click(object sender, EventArgs e)
        {

        }

        private void txt_Codigo_TextChanged(object sender, EventArgs e)
        {
            if (txt_Codigo.Text.Trim() != string.Empty)
            {

            }
        }

        private void txt_Codigo_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView listas = new DataGridView();
            try
            {
                if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
                {
                    if (txt_Codigo.Text.Trim() != string.Empty)
                        {
                        int codigoArea = Convert.ToInt32(((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO);
                        if (His.Parametros.CuentasPacientes.CodigoLaboratorio == codigoArea || His.Parametros.CuentasPacientes.CodigoLaboratorioP == codigoArea)
                        {
                            CargarTarifarioIess();
                        }
                        else
                        {
                            if (codigoArea == His.Parametros.CuentasPacientes.CodigoHonorarios || codigoArea == His.Parametros.CuentasPacientes.CodigoImagen || codigoArea == His.Parametros.CuentasPacientes.CodigoServicosI)
                            {
                                CargarTarifarioHonorarios();
                            }
                            else
                                CargarProducto();
                        }
                        txt_Nombre.Focus();
                    }
                }
                else
                {
                    if ((e.KeyCode == Keys.F1))
                    {
                        int codigoArea = Convert.ToInt32(((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO);
                        //if (codigoArea == His.Parametros.CuentasPacientes.CodigoHonorarios || codigoArea == His.Parametros.CuentasPacientes.CodigoImagen || codigoArea == His.Parametros.CuentasPacientes.CodigoServicosI)
                        //{
                        RUBROS rubro = (RUBROS)(cmb_Rubros.SelectedItem);
                        PEDIDOS_AREAS pedidosAreas = (PEDIDOS_AREAS)(cmb_Areas.SelectedItem);
                        //if (pedidosAreas.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoHonorarios ||
                        //    pedidosAreas.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoImagen ||
                        //    pedidosAreas.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoServicosI)
                        //{
                        frmAyudaTarifarios frm = new frmAyudaTarifarios(txtCodMedico.Text.Trim(), dateTimePickerFechaAlta);
                        frm.ShowDialog();
                        listas = frm.lista;
                        if (listas != null)
                        {
                            btnGuardar.Enabled = true;
                            for (Int16 i = 0; i <= listas.RowCount - 2; i++)
                            {
                                DataGridViewRow fila = listas.Rows[i];

                                string porcentaje = fila.Cells["PORCENTAJE"].Value.ToString();
                                string cantidad = fila.Cells["VCANTIDAD"].Value.ToString();
                                decimal valorAux = Convert.ToDecimal(fila.Cells["VALORUNITARIO"].Value.ToString());
                                double aux = (double)valorAux;
                                valorAux = (decimal)aux;
                                string referencia = fila.Cells["CODIGO"].Value.ToString(); ;
                                string detalle = fila.Cells["NOMBRE"].Value.ToString(); ;
                                cuentaPacientes = new CUENTAS_PACIENTES();
                                cuentaPacientes.ATE_CODIGO = Convert.ToInt32(txtAtencion.Text.Trim());
                                cuentaPacientes.CUE_FECHA = Convert.ToDateTime(fila.Cells["FECHAS"].Value.ToString());
                                cuentaPacientes.PRO_CODIGO_BARRAS = referencia.Trim();
                                cuentaPacientes.CUE_DETALLE = detalle.Trim();
                                if (porcentaje.Trim() == "1.5")
                                    cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(Convert.ToDecimal(fila.Cells["TOTAL"].Value.ToString()));
                                else
                                    cuentaPacientes.CUE_VALOR_UNITARIO = valorAux;
                                cuentaPacientes.CUE_CANTIDAD = Convert.ToByte(cantidad);
                                if (porcentaje.Trim() == "1.5")
                                    cuentaPacientes.CUE_VALOR = Convert.ToDecimal(Convert.ToDecimal(fila.Cells["TOTAL"].Value.ToString()));
                                else
                                {
                                    valorAux = (Convert.ToDecimal(Convert.ToDecimal(fila.Cells["VALORUNITARIO"].Value.ToString())) * (Convert.ToInt16(fila.Cells["PORCENTAJE"].Value.ToString()) / 100));
                                    cuentaPacientes.CUE_VALOR =
                                        Decimal.Round(
                                            Convert.ToInt16(cantidad) *
                                            Decimal.Round(Convert.ToDecimal(Decimal.Round(valorAux, 2)), 2), 2);
                                }
                                cuentaPacientes.CUE_IVA = 0;
                                cuentaPacientes.PED_CODIGO = pedidosArea.DIV_CODIGO;
                                cuentaPacientes.RUB_CODIGO = rubro.RUB_CODIGO;
                                cuentaPacientes.CUE_ESTADO = 1;
                                cuentaPacientes.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                                cuentaPacientes.CAT_CODIGO = 0;
                                cuentaPacientes.PRO_CODIGO = "0";
                                cuentaPacientes.CUE_NUM_CONTROL = "0";
                                cuentaPacientes.CUE_NUM_FAC = "0";
                                cuentaPacientes.MED_CODIGO = Convert.ToInt16(fila.Cells["MEDICOS"].Value.ToString());
                                NegCuentasPacientes.CrearCuenta(cuentaPacientes);
                                ugrdCuenta.DataSource = null;

                            }
                        }

                        //}
                        //}
                        //else
                        //{
                        //    frmAyudaProductos form = new frmAyudaProductos((PEDIDOS_AREAS)(cmb_Areas.SelectedItem));
                        //    form.ShowDialog();
                        //    listaProductosSolicitados = form.listaProductosSolicitados;
                        //    guardarProductos(listaProductosSolicitados);
                        //    cargarDetalleFactura(listaCuenta);
                        //}
                        cargarProductos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void txt_Codigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txt_Nombre.Focus();
            }
        }

        private void txt_Nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtValorNeto.Focus();
            }
        }

        private void txt_Precio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtPorcentaje.Focus();
            }
        }

        private void msk_Cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtTotal.Focus();
            }
        }

        private void maskedTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                dtpFechaPedido.Focus();
            }
        }

        private void msk_Cantidad_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter))
                {
                    if (txtCantidad.Text.Trim() != string.Empty)
                    {
                        //txt_Total.Text = Convert.ToString(Convert.ToDecimal(txtCantidad.Text) * Convert.ToInt32(txtValorNeto.Text));
                        calcularValoresFactura();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void txtValorNeto_TextChanged(object sender, EventArgs e)
        {
            if (txtValorNeto.Text.ToString() != string.Empty || txtValorNeto.Text.ToString() != "0.00")
            {
                if (Microsoft.VisualBasic.Information.IsNumeric(txtValorNeto.Text))
                {
                    calcularValoresFactura();
                }
                else
                {
                    txtValorNeto.Text = string.Empty;
                }
            }


            if (txtValorNeto.Text.ToString() == "0.00" || txtValorNeto.Text.ToString() == string.Empty)
            {
                //txt_Codigo.Text = string.Empty;
                //txt_Nombre.Text = string.Empty;
                //txtValorNeto.Text = string.Empty;
                //txtCantidad.Text = string.Empty;
            }
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
                {
                    if (txtCantidad.Text.Trim() != string.Empty)
                    {
                        if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoOtrosP)
                        {
                            if (rdb_ConIva.Checked == true)
                                calcularValoresFacturaIva();
                            else
                                calcularValoresFactura();
                        }
                        else
                        {
                            calcularValoresFactura();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void txtValorNeto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void btnAnadir_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_Codigo.Text != string.Empty || txtCodigoSic.Text != string.Empty)
                {
                    if (txt_Nombre.Text != string.Empty)
                    {
                        if (txtValorNeto.Text.ToString() != "0.00" || txtValorNeto.Text.ToString() == string.Empty)
                        {
                            if (txtCantidad.Text != string.Empty || txtCantidad.Text != "0")
                            {
                                if (txtTotal.Text.ToString() != "0.00" || txtTotal.Text.ToString() == string.Empty)
                                {
                                    GuardarProducto();
                                    //if (tarifarioHI != null) // Comento ya que el 1.5 segun requerimiento activa salud ya no va/ Giovanny Tapia / 14/12/2012 
                                    //{
                                    //    txtCodigoSic.Text = tarifarioH.TAD_REFERENCIA;
                                    //    txt_Nombre.Text = tarifarioHI.TAD_DESCRIPCION;
                                    //    txtValorNeto.Text =
                                    //        Convert.ToString(((Convert.ToInt32(txtCantidad.Text))*
                                    //                          (Convert.ToDouble(unidadesUvr)))*0.015);
                                    //    txtCantidad.Text = "1";
                                    //    txtTotal.Text =
                                    //        Convert.ToString(((Convert.ToInt32(txtCantidad.Text))*
                                    //                          (Convert.ToDouble(unidadesUvr)))*0.015);
                                    //    txtCControl.Text = "0";
                                    //    GuardarProducto();
                                    //    tarifarioHI = null;
                                    //    tarifarioH = null;
                                    //}
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (txt_Codigo.Text == string.Empty && txtCodigoSic.Text == string.Empty)
                    {
                        GuardarProducto();
                        //if (tarifarioHI != null) ' Ingreso del rubro del 1.5 / Giovanny Tapia / 14/12/2012
                        //{
                        //    txtCodigoSic.Text = tarifarioH.TAD_REFERENCIA;
                        //    txt_Nombre.Text = tarifarioHI.TAD_DESCRIPCION;
                        //    txtValorNeto.Text =
                        //        Convert.ToString(((Convert.ToInt32(txtCantidad.Text))*(Convert.ToDouble(unidadesUvr)))*
                        //                         0.015);
                        //    txtCantidad.Text = "1";
                        //    txtTotal.Text =
                        //        Convert.ToString(((Convert.ToInt32(txtCantidad.Text))*(Convert.ToDouble(unidadesUvr)))*
                        //                         0.015);
                        //    txtCControl.Text = "0";
                        //    GuardarProducto();
                        //    tarifarioHI = null;
                        //    tarifarioH = null;
                        //}
                    }
                }
                txtCodigoSic.Text = "0";
                txt_Codigo.Focus();
                if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
                    CalcularIvaSuministros(Convert.ToInt32(txtAtencion.Text.Trim()));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void btnAnadir_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter))
                {
                    if (txt_Codigo.Text != string.Empty || txtCodigoSic.Text != string.Empty)
                    {
                        if (txt_Nombre.Text != string.Empty)
                        {
                            if (txtValorNeto.Text.ToString() != "0.00" || txtValorNeto.Text.ToString() == string.Empty)
                            {
                                if (txtCantidad.Text != string.Empty || txtCantidad.Text != "0")
                                {
                                    if (txtTotal.Text.ToString() != "0.00" || txtTotal.Text.ToString() == string.Empty)
                                    {
                                        GuardarProducto();
                                        //if (tarifarioHI != null)// para quitar el 1.5% euipo digital que ya no viene con el MSP David Mantila 13/11/2012
                                        //{
                                        //    txtCodigoSic.Text = tarifarioH.TAD_REFERENCIA;
                                        //    txt_Nombre.Text = tarifarioHI.TAD_DESCRIPCION;
                                        //    txtValorNeto.Text = Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) * (Convert.ToDouble(unidadesUvr))) * 0.015);
                                        //    txtCantidad.Text = "1";
                                        //    txtTotal.Text = Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) * (Convert.ToDouble(unidadesUvr))) * 0.015);
                                        //    txtCControl.Text = "0";
                                        //    GuardarProducto();
                                        //    tarifarioHI = null;
                                        //    tarifarioH = null;
                                        //}
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (txt_Codigo.Text == string.Empty && txtCodigoSic.Text == string.Empty)
                        {
                            GuardarProducto();
                            //if (tarifarioHI != null) // Comento ya que el 1.5 segun requerimiento activa salud ya no va/ Giovanny Tapia / 14/12/2012
                            //{
                            //    txtCodigoSic.Text = tarifarioH.TAD_REFERENCIA;
                            //    txt_Nombre.Text = tarifarioHI.TAD_DESCRIPCION;
                            //    txtValorNeto.Text = Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) * (Convert.ToDouble(unidadesUvr))) * 0.015);
                            //    txtCantidad.Text = "1";
                            //    txtTotal.Text = Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) * (Convert.ToDouble(unidadesUvr))) * 0.015);
                            //    txtCControl.Text = "0";
                            //    GuardarProducto();
                            //    tarifarioHI = null;
                            //    tarifarioH = null;
                            //}
                        }
                    }
                    txtCodigoSic.Text = "0";
                    txt_Codigo.Focus();
                    if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
                        CalcularIvaSuministros(Convert.ToInt32(txtAtencion.Text.Trim()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }

        }

        private void ugrdCuenta_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Delete))
                {
                    if (ugrdCuenta.ActiveRow != null)
                    {
                        int codigo = Convert.ToInt32(ugrdCuenta.ActiveRow.Cells[0].Value);
                        NegCuentasPacientes.EliminarCuenta(codigo);
                        ugrdCuenta.ActiveRow.Delete();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void txtCodigoSic_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
                {
                    //dtpFechaPedido.Value = DateTime.Now;
                    if (txt_Codigo.Text.Trim() == string.Empty)
                    {
                        txt_Codigo.Text = string.Empty;
                        //txtCodigoSic.Text = string.Empty;
                        txt_Nombre.Text = string.Empty;
                        txtValorNeto.Text = "0.00";
                        txtCantidad.Text = "1";
                        txtTotal.Text = "0.00";
                        txtCControl.Text = "0.00";
                    }
                    else
                    {
                        if (txt_Codigo.Text.Trim() != txtCodigoSic.Text.Trim())
                        {
                            txt_Codigo.Text = string.Empty;
                            txt_Nombre.Text = string.Empty;
                            txtValorNeto.Text = "0.00";
                            txtCantidad.Text = "1";
                            txtTotal.Text = "0.00";
                            txtCControl.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void txtCodigoSic_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void ugrdCuenta_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            try
            {
                if (ugrdCuenta.ActiveRow != null)
                {

                    //int codigo = Convert.ToInt32(ugrdCuenta.ActiveRow.Cells[0].Value);
                    txt_Codigo.Text = (ugrdCuenta.ActiveRow.Cells[15].Value).ToString();
                    txtCodigoSic.Text = (ugrdCuenta.ActiveRow.Cells[15].Value).ToString();
                    txt_Nombre.Text = (ugrdCuenta.ActiveRow.Cells[3].Value).ToString();
                    txtValorNeto.Text = (ugrdCuenta.ActiveRow.Cells[14].Value).ToString();
                    txtCantidad.Text = (ugrdCuenta.ActiveRow.Cells[8].Value).ToString();
                    txtTotal.Text = (ugrdCuenta.ActiveRow.Cells[4].Value).ToString();

                    if ((ugrdCuenta.ActiveRow.Cells[16].Value) != null) // Para evitar que de error cuando el numero de control no esxiste // Giovanny Tapia // 13/12/2012
                    {
                        txtCControl.Text = (ugrdCuenta.ActiveRow.Cells[16].Value).ToString();
                    }
                    else
                    {
                        txtCControl.Text = "0";
                    }

                    dtpFechaPedido.Value = Convert.ToDateTime(ugrdCuenta.ActiveRow.Cells[2].Value);
                    txtPorcentaje.Text = "100";
                    NumeroFila = ugrdCuenta.ActiveRow.Index;
                    gridRow = ugrdCuenta.ActiveRow;

                    if ((ugrdCuenta.ActiveRow.Cells[18].Value) != null)
                    {
                        txtCodMedico.Text = (ugrdCuenta.ActiveRow.Cells[18].Value).ToString();
                        CargarMedico(Convert.ToInt32(ugrdCuenta.ActiveRow.Cells[18].Value));
                        this.cmbTipoMedico.SelectedIndex = Convert.ToInt32(ugrdCuenta.ActiveRow.Cells["Id_Tipo_Medico"].Value);
                    }
                    else
                    {
                        txtCodMedico.Text = string.Empty;
                        txtNombreMedico.Text = string.Empty;
                        this.cmbTipoMedico.SelectedIndex = 0;
                    }
                    cuentaModificada = (CUENTAS_PACIENTES)(ugrdCuenta.ActiveRow.ListObject);
                    if (cuentaModificada.RUB_CODIGO == His.Parametros.CuentasPacientes.RubroOtrosP || cuentaModificada.RUB_CODIGO == His.Parametros.CuentasPacientes.RubroSuministros) { }
                    {
                        cmb_Areas.Enabled = false;
                        rdb_SinIva.Enabled = true;
                        rdb_ConIva.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            openFileDialogo.InitialDirectory = "c:\\";
            openFileDialogo.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialogo.FilterIndex = 2;
            openFileDialogo.RestoreDirectory = true;

            if (openFileDialogo.ShowDialog() == DialogResult.OK)
            {
                txtDirArchivo.Text = openFileDialogo.FileName;

            }
        }

        private void btn_Cargar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            gpbDatos.Enabled = false;
            ugrdCuenta.DataSource = null;
            try
            {
                if (rdbTexto.Checked == true || rdbCSV.Checked == true)
                {
                    var listaFilas = ImportarArchivoDelimitado(txtDirArchivo.Text.Trim(), new char[] { ';' });
                    if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
                    {
                        ugrdCuenta.DataSource = listaFilas.Select(c => new CUENTAS_PACIENTES()
                        {
                            ATE_CODIGO = Convert.ToInt32(txtAtencion.Text),
                            CUE_FECHA = Convert.ToDateTime(c[1]),
                            PRO_CODIGO_BARRAS = (c[0]).ToString(),
                            CUE_DETALLE = (c[4]).ToString(),
                            CUE_VALOR_UNITARIO = Convert.ToDecimal(c[6]),
                            CUE_CANTIDAD = Convert.ToDecimal(c[5]),
                            CUE_VALOR = Convert.ToDecimal(c[7]),
                            CUE_IVA =Convert.ToDecimal(0.00),
                            CUE_ESTADO = 1,
                            PRO_CODIGO = "0",
                            CUE_NUM_FAC = "0",
                            MED_CODIGO = 0,
                            CUE_NUM_CONTROL = (c[2]).ToString(),
                            RUB_CODIGO = ((RUBROS)(cmb_Rubros.SelectedItem)).RUB_CODIGO,
                            PED_CODIGO = ((PEDIDOS_AREAS)(cmb_Areas.SelectedItem)).DIV_CODIGO,
                            ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario,
                            CAT_CODIGO = 0
                        }).ToList();
                    }
                    else
                    {
                        ugrdCuenta.DataSource = listaFilas.Select(c => new CUENTAS_PACIENTES()
                        {
                            ATE_CODIGO = Convert.ToInt32(txtAtencion.Text),
                            CUE_FECHA = Convert.ToDateTime(c[0]),
                            PRO_CODIGO_BARRAS = (c[1]).ToString(),
                            CUE_DETALLE = (c[2]).ToString(),
                            CUE_VALOR_UNITARIO = Convert.ToDecimal(c[3]),
                            CUE_CANTIDAD = Convert.ToByte(c[4]),
                            CUE_VALOR = Convert.ToDecimal(c[5]),
                            CUE_IVA = Convert.ToDecimal(0.00),
                            CUE_ESTADO = 1,
                            PRO_CODIGO = "0",
                            CUE_NUM_FAC = "0",
                            CUE_NUM_CONTROL = "0",
                            MED_CODIGO = 0,
                            RUB_CODIGO =
                                ((RUBROS)(cmb_Rubros.SelectedItem)).
                                RUB_CODIGO,
                            PED_CODIGO =
                                ((PEDIDOS_AREAS)(cmb_Areas.SelectedItem))
                                .DIV_CODIGO,
                            ID_USUARIO =
                                His.Entidades.Clases.Sesion.codUsuario,
                            CAT_CODIGO = 0
                        }).ToList();
                    }
                    UltraGridBand bandUno = ugrdCuenta.DisplayLayout.Bands[0];
                    SummarySettings sumTarifa = bandUno.Summaries.Add("CUE_VALOR", SummaryType.Sum,
                                                                      bandUno.Columns["CUE_VALOR"]);
                    MessageBox.Show("Archivo cargado  exitosamente");
                    btnActualizar.Enabled = false;
                    btnGuardar.Enabled = true;
                }
                else
                {
                    if (rdbExcel.Checked == true)
                    {
                        btnGuardar.Enabled = true;
                        if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO != His.Parametros.CuentasPacientes.CodigoFarmacia)
                        {
                            string rutaExcel = txtDirArchivo.Text.Trim();
                            //Creamos la cadena de conexión con el fichero excel
                            OleDbConnectionStringBuilder cb = new OleDbConnectionStringBuilder();
                            cb.DataSource = rutaExcel;
                            if (Path.GetExtension(rutaExcel).ToUpper() == ".XLS")
                            {
                                cb.Provider = "Microsoft.Jet.OLEDB.4.0";
                                cb.Add("Extended Properties", "Excel 8.0;HDR=YES;IMEX=0;");
                            }
                            else if (Path.GetExtension(rutaExcel).ToUpper() == ".XLSX")
                            {
                                cb.Provider = "Microsoft.ACE.OLEDB.12.0";
                                cb.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES;IMEX=0;");
                            }
                            DataTable dt = new DataTable("Datos");
                            using (OleDbConnection conn = new OleDbConnection(cb.ConnectionString))
                            {
                                //Abrimos la conexión
                                conn.Open();
                                using (OleDbCommand cmd = conn.CreateCommand())
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = "SELECT * FROM [Hoja1$]";
                                    //Guardamos los datos en el DataTable
                                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                                    da.Fill(dt);
                                }
                                //Cerramos la conexión

                                var results = from myRow in dt.AsEnumerable()
                                              select myRow;
                                conn.Close();

                                ugrdCuenta.DataSource = results.Select(c => new CUENTAS_PACIENTES()
                                {
                                    ATE_CODIGO = Convert.ToInt32(txtAtencion.Text),
                                    CUE_FECHA = Convert.ToDateTime(c[0]),
                                    PRO_CODIGO_BARRAS = (c[1]).ToString(),
                                    CUE_DETALLE = (c[2]).ToString(),
                                    CUE_VALOR_UNITARIO = Convert.ToDecimal(c[3]),
                                    CUE_CANTIDAD = Convert.ToByte(c[4]),
                                    CUE_VALOR = Convert.ToDecimal(c[5]),
                                    CUE_IVA = Convert.ToDecimal(0.00),
                                    CUE_ESTADO = 1,
                                    PRO_CODIGO = "0",
                                    CUE_NUM_FAC = "0",
                                    CUE_NUM_CONTROL = "0",
                                    MED_CODIGO = 0,
                                    RUB_CODIGO =
                                        ((RUBROS)(cmb_Rubros.SelectedItem)).
                                        RUB_CODIGO,
                                    PED_CODIGO =
                                        ((PEDIDOS_AREAS)(cmb_Areas.SelectedItem))
                                        .DIV_CODIGO,
                                    ID_USUARIO =
                                        His.Entidades.Clases.Sesion.codUsuario,
                                    CAT_CODIGO = 0
                                }).ToList();
                                UltraGridBand bandUno = ugrdCuenta.DisplayLayout.Bands[0];
                                SummarySettings sumTarifa = bandUno.Summaries.Add("CUE_VALOR", SummaryType.Sum,
                                                                                  bandUno.Columns["CUE_VALOR"]);
                                MessageBox.Show("Archivo cargado  exitosamente");
                                btnActualizar.Enabled = false;
                                btnGuardar.Enabled = true;
                            }
                        }
                    }
                }
                txtDirArchivo.Text = string.Empty;
                //ultraTabControlCuenta.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo  cargar  archivo ");
                btnGuardar.Enabled = false;
            }
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtCControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                dtpFechaPedido.Focus();
            }
        }

        private void chbEliminar_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
        }

        private void btnEliminarProductos_Click(object sender, EventArgs e)
        {
            int contador = 0;
            if (chbEliminar.Checked == true)
            {
                if (ugrdCuenta.Rows.Count > 0)
                {
                    for (int i = 0; i < ugrdCuenta.Rows.Count; i++)
                    {
                        NegCuentasPacientes.EliminarCuenta(Convert.ToInt32(ugrdCuenta.Rows[i].Cells[0].Value));
                    }
                    MessageBox.Show("Datos eliminados Correctamente");
                    cargarProductos();
                    chbEliminar.Checked = false;
                }
            }
            if (checkBox1.Checked == true)
            {
                foreach (var item in ugrdCuenta.Rows)
                {
                    if ((bool)item.Cells["CHECKTRANS"].Value == true)
                    {
                        NegCuentasPacientes.EliminarCuenta(Convert.ToInt32(item.Cells["CUE_CODIGO"].Value));
                        contador++;
                    }
                }
                if (contador > 0)
                {
                    MessageBox.Show("Registros Eliminados ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarProductos();
                    checkBox1.Checked = false;

                }
                else
                {
                    MessageBox.Show("Seleccione una  cuenta para eliminar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ugrdCuenta.DataSource = null;
            HabilitarBotones(true, false, true, true, true);

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            chbEliminar.Checked = false;
        }

        private void btnMedico_Click(object sender, EventArgs e)
        {
            //His.Admision.
            List<MEDICOS> listaMedicos = NegMedicos.listaMedicosIncTipoMedico();

            His.Admision.frm_Ayudas ayuda = new frm_Ayudas(listaMedicos, "MEDICOS", "CODIGO", "");
            ayuda.campoPadre = txtCodMedico;
            ayuda.ShowDialog();

            if (ayuda.campoPadre.Text != string.Empty)
                CargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
        }

        private void CargarMedico(int codMedico)
        {
            DataTable med = NegMedicos.MedicoIDValida(codMedico);
            if (med.Rows[0][0].ToString() == "7")
            {
                MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            medico = NegMedicos.MedicoID(codMedico);
            if (medico != null)
                txtNombreMedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + "  " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
            else
                txtNombreMedico.Text = string.Empty;
        }

        private void txtCodMedico_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.F1))
                {
                    List<MEDICOS> listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
                    His.Admision.frm_Ayudas ayuda = new frm_Ayudas(listaMedicos, "MEDICOS", "CODIGO", "");
                    ayuda.campoPadre = txtCodMedico;
                    ayuda.ShowDialog();
                    if (ayuda.campoPadre.Text != string.Empty)
                        CargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
                }
                else
                {
                    if ((e.KeyCode == Keys.Enter))
                    {
                        if (txtCodMedico.Text.Trim() != string.Empty)
                            CargarMedico(Convert.ToInt32(txtCodMedico.Text.Trim()));
                        else
                        {
                            txtNombreMedico.Text = string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void txtPorcentaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtCantidad.Focus();
            }
        }

        private void btnMedico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtCodMedico.Focus();
            }
        }

        private void txtCodMedico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                btnAnadir.Focus();
            }
        }

        private void dateTimePickerFechaAlta_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePickerFechaAlta.Value != Convert.ToDateTime("12/12/2011") || dateTimePickerFechaAlta.Value == DateTime.Now)
            {
                if (dateTimePickerFechaAlta.Value.Date >= dateTimePickerFechaIngreso.Value.Date)
                    dtpFechaPedido.Value = dateTimePickerFechaAlta.Value;
                else
                    dateTimePickerFechaAlta.Value = DateTime.Now;
            }
            else
                dateTimePickerFechaAlta.Value = DateTime.Now;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("¿Está Seguro de Cerrar Prefactura?", "HIS3000", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    atencion.ESC_CODIGO = 3;
                    atencion.ATE_FECHA_ALTA = dateTimePickerFechaAlta.Value;
                    
                    NegAtenciones.EditarAtencionAdmision(atencion, 0);
                    MessageBox.Show("Atención Prefacturada  Exitosamente");
                    this.Close();
                }
                else
                {
                    //No hace nada.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                dateTimePickerFechaAlta.Enabled = true;
                this.accionCuenta = true;
                HabilitarBotones(false, true, true, true, true);
                //atencion.ATE_FECHA_ALTA = dateTimePickerFechaAlta.Value;
                //NegAtenciones.EditarAtencionAdmision(atencion);
                //MessageBox.Show("Atención Actualizada  Exitosamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error............! ");
            }
        }

        private void rdb_ConIva_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_ConIva.Checked == true)
                txtPorcentaje.Text = "10";
            else
                txtPorcentaje.Text = "100";
        }

        private void rdb_SinIva_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_SinIva.Checked == true)
                txtPorcentaje.Text = "100";

        }

        private void cuentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmDetalleCuenta(Convert.ToInt32(txtAtencion.Text.Trim()));
            frm.Show();
        }

        private void cuentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmConsolidarCuentas(Convert.ToInt32(txtAtencion.Text.Trim()), txtPacienteHCL.Text.Trim(),
                txtPacienteNombre.Text + " " + txtPacienteNombre2.Text + " " + txtPacienteApellidoPaterno.Text + " " + txtPacienteApellidoMaterno.Text,
                txtPacienteCedula.Text, txtHabitacion.Text, Convert.ToString(dateTimePickerFechaIngreso.Value), Convert.ToString(dateTimePickerFechaAlta.Value));
            frm.Show();
        }

        private void btnCancelarRubro_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void btnCancelarRubro_KeyDown(object sender, KeyEventArgs e)
        {
            limpiarCampos();
        }

        private void gpbDatos_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmActualizaFechas frmActualizar = new frmActualizaFechas(atencion.ATE_CODIGO, Convert.ToDateTime(atencion.ATE_FECHA_INGRESO), Convert.ToDateTime(atencion.ATE_FECHA_ALTA));
            frmActualizar.ShowDialog();

            cargarProductos();
        }

        private void txtCodMedico_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombreMedico_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Data.OleDb;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using His.Entidades;
//using His.Entidades.Pedidos;
//using His.Formulario;
//using His.Negocio;
//using His.Parametros;
//using Infragistics.Win.UltraWinGrid;
//using Recursos;
//using frm_Ayudas = His.Admision.frm_Ayudas;

//namespace CuentaPaciente
//{
//    public partial class frmCuentaPacientesIess : Form
//    {
//        #region Declaracion de Variables
//        public ListView listaDatos = null;
//        public PACIENTES paciente;
//        public PACIENTES_DATOS_ADICIONALES datos;
//        ATENCIONES atencion = null;
//        public int codigoAtencion;
//        public List<PRODUCTO> listaProductosSolicitados;
//        private byte codigoEstacion;
//        public PRODUCTO producto;
//        private string direccion;
//        List<DtoCuentaCargas> listaCuentaCargas = new List<DtoCuentaCargas>();
//        List<DtoDetalleCuentaPaciente> listaCuenta = new List<DtoDetalleCuentaPaciente>();
//        ATENCIONES_DETALLE_SEGUROS atdSeguros = new ATENCIONES_DETALLE_SEGUROS();
//        CUENTAS_PACIENTES cuentaPacientes = new CUENTAS_PACIENTES();
//        CUENTAS_PACIENTES cuentaModificada = null;
//        List<HC_CATALOGO_SUBNIVEL> listaCatSub;
//        PEDIDOS_AREAS pedidosArea = new PEDIDOS_AREAS();
//        private Boolean accionCuenta = false;
//        private TARIFARIOS_DETALLE tarifarioHI;
//        private TARIFARIOS_DETALLE tarifarioH;
//        decimal unidadesUvr;
//        decimal precioUnitario;
//        private MEDICOS medico;
//        private List<ATENCION_DETALLE_CATEGORIAS> detalleCategorias = null;
//        private List<ASEGURADORAS_EMPRESAS> aseguradoras = new List<ASEGURADORAS_EMPRESAS>();
//        private ATENCION_DETALLE_CATEGORIAS atencionDCategoria;
//        Int32 NumeroFila = 0;
//        Infragistics.Win.UltraWinGrid.UltraGridRow gridRow;

//        #endregion

//        #region Contructor
//        public frmCuentaPacientesIess()
//        {
//            InitializeComponent();
//            HabilitarBotones(true, false,false,true, true);
       
//            //ultraTabControlCuenta.Enabled = true;
//        }
//        #endregion
//        private void cargarProductos()
//        {
            
//            btnGuardar.Enabled = false;
//            List<CUENTAS_PACIENTES> listaCuentasPacientes = new List<CUENTAS_PACIENTES>();
//            ugrdCuenta.DataSource = null;
//            try
//            {
//                if (txtAtencion.Text.Trim() != string.Empty || txtAtencion.Text.Trim() != "")
//                {
//                    pedidosArea = (PEDIDOS_AREAS) cmb_Areas.SelectedItem;
//                    if (pedidosArea != null)
//                    {
//                        listaCuentasPacientes =
//                            NegCuentasPacientes.RecuperarCuentaArea(Convert.ToInt32(txtAtencion.Text.Trim()),Convert.ToInt32(pedidosArea.DIV_CODIGO));
//                        if (listaCuentasPacientes.Count > 0)
//                        {
//                            ugrdCuenta.DataSource = listaCuentasPacientes;
//                            UltraGridBand bandUno = ugrdCuenta.DisplayLayout.Bands[0];
//                            SummarySettings sumTarifa = bandUno.Summaries.Add("CUE_VALOR", SummaryType.Sum,
//                                                                              bandUno.Columns["CUE_VALOR"]);
//                            if (NumeroFila != 0)
//                            {
//                                ugrdCuenta.Rows[NumeroFila].Activate();
//                            }
//                        }
//                    }
//                }

//                //cargo los valores por defecto de la grilla
//            }
//            catch (Exception err)
//            {
//                MessageBox.Show(err.Message);
//            }

//        }
//        public void CargarPaciente(int codigo)
//                                {
//            //paciente = new DtoPacientes();
//            paciente = NegPacientes.RecuperarPacienteID(codigo);

//            if (paciente != null)
//            {

//                txtPacienteNombre.Text = paciente.PAC_NOMBRE1;
//                txtPacienteNombre2.Text = paciente.PAC_NOMBRE2;
//                txtPacienteApellidoPaterno.Text = paciente.PAC_APELLIDO_PATERNO;
//                txtPacienteApellidoMaterno.Text = paciente.PAC_APELLIDO_MATERNO;
//                txtPacienteHCL.Text = paciente.PAC_HISTORIA_CLINICA;
//                txtPacienteCedula.Text = paciente.PAC_IDENTIFICACION;
//                datos = NegPacienteDatosAdicionales.RecuperarDatosAdicionalesPaciente(paciente.PAC_CODIGO);
//                txtPacienteDireccion.Text = datos.DAP_DIRECCION_DOMICILIO;
//                txtPacienteTelf.Text = datos.DAP_TELEFONO1;
//                //CargarAtencion();
//                CargarDatos();
//                limpiarCampos();
//            }
//            else
//            {
//                txtPacienteNombre.Text = string.Empty;
//                txtPacienteNombre2.Text = string.Empty;
//                txtPacienteApellidoPaterno.Text = string.Empty;
//                txtPacienteApellidoMaterno.Text = string.Empty;
//                txtPacienteDireccion.Text = string.Empty;
//                txtPacienteHCL.Text = string.Empty;
//                txtPacienteTelf.Text = string.Empty;
//                txtPacienteCedula.Text = string.Empty;
//                datos = null;
//                limpiarCampos();
//            }

//            //cargarFiltros();
//        }

//        private void CargarAtencion()
//        {

//            try
//            {
//                atencion = NegAtenciones.AtencionID(Convert.ToInt32(txtAtencion.Text.Trim()));
//                if (atencion != null)
//                {
//                    atdSeguros = NegAtencionDetalleSeguros.RecuAtencionesDetalleSeguros(atencion.ATE_CODIGO);                   
//                    if (atdSeguros != null)
//                    {
                       
//                        //txt_nombreTitular.Text = atdSeguros.ADS_ASEGURADO_NOMBRE;
//                        //txt_CedulaTitular.Text = atdSeguros.ADS_ASEGURADO_CEDULA;
//                        //txt_CiudadTitular.Text = atdSeguros.ADS_ASEGURADO_CIUDAD;
//                        //txt_TelefonoTitular.Text = atdSeguros.ADS_ASEGURADO_TELEFONO;
//                        //txt_DireccionTitular.Text = atdSeguros.ADS_ASEGURADO_DIRECCION;
//                    }else
//                    {
//                        //txt_nombreTitular.Text = string.Empty;
//                        //txt_CedulaTitular.Text = string.Empty;
//                        //txt_CiudadTitular.Text = string.Empty;
//                        //txt_TelefonoTitular.Text = string.Empty;
//                        //txt_DireccionTitular.Text = string.Empty;
//                    }
//                    CargarConvenio();
//                }
//            }catch (Exception err) { MessageBox.Show(err.Message);}

//        }

//        private void CargarConvenio()
//        {
//            //ATENCION_DETALLE_CATEGORIAS
//            List<ASEGURADORAS_EMPRESAS> listaAseguradoras = new List<ASEGURADORAS_EMPRESAS>();
//            detalleCategorias = NegAtencionDetalleCategorias.RecuperarDetalleCategoriasAtencion(atencion.ATE_CODIGO);
//            Int16 CategoriaId=0; // Variable para cargar la Aseguradora (IESS/SOAT) / Giovanny Tapia /30/08/2012
//            if (detalleCategorias != null)
//            {
//                foreach (ATENCION_DETALLE_CATEGORIAS detalle in detalleCategorias)
//                {
//                    if (detalle != null)
//                    {
//                        if ((Convert.ToInt32(detalle.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value) == 21) || (Convert.ToInt32(detalle.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value) == 125) || (Convert.ToInt32(detalle.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value) == 54) || (Convert.ToInt32(detalle.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value) == 62)) // Convenio 21=IESS /125 soat (Aumento 125 para que se cargue aseguradoras SOAT )(Aumento 54 para que se cargue aseguradoras MINISTERIO DE SALUD ) /Giovanny Tapia / 26/11/2012
//                        {
//                            CategoriaId=(Convert.ToInt16(detalle.CATEGORIAS_CONVENIOSReference.EntityKey.EntityKeyValues[0].Value)) ;
//                            txtConvenio.Text =
//                                //((CATEGORIAS_CONVENIOS) (NegCategorias.RecuperaCategoriaID(21))).CAT_NOMBRE;                     
//                                ((CATEGORIAS_CONVENIOS) (NegCategorias.RecuperaCategoriaID(CategoriaId))).CAT_NOMBRE; //Busco el nombre de la aseguradora apartir del codigo / Giovanny Tapia / 30/08/2012
                                
//                            ultraTabControlCuenta.Enabled = true;
//                            break;
//                        }else
//                        {
//                            txtConvenio.Text = String.Empty;
//                            ultraTabControlCuenta.Enabled = false;
//                        }
//                    }
//                }
//            }
//            else
//            {
//                txtConvenio.Text = String.Empty;
//                ultraTabControlCuenta.Enabled = false;
//            }
//        }


//        #region Cargar Datos  public void cargarFiltros()
        

//        public void CargarDatos()
//        {
//            ultraTabControlCuenta.Enabled = true;
//            cmb_Areas.DataSource = NegPedidos.recuperarListaAreas().OrderBy(a => a.PEA_NOMBRE).ToList();
//            cmb_Areas.ValueMember = "PEA_CODIGO";
//            cmb_Areas.DisplayMember = "PEA_NOMBRE";
//            cmb_Areas.SelectedIndex = 0;
//            listaCatSub = NegHcCatalogoSubNivel.RecuperarSubNivel(40);
//            //cmb_Parentesco.DataSource = listaCatSub;
//            //cmb_Parentesco.DisplayMember = "CA_DESCRIPCION";
//            //cmb_Parentesco.ValueMember = "CA_CODIGO";
//            //cmb_Parentesco.AutoCompleteSource = AutoCompleteSource.ListItems;
//            //cmb_Parentesco.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
//            txtCantidad.Text = "1";
//            ArchivoIni archivo = new ArchivoIni(Environment.CurrentDirectory + "\\his3000.ini");
//            byte codigoEstacion = Convert.ToByte(archivo.IniReadValue("Pedidos", "estacion"));
//        }

//        public void HabilitarBotones(Boolean editar,Boolean guardar, Boolean cancelar,Boolean salir, Boolean cerrar )
//        {
//            btnActualizar.Enabled = editar;
//            btnGuardar.Enabled = guardar;
//            btnCancelar.Enabled = cancelar;
//            btnSalir.Enabled = salir;
//            //btnEditar.Enable = editar;
//            btnCerrar.Enabled = cerrar;
//            gpbDatos.Enabled = true;
//        }

//        public void GuardarDatos()
//        {
//            if(!ValidarCampos())
//            {
//                if (ugrdCuenta.Rows.Count > 0)
//                {
//                    List<CUENTAS_PACIENTES> listaCuentas = new List<CUENTAS_PACIENTES>();
                   
//                }
//            }
//        }

//        public bool ValidarCampos()
//        {
//            try
//            {
//                erroresAtencion.Clear();
//                bool validoCampo = true;
//                if (ugrdCuenta.Rows.Count <= 0)
//                {
//                    AgregarError(ugrdCuenta);
//                    validoCampo = false;
//                }
//                return validoCampo;
//            }catch (Exception err) { MessageBox.Show(err.Message);return false;}
//        }

//        private void AgregarError(Control control)
//        {
//            erroresAtencion.SetError(control, "Campo Requerido");
//        }


//        private void limpiarCampos()
//        {
//            cmb_Areas.Enabled = true;
//            cuentaModificada = null;
//            txt_Codigo.Text = string.Empty;
//            txtCodigoSic.Text = string.Empty;
//            txt_Nombre.Text = string.Empty;
//            txtValorNeto.Text = "0.00";
//            txtCantidad.Text = "1";
//            txtTotal.Text = "0.00";

//            if (chkNumeroControl.Checked != true)
//            {
//            txtCControl.Text = string.Empty;
//            }


//            txtCodMedico.Text = string.Empty;
//            txtNombreMedico.Text = string.Empty;
//            precioUnitario = 0;
//            txtPorcentaje.Text = "100";
//            rdb_SinIva.Checked = true;
//            rdb_ConIva.Enabled = false;
//            rdb_SinIva.Enabled = false;

//            cmbTipoMedico.SelectedIndex = 0;

//        }


//        #endregion
        
//        #region Eventos
//        //public void cargarFiltros()
//        //{
//        //    HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM); 
//        //    var tarifarioQuery = from t in contexto.TARIFARIOS.Include("ESPECIALIDADES_TARIFARIOS")
//        //                         orderby t.TAR_CODIGO
//        //                         select t;
//        //    try
//        //    {
//        //        // lleno la lista de tarifarios
//        //        this.tarifarioList.DataSource = tarifarioQuery;
//        //        this.tarifarioList.DisplayMember = "TAR_NOMBRE";
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        MessageBox.Show(ex.Message);
//        //    }
//        //    ultraGrid.DataSource = null;
//        //    ultraGrid.ClearUndoHistory();
//        //    ultraGrid.DataMember = null;
//        //}

       


//        //public void cargarAseguradoras(Int16 codigoTarifario)
//        //{
//        //    try
//        //    {
//        //        HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
//        //        var aseguradorasQuery = (from a in contexto.ASEGURADORAS_EMPRESAS
//        //                                 join c in contexto.CONVENIOS_TARIFARIOS on a.ASE_CODIGO equals c.ASEGURADORAS_EMPRESAS.ASE_CODIGO
//        //                                 join e in contexto.ESPECIALIDADES_TARIFARIOS on c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO equals e.EST_CODIGO
//        //                                 where a.ASE_ESTADO == true && e.TARIFARIOS.TAR_CODIGO == codigoTarifario
//        //                                 select a).Distinct().OrderBy(a => a.ASE_NOMBRE).ToList();
//        //        this.aseguradoraList.DataSource = null;
//        //        this.aseguradoraList.DataSource = aseguradorasQuery;
//        //        this.aseguradoraList.DisplayMember = "ASE_NOMBRE";
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        MessageBox.Show(ex.Message);
//        //    }
//        //}

//        private void txtCodigoPaciente_TextChanged(object sender, EventArgs e)
//        {
         
//            btnActualizar.Enabled = true;

//            btnCerrar.Enabled = true;
//            try
//            {
//                if (Microsoft.VisualBasic.Information.IsNumeric(txtCodigoPaciente.Text) == false)
//                    txtCodigoPaciente.Text = string.Empty;

//                if (txtCodigoPaciente.Text != string.Empty)
//                {
//                    CargarPaciente(Convert.ToInt32(txtCodigoPaciente.Text.ToString()));

//                    //if ((bool)txtCodigoPaciente.Tag)
//                    //    cargarDatosPorPaciente(Convert.ToInt32(txtCodigoPaciente.Text.ToString()));
//                }
//                else
//                {
//                    paciente = null;
//                    txtPacienteNombre.Text = string.Empty;
//                    txtPacienteNombre2.Text = string.Empty;
//                    txtPacienteApellidoPaterno.Text = string.Empty;
//                    txtPacienteApellidoMaterno.Text = string.Empty;
//                    txtPacienteDireccion.Text = string.Empty;
//                    txtPacienteHCL.Text = string.Empty;
//                    txtPacienteTelf.Text = string.Empty;
//                    txtPacienteCedula.Text = string.Empty;

//                    txtPacienteNombre.ReadOnly = false;
//                    txtPacienteNombre2.ReadOnly = false;
//                    txtPacienteApellidoPaterno.ReadOnly = false;
//                    txtPacienteApellidoMaterno.ReadOnly = false;
//                    txtPacienteDireccion.ReadOnly = false;
//                    txtPacienteHCL.ReadOnly = false;
//                    txtPacienteTelf.ReadOnly = false;
//                    txtPacienteCedula.ReadOnly = false;

//                }
//            }
//            catch (Exception err)
//            {

//            }
//        }

//        private void txtAtencion_TextChanged(object sender, EventArgs e)
//                        {
//            try
//            {
//                if (Microsoft.VisualBasic.Information.IsNumeric(txtAtencion.Text) == false)
//                    txtAtencion.Text = string.Empty;

//                if (txtAtencion.Text != string.Empty)
//                {
//                    CargarAtencion();
//                    cargarProductos();
//                }
//                else
//                {
                   

//                }
//            }
//            catch (Exception err)
//            {

//            }

//        }

//        private void ayudaPacientes_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                string a = "";
//                frmAyudaPacientesIess frm = new frmAyudaPacientesIess();
//                frm.campoPadre = txtCodigoPaciente;
//                frm.campoAtencion = txtAtencion;
//                frm.campoHabitacion = txtHabitacion;
//                frm.txtfactura = txtFactura;
//                frm.txtNumControl = txtNumControl;
//                frm.fechafacturacion = dateTimePickerFechaFactura;
//                frm.fechaingreso = dateTimePickerFechaIngreso;
//                frm.fechaalta = dateTimePickerFechaAlta;
//                frm.ShowDialog();
//                    HabilitarBotones(true, true, true, true, true);
//                    if (txtAtencion.Text != "")
//                        NumeroFila = 0;
//                        cargarProductos();
//            }
//            catch (Exception ex)
//            {
//                //MessageBox.Show(ex.InnerException.Message);
//            }
//        }


//        private void tarifarioList_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            //TARIFARIOS tarifario = (TARIFARIOS)this.tarifarioList.SelectedItem;
//            //cargarEspecialidadesTarifario(tarifario);
//            //cargarAseguradoras(tarifario.TAR_CODIGO);
//            //if (this.Visible == true)
//            //{
//            //    //cargarDetalleTarifario();
//            //}
//        }

//        private void btnSolicitar_Click(object sender, EventArgs e)
//        {
            
//        }

//        private void cmb_Areas_SelectedIndexChanged(object sender, EventArgs e)
//        {
            
//        }

//        private void cmb_Areas_SelectedIndexChanged_1(object sender, EventArgs e)
//        {
//            try
//            {
//                cmb_Rubros.DataSource = null;
//                if (cmb_Areas.SelectedItem != null)
//                {

//                    if (this.chkNumeroControl.Checked == true)
//                    {
//                        this.chkNumeroControl.Checked = false;
//                    }

//                    limpiarCampos();
//                    PEDIDOS_AREAS areaP = (PEDIDOS_AREAS)cmb_Areas.SelectedItem;
//                    List<RUBROS> listaRubros = NegRubros.recuperarRubros(Convert.ToInt32(areaP.DIV_CODIGO));
//                    if (areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoHonorarios)
//                        cmb_Rubros.DataSource = listaRubros.OrderByDescending(pa => pa.RUB_NOMBRE.Trim()).ToList();
//                    else
//                        cmb_Rubros.DataSource = listaRubros.OrderBy(pa => pa.RUB_NOMBRE.Trim()).ToList();
//                    if (areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoLaboratorioP || areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoHonorarios)
//                    {
//                        txtCodMedico.Text = string.Empty;
//                        txtNombreMedico.Text = string.Empty;
//                        btnMedico.Enabled = true;
//                        txtCodMedico.Enabled = true;
//                        this.cmbTipoMedico.Enabled = true;
//                    }
//                    else
//                    {
//                        txtCodMedico.Text = string.Empty;
//                        txtNombreMedico.Text = string.Empty;
//                        btnMedico.Enabled = false;
//                        txtCodMedico.Enabled = false;
//                        this.cmbTipoMedico.Enabled = false;
//                        if (areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoOtrosP || areaP.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
//                        {
//                            rdb_ConIva.Enabled = true;
//                            rdb_SinIva.Enabled = true;
//                        }
//                    }

//                    cmb_Rubros.DisplayMember = "RUB_NOMBRE".Trim();
//                    cmb_Rubros.ValueMember = "RUB_CODIGO";
//                    NumeroFila = 0;
//                    cargarProductos();
//                }
//            }
//            catch (Exception err)
//            {
//                MessageBox.Show(err.Message);
//            }
//        }

//        private void cmb_Rubros_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            //try
//            //{
//            //    cmb_Rubros.DataSource = null;
//            //    if (cmb_Rubros.SelectedItem != null)
//            //    {
//            //        RUBROS rubroC = (RUBROS)cmb_Rubros.SelectedItem;
//            //        if (rubroC.RUB_CODIGO == His.Parametros.CuentasPacientes.RubroOtrosP || rubroC.RUB_CODIGO == His.Parametros.CuentasPacientes.RubroSuministros) 
//            //        {
//            //        }
//            //    }
//            //}
//            //catch (Exception err)
//            //{
//            //    MessageBox.Show(err.Message);
//            //}
//        }

//        //private void btnExaminar_Click(object sender, EventArgs e)
//        //{
//        //    openFileDialogo.InitialDirectory = "c:\\";
//        //    openFileDialogo.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
//        //    openFileDialogo.FilterIndex = 2;
//        //    openFileDialogo.RestoreDirectory = true;

//        //    if (openFileDialogo.ShowDialog() == DialogResult.OK)
//        //    {
//        //        txtDirArchivo.Text = openFileDialogo.FileName;

//        //    }
//        //}

//        //private void button2_Click(object sender, EventArgs e)
//        //{
//        //    ugrdCuenta.DataSource = null;
//        //    try
//        //    {
//        //        var listaFilas = ImportarArchivoDelimitado(txtDirArchivo.Text.Trim(), new char[] { ';' });

//        //        ugrdCuenta.DataSource = listaFilas.Select(c => new CUENTAS_PACIENTES()
//        //        {
//        //            ATE_CODIGO = Convert.ToInt32(txtAtencion.Text),
//        //            CUE_FECHA = Convert.ToDateTime(c[0]),
//        //            PRO_CODIGO = (c[1]).ToString(),
//        //            CUE_DETALLE = (c[2]).ToString(),
//        //            CUE_VALOR_UNITARIO = Convert.ToDecimal(c[3]),
//        //            CUE_CANTIDAD = Convert.ToByte(c[4]),
//        //            CUE_VALOR = Convert.ToDecimal(c[5]),
//        //            CUE_IVA = Convert.ToDecimal(0.00),
//        //            CUE_ESTADO = 1,
//        //            CUE_NUM_FAC = "0",
//        //            RUB_CODIGO =
//        //                ((RUBROS)(cmb_Rubros.SelectedItem)).
//        //                RUB_CODIGO,
//        //            PED_CODIGO =
//        //                ((PEDIDOS_AREAS)(cmb_Areas.SelectedItem))
//        //                .
//        //                PEA_CODIGO,
//        //            ID_USUARIO =
//        //                His.Entidades.Clases.Sesion.codUsuario,
//        //            CAT_CODIGO = 0
//        //        }).ToList();
//        //        UltraGridBand bandUno = ugrdCuenta.DisplayLayout.Bands[0];
//        //        SummarySettings sumTarifa = bandUno.Summaries.Add("CUE_VALOR", SummaryType.Sum,
//        //                                                          bandUno.Columns["CUE_VALOR"]);
//        //        MessageBox.Show("Archivo cargado  exitosamente");
//        //        btnGuardar.Enabled = true;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        MessageBox.Show("No se pudo  cargar  archivo ");
//        //        btnGuardar.Enabled = false;
//        //    }
//        //    //SummarySettings sumTarifa = ultraGrid.DisplayLayout.Bands[Num_banda].Summaries.Add("NOMBRE_COLUMNA", SummaryType.Sum, bandUno.Columns["NOMBRE_COLUMNA"]);
//        //}

//        public List<string[]> ImportarArchivoDelimitado(string archivo, char[] separador)
//        {
//            var listaFilas = new List<string[]>();
//            if (txtDirArchivo.Text.Trim() != string.Empty)
//            {
//                try
//                {
//                    using (var file = new StreamReader(archivo))
//                    {
//                        string linea;
//                        while ((linea = file.ReadLine()) != null)
//                        {
//                            if (linea.Trim().Length > 0)
//                            {
//                                string[] columns = linea.Split(separador, StringSplitOptions.RemoveEmptyEntries);
//                                listaFilas.Add(columns);
//                            }
//                        }

                       
//                    }

//                }catch(Exception err)
//                {
//                    MessageBox.Show(err.Message + "Formato de archivo incorrecto");
//                }
//            }
//            return listaFilas;
//        }

//        private void ultraGrid_InitializeLayout(object sender, InitializeLayoutEventArgs e)
//        {
//            if (!ugrdCuenta.DisplayLayout.Bands[0].Columns.Exists("CHECKTRANS"))
//            {
//                ugrdCuenta.DisplayLayout.Bands[0].Columns.Add("CHECKTRANS", "");
//                ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].DataType = typeof(bool);
//                ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
//                ugrdCuenta.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
              
//            }
//            ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].Header.VisiblePosition = 0;
//            ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].CellActivation = Activation.AllowEdit;
//            ugrdCuenta.DisplayLayout.Bands[0].Columns["CHECKTRANS"].DefaultCellValue = true;
      
//            UltraGridBand bandUno = ugrdCuenta.DisplayLayout.Bands[0];
//            //ugrdCuenta.DisplayLayout.ViewStyle = ViewStyle.SingleBand;

//            //ugrdCuenta.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
//            //ugrdCuenta.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
//            //ugrdCuenta.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
//            //bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

//            //bandUno.Override.CellClickAction = CellClickAction.RowSelect;
//            //bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

//            //ugrdCuenta.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
//            //ugrdCuenta.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
//            //ugrdCuenta.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

//            //Caracteristicas de Filtro en la grilla
//            ugrdCuenta.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
//            ugrdCuenta.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
//            ugrdCuenta.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
//            ugrdCuenta.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
//            ugrdCuenta.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

//            ugrdCuenta.DisplayLayout.UseFixedHeaders = true;

//            bandUno.Columns["CUE_CODIGO"].Header.Caption = "CODIGO";
//            bandUno.Columns["ATE_CODIGO"].Header.Caption = "ATENCION";
//            bandUno.Columns["CUE_FECHA"].Header.Caption = "FECHA";
//            bandUno.Columns["PRO_CODIGO"].Header.Caption = "CODIGO_PRO";
//            bandUno.Columns["CUE_DETALLE"].Header.Caption = "DESCRIPCION";
//            bandUno.Columns["CUE_VALOR_UNITARIO"].Header.Caption = "TOTAL_UNITARIO";
//            bandUno.Columns["CUE_CANTIDAD"].Header.Caption = "CANTIDAD";
//            bandUno.Columns["CUE_VALOR"].Header.Caption = "TOTAL";
//            bandUno.Columns["CUE_IVA"].Header.Caption = "IVA";
//            bandUno.Columns["CUE_ESTADO"].Header.Caption = "ESTADO";
//            bandUno.Columns["CUE_NUM_FAC"].Header.Caption = "N. FACTURA";
//            bandUno.Columns["RUB_CODIGO"].Header.Caption = "RUBRO";
//            bandUno.Columns["PED_CODIGO"].Header.Caption = "PEDIDO";
//            bandUno.Columns["ID_USUARIO"].Header.Caption = "USUARIO";
//            bandUno.Columns["CAT_CODIGO"].Header.Caption = "CATALOGO";
//            bandUno.Columns["PRO_CODIGO_BARRAS"].Header.Caption = "CODIGO";
//            bandUno.Columns["CUE_NUM_CONTROL"].Header.Caption = "NUM_CONTROL";
//            bandUno.Columns["Id_Tipo_Medico"].Header.Caption = "Id_Tipo_Medico";

//            bandUno.Columns["CUE_CODIGO"].Width = 80;
//            bandUno.Columns["ATE_CODIGO"].Width = 80;
//            bandUno.Columns["CUE_FECHA"].Width = 80;
//            bandUno.Columns["PRO_CODIGO"].Width = 80;
//            bandUno.Columns["CUE_DETALLE"].Width = 280;
//            bandUno.Columns["CUE_VALOR_UNITARIO"].Width = 80;
//            bandUno.Columns["CUE_CANTIDAD"].Width = 80;
//            bandUno.Columns["CUE_VALOR"].Width = 80;
//            bandUno.Columns["CUE_IVA"].Width = 50;
//            bandUno.Columns["CUE_ESTADO"].Width = 80;
//            bandUno.Columns["CUE_NUM_FAC"].Width = 80;
//            bandUno.Columns["RUB_CODIGO"].Width = 80;
//            bandUno.Columns["PED_CODIGO"].Width = 80;
//            bandUno.Columns["ID_USUARIO"].Width = 80;
//            bandUno.Columns["CAT_CODIGO"].Width = 80;
//            bandUno.Columns["PRO_CODIGO_BARRAS"].Width = 80;
//            bandUno.Columns["CUE_NUM_CONTROL"].Width = 80;
//            bandUno.Columns["Id_Tipo_Medico"].Width = 80;

//            bandUno.Columns["CUE_CODIGO"].Hidden = true;
//            bandUno.Columns["ATE_CODIGO"].Hidden = false;
//            bandUno.Columns["CUE_FECHA"].Hidden = false;
//            bandUno.Columns["PRO_CODIGO"].Hidden = false;
//            bandUno.Columns["CUE_DETALLE"].Hidden = false;
//            bandUno.Columns["CUE_VALOR_UNITARIO"].Hidden = false;
//            bandUno.Columns["CUE_CANTIDAD"].Hidden = false;
//            bandUno.Columns["CUE_VALOR"].Hidden = false;
//            bandUno.Columns["CUE_IVA"].Hidden = false;
//            bandUno.Columns["CUE_ESTADO"].Hidden = true;
//            bandUno.Columns["CUE_NUM_FAC"].Hidden = false;
//            bandUno.Columns["RUB_CODIGO"].Hidden = true;
//            bandUno.Columns["PED_CODIGO"].Hidden = true;
//            bandUno.Columns["ID_USUARIO"].Hidden = true;
//            bandUno.Columns["CAT_CODIGO"].Hidden = true;
//            bandUno.Columns["PRO_CODIGO_BARRAS"].Hidden = false;
//            bandUno.Columns["CUE_NUM_CONTROL"].Hidden = false;
//            bandUno.Columns["Id_Tipo_Medico"].Hidden = true;

//            //ordeno las columnas
//            bandUno.Columns["CUE_CODIGO"].Header.VisiblePosition = 0;
//            bandUno.Columns["ATE_CODIGO"].Header.VisiblePosition = 1;
//            bandUno.Columns["CUE_FECHA"].Header.VisiblePosition = 2;
//            bandUno.Columns["PRO_CODIGO_BARRAS"].Header.VisiblePosition = 3;
//            bandUno.Columns["CUE_DETALLE"].Header.VisiblePosition = 4;
//            bandUno.Columns["CUE_VALOR_UNITARIO"].Header.VisiblePosition = 5;
//            bandUno.Columns["CUE_CANTIDAD"].Header.VisiblePosition = 6;
//            bandUno.Columns["CUE_VALOR"].Header.VisiblePosition = 7;
//            bandUno.Columns["CUE_IVA"].Header.VisiblePosition = 8;
//            bandUno.Columns["CUE_NUM_CONTROL"].Header.VisiblePosition = 9;
//            bandUno.Columns["CUE_ESTADO"].Header.VisiblePosition = 10;
//            bandUno.Columns["CUE_NUM_FAC"].Header.VisiblePosition = 11;
//            bandUno.Columns["RUB_CODIGO"].Header.VisiblePosition = 12;
//            bandUno.Columns["PED_CODIGO"].Header.VisiblePosition = 13;
//            bandUno.Columns["ID_USUARIO"].Header.VisiblePosition = 14;
//            bandUno.Columns["CAT_CODIGO"].Header.VisiblePosition = 15;
//            bandUno.Columns["PRO_CODIGO"].Header.VisiblePosition = 16;     
//            //bandUno.Columns["Id_Tipo_Medico"].Header.VisiblePosition = 16;   
//        }

//        private void btnGuardar_Click(object sender, EventArgs e)
//        {

//            if (this.accionCuenta == true)
//            {
//                atencion.ATE_FECHA_ALTA = dateTimePickerFechaAlta.Value;
//                NegAtenciones.EditarAtencionAdmision(atencion,1);
//                NegCuentasPacientes.ActualizarFechaCuentas(atencion.ATE_CODIGO);
//                MessageBox.Show("Atención Actualizada  Exitosamente");
//                this.dateTimePickerFechaAlta.Enabled = false;
//                this.accionCuenta = false;
//            }
//            else
//            {
//                if (ValidarCampos())
//                {
//                    RUBROS rubroM = new RUBROS();
//                    RUBROS rubroS = new RUBROS();
//                    if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
//                    {
//                        rubroM = (RUBROS)(cmb_Rubros.SelectedItem);
//                        cmb_Rubros.SelectedIndex = 1;
//                        rubroS = (RUBROS)(cmb_Rubros.SelectedItem);
//                        cmb_Rubros.SelectedIndex = 0;
//                    }
//                    foreach (UltraGridRow cuenta in ugrdCuenta.Rows)
//                    {
//                        cuentaPacientes = new CUENTAS_PACIENTES();
//                        cuentaPacientes.ATE_CODIGO = Convert.ToInt32(cuenta.Cells[1].Value);
//                        cuentaPacientes.CUE_FECHA = Convert.ToDateTime(cuenta.Cells[2].Value);
//                        cuentaPacientes.PRO_CODIGO_BARRAS = (cuenta.Cells[15].Value).ToString();
//                        cuentaPacientes.CUE_DETALLE = (cuenta.Cells[3].Value).ToString();
//                        cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(cuenta.Cells[14].Value);
//                        cuentaPacientes.CUE_CANTIDAD = Convert.ToByte(cuenta.Cells[8].Value);
//                        cuentaPacientes.CUE_VALOR = Convert.ToDecimal(cuentaPacientes.CUE_VALOR_UNITARIO * cuentaPacientes.CUE_CANTIDAD);
//                        cuentaPacientes.CUE_ESTADO = Convert.ToInt32(cuenta.Cells[6].Value);
//                        cuentaPacientes.CUE_NUM_FAC = (cuenta.Cells[7].Value).ToString();
//                        cuentaPacientes.CUE_NUM_CONTROL = (cuenta.Cells[16].Value).ToString();
//                        cuentaPacientes.PRO_CODIGO = "0";
//                        cuentaPacientes.MED_CODIGO = 0;
//                        if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
//                        {
//                            if ((cuenta.Cells[15].Value).ToString().Substring(0, 1).Equals("2"))
//                            {
//                                cuentaPacientes.RUB_CODIGO = rubroS.RUB_CODIGO;
//                                string ivaTotal = Convert.ToString(Convert.ToDouble(cuentaPacientes.CUE_VALOR) * 0.12);
//                                cuentaPacientes.CUE_IVA = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
//                                //cuentaPacientes.CUE_IVA = Math.Round(Convert.ToDecimal((cuentaPacientes.CUE_VALOR) * Convert.ToDecimal(0.12)),3);
//                            }
//                            else
//                            {
//                                cuentaPacientes.RUB_CODIGO = rubroM.RUB_CODIGO;
//                                cuentaPacientes.CUE_IVA = 0;
//                            }
//                        }
//                        else
//                        {
//                            cuentaPacientes.RUB_CODIGO = Convert.ToInt16(cuenta.Cells[9].Value);
//                            cuentaPacientes.CUE_IVA = 0;
//                        }
//                        cuentaPacientes.PED_CODIGO = Convert.ToInt32(cuenta.Cells[10].Value);
//                        cuentaPacientes.ID_USUARIO = Convert.ToInt16(cuenta.Cells[11].Value);
//                        cuentaPacientes.CAT_CODIGO = Convert.ToInt16(cuenta.Cells[12].Value);
//                        NegCuentasPacientes.CrearCuenta(cuentaPacientes);
//                    }
//                    //NegCuentasPacientes.CrearCuenta(cuentaPacientes);
//                    CalcularIvaSuministros(Convert.ToInt32(txtAtencion.Text.Trim()));
//                    MessageBox.Show("Datos Almacenados Correctamente");
                   
//                    txtDirArchivo.Text = string.Empty;
//                    //ultraTabControlCuenta.Enabled = false;
//                    cargarProductos();
//                }
                
//            }

//            HabilitarBotones(true, false, false, true, true);
//        }


//        private void CalcularIvaSuministros(int codigoAtencion)
//        {
//            CUENTAS_PACIENTES cuentaIva = new CUENTAS_PACIENTES();
//            cuentaIva = NegCuentasPacientes.RecuperarCuentasIvaS(codigoAtencion, His.Parametros.CuentasPacientes.CodigoSuministros);
//            List<CUENTAS_PACIENTES> listaCuentaRubro = NegCuentasPacientes.RecuperarCuentasRubros(codigoAtencion,
//                                                                          His.Parametros.CuentasPacientes.
//                                                                              RubroSuministros);
//            List<CUENTAS_PACIENTES> listaCuentaRubroO = NegCuentasPacientes.RecuperarCuentasRubros(codigoAtencion,
//                                                                          His.Parametros.CuentasPacientes.RubroOtrosP);           
//            if(listaCuentaRubro.Count>0)
//            {
//                Decimal iva = 0;
//                CUENTAS_PACIENTES cuentas = new CUENTAS_PACIENTES();
//                for (int i = 0; i < listaCuentaRubro.Count; i++)
//                {
//                    cuentas = listaCuentaRubro.ElementAt(i);
//                    if (cuentas.CUE_ESTADO == 1)
//                    {
//                        iva = iva + Convert.ToDecimal(cuentas.CUE_VALOR);
//                    }
//                }
//                if (listaCuentaRubroO.Count > 0)
//                {
//                    for (int i = 0; i < listaCuentaRubroO.Count; i++)
//                    {
//                        cuentas = listaCuentaRubroO.ElementAt(i);
//                        if (cuentas.CUE_IVA > 0)
//                        {
//                            if (cuentas.CUE_ESTADO == 1)
//                            {
//                                iva = iva + Convert.ToDecimal(cuentas.CUE_VALOR);
//                            }
//                        }
//                    }
//                }
//                if(cuentaIva == null)
//                {
//                    cuentaPacientes = new CUENTAS_PACIENTES();
//                    cuentaPacientes.ATE_CODIGO = codigoAtencion;
//                    if(atencion.ATE_FECHA_INGRESO< atencion.ATE_FECHA_ALTA)
//                        cuentaPacientes.CUE_FECHA = atencion.ATE_FECHA_ALTA;
//                    else
//                        cuentaPacientes.CUE_FECHA = atencion.ATE_FECHA_INGRESO;
//                    cuentaPacientes.PRO_CODIGO_BARRAS = His.Parametros.CuentasPacientes.CodigoSuministros;
//                    cuentaPacientes.CUE_DETALLE = "IVA 12% SUMINISTROS";
//                    string ivaTotal = Convert.ToString(Convert.ToDouble(iva) * 0.12);
//                    cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));                    
//                    cuentaPacientes.CUE_CANTIDAD = 1;
//                    cuentaPacientes.CUE_VALOR = cuentaPacientes.CUE_VALOR_UNITARIO;                    
//                        //(iva * Convert.ToDecimal(0.12));
//                    cuentaPacientes.CUE_ESTADO = 1;
//                    cuentaPacientes.CUE_NUM_FAC = "0";
//                    cuentaPacientes.CUE_NUM_CONTROL = "0";
//                    cuentaPacientes.PRO_CODIGO = "0";
//                    cuentaPacientes.RUB_CODIGO = 26;
//                    cuentaPacientes.CUE_IVA = 0;
//                    cuentaPacientes.PED_CODIGO = Convert.ToInt32(His.Parametros.CuentasPacientes.CodigoServicosI);
//                    cuentaPacientes.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario; 
//                    cuentaPacientes.CAT_CODIGO = 0;
//                    cuentaPacientes.MED_CODIGO = 0; //----->> Para la relacion con los medicos. / Giovanny Tapia / 05/09/2012
//                    NegCuentasPacientes.CrearCuenta(cuentaPacientes);
//                }
//                else
//                {
//                    if (atencion.ATE_FECHA_INGRESO < atencion.ATE_FECHA_ALTA)
//                        cuentaIva.CUE_FECHA = atencion.ATE_FECHA_ALTA;
//                    else
//                        cuentaIva.CUE_FECHA = atencion.ATE_FECHA_INGRESO;
//                    string ivaTotal = Convert.ToString(Convert.ToDouble(iva) * 0.12);                    
//                    cuentaIva.CUE_VALOR = (iva * Convert.ToDecimal(0.12));
//                    cuentaIva.CUE_VALOR_UNITARIO = cuentaIva.CUE_VALOR;
//                    cuentaIva.MED_CODIGO = 0; //----->> Para la relacion con los medicos. / Giovanny Tapia / 05/09/2012
//                    NegCuentasPacientes.ModificarCuenta(cuentaIva);
                    
//                }
//            }

//        }

//        private void ActualizaFechaCuentas()
//        {

//            List<CUENTAS_PACIENTES> listaCuentasP = new List<CUENTAS_PACIENTES>();
//            listaCuentasP = NegCuentasPacientes.RecuperarCuenta(Convert.ToInt32(this.txtAtencion.Text.Trim()));



//        }

//        //private void CalcularIvaOtrosP(int codigoAtencion)
//        //{
//        //    CUENTAS_PACIENTES cuentaIva = new CUENTAS_PACIENTES();
//        //    cuentaIva = NegCuentasPacientes.RecuperarCuentasIvaS(codigoAtencion, His.Parametros.CuentasPacientes.CodigoSuministros);
//        //    List<CUENTAS_PACIENTES> listaCuentaRubro = NegCuentasPacientes.RecuperarCuentasRubros(codigoAtencion,
//        //                                                                  His.Parametros.CuentasPacientes.RubroOtrosP);            if (listaCuentaRubro.Count > 0)
//        //    {
//        //        Decimal iva = 0;
//        //        CUENTAS_PACIENTES cuentas = new CUENTAS_PACIENTES();
//        //        for (int i = 0; i < listaCuentaRubro.Count; i++)
//        //        {
//        //            cuentas = listaCuentaRubro.ElementAt(i);
//        //            if (cuentas.CUE_IVA > 0)
//        //            {
//        //                iva = iva + Convert.ToDecimal(cuentas.CUE_VALOR);
//        //            }
//        //        }
//        //        if (cuentaIva == null)
//        //        {
//        //            cuentaPacientes = new CUENTAS_PACIENTES();
//        //            cuentaPacientes.ATE_CODIGO = codigoAtencion;
//        //            if (atencion.ATE_FECHA_INGRESO < atencion.ATE_FECHA_ALTA)
//        //                cuentaPacientes.CUE_FECHA = atencion.ATE_FECHA_ALTA;
//        //            else
//        //                cuentaPacientes.CUE_FECHA = atencion.ATE_FECHA_INGRESO;
//        //            cuentaPacientes.PRO_CODIGO_BARRAS = His.Parametros.CuentasPacientes.CodigoSuministros;
//        //            cuentaPacientes.CUE_DETALLE = "IVA 12% SUMINISTROS";
//        //            cuentaPacientes.CUE_VALOR_UNITARIO = (iva * Convert.ToDecimal(0.12));
//        //            cuentaPacientes.CUE_CANTIDAD = 1;
//        //            cuentaPacientes.CUE_VALOR = (iva * Convert.ToDecimal(0.12));
//        //            cuentaPacientes.CUE_ESTADO = 1;
//        //            cuentaPacientes.CUE_NUM_FAC = "0";
//        //            cuentaPacientes.CUE_NUM_CONTROL = "0";
//        //            cuentaPacientes.PRO_CODIGO = "0";
//        //            cuentaPacientes.RUB_CODIGO = 26;
//        //            cuentaPacientes.CUE_IVA = 0;
//        //            cuentaPacientes.PED_CODIGO = Convert.ToInt32(His.Parametros.CuentasPacientes.CodigoServicosI);
//        //            cuentaPacientes.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
//        //            cuentaPacientes.CAT_CODIGO = 0;
//        //            NegCuentasPacientes.CrearCuenta(cuentaPacientes);
//        //        }
//        //        else
//        //        {
//        //            if (atencion.ATE_FECHA_INGRESO < atencion.ATE_FECHA_ALTA)
//        //                cuentaIva.CUE_FECHA = atencion.ATE_FECHA_ALTA;
//        //            else
//        //                cuentaIva.CUE_FECHA = atencion.ATE_FECHA_INGRESO;
//        //            cuentaIva.CUE_VALOR = (iva * Convert.ToDecimal(0.12));
//        //            cuentaIva.CUE_VALOR_UNITARIO = (iva * Convert.ToDecimal(0.12));
//        //            NegCuentasPacientes.ModificarCuenta(cuentaIva);
//        //        }
//        //    }

//        //}


//        #endregion

//        private void btnSolicitar_Click_1(object sender, EventArgs e)
//                    {
//            DataGridView listas = new DataGridView();
//            RUBROS rubro = (RUBROS)(cmb_Rubros.SelectedItem);
//            PEDIDOS_AREAS pedidosAreas = (PEDIDOS_AREAS)(cmb_Areas.SelectedItem);
//            if (pedidosAreas.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoHonorarios || pedidosAreas.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoImagen || pedidosAreas.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoServicosI)
//            {
//                frmAyudaTarifarios frm = new frmAyudaTarifarios(txtCodMedico.Text.Trim(),dateTimePickerFechaAlta);
//                frm.ShowDialog();
//                listas = frm.lista;
//                if (listas != null)
//                {
//                    btnGuardar.Enabled = true;
//                    for (Int16 i = 0; i <= listas.RowCount - 2; i++)
//                    {
//                        DataGridViewRow fila = listas.Rows[i];


//                        string  porcentaje = fila.Cells["PORCENTAJE"].Value.ToString();
                       
//                        string cantidad = fila.Cells["VCANTIDAD"].Value.ToString();
//                        decimal valorAux = Convert.ToDecimal(fila.Cells["VALORUNITARIO"].Value.ToString());
//                        double aux = (double)valorAux;
//                        valorAux = (decimal)aux;
//                        string referencia = fila.Cells["CODIGO"].Value.ToString(); ;
//                        string detalle = fila.Cells["NOMBRE"].Value.ToString(); ;
//                        cuentaPacientes = new CUENTAS_PACIENTES();
//                        cuentaPacientes.ATE_CODIGO = Convert.ToInt32(txtAtencion.Text.Trim());
//                        cuentaPacientes.CUE_FECHA = Convert.ToDateTime(fila.Cells["FECHAS"].Value.ToString());
//                        cuentaPacientes.PRO_CODIGO_BARRAS = referencia.Trim();
//                        cuentaPacientes.CUE_DETALLE = detalle.Trim();
//                        if(porcentaje.Trim()=="1.5")
//                        cuentaPacientes.CUE_VALOR_UNITARIO =  Convert.ToDecimal(Convert.ToDecimal(fila.Cells["TOTAL"].Value.ToString()));
//                        else
//                        cuentaPacientes.CUE_VALOR_UNITARIO = valorAux;
//                        cuentaPacientes.CUE_CANTIDAD = Convert.ToByte(cantidad);
//                        if (porcentaje.Trim() == "1.5")
//                            cuentaPacientes.CUE_VALOR = Convert.ToDecimal(Convert.ToDecimal(fila.Cells["TOTAL"].Value.ToString()));
//                        else
//                        {
//                            valorAux = (Convert.ToDecimal(Convert.ToDecimal(fila.Cells["VALORUNITARIO"].Value.ToString())) * (Convert.ToInt16(fila.Cells["PORCENTAJE"].Value.ToString()) / 100));
//                            cuentaPacientes.CUE_VALOR =
//                                Decimal.Round(
//                                    Convert.ToInt16(cantidad) *
//                                    Decimal.Round(Convert.ToDecimal(Decimal.Round(valorAux, 3)), 3), 3);
//                        }
//                        cuentaPacientes.CUE_IVA = 0;
//                        cuentaPacientes.PED_CODIGO = pedidosArea.DIV_CODIGO;
//                        cuentaPacientes.RUB_CODIGO = rubro.RUB_CODIGO;
//                        cuentaPacientes.CUE_ESTADO = 1;
//                        cuentaPacientes.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
//                        cuentaPacientes.CAT_CODIGO = 0;
//                        cuentaPacientes.PRO_CODIGO = "0";
//                        cuentaPacientes.CUE_NUM_CONTROL = "0";
//                        cuentaPacientes.CUE_NUM_FAC = "0";
//                        cuentaPacientes.MED_CODIGO = Convert.ToInt16(fila.Cells["MEDICOS"].Value.ToString());
//                        NegCuentasPacientes.CrearCuenta(cuentaPacientes);
//                        ugrdCuenta.DataSource = null;

//                    }
//                }

//                //if (frm.descripcion != "0")
//                //{


//                //    btnGuardar.Enabled = true;

//                //    string cantidad = Convert.ToString(frm.cantidad);
//                //    decimal valorAux = Convert.ToDecimal(frm.valorU);
//                //    double aux = (double)valorAux;
//                //    valorAux = (decimal)aux;
//                //    string referencia = frm.referencia;
//                //    string detalle = frm.descripcion;
//                //    cuentaPacientes = new CUENTAS_PACIENTES();
//                //    cuentaPacientes.ATE_CODIGO = Convert.ToInt32(txtAtencion.Text.Trim());
//                //    cuentaPacientes.CUE_FECHA = frm.fecha.Value;
//                //    cuentaPacientes.PRO_CODIGO_BARRAS = referencia.Trim();
//                //    cuentaPacientes.CUE_DETALLE = detalle.Trim();
//                //    cuentaPacientes.CUE_VALOR_UNITARIO = valorAux;
//                //    cuentaPacientes.CUE_CANTIDAD = Convert.ToByte(cantidad);

//                //    valorAux = (Convert.ToDecimal(frm.valorU) * (frm.porcentajeFacturar / 100));
//                //    cuentaPacientes.CUE_VALOR =
//                //        Decimal.Round(
//                //            Convert.ToInt16(cantidad) *
//                //            Decimal.Round(Convert.ToDecimal(Decimal.Round(valorAux, 2)), 2), 2);
//                //    cuentaPacientes.CUE_IVA = 0;
//                //    cuentaPacientes.PED_CODIGO = pedidosArea.DIV_CODIGO;
//                //    cuentaPacientes.RUB_CODIGO = rubro.RUB_CODIGO;
//                //    cuentaPacientes.CUE_ESTADO = 1;
//                //    cuentaPacientes.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
//                //    cuentaPacientes.CAT_CODIGO = 0;
//                //    cuentaPacientes.PRO_CODIGO = "0";
//                //    cuentaPacientes.CUE_NUM_CONTROL = "0";
//                //    cuentaPacientes.CUE_NUM_FAC = "0";
//                //    NegCuentasPacientes.CrearCuenta(cuentaPacientes);
//                //    ugrdCuenta.DataSource = null;

//                cargarProductos();
//            }


//            else
//            {
//                //frmAyudaProductos form = new frmAyudaProductos((PEDIDOS_AREAS)(cmb_Areas.SelectedItem));
//                //form.ShowDialog();
//                //listaProductosSolicitados = form.listaProductosSolicitados;
//                //guardarProductos(listaProductosSolicitados);
//                //cargarDetalleFactura(listaCuenta);
//            }
//        }
//        public void guardarProductos(List<PRODUCTO> listaProductosSolicitados)
//        {
//            RUBROS rubroM = new RUBROS();
//            RUBROS rubroS = new RUBROS();
//            if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
//            {
//                rubroM = (RUBROS)(cmb_Rubros.SelectedItem);
//                cmb_Rubros.SelectedIndex = 1;
//                rubroS = (RUBROS)(cmb_Rubros.SelectedItem);
//                cmb_Rubros.SelectedIndex = 0;
//            }

//            foreach (var solicitud in listaProductosSolicitados)
//            {

//                cuentaPacientes = new CUENTAS_PACIENTES();
//                cuentaPacientes.ATE_CODIGO = Convert.ToInt32(txtAtencion.Text.Trim());
//                cuentaPacientes.CUE_FECHA = DateTime.Now.Date;
//                cuentaPacientes.PRO_CODIGO = solicitud.PRO_CODIGO.ToString();
//                cuentaPacientes.CUE_DETALLE =solicitud.PRO_DESCRIPCION.ToString();
//                cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(solicitud.PRO_PRECIO);
//                cuentaPacientes.CUE_CANTIDAD = Convert.ToByte(solicitud.PRO_CANTIDAD);
//                cuentaPacientes.CUE_VALOR = (solicitud.PRO_PRECIO * ((solicitud.PRO_IVA / 100) + 1) * solicitud.PRO_CANTIDAD);
//                cuentaPacientes.CUE_ESTADO = 1;
//                cuentaPacientes.CUE_NUM_FAC = "0";
//                cuentaPacientes.RUB_CODIGO = Convert.ToInt16(cmb_Areas.SelectedValue.ToString());
//                string ivaTotal = Convert.ToString(Convert.ToDouble(cuentaPacientes.CUE_VALOR) * 0.12);
//                cuentaPacientes.CUE_IVA = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
//                //cuentaPacientes.CUE_IVA = Convert.ToDecimal(cuentaPacientes.CUE_VALOR) * Convert.ToDecimal(0.12);

//                cuentaPacientes.PED_CODIGO = NegPedidos.ultimoCodigoPedidos() + 1;
//                cuentaPacientes.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
//                cuentaPacientes.CAT_CODIGO = 0;
//                NegCuentasPacientes.CrearCuenta(cuentaPacientes);          
//                 ugrdCuenta.DataSource = null;
//            }
//                    cargarProductos();

//        }

//        /// <summary>
//        /// Leer Productos de la Bade de Datos de la tabla Productos
//        /// </summary>
//        public void CargarProducto()
//        {
//            //dtpFechaPedido.Value = DateTime.Now;
//            PRODUCTO producto = NegProducto.RecuperarProductoID(Convert.ToInt32(txt_Codigo.Text.Trim()));
//            if (producto != null)
//            {
//                txt_Codigo.Text = (producto.PRO_CODIGO).ToString();
//                txt_Nombre.Text = producto.PRO_DESCRIPCION;
//                //txtValorNeto.Text = Convert.ToString(producto.PRO_PRECIO);
//                txtValorNeto.Text = String.Format("{0:0.000}", producto.PRO_PRECIO);                
//                txtCodigoSic.Text = (producto.PRO_CODIGO_BARRAS).ToString();
//                txtCodigoSic.Enabled = false;
//            }
//            else
//            {
//                txtCodigoSic.Enabled = true;
//                txt_Codigo.Text = string.Empty;
//                txtCodigoSic.Text = "0";
//                txt_Nombre.Text = string.Empty;
//                txtValorNeto.Text = "0.00";
//                txtCantidad.Text = "1";
//                txtTotal.Text = "0.00";
//            }
//        }

//        public void CargarTarifarioIess()
//        {
//            //dtpFechaPedido.Value = DateTime.Now;
//            TARIFARIO_IESS tarifarioI = NegTarifario.RecuperarTarifarioIess(Convert.ToInt32(txt_Codigo.Text.Trim()));
//            if (tarifarioI != null)
//            {
//                txt_Codigo.Text = (tarifarioI.COD_IESS).ToString();
//                txt_Nombre.Text = tarifarioI.NOM_EXA;
//                txtValorNeto.Text = String.Format("{0:0.000}",tarifarioI.PR_LAB_NV3);
//                txtCodigoSic.Text = (tarifarioI.COD_IESS).ToString();
//                txtCodigoSic.Enabled = false;
//                txt_Nombre.Focus();
//            }
//            else
//            {
//                txtCodigoSic.Enabled = true;
//                txt_Codigo.Text = string.Empty;
//                txtCodigoSic.Text = "0";
//                txt_Nombre.Text = string.Empty;
//                txtValorNeto.Text = "0.00";
//                txtCantidad.Text = "1";
//                txtTotal.Text = "0.00";
//                txt_Codigo.Focus();
//            }
//        }


//        public void CargarTarifarioHonorarios()
//        {           
//            TARIFARIOS_DETALLE tarifarioH = NegTarifario.RecuperarTarifarioHono(txt_Codigo.Text.Trim());
//            if (tarifarioH != null)
//            {
//                txt_Codigo.Text = (tarifarioH.TAD_REFERENCIA).ToString();
//                if (((RUBROS)cmb_Rubros.SelectedItem).RUB_CODIGO == His.Parametros.CuentasPacientes.RubroAnestesia)
//                    txt_Nombre.Text = tarifarioH.TAD_DESCRIPCION + (((RUBROS)cmb_Rubros.SelectedItem)).RUB_NOMBRE;
//                else
//                txt_Nombre.Text = tarifarioH.TAD_DESCRIPCION;
//                txtCodigoSic.Text = (tarifarioH.TAD_REFERENCIA).ToString();
//                calcularValor(tarifarioH.TAD_CODIGO);
//                txtValorNeto.Text = (tarifarioH.TAD_UVR).ToString() ;
//                txtCodigoSic.Enabled = false;
//                txt_Nombre.Focus();
//            }
//            else
//            {
//                txtCodigoSic.Enabled = true;
//                txt_Codigo.Text = string.Empty;
//                txtCodigoSic.Text = "0";
//                txt_Nombre.Text = string.Empty;
//                txtValorNeto.Text = "0.00";
//                txtCantidad.Text = "1";
//                txtTotal.Text = "0.00";
//                txt_Codigo.Focus();
//            }
//        }


//        private void calcularValor(Int64 codigoTarifarioH)
//        {
//            HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
//            tarifarioH = contexto.TARIFARIOS_DETALLE.Include("ESPECIALIDADES_TARIFARIOS").FirstOrDefault(
//                                                        t => t.TAD_CODIGO == codigoTarifarioH);
//            Int64 codigo = tarifarioH.ESPECIALIDADES_TARIFARIOS.EST_CODIGO;
//                addHonorarioDetalle(codigo, Convert.ToDouble(tarifarioH.TAD_UVR));
//            tarifarioHI = contexto.TARIFARIOS_DETALLE.Include("ESPECIALIDADES_TARIFARIOS").FirstOrDefault(t => t.TAD_PADRE == codigoTarifarioH);
//        }

//        private void addHonorarioDetalle(Int64 codigoEspecialidad, double uvr)
//        {
//            try
//                    {
//                HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
//                CONVENIOS_TARIFARIOS convenios = contexto.CONVENIOS_TARIFARIOS.FirstOrDefault(
//                                                c => c.ASEGURADORAS_EMPRESAS.ASE_CODIGO == 13
//                                                    && c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO == codigoEspecialidad);
//                double costoU = 0;
//                //decimal Val1 = 0;
//                //string ValorCostoU="";
//                if (convenios != null)
                    
//                {
//                    //Val1 = Convert.ToDecimal(Math.Round((Convert.ToDouble(convenios.CON_VALOR_UVR) * uvr), 3));
//                    //ValorCostoU = String.Format("{0:0.000}",Val1);
//                    costoU = His.General.CalculosCuentas.CalcularRedondeo(String.Format("{0:0.000}",Math.Round((Convert.ToDouble(convenios.CON_VALOR_UVR) * uvr),3)));
//                    //costoU = His.General.CalculosCuentas.CalcularRedondeo("10.5556");
//                }
//                else
//                {
//                    MessageBox.Show("No existe un Convenio", MessageBoxIcon.Information.ToString());
//                    return;
//                }
//                unidadesUvr = Convert.ToDecimal(costoU) * Convert.ToInt32(txtCantidad.Text);
//                //txtValorNeto.Text = Convert.ToString(Math.Round(unidadesUvr, 3));
//                txtValorNeto.Text = String.Format("{0:0.000}", Math.Round(unidadesUvr, 3));
//                //txtPrecio.Text = txtValorNeto.Text;
//                txt_Nombre.Focus();
//            }
//            catch (Exception err)
//            {
//                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        public void GuardarProducto()
//        {
//            RUBROS rubroM = new RUBROS();
//            RUBROS rubroS = new RUBROS();
//            Boolean accion = false;
//            if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
//            {
//                rubroM = (RUBROS)(cmb_Rubros.SelectedItem);
//                cmb_Rubros.SelectedIndex = 1;
//                rubroS = (RUBROS)(cmb_Rubros.SelectedItem);
//                cmb_Rubros.SelectedIndex = 0;
//            }
//            if(cuentaModificada == null)
//            {
                
//                cuentaPacientes = new CUENTAS_PACIENTES();
//                cuentaPacientes.ATE_CODIGO = Convert.ToInt32(txtAtencion.Text);
//                cuentaPacientes.CUE_FECHA = dtpFechaPedido.Value;
//                cuentaPacientes.PRO_CODIGO = "0";
//                cuentaPacientes.CUE_DETALLE = (txt_Nombre.Text).ToString();
//                //cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(precioUnitario); ' Debe almacenar el valor que muestra en la caja de texto / Giovanny Tapia / 12/12/2012 / NOTA: Requerimiento Sra. Vicky
//                cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(txtValorNeto.Text);
//                cuentaPacientes.CUE_CANTIDAD = Convert.ToDecimal(txtCantidad.Text);
//                cuentaPacientes.CUE_VALOR = Convert.ToDecimal(txtTotal.Text);
//                cuentaPacientes.CUE_ESTADO = 1;
//                cuentaPacientes.CUE_NUM_CONTROL = txtCControl.Text;
//                cuentaPacientes.CUE_NUM_FAC = "0";
//                cuentaPacientes.PRO_CODIGO_BARRAS = txtCodigoSic.Text.Trim();
//                if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
//                {
//                    if ((txtCodigoSic.Text).ToString().Substring(0, 1).Equals("2"))
//                    {
//                        cuentaPacientes.RUB_CODIGO = rubroS.RUB_CODIGO;
//                        string ivaTotal = Convert.ToString(Convert.ToDouble(cuentaPacientes.CUE_VALOR) * 0.12);
//                        cuentaPacientes.CUE_IVA = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
//                        //cuentaPacientes.CUE_IVA = Convert.ToDecimal(cuentaPacientes.CUE_VALOR) * Convert.ToDecimal(0.12);
//                    }
//                    else
//                    {
//                        cuentaPacientes.RUB_CODIGO = rubroM.RUB_CODIGO;
//                        cuentaPacientes.CUE_IVA = 0;
//                    }
//                }
//                else
//                {
//                    cuentaPacientes.RUB_CODIGO = Convert.ToInt16(((RUBROS)(cmb_Rubros.SelectedItem)).RUB_CODIGO);
//                    if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoOtrosP)
//                    {
//                        accion = true;
//                        if (rdb_ConIva.Checked == true)
//                        {
//                            string ivaTotal = Convert.ToString(Convert.ToDouble(txtTotal.Text) * 0.12);
//                            cuentaPacientes.CUE_IVA = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
//                            //cuentaPacientes.CUE_IVA = Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(0.12);
//                        }
//                        else
//                            cuentaPacientes.CUE_IVA = 0;
//                    }
//                    else
//                        cuentaPacientes.CUE_IVA = 0;                    
//                }
//                cuentaPacientes.PED_CODIGO = Convert.ToInt32(((PEDIDOS_AREAS)(cmb_Areas.SelectedItem)).DIV_CODIGO);
//                cuentaPacientes.ID_USUARIO = Convert.ToInt16(His.Entidades.Clases.Sesion.codUsuario);
//                cuentaPacientes.CAT_CODIGO = 0;
//                if (txtCodMedico.Text.Trim() != string.Empty)
//                {
//                    cuentaPacientes.MED_CODIGO = Convert.ToInt32(txtCodMedico.Text.Trim());
//                    cuentaPacientes.Id_Tipo_Medico = Convert.ToInt32(this.cmbTipoMedico.SelectedValue.ToString());
//                }

//                else // Añado el else para que cuando no se ingrese el medico en la DB se inserte el valor 0(Ninguno) / Giovanny Tapia / 09/08/2012
//                {
//                    cuentaPacientes.MED_CODIGO = 0;
//                    cuentaPacientes.Id_Tipo_Medico = 0;
//                }

//                    NegCuentasPacientes.CrearCuenta(cuentaPacientes);
//            }
//            else
//            {
//                cuentaModificada.CUE_FECHA = dtpFechaPedido.Value;
//                cuentaModificada.PRO_CODIGO = "0";
//                cuentaModificada.CUE_DETALLE = (txt_Nombre.Text).ToString();
//                cuentaModificada.CUE_VALOR_UNITARIO = Convert.ToDecimal(precioUnitario);
//                cuentaModificada.CUE_CANTIDAD = Convert.ToDecimal(txtCantidad.Text);
//                cuentaModificada.CUE_VALOR = Convert.ToDecimal(txtTotal.Text);
//                cuentaModificada.CUE_NUM_CONTROL = txtCControl.Text;
//                cuentaModificada.PRO_CODIGO_BARRAS = txtCodigoSic.Text.Trim();
//                if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
//                {
//                    if ((txtCodigoSic.Text).ToString().Substring(0, 1).Equals("2"))
//                    {
//                        cuentaModificada.RUB_CODIGO = rubroS.RUB_CODIGO;
//                        string ivaTotal = Convert.ToString(Convert.ToDouble(cuentaModificada.CUE_VALOR) * 0.12);
//                        cuentaModificada.CUE_IVA = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));                                                
//                        //cuentaModificada.CUE_IVA = Convert.ToDecimal(cuentaModificada.CUE_VALOR) * Convert.ToDecimal(0.12);
//                    }
//                    else
//                    {
//                        cuentaModificada.RUB_CODIGO = rubroM.RUB_CODIGO;
//                        cuentaModificada.CUE_IVA = 0;
//                    }
//                }
//                else
//                {
//                    //cuentaPacientes.RUB_CODIGO = Convert.ToInt16(((RUBROS)(cmb_Rubros.SelectedItem)).RUB_CODIGO);
//                    if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoOtrosP)
//                    {
//                        accion = true;
//                        if (rdb_ConIva.Checked == true)
//                        {
//                            string ivaTotal = Convert.ToString(Convert.ToDouble(cuentaModificada.CUE_VALOR) * 0.12);
//                            cuentaModificada.CUE_IVA = Convert.ToDecimal(His.General.CalculosCuentas.CalcularIVA(ivaTotal));
//                            //cuentaModificada.CUE_IVA = Convert.ToDecimal(txtTotal.Text) * Convert.ToDecimal(0.12);
//                        }
//                        else
//                            cuentaModificada.CUE_IVA = 0;
//                    }
//                    else
//                        cuentaModificada.CUE_IVA = 0;   
//                }
//                if (txtCodMedico.Text.Trim() != string.Empty)
//                {
//                    cuentaModificada.MED_CODIGO = Convert.ToInt32(txtCodMedico.Text.Trim());
//                    cuentaModificada.Id_Tipo_Medico = Convert.ToInt32(this.cmbTipoMedico.SelectedValue.ToString());

//                }
//                else // Añado el else para que cuando no se ingrese el medico en la DB se inserte el valor 0(Ninguno) / Giovanny Tapia / 09/08/2012
//                {
//                    cuentaModificada.MED_CODIGO = 0;
//                    cuentaModificada.Id_Tipo_Medico = 0;
//                }

//                NegCuentasPacientes.ModificarCuenta(cuentaModificada);
//            }
//            if (accion== true) 
//                CalcularIvaSuministros(Convert.ToInt32(txtAtencion.Text));
//            limpiarCampos();
//            cargarProductos();
//        }

//        public void calcularValoresFactura()
//        {
//            try
//            {
//                if (txtValorNeto.Text != string.Empty && txtPorcentaje.Text != string.Empty)
//                {
//                    double porcentaje =  Math.Round((Convert.ToDouble(txtPorcentaje.Text)/100),2);
//                    //txtTotal.Text = ' Cambio el formato de los decimales a tres / Giovanny Tapia / 14/12/2012
//                    //Convert.ToString(((Convert.ToDouble(txtCantidad.Text)) *
//                    //(Convert.ToDouble(txtValorNeto.Text.ToString()))) * porcentaje);

//                    txtTotal.Text =
//                    String.Format("{0:0.000}", ((Convert.ToDouble(txtCantidad.Text)) *
//                    (Convert.ToDouble(txtValorNeto.Text.ToString()))) * porcentaje);

//                    if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO != His.Parametros.CuentasPacientes.CodigoFarmacia)
//                    {
//                        if (txtCantidad.Text == "1")
//                        {
//                            precioUnitario = Convert.ToDecimal(txtTotal.Text);
//                        }
//                        else
//                        {
//                            precioUnitario = Math.Round(Convert.ToDecimal(txtTotal.Text) / Convert.ToDecimal(txtCantidad.Text.Trim()), 3);
//                        }
//                    }else
//                    {
//                        precioUnitario = Math.Round(Convert.ToDecimal(txtTotal.Text) / Convert.ToDecimal(txtCantidad.Text.Trim()), 3);
//                    }
//                }
//            }
//            catch (Exception err)
//            {
//                MessageBox.Show(err.Message);
//            }
//        }


//        public void calcularValoresFacturaIva()
//        {
//            try
//            {
//                if (txtValorNeto.Text != string.Empty && txtPorcentaje.Text != string.Empty)
//                {
//                    double porcentaje = Math.Round((Convert.ToDouble(txtPorcentaje.Text) / 100), 2);
//                    string TotalC = Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) *
//                                         (Convert.ToDouble(txtValorNeto.Text.ToString()))) * porcentaje);
//                    txtTotal.Text = Convert.ToString(Convert.ToInt32(txtCantidad.Text) *(
//                                         Convert.ToDouble(txtValorNeto.Text.ToString()) + Convert.ToDouble(TotalC)));
//                    precioUnitario = Math.Round(Convert.ToDecimal(txtTotal.Text) / Convert.ToInt32(txtCantidad.Text.Trim()), 2);                    
//                }
//            }
//            catch (Exception err)
//            {
//                MessageBox.Show(err.Message);
//            }
//        }


//        private void button1_Click(object sender, EventArgs e)
//        {

//        }

//        private void frmCuentaPacientesIess_Load(object sender, EventArgs e)
//        {
//            NumeroFila = 0;

//            DataTable dt = new DataTable();

//            dt = NegCuentasPacientes.CargaTipoMedico();

//            cmbTipoMedico.DataSource = dt;
//            cmbTipoMedico.ValueMember = "Id_Tipo_Medico";
//            cmbTipoMedico.DisplayMember = "Descripcion_Tipo_Medico";


//        }

//        private void bindingNavigatorPositionItem_Click(object sender, EventArgs e)
//        {

//        }

//        private void txt_Codigo_TextChanged(object sender, EventArgs e)
//        {
//            if (txt_Codigo.Text.Trim() != string.Empty)
//            {
               
//            }
//        }

//        private void txt_Codigo_KeyDown(object sender, KeyEventArgs e)
//        {
//            DataGridView listas = new DataGridView();
//            try
//            {
//                if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
//                {
//                                            if (txt_Codigo.Text.Trim() != string.Empty)
//                    {
//                        int codigoArea = Convert.ToInt32(((PEDIDOS_AREAS) cmb_Areas.SelectedItem).DIV_CODIGO);
//                        if (His.Parametros.CuentasPacientes.CodigoLaboratorio == codigoArea || His.Parametros.CuentasPacientes.CodigoLaboratorioP == codigoArea)
//                        {
//                            CargarTarifarioIess();
//                        }else
//                        {
//                            if (codigoArea == His.Parametros.CuentasPacientes.CodigoHonorarios || codigoArea == His.Parametros.CuentasPacientes.CodigoImagen || codigoArea == His.Parametros.CuentasPacientes.CodigoServicosI)
//                            {
//                                CargarTarifarioHonorarios();
//                            }
//                            else
//                                CargarProducto();
//                        }
//                        txt_Nombre.Focus();
//                    }
//                }else
//                {
//                    if ((e.KeyCode == Keys.F1))
//                    {
//                        int codigoArea = Convert.ToInt32(((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO);
//                        //if (codigoArea == His.Parametros.CuentasPacientes.CodigoHonorarios || codigoArea == His.Parametros.CuentasPacientes.CodigoImagen || codigoArea == His.Parametros.CuentasPacientes.CodigoServicosI)
//                        //{
//                            RUBROS rubro = (RUBROS) (cmb_Rubros.SelectedItem);
//                            PEDIDOS_AREAS pedidosAreas = (PEDIDOS_AREAS) (cmb_Areas.SelectedItem);
//                            //if (pedidosAreas.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoHonorarios ||
//                            //    pedidosAreas.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoImagen ||
//                            //    pedidosAreas.DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoServicosI)
//                            //{
//                            frmAyudaTarifarios frm = new frmAyudaTarifarios(txtCodMedico.Text.Trim(), dateTimePickerFechaAlta);
//                                frm.ShowDialog();
//                                listas = frm.lista;
//                                if (listas != null)
//                                {
//                                    btnGuardar.Enabled = true;
//                                    for (Int16 i = 0; i <= listas.RowCount - 2; i++)
//                                    {
//                                        DataGridViewRow fila = listas.Rows[i];

//                                        string porcentaje = fila.Cells["PORCENTAJE"].Value.ToString();
//                                        string cantidad = fila.Cells["VCANTIDAD"].Value.ToString();
//                                        decimal valorAux = Convert.ToDecimal(fila.Cells["VALORUNITARIO"].Value.ToString());
//                                        double aux = (double)valorAux;
//                                        valorAux = (decimal)aux;
//                                        string referencia = fila.Cells["CODIGO"].Value.ToString(); ;
//                                        string detalle = fila.Cells["NOMBRE"].Value.ToString(); ;
//                                        cuentaPacientes = new CUENTAS_PACIENTES();
//                                        cuentaPacientes.ATE_CODIGO = Convert.ToInt32(txtAtencion.Text.Trim());
//                                        cuentaPacientes.CUE_FECHA = Convert.ToDateTime(fila.Cells["FECHAS"].Value.ToString());
//                                        cuentaPacientes.PRO_CODIGO_BARRAS = referencia.Trim();
//                                        cuentaPacientes.CUE_DETALLE = detalle.Trim();
//                                        if (porcentaje.Trim() == "1.5")
//                                            cuentaPacientes.CUE_VALOR_UNITARIO = Convert.ToDecimal(Convert.ToDecimal(fila.Cells["TOTAL"].Value.ToString()));
//                                        else
//                                            cuentaPacientes.CUE_VALOR_UNITARIO = valorAux;
//                                        cuentaPacientes.CUE_CANTIDAD = Convert.ToByte(cantidad);
//                                        if (porcentaje.Trim() == "1.5")
//                                            cuentaPacientes.CUE_VALOR = Convert.ToDecimal(Convert.ToDecimal(fila.Cells["TOTAL"].Value.ToString()));
//                                        else
//                                        {
//                                            valorAux = (Convert.ToDecimal(Convert.ToDecimal(fila.Cells["VALORUNITARIO"].Value.ToString())) * (Convert.ToInt16(fila.Cells["PORCENTAJE"].Value.ToString()) / 100));
//                                            cuentaPacientes.CUE_VALOR =
//                                                Decimal.Round(
//                                                    Convert.ToInt16(cantidad) *
//                                                    Decimal.Round(Convert.ToDecimal(Decimal.Round(valorAux, 2)), 2), 2);
//                                        }
//                                        cuentaPacientes.CUE_IVA = 0;
//                                        cuentaPacientes.PED_CODIGO = pedidosArea.DIV_CODIGO;
//                                        cuentaPacientes.RUB_CODIGO = rubro.RUB_CODIGO;
//                                        cuentaPacientes.CUE_ESTADO = 1;
//                                        cuentaPacientes.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
//                                        cuentaPacientes.CAT_CODIGO = 0;
//                                        cuentaPacientes.PRO_CODIGO = "0";
//                                        cuentaPacientes.CUE_NUM_CONTROL = "0";
//                                        cuentaPacientes.CUE_NUM_FAC = "0";
//                                        cuentaPacientes.MED_CODIGO = Convert.ToInt16(fila.Cells["MEDICOS"].Value.ToString());
//                                        NegCuentasPacientes.CrearCuenta(cuentaPacientes);
//                                        ugrdCuenta.DataSource = null;

//                                    }
//                                }
                
//                            //}
//                        //}
//                        //else
//                        //{
//                        //    frmAyudaProductos form = new frmAyudaProductos((PEDIDOS_AREAS)(cmb_Areas.SelectedItem));
//                        //    form.ShowDialog();
//                        //    listaProductosSolicitados = form.listaProductosSolicitados;
//                        //    guardarProductos(listaProductosSolicitados);
//                        //    cargarDetalleFactura(listaCuenta);
//                        //}
//                                cargarProductos();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.InnerException.Message);
//            }
//        }

//        private void txt_Codigo_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
//            {
//                e.Handled = true;
//                SendKeys.SendWait("{TAB}");
//                txt_Nombre.Focus();
//            }
//        }

//        private void txt_Nombre_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
//            {
//                e.Handled = true;
//                SendKeys.SendWait("{TAB}");
//                txtValorNeto.Focus();
//            }
//        }

//        private void txt_Precio_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
//            {
//                e.Handled = true;
//                SendKeys.SendWait("{TAB}");
//                txtPorcentaje.Focus();
//            }
//        }

//        private void msk_Cantidad_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
//            {
//                e.Handled = true;
//                SendKeys.SendWait("{TAB}");
//                txtTotal.Focus();
//            }
//        }

//        private void maskedTextBox3_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
//            {
//                e.Handled = true;
//                SendKeys.SendWait("{TAB}");
//                dtpFechaPedido.Focus();
//            }
//        }

//        private void msk_Cantidad_KeyDown(object sender, KeyEventArgs e)
//        {
//            try
//            {
//                if ((e.KeyCode == Keys.Enter))
//                {
//                    if (txtCantidad.Text.Trim() != string.Empty)
//                    {
//                        //txt_Total.Text = Convert.ToString(Convert.ToDecimal(txtCantidad.Text) * Convert.ToInt32(txtValorNeto.Text));
//                        calcularValoresFactura();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.InnerException.Message);
//            }
//        }

//        private void txtValorNeto_TextChanged(object sender, EventArgs e)
//            {
//            if (txtValorNeto.Text.ToString() != string.Empty || txtValorNeto.Text.ToString() != "0.00")
//            {
//                if (Microsoft.VisualBasic.Information.IsNumeric(txtValorNeto.Text))
//                {
//                    calcularValoresFactura();
//                }
//                else
//                {
//                    txtValorNeto.Text = string.Empty;
//                }
//            }


//            if (txtValorNeto.Text.ToString() == "0.00" || txtValorNeto.Text.ToString() == string.Empty)
//            {
//                //txt_Codigo.Text = string.Empty;
//                //txt_Nombre.Text = string.Empty;
//                //txtValorNeto.Text = string.Empty;
//                //txtCantidad.Text = string.Empty;
//            }
//        }

//        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
//        {
//            try
//            {
//                if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
//                {
//                    if (txtCantidad.Text.Trim() != string.Empty)
//                    {
//                        if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoOtrosP)
//                        {
//                            if(rdb_ConIva.Checked == true)
//                                calcularValoresFacturaIva();
//                            else
//                                calcularValoresFactura();
//                        }
//                        else
//                        {
//                            calcularValoresFactura();
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.InnerException.Message);
//            }
//        }

//        private void txtValorNeto_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
//            {
//                e.Handled = true;
//                SendKeys.SendWait("{TAB}");
//            }
//        }

//        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
//            {
//                e.Handled = true;
//                SendKeys.SendWait("{TAB}");
//            }
//        }

//        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
//            {
//                e.Handled = true;
//                SendKeys.SendWait("{TAB}");
//            }
//        }

//        private void btnAnadir_Click(object sender, EventArgs e)
//                        {
//            try
//            {
//                if (txt_Codigo.Text != string.Empty || txtCodigoSic.Text != string.Empty)
//                {
//                    if (txt_Nombre.Text != string.Empty)
//                    {
//                        if (txtValorNeto.Text.ToString() != "0.00" || txtValorNeto.Text.ToString() == string.Empty )
//                        {
//                            if (txtCantidad.Text != string.Empty || txtCantidad.Text != "0")
//                            {
//                                if (txtTotal.Text.ToString() != "0.00" || txtTotal.Text.ToString() == string.Empty)
//                                {
//                                    GuardarProducto();
//                                    //if (tarifarioHI != null) // Comento ya que el 1.5 segun requerimiento activa salud ya no va/ Giovanny Tapia / 14/12/2012 
//                                    //{
//                                    //    txtCodigoSic.Text = tarifarioH.TAD_REFERENCIA;
//                                    //    txt_Nombre.Text = tarifarioHI.TAD_DESCRIPCION;
//                                    //    txtValorNeto.Text =
//                                    //        Convert.ToString(((Convert.ToInt32(txtCantidad.Text))*
//                                    //                          (Convert.ToDouble(unidadesUvr)))*0.015);
//                                    //    txtCantidad.Text = "1";
//                                    //    txtTotal.Text =
//                                    //        Convert.ToString(((Convert.ToInt32(txtCantidad.Text))*
//                                    //                          (Convert.ToDouble(unidadesUvr)))*0.015);
//                                    //    txtCControl.Text = "0";
//                                    //    GuardarProducto();
//                                    //    tarifarioHI = null;
//                                    //    tarifarioH = null;
//                                    //}
//                                }
//                            }
//                        }
//                    }
//                }
//                else
//                {
//                    if (txt_Codigo.Text == string.Empty && txtCodigoSic.Text == string.Empty)
//                    {
//                        GuardarProducto();
//                        //if (tarifarioHI != null) ' Ingreso del rubro del 1.5 / Giovanny Tapia / 14/12/2012
//                        //{
//                        //    txtCodigoSic.Text = tarifarioH.TAD_REFERENCIA;
//                        //    txt_Nombre.Text = tarifarioHI.TAD_DESCRIPCION;
//                        //    txtValorNeto.Text =
//                        //        Convert.ToString(((Convert.ToInt32(txtCantidad.Text))*(Convert.ToDouble(unidadesUvr)))*
//                        //                         0.015);
//                        //    txtCantidad.Text = "1";
//                        //    txtTotal.Text =
//                        //        Convert.ToString(((Convert.ToInt32(txtCantidad.Text))*(Convert.ToDouble(unidadesUvr)))*
//                        //                         0.015);
//                        //    txtCControl.Text = "0";
//                        //    GuardarProducto();
//                        //    tarifarioHI = null;
//                        //    tarifarioH = null;
//                        //}
//                    }
//                }
//                txtCodigoSic.Text = "0";
//                txt_Codigo.Focus(); 
//                if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
//                    CalcularIvaSuministros(Convert.ToInt32(txtAtencion.Text.Trim()));
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.InnerException.Message);
//            }
//        }

//        private void btnAnadir_KeyDown(object sender, KeyEventArgs e)
//        {
//            try
//            {
//                if ((e.KeyCode == Keys.Enter))
//                {
//                    if (txt_Codigo.Text != string.Empty || txtCodigoSic.Text != string.Empty)
//                    {
//                        if (txt_Nombre.Text != string.Empty)
//                        {
//                            if (txtValorNeto.Text.ToString() != "0.00" || txtValorNeto.Text.ToString() == string.Empty)
//                            {
//                                if (txtCantidad.Text != string.Empty || txtCantidad.Text != "0")
//                                {
//                                    if (txtTotal.Text.ToString() != "0.00" || txtTotal.Text.ToString() == string.Empty)
//                                    {
//                                        GuardarProducto();
//                                        //if (tarifarioHI != null)// para quitar el 1.5% euipo digital que ya no viene con el MSP David Mantila 13/11/2012
//                                        //{
//                                        //    txtCodigoSic.Text = tarifarioH.TAD_REFERENCIA;
//                                        //    txt_Nombre.Text = tarifarioHI.TAD_DESCRIPCION;
//                                        //    txtValorNeto.Text = Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) * (Convert.ToDouble(unidadesUvr))) * 0.015);
//                                        //    txtCantidad.Text = "1";
//                                        //    txtTotal.Text = Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) * (Convert.ToDouble(unidadesUvr))) * 0.015);
//                                        //    txtCControl.Text = "0";
//                                        //    GuardarProducto();
//                                        //    tarifarioHI = null;
//                                        //    tarifarioH = null;
//                                        //}
//                                    }
//                                }
//                            }
//                        }
//                    }
//                    else
//                    {
//                        if (txt_Codigo.Text == string.Empty && txtCodigoSic.Text == string.Empty)
//                        {
//                            GuardarProducto();
//                            //if (tarifarioHI != null) // Comento ya que el 1.5 segun requerimiento activa salud ya no va/ Giovanny Tapia / 14/12/2012
//                            //{
//                            //    txtCodigoSic.Text = tarifarioH.TAD_REFERENCIA;
//                            //    txt_Nombre.Text = tarifarioHI.TAD_DESCRIPCION;
//                            //    txtValorNeto.Text = Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) * (Convert.ToDouble(unidadesUvr))) * 0.015);
//                            //    txtCantidad.Text = "1";
//                            //    txtTotal.Text = Convert.ToString(((Convert.ToInt32(txtCantidad.Text)) * (Convert.ToDouble(unidadesUvr))) * 0.015);
//                            //    txtCControl.Text = "0";
//                            //    GuardarProducto();
//                            //    tarifarioHI = null;
//                            //    tarifarioH = null;
//                            //}
//                        }
//                    }
//                    txtCodigoSic.Text = "0";
//                    txt_Codigo.Focus();
//                    if(((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
//                        CalcularIvaSuministros(Convert.ToInt32(txtAtencion.Text.Trim()));
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.InnerException.Message);
//            }
//        }

//        private void ugrdCuenta_KeyDown(object sender, KeyEventArgs e)
//        {
//            try
//            {
//                if ((e.KeyCode == Keys.Delete))
//                {
//                    if(ugrdCuenta.ActiveRow != null)
//                    {
//                        int codigo =  Convert.ToInt32(ugrdCuenta.ActiveRow.Cells[0].Value);
//                        NegCuentasPacientes.EliminarCuenta(codigo);
//                        ugrdCuenta.ActiveRow.Delete();
//                    }

//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.InnerException.Message);
//            }
//        }

//        private void txtCodigoSic_KeyDown(object sender, KeyEventArgs e)
//        {
//            try
//            {
//                if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
//                {
//                    //dtpFechaPedido.Value = DateTime.Now;
//                    if (txt_Codigo.Text.Trim() == string.Empty)
//                    {
//                        txt_Codigo.Text = string.Empty;
//                        //txtCodigoSic.Text = string.Empty;
//                        txt_Nombre.Text = string.Empty;
//                        txtValorNeto.Text = "0.00";
//                        txtCantidad.Text = "1";
//                        txtTotal.Text = "0.00";
//                        txtCControl.Text = "0.00";
//                    }
//                    else
//                    {
//                        if (txt_Codigo.Text.Trim() != txtCodigoSic.Text.Trim())
//                        {
//                            txt_Codigo.Text = string.Empty;
//                            txt_Nombre.Text = string.Empty;
//                            txtValorNeto.Text = "0.00";
//                            txtCantidad.Text = "1";
//                            txtTotal.Text = "0.00";
//                            txtCControl.Text = string.Empty;
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.InnerException.Message);
//            }
//        }

//        private void txtCodigoSic_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
//            {
//                e.Handled = true;
//                SendKeys.SendWait("{TAB}");
//            }
//        }

//        private void btnSalir_Click(object sender, EventArgs e)
//        {
//            this.Close();
//        }



//        private void ugrdCuenta_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
//        {
//            try
//            {
//                if (ugrdCuenta.ActiveRow != null)
//                {

//                    //int codigo = Convert.ToInt32(ugrdCuenta.ActiveRow.Cells[0].Value);
//                    txt_Codigo.Text = (ugrdCuenta.ActiveRow.Cells[15].Value).ToString();
//                    txtCodigoSic.Text = (ugrdCuenta.ActiveRow.Cells[15].Value).ToString();
//                    txt_Nombre.Text = (ugrdCuenta.ActiveRow.Cells[3].Value).ToString();
//                    txtValorNeto.Text = (ugrdCuenta.ActiveRow.Cells[14].Value).ToString();
//                    txtCantidad.Text = (ugrdCuenta.ActiveRow.Cells[8].Value).ToString();
//                    txtTotal.Text = (ugrdCuenta.ActiveRow.Cells[4].Value).ToString();

//                    if ((ugrdCuenta.ActiveRow.Cells[16].Value) != null) // Para evitar que de error cuando el numero de control no esxiste // Giovanny Tapia // 13/12/2012
//                    {
//                        txtCControl.Text = (ugrdCuenta.ActiveRow.Cells[16].Value).ToString();
//                    }
//                    else
//                    {
//                        txtCControl.Text = "0";
//                    }
                    
//                    dtpFechaPedido.Value = Convert.ToDateTime(ugrdCuenta.ActiveRow.Cells[2].Value);
//                    txtPorcentaje.Text = "100";
//                    NumeroFila = ugrdCuenta.ActiveRow.Index;
//                    gridRow = ugrdCuenta.ActiveRow;

//                    if ((ugrdCuenta.ActiveRow.Cells[18].Value) != null)
//                    {
//                        txtCodMedico.Text = (ugrdCuenta.ActiveRow.Cells[18].Value).ToString();
//                        CargarMedico(Convert.ToInt32(ugrdCuenta.ActiveRow.Cells[18].Value));
//                        this.cmbTipoMedico.SelectedIndex = Convert.ToInt32(ugrdCuenta.ActiveRow.Cells["Id_Tipo_Medico"].Value);
//                    }
//                    else
//                    {
//                        txtCodMedico.Text = string.Empty;
//                        txtNombreMedico.Text = string.Empty;
//                        this.cmbTipoMedico.SelectedIndex = 0;
//                    }
//                    cuentaModificada = (CUENTAS_PACIENTES)(ugrdCuenta.ActiveRow.ListObject);
//                    if (cuentaModificada.RUB_CODIGO == His.Parametros.CuentasPacientes.RubroOtrosP || cuentaModificada.RUB_CODIGO == His.Parametros.CuentasPacientes.RubroSuministros) { }
//                    {
//                        cmb_Areas.Enabled = false;
//                        rdb_SinIva.Enabled = true;
//                        rdb_ConIva.Enabled = true;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.InnerException.Message);
//            }
//        }

//        private void btnExaminar_Click(object sender, EventArgs e)
//        {
//            openFileDialogo.InitialDirectory = "c:\\";
//            openFileDialogo.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
//            openFileDialogo.FilterIndex = 2;
//            openFileDialogo.RestoreDirectory = true;

//            if (openFileDialogo.ShowDialog() == DialogResult.OK)
//            {
//                txtDirArchivo.Text = openFileDialogo.FileName;

//            }
//        }

//        private void btn_Cargar_Click(object sender, EventArgs e)
//        {
//            limpiarCampos();
//            gpbDatos.Enabled = false;
//            ugrdCuenta.DataSource = null;
//            try
//            {
//                if(rdbTexto.Checked== true || rdbCSV.Checked == true)
//                {
//                   var listaFilas = ImportarArchivoDelimitado(txtDirArchivo.Text.Trim(), new char[] { ';' });
//                   if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO == His.Parametros.CuentasPacientes.CodigoFarmacia)
//                   {
//                       ugrdCuenta.DataSource = listaFilas.Select(c => new CUENTAS_PACIENTES()
//                       {
//                           ATE_CODIGO = Convert.ToInt32(txtAtencion.Text),
//                           CUE_FECHA = Convert.ToDateTime(c[3]),
//                           PRO_CODIGO_BARRAS = (c[0]).ToString(),
//                           CUE_DETALLE = (c[1]).ToString(),
//                           CUE_VALOR_UNITARIO = Convert.ToDecimal(c[4]),
//                           CUE_CANTIDAD = Convert.ToByte(c[5]),
//                           CUE_VALOR = Convert.ToDecimal(c[6]),
//                           CUE_IVA = Convert.ToDecimal(0.00),
//                           CUE_ESTADO = 1,
//                           PRO_CODIGO = "0",
//                           CUE_NUM_FAC = "0",
//                           MED_CODIGO = 0,
//                           CUE_NUM_CONTROL = (c[2]).ToString(),
//                           RUB_CODIGO = ((RUBROS)(cmb_Rubros.SelectedItem)).
//                               RUB_CODIGO,
//                           PED_CODIGO = ((PEDIDOS_AREAS)(cmb_Areas.SelectedItem)).DIV_CODIGO,
//                           ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario,
//                           CAT_CODIGO = 0
//                       }).ToList();
//                   }
//                   else
//                   {
//                       ugrdCuenta.DataSource = listaFilas.Select(c => new CUENTAS_PACIENTES()
//                       {
//                           ATE_CODIGO = Convert.ToInt32(txtAtencion.Text),
//                           CUE_FECHA = Convert.ToDateTime(c[0]),
//                           PRO_CODIGO_BARRAS = (c[1]).ToString(),
//                           CUE_DETALLE = (c[2]).ToString(),
//                           CUE_VALOR_UNITARIO = Convert.ToDecimal(c[3]),
//                           CUE_CANTIDAD = Convert.ToByte(c[4]),
//                           CUE_VALOR = Convert.ToDecimal(c[5]),
//                           CUE_IVA = Convert.ToDecimal(0.00),
//                           CUE_ESTADO = 1,
//                           PRO_CODIGO = "0",
//                           CUE_NUM_FAC = "0",
//                           CUE_NUM_CONTROL = "0",
//                           MED_CODIGO = 0,
//                           RUB_CODIGO =
//                               ((RUBROS)(cmb_Rubros.SelectedItem)).
//                               RUB_CODIGO,
//                           PED_CODIGO =
//                               ((PEDIDOS_AREAS)(cmb_Areas.SelectedItem))
//                               .DIV_CODIGO,
//                           ID_USUARIO =
//                               His.Entidades.Clases.Sesion.codUsuario,
//                           CAT_CODIGO = 0
//                       }).ToList();
//                   }
//                   UltraGridBand bandUno = ugrdCuenta.DisplayLayout.Bands[0];
//                   SummarySettings sumTarifa = bandUno.Summaries.Add("CUE_VALOR", SummaryType.Sum,
//                                                                     bandUno.Columns["CUE_VALOR"]);
//                   MessageBox.Show("Archivo cargado  exitosamente");
//                   btnActualizar.Enabled = false; 
//                   btnGuardar.Enabled = true;
//                }else
//                {
//                    if(rdbExcel.Checked== true)
//                    {
//                        btnGuardar.Enabled = true;
//                        if (((PEDIDOS_AREAS)cmb_Areas.SelectedItem).DIV_CODIGO != His.Parametros.CuentasPacientes.CodigoFarmacia)
//                        {
//                            string rutaExcel = txtDirArchivo.Text.Trim();
//                            //Creamos la cadena de conexión con el fichero excel
//                            OleDbConnectionStringBuilder cb = new OleDbConnectionStringBuilder();
//                            cb.DataSource = rutaExcel;
//                            if (Path.GetExtension(rutaExcel).ToUpper() == ".XLS")
//                            {
//                                cb.Provider = "Microsoft.Jet.OLEDB.4.0";
//                                cb.Add("Extended Properties", "Excel 8.0;HDR=YES;IMEX=0;");
//                            }
//                            else if (Path.GetExtension(rutaExcel).ToUpper() == ".XLSX")
//                            {
//                                cb.Provider = "Microsoft.ACE.OLEDB.12.0";
//                                cb.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES;IMEX=0;");
//                            }
//                            DataTable dt = new DataTable("Datos");
//                            using (OleDbConnection conn = new OleDbConnection(cb.ConnectionString))
//                            {
//                                //Abrimos la conexión
//                                conn.Open();
//                                using (OleDbCommand cmd = conn.CreateCommand())
//                                {
//                                    cmd.CommandType = CommandType.Text;
//                                    cmd.CommandText = "SELECT * FROM [Hoja1$]";
//                                    //Guardamos los datos en el DataTable
//                                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
//                                    da.Fill(dt);
//                                }
//                                //Cerramos la conexión

//                                var results = from myRow in dt.AsEnumerable()
//                                              select myRow;
//                                conn.Close();

//                                ugrdCuenta.DataSource = results.Select(c => new CUENTAS_PACIENTES()
//                                {
//                                    ATE_CODIGO = Convert.ToInt32(txtAtencion.Text),
//                                    CUE_FECHA = Convert.ToDateTime(c[0]),
//                                    PRO_CODIGO_BARRAS = (c[1]).ToString(),
//                                    CUE_DETALLE = (c[2]).ToString(),
//                                    CUE_VALOR_UNITARIO = Convert.ToDecimal(c[3]),
//                                    CUE_CANTIDAD = Convert.ToByte(c[4]),
//                                    CUE_VALOR = Convert.ToDecimal(c[5]),
//                                    CUE_IVA = Convert.ToDecimal(0.00),
//                                    CUE_ESTADO = 1,
//                                    PRO_CODIGO = "0",
//                                    CUE_NUM_FAC = "0",
//                                    CUE_NUM_CONTROL = "0",
//                                    MED_CODIGO = 0,
//                                    RUB_CODIGO =
//                                        ((RUBROS)(cmb_Rubros.SelectedItem)).
//                                        RUB_CODIGO,
//                                    PED_CODIGO =
//                                        ((PEDIDOS_AREAS)(cmb_Areas.SelectedItem))
//                                        .DIV_CODIGO,
//                                    ID_USUARIO =
//                                        His.Entidades.Clases.Sesion.codUsuario,
//                                    CAT_CODIGO = 0
//                                }).ToList();
//                                UltraGridBand bandUno = ugrdCuenta.DisplayLayout.Bands[0];
//                                SummarySettings sumTarifa = bandUno.Summaries.Add("CUE_VALOR", SummaryType.Sum,
//                                                                                  bandUno.Columns["CUE_VALOR"]);
//                                MessageBox.Show("Archivo cargado  exitosamente");
//                                btnActualizar.Enabled = false;
//                                btnGuardar.Enabled = true;
//                            }
//                        }
//                    }
//                }
//                txtDirArchivo.Text = string.Empty;
//                //ultraTabControlCuenta.Enabled = false;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("No se pudo  cargar  archivo ");
//                btnGuardar.Enabled = false;
//            }
//        }

//        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
//            {
//                e.Handled = true;
//                SendKeys.SendWait("{TAB}");
//            }
//        }

//        private void txtCControl_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
//            {
//                e.Handled = true;
//                SendKeys.SendWait("{TAB}");
//                dtpFechaPedido.Focus();
//            }
//        }

//        private void chbEliminar_CheckedChanged(object sender, EventArgs e)
//        {
//            checkBox1.Checked = false;
//        }

//        private void btnEliminarProductos_Click(object sender, EventArgs e)
//        {
//            int contador = 0;
//            if (chbEliminar.Checked == true)
//            {
//                if (ugrdCuenta.Rows.Count > 0)
//                {
//                    for (int i = 0; i < ugrdCuenta.Rows.Count; i++)
//                    {
//                        NegCuentasPacientes.EliminarCuenta(Convert.ToInt32(ugrdCuenta.Rows[i].Cells[0].Value));
//                    }
//                    MessageBox.Show("Datos eliminados Correctamente");
//                    cargarProductos();
//                    chbEliminar.Checked = false;
//                }
//            }
//            if (checkBox1.Checked == true)
//            {
//                foreach (var item in ugrdCuenta.Rows)
//                {
//                    if ((bool)item.Cells["CHECKTRANS"].Value == true)
//                    {
//                        NegCuentasPacientes.EliminarCuenta( Convert.ToInt32(item.Cells["CUE_CODIGO"].Value));
//                        contador++;
//                    }
//                }
//                if (contador > 0)
//                {
//                    MessageBox.Show("Registros Eliminados ", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    cargarProductos();
//                    checkBox1.Checked = false;
                
//                }
//                else
//                {
//                    MessageBox.Show("Seleccione una  cuenta para eliminar", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                }

//            }

//        }

//        private void btnCancelar_Click(object sender, EventArgs e)
//        {
//            ugrdCuenta.DataSource = null;
//            HabilitarBotones(true, false, true, true, true);

//        }

//        private void checkBox1_CheckedChanged(object sender, EventArgs e)
//        {
//            chbEliminar.Checked = false;
//        }

//        private void btnMedico_Click(object sender, EventArgs e)
//        {
//            //His.Admision.
//            List<MEDICOS> listaMedicos = NegMedicos.listaMedicosIncTipoMedico();

//            His.Admision.frm_Ayudas ayuda = new frm_Ayudas(listaMedicos, "MEDICOS", "CODIGO","");
//            ayuda.campoPadre = txtCodMedico;
//            ayuda.ShowDialog();

//            if (ayuda.campoPadre.Text != string.Empty)
//                CargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
//        }

//        private void CargarMedico(int codMedico)
//        {
//            medico = NegMedicos.MedicoID(codMedico);
//            if (medico != null)
//                txtNombreMedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + "  " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
//            else
//                txtNombreMedico.Text = string.Empty;
//        }

//        private void txtCodMedico_KeyDown(object sender, KeyEventArgs e)
//        {
//            try
//            {
//                if ((e.KeyCode == Keys.F1))
//                {
//                    List<MEDICOS> listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
//                    His.Admision.frm_Ayudas ayuda = new frm_Ayudas(listaMedicos, "MEDICOS", "CODIGO","");
//                    ayuda.campoPadre = txtCodMedico;
//                    ayuda.ShowDialog();
//                    if (ayuda.campoPadre.Text != string.Empty)
//                        CargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
//                }else
//                {
//                    if ((e.KeyCode == Keys.Enter))
//                    {
//                        if (txtCodMedico.Text.Trim() != string.Empty)
//                            CargarMedico(Convert.ToInt32(txtCodMedico.Text.Trim()));
//                        else
//                        {
//                            txtNombreMedico.Text = string.Empty;
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.InnerException.Message);
//            }
//        }

//        private void txtPorcentaje_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
//            {
//                e.Handled = true;
//                SendKeys.SendWait("{TAB}");
//                txtCantidad.Focus();
//            }
//        }

//        private void btnMedico_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
//            {
//                e.Handled = true;
//                SendKeys.SendWait("{TAB}");
//                txtCodMedico.Focus();
//            }
//        }

//        private void txtCodMedico_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
//            {
//                e.Handled = true;
//                SendKeys.SendWait("{TAB}");
//                btnAnadir.Focus();
//            }
//        }

//        private void dateTimePickerFechaAlta_ValueChanged(object sender, EventArgs e)
//        {
//            if (dateTimePickerFechaAlta.Value != Convert.ToDateTime("12/12/2011") || dateTimePickerFechaAlta.Value == DateTime.Now )
//            {
//                if (dateTimePickerFechaAlta.Value.Date >= dateTimePickerFechaIngreso.Value.Date)                
//                    dtpFechaPedido.Value = dateTimePickerFechaAlta.Value;             
//                else
//                    dateTimePickerFechaAlta.Value = DateTime.Now;
//            }
//            else
//                dateTimePickerFechaAlta.Value = DateTime.Now;       
//        }

//        private void btnCerrar_Click(object sender, EventArgs e)
//        {
//            try 
//            { 
//                atencion.ESC_CODIGO = 3;
//                atencion.ATE_FECHA_ALTA = dateTimePickerFechaAlta.Value;
//                NegAtenciones.EditarAtencionAdmision(atencion,1);
//                MessageBox.Show("Atención Prefacturada  Exitosamente");
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error............! ");                
//            }
//        }

//        private void btnActualizar_Click(object sender, EventArgs e)
//        {
//            try 
//            {
//                dateTimePickerFechaAlta.Enabled = true;
//                this.accionCuenta = true;
//                HabilitarBotones(false, true, true, true, true);
//                //atencion.ATE_FECHA_ALTA = dateTimePickerFechaAlta.Value;
//                //NegAtenciones.EditarAtencionAdmision(atencion);
//                //MessageBox.Show("Atención Actualizada  Exitosamente");
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error............! ");                
//            }
//        }

//        private void rdb_ConIva_CheckedChanged(object sender, EventArgs e)
//        {
//            if (rdb_ConIva.Checked == true)            
//                txtPorcentaje.Text = "10";            
//            else
//                txtPorcentaje.Text = "100";
//        }

//        private void rdb_SinIva_CheckedChanged(object sender, EventArgs e)
//        {
//            if (rdb_SinIva.Checked == true)            
//                txtPorcentaje.Text = "100";
            
//        }

//        private void cuentaToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            Form frm = new frmDetalleCuenta(Convert.ToInt32(txtAtencion.Text.Trim()));
//            frm.Show();
//        }

//        private void cuentasToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            Form frm = new frmConsolidarCuentas(Convert.ToInt32(txtAtencion.Text.Trim()), txtPacienteHCL.Text.Trim(),
//                txtPacienteNombre.Text+" "+txtPacienteNombre2.Text+" "+txtPacienteApellidoPaterno.Text+" "+txtPacienteApellidoMaterno.Text,
//                txtPacienteCedula.Text, txtHabitacion.Text, Convert.ToString(dateTimePickerFechaIngreso.Value), Convert.ToString(dateTimePickerFechaAlta.Value));
//            frm.Show();
//        }

//        private void btnCancelarRubro_Click(object sender, EventArgs e)
//        {
//            limpiarCampos();
//        }

//        private void btnCancelarRubro_KeyDown(object sender, KeyEventArgs e)
//        {
//            limpiarCampos();
//        }

//        private void gpbDatos_Enter(object sender, EventArgs e)
//        {

//        }

//        private void button1_Click_1(object sender, EventArgs e)
//        {
//            frmActualizaFechas frmActualizar = new frmActualizaFechas(atencion.ATE_CODIGO, Convert.ToDateTime(atencion.ATE_FECHA_INGRESO), Convert.ToDateTime(atencion.ATE_FECHA_ALTA));
//            frmActualizar.ShowDialog();

//            cargarProductos();
//        }

//        private void txtCodMedico_TextChanged(object sender, EventArgs e)
//        {

//        }

//        private void txtNombreMedico_TextChanged(object sender, EventArgs e)
//        {

//        }
//    }
//}
