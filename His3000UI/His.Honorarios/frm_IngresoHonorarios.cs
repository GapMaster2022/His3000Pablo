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
using Core.Entidades;
using Recursos;
using His.General;
using His.Parametros;
using Infragistics.Win.UltraWinGrid;
using System.IO;
using His.Datos;
using His.Entidades.Clases;

namespace His.Honorarios
{
    public partial class frm_IngresoHonorarios : Form
    {
        #region Declaracion de Variables
        public string tipo_empresa = "1";
        public List<DtoAtenciones> atenciones = new List<DtoAtenciones>();
        public DtoAtenciones atencionDtoActual;
        public DtoAtenciones atencionDtoModificada;
        ATENCIONES atencionOriginal;
        ATENCIONES atencionModificada;
        ATENCIONES atencionEliminar;
        public MEDICOS medico;
        public FORMA_PAGO formaPago;
        public TIPO_FORMA_PAGO tipoPago;
        public PACIENTES paciente;
        public PACIENTES_DATOS_ADICIONALES datos;
        public HABITACIONES habitacion;
        public DtoCajas caja;
        public static int referenciaAtencion;
        public static int posicion;
        public static string accion = null;
        public static bool existeHCL = false;
        public static bool iniGrid = false;
        Datos.Atencion obj_atencion = new His.Honorarios.Datos.Atencion();
        DataTable plazoPago = new DataTable();
        List<TIPO_FORMA_PAGO> listaTipoPagos = new List<TIPO_FORMA_PAGO>();
        List<FORMA_PAGO> listaPagos = new List<FORMA_PAGO>();

        List<TIPO_EMPRESA> listaempresa = new List<TIPO_EMPRESA>();

        DataTable DatosAseguradora = new DataTable();
        Datos.Atencion pagos = new His.Honorarios.Datos.Atencion();

        #endregion

        #region Contructor
        public frm_IngresoHonorarios()
        {
            if (NegValidaciones.localAsignado() == false)
            {
                frm_AsignaLocal lista = new frm_AsignaLocal();
                lista.ShowDialog();
            }
            caja = NegCajas.RecuperaCajas().FirstOrDefault(c => c.LOC_CODIGO == His.Entidades.Clases.Sesion.codLocal);
            His.Entidades.Clases.Sesion.nomCaja = caja.CAJ_NOMBRE;
            listaTipoPagos = NegFormaPago.RecuperaTipoFormaPagos();
            listaPagos = NegFormaPago.listaFormasPago();

            InitializeComponent();

            fecCaja.Value = DateTime.Now;
            dateTimePickerFecha.Value = DateTime.Now;
            dateTimePickerFechaFactura.Value = DateTime.Now;
            dateTimePickerFecha.Value = DateTime.Now;
            dtpCaducidad.Value = DateTime.Now;
            fecCaja.MaxDate = DateTime.Now;
            txtPacienteHCL.MaxLength = His.Parametros.HonorariosPAR.TamanoHCL;
            btnNuevo.Image = Recursos.Archivo.imgBtnAdd;
            btnCancelar.Image = Recursos.Archivo.imgBtnError;
            btnEliminar.Image = Recursos.Archivo.imgBtnDelete;
            btnActualizar.Image = Recursos.Archivo.imgBtnRestart;
            btnGuardar.Image = Recursos.Archivo.imgBtnGuardar32;
            btnImprimirHonor.Image = Recursos.Archivo.icono_imprimir;
            btnSalir.Image = Recursos.Archivo.imgBtnSalir32;
            btnImprimir.Image = Recursos.Archivo.imgBtnImprimir32;
            btnRefresh.Appearance.Image = Recursos.Archivo.imgBtnRestart;
            btnAniadir.Appearance.Image = Archivo.imgBtnAdd;
            btnAniadir.ImageSize = new Size(24, 16);
            this.Icon = Recursos.Archivo.icoBtnOrganigrama;

            //cargarTipoFormaPago(cboFiltroTipoFormaPago);

            //bindingNavigatorMoveNextItem.Enabled = false;
            //bindingNavigatorMoveLastItem.Enabled = false;
            //bindingNavigatorMovePreviousItem.Enabled = false;
            //bindingNavigatorMoveFirstItem.Enabled = false;
            //bindingNavigatorCountItem.Enabled = false;
            //bindingNavigatorPositionItem.Enabled = false;
            //    btnActualizar.Enabled = false;
            //btnEliminar.Enabled = false;
            btnImprimir.Enabled = false;
            btnImprimirHonor.Enabled = false;
            btncerrar.Enabled = false;
            //ayudaPacientes.Visible = false;
            ayudaHabitacion.Visible = false;

            //cargarDatos();
        }
        #endregion

        #region Cargar Datos, Consultas


        public void cargarDatos()
        {
            try
            {
                //bindingNavigatorMoveNextItem.Enabled = false;
                //bindingNavigatorMoveLastItem.Enabled = false;
                //bindingNavigatorMovePreviousItem.Enabled = false;
                //bindingNavigatorMoveFirstItem.Enabled = false;
                //bindingNavigatorCountItem.Enabled = false;
                //bindingNavigatorPositionItem.Enabled = false;

                atenciones = NegAtenciones.RecuperaAtenciones(caja.CAJ_CODIGO, fecCaja.Value, His.Entidades.Clases.Sesion.codUsuario);

                DateTime max = new DateTime(fecCaja.Value.Date.Year, fecCaja.Value.Date.Month, fecCaja.Value.Date.Day, 23, 59, 59);
                DateTime min = new DateTime(fecCaja.Value.Date.Year, fecCaja.Value.Date.Month, fecCaja.Value.Date.Day, 0, 0, 0);

                if (min < dateTimePickerFechaFactura.MaxDate)
                {
                    dateTimePickerFechaFactura.MinDate = min;
                    dateTimePickerFechaFactura.MaxDate = max;
                }
                else
                {
                    dateTimePickerFechaFactura.MaxDate = max;
                    dateTimePickerFechaFactura.MinDate = min;
                }

                dateTimePickerFechaIngreso.MaxDate = max;
                dateTimePickerFechaAlta.MaxDate = max;
                dateTimePickerFecha.MaxDate = max;

                limpiarCampos();
                limpiarCamposAtencion();

                if (atenciones.Count > 0)
                {
                    referenciaAtencion = 0;
                    posicion = 1;
                    //bindingNavigatorCountItem.Text = "de " + atenciones.Count;
                    //bindingNavigatorCountItem.Enabled = true;
                    //bindingNavigatorPositionItem.Enabled = true;
                    atencionDtoActual = atenciones.ElementAt(referenciaAtencion);
                    cargarAtencion(atencionDtoActual);
                }

                habilitarCampos();

                if (atenciones.Count <= 0)
                {
                    MessageBox.Show("No existen atenciones", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Excepcion al Cargar Datos: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (e.InnerException != null)
                {
                    MessageBox.Show(e.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        public void cargarDatosPorPaciente(int idPaciente)
        {
            try
            {
                //bindingNavigatorMoveNextItem.Enabled = false;
                //bindingNavigatorMoveLastItem.Enabled = false;
                //bindingNavigatorMovePreviousItem.Enabled = false;
                //bindingNavigatorMoveFirstItem.Enabled = false;
                //bindingNavigatorCountItem.Enabled = false;
                //bindingNavigatorPositionItem.Enabled = false;

                atenciones = NegAtenciones.RecuperarAtencionesPaciente(idPaciente, caja.CAJ_CODIGO, His.Entidades.Clases.Sesion.codUsuario);


                limpiarCampos();
                limpiarCamposAtencion();

                if (atenciones.Count > 0)
                {
                    referenciaAtencion = 0;
                    posicion = 1;
                    //bindingNavigatorCountItem.Text = "de " + atenciones.Count;
                    //bindingNavigatorCountItem.Enabled = true;
                    //bindingNavigatorPositionItem.Enabled = true;
                    atencionDtoActual = atenciones.ElementAt(referenciaAtencion);
                    cargarAtencion(atencionDtoActual);
                }

                habilitarCampos();

                if (atenciones.Count <= 0)
                {
                    // MessageBox.Show("No existen atenciones", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message, "Excepcion al Cargar Datos: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //if (e.InnerException != null)
                //{
                //    MessageBox.Show(e.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }

        }

        public void cargarAtencion(DtoAtenciones atencion)
        {
            try
            {
                referenciaAtencion = atenciones.IndexOf(atencion);
                posicion = referenciaAtencion + 1;
                //bindingNavigatorPositionItem.Text = posicion.ToString();

                txtNumControl.Text = atencion.ATE_NUMERO_CONTROL;
                txtFactura.Text = atencion.ATE_FACTURA_PACIENTE;

                if (atencion.ATE_FACTURA_FECHA != null)
                    dateTimePickerFechaFactura.Value = Convert.ToDateTime(atencion.ATE_FACTURA_FECHA);

                dateTimePickerFechaIngreso.Value = Convert.ToDateTime(atencion.ATE_FECHA_INGRESO);
                dateTimePickerFechaAlta.Value = Convert.ToDateTime(atencion.ATE_FECHA_ALTA);
                txtCodigoPaciente.Text = atencion.PAC_CODIGO.ToString();
                txtHabitacion.Text = atencion.HAB_NUMERO;
                txtAtencion.Text = atencion.ATE_NUMERO_ATENCION;
                checkBoxReferido.Checked = Convert.ToBoolean(atencion.ATE_REFERIDO);

                //cargarHonorariosMedicos();
                validarPosicion();
            }
            catch (Exception f)
            {
                MessageBox.Show("Cargar Atencion excepcion: " + f.Message);
                if (f.InnerException != null)
                    MessageBox.Show(f.InnerException.Message);
            }

        }

        public void cargarHonorariosMedicos()
        {
            try
            {
                ultraGrid.DataSource = NegHonorariosMedicos.listaHonorariosaAtencion(
                   Convert.ToInt32(txtAtencion.Text.Trim()),
                    His.Parametros.HonorariosPAR.codigoTipoMovimientoHonorariosMedicos).Select(h => new
                    {
                        COD = h.HOM_CODIGO,
                        MEDICO = h.MED_NOMBRE,
                        FACTURA = h.HOM_FACTURA_MEDICO,
                        VALE = h.HOM_VALE,
                        FECHA = h.HOM_FACTURA_FECHA,
                        FORMAPAGO = h.FORMAPAGO,
                        F_PAGO = h.FOR_DESCRIPCION,
                        VALOR_NETO = h.HOM_VALOR_NETO,
                        RETENCION = h.HOM_RETENCION,
                        COMISION_CLINICA = h.HOM_COMISION_CLINICA,
                        APORTE_LLAMADA = h.HOM_APORTE_LLAMADA,
                        VALOR_TOTAL = h.HOM_VALOR_TOTAL,
                        MED_CODIGO = h.MED_CODIGO,
                        OBSER = h.HOM_OBSERVACION,
                        POR_FUERA = h.HOM_FUERA
                    }).ToList();



                //PABLO ROCHA 13-03-2019 RECUEPRA HONORARIOS MEDICOS

                //ultraGrid.DataSource = NegHonorariosMedicos.RecuperaHonorarios(Convert.ToInt32(txtAtencion.Text.Trim()));
                //ultraGrid.DataBind();

                //DataTable dtDetalleUltraGrid = new DataTable();
                //dtDetalleUltraGrid = NegHonorariosMedicos.RecuperaHonorarios(Convert.ToInt32(txtAtencion.Text.Trim()));
                //ultraGrid.DataSource = dtDetalleUltraGrid;
                //DataRow drDetalle;
                //for(int aux=0;aux<= dtDetalleUltraGrid.Rows.Count; aux++)
                //{
                //    drDetalle = dtDetalleUltraGrid.NewRow();
                //    drDetalle["COD"] = ultraGrid.Rows[0].Cells[aux].ToString();
                //    drDetalle["MEDICO"] = "COPAGO";
                //    drDetalle["FACTURA"] = String.Format("{0:0.00}", 0.00);
                //    drDetalle["VALE"] ="";
                //    drDetalle["FECHA"] ="";
                //    drDetalle["F_PAGO"] ="";
                //    drDetalle["VALOR_NETO"] ="";
                //    drDetalle["RETENCION"] ="";
                //    drDetalle["COMISION_CLINICA"] ="";
                //    drDetalle["APORTE_LLAMADA"] ="";
                //    drDetalle["VALOR_TOTAL"] ="";
                //    drDetalle["MED_CODIGO"] ="";
                //    dtDetalleUltraGrid.Rows.Add(drDetalle);
                //}


                //temporal.Columns["MEDICO"].Width = 250;
                //temporal.Columns["MED_CODIGO"].Visible = false;

                calcularValoresFacturaPaciente();
                ultraGrid.DisplayLayout.Bands[0].Columns[1].Width = 400;
                ValidaGuardar();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al cargar los honorarios: " + e.Message + ", " + e.InnerException.Message);
            }
        }

        public void cargarMedico(int codigo)
        {
            medico = new MEDICOS();
            medico = NegMedicos.RecuperaMedicoId(codigo);

            if (medico != null)
            {
                txtNombreMedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                txtTelfMedico.Text = medico.MED_TELEFONO_CASA;
                txtAutSri.Text = medico.MED_AUTORIZACION_SRI;
                //txtFecCaducidad.Text = medico.MED_VALIDEZ_AUTORIZACION;

            }
            else
            {

                txtNombreMedico.Text = string.Empty;
                txtTelfMedico.Text = string.Empty;
                txtAutSri.Text = string.Empty;
                //txtFecCaducidad.Text = string.Empty;
                //lblPorcentajeRetencion.Text = string.Empty;
            }
            //calcularReferido();
            //calcularRetencionMedico();
        }

        public void cargarPaciente(int codigo)
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
            }
        }

        public void cargarComision(int codigo)
        {
            bool band = false;
            formaPago = new FORMA_PAGO();
            formaPago = NegFormaPago.RecuperaFormaPagoID((Int16)codigo);
            lblComision.Text = lblComision.Text + "%";
            if (cmb_empresas.Text == "TARJETA DE CREDITO")
            {
                band = true;
            }

            //for (int i = 0; i < His.Parametros.HonorariosPAR.getCodigoTarjetasCredito().Length; i++)
            //    if (formaPago.TIPO_FORMA_PAGO.TIF_CODIGO == His.Parametros.HonorariosPAR.getCodigoTarjetasCredito()[i])
            //        band = true;

            if (band == true)
            {
                txtLote.Visible = true;
                lblLote.Visible = true;
            }
            else
            {
                txtLote.Visible = false;
                lblLote.Visible = false;
            }
            calcularReferido();

            //if (formaPago != null)
            //{


            //}
            //else
            //{
            //    formaPago = null;
            //    txtLote.Text = string.Empty;
            //    lblComision.Text = string.Empty;
            //    lblAporte.Text = string.Empty;
            //    txtLote.Visible = false;
            //    lblLote.Visible = false;
            //}
            calcularRetencionMedico();

        }

        //Carga los combos con los tipos de formas de pago y con sus respectivos pagos

        public void cargarTipoFormaPago(ComboBox combo)
        {
            try
            {
                //NegFormaPago.RecuperaTipoFormaPagos
                //List<TIPO_FORMA_PAGO> listaTipoFormaPago = NegFormaPago.RecuperaTipoFormaPagos();
                ///foreach (TIPO_FORMA_PAGO tipoFormaPago in listaTipoFormaPago)
                //{
                //   combo.Items.Add(new KeyValuePair<int, string>(tipoFormaPago.TIF_CODIGO, tipoFormaPago.TIF_NOMBRE));
                //}
                //combo.DisplayMember = "Value";
                //combo.ValueMember = "Key";
                //                combo.SelectedIndex = 0;

                TIPO_FORMA_PAGO nuevo = new TIPO_FORMA_PAGO();
                nuevo.TIF_CODIGO = 0;
                nuevo.TIF_NOMBRE = "TODAS";
                listaTipoPagos.Insert(0, nuevo);
                combo.DataSource = listaTipoPagos;
                combo.ValueMember = "TIF_CODIGO";
                combo.DisplayMember = "TIF_NOMBRE";
                combo.AutoCompleteSource = AutoCompleteSource.ListItems;
                //combo.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
                combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "err");
            }
        }

        public void cargarFormaPago(ComboBox combo, Int16 tipoFormaPago)
        {
            try
            {
                combo.Items.Clear();
                List<FORMA_PAGO> listaFormaPago = NegFormaPago.RecuperaFormaPago(tipoFormaPago);
                combo.Items.Add(new KeyValuePair<int, string>(0, "TODAS"));
                foreach (FORMA_PAGO formaPago in listaFormaPago)
                {
                    combo.Items.Add(new KeyValuePair<int, string>(formaPago.FOR_CODIGO, formaPago.FOR_DESCRIPCION));
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
        public string numrec = "";
        //Carga un honorario en los campos del medico luego de presionar actualizar
        public void cargarHonorario(int codigoHonorario)
        {
            try
            {
                HONORARIOS_MEDICOS honorario = new NegHonorariosMedicos().RecuperaHonorariosMedicosID(codigoHonorario);

                DataTable datos_honorarios = new DataTable();
                datos_honorarios = obj_atencion.buscar_honorarios(codigoHonorario.ToString());
                txt_codigoFormaspago.Text = datos_honorarios.Rows[0]["FOR_CODIGO"].ToString();
                txt_nombre_pagos.Text = datos_honorarios.Rows[0]["FOR_DESCRIPCION"].ToString();
                string te_codigo = datos_honorarios.Rows[0]["TE_CODIGO"].ToString();

                if (txt_codigoFormaspago.Text == "267")
                {
                    numrec = NegHonorariosMedicos.RecuperarNUMREC(Convert.ToInt32(codigoHonorario));
                    if (numrec != "")
                        NegHonorariosMedicos.ReponerAnticipoSic(numrec, Convert.ToDouble(honorario.HOM_VALOR_NETO.ToString()));
                }

                TXTHOMCOD.Text = honorario.HOM_CODIGO.ToString(); //ALEX3030

                txtCodMedico.Text = Convert.ToString(NegHonorariosMedicos.HonorarioMedico(codigoHonorario));
                txtFacturaMedico.Text = honorario.HOM_FACTURA_MEDICO;
                txt_vale.Text = honorario.HOM_VALE.ToString();
                txtLote.Text = honorario.HOM_LOTE;
                if (txt_vale.Text == "Ninguno" || txt_vale.Text == "")
                {
                    ch_factura.Checked = true;
                    ch_factura.Enabled = true;
                }

                else
                {
                    ch_vale.Enabled = true;
                    ch_vale.Checked = true;
                    txt_vale.Text = honorario.HOM_VALE.ToString();
                    numVale = honorario.HOM_VALE.ToString();
                }
                Int16 codPago = (Int16)honorario.FOR_CODIGO;
                formaPago = NegFormaPago.RecuperaFormaPagoID((Int16)codPago);

                //CAMBIOS EDGAR 20210521
                DataTable FP = new DataTable();
                FP = NegHonorariosMedicos.FPHonorarios(formaPago.FOR_CODIGO);
                int index1 = Convert.ToInt32(FP.Rows[0][0].ToString()); //Codigo de la FORMA DE PAGO
                int index2 = Convert.ToInt32(FP.Rows[0][1].ToString()); //Codigo de la EMPRESA

                cmb_empresas.SelectedValue = index2;
                cboFiltroTipoFormaPago.SelectedValue = index1;
                cboFiltroFormaPago.SelectedValue = codPago;
                //Fin cambios 20210521

                //cmb_empresas.SelectedItem = listaempresa.FirstOrDefault(Z => Z.TE_CODIGO == Convert.ToInt16(te_codigo));
                //cargar_combo_empresas();
                //cmb_empresas.SelectedValue = Convert.ToInt16(te_codigo);
                //cboFiltroTipoFormaPago.SelectedItem = listaTipoPagos.FirstOrDefault(t => t.TIF_CODIGO == formaPago.TIPO_FORMA_PAGO.TIF_CODIGO);
                //cboFiltroFormaPago.SelectedItem = listaPagos.FirstOrDefault(f => f.FOR_CODIGO == codPago);
                bool band = false;

                for (int i = 0; i < His.Parametros.HonorariosPAR.getCodigoTarjetasCredito().Length; i++)
                    if (formaPago.TIPO_FORMA_PAGO.TIF_CODIGO == His.Parametros.HonorariosPAR.getCodigoTarjetasCredito()[i])
                        band = true;

                if (band == true && txtLote.Visible == true)
                    txtLote.Text = honorario.HOM_LOTE;

                if (honorario.TMO_CODIGO == His.Parametros.HonorariosPAR.codigoTipoMovimientoHonorariosDirectos)
                    chkHonorarioDirecto.Checked = true;
                else
                    chkHonorarioDirecto.Checked = false;

                dateTimePickerFecha.Value = Convert.ToDateTime(honorario.HOM_FACTURA_FECHA.ToString());
                txtValorNeto.Text = honorario.HOM_VALOR_NETO.ToString();
                txtAporteMedLLam.Text = honorario.HOM_APORTE_LLAMADA.ToString();
                txtComisionClinica.Text = honorario.HOM_COMISION_CLINICA.ToString();
                txtRetencion.Text = honorario.HOM_RETENCION.ToString();
                txtValorTotal.Text = honorario.HOM_VALOR_TOTAL.ToString();
                if (honorario.HOM_OBSERVACION != null)
                    txtObservacion.Text = honorario.HOM_OBSERVACION;

            }
            catch (Exception err)
            {
                MessageBox.Show("Error al cargar el honorario: " + err.Message);
            }
        }

        #endregion

        #region Ingreso de honorarios y atenciones en la BDD.

        //Guarda en la base de datos un nuevo honorario
        public bool guardarHonorario()
        {
            string campo_vale = "Ninguno";
            try
            {
                if (!Editar)
                {
                    HONORARIOS_MEDICOS nuevoHonorario = new HONORARIOS_MEDICOS();

                    nuevoHonorario.HOM_CODIGO = Convert.ToInt32(ultimoCodigoHonorario() + 1);
                    TXTHOMCOD.Text = nuevoHonorario.HOM_CODIGO.ToString(); //alex3030
                    nuevoHonorario.ATE_CODIGO = Convert.ToInt32(txtAtencion.Text.Trim());                //atencionDtoActual.ATE_CODIGO;

                    int codMedico = Convert.ToInt32(txtCodMedico.Text.ToString());
                    nuevoHonorario.MEDICOSReference.EntityKey = NegMedicos.MedicoID(codMedico).EntityKey;
                    DataTable med = NegMedicos.MedicoIDValida(codMedico);
                    if (med.Rows[0][0].ToString() == "7")
                    {
                        MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCodMedico.Text = "";
                        txtNombreMedico.Text = "";
                        return false;
                    }

                    //KeyValuePair<int, string> item = (KeyValuePair<int, string>)cboFiltroFormaPago.SelectedItem;
                    //Int16 codigoPago = (Int16)(item.Key);
                    //String descPago = (String)(item.Value);
                    try
                    {
                        //if (formaPago == null)
                        //{
                        //    MessageBox.Show("Debe ingresar una forma de pago.", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //    return false;

                        //}
                        //else
                        if (cboFiltroTipoFormaPago.DataSource != null)
                        {
                            string validaforma = formaPago.FOR_CODIGO.ToString();

                            nuevoHonorario.FOR_CODIGO = formaPago.FOR_CODIGO;

                            nuevoHonorario.HOM_FECHA_INGRESO = System.DateTime.Now;

                            bool flag = false;
                            bool flag2 = false;


                            if (chkHonorarioDirecto.Checked == true)
                            {
                                nuevoHonorario.TMO_CODIGO = His.Parametros.HonorariosPAR.codigoTipoMovimientoHonorariosDirectos;
                            }
                            else
                            {
                                nuevoHonorario.TMO_CODIGO = His.Parametros.HonorariosPAR.codigoTipoMovimientoHonorariosMedicos;
                            }
                            if (txt_vale.Text != "" && txtFacturaMedico.Text != "")
                            {
                                campo_vale = txt_vale.Text.Trim();
                            }
                            else
                            {
                                if (txt_vale.Text == "")
                                {
                                    numVale = NegHonorariosMedicos.NumVale();
                                    if (numVale != "")//es vacio cuando son por dentro, ya que viene el numero de vale
                                    {
                                        Int64 valorVale = Convert.ToInt64(numVale);
                                        campo_vale = valorVale.ToString();
                                        valorVale++;
                                        NegHonorariosMedicos.IncrementoVale(valorVale);
                                    }
                                    //campo_vale = "CON FACTURA";
                                }
                                else
                                    campo_vale = txt_vale.Text;
                            }

                            nuevoHonorario.HOM_VALE = campo_vale;
                            nuevoHonorario.HOM_FACTURA_MEDICO = txtFacturaMedico.Text.Replace("-", string.Empty);
                            nuevoHonorario.HOM_FACTURA_FECHA = dateTimePickerFecha.Value;
                            nuevoHonorario.HOM_VALOR_NETO = Convert.ToDecimal(txtValorNeto.Text.ToString());
                            nuevoHonorario.HOM_COMISION_CLINICA = Convert.ToDecimal(txtComisionClinica.Text.ToString());
                            nuevoHonorario.HOM_APORTE_LLAMADA = Convert.ToDecimal(txtAporteMedLLam.Text.ToString());
                            nuevoHonorario.HOM_RETENCION = Convert.ToDecimal(txtRetencion.Text.ToString());
                            nuevoHonorario.HOM_VALOR_TOTAL = Convert.ToDecimal(txtValorTotal.Text.ToString());
                            nuevoHonorario.HOM_ESTADO = His.Parametros.HonorariosPAR.HonorarioCreado;
                            nuevoHonorario.HOM_VALOR_PAGADO = 0;
                            nuevoHonorario.HOM_VALOR_CANCELADO = 0;
                            nuevoHonorario.HOM_RECORTE = Convert.ToDecimal(txtrecorte.Text.ToString());

                            if (txtLote.Visible == true)
                                nuevoHonorario.HOM_LOTE = txtLote.Text.ToString();
                            else
                                nuevoHonorario.HOM_LOTE = "";
                            if (txtObservacion.Text == string.Empty)
                                nuevoHonorario.HOM_OBSERVACION = null;
                            else
                                nuevoHonorario.HOM_OBSERVACION = txtObservacion.Text.ToString();
                            if (!chkXFuera.Checked && id_usuario != 0)
                                nuevoHonorario.USUARIOSReference.EntityKey = NegUsuarios.RecuperaUsuario(Convert.ToInt16(id_usuario)).EntityKey;
                            else
                                nuevoHonorario.USUARIOSReference.EntityKey = NegUsuarios.RecuperaUsuario(His.Entidades.Clases.Sesion.codUsuario).EntityKey;
                            nuevoHonorario.HOM_PACIENTE = txtPacienteApellidoPaterno.Text + " " + txtPacienteApellidoMaterno.Text + " " + txtPacienteNombre.Text + " " + txtPacienteNombre2.Text;

                            NegHonorariosMedicos.CrearHonorarioMedico(nuevoHonorario);

                            if (chkXFuera.Checked && Convert.ToDouble(txtcubierto.Text) > 0)
                                NegHonorariosMedicos.ModificarHonorarioMedico(nuevoHonorario, codMedico, dtpCaducidad.Value, Convert.ToDecimal(txtcubierto.Text), txtAutSri.Text, Convert.ToDecimal(txtapc.Text));
                            id_usuario = 0;

                            //SE CREA PROCESO DE AUDITORIA
                            NegHonorariosMedicos.CrearHonorarioAuditoria(nuevoHonorario, chkXFuera.Checked, false, "NUEVO", Caja(), Convert.ToDouble(txtcubierto.Text), codMedico);

                            if (flag == true)
                                NegNumeroControl.LiberaNumeroControl(7);

                            if (flag2 == true)
                                NegNumeroControl.LiberaNumeroControl(9);

                            if (ch_vale.Checked)
                            {
                                if (numVale != "")//es vacio cuando son por dentro, ya que viene el numero de vale
                                {
                                    Int64 valorVale = Convert.ToInt64(numVale);
                                    valorVale++;
                                    NegHonorariosMedicos.IncrementoVale(valorVale);
                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Algo ocurrio al ingresar datos. Mas Detalles: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    return true;
                }
                else
                {

                    HONORARIOS_MEDICOS nuevoHonorario = new HONORARIOS_MEDICOS();

                    nuevoHonorario.HOM_CODIGO = Convert.ToInt32(TXTHOMCOD.Text);
                    nuevoHonorario.ATE_CODIGO = Convert.ToInt32(txtAtencion.Text.Trim());                //atencionDtoActual.ATE_CODIGO;

                    int codMedico = Convert.ToInt32(txtCodMedico.Text.ToString());
                    nuevoHonorario.MEDICOSReference.EntityKey = NegMedicos.MedicoID(codMedico).EntityKey;
                    DataTable med = NegMedicos.MedicoIDValida(codMedico);
                    if (med.Rows[0][0].ToString() == "7")
                    {
                        MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCodMedico.Text = "";
                        txtNombreMedico.Text = "";
                        return false;
                    }
                    try
                    {
                        if (cboFiltroTipoFormaPago.DataSource != null)
                        {
                            string validaforma = formaPago.FOR_CODIGO.ToString();

                            nuevoHonorario.FOR_CODIGO = formaPago.FOR_CODIGO;

                            nuevoHonorario.HOM_FECHA_INGRESO = System.DateTime.Now;

                            bool flag = false;
                            bool flag2 = false;


                            if (chkHonorarioDirecto.Checked == true)
                            {
                                nuevoHonorario.TMO_CODIGO = His.Parametros.HonorariosPAR.codigoTipoMovimientoHonorariosDirectos;
                            }
                            else
                            {
                                nuevoHonorario.TMO_CODIGO = His.Parametros.HonorariosPAR.codigoTipoMovimientoHonorariosMedicos;
                            }
                            if (txt_vale.Text != "" && txtFacturaMedico.Text != "")
                            {
                                campo_vale = txt_vale.Text.Trim();
                            }
                            else
                            {
                                numVale = NegHonorariosMedicos.NumVale();
                                if (numVale != "")//es vacio cuando son por dentro, ya que viene el numero de vale
                                {
                                    Int64 valorVale = Convert.ToInt64(numVale);
                                    campo_vale = valorVale.ToString();
                                    valorVale++;
                                    NegHonorariosMedicos.IncrementoVale(valorVale);
                                }
                            }

                            nuevoHonorario.HOM_VALE = campo_vale;
                            nuevoHonorario.HOM_FACTURA_MEDICO = txtFacturaMedico.Text.Replace("-", string.Empty);
                            nuevoHonorario.HOM_FACTURA_FECHA = dateTimePickerFecha.Value;
                            nuevoHonorario.HOM_VALOR_NETO = Convert.ToDecimal(txtValorNeto.Text.ToString());
                            nuevoHonorario.HOM_COMISION_CLINICA = Convert.ToDecimal(txtComisionClinica.Text.ToString());
                            nuevoHonorario.HOM_APORTE_LLAMADA = Convert.ToDecimal(txtAporteMedLLam.Text.ToString());
                            nuevoHonorario.HOM_RETENCION = Convert.ToDecimal(txtRetencion.Text.ToString());
                            nuevoHonorario.HOM_VALOR_TOTAL = Convert.ToDecimal(txtValorTotal.Text.ToString());
                            nuevoHonorario.HOM_ESTADO = His.Parametros.HonorariosPAR.HonorarioCreado;
                            nuevoHonorario.HOM_VALOR_PAGADO = 0;
                            nuevoHonorario.HOM_VALOR_CANCELADO = 0;
                            nuevoHonorario.HOM_RECORTE = Convert.ToDecimal(txtrecorte.Text.ToString());

                            if (txtLote.Visible == true)
                                nuevoHonorario.HOM_LOTE = txtLote.Text.ToString();
                            else
                                nuevoHonorario.HOM_LOTE = "";
                            if (txtObservacion.Text == string.Empty)
                                nuevoHonorario.HOM_OBSERVACION = null;
                            else
                                nuevoHonorario.HOM_OBSERVACION = txtObservacion.Text.ToString();
                            if (!chkXFuera.Checked && id_usuario != 0)
                                nuevoHonorario.USUARIOSReference.EntityKey = NegUsuarios.RecuperaUsuario(Convert.ToInt16(id_usuario)).EntityKey;
                            else
                                nuevoHonorario.USUARIOSReference.EntityKey = NegUsuarios.RecuperaUsuario(His.Entidades.Clases.Sesion.codUsuario).EntityKey;
                            nuevoHonorario.HOM_PACIENTE = txtPacienteApellidoPaterno.Text + " " + txtPacienteApellidoMaterno.Text + " " + txtPacienteNombre.Text + " " + txtPacienteNombre2.Text;

                            NegHonorariosMedicos.ModificarHonorarioMedico(nuevoHonorario, codMedico, dtpCaducidad.Value, Convert.ToDecimal(txtcubierto.Text), txtAutSri.Text, Convert.ToDecimal(txtapc.Text));

                            //SE CREA PROCESO DE AUDITORIA
                            NegHonorariosMedicos.CrearHonorarioAuditoria(nuevoHonorario, chkXFuera.Checked, false, "MODIFICADO", Caja(), Convert.ToDouble(txtcubierto.Text), codMedico);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message);
                if (z.InnerException != null)
                    MessageBox.Show(z.InnerException.Message);
                return false;
            }

        }

        //Eliminar un registro de la tabla HonorariosMedicos
        public void eliminarHonorario(int codigoHonorario)
        {
            try
            {
                NegHonorariosMedicos.EliminarHonorarioMedico(codigoHonorario);
            }
            catch (Exception z)
            {
                MessageBox.Show(z.InnerException.Message);
            }
        }

        //Guarda en la base de datos una nueva atencion
        public void crearAtencion()
        {
            try
            {

                FtpClient ftp = new FtpClient();
                ftp.Login();
                ftp.ChangeDir(His.Parametros.HonorariosPAR.DirectorioMicrofilms);
                string dirPac = txtPacienteHCL.Text.Trim().ToString();

                if (paciente == null)
                {
                    paciente = new PACIENTES();
                    paciente.PAC_CODIGO = ultimoCodigoPacientes() + 1;
                    paciente.PAC_NOMBRE1 = txtPacienteNombre.Text.ToString();
                    paciente.PAC_NOMBRE2 = txtPacienteNombre2.Text.ToString();
                    paciente.PAC_APELLIDO_PATERNO = txtPacienteApellidoPaterno.Text.ToString();
                    paciente.PAC_APELLIDO_MATERNO = txtPacienteApellidoMaterno.Text.ToString();
                    paciente.PAC_IDENTIFICACION = txtPacienteCedula.Text.ToString();
                    paciente.PAC_HISTORIA_CLINICA = txtPacienteHCL.Text.ToString();
                    //paciente.CIUDADReference.EntityKey = NegCiudad.RecuperarCiudadID(His.Parametros.HonorariosPAR.CodigoQuito).EntityKey;
                    paciente.ETNIAReference.EntityKey = NegEtnias.RecuperarEtniaID(3).EntityKey;
                    paciente.GRUPO_SANGUINEOReference.EntityKey = NegGrupoSanguineo.RecuperarGrupoSanguineoID(1).EntityKey;
                    paciente.PAC_FECHA_CREACION = System.DateTime.Now;
                    paciente.PAC_FECHA_NACIMIENTO = System.DateTime.Now;
                    paciente.PAC_ESTADO = true;
                    ftp.MakeDir(dirPac);
                    paciente.PAC_DIRECTORIO = ftp.RemotePath + paciente.PAC_HISTORIA_CLINICA;

                    NegPacientes.crearPaciente(paciente);

                    datos = new PACIENTES_DATOS_ADICIONALES();
                    datos.DAP_CODIGO = NegPacienteDatosAdicionales.ultimoCodigoDatos() + 1;
                    datos.DAP_DIRECCION_DOMICILIO = txtPacienteDireccion.Text.ToString();
                    datos.DAP_TELEFONO1 = txtPacienteTelf.Text.Replace("-", string.Empty).ToString();
                    datos.ESTADO_CIVILReference.EntityKey = NegEstadoCivil.RecuperarEstadoCivilID(1).EntityKey;
                    datos.TIPO_CIUDADANOReference.EntityKey = NegTipoCiudadano.RecuperarTipoCiudadanoID(1).EntityKey;
                    datos.DAP_FECHA_INGRESO = DateTime.Now;
                    datos.DAP_ESTADO = true;
                    datos.PACIENTESReference.EntityKey = paciente.EntityKey;
                    NegPacienteDatosAdicionales.CrearPacienteDatosAdicionales(datos, paciente.PAC_CODIGO);

                }
                else
                {
                    paciente.PAC_NOMBRE1 = txtPacienteNombre.Text.ToString();
                    paciente.PAC_NOMBRE2 = txtPacienteNombre2.Text.ToString();
                    paciente.PAC_APELLIDO_PATERNO = txtPacienteApellidoPaterno.Text.ToString();
                    paciente.PAC_APELLIDO_MATERNO = txtPacienteApellidoMaterno.Text.ToString();
                    paciente.PAC_IDENTIFICACION = txtPacienteCedula.Text.ToString();
                    //paciente.PAC_HISTORIA_CLINICA = txtPacienteHCL.Text.ToString();
                    NegPacientes.ActualizarPacienteAtencion(paciente);


                    datos.DAP_DIRECCION_DOMICILIO = txtPacienteDireccion.Text.ToString();
                    datos.DAP_TELEFONO1 = txtPacienteTelf.Text.Replace("-", string.Empty).ToString();
                    NegPacienteDatosAdicionales.EditarPacienteDatosAdicionalesHonorarios(datos);

                }
                atencionDtoModificada.ATE_CODIGO = ultimoCodigoAtenciones() + 1;
                atencionDtoModificada.ATE_NUMERO_CONTROL = txtNumControl.Text;
                atencionDtoModificada.ATE_FACTURA_PACIENTE = txtFactura.Text.Replace("-", string.Empty);
                atencionDtoModificada.ATE_FACTURA_FECHA = dateTimePickerFechaFactura.Value;
                atencionDtoModificada.ATE_FECHA_INGRESO = dateTimePickerFechaIngreso.Value;
                atencionDtoModificada.ATE_FECHA_ALTA = dateTimePickerFechaAlta.Value;
                atencionDtoModificada.ATE_FECHA = DateTime.Today;
                atencionDtoModificada.ATE_REFERIDO = checkBoxReferido.Checked;
                atencionDtoModificada.ATE_ESTADO = false;

                atencionDtoModificada.HAB_CODIGO = habitacion.hab_Codigo;
                atencionDtoModificada.HAB_NUMERO = txtHabitacion.Text;
                atencionDtoModificada.PAC_CODIGO = Convert.ToInt32(txtCodigoPaciente.Text);
                atencionDtoModificada.PAC_NOMBRE = txtPacienteNombre.Text;
                atencionDtoModificada.PAC_NOMBRE2 = txtPacienteNombre2.Text;
                atencionDtoModificada.PAC_APELLIDO_PATERNO = txtPacienteApellidoPaterno.Text;
                atencionDtoModificada.PAC_APELLIDO_MATERNO = txtPacienteApellidoMaterno.Text;
                atencionDtoModificada.PAC_HCL = txtPacienteHCL.Text;
                atencionDtoModificada.PAC_DIRECCION = txtPacienteDireccion.Text;
                atencionDtoModificada.PAC_TELEFONO = txtPacienteTelf.Text;
                atencionDtoModificada.PAC_CEDULA = txtPacienteCedula.Text;
                atencionDtoModificada.ENTITYSETNAME = "HIS3000BDEntities.ATENCIONES";
                atencionDtoModificada.ENTITYID = "ATE_CODIGO";

                atencionModificada = new ATENCIONES();
                NUMERO_CONTROL numerocontrol = new NUMERO_CONTROL();

                if (NegNumeroControl.NumerodeControlAutomatico(8))
                    numerocontrol = NegNumeroControl.RecuperarNumeroControlID(8);

                atencionModificada.ATE_NUMERO_ATENCION = numerocontrol.NUMCON;

                //ftp.ChangeDir(dirPac);
                //ftp.MakeDir(atencionModificada.ATE_NUMERO_ATENCION);

                atencionModificada.ATE_DIRECTORIO = ftp.RemotePath + atencionModificada.ATE_NUMERO_ATENCION;
                atencionModificada.ATE_CODIGO = atencionDtoModificada.ATE_CODIGO;
                atencionModificada.ATE_NUMERO_CONTROL = atencionDtoModificada.ATE_NUMERO_CONTROL;
                atencionModificada.ATE_FACTURA_PACIENTE = atencionDtoModificada.ATE_FACTURA_PACIENTE;
                atencionModificada.ATE_FACTURA_FECHA = atencionDtoModificada.ATE_FACTURA_FECHA;
                atencionModificada.ATE_FECHA_INGRESO = atencionDtoModificada.ATE_FECHA_INGRESO;
                atencionModificada.ATE_FECHA_ALTA = atencionDtoModificada.ATE_FECHA_ALTA;
                atencionModificada.ATE_FECHA = atencionDtoModificada.ATE_FECHA;
                atencionModificada.ATE_REFERIDO = atencionDtoModificada.ATE_REFERIDO;
                atencionModificada.ATE_ESTADO = atencionDtoModificada.ATE_ESTADO;

                atencionModificada.PACIENTESReference.EntityKey = paciente.EntityKey;
                atencionModificada.HABITACIONESReference.EntityKey = habitacion.EntityKey;
                atencionModificada.PACIENTES_DATOS_ADICIONALESReference.EntityKey = datos.EntityKey;

                CAJAS kaja = NegCajas.ListaCajas().FirstOrDefault(c => c.CAJ_CODIGO == caja.CAJ_CODIGO);
                atencionModificada.CAJASReference.EntityKey = kaja.EntityKey;
                USUARIOS usuario = NegUsuarios.RecuperaUsuario(His.Entidades.Clases.Sesion.codUsuario);
                atencionModificada.USUARIOSReference.EntityKey = usuario.EntityKey;

                NegAtenciones.CrearAtencion(atencionModificada);
                NegNumeroControl.LiberaNumeroControl(8);
                atenciones.Add(atencionDtoModificada);
                atencionDtoActual = atencionDtoModificada;
                //bindingNavigatorCountItem.Text = "de " + atenciones.Count;
                MessageBox.Show("Información almacenada exitosamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message);

                if (z.InnerException != null)
                    MessageBox.Show(z.InnerException.Message);
            }

        }

        //Editar una atencion
        public void actualizarAtencion()
        {
            try
            {
                if (paciente == null)
                {
                    paciente = new PACIENTES();
                    paciente.PAC_CODIGO = ultimoCodigoPacientes() + 1;
                    paciente.PAC_NOMBRE1 = txtPacienteNombre.Text.ToString();
                    paciente.PAC_NOMBRE2 = txtPacienteNombre2.Text.ToString();
                    paciente.PAC_APELLIDO_PATERNO = txtPacienteApellidoPaterno.Text.ToString();
                    paciente.PAC_APELLIDO_MATERNO = txtPacienteApellidoMaterno.Text.ToString();
                    paciente.PAC_IDENTIFICACION = txtPacienteCedula.Text.ToString();
                    paciente.PAC_HISTORIA_CLINICA = txtPacienteHCL.Text.ToString();
                    //paciente.CIUDADReference.EntityKey = NegCiudad.RecuperarCiudadID(His.Parametros.HonorariosPAR.CodigoQuito).EntityKey;
                    paciente.ETNIAReference.EntityKey = NegEtnias.RecuperarEtniaID(3).EntityKey;
                    paciente.GRUPO_SANGUINEOReference.EntityKey = NegGrupoSanguineo.RecuperarGrupoSanguineoID(1).EntityKey;
                    paciente.PAC_FECHA_CREACION = System.DateTime.Now;
                    paciente.PAC_FECHA_NACIMIENTO = System.DateTime.Now;
                    paciente.PAC_ESTADO = true;

                    NegPacientes.crearPaciente(paciente);

                    datos = new PACIENTES_DATOS_ADICIONALES();
                    datos.DAP_CODIGO = NegPacienteDatosAdicionales.ultimoCodigoDatos() + 1;
                    datos.DAP_DIRECCION_DOMICILIO = txtPacienteDireccion.Text.ToString();
                    datos.DAP_TELEFONO1 = txtPacienteTelf.Text.Replace("-", string.Empty).ToString();
                    datos.ESTADO_CIVILReference.EntityKey = NegEstadoCivil.RecuperarEstadoCivilID(1).EntityKey;
                    datos.TIPO_CIUDADANOReference.EntityKey = NegTipoCiudadano.RecuperarTipoCiudadanoID(1).EntityKey;
                    datos.DAP_FECHA_INGRESO = DateTime.Now;
                    datos.DAP_ESTADO = true;
                    datos.PACIENTESReference.EntityKey = paciente.EntityKey;
                    NegPacienteDatosAdicionales.CrearPacienteDatosAdicionales(datos, paciente.PAC_CODIGO);

                }
                else
                {
                    paciente.PAC_NOMBRE1 = txtPacienteNombre.Text.ToString();
                    paciente.PAC_NOMBRE2 = txtPacienteNombre2.Text.ToString();
                    paciente.PAC_APELLIDO_PATERNO = txtPacienteApellidoPaterno.Text.ToString();
                    paciente.PAC_APELLIDO_MATERNO = txtPacienteApellidoMaterno.Text.ToString();
                    paciente.PAC_IDENTIFICACION = txtPacienteCedula.Text.ToString();
                    //paciente.PAC_HISTORIA_CLINICA = txtPacienteHCL.Text.ToString();
                    NegPacientes.ActualizarPacienteAtencion(paciente);


                    datos.DAP_DIRECCION_DOMICILIO = txtPacienteDireccion.Text.ToString();
                    datos.DAP_TELEFONO1 = txtPacienteTelf.Text.Replace("-", string.Empty).ToString();
                    NegPacienteDatosAdicionales.EditarPacienteDatosAdicionalesHonorarios(datos);

                }

                atencionDtoModificada.ATE_CODIGO = Convert.ToInt32(txtAtencion.Text.Trim());  //atencionDtoActual.ATE_CODIGO;
                atencionDtoModificada.ATE_NUMERO_CONTROL = txtNumControl.Text;
                atencionDtoModificada.ATE_FACTURA_PACIENTE = txtFactura.Text.Replace("-", string.Empty);
                atencionDtoModificada.ATE_FACTURA_FECHA = dateTimePickerFechaFactura.Value;
                atencionDtoModificada.ATE_FECHA_INGRESO = dateTimePickerFechaIngreso.Value;
                atencionDtoModificada.ATE_FECHA_ALTA = dateTimePickerFechaAlta.Value;
                atencionDtoModificada.ATE_FECHA = atencionDtoActual.ATE_FECHA;
                atencionDtoModificada.ATE_REFERIDO = checkBoxReferido.Checked;
                atencionDtoModificada.ATE_ESTADO = atencionDtoActual.ATE_ESTADO;
                atencionDtoModificada.HAB_CODIGO = habitacion.hab_Codigo;
                atencionDtoModificada.HAB_NUMERO = txtHabitacion.Text;
                atencionDtoModificada.PAC_CODIGO = Convert.ToInt32(txtCodigoPaciente.Text);
                atencionDtoModificada.PAC_NOMBRE = txtPacienteNombre.Text;
                atencionDtoModificada.PAC_NOMBRE2 = txtPacienteNombre2.Text;
                atencionDtoModificada.PAC_APELLIDO_PATERNO = txtPacienteApellidoPaterno.Text;
                atencionDtoModificada.PAC_APELLIDO_MATERNO = txtPacienteApellidoMaterno.Text;
                atencionDtoModificada.PAC_HCL = txtPacienteHCL.Text;
                atencionDtoModificada.PAC_DIRECCION = txtPacienteDireccion.Text;
                atencionDtoModificada.PAC_TELEFONO = txtPacienteTelf.Text;
                atencionDtoModificada.PAC_CEDULA = txtPacienteCedula.Text;
                atencionDtoModificada.ENTITYSETNAME = "HIS3000BDEntities.ATENCIONES";
                atencionDtoModificada.ENTITYID = "ATE_CODIGO";

                atencionModificada = new ATENCIONES();
                atencionModificada.ATE_CODIGO = atencionDtoModificada.ATE_CODIGO;
                atencionModificada.ATE_NUMERO_CONTROL = atencionDtoModificada.ATE_NUMERO_CONTROL;
                atencionModificada.ATE_FACTURA_PACIENTE = atencionDtoModificada.ATE_FACTURA_PACIENTE;
                atencionModificada.ATE_FACTURA_FECHA = atencionDtoModificada.ATE_FACTURA_FECHA;
                atencionModificada.ATE_FECHA_INGRESO = atencionDtoModificada.ATE_FECHA_INGRESO;
                atencionModificada.ATE_FECHA_ALTA = atencionDtoModificada.ATE_FECHA_ALTA;
                atencionModificada.ATE_FECHA = atencionDtoModificada.ATE_FECHA;
                atencionModificada.ATE_REFERIDO = atencionDtoModificada.ATE_REFERIDO;
                atencionModificada.ATE_ESTADO = atencionDtoModificada.ATE_ESTADO;


                atencionModificada.PACIENTESReference.EntityKey = NegPacientes.RecuperarPacienteID(atencionDtoModificada.PAC_CODIGO).EntityKey;
                atencionModificada.HABITACIONESReference.EntityKey = NegHabitaciones.RecuperarHabitacionId(atencionDtoModificada.HAB_CODIGO).EntityKey;
                //CAJAS kaja = NegCajas.ListaCajas().FirstOrDefault(c => c.CAJ_CODIGO == caja.CAJ_CODIGO);
                //atencionModificada.CAJASReference.EntityKey = kaja.EntityKey;

                atencionModificada.EntityKey = new EntityKey("HIS3000BDEntities.ATENCIONES"
                       , "ATE_CODIGO", atencionDtoModificada.ATE_CODIGO);

                atencionOriginal = new ATENCIONES();
                atencionOriginal.ATE_CODIGO = Convert.ToInt32(txtAtencion.Text.Trim());//atencionDtoActual.ATE_CODIGO;
                atencionOriginal.ATE_NUMERO_CONTROL = atencionDtoActual.ATE_NUMERO_CONTROL;
                atencionOriginal.ATE_FACTURA_PACIENTE = atencionDtoActual.ATE_FACTURA_PACIENTE;
                atencionOriginal.ATE_FACTURA_FECHA = atencionDtoActual.ATE_FACTURA_FECHA;
                atencionOriginal.ATE_FECHA_INGRESO = atencionDtoActual.ATE_FECHA_INGRESO;
                atencionOriginal.ATE_FECHA_ALTA = atencionDtoActual.ATE_FECHA_ALTA;
                atencionOriginal.ATE_FECHA = atencionDtoActual.ATE_FECHA;
                atencionOriginal.ATE_REFERIDO = atencionDtoActual.ATE_REFERIDO;
                atencionOriginal.ATE_ESTADO = atencionDtoActual.ATE_ESTADO;

                atencionOriginal.PACIENTESReference.EntityKey = paciente.EntityKey;
                atencionOriginal.HABITACIONESReference.EntityKey = habitacion.EntityKey;

                atencionOriginal.EntityKey = new EntityKey("HIS3000BDEntities.ATENCIONES"
                       , "ATE_CODIGO", Convert.ToInt32(txtAtencion.Text.Trim()));//atencionDtoActual.ATE_CODIGO);

                if (atencionDtoActual.ATE_REFERIDO != atencionDtoModificada.ATE_REFERIDO)
                    if (ultraGrid.Rows.Count > 0)
                    {
                        actualizarValoresFacturas(atencionDtoModificada);
                    }

                NegAtenciones.GrabarAtencion(atencionModificada, atencionOriginal);
                int indice = atenciones.IndexOf(atencionDtoActual);
                atenciones[indice] = atencionDtoModificada;
                atencionDtoActual = atencionDtoModificada;
                MessageBox.Show("Información almacenada exitosamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception z)
            {
                MessageBox.Show(z.Message);
                if (z.InnerException != null)
                    MessageBox.Show(z.InnerException.Message);
            }

        }

        //Eliminar una atencion
        public void eliminarAtencion()
        {
            try
            {
                atencionEliminar = new ATENCIONES();
                atencionEliminar = NegAtenciones.AtencionID(Convert.ToInt32(txtAtencion.Text.Trim()));//atencionDtoActual.ATE_CODIGO);

                NegAtenciones.EliminarAtencion(atencionEliminar.ClonarEntidad());
                atenciones.Remove(atencionDtoActual);
            }
            catch (Exception es)
            {
                MessageBox.Show("La atencion no puede ser eliminada:" + es.InnerException.Message);
            }
        }

        public void actualizarValoresFacturas(DtoAtenciones atencion)
        {
            try
            {
                for (int i = 0; i < ultraGrid.Rows.Count; i++)
                {
                    Int32 codMedico = Convert.ToInt32(ultraGrid.Rows[i].Cells["MED_CODIGO"].Value.ToString());
                    Int32 codHonorario = Convert.ToInt32(ultraGrid.Rows[i].Cells["COD"].Value.ToString());
                    HONORARIOS_MEDICOS hon = new NegHonorariosMedicos().RecuperaHonorariosMedicosID(codHonorario);
                    MEDICOS consultaMedico = NegMedicos.RecuperaMedicoId(codMedico);

                    if (consultaMedico.TIPO_HONORARIO.TIH_CODIGO != 4)
                    {
                        decimal valorNeto = hon.HOM_VALOR_NETO;
                        decimal aporteLlamada = (Decimal)hon.HOM_APORTE_LLAMADA;

                        if (checkBoxReferido.Checked == true)
                        {
                            FORMA_PAGO pago = NegFormaPago.RecuperaFormaPagoID((Int16)hon.FOR_CODIGO);
                            aporteLlamada = Decimal.Round(valorNeto * (Decimal)pago.FOR_REFERIDO / 100, 2);
                        }
                        else
                        {
                            aporteLlamada = 0;
                        }
                        aporteLlamada = Decimal.Round(aporteLlamada, 2);
                        decimal valorTotal = valorNeto - aporteLlamada - (Decimal)hon.HOM_COMISION_CLINICA - (Decimal)hon.HOM_RETENCION;

                        hon.HOM_APORTE_LLAMADA = Decimal.Round(aporteLlamada, 2);
                        hon.HOM_VALOR_TOTAL = Decimal.Round(valorTotal, 2);

                        HONORARIOS_MEDICOS honorarioModificado = new NegHonorariosMedicos().RecuperaHonorariosMedicosID(hon.HOM_CODIGO);
                        honorarioModificado.HOM_APORTE_LLAMADA = hon.HOM_APORTE_LLAMADA;
                        honorarioModificado.HOM_VALOR_TOTAL = hon.HOM_VALOR_TOTAL;
                        new NegHonorariosMedicos().ActualizarValoresHonorariosMedicos(honorarioModificado);
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error al actualizar los valores del honorario: \n" + e.Message);
            }
        }

        #endregion

        #region Calculo de valores

        public Int64 ultimoCodigoHonorario()
        {
            return NegHonorariosMedicos.ultimoCodigoHonorarios();
        }

        public int ultimoCodigoAtenciones()
        {
            return NegAtenciones.UltimoCodigoAtenciones();
        }

        public int ultimoCodigoPacientes()
        {
            return NegPacientes.ultimoCodigoPacientes();
        }

        public bool verificarHonorario(String numFactura, int codMedico, int codigoAtencion)
        {
            return NegHonorariosMedicos.VerificarExistenciaHonorario(numFactura, codMedico, codigoAtencion);
        }

        public void calcularValoresFactura()
        {
            try
            {
                var valor_neto = Convert.ToDouble(txtValorNeto.Text.ToString());
                var comision_clinica = valor_neto * obtenerComisionClinica();
                var aporte_med_llamada = valor_neto * obtenerAporteLlamada();
                var impuesto_renta = 0.00;
                if (lblPorcentajeRetencion.Text != string.Empty)
                    if (lblPorcentajeRetencion.Text == "lblRetenc")
                    {
                        lblPorcentajeRetencion.Text = "10";
                    }
                impuesto_renta = valor_neto * Convert.ToDouble(lblPorcentajeRetencion.Text.ToString().TrimEnd('%')) / 100;
                if (chkXFuera.Checked)
                {
                    impuesto_renta = 0.0;
                }

                var valor_total = valor_neto - comision_clinica - aporte_med_llamada - impuesto_renta;
                txtComisionClinica.Text = "" + comision_clinica.ToString("N2");
                txtAporteMedLLam.Text = "" + aporte_med_llamada.ToString("N2");

                txtRetencion.Text = "" + impuesto_renta.ToString("N2");

                txtValorTotal.Text = "" + valor_total.ToString("N2");
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message); //Aqui solo cuando limpiamos los campos 
            }
        }

        public double obtenerComisionClinica()
        {
            if (lblComision.Text != string.Empty)
            {
                return Convert.ToDouble(lblComision.Text.ToString().TrimEnd('%')) / 100;
            }
            else
                return 0;
        }

        public double obtenerAporteLlamada()
        {
            if (lblAporte.Text != string.Empty)
                return Convert.ToDouble(lblAporte.Text.ToString().TrimEnd('%')) / 100;
            else
                return 0;
        }

        public void calcularValoresFacturaPaciente()
        {
            if (ultraGrid.Rows.Count > 0 && ultraGrid.DataSource != null)
            {
                Decimal totalFactura = 0;
                Decimal totalComision = 0;
                Decimal totalAporte = 0;
                Decimal totalRetenciones = 0;
                Decimal totalHonorarios = 0;
                for (int i = 0; i < ultraGrid.Rows.Count; i++)
                {
                    totalFactura = totalFactura + Convert.ToDecimal(ultraGrid.Rows[i].Cells["VALOR_NETO"].Value.ToString());
                    totalComision = totalComision + Convert.ToDecimal(ultraGrid.Rows[i].Cells["COMISION_CLINICA"].Value.ToString());
                    totalAporte = totalAporte + Convert.ToDecimal(ultraGrid.Rows[i].Cells["APORTE_LLAMADA"].Value.ToString());
                    totalRetenciones = totalRetenciones + Convert.ToDecimal(ultraGrid.Rows[i].Cells["RETENCION"].Value.ToString());
                    totalHonorarios = totalHonorarios + Convert.ToDecimal(ultraGrid.Rows[i].Cells["VALOR_TOTAL"].Value.ToString());
                }
                txtValorFacturaPaciente.Text = totalFactura.ToString();
                txtComisionTotal.Text = totalComision.ToString();
                txtAporteTotal.Text = totalAporte.ToString();
                txtRetencionTotal.Text = totalRetenciones.ToString();
                txtHonorariosTotales.Text = totalHonorarios.ToString();
            }
            else
            {
                txtValorFacturaPaciente.Text = "0.00";
                txtComisionTotal.Text = "0.00";
                txtAporteTotal.Text = "0.00";
                txtRetencionTotal.Text = "0.00";
                txtHonorariosTotales.Text = "0.00";
            }
        }

        public void calcularReferido()
        {
            if (checkBoxReferido.Checked == true && chkHonorarioDirecto.Checked == false)
                if (medico != null)
                    if (medico.TIPO_HONORARIO.TIH_CODIGO != His.Parametros.HonorariosPAR.getCodigoTipoHonorarioNoCobraComision())
                        lblAporte.Text = lblAporte.Text + "%";
                    else
                        lblAporte.Text = "0.00%";
                else
                    lblAporte.Text = lblAporte.Text + "%";
            else
            {
                if (medico != null)
                {
                    if ((bool)medico.MED_CON_TRANSFERENCIA)
                        lblAporte.Text = lblAporte.Text + "%";
                    else
                        lblAporte.Text = "0.00%";
                }
                else
                    lblAporte.Text = "0.00%";
            }
        }

        public void calcularRetencionMedico()
        {
            lblPorcentajeRetencion.Text = "0.00%";
            lblPorcentajeRetencion.Visible = true;
            if (formaPago != null)
            {
                if (medico != null)
                    if (chkHonorarioDirecto.Checked == true
                        || medico.TIPO_HONORARIO.TIH_CODIGO == His.Parametros.HonorariosPAR.getCodigoTipoHonorarioNoCobraComision())
                        lblPorcentajeRetencion.Text = "0.00%";
                    else
                        lblPorcentajeRetencion.Text = "" + Convert.ToDecimal(medico.RETENCIONES_FUENTE.RET_PORCENTAJE) + "%";
                else
                    lblPorcentajeRetencion.Text = string.Empty;
            }
            else
            {
                if (medico != null)
                    if (medico.TIPO_HONORARIO.TIH_CODIGO == His.Parametros.HonorariosPAR.getCodigoTipoHonorarioNoCobraComision())
                        lblPorcentajeRetencion.Text = "0.00%";
                    else
                        lblPorcentajeRetencion.Text = "" + Convert.ToDecimal(medico.RETENCIONES_FUENTE.RET_PORCENTAJE) + "%";
                else
                    lblPorcentajeRetencion.Text = string.Empty;
            }

        }

        //public bool existenciaAtencion(String numControl, String numFactura)
        //{
        //    return NegAtenciones.existeAtencion(numControl, numFactura);
        //}

        public bool honorariosGeneraronPagosRetenciones()
        {
            return NegHonorariosMedicos.honorariosGeneraronPagosRetenciones(Convert.ToInt32(txtAtencion.Text.Trim()));//atencionDtoActual.ATE_CODIGO);
        }


        #endregion

        #region Control de eventos sobre los botones

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            mensajeMedico = "";
            #region Codigo Anterior
            //if (this.dateTimePickerFechaFactura.Value < this.dateTimePickerFechaIngreso.Value)
            //{
            //    MessageBox.Show("La fecha de facturación no puede ser menor a la fecha de ingreso.", "His3000");
            //    return;
            //}

            //if (this.dateTimePickerFechaAlta.Value < this.dateTimePickerFechaIngreso.Value)
            //{
            //    MessageBox.Show("La fecha de alta no puede ser menor a la fecha de ingreso.", "His3000");
            //    return;
            //}


            //string fecha_factura = dateTimePickerFechaFactura.Value.Date.Month.ToString() + "/" +
            //                         dateTimePickerFechaFactura.Value.Date.Day.ToString() + "/" +
            //                         dateTimePickerFechaFactura.Value.Date.Year.ToString();
            //string fechaIngreso = dateTimePickerFechaIngreso.Value.Date.Month.ToString() + "/" +
            //                         dateTimePickerFechaIngreso.Value.Date.Day.ToString() + "/" +
            //                         dateTimePickerFechaIngreso.Value.Date.Year.ToString();

            //string fechaAlta = dateTimePickerFechaAlta.Value.Date.Month.ToString() + "/" +
            //                         dateTimePickerFechaAlta.Value.Date.Day.ToString() + "/" +
            //                         dateTimePickerFechaAlta.Value.Date.Year.ToString();


            //var op = MessageBox.Show("Seguro desea guardar los cambios realizados?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (op == DialogResult.Yes)
            //{
            //    if (obj_atencion.modificar(txtAtencion.Text.Trim(),
            //                               txtFactura.Text.Trim(),
            //                               txtNumControl.Text.Trim(),
            //                               fecha_factura,
            //                               fechaIngreso,
            //                               fechaAlta))
            //    {
            //        MessageBox.Show("Registro guardado exitosamente");
            //        btnActualizar.Enabled = true;
            //        btnGuardar.Enabled = false;
            //        txtFactura.ReadOnly = true;
            //        txtNumControl.ReadOnly = true;
            //        btnCancelar.Enabled = false;
            //    }
            //    else
            //    {
            //        MessageBox.Show("Error al  guardar Registro");
            //        btnActualizar.Enabled = true;
            //        btnGuardar.Enabled = false;
            //        txtFactura.ReadOnly = true;
            //        txtNumControl.ReadOnly = true;
            //        btnCancelar.Enabled = false;
            //    }
            //}
            //txtCodigoPaciente.Focus();

            //if (paciente != null)
            //    existeHCL = false;

            //if (existeHCL==false)
            //{
            //    progressBar1.Increment(20);
            //    if (accion == "nuevo" && ValidarFormularioAtenciones() == true)
            //    {
            //        //if (existenciaAtencion(txtNumControl.Text, txtFactura.Text.Replace("-", string.Empty)) == false)
            //        //{
            //            crearAtencion();
            //            progressBar1.Increment(20);
            //            habilitarCampos();
            //            limpiarCampos();
            //            progressBar1.Increment(20);
            //            cboFiltroTipoFormaPago.SelectedIndex = 0;
            //            controlErrores.Clear();
            //            cargarAtencion(atencionDtoActual);
            //            accion = null;
            //            progressBar1.Increment(20);
            //            txtCodMedico.Focus();
            //            atencionDtoModificada = null;
            //            progressBar1.Increment(20);

            //        //}
            //        //else
            //        //{
            //        //    MessageBox.Show("Esta factura ya fue ingresada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        //    txtNumControl.Text = string.Empty;
            //        //    txtFactura.Text = string.Empty;
            //        //    txtNumControl.Focus();
            //        //}
            //    }
            //    if (accion == "editar" && ValidarFormularioAtenciones() == true)
            //    {
            // actualizarAtencion();
            //        progressBar1.Increment(20);
            //        limpiarCampos();
            //        progressBar1.Increment(20);
            //        cboFiltroTipoFormaPago.SelectedIndex = 0;
            //        controlErrores.Clear();
            //        habilitarCampos();
            //        progressBar1.Increment(20);
            //        cargarAtencion(atencionDtoActual);
            //        accion = null;
            //        txtCodMedico.Focus();
            //        atencionDtoModificada = null;
            //        progressBar1.Increment(20);
            //    }
            //    progressBar1.Value = progressBar1.Minimum;
            //}
            //existeHCL = false;
            #endregion
            bool x = false; //VALIDA SI HAY ERROR AL GUARDAR
            if (MessageBox.Show("¿Está seguro de generar directamente el Asiento contable?", "HIS3000", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //Solo se generaran los que el campo generado_asiento = 0
                foreach (UltraGridRow item in ultraGrid.Rows)
                {
                    if (!NegHonorariosMedicos.AsientoGenerado(Convert.ToInt64(item.Cells["COD"].Value)))
                    {
                        DataTable Tabla = new DataTable();
                        Tabla = NegHonorariosMedicos.HonorarioIndividual(Convert.ToInt64(item.Cells["COD"].Value));
                        if (Tabla.Rows.Count > 0)
                        {
                            if (!AsientoContable(Tabla))
                            {
                                x = false;
                                break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Algo ocurrio con el honorario del medico: " + item.Cells["MEDICO"].Value.ToString(), "His3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                if (!x)
                {
                    MessageBox.Show("Asiento(s) generado correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ValidaGuardar();
                    if (mensajeMedico.Trim() != "")
                        MessageBox.Show(mensajeMedico, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    this.Close();
            }
        }
        string mensajeMedico = "";
        public bool AsientoContable(DataTable row)
        {
            try
            {
                List<DtoCgDetmae> cgDetmaes = new List<DtoCgDetmae>();

                int AUXILIARLINEA = 1;
                string numing, tipord, fecha, linea, codcue, columna, valor, codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche, tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad, observa, fecpago, codretencion, cajachica, forpag, for_descripcion;
                DataTable cgcodconcli = NegDietetica.getDataTable("getCgCueCliente", row.Rows[0]["MED_CODIGO"].ToString());
                try
                {
                    cuecliente = cgcodconcli.Rows[0][0].ToString();
                }
                catch (Exception ex)
                {
                    cuecliente = "0";
                    mensajeMedico = "Medico: " + row.Rows[0]["MEDICO"].ToString() + " sin codigo contable";
                    return false;
                }

                ///GERMANIZA:: "SI TIENES EN 0 NO TE DEBE PERMITIR GENERAR EL ASIENTO"
                if (cuecliente == "0")
                {
                    MessageBox.Show("No existe el codigo del medico " + row.Rows[0]["MEDICO"].ToString() + " creado en Contabilidad, no es posible generar el asiento de la factura " + row.Rows[0]["FACTURA"].ToString(), "His3000");
                    return false;
                }

                if (row.Rows[0]["FACTURA"].ToString().Trim() == string.Empty)
                {
                    if (!Convert.ToBoolean(row.Rows[0]["HON_X_FUERA"].ToString().Trim()))
                    {
                        MessageBox.Show("Es necesario registre el numero de factura del medico " + row.Rows[0]["MEDICO"].ToString() + " , no es posible generar el asiento.", "His3000");
                        return false;
                    }

                }

                DataTable dtC = NegDietetica.getDataTable("EscCodigo_byAteCodigo", row.Rows[0]["ATE_CODIGO"].ToString());
                string xauxx = dtC.Rows[0]["ESC_CODIGO"].ToString();
                if (!Convert.ToBoolean(row.Rows[0]["HON_X_FUERA"].ToString().Trim()))
                {
                    if (xauxx.Trim() != "6")
                    {

                        //if (row.Cells["FACTURA"].Value.ToString().Trim() == string.Empty || row.Cells["forpag"].Value.ToString()==)
                        MessageBox.Show("Es necesario tener el Nº facturada, La cuenta para que se generen los asientos, no es posible generar el asiento.", "His3000");
                        return false;
                    }
                }

                #region Genero Asiento
                DataTable dt1 = NegDietetica.getDataTable("getNumeroControlAsiento", row.Rows[0]["HOM_CODIGO"].ToString());
                numing = dt1.Rows[0][0].ToString();

                tipord = "8";
                //fecha = (Convert.ToDateTime(row.Cells["FECHA_FACTURA_MED"].Value)).ToString("yyyy'-'MM'-'dd'T'00':'00':'00");
                fecha = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'00':'00':'00");
                codlocal = "1";
                codzona = "1";
                estado = "N";

                forpag = ""; for_descripcion = "";
                forpag = row.Rows[0]["forpag"].ToString();
                for_descripcion = row.Rows[0]["FORMA PAGO"].ToString();
                fecaux = fecha.Substring(0, 10).Replace("-", "");
                PACIENTES dtoPacientes = new PACIENTES();
                ATENCIONES ultimaAtencion = new ATENCIONES();
                dtoPacientes = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(row.Rows[0]["ATE_CODIGO"].ToString()));
                ultimaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(row.Rows[0]["ATE_CODIGO"].ToString()));
                HONORARIOS_MEDICOS honorarioaBorrar = new HONORARIOS_MEDICOS();
                honorarioaBorrar = new NegHonorariosMedicos().RecuperaHonorariosMedicosID(Convert.ToInt64(row.Rows[0]["HOM_CODIGO"].ToString()));

                DataTable Tabla = new DataTable();
                Tabla = NegHonorariosMedicos.HMDatosAdiciones(Convert.ToInt32(row.Rows[0]["HOM_CODIGO"].ToString()));
                if (row.Rows[0]["FACTURA"].ToString().Trim() == "")
                    nocomp = honorarioaBorrar.HOM_VALE;
                else
                    nocomp = row.Rows[0]["FACTURA"].ToString();
                numche = "0";


                //tipmov = "FCS";/////////????????????????
                codcentrocosto = "0";
                codrubro = "0";
                codactividad = "0";
                autorizacion = row.Rows[0]["AUTORIZACION"].ToString();
                fechacad = row.Rows[0]["CADUCIDAD"].ToString();
                if (honorarioaBorrar.HOM_OBSERVACION != null)
                    observa = honorarioaBorrar.HOM_OBSERVACION + " PCTE." + row.Rows[0]["PACIENTE"].ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Rows[0]["ATE_CODIGO"].ToString() + "]";
                else
                    observa = "PCTE." + row.Rows[0]["PACIENTE"].ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Rows[0]["ATE_CODIGO"].ToString() + "]";
                fecpago = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'00':'00':'00");
                if (!Convert.ToBoolean(row.Rows[0]["HON_X_FUERA"].ToString().Trim()))
                    codretencion = row.Rows[0]["COD_RET"].ToString(); ///TOMO DEL EXISTENTE, ATADO AL MEDICO
                else
                    codretencion = "0";
                cajachica = "3";

                //asiento de VALOR NETO
                if (!Convert.ToBoolean(row.Rows[0]["HON_X_FUERA"].ToString().Trim()))
                {
                    tipmov = "FSS";
                    linea = AUXILIARLINEA.ToString();
                    codcue = row.Rows[0]["CTA_HONORARIOS"].ToString();
                    columna = "DEBE";
                    valor = (Convert.ToDouble(row.Rows[0]["VALOR"].ToString())).ToString().Replace(",", ".");
                    valor = Math.Round(Convert.ToDouble(valor), 2).ToString();
                    comentario = "HONORARIOS MEDICOS";
                    //if (Convert.ToDouble(row.Cells["VALOR"].Value) > 0)
                    //{
                    string[] x1 = new string[] { numing, tipord, fecha, linea, codcue, columna, valor,
                                codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche,
                                tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad,
                                observa, fecpago, codretencion, cajachica, forpag, for_descripcion,
                            row.Rows[0]["HOM_CODIGO"].ToString(), Sesion.codUsuario.ToString()};
                    //NegDietetica.setROW("AsientoContable", x1);

                    if (Convert.ToDouble(valor) > 0)
                    {
                        //alimento los campos del CGDETMAe
                        DtoCgDetmae Detmae = new DtoCgDetmae();
                        Detmae.tipdoc = "AD";
                        Detmae.numdoc = 0; //mando en cero
                        Detmae.linea = AUXILIARLINEA;
                        Detmae.año = dtpFechaAsiento.Value.Year.ToString();
                        Detmae.fechatran = dtpFechaAsiento.Value;
                        Detmae.codzona = 1; Detmae.codloc = 1;
                        string[] arr = codcue.Split('-');//separo la cuenta en dos
                        Detmae.cuenta_pc = arr[0];
                        Detmae.subcta_pc = arr[1];
                        Detmae.codcue_cp = arr[0].Substring(0, 1);
                        Detmae.codpre_pc = codcue;
                        Detmae.codigo_c = Convert.ToDouble(cuecliente.Trim());
                        Detmae.nocomp = nocomp;
                        Detmae.beneficiario = row.Rows[0]["MEDICO"].ToString();
                        Detmae.debe = Convert.ToDouble(valor);
                        Detmae.haber = 0;
                        Detmae.comentario = "HONORARIOS MEDICOS" + " FORMA DE PAGO: " + forpag + " - " + for_descripcion;
                        Detmae.movbanc = tipmov;
                        if (honorarioaBorrar.HOM_OBSERVACION != null)
                            Detmae.observacion = honorarioaBorrar.HOM_OBSERVACION + " PCTE." + row.Rows[0]["PACIENTE"].ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Rows[0]["ATE_CODIGO"].ToString() + "]";
                        else
                            Detmae.observacion = "PCTE." + row.Rows[0]["PACIENTE"].ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Rows[0]["ATE_CODIGO"].ToString() + "]";
                        Detmae.hom_codigo = Convert.ToInt64(row.Rows[0]["HOM_CODIGO"].ToString());
                        Detmae.forpag = forpag;
                        Detmae.despag = for_descripcion;

                        cgDetmaes.Add(Detmae);//añado a la lista
                        AUXILIARLINEA++;
                    }
                }
                else
                {
                    string cuenta = NegMedicos.CuentaContableSic(row.Rows[0]["forpag"].ToString());
                    tipmov = "0";
                    linea = AUXILIARLINEA.ToString();
                    codcue = cuenta;
                    columna = "DEBE";
                    valor = (Convert.ToDouble(row.Rows[0]["VALOR"].ToString())).ToString().Replace(",", ".");
                    valor = (Math.Round(Convert.ToDouble(valor), 2)).ToString();
                    if (row.Rows[0]["VALOR_CUBIERTO"].ToString() != "")
                    {
                        valor = (Convert.ToDouble(valor) - Convert.ToDouble(row.Rows[0]["VALOR_CUBIERTO"].ToString()) - Convert.ToDouble(row.Rows[0]["RECORTE"].ToString())).ToString();
                        valor = (Math.Round(Convert.ToDouble(valor), 2)).ToString();
                    }
                    else
                    {
                        valor = (Convert.ToDouble(valor) - Convert.ToDouble(row.Rows[0]["RECORTE"].ToString())).ToString();
                        valor = (Math.Round(Convert.ToDouble(valor), 2)).ToString();
                    }
                    comentario = for_descripcion;
                    //if (Convert.ToDouble(row.Cells["VALOR"].Value) > 0)
                    //{
                    string[] x1 = new string[] { numing, tipord, fecha, linea, codcue, columna, valor, codlocal,
                                codzona, estado, fecaux, comentario, cuecliente, nocomp, numche, tipmov, codcentrocosto,
                                codrubro, codactividad, autorizacion, fechacad, observa, fecpago, codretencion, cajachica,
                                forpag, for_descripcion,  row.Rows[0]["HOM_CODIGO"].ToString(), Sesion.codUsuario.ToString() };
                    //NegDietetica.setROW("AsientoContable", x1);
                    if (Convert.ToDouble(valor) > 0)
                    {
                        DtoCgDetmae Detmae = new DtoCgDetmae();
                        Detmae.tipdoc = "AD";
                        Detmae.numdoc = 0; //mando en cero
                        Detmae.linea = AUXILIARLINEA;
                        Detmae.año = dtpFechaAsiento.Value.Year.ToString();
                        Detmae.fechatran = dtpFechaAsiento.Value;
                        Detmae.codzona = 1; Detmae.codloc = 1;
                        string[] arr = codcue.Split('-');//separo la cuenta en dos
                        Detmae.cuenta_pc = arr[0];
                        Detmae.subcta_pc = arr[1];
                        Detmae.codcue_cp = arr[0].Substring(0, 1);//CUENTA CONTABLE 
                        Detmae.codpre_pc = codcue;
                        Detmae.codigo_c = Convert.ToDouble(cuecliente.Trim());
                        Detmae.nocomp = nocomp;
                        Detmae.beneficiario = row.Rows[0]["MEDICO"].ToString();
                        Detmae.debe = Convert.ToDouble(valor);
                        Detmae.haber = 0;
                        Detmae.comentario = "HONORARIOS MEDICOS" + " FORMA DE PAGO: " + forpag + " - " + for_descripcion;
                        Detmae.movbanc = tipmov;
                        if (honorarioaBorrar.HOM_OBSERVACION != null)
                            Detmae.observacion = honorarioaBorrar.HOM_OBSERVACION + " PCTE." + row.Rows[0]["PACIENTE"].ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Rows[0]["ATE_CODIGO"].ToString() + "]";
                        else
                            Detmae.observacion = "PCTE." + row.Rows[0]["PACIENTE"].ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Rows[0]["ATE_CODIGO"].ToString() + "]";
                        Detmae.hom_codigo = Convert.ToInt64(row.Rows[0]["HOM_CODIGO"].ToString());
                        Detmae.forpag = forpag;
                        Detmae.despag = for_descripcion;
                        cgDetmaes.Add(Detmae);//añado a la lista
                        AUXILIARLINEA++;
                        //}
                    }

                }

                //asiento de APORTE
                linea = AUXILIARLINEA.ToString();
                tipmov = "0";
                codcue = row.Rows[0]["CTA_APORTE"].ToString();
                columna = "HABER";
                valor = (Convert.ToDouble(row.Rows[0]["APORTE"].ToString())).ToString().Replace(",", ".");
                valor = Math.Round(Convert.ToDouble(valor), 2).ToString();
                if (valor.Trim() == "")
                    valor = "0";
                comentario = "APORTE";

                if (Convert.ToDouble(row.Rows[0]["APORTE"].ToString()) > 0)
                {
                    string[] x2 = new string[] { numing, tipord, fecha, linea, codcue, columna, valor,
                                    codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche,
                                    tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad,
                                    observa, fecpago, codretencion, cajachica, forpag, for_descripcion,
                                 row.Rows[0]["HOM_CODIGO"].ToString(), Sesion.codUsuario.ToString()};


                    //NegDietetica.setROW("AsientoContable", x2);

                    DtoCgDetmae Detmae = new DtoCgDetmae();
                    Detmae.tipdoc = "AD";
                    Detmae.numdoc = 0; //mando en cero
                    Detmae.linea = AUXILIARLINEA;
                    Detmae.año = dtpFechaAsiento.Value.Year.ToString();
                    Detmae.fechatran = dtpFechaAsiento.Value;
                    Detmae.codzona = 1; Detmae.codloc = 1;
                    string[] arr = codcue.Split('-');//separo la cuenta en dos
                    Detmae.cuenta_pc = arr[0];
                    Detmae.subcta_pc = arr[1];
                    Detmae.codcue_cp = arr[0].Substring(0, 1);
                    Detmae.codpre_pc = codcue;
                    Detmae.codigo_c = Convert.ToDouble(cuecliente.Trim());
                    Detmae.nocomp = nocomp;
                    Detmae.beneficiario = row.Rows[0]["MEDICO"].ToString();
                    Detmae.debe = 0;
                    Detmae.haber = Convert.ToDouble(valor);
                    Detmae.comentario = "APORTE" + " FORMA DE PAGO: " + forpag + " - " + for_descripcion;
                    Detmae.movbanc = tipmov;
                    if (honorarioaBorrar.HOM_OBSERVACION != null)
                        Detmae.observacion = honorarioaBorrar.HOM_OBSERVACION + " PCTE." + row.Rows[0]["PACIENTE"].ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Rows[0]["ATE_CODIGO"].ToString() + "]";
                    else
                        Detmae.observacion = "PCTE." + row.Rows[0]["PACIENTE"].ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Rows[0]["ATE_CODIGO"].ToString() + "]";
                    Detmae.hom_codigo = Convert.ToInt64(row.Rows[0]["HOM_CODIGO"].ToString());
                    Detmae.forpag = forpag;
                    Detmae.despag = for_descripcion;
                    cgDetmaes.Add(Detmae);//añado a la lista
                    AUXILIARLINEA++;
                }
                else
                {
                    //APORTE DE APC QUE NO SE TOMO EN CUENTA 
                    if (Tabla.Rows[0]["HON_EXCESO"].ToString() != "" && Tabla.Rows[0]["HON_EXCESO"].ToString() != "0.00")
                    {
                        linea = AUXILIARLINEA.ToString();
                        tipmov = "0";
                        codcue = row.Rows[0]["CTA_APORTE"].ToString();
                        columna = "HABER";
                        valor = Tabla.Rows[0]["HON_EXCESO"].ToString();
                        valor = Math.Round(Convert.ToDouble(valor), 2).ToString();
                        if (valor.Trim() == "")
                            valor = "0";
                        comentario = "APORTE";

                        string[] x2 = new string[] { numing, tipord, fecha, linea, codcue, columna, valor,
                                    codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche,
                                    tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad,
                                    observa, fecpago, codretencion, cajachica, forpag, for_descripcion,
                                 row.Rows[0]["HOM_CODIGO"].ToString(), Sesion.codUsuario.ToString()};
                        //NegDietetica.setROW("AsientoContable", x2);

                        DtoCgDetmae Detmae = new DtoCgDetmae();
                        Detmae.tipdoc = "AD";
                        Detmae.numdoc = 0; //mando en cero
                        Detmae.linea = AUXILIARLINEA;
                        Detmae.año = dtpFechaAsiento.Value.Year.ToString();
                        Detmae.fechatran = dtpFechaAsiento.Value;
                        Detmae.codzona = 1; Detmae.codloc = 1;
                        string[] arr = codcue.Split('-');//separo la cuenta en dos
                        Detmae.cuenta_pc = arr[0];
                        Detmae.subcta_pc = arr[1];
                        Detmae.codcue_cp = arr[0].Substring(0, 1);
                        Detmae.codpre_pc = codcue;
                        Detmae.codigo_c = Convert.ToDouble(cuecliente.Trim());
                        Detmae.nocomp = nocomp;
                        Detmae.beneficiario = row.Rows[0]["MEDICO"].ToString();
                        Detmae.debe = 0;
                        Detmae.haber = Convert.ToDouble(valor);
                        Detmae.comentario = "APORTE" + " FORMA DE PAGO: " + forpag + " - " + for_descripcion;
                        Detmae.movbanc = tipmov;
                        if (honorarioaBorrar.HOM_OBSERVACION != null)
                            Detmae.observacion = honorarioaBorrar.HOM_OBSERVACION + " PCTE." + row.Rows[0]["PACIENTE"].ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Rows[0]["ATE_CODIGO"].ToString() + "]";
                        else
                            Detmae.observacion = "PCTE." + row.Rows[0]["PACIENTE"].ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Rows[0]["ATE_CODIGO"].ToString() + "]";
                        Detmae.hom_codigo = Convert.ToInt64(row.Rows[0]["HOM_CODIGO"].ToString());
                        cgDetmaes.Add(Detmae);//añado a la lista
                        AUXILIARLINEA++;

                        linea = AUXILIARLINEA.ToString();
                        tipmov = "0";
                        codcue = "210101-005";
                        columna = "DEBE";
                        valor = Tabla.Rows[0]["HON_EXCESO"].ToString();
                        valor = Math.Round(Convert.ToDouble(valor), 2).ToString();
                        if (valor.Trim() == "")
                            valor = "0";
                        comentario = "HON.MED." + (row.Rows[0]["MEDICO"].ToString()); //NOMBRE DEL MEDICO

                        string[] x = new string[] { numing, tipord, fecha, linea, codcue, columna, valor,
                                    codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche,
                                    tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad, observa,
                                    fecpago, codretencion, cajachica, forpag, for_descripcion,
                                 row.Rows[0]["HOM_CODIGO"].ToString(), Sesion.codUsuario.ToString()};
                        //NegDietetica.setROW("AsientoContable", x);

                        Detmae = new DtoCgDetmae();
                        Detmae.tipdoc = "AD";
                        Detmae.numdoc = 0; //mando en cero
                        Detmae.linea = AUXILIARLINEA;
                        Detmae.año = dtpFechaAsiento.Value.Year.ToString();
                        Detmae.fechatran = dtpFechaAsiento.Value;
                        Detmae.codzona = 1; Detmae.codloc = 1;
                        arr = codcue.Split('-');//separo la cuenta en dos
                        Detmae.cuenta_pc = arr[0];
                        Detmae.subcta_pc = arr[1];
                        Detmae.codcue_cp = arr[0].Substring(0, 1);
                        Detmae.codpre_pc = codcue;
                        Detmae.codigo_c = Convert.ToDouble(cuecliente.Trim());
                        Detmae.nocomp = nocomp;
                        Detmae.beneficiario = row.Rows[0]["MEDICO"].ToString();
                        Detmae.debe = Convert.ToDouble(valor);
                        Detmae.haber = 0;
                        Detmae.comentario = "HON.MED. " + row.Rows[0]["MEDICO"].ToString() + " FORMA DE PAGO: " + forpag + " - " + for_descripcion;
                        Detmae.movbanc = tipmov;
                        if (honorarioaBorrar.HOM_OBSERVACION != null)
                            Detmae.observacion = honorarioaBorrar.HOM_OBSERVACION + " PCTE." + row.Rows[0]["PACIENTE"].ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Rows[0]["ATE_CODIGO"].ToString() + "]";
                        else
                            Detmae.observacion = "PCTE." + row.Rows[0]["PACIENTE"].ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Rows[0]["ATE_CODIGO"].ToString() + "]";
                        Detmae.hom_codigo = Convert.ToInt64(row.Rows[0]["HOM_CODIGO"].ToString());
                        Detmae.forpag = forpag;
                        Detmae.despag = for_descripcion;
                        cgDetmaes.Add(Detmae);//añado a la lista
                        AUXILIARLINEA++;

                    }
                }
                //asiento de COMISION
                linea = AUXILIARLINEA.ToString();
                tipmov = "0";
                codcue = row.Rows[0]["CTA_COMISION"].ToString();
                columna = "HABER";
                valor = (Convert.ToDouble(row.Rows[0]["COMISION"].ToString())).ToString().Replace(",", ".");
                if (valor.Trim() == "")
                    valor = "0";
                comentario = "COMISION";
                if (Convert.ToDouble(row.Rows[0]["COMISION"].ToString()) > 0)
                {
                    string[] x3 = new string[] { numing, tipord, fecha, linea, codcue, columna, valor,
                                    codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche,
                                    tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad,
                                    observa, fecpago, codretencion, cajachica, forpag, for_descripcion,
                                 row.Rows[0]["HOM_CODIGO"].ToString(), Sesion.codUsuario.ToString()};
                    //NegDietetica.setROW("AsientoContable", x3);

                    DtoCgDetmae Detmae = new DtoCgDetmae();
                    Detmae.tipdoc = "AD";
                    Detmae.numdoc = 0; //mando en cero
                    Detmae.linea = AUXILIARLINEA;
                    Detmae.año = dtpFechaAsiento.Value.Year.ToString();
                    Detmae.fechatran = dtpFechaAsiento.Value;
                    Detmae.codzona = 1; Detmae.codloc = 1;
                    string[] arr = codcue.Split('-');//separo la cuenta en dos
                    Detmae.cuenta_pc = arr[0];
                    Detmae.subcta_pc = arr[1];
                    Detmae.codcue_cp = arr[0].Substring(0, 1);
                    Detmae.codpre_pc = codcue;
                    Detmae.codigo_c = Convert.ToDouble(cuecliente.Trim());
                    Detmae.nocomp = nocomp;
                    Detmae.beneficiario = row.Rows[0]["MEDICO"].ToString();
                    Detmae.debe = 0;
                    Detmae.haber = Convert.ToDouble(valor);
                    Detmae.comentario = "COMISION" + " FORMA DE PAGO: " + forpag + " - " + for_descripcion;
                    Detmae.movbanc = tipmov;
                    if (honorarioaBorrar.HOM_OBSERVACION != null)
                        Detmae.observacion = honorarioaBorrar.HOM_OBSERVACION + " PCTE." + row.Rows[0]["PACIENTE"].ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Rows[0]["ATE_CODIGO"].ToString() + "]";
                    else
                        Detmae.observacion = "PCTE." + row.Rows[0]["PACIENTE"].ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Rows[0]["ATE_CODIGO"].ToString() + "]";
                    Detmae.hom_codigo = Convert.ToInt64(row.Rows[0]["HOM_CODIGO"].ToString());
                    Detmae.forpag = forpag;
                    Detmae.despag = for_descripcion;
                    cgDetmaes.Add(Detmae);//añado a la lista
                    AUXILIARLINEA++;
                }
                //asiento de RETENCION
                linea = AUXILIARLINEA.ToString();
                tipmov = "RFS";
                codcue = row.Rows[0]["CTA_RETENCION"].ToString();
                columna = "HABER";
                valor = (Convert.ToDouble(row.Rows[0]["RETENCION"].ToString())).ToString().Replace(",", ".");
                if (valor.Trim() == "")
                    valor = "0";
                comentario = "RETENCION";
                if (Convert.ToDouble(row.Rows[0]["RETENCION"].ToString()) > 0)
                {
                    string[] x4 = new string[] { numing, tipord, fecha, linea, codcue, columna,
                                    valor, codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp,
                                    numche, tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad,
                                    observa, fecpago, codretencion, cajachica, forpag, for_descripcion,
                                 row.Rows[0]["HOM_CODIGO"].ToString(), Sesion.codUsuario.ToString()};
                    //NegDietetica.setROW("AsientoContable", x4);

                    DtoCgDetmae Detmae = new DtoCgDetmae();
                    Detmae.tipdoc = "AD";
                    Detmae.numdoc = 0; //mando en cero
                    Detmae.linea = AUXILIARLINEA;
                    Detmae.año = dtpFechaAsiento.Value.Year.ToString();
                    Detmae.fechatran = dtpFechaAsiento.Value;
                    Detmae.codzona = 1; Detmae.codloc = 1;
                    string[] arr = codcue.Split('-');//separo la cuenta en dos
                    Detmae.cuenta_pc = arr[0];
                    Detmae.subcta_pc = arr[1];
                    Detmae.codcue_cp = arr[0].Substring(0, 1);
                    Detmae.codpre_pc = codcue;
                    Detmae.codigo_c = Convert.ToDouble(cuecliente.Trim());
                    Detmae.nocomp = nocomp;
                    Detmae.beneficiario = row.Rows[0]["MEDICO"].ToString();
                    Detmae.debe = 0;
                    Detmae.haber = Convert.ToDouble(valor);
                    Detmae.comentario = "RETENCION" + " FORMA DE PAGO: " + forpag + " - " + for_descripcion;
                    Detmae.movbanc = tipmov;
                    if (honorarioaBorrar.HOM_OBSERVACION != null)
                        Detmae.observacion = honorarioaBorrar.HOM_OBSERVACION + " PCTE." + row.Rows[0]["PACIENTE"].ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Rows[0]["ATE_CODIGO"].ToString() + "]";
                    else
                        Detmae.observacion = "PCTE." + row.Rows[0]["PACIENTE"].ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Rows[0]["ATE_CODIGO"].ToString() + "]";
                    Detmae.hom_codigo = Convert.ToInt64(row.Rows[0]["HOM_CODIGO"].ToString());
                    Detmae.forpag = forpag;
                    Detmae.despag = for_descripcion;
                    cgDetmaes.Add(Detmae);//añado a la lista
                    AUXILIARLINEA++;
                }
                //asiento de POR PAGAR
                linea = AUXILIARLINEA.ToString();
                tipmov = "0";
                codcue = row.Rows[0]["CTA_MEDICO"].ToString();
                columna = "HABER";
                if (Convert.ToBoolean(row.Rows[0]["HON_X_FUERA"].ToString().Trim()))
                {
                    valor = (Convert.ToDouble(row.Rows[0]["A_PAGAR"].ToString())).ToString().Replace(",", ".");
                }
                else
                    valor = (Convert.ToDouble(row.Rows[0]["A_PAGAR"].ToString())).ToString().Replace(",", ".");
                if (valor.Trim() == "")
                    valor = "0";
                comentario = "HON.MED." + (row.Rows[0]["MEDICO"].ToString()); ////A NOMBRE DE MEDICO

                if (Convert.ToDouble(row.Rows[0]["A_PAGAR"].ToString()) > 0)
                {
                    string[] x5 = new string[] { numing, tipord, fecha, linea, codcue, columna, valor,
                                    codlocal, codzona, estado, fecaux, comentario, cuecliente, nocomp, numche,
                                    tipmov, codcentrocosto, codrubro, codactividad, autorizacion, fechacad, observa,
                                    fecpago, codretencion, cajachica, forpag, for_descripcion,
                                 row.Rows[0]["HOM_CODIGO"].ToString(), Sesion.codUsuario.ToString()};
                    //NegDietetica.setROW("AsientoContable", x5);

                    DtoCgDetmae Detmae = new DtoCgDetmae();
                    Detmae.tipdoc = "AD";
                    Detmae.numdoc = 0; //mando en cero
                    Detmae.linea = AUXILIARLINEA;
                    Detmae.año = dtpFechaAsiento.Value.Year.ToString();
                    Detmae.fechatran = dtpFechaAsiento.Value;
                    Detmae.codzona = 1; Detmae.codloc = 1;
                    string[] arr = codcue.Split('-');//separo la cuenta en dos
                    Detmae.cuenta_pc = arr[0];
                    Detmae.subcta_pc = arr[1];
                    Detmae.codcue_cp = arr[0].Substring(0, 1);
                    Detmae.codpre_pc = codcue;
                    Detmae.codigo_c = Convert.ToDouble(cuecliente.Trim());
                    Detmae.nocomp = nocomp;
                    Detmae.beneficiario = row.Rows[0]["MEDICO"].ToString();
                    Detmae.debe = 0;
                    Detmae.haber = Convert.ToDouble(valor);
                    Detmae.comentario = "HON.MED. " + row.Rows[0]["MEDICO"].ToString() + " FORMA DE PAGO: " + forpag + " - " + for_descripcion;
                    Detmae.movbanc = tipmov;
                    if (honorarioaBorrar.HOM_OBSERVACION != null)
                        Detmae.observacion = honorarioaBorrar.HOM_OBSERVACION + " PCTE." + row.Rows[0]["PACIENTE"].ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Rows[0]["ATE_CODIGO"].ToString() + "]";
                    else
                        Detmae.observacion = "PCTE." + row.Rows[0]["PACIENTE"].ToString() + " HC: " + dtoPacientes.PAC_HISTORIA_CLINICA + " ATENCION: " + ultimaAtencion.ATE_NUMERO_ATENCION.Trim() + "  [" + row.Rows[0]["ATE_CODIGO"].ToString() + "]";
                    Detmae.hom_codigo = Convert.ToInt64(row.Rows[0]["HOM_CODIGO"].ToString());
                    Detmae.forpag = forpag;
                    Detmae.despag = for_descripcion;

                    cgDetmaes.Add(Detmae);//añado a la lista
                    AUXILIARLINEA++;
                }

                //GENERO EL ASIENTO
                if (cgDetmaes.Count > 0)
                {
                    string mensaje = NegHonorariosMedicos.GenerarAsientoContableHonorario(cgDetmaes, dtpFechaAsiento.Value);
                    if (mensaje != "Asiento directo generado correctamente.")
                    {
                        MessageBox.Show(mensaje, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }


                    //SE CREA LA AUDITORIA DE LOS HONORARIOS
                    NegHonorariosMedicos.CrearHonorarioAuditoria(honorarioaBorrar, Convert.ToBoolean(row.Rows[0]["HON_X_FUERA"].ToString()), true, "ASIENTO", cajachica, Convert.ToDouble(row.Rows[0]["VALOR_CUBIERTO"].ToString()), Convert.ToInt64(row.Rows[0]["MED_CODIGO"].ToString()));
                    return true;
                }
                else
                {
                    MessageBox.Show("No es necesario generar honorario cubierto al 100%", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            accion = "editar";
            //atencionDtoModificada = new DtoAtenciones();
            /// deshabilitarCampos();
            //txtCodMedico.Focus();
            // limpiarCampos();
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            txtFactura.ReadOnly = false;
            txtNumControl.ReadOnly = false;
            dateTimePickerFechaFactura.Enabled = true;
            dateTimePickerFechaIngreso.Enabled = true;
            dateTimePickerFechaAlta.Enabled = true;

        }

        private void btnReporte_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            atencionDtoModificada = new DtoAtenciones();
            paciente = null;
            accion = "nuevo";
            limpiarCamposAtencion();
            limpiarCampos();
            calcularValoresFacturaPaciente();
            //txtCodigoPaciente.Text = (ultimoCodigoPacientes() + 1).ToString();
            deshabilitarCampos();
            txtCodigoPaciente.Focus();
            codAnticipo = null;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            #region Codigo anterior
            //if (honorariosGeneraronPagosRetenciones() == false)
            //{
            //    var op = MessageBox.Show("Seguro desea eliminar esta atención?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (op == DialogResult.Yes)
            //    {
            //        int indice = atenciones.IndexOf(atencionDtoActual);
            //        if (indice != 0)
            //            indice = indice - 1;

            //        eliminarAtencion();

            //        if (atenciones.Count > 0)
            //        {
            //            atencionDtoActual = atenciones[indice];
            //            //bindingNavigatorCountItem.Text = "de " + atenciones.Count;
            //            cargarAtencion(atencionDtoActual);
            //        }
            //        else
            //        {
            //            limpiarCamposAtencion();
            //            //bindingNavigatorCountItem.Text = "de " + atenciones.Count;
            //            //bindingNavigatorPositionItem.Text = "0";
            //            validarPosicion();
            //        }
            //        habilitarCampos();
            //        limpiarCampos();
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Esta atencion no puede ser eliminada, \n ya que sus honorarios ya fueron pagados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            #endregion
            UltraGridRow Fila = ultraGrid.ActiveRow;
            if (Fila != null)
            {
                if (MessageBox.Show("¿Está seguro de anular el asiento del medico: " + Fila.Cells["MEDICO"].Value.ToString(), "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
                {

                    DataTable cgcodconcli = NegDietetica.getDataTable("getCgCueCliente", Fila.Cells["MED_CODIGO"].Value.ToString());
                    string codcli = cgcodconcli.Rows[0][0].ToString();

                    DataTable ValidaAsiento = new DataTable();
                    if (Fila.Cells["FACTURA"].Value.ToString().Trim() != "")
                        ValidaAsiento = NegHonorariosMedicos.ValidacionAD(Convert.ToInt64(Fila.Cells["COD"].Value), codcli, Fila.Cells["FACTURA"].Value.ToString());
                    else
                        ValidaAsiento = NegHonorariosMedicos.ValidacionAD(Convert.ToInt64(Fila.Cells["COD"].Value), codcli, Fila.Cells["VALE"].Value.ToString());

                    if (ValidaAsiento.Rows.Count > 0)
                    {
                        try
                        {
                            USUARIOS usuario = NegUsuarios.RecuperarUsuarioID(Convert.ToInt32(ValidaAsiento.Rows[0][2].ToString()));
                            DEPARTAMENTOS departamento = NegDepartamentos.RecuperarDepartamento(usuario.ID_USUARIO);

                            if (departamento.DEP_CODIGO == His.Entidades.Clases.Sesion.codDepartamento)
                            {
                                for (int i = 0; i < ValidaAsiento.Rows.Count; i++)
                                {
                                    if (ValidaAsiento.Rows[i]["VALOR"].ToString() == "0")
                                    {
                                        MessageBox.Show(ValidaAsiento.Rows[i]["TIPO"].ToString() + " ya ha sido cancelado.\r\nNo se puede anular, Comuniquese con contabilidad.", "HIS3000",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }

                                //Si no regreso es porque aun no esta pagado.
                                if (NegCg3000.AnularAD(Convert.ToInt64(Fila.Cells["COD"].Value)))
                                    MessageBox.Show("Se Anulo correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    MessageBox.Show("Algo ocurrio al anular por favor comuniquese con sistemas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show("No puede anular asiento generado por: " + usuario.APELLIDOS + " " + usuario.NOMBRES + " departamento: " + departamento.DEP_NOMBRE,
                                    "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("No tiene valores para anular sobre ese medico.\r\nMas detalle: " + ex.Message,
                                "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
                MessageBox.Show("Debe elegir el honorario a anular.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        # endregion

        # region Eventos sobre el DataGridView de los honorarios Medicos


        private void temporal_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }


        #endregion

        # region Controles sobre los campos de texto y etiquetas para el ingreso de una nueva atencion junto con sus honorarios

        private void txtCodMedico_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == GeneralPAR.TeclaAyuda)
                {
                    frm_Ayudas lista = new frm_Ayudas(NegMedicos.listaMedicos());
                    lista.tabla = "MEDICOS";
                    lista.campoPadre = txtCodMedico;
                    lista.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void txtCodMedico_TextChanged(object sender, EventArgs e)
        {
            if (Microsoft.VisualBasic.Information.IsNumeric(txtCodMedico.Text) == false)
                txtCodMedico.Text = string.Empty;

            if (txtCodMedico.Text != string.Empty)
            {
                cargarMedico(Convert.ToInt32(txtCodMedico.Text.ToString()));
                cmb_empresas.Enabled = true;
                cboFiltroTipoFormaPago.Enabled = true;
            }
            else
            {
                medico = null;
                //calcularReferido();
                calcularRetencionMedico();
                txtNombreMedico.Text = string.Empty;
                txtTelfMedico.Text = string.Empty;
                txtAutSri.Text = string.Empty;
                //txtFecCaducidad.Text = string.Empty;
                lblPorcentajeRetencion.Text = string.Empty;
                //txt_nombre_pagos.Text = string.Empty; ' La forma de pago es del paciente / Giovanny Tapia / 14/03/2013
                txtLote.Text = string.Empty;
            }
        }

        private void txtValorNeto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboFiltroFormaPago.Text == "ANTICIPOS" && chkXFuera.Checked)
                {
                    if (codAnticipo != null)
                    {
                        DataTable AnticipoFormaP = NegFormaPago.FormaPagoAnticipo(codAnticipo);
                        if (AnticipoFormaP.Rows.Count > 0)
                        {
                            if (AnticipoFormaP.Rows[0][1].ToString().Contains("EFECTIVO"))
                                lblComision.Text = "0.00 %";
                        }
                    }
                }
                Calculos();
            }
            catch
            {

            }
        }
        public void Calculos()
        {
            if (cboFiltroTipoFormaPago.Text != "")
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
                    txtComisionClinica.Text = "0.00";
                    txtRetencion.Text = "0.00";
                    txtAporteMedLLam.Text = "0.00";
                    txtValorTotal.Text = "0.00";
                    txtcubierto.Text = "0.00";
                }
            }
            else
            {
                if (txtValorNeto.Text != "0.00")
                {
                    txtValorNeto.Text = "0.00";
                    MessageBox.Show("Debe primero escoger la forma de pago", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }

        private void txtValorNeto_Enter(object sender, EventArgs e)
        {
            if (txtValorNeto.Text == "0.00")
            {
                txtValorNeto.Text = string.Empty;
            }
        }

        private void txtValorNeto_Leave(object sender, EventArgs e)
        {
            if (txtValorNeto.Text == string.Empty)
            {
                txtValorNeto.Text = "0.00";
            }
        }

        private void lblAporte_TextChanged(object sender, EventArgs e)
        {
            //calcularValoresFactura();
        }

        private void lblPorcentajeRetencion_TextChanged(object sender, EventArgs e)
        {
            //calcularValoresFactura();
        }

        private void lblComision_TextChanged(object sender, EventArgs e)
        {
            //calcularValoresFactura();
        }

        private void txtBuscarNumControl_TextChanged(object sender, EventArgs e)
        {

            if (Microsoft.VisualBasic.Information.IsNumeric(txtBuscarNumControl.Text.ToString()) == false)
                txtBuscarNumControl.Text = string.Empty;

            if (txtBuscarNumControl.Text != string.Empty)
            {
                var numControl = txtBuscarNumControl.Text.ToString();

                DtoAtenciones atencionBuscar = atenciones.FirstOrDefault(
                    a => a.ATE_NUMERO_CONTROL == numControl);

                if (atencionBuscar != null)
                {
                    atencionDtoActual = atencionBuscar;
                    cargarAtencion(atencionDtoActual);
                }
            }
        }

        private void txtBuscarNumControl_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    frm_Ayudas lista = new frm_Ayudas(atenciones);
                    lista.campoPadre = txtBuscarNumControl;
                    lista.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void txtCodMedico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //chkHonorarioDirecto.Focus();
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtCodFormaPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void dateTimePickerFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //cboFiltroTipoFormaPago.Focus();
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtValorNeto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                //btnAniadir.Focus();
            }
        }

        private void txtBuscarNumControl_MouseMove(object sender, MouseEventArgs e)
        {
            txtBuscarNumControl.BackColor = Color.White;
            txtBuscarNumControl.ForeColor = Color.Gray;
        }

        private void txtBuscarNumControl_Enter(object sender, EventArgs e)
        {
            txtBuscarNumControl.BackColor = Color.White;
            txtBuscarNumControl.ForeColor = Color.Gray;
        }

        private void txtBuscarNumControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //groupBoxPaciente.Focus();
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtBuscarNumControl_Leave(object sender, EventArgs e)
        {
            txtBuscarNumControl.BackColor = Color.Gray;
            txtBuscarNumControl.ForeColor = Color.White;
        }

        private void txtFacturaMed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = false;
            else if (Char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
                {
                    e.Handled = true;
                    //dateTimePickerFecha.Focus();
                    cmb_empresas.Focus();
                    //SendKeys.SendWait("{TAB}");
                }
            }
            else if (Char.IsSeparator(e.KeyChar))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void cboFiltroFormaPago_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtLote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //txtObservacion.Focus();
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtFacturaMedico_Leave(object sender, EventArgs e)
        {
            //if (chkXFuera.Checked)
            //    validarNumeroFacturaMedico();
        }

        public void validarNumeroFacturaMedico()
        {
            if (txtFacturaMedico.Text != "   -   -")
            {
                string factura = txtFacturaMedico.Text.Replace("-", "");
                factura = factura.Replace(" ", "");
                if (factura.Length == 15)
                {
                    int codMedico = Convert.ToInt16(txtCodMedico.Text);
                    bool facturaRepetida = false;
                    facturaRepetida = NegHonorariosMedicos.DatosRecuperaFacturasMedicos(codMedico, factura);
                    if (!facturaRepetida)
                    {
                        string serie = factura.Substring(0, 6);
                        Int64 numFact = Convert.ToInt64(factura.Substring(6, 9));
                        //DataTable validaFactura = new DataTable();
                        //validaFactura = obj_atencion.ValidaFactura(Convert.ToInt64(txtCodMedico.Text));
                        //if (validaFactura.Rows.Count > 0)
                        //{
                        //    foreach (DataRow item in validaFactura.Rows)
                        //    {
                        //        if (DateTime.Now.Date <= Convert.ToDateTime(item[5].ToString()).Date)
                        //        {
                        //            if (serie == item[6].ToString())
                        //            {
                        //                if (numFact >= Convert.ToInt64(item[7].ToString()) && numFact <= Convert.ToInt64(item[8].ToString()))
                        //                {
                        //                    cmb_empresas.Focus();
                        //                    txtAutSri.Text = item[4].ToString();
                        //                    dtpCaducidad.Value = Convert.ToDateTime(item[5].ToString()).Date;
                        //                    ch_factura.Checked = true;
                        //                    ch_vale.Checked = false;
                        //                    txt_vale.Enabled = false;
                        //                    return;
                        //                }
                        //            }
                        //        }
                        //        //    else
                        //        //        MessageBox.Show("Secuencial de factura ingresada no coincide con lo registrado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //        //else
                        //        //    MessageBox.Show("Serie de factura ingresada no coincide con lo registrado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //        //else
                        //        //{
                        //        //    MessageBox.Show("Libretin de facturas registradas se encuentra caducado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //        //    return;
                        //        //}
                        //        //txtFacturaMedico.Text = "";
                        //        //MessageBox.Show("Serie de factura ingresada no coincide con lo registrado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    }
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Médico no cuenta con facturas registradas ó Libretin de facturas registradas se encuentra caducado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    txtFacturaMedico.Text = "";
                        //}
                    }
                    else
                    {
                        MessageBox.Show("Factura del médico ya fue utilizada para liquidar otro Honorario", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtFacturaMedico.Text = "";
                        txtFacturaMedico.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("formato de factura incorrecto", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void checkBoxReferido_CheckedChanged(object sender, EventArgs e)
        {
            if (cboFiltroFormaPago.SelectedIndex > 0) { }
            //calcularReferido();
        }

        private void txtCodMedico_Leave(object sender, EventArgs e)
        {
            if (txtCodMedico.Text != string.Empty)
            {
                if (medico != null)
                {
                    validarNumeroFacturaMedico();
                }
                else
                {
                    txtCodMedico.Focus();
                }
            }
        }

        private void txtCodigoPaciente_TextChanged(object sender, EventArgs e)
        {
            ultraGrid.DataSource = null;
            btnRefresh.Enabled = true;
            btn_ayuda_pagos.Enabled = true;
            btnActualizar.Enabled = true;
            txtLote.Visible = false;
            lblLote.Visible = false;

            try
            {
                if (Microsoft.VisualBasic.Information.IsNumeric(txtCodigoPaciente.Text) == false)
                    txtCodigoPaciente.Text = string.Empty;

                if (txtCodigoPaciente.Text != string.Empty)
                {
                    cargarPaciente(Convert.ToInt32(txtCodigoPaciente.Text.ToString()));
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

        private void txtCodigoPaciente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtCodigoPaciente_KeyDown(object sender, KeyEventArgs e)
        {
            if (accion != null)
            {
                try
                {
                    if (e.KeyCode == GeneralPAR.TeclaAyuda)
                    {
                        frm_ayudapac lista = new frm_ayudapac();
                        lista.campoPadre = txtCodigoPaciente;
                        lista.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }

        }

        private void txtCodHabitacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (accion != null)
            {
                try
                {
                    if (e.KeyCode == GeneralPAR.TeclaAyuda)
                    {
                        frm_Ayudas lista = new frm_Ayudas(NegHabitaciones.listaHabitaciones());
                        lista.tabla = "HABITACIONES";
                        lista.campoPadre = txtHabitacion;
                        lista.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }
        }

        private void txtCodHabitacion_TextChanged(object sender, EventArgs e)
        {
            string numHab = txtHabitacion.Text.Trim().ToString();
            if (numHab != string.Empty)
            {
                habitacion = NegHabitaciones.RecuperaHabitacionPorNumero(numHab);
            }
            else
            {
                habitacion = null;
            }
        }

        private void txtCodHabitacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void checkBoxReferido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void dateTimePickerFechaIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtFactura_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }

        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
                txtFactura.Focus();
            }
        }

        private void txtFactura_Leave(object sender, EventArgs e)
        {
            if (txtFactura.Text != "   -   -")
            {
                string factura = txtFactura.Text.Trim().Replace("-", string.Empty);
                factura = factura.Replace(" ", "");
                if (factura.Length > 9)
                    factura = factura.Substring(6);

                if (txtFactura.Text.Replace("-", string.Empty).Length < 15)
                    txtFactura.Text = "001-001-" + string.Format("{0:000000000}", int.Parse(factura));

            }
        }

        //private void txtNumControl_Leave(object sender, EventArgs e)
        //{
        //    string ncontrol = txtNumControl.Text.ToString().Trim();

        //    if (ncontrol != string.Empty)
        //    {
        //        if (ncontrol.Length < 6)
        //        {

        //            ncontrol = ncontrol.Replace(" ", string.Empty);
        //            string cerosizq = "";
        //            for (int i = 0; i < 6 - ncontrol.Length; i++)
        //                cerosizq = cerosizq + "0";

        //            ncontrol = cerosizq + ncontrol;
        //            txtNumControl.Text = ncontrol;
        //        }
        //    }
        //}

        private void txtCodigoPaciente_Leave(object sender, EventArgs e)
        {
            if (paciente == null && accion != null)
            {
                //txtCodigoPaciente.Text = (ultimoCodigoPacientes() + 1).ToString();
                txtCodigoPaciente.Text = string.Empty;
            }
        }

        private void txtPacienteNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //txtPacienteNombre2.Focus();
                //SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtPacienteNombre2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //txtPacienteApellidoPaterno.Focus();
                //SendKeys.SendWait("{TAB}");
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtPacienteApellidoPaterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //txtPacienteApellidoMaterno.Focus();
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtPacienteApellidoMaterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //txtPacienteHCL.Focus();
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtPacienteHCL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //txtPacienteTelf.Focus();
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtPacienteTelf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //txtPacienteCedula.Focus();
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtPacienteCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //txtPacienteDireccion.Focus();
                SendKeys.SendWait("{TAB}");
            }
        }

        private void txtPacienteDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //txtNumControl.Focus();
                SendKeys.SendWait("{TAB}");
            }
        }

        private void dateTimePickerFechaFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //dateTimePickerFechaIngreso.Focus();
                SendKeys.SendWait("{TAB}");
            }
        }

        private void dateTimePickerFechaAlta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                SendKeys.SendWait("{TAB}");
            }

        }

        #endregion

        #region Otros

        public void limpiarCampos()
        {
            txtCodMedico.Text = string.Empty;
            txtFacturaMedico.Text = string.Empty;
            //dateTimePickerFecha.Value = dateTimePickerFecha.MaxDate;
            dateTimePickerFecha.Value = dateTimePickerFecha.Value;
            if (Convert.ToInt32(cboFiltroTipoFormaPago.SelectedValue) != 15)
                numrec = "";
            if (numrec != "")
            {
                double monto = NegHonorariosMedicos.ValorAnticipo(numrec);//este es el valor disponible
                HONORARIOS_MEDICOS honorario = new NegHonorariosMedicos().RecuperaHonorariosMedicosID(Convert.ToInt64(TXTHOMCOD.Text));
                if ((double)honorario.HOM_VALOR_NETO == monto)
                {
                    NegHonorariosMedicos.HonorarioAnticipoSic(1, Convert.ToDouble(honorario.HOM_VALOR_NETO), numrec); //ocupo todo
                    numrec = "";
                }
                else if (monto != 0)
                {
                    NegHonorariosMedicos.HonorarioAnticipoSic(0, Convert.ToDouble(honorario.HOM_VALOR_NETO), numrec); //ocupa valor neto mas no todo el anticipo
                    numrec = "";
                }
                else
                    numrec = "";
            }
            try
            {
                txtValorNeto.Text = "0.00";
            }
            catch
            {

            }
            txtCodMedico.Text = string.Empty;
            txtFacturaMedico.Text = string.Empty;
            txtObservacion.Text = string.Empty;
            txt_vale.Text = string.Empty;
            chkXFuera.Checked = true;
            chkXFuera.Enabled = false;
            dtpCaducidad.Value = DateTime.Now;
            cmb_empresas.Enabled = false;
            cboFiltroTipoFormaPago.Enabled = false;
            cboFiltroFormaPago.Enabled = false;
            txtapc.Visible = false;
            txtapc.Text = "0.00";
            txtrecorte.Text = "0.00";
            txtAporteMedLLam.Visible = true;
            if (txtPacienteApellidoPaterno.Text.Trim() != "")
            {
                cargarHonorariosMedicos();
            }
            apcActivado = false;
            chkComision.Checked = false;
            btnAnticipos.Visible = false;
            codAnticipo = null;
        }

        public void limpiarCamposAtencion()
        {
            //txtCodigoPaciente.Text=string.Empty;
            txtNumControl.Text = string.Empty;
            txtFactura.Text = string.Empty;
            txtPacienteDireccion.Text = string.Empty;
            txtPacienteHCL.Text = string.Empty;
            txtPacienteNombre.Text = string.Empty;
            txtPacienteNombre2.Text = string.Empty;
            txtPacienteApellidoPaterno.Text = string.Empty;
            txtPacienteApellidoMaterno.Text = string.Empty;
            txtPacienteTelf.Text = string.Empty;
            txtPacienteCedula.Text = string.Empty;
            txtHabitacion.Text = string.Empty;
            dateTimePickerFechaIngreso.Value = dateTimePickerFechaIngreso.MaxDate;
            dateTimePickerFechaAlta.Value = dateTimePickerFechaAlta.MaxDate;
            dateTimePickerFechaFactura.Value = dateTimePickerFechaFactura.MaxDate;
            checkBoxReferido.Checked = false;
            //temporal.DataSource = null;
            ultraGrid.DataSource = null;
        }

        public void deshabilitarCampos()
        {
            //temporal.Columns["delete"].Visible = false;
            //temporal.Columns["edit"].Visible = false;

            if (ultraGrid.DisplayLayout.Bands[0].Columns.Exists("delete"))
                ultraGrid.DisplayLayout.Bands[0].Columns["delete"].Hidden = true;


            if (ultraGrid.DisplayLayout.Bands[0].Columns.Exists("edit"))
                ultraGrid.DisplayLayout.Bands[0].Columns["edit"].Hidden = true;

            bloquearIngresoHonorarios();
            //bindingNavigatorCountItem.Enabled = false;
            //bindingNavigatorMoveFirstItem.Enabled = false;
            //bindingNavigatorMoveLastItem.Enabled = false;
            //bindingNavigatorMoveNextItem.Enabled = false;
            //bindingNavigatorMovePreviousItem.Enabled = false;
            //bindingNavigatorPositionItem.Enabled = false;

            txtBuscarNumControl.Enabled = false;
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;
            btnImprimir.Visible = false;
            btnSalir.Visible = false;
            btnActualizar.Visible = false;
            //btnNuevo.Visible = false;
            //btnEliminar.Visible = false;
            btnRefresh.Enabled = false;
            btncerrar.Enabled = false;

            txtCodigoPaciente.ReadOnly = false;
            dateTimePickerFechaIngreso.Enabled = true;
            dateTimePickerFechaAlta.Enabled = true;
            dateTimePickerFechaFactura.Enabled = true;
            txtHabitacion.ReadOnly = false;
            txtPacienteCedula.ReadOnly = false;
            txtPacienteDireccion.ReadOnly = false;
            txtPacienteHCL.ReadOnly = false;
            txtPacienteNombre.ReadOnly = false;
            txtPacienteNombre2.ReadOnly = false;
            txtPacienteApellidoPaterno.ReadOnly = false;
            txtPacienteApellidoMaterno.ReadOnly = false;
            txtPacienteTelf.ReadOnly = false;

            if (accion == "nuevo")
            {
                //txtNumControl.ReadOnly = false;
                txtFactura.ReadOnly = false;
                ayudaPacientes.Visible = true;
                checkBoxReferido.Enabled = true;
            }
            else
            {
                if (honorariosGeneraronPagosRetenciones() == true)
                    checkBoxReferido.Enabled = false;
                else
                    checkBoxReferido.Enabled = true;
                txtNumControl.ReadOnly = true;
                txtFactura.ReadOnly = true;
                ayudaPacientes.Visible = false;
            }


            ayudaHabitacion.Visible = true;
            ayudaAtenciones.Visible = false;



        }

        public void habilitarCampos()
        {
            if (atenciones.Count > 0)
            {
                txtBuscarNumControl.Enabled = true;

                btnImprimir.Visible = true;
                btnActualizar.Visible = true;
                btnEliminar.Visible = true;
                btncerrar.Enabled = true;
                btnImprimir.Enabled = true;
                btnActualizar.Enabled = true;
                //btnEliminar.Enabled = true;



                //temporal.Columns["delete"].Visible = true;
                //temporal.Columns["edit"].Visible = true;

                if (ultraGrid.DisplayLayout.Bands[0].Columns.Exists("delete"))
                    ultraGrid.DisplayLayout.Bands[0].Columns["delete"].Hidden = false;


                if (ultraGrid.DisplayLayout.Bands[0].Columns.Exists("edit"))
                    ultraGrid.DisplayLayout.Bands[0].Columns["edit"].Hidden = false;

                desbloquearIngresoHonorarios();
                //bindingNavigatorCountItem.Enabled = true;
                //bindingNavigatorPositionItem.Enabled = true;
            }
            else
            {
                bloquearIngresoHonorarios();
                //bindingNavigatorCountItem.Text = "de 0";
                //bindingNavigatorPositionItem.Text = "0";
                //bindingNavigatorMoveFirstItem.Enabled = false;
                //bindingNavigatorMoveLastItem.Enabled = false;
                //bindingNavigatorMoveNextItem.Enabled = false;
                //bindingNavigatorMovePreviousItem.Enabled = false;
                txtBuscarNumControl.Enabled = false;

                btnImprimir.Visible = true;
                btnActualizar.Visible = true;
                //btnEliminar.Visible = true;

                btnActualizar.Enabled = false;
                //btnEliminar.Enabled = false;
                //btnImprimir.Enabled = false;

                //btnNuevo.Enabled = true;
                txtBuscarNumControl.Enabled = false;

                //temporal.Columns["delete"].Visible = false;
                //temporal.Columns["edit"].Visible = false;

                if (ultraGrid.DisplayLayout.Bands[0].Columns.Exists("delete"))
                    ultraGrid.DisplayLayout.Bands[0].Columns["delete"].Hidden = true;


                if (ultraGrid.DisplayLayout.Bands[0].Columns.Exists("edit"))
                    ultraGrid.DisplayLayout.Bands[0].Columns["edit"].Hidden = true;

            }

            //ayudaPacientes.Visible = false;
            ayudaHabitacion.Visible = false;
            ayudaAtenciones.Visible = true;
            checkBoxReferido.Enabled = false;
            txtCodigoPaciente.ReadOnly = true;
            txtPacienteCedula.ReadOnly = true;
            txtPacienteDireccion.ReadOnly = true;
            txtPacienteHCL.ReadOnly = true;
            txtPacienteNombre.ReadOnly = true;
            txtPacienteNombre2.ReadOnly = true;
            txtPacienteApellidoPaterno.ReadOnly = true;
            txtPacienteApellidoMaterno.ReadOnly = true;
            txtPacienteTelf.ReadOnly = true;
            txtHabitacion.ReadOnly = true;
            txtNumControl.ReadOnly = true;
            txtFactura.ReadOnly = true;
            dateTimePickerFechaIngreso.Enabled = false;
            dateTimePickerFechaAlta.Enabled = false;
            dateTimePickerFechaFactura.Enabled = false;

            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
            //btnNuevo.Visible = true;
            btnSalir.Visible = true;
            btnRefresh.Enabled = true;
        }

        private bool ValidarFormulario()
        {
            controlErrores.Clear();

            bool valido = true;
            if (ch_factura.Checked == true)
            {
                if (txtFacturaMedico.Text == "   -   -")
                {
                    AgregarError(txtFacturaMedico);
                    valido = false;
                }

            }
            else if (ch_vale.Checked == true)
            {
                if (txt_vale.Text == string.Empty)
                {
                    AgregarError(txt_vale);
                    valido = false;
                }

            }
            if (txtNombreMedico.Text == string.Empty)
            {
                AgregarError(txtNombreMedico);
                valido = false;
            }
            //if (formaPago == null)
            //{
            //    AgregarError(txt_nombre_pagos);
            //   valido = false;
            // }
            if (txtValorNeto.Text == string.Empty)
            {
                AgregarError(txtValorNeto);
                valido = false;
            }
            if (txtLote.Visible == true && txtLote.Text == string.Empty)
            {
                AgregarError(txtLote);
                valido = false;
            }

            if (lblComision.Text == string.Empty || lblAporte.Text == string.Empty)
            {
                lblComision.Text = "0";
                lblAporte.Text = "0";

                // AgregarError(cboFiltroFormaPago);
                //valido = false;
            }
            if (cmb_empresas.SelectedIndex <= -1)
            {
                AgregarError(cmb_empresas);
                valido = false;
            }
            if (cboFiltroFormaPago.SelectedIndex <= -1)
            {
                AgregarError(cboFiltroFormaPago);
                valido = false;
            }
            if (cboFiltroTipoFormaPago.SelectedIndex <= -1)
            {
                AgregarError(cboFiltroTipoFormaPago);
                valido = false;
            }
            if (txtObservacion.Text.Trim() == "")
            {
                txtObservacion.Text = ".";
            }
            if (Convert.ToDouble(txtValorTotal.Text) < 0)
            {
                controlErrores.SetError(txtValorTotal, "Valor a pagar no puede ser negativo");
                valido = false;
            }
            //HONORARIOS_MEDICOS facturasMedico = new HONORARIOS_MEDICOS();
            //facturasMedico = NegHonorariosMedicos.HonorarioFacturaMedico(txtFacturaMedico.Text.Replace("-", string.Empty).Trim(), Convert.ToInt16(txtCodMedico.Text.ToString()));
            //if(facturasMedico != null)
            //{
            //    if(facturasMedico.ATE_CODIGO != Convert.ToInt64(txtAtencion.Text))
            //    {
            //        controlErrores.SetError(txtFacturaMedico, "Esta factura ya le pertenece a otro paciente, por favor revisar");
            //        valido = false;
            //    }
            //}
            return valido;
        }

        private bool ValidarFormularioAtenciones()
        {
            erroresAtencion.Clear();

            bool valido = true;
            //if (txtCodigoPaciente.Text == string.Empty && paciente !=null)
            //{
            //    AgregarErrorAtenciones(txtCodigoPaciente);
            //    valido = false;
            //}
            if (txtPacienteNombre.Text == string.Empty)
            {
                AgregarErrorAtenciones(txtPacienteNombre);
                valido = false;
            }
            if (txtPacienteApellidoPaterno.Text == string.Empty)
            {
                AgregarErrorAtenciones(txtPacienteApellidoPaterno);
                valido = false;
            }
            if (txtPacienteDireccion.Text == string.Empty)
            {
                AgregarErrorAtenciones(txtPacienteDireccion);
                valido = false;
            }
            if (txtPacienteHCL.Text == string.Empty)
            {
                AgregarErrorAtenciones(txtPacienteHCL);
                valido = false;
            }

            if (txtPacienteTelf.Text == string.Empty)
            {
                AgregarErrorAtenciones(txtPacienteTelf);
                valido = false;
            }
            if (txtPacienteCedula.Text == string.Empty)
            {
                AgregarErrorAtenciones(txtPacienteCedula);
                valido = false;
            }
            if (habitacion == null)
            {
                AgregarErrorAtenciones(txtHabitacion);
                valido = false;
            }
            //if (txtNumControl.Text == string.Empty)
            //{
            //    AgregarErrorAtenciones(txtNumControl);
            //    valido = false;
            //}
            //if (txtFactura.Text == string.Empty)
            //{
            //    AgregarErrorAtenciones(txtFactura);
            //    valido = false;
            //}

            return valido;
        }

        private void AgregarError(Control control)
        {
            controlErrores.SetError(control, "Campo Requerido");
        }

        private void AgregarErrorAtenciones(Control control)
        {
            erroresAtencion.SetError(control, "Campo Requerido");
        }

        private void bloquearIngresoHonorarios()
        {
            //if (splitContainer.Panel2Collapsed == false)
            //{
            //    int height1 = groupBoxPaciente.Height + 10;
            //    splitContainer.Panel2Collapsed = true;

            //    splitContainer.SetBounds(splitContainer.Location.X, splitContainer.Location.Y, splitContainer.Size.Width, height1);
            //    ultraGrid.SetBounds(ultraGrid.Location.X, splitContainer.Location.Y + splitContainer.Size.Height + 2, ultraGrid.Size.Width, ultraGrid.Height + groupBoxMedico.Height);
            //}
            // groupBoxMedico.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
            //   groupBoxMedico.Enabled = false;
            //btnAniadir.Enabled = false;

        }

        private void desbloquearIngresoHonorarios()
        {
            //if (splitContainer.Panel2Collapsed == true)
            //{
            //    splitContainer.SetBounds(splitContainer.Location.X, splitContainer.Location.Y, splitContainer.Size.Width, 280);
            //    splitContainer.Panel2Collapsed = false;
            //    ultraGrid.SetBounds(ultraGrid.Location.X, splitContainer.Location.Y + splitContainer.Size.Height + 2, ultraGrid.Size.Width, ultraGrid.Height - groupBoxMedico.Height);
            //}
            grp2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            grp2.Enabled = true;
        }

        #endregion

        #region Botones de Navegacion

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            referenciaAtencion = referenciaAtencion + 1;
            atencionDtoActual = atenciones.ElementAt(referenciaAtencion);
            cargarAtencion(atencionDtoActual);
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            referenciaAtencion = referenciaAtencion - 1;
            atencionDtoActual = atenciones.ElementAt(referenciaAtencion);
            cargarAtencion(atencionDtoActual);
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            referenciaAtencion = atenciones.Count - 1;
            atencionDtoActual = atenciones.ElementAt(referenciaAtencion);
            cargarAtencion(atencionDtoActual);
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            referenciaAtencion = 0;
            atencionDtoActual = atenciones.ElementAt(referenciaAtencion);
            cargarAtencion(atencionDtoActual);
        }

        //private void bindingNavigatorPositionItem_Leave(object sender, EventArgs e)
        //{
        //    if (atenciones.Count > 0)
        //    {
        //        if (bindingNavigatorPositionItem.Text != string.Empty)
        //        {
        //            if (Convert.ToInt32(bindingNavigatorPositionItem.Text) > atenciones.Count)
        //                referenciaAtencion = atenciones.Count - 1;
        //            else if (Convert.ToInt32(bindingNavigatorPositionItem.Text) <= 0)
        //                referenciaAtencion = 0;
        //            else
        //                referenciaAtencion = Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1;

        //            atencionDtoActual = atenciones.ElementAt(referenciaAtencion);
        //        }

        //        cargarAtencion(atencionDtoActual);
        //    }

        //}

        //private void bindingNavigatorPositionItem_TextChanged(object sender, EventArgs e)
        //{
        //    if (Microsoft.VisualBasic.Information.IsNumeric(bindingNavigatorPositionItem.Text.ToString()) == false)
        //        bindingNavigatorPositionItem.Text = string.Empty;
        //}

        public void validarPosicion()
        {
            //if (posicion == 1)
            //{
            //    bindingNavigatorMovePreviousItem.Enabled = false;
            //    bindingNavigatorMoveFirstItem.Enabled = false;
            //}
            //if (posicion < atenciones.Count)
            //{
            //    bindingNavigatorMoveNextItem.Enabled = true;
            //    bindingNavigatorMoveLastItem.Enabled = true;
            //}
            //if (posicion == atenciones.Count)
            //{
            //    bindingNavigatorMoveNextItem.Enabled = false;
            //    bindingNavigatorMoveLastItem.Enabled = false;
            //}
            //if (posicion > 1)
            //{
            //    bindingNavigatorMovePreviousItem.Enabled = true;
            //    bindingNavigatorMoveFirstItem.Enabled = true;
            //}
            //if (medico == null)
            //{
            //    cboFiltroTipoFormaPago.SelectedItem = listaTipoPagos.FirstOrDefault(t => t.TIF_CODIGO == 0);
            //    //cboFiltroFormaPago.SelectedIndex = 0;
            //    chkHonorarioDirecto.Checked = false;
            //}
        }
        //private void bindingNavigatorPositionItem_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)(Keys.Enter))
        //    {
        //        if (bindingNavigatorPositionItem.Text != string.Empty)
        //        {

        //            if (Convert.ToInt32(bindingNavigatorPositionItem.Text) > atenciones.Count)
        //                referenciaAtencion = atenciones.Count - 1;
        //            else if (Convert.ToInt32(bindingNavigatorPositionItem.Text) <= 0)
        //                referenciaAtencion = 0;
        //            else
        //                referenciaAtencion = Convert.ToInt32(bindingNavigatorPositionItem.Text) - 1;

        //            atencionDtoActual = atenciones.ElementAt(referenciaAtencion);
        //            cargarAtencion(atencionDtoActual);

        //            validarPosicion();
        //        }
        //    }
        //}


        #endregion

        public string ate_codigo;
        private void txtLote_Leave(object sender, EventArgs e)
        {
            string nlote = txtLote.Text.Replace(" ", string.Empty);
            if (nlote != string.Empty)
                txtLote.Text = nlote;
        }

        private void atenciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportes frm = new frmReportes();
            frm.reporte = "rHonorariosAtencion";
            frm.campo1 = txtAtencion.Text.Trim();// atencionDtoActual.ATE_CODIGO.ToString();
            frm.Show();
        }

        private void honorariosDiariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ParametrosHonorarios frm = new frm_ParametrosHonorarios();
            frm.caja = caja.CAJ_CODIGO;
            frm.nomCaja = caja.CAJ_NOMBRE;
            frm.nomUsuario = His.Entidades.Clases.Sesion.nomUsuario;
            frm.Show();
        }

        private void txtPacienteCedula_Leave(object sender, EventArgs e)
        {
            if (txtPacienteCedula.Text != string.Empty)
            {
                if (NegValidaciones.esCedulaValida(txtPacienteCedula.Text.Trim()) != true)
                {
                    MessageBox.Show("Cédula Incorrecta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPacienteCedula.ForeColor = Color.Red;
                }
                else
                {
                    txtPacienteCedula.ForeColor = Color.Black;
                }
            }
            else
            {
                txtPacienteCedula.ForeColor = Color.Black;
            }
        }

        private void txtObservacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //txtValorNeto.Focus();
                SendKeys.SendWait("{TAB}");
            }

            if (e.KeyChar == (char)(Keys.F1))
            {

                //frmAyudaTarifarios form = new frmAyudaTarifarios();

            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //txtCodigoPaciente.Tag = false;
            //cargarDatos();
            cargarHonorariosMedicos();
        }

        private void txtPacienteTelf_Leave(object sender, EventArgs e)
        {
            if (txtPacienteTelf.Text.ToString() != "  -   -")
            {
                if (NegValidaciones.esTelefonoValido(txtPacienteTelf.Text.Replace("-", string.Empty).ToString()) == false)
                {
                    // MessageBox.Show("Numero de teléfono incorrecto","Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    txtPacienteTelf.Focus();
                }
            }
        }

        private void chkHonorarioDirecto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHonorarioDirecto.Checked == true)
            {

                TIPO_FORMA_PAGO tipoFormaPago = listaTipoPagos.FirstOrDefault(t => t.TIF_CODIGO == 1);
                cboFiltroTipoFormaPago.SelectedItem = tipoFormaPago;
                cboFiltroFormaPago.SelectedItem = listaPagos.FirstOrDefault(f => f.FOR_CODIGO == 1);
            }
            else
            {

            }
            calcularRetencionMedico();
            //calcularReferido();
        }

        private void chkHonorarioDirecto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                //txtFacturaMedico.Focus();
                SendKeys.SendWait("{TAB}");
            }
        }

        private void frm_IngresoHonorarios_KeyDown_2(object sender, KeyEventArgs e)
        {

        }

        private void cboFiltroFormaPago_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void txtPacienteHCL_Leave(object sender, EventArgs e)
        {
            if (btnGuardar.Enabled == true)
            {
                if (paciente == null)
                {
                    PACIENTES consulta = NegPacientes.RecuperarPacienteID(txtPacienteHCL.Text.Trim().ToString());
                    if (consulta != null)
                    {

                        var resp = MessageBox.Show("Esta historia clinica ya existe.\nDesea cargar el paciente con esta historia clinica?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (resp == DialogResult.No)
                        {
                            txtPacienteHCL.Focus();
                            existeHCL = true;
                        }
                        else
                        {
                            txtCodigoPaciente.Text = consulta.PAC_CODIGO.ToString();
                            existeHCL = false;
                        }
                    }
                }
                else
                {
                    existeHCL = false;
                }
            }
        }

        private void cboFiltroFormaPago_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void frm_IngresoHonorarios_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == GeneralPAR.TeclaNuevo)
            {
                //btnNuevo.PerformClick();
            }
            if (e.KeyCode == GeneralPAR.TeclaEditar)
            {
                btnActualizar.PerformClick();
            }
            if (e.KeyCode == GeneralPAR.TeclaEliminar)
            {
                btnEliminar.PerformClick();
            }
            if (e.KeyCode == GeneralPAR.TeclaGuardar)
            {
                btnGuardar.PerformClick();
            }
            if (e.KeyCode == GeneralPAR.TeclaCancelar)
            {
                btnCancelar.PerformClick();
            }
        }

        private void groupBoxMedico_Click(object sender, EventArgs e)
        {

        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            try
            {
                frm_AyudaPacien frm = new frm_AyudaPacien();
                frm.fechaFacturaHis = fecCaja.Value.ToString("dd/MM/yyyy");
                frm.campoPadre = txtCodigoPaciente;
                frm.campoAtencion = txtAtencion;
                frm.ate_num = txtnumatencion;
                frm.campoHabitacion = txtHabitacion;
                frm.campoReferido = checkBoxReferido;
                frm.txtfactura = txtFactura;
                frm.txtNumControl = txtNumControl;
                frm.fechafacturacion = dateTimePickerFechaFactura;
                frm.fechaingreso = dateTimePickerFechaIngreso;
                frm.fechaalta = dateTimePickerFechaAlta;
                frm.ShowDialog();

                btnImprimirHonor.Enabled = true;
                btncerrar.Enabled = true;

                DataTable ValidaCerrado = NegHonorariosMedicos.ValidaCerrados(Convert.ToInt64(txtAtencion.Text));
                DataTable FacturaEstado = new DataTable();
                FacturaEstado = NegHonorariosMedicos.FacturaEstado(txtFactura.Text.Replace("-", string.Empty).Trim());



                if (ValidaCerrado.Rows.Count > 0)
                {
                    if (Sesion.codDepartamento == 7)
                    {
                        if (ValidaCerrado.Rows[0][0].ToString() == "1")
                        {
                            btncerrar.Enabled = false;
                            ayudaMedicos.Enabled = false;
                            ultraButton2.Enabled = false;
                            ultraGrid.Enabled = false;
                        }
                        else
                        {
                            btncerrar.Enabled = true;
                            ayudaMedicos.Enabled = true;
                            ultraButton2.Enabled = true;
                            ultraGrid.Enabled = true;
                        }

                    }
                    else
                    {
                        btncerrar.Visible = false;
                        ayudaMedicos.Enabled = true;
                        ultraButton2.Enabled = true;
                        ultraGrid.Enabled = true;
                    }
                }
                if (FacturaEstado.Rows.Count > 0) //MUESTRA EL ESTADO DE LA FACTURA
                {
                    lblEstado.Visible = true;
                    lblEstado.Text = FacturaEstado.Rows[0][0].ToString();
                    if (FacturaEstado.Rows[0][1].ToString() != "0")
                        lblEstado.Text = lblEstado.Text + ": " + FacturaEstado.Rows[0][1].ToString();
                    else if (FacturaEstado.Rows[0][2].ToString() != "0")
                        lblEstado.Text = lblEstado.Text + ": " + FacturaEstado.Rows[0][2].ToString();
                }
                else
                {
                    lblEstado.Visible = false;
                }
                lblReferido.Visible = true;
                if (frm.campoReferido.Checked)
                {
                    lblReferido.Text = "HOSPITALARIO";
                }
                else
                {
                    lblReferido.Text = "PRIVADO";
                }

                // CARGA DATOS ATENCIONES

                if (txtAtencion.Text != "")
                {
                    DatosAseguradora = pagos.cargar_aseguradoras_empresas("1", txtAtencion.Text.Trim());
                    if (DatosAseguradora.Rows.Count > 0)
                    {

                        this.txt_codigoFormaspago.Text = DatosAseguradora.Rows[0]["FOR_CODIGO"].ToString();
                        this.txt_nombre_pagos.Text = DatosAseguradora.Rows[0]["FOR_DESCRIPCION"].ToString();
                        this.lblComision.Text = DatosAseguradora.Rows[0]["FOR_COMISION"].ToString();

                    }
                    cargarHonorariosMedicos();
                    grp2.Enabled = true;
                }

                if (txtFactura.Text != "   -   -")
                {
                    lblTotal.Text = "Total: $";
                    DataTable dts_cuenta = new DataTable();
                    dts_cuenta = obj_atencion.CuentaPaciente(Convert.ToInt64(txtAtencion.Text.Trim()));
                    decimal aux = Math.Round(Convert.ToDecimal(dts_cuenta.Rows[0][0].ToString()), 2);
                    lblTotal.Text += aux;
                    lblTotal.Visible = true;
                }
                else
                    lblTotal.Visible = false;


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.InnerException.Message);
            }
        }
        public void ValidaGuardar()
        {
            List<PERFILES> perfilUsuario = new NegPerfil().RecuperarPerfil(Sesion.codUsuario);
            foreach (var item in perfilUsuario)
            {
                List<ACCESO_OPCIONES> accop = NegUtilitarios.ListaAccesoOpcionesPorPerfil(item.ID_PERFIL, 2);
                foreach (var items in accop)
                {
                    if (items.ID_ACCESO == 22210)// se cambia del perfil  24 a opcion 22210// Mario Valencia 22/01/2023 // cambio en seguridades.
                    {
                        //Verificar si ya no esta generado el asiento
                        foreach (UltraGridRow i in ultraGrid.Rows)
                        {
                            if (!NegHonorariosMedicos.AsientoGenerado(Convert.ToInt64(i.Cells["COD"].Value)))//valida si no tiene honorarios que no han sido generados el asiento
                            {
                                btnGuardar.Enabled = true;
                                btnImprimir.Enabled = true;
                                lblFechaAsiento.Visible = true;
                                dtpFechaAsiento.Visible = true;
                                btnEliminar.Enabled = true;
                                break;
                            }
                            else
                            {
                                btnGuardar.Enabled = false; //encontro que todos han sido generados el asiento asi que bloqueo el boton.
                                btnImprimir.Enabled = true;
                                lblFechaAsiento.Visible = false;
                                dtpFechaAsiento.Visible = false;
                                btnEliminar.Enabled = true;
                            }
                        }
                    }
                    if (items.ID_ACCESO == 22220)
                    {
                        chkComision.Visible = true;
                    }
                }
            }
        }

        private void ayudaHabitacion_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Ayudas lista = new frm_Ayudas(NegHabitaciones.listaHabitaciones());
                lista.tabla = "HABITACIONES";
                lista.campoPadre = txtHabitacion;
                lista.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void ayudaMedicos_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MedicoCP)
                {
                    frm_Ayudas lista = new frm_Ayudas(NegMedicos.listaMedicos());
                    lista.tabla = "MEDICOS";
                    lista.campoPadre = txtCodMedico;
                    lista.ShowDialog();
                    txtFacturaMedico.Focus();
                    calcularRetencionMedico();
                    lblAporte.Visible = false;
                    lblComision.Visible = false;
                    numVale = NegHonorariosMedicos.NumVale();
                    if (ch_vale.Checked)
                        txt_vale.Text = numVale;
                }
                else
                {
                    if (MessageBox.Show("¿Está seguro de cambiar el médico: " + txtNombreMedico.Text + " por otro?",
                         "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        frm_Ayudas lista = new frm_Ayudas(NegMedicos.listaMedicos());
                        lista.tabla = "MEDICOS";
                        lista.campoPadre = txtCodMedico;
                        lista.ShowDialog();
                        txtFacturaMedico.Focus();
                        calcularRetencionMedico();
                        lblAporte.Visible = false;
                        lblComision.Visible = false;
                        Calculos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void ultraGrid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {

                Infragistics.Win.UltraWinGrid.UltraGridBand bandUno = ultraGrid.DisplayLayout.Bands[0];

                ultraGrid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                ultraGrid.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                ultraGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                ultraGrid.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;

                ultraGrid.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;


                if (!ultraGrid.DisplayLayout.Bands[0].Columns.Exists("delete"))
                {
                    UltraGridColumn columna = new UltraGridColumn("");

                    bandUno.Columns.Add("delete", "");
                    bandUno.Columns["delete"].DataType = typeof(string);
                    bandUno.Columns["delete"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
                    bandUno.Columns["delete"].ButtonDisplayStyle = ButtonDisplayStyle.Always;
                }

                if (!ultraGrid.DisplayLayout.Bands[0].Columns.Exists("edit"))
                {
                    bandUno.Columns.Add("edit", "");
                    bandUno.Columns["edit"].DataType = typeof(string);
                    bandUno.Columns["edit"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
                    bandUno.Columns["edit"].ButtonDisplayStyle = ButtonDisplayStyle.Always;
                }

                bandUno.Columns["delete"].Width = 20;
                bandUno.Columns["edit"].Width = 20;

                bandUno.Columns["delete"].MaxWidth = 20;
                bandUno.Columns["delete"].MinWidth = 20;


                bandUno.Columns["edit"].MaxWidth = 20;
                bandUno.Columns["edit"].MinWidth = 20;


                bandUno.Columns["delete"].Header.VisiblePosition = 0;
                bandUno.Columns["edit"].Header.VisiblePosition = 1;
                //bandUno.Columns["COD"].Header.VisiblePosition = 2;
                bandUno.Columns["MEDICO"].Header.VisiblePosition = 2;
                bandUno.Columns["FACTURA"].Header.VisiblePosition = 3;
                bandUno.Columns["FECHA"].Header.VisiblePosition = 4;
                bandUno.Columns["FORMAPAGO"].Header.VisiblePosition = 5;
                bandUno.Columns["F_PAGO"].Header.VisiblePosition = 6;
                bandUno.Columns["VALOR_NETO"].Header.VisiblePosition = 7;
                bandUno.Columns["RETENCION"].Header.VisiblePosition = 8;
                bandUno.Columns["COMISION_CLINICA"].Header.VisiblePosition = 9;
                bandUno.Columns["APORTE_LLAMADA"].Header.VisiblePosition = 10;
                bandUno.Columns["VALOR_TOTAL"].Header.VisiblePosition = 11;
                bandUno.Columns["vale"].Header.VisiblePosition = 12;
                bandUno.Columns["MED_CODIGO"].Hidden = true;
                bandUno.Columns["COD"].Hidden = true;

                //DataGridViewButtonCell

                for (int i = 0; i < ultraGrid.Rows.Count; i++)
                {
                    //ultraGrid.Rows[i].Cells["delete"].Appearance.Image = Archivo.btnEliminar16;
                    ultraGrid.Rows[i].Cells["delete"].ButtonAppearance.Image = Archivo.ButtonDelete;
                    ultraGrid.Rows[i].Cells["edit"].ButtonAppearance.Image = Archivo.ButtonRefresh;

                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ultraGrid_ClickCellButton(object sender, CellEventArgs e)
        {
            try
            {
                if (e.Cell.Column == ultraGrid.DisplayLayout.Bands[0].Columns["edit"] || e.Cell.Column == ultraGrid.DisplayLayout.Bands[0].Columns["delete"])
                {
                    ///alex
                   // MessageBox.Show(ultraGrid.Rows[e.Cell.Row.Index].Cells["HOM_CODIGO"].Value.ToString());
                    DataTable auxx = NegDietetica.getDataTable("HMGeneradoAsiento", ultraGrid.Rows[e.Cell.Row.Index].Cells["COD"].Value.ToString());
                    if (auxx.Rows[0][0].ToString().Trim() == "1")
                    {
                        MessageBox.Show("Ya se ha generado un asiento contable del honorario, por lo que no es posible modificar o eliminar.", "HIS3000");
                        return;
                    }

                    ////

                    int codHonorario = Convert.ToInt32(ultraGrid.Rows[e.Cell.Row.Index].Cells["COD"].Value);
                    //MessageBox.Show(codHonorario.ToString());
                    HONORARIOS_MEDICOS honorarioaBorrar = new HONORARIOS_MEDICOS();
                    honorarioaBorrar = new NegHonorariosMedicos().RecuperaHonorariosMedicosID(codHonorario);

                    DataTable DAdicionales = NegHonorariosMedicos.HMDatosAdiciones(codHonorario);
                    //if (honorarioaBorrar.HOM_VALOR_PAGADO == 0 && DAdicionales.Rows[0][6].ToString() != "1")
                    if (honorarioaBorrar.HOM_VALOR_PAGADO == 0)
                    {
                        if (e.Cell.Column == ultraGrid.DisplayLayout.Bands[0].Columns["delete"])
                        {
                            var op = MessageBox.Show("Seguro que desea eliminar este registro?", "HIS3000", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (op == DialogResult.OK)
                            {
                                if (honorarioaBorrar.FOR_CODIGO.ToString() == "267")
                                {
                                    string numrec = NegHonorariosMedicos.RecuperarNUMREC(Convert.ToInt32(honorarioaBorrar.HOM_CODIGO));
                                    if (numrec != "")
                                        NegHonorariosMedicos.ReponerAnticipoSic(numrec, Convert.ToDouble(honorarioaBorrar.HOM_VALOR_NETO.ToString()));
                                }
                                eliminarHonorario(honorarioaBorrar.HOM_CODIGO);

                                NegHonorariosMedicos.CrearHonorarioAuditoria(honorarioaBorrar, (bool)ultraGrid.ActiveRow.Cells["POR_FUERA"].Value, false, "ELIMINADO", numCaja, (double)honorarioaBorrar.HOM_VALOR_NETO, Convert.ToInt64(ultraGrid.ActiveRow.Cells["MED_CODIGO"].Value));
                                NegHonorariosMedicos.deleteHMDatosAdicionales(Convert.ToInt32(honorarioaBorrar.HOM_CODIGO));
                                cargarHonorariosMedicos();
                            }
                        }
                        if (e.Cell.Column == ultraGrid.DisplayLayout.Bands[0].Columns["edit"])
                        {
                            var op = MessageBox.Show("¿Seguro que desea editar este registro?", "HIS3000", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                            if (op == DialogResult.OK)
                            {
                                //if (honorarioaBorrar.FOR_CODIGO.ToString() == "267")
                                //{
                                //    string numrec = NegHonorariosMedicos.RecuperarNUMREC(Convert.ToInt32(honorarioaBorrar.HOM_CODIGO));
                                //    if (numrec != "")
                                //        NegHonorariosMedicos.ReponerAnticipoSic(numrec, Convert.ToDouble(honorarioaBorrar.HOM_VALOR_NETO.ToString()));
                                //}
                                Editar = true;
                                TXTHOMCOD.Text = honorarioaBorrar.HOM_CODIGO.ToString(); //ALEX3030
                                cargarHonorario(honorarioaBorrar.HOM_CODIGO);
                                loadHMDatosAdicionles();
                                //NegHonorariosMedicos.deleteHMDatosAdicionales(Convert.ToInt32(TXTHOMCOD.Text.Trim())); ELIMINA DATOS ADICIONALES
                                //eliminarHonorario(honorarioaBorrar.HOM_CODIGO); //ELIMINA HONORARIOS_MEDICOS
                                controlErrores.Clear();
                                cargarHonorariosMedicos();
                                Calculos();
                                foreach (UltraGridRow row in ultraGrid.Rows)
                                {
                                    if (row.Cells["COD"].Value.ToString() == honorarioaBorrar.HOM_CODIGO.ToString())
                                    {
                                        row.Hidden = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("El honorario no puede ser modificado ya que ya se han generado pagos ó arqueos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ec)
            {
                MessageBox.Show("Error al modificar el honorario: " + ec.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool Editar = false;
        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == (GeneralPAR.TeclaTabular))
            {
                e.Handled = true;
                //SendKeys.SendWait("{TAB}");
                if (txtLote.Visible == true)
                    txtLote.Focus();
                else
                    txtObservacion.Focus();
            }
        }

        //private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    //TIPO_FORMA_PAGO tfp = (TIPO_FORMA_PAGO)cboFiltroTipoFormaPago.SelectedItem;

        //    //if (tfp.TIF_CODIGO != 0)
        //    //{
        //    //    tipoPago = (TIPO_FORMA_PAGO)cboFiltroTipoFormaPago.SelectedItem;
        //    //    listaPagos = NegFormaPago.RecuperaFormaPago(tfp.TIF_CODIGO);

        //    //    FORMA_PAGO nuevo = new FORMA_PAGO();
        //    //    nuevo.FOR_CODIGO = 0;
        //    //    nuevo.FOR_DESCRIPCION = "TODAS";
        //    //    listaPagos.Insert(0, nuevo);

        //    //    cboFiltroFormaPago.DataSource = listaPagos;
        //    //    cboFiltroFormaPago.ValueMember = "FOR_CODIGO";
        //    //    cboFiltroFormaPago.DisplayMember = "FOR_DESCRIPCION";
        //    //    cboFiltroFormaPago.AutoCompleteSource = AutoCompleteSource.ListItems;
        //    //    cboFiltroFormaPago.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //    //}
        //    //else
        //    //{
        //    //    tipoPago = null;
        //    //    cboFiltroFormaPago.DataSource = null;
        //    //}
        //}

        private void ultraButton2_Click(object sender, EventArgs e)
        {

        }
        public void agregoHonorario()
        {
            //SI ES ANTICIPO
            if (cboFiltroFormaPago.Text == "ANTICIPOS")
            {
                if (Convert.ToDouble(valorAnticipo) == Convert.ToDouble(txtValorNeto.Text))
                {
                    NegHonorariosMedicos.HonorarioAnticipoSic(1, Convert.ToDouble(valorAnticipo), codAnticipo);
                    if (guardarHonorario())
                    {
                        cargarHonorariosMedicos();
                        saveHMDatosAdicionales();
                        numrec = ""; //LIMPI VARIABLE DESPUES DE GUARDAR.
                        limpiarCampos();
                        txtCodMedico.Focus();
                        apcActivado = false;
                        checkBox1.Checked = false;
                        chkComision.Checked = false;
                    }
                }
                else if (Convert.ToDouble(txtValorNeto.Text) < Convert.ToDouble(valorAnticipo))
                {
                    NegHonorariosMedicos.HonorarioAnticipoSic(0, Convert.ToDouble(txtValorNeto.Text), codAnticipo);
                    if (guardarHonorario())
                    {
                        cargarHonorariosMedicos();
                        saveHMDatosAdicionales();
                        numrec = ""; //LIMPI VARIABLE DESPUES DE GUARDAR.
                        limpiarCampos();
                        txtCodMedico.Focus();
                        apcActivado = false;
                        checkBox1.Checked = false;
                        chkComision.Checked = false;

                    }
                }
                else if (Convert.ToDouble(txtValorNeto.Text) > Convert.ToDouble(valorAnticipo))
                {
                    MessageBox.Show("Valor de Anticipo exceder el valor de : " + valorAnticipo, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            else
            {
                if (guardarHonorario())
                {
                    cargarHonorariosMedicos();
                    saveHMDatosAdicionales();
                    limpiarCampos();
                    txtCodMedico.Focus();
                    apcActivado = false;
                    checkBox1.Checked = false;
                    chkComision.Checked = false;

                }
            }
        }
        private void ultraButton2_Click_1(object sender, EventArgs e)
        {
            if (ValidarFormulario() == true)
            {
                if (ch_factura.Checked == true)
                {
                    if (verificarHonorario(txtFacturaMedico.Text.Replace("-", string.Empty).Trim(), Convert.ToInt16(txtCodMedico.Text.ToString()), Convert.ToInt32(txtAtencion.Text.Trim())) == false)
                    {
                        //SI ES ANTICIPO
                        if (cboFiltroFormaPago.Text == "ANTICIPOS" && chkXFuera.Checked)
                        {
                            DataTable anticipoValida = NegFactura.ValidaAnticipos(Convert.ToInt64(codAnticipo));
                            if(anticipoValida == null)
                            {
                                MessageBox.Show("No a seleccionado el anticipo desde el cual va liquidar el honorario del medico", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                                return;
                            }
                            if (Convert.ToDouble(anticipoValida.Rows[0][0].ToString()) < Convert.ToDouble(txtValorNeto.Text.Trim()))
                            {
                                MessageBox.Show("Por favor volver a verificar saldo actual del anticipo.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (Convert.ToDouble(valorAnticipo) == Convert.ToDouble(txtValorNeto.Text))
                            {
                                NegHonorariosMedicos.HonorarioAnticipoSic(1, Convert.ToDouble(valorAnticipo), codAnticipo);
                                if (guardarHonorario())
                                {
                                    cargarHonorariosMedicos();
                                    saveHMDatosAdicionales();
                                    numrec = ""; //LIMPI VARIABLE DESPUES DE GUARDAR.
                                    limpiarCampos();
                                    txtCodMedico.Focus();
                                    apcActivado = false;
                                    checkBox1.Checked = false;

                                }
                            }
                            else if (Convert.ToDouble(txtValorNeto.Text) < Convert.ToDouble(valorAnticipo))
                            {
                                NegHonorariosMedicos.HonorarioAnticipoSic(0, Convert.ToDouble(txtValorNeto.Text), codAnticipo);
                                if (guardarHonorario())
                                {
                                    cargarHonorariosMedicos();
                                    saveHMDatosAdicionales();
                                    numrec = ""; //LIMPI VARIABLE DESPUES DE GUARDAR.
                                    limpiarCampos();
                                    txtCodMedico.Focus();
                                    apcActivado = false;
                                    checkBox1.Checked = false;
                                    chkComision.Checked = false;
                                }
                            }
                            else if (Convert.ToDouble(txtValorNeto.Text) > Convert.ToDouble(valorAnticipo))
                            {
                                MessageBox.Show("Valor de Anticipo exceder el valor de : " + valorAnticipo, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            if (guardarHonorario())
                            {
                                cargarHonorariosMedicos();
                                saveHMDatosAdicionales();
                                numrec = ""; //LIMPI VARIABLE DESPUES DE GUARDAR.
                                limpiarCampos();
                                txtCodMedico.Focus();
                                apcActivado = false;
                                checkBox1.Checked = false;
                                chkComision.Checked = false;
                            }
                        }

                    }
                    else
                    {
                        if (!Editar)
                        {
                            bool existe = NegHonorariosMedicos.EsOtraFormaPago(Convert.ToInt64(txtAtencion.Text), Convert.ToInt32(cboFiltroFormaPago.SelectedValue.ToString()), Convert.ToInt32(txtCodMedico.Text));
                            if (existe)
                            {
                                if (cboFiltroFormaPago.Text == "ANTICIPOS")
                                {
                                    bool fpAnticipo = NegHonorariosMedicos.FpAnticipo(Convert.ToInt64(txtAtencion.Text), Convert.ToInt32(txtCodMedico.Text), codAnticipo);
                                    if (fpAnticipo)
                                    {
                                        MessageBox.Show("Esta Nº factura y forma de pago ya fue ingresada..!");
                                        txtFacturaMedico.Text = string.Empty;
                                        txtFacturaMedico.Focus();
                                        return;
                                    }
                                    else
                                    {
                                        agregoHonorario();
                                    }
                                }
                                else if (Convert.ToInt32(cboFiltroFormaPago.SelectedValue.ToString()) == 4) //valida las tarjetas
                                {
                                    bool fpTarjeta = NegHonorariosMedicos.FpTarjeta(Convert.ToInt64(txtAtencion.Text), Convert.ToInt32(txtCodMedico.Text), txtLote.Text);
                                    if (fpTarjeta)
                                    {
                                        MessageBox.Show("Esta Nº factura y forma de pago ya fue ingresada..!");
                                        txtFacturaMedico.Text = string.Empty;
                                        txtFacturaMedico.Focus();
                                        return;
                                    }
                                    else
                                    {
                                        agregoHonorario();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Esta Nº factura y forma de pago ya fue ingresada..!");
                                    txtFacturaMedico.Text = string.Empty;
                                    txtFacturaMedico.Focus();
                                }
                            }
                            else
                            {
                                //SI ES ANTICIPO
                                if (cboFiltroFormaPago.Text == "ANTICIPOS")
                                {
                                    if (Convert.ToDouble(valorAnticipo) == Convert.ToDouble(txtValorNeto.Text))
                                    {
                                        NegHonorariosMedicos.HonorarioAnticipoSic(1, Convert.ToDouble(valorAnticipo), codAnticipo);
                                        if (guardarHonorario())
                                        {
                                            cargarHonorariosMedicos();
                                            saveHMDatosAdicionales();
                                            numrec = ""; //LIMPI VARIABLE DESPUES DE GUARDAR.
                                            limpiarCampos();
                                            txtCodMedico.Focus();
                                            apcActivado = false;
                                            checkBox1.Checked = false;
                                            chkComision.Checked = false;
                                        }
                                    }
                                    else if (Convert.ToDouble(txtValorNeto.Text) < Convert.ToDouble(valorAnticipo))
                                    {
                                        NegHonorariosMedicos.HonorarioAnticipoSic(0, Convert.ToDouble(txtValorNeto.Text), codAnticipo);
                                        if (guardarHonorario())
                                        {
                                            cargarHonorariosMedicos();
                                            saveHMDatosAdicionales();
                                            numrec = ""; //LIMPI VARIABLE DESPUES DE GUARDAR.
                                            limpiarCampos();
                                            txtCodMedico.Focus();
                                            apcActivado = false;
                                            checkBox1.Checked = false;
                                            chkComision.Checked = false;

                                        }
                                    }
                                    else if (Convert.ToDouble(txtValorNeto.Text) > Convert.ToDouble(valorAnticipo))
                                    {
                                        MessageBox.Show("Valor de Anticipo exceder el valor de : " + valorAnticipo, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }

                                }
                                else
                                {
                                    if (guardarHonorario())
                                    {
                                        cargarHonorariosMedicos();
                                        saveHMDatosAdicionales();
                                        limpiarCampos();
                                        txtCodMedico.Focus();
                                        apcActivado = false;
                                        checkBox1.Checked = false;
                                        chkComision.Checked = false;

                                    }
                                }
                            }
                        }
                        else
                        {
                            if (cboFiltroFormaPago.Text == "ANTICIPOS")
                            {
                                if (Convert.ToDouble(valorAnticipo) == Convert.ToDouble(txtValorNeto.Text))
                                {
                                    NegHonorariosMedicos.HonorarioAnticipoSic(1, Convert.ToDouble(valorAnticipo), codAnticipo);
                                    if (guardarHonorario())
                                    {
                                        cargarHonorariosMedicos();
                                        saveHMDatosAdicionales();
                                        numrec = ""; //LIMPI VARIABLE DESPUES DE GUARDAR.
                                        limpiarCampos();
                                        txtCodMedico.Focus();
                                        apcActivado = false;
                                        checkBox1.Checked = false;
                                        chkComision.Checked = false;
                                    }
                                }
                                else if (Convert.ToDouble(txtValorNeto.Text) < Convert.ToDouble(valorAnticipo))
                                {
                                    NegHonorariosMedicos.HonorarioAnticipoSic(0, Convert.ToDouble(txtValorNeto.Text), codAnticipo);
                                    if (guardarHonorario())
                                    {
                                        cargarHonorariosMedicos();
                                        saveHMDatosAdicionales();
                                        numrec = ""; //LIMPI VARIABLE DESPUES DE GUARDAR.
                                        limpiarCampos();
                                        txtCodMedico.Focus();
                                        apcActivado = false;
                                        checkBox1.Checked = false;
                                        chkComision.Checked = false;
                                    }
                                }
                                else if (Convert.ToDouble(txtValorNeto.Text) > Convert.ToDouble(valorAnticipo))
                                {
                                    MessageBox.Show("Valor de Anticipo exceder el valor de : " + valorAnticipo, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                            }
                            else
                            {
                                if (guardarHonorario())
                                {
                                    cargarHonorariosMedicos();
                                    saveHMDatosAdicionales();
                                    limpiarCampos();
                                    txtCodMedico.Focus();
                                    apcActivado = false;
                                    checkBox1.Checked = false;
                                    chkComision.Checked = false;

                                }
                            }
                        }

                    }
                }
                else if (ch_vale.Checked == true)
                {
                    if (verificarHonorario(txt_vale.Text, Convert.ToInt16(txtCodMedico.Text.ToString()), Convert.ToInt32(txtAtencion.Text.Trim())) == false)
                    {
                        //SI ES ANTICIPO
                        if (cboFiltroFormaPago.Text == "ANTICIPOS" && chkXFuera.Checked)
                        {
                            if (Convert.ToDouble(valorAnticipo) == Convert.ToDouble(txtValorNeto.Text))
                            {
                                NegHonorariosMedicos.HonorarioAnticipoSic(1, Convert.ToDouble(valorAnticipo), codAnticipo);
                                if (guardarHonorario())
                                {
                                    cargarHonorariosMedicos();
                                    saveHMDatosAdicionales();
                                    numrec = ""; //LIMPI VARIABLE DESPUES DE GUARDAR.
                                    limpiarCampos();
                                    txtCodMedico.Focus();
                                    apcActivado = false;
                                    checkBox1.Checked = false;
                                    chkComision.Checked = false;

                                }
                            }
                            else if (Convert.ToDouble(txtValorNeto.Text) < Convert.ToDouble(valorAnticipo))
                            {
                                NegHonorariosMedicos.HonorarioAnticipoSic(0, Convert.ToDouble(txtValorNeto.Text), codAnticipo);
                                if (guardarHonorario())
                                {
                                    cargarHonorariosMedicos();
                                    saveHMDatosAdicionales();
                                    numrec = ""; //LIMPI VARIABLE DESPUES DE GUARDAR.
                                    limpiarCampos();
                                    txtCodMedico.Focus();
                                    apcActivado = false;
                                    checkBox1.Checked = false;
                                    chkComision.Checked = false;

                                }
                            }
                            else if (Convert.ToDouble(txtValorNeto.Text) > Convert.ToDouble(valorAnticipo))
                            {
                                MessageBox.Show("Valor de Anticipo exceder el valor de : " + valorAnticipo, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                        }
                        else
                        {
                            if (guardarHonorario())
                            {
                                cargarHonorariosMedicos();
                                saveHMDatosAdicionales();
                                limpiarCampos();
                                txtCodMedico.Focus();
                                apcActivado = false;
                                checkBox1.Checked = false;
                                chkComision.Checked = false;

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Este Vale ya fue ingresado..!");
                        txtFacturaMedico.Text = string.Empty;
                        txtFacturaMedico.Focus();
                    }
                }
                cargarHonorariosMedicos();
                Editar = false;
                MedicoCP = false;
                cargar_combo_empresas();
            }
            else
            {
                MessageBox.Show("Datos del honorario incompletos");
            }
        }

        private void ultraButton2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ultraButton2_Click_1(null, null);
            }
        }
        public string numVale = "";
        private void frm_IngresoHonorarios_Load(object sender, EventArgs e)
        {
            cargar_combo_empresas();

            if (Sesion.codDepartamento == 17 || Sesion.codDepartamento == 1)
            {
                chkComision.Visible = true;
            }
        }
        public void cargar_combo_empresas()
        {

            DataSet dts_combos = new DataSet();
            dts_combos = obj_atencion.combo_tipo_empresa();

            cmb_empresas.DataSource = dts_combos.Tables[0];
            cmb_empresas.DisplayMember = dts_combos.Tables[0].Columns["desclas"].ColumnName.ToString();
            cmb_empresas.ValueMember = dts_combos.Tables[0].Columns["codclas"].ColumnName.ToString();

        }

        private void ayudaAtenciones_Click(object sender, EventArgs e)
        {

        }



        private void chc_fecha_CheckedChanged(object sender, EventArgs e)
        {
            if (chc_fecha.Checked == true)
            {
                dateTimePickerFechaFactura.Enabled = true;
                dateTimePickerFechaIngreso.Enabled = true;
                dateTimePickerFechaAlta.Enabled = true;
            }
            if (chc_fecha.Checked == false)
            {
                dateTimePickerFechaFactura.Enabled = false;
                dateTimePickerFechaIngreso.Enabled = false;
                dateTimePickerFechaAlta.Enabled = false;
            }
        }

        private void ch_factura_CheckedChanged(object sender, EventArgs e)
        {
            if (ch_factura.Checked == true)
            {
                if (chkXFuera.Checked)//ES FACTURA Y ES HONORARIO X FUERA
                {
                    txtAutSri.Visible = false;
                    label20.Visible = false;
                    lblCaducidad.Visible = false;
                    dtpCaducidad.Visible = false;
                }
                else //ES FACTURA Y ES HONORARIO POR DENTRO
                {
                    txtAutSri.Visible = true;
                    label20.Visible = true;
                    lblCaducidad.Visible = true;
                    dtpCaducidad.Visible = true;
                }
                txtFacturaMedico.Enabled = true;
                txt_vale.Enabled = false;
                ch_vale.Checked = false;
            }

        }

        private void ch_vale_CheckedChanged(object sender, EventArgs e)
        {
            if (ch_vale.Checked == true)
            {
                txt_vale.Text = numVale;
                label20.Visible = false;
                txtAutSri.Visible = false;
                txtFacturaMedico.Enabled = false;
                txt_vale.Enabled = true;
                ch_factura.Checked = false;
                dtpCaducidad.Visible = false;
                lblCaducidad.Visible = false;
            }
        }

        private void ultraButton2_Click_2(object sender, EventArgs e)
        {
            if (txtCodigoPaciente.Text != string.Empty)
            {
                try
                {
                    frm_ayudaFormaspago lista = new frm_ayudaFormaspago(tipo_empresa, txtAtencion.Text.Trim());
                    lista.txtdescripcion = txt_nombre_pagos;
                    lista.txtcodigo = txt_codigoFormaspago;
                    lista.ShowDialog();
                    txtObservacion.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }
        }

        private void txt_codigoFormaspago_TextChanged(object sender, EventArgs e)
        { //cambiar  el metodo  de  busqueda 
            //if (Microsoft.VisualBasic.Information.IsNumeric(txt_codigoFormaspago.Text) == false)
            //    txtCodMedico.Text = string.Empty;

            //if (tipo_empresa.Trim() == "3")
            //{
            //    formaPago = NegFormaPago.RecuperaFormaPagoID(Convert.ToInt16(txt_codigoFormaspago.Text));
            //    cargarComision();
            //}
            //if (tipo_empresa.Trim() == "2")
            //{
            //    formaPago = NegFormaPago.RecuperaFormaPagoID(Convert.ToInt16(txt_codigoFormaspago.Text));
            //    cargarComision();
            //}
            //if (tipo_empresa.Trim() == "1")
            //{
            //    formaPago = NegFormaPago.RecuperaFormaPagoID(Convert.ToInt16(txt_codigoFormaspago.Text));
            //    cargarComision();
            //}

        }

        private void cmb_empresas_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmb_empresas.Text != "")
            {
                //if (cmb_empresas.SelectedValue.ToString().Trim() == "3")
                //{
                //    btn_ayuda_pagos.Enabled = true;
                //    txt_nombre_pagos.Enabled = true;
                //    //txt_nombre_pagos.Text = string.Empty;
                //    //txt_codigoFormaspago.Text = string.Empty;
                //    tipo_empresa = "3";

                //}
                //if (cmb_empresas.SelectedValue.ToString().Trim() == "2")
                //{
                //    btn_ayuda_pagos.Enabled = true;
                //    txt_nombre_pagos.Enabled = true;
                //    //txt_nombre_pagos.Text = string.Empty;
                //    //txt_codigoFormaspago.Text = string.Empty;
                //    tipo_empresa = "2";

                //}
                //if (cmb_empresas.SelectedValue.ToString().Trim() == "1")
                //{
                //    btn_ayuda_pagos.Enabled = true;
                //    txt_nombre_pagos.Enabled = true;
                //    //txt_nombre_pagos.Text = string.Empty;
                //    //txt_codigoFormaspago.Text = string.Empty;
                //    tipo_empresa = "1";
                //}

            }
        }

        private void txt_nombre_pagos_TextChanged(object sender, EventArgs e)
        {
            //if (tipo_empresa.Trim() == "3")
            //{
            //    formaPago = NegFormaPago.RecuperaFormaPagoID(Convert.ToInt16(txt_codigoFormaspago.Text));
            //    cargarComision();
            //}
            //if (tipo_empresa.Trim() == "2")
            //{
            //    formaPago = NegFormaPago.RecuperaFormaPagoID(Convert.ToInt16(txt_codigoFormaspago.Text));
            //    cargarComision();
            //}
            //if (tipo_empresa.Trim() == "1")
            //{
            //    formaPago = NegFormaPago.RecuperaFormaPagoID(Convert.ToInt16(txt_codigoFormaspago.Text));
            //    cargarComision();
            //}

        }

        private void txt_nombre_pagos_Leave(object sender, EventArgs e)
        {

        }

        private void txt_codigoFormaspago_Leave(object sender, EventArgs e)
        {
            txt_codigoFormaspago.Focus();
        }

        private void txt_nombre_pagos_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtCodigoPaciente.Text != string.Empty)
            {
                try
                {
                    if (e.KeyCode == GeneralPAR.TeclaAyuda)
                    {

                        frm_ayudaFormaspago lista = new frm_ayudaFormaspago(tipo_empresa, txtAtencion.Text.Trim());
                        lista.txtdescripcion = txt_nombre_pagos;
                        lista.txtcodigo = txt_codigoFormaspago;
                        lista.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.Message);
                }
            }
        }

        private void txt_vale_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                cmb_empresas.Focus();

            }
        }

        private void cmb_empresas_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txt_nombre_pagos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtObservacion.Focus();

            }
        }





        //private void cmb_empresas_SelectionChangeCommitted(object sender, EventArgs e)
        //{
        //    //           cargarTipoFormaPago(cboFiltroTipoFormaPago);
        //}

        private void btnImprimirHonor_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DatosHonorario = new DataTable();
                DatosHonorario = NegHonorariosMedicos.DatosReporte(Convert.ToInt64(txtAtencion.Text.Trim()));

                if (DatosHonorario.Rows.Count > 0)
                {
                    DSHonorarioDetalle honorario = new DSHonorarioDetalle();
                    DataRow drhonorario;
                    string usuarios = "";
                    foreach (DataRow item in DatosHonorario.Rows)
                    {
                        drhonorario = honorario.Tables["Honorario"].NewRow();
                        drhonorario["Paciente"] = item[0].ToString();
                        drhonorario["Logo"] = NegUtilitarios.RutaLogo("General");
                        drhonorario["Direccion"] = item[1].ToString();
                        drhonorario["F_Ingreso"] = item[2].ToString();
                        drhonorario["F_Alta"] = item[3].ToString();
                        drhonorario["Identificacion"] = item[4].ToString();
                        drhonorario["Factura"] = item[5].ToString();
                        drhonorario["HC"] = item[6].ToString();
                        drhonorario["Atencion"] = item[7].ToString();
                        drhonorario["Habitacion"] = item[8].ToString();
                        drhonorario["Medico"] = item[9].ToString();
                        drhonorario["MFactura"] = item[10].ToString();
                        drhonorario["MFecha"] = item[11].ToString();
                        drhonorario["FormaPago"] = item[18].ToString();
                        drhonorario["Difiere"] = item[19].ToString();
                        drhonorario["Neto"] = item[12].ToString();
                        drhonorario["Comision"] = item[13].ToString();
                        drhonorario["Aporte"] = item[14].ToString();
                        drhonorario["Retencion"] = item[15].ToString();
                        if (item[16].ToString() != "")
                            drhonorario["NoCubierto"] = item[16].ToString();
                        else
                            drhonorario["NoCubierto"] = 0;
                        drhonorario["Total"] = item[17].ToString();
                        drhonorario["Referido"] = item[20].ToString();
                        if (usuarios != "")
                        {
                            string[] validador = usuarios.Split(';');
                            for (int i = 0; i < validador.Length; i++)
                            {
                                if (validador[i].ToString() != item[21].ToString() && validador[i].ToString() != "")
                                    usuarios += " " + item[21].ToString() + ";";
                            }
                        }
                        else
                            usuarios = item[21].ToString() + ";";
                        drhonorario["Usuario"] = usuarios;
                        drhonorario["UsuarioImprime"] = Sesion.nomUsuario;
                        drhonorario["Vale"] = item[22].ToString();
                        drhonorario["Recorte"] = item[23].ToString();
                        drhonorario["Cliente"] = item[24].ToString();
                        honorario.Tables["Honorario"].Rows.Add(drhonorario);
                    }
                    //// cambio hr reporte honorarios 02122019
                    //frmReportes frm = new frmReportes();
                    //frm.reporte = "rHonorariosAtencion";
                    //frm.campo1 = txtAtencion.Text.Trim();// atencionDtoActual.ATE_CODIGO.ToString();
                    //frm.Show();

                    //CAMBIO NUEVO REPORTE DE HONORARIO DETALLE
                    frmReportes x = new frmReportes("HonorarioDetalle", honorario);
                    x.Show();
                }
                else
                {
                    MessageBox.Show("No tiene honorarios a mostrar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio al imprimir el reporte, consulte con sistemas. " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtValorNeto_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void chkXFuera_CheckedChanged(object sender, EventArgs e)
        {
            calcularValoresFactura();
            if (chkXFuera.Checked)
                dtpCaducidad.Visible = false;
            else
                dtpCaducidad.Visible = true;
        }

        private void saveHMDatosAdicionales()
        {

            int HOMCODIGO = Convert.ToInt32(TXTHOMCOD.Text);
            string FecCaducidad = dtpCaducidad.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            int HonFuera = 0;
            if (chkXFuera.Checked)
            {
                HonFuera = 1;
            }
            if (codAnticipo == null && !Editar)
                NegHonorariosMedicos.saveHMDatosAdicionales(HOMCODIGO, FecCaducidad, HonFuera, txtAutSri.Text.Trim(), Caja(), Convert.ToDecimal(txtcubierto.Text), Convert.ToDecimal(txtapc.Text));
            else if (codAnticipo != null && Editar)
                NegHonorariosMedicos.saveHMDatosAdicionales2(HOMCODIGO, FecCaducidad, HonFuera, txtAutSri.Text.Trim(), Caja(), codAnticipo, Convert.ToDecimal(txtcubierto.Text), Convert.ToDecimal(txtapc.Text));
            else if (!Editar)
                NegHonorariosMedicos.saveHMDatosAdicionales1(HOMCODIGO, FecCaducidad, HonFuera, txtAutSri.Text.Trim(), Caja(), codAnticipo, Convert.ToDecimal(txtcubierto.Text), Convert.ToDecimal(txtapc.Text));


            //OCULTO EL APC EN ROJO
            txtapc.Text = "0.00";
            txtapc.Visible = false;
            txtAporteMedLLam.Visible = true;
        }
        string DirectorioBaseSic3000 = "";
        private string DirectorioEmpresa()
        {
            String line;
            try
            {
                StreamReader sr = new StreamReader("C:\\Sic3000\\Datos\\Directorio.ini");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the lie to console window                    
                    DirectorioBaseSic3000 = line.ToString();
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                return DirectorioBaseSic3000;
            }
            catch (Exception e)
            {
                MessageBox.Show("Algo ocurrio con el directorio.ini. Consulte con el Administrador");
                return "";
            }
        }
        public string numCaja = ""; //da error al eliminar pero no cuando se guarda
        private String Caja()
        {
            string Empresa = "";
            string Caja = "";
            String line;
            try
            {
                Empresa = DirectorioEmpresa();
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C:\\Sic3000\\" + Empresa + "\\Sic3000.ini");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the lie to console window                    
                    Caja = line.ToString();
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                if (numCaja == "")
                    numCaja = Caja.Substring(Caja.Length - 3, 3);
                return Caja.Substring(Caja.Length - 3, 3);
            }
            catch (Exception e)
            {
                MessageBox.Show("Algo ocurrio con el numero de caja con el Sic3000.ini, consulte con el Administrador");
                return "";
            }
        }

        private void loadHMDatosAdicionles()
        {
            int HOMCODIGO = Convert.ToInt32(TXTHOMCOD.Text);

            if (NegHonorariosMedicos.existHMDatosAdicionales(HOMCODIGO))
            {
                DataTable x = NegHonorariosMedicos.getHMDatosAdicionales(HOMCODIGO);
                foreach (DataRow row in x.Rows)
                {
                    if (row["HON_FUERA"].ToString() == "1")
                    {
                        chkXFuera.Checked = true;
                    }
                    else if (row["HON_FUERA"].ToString() == "0")
                    {
                        chkXFuera.Checked = false;
                        if (txt_codigoFormaspago.Text == "267")
                            btnAnticipos.Visible = false;
                    }
                    else if (row["HON_FUERA"].ToString() == "True")
                    {
                        chkXFuera.Checked = true;
                        DataTable AnticipoFormaP = NegFormaPago.FormaPagoAnticipo(numrec);
                        if (AnticipoFormaP.Rows.Count > 0)
                        {
                            if (AnticipoFormaP.Rows[0][1].ToString().Contains("EFECTIVO"))
                                lblComision.Text = "0.00 %";
                        }
                    }
                    else if (row["HON_FUERA"].ToString() == "False")
                    {
                        chkXFuera.Checked = false;
                        if (txt_codigoFormaspago.Text == "267")
                            btnAnticipos.Visible = false;
                        DataTable PagoHonorario = NegFactura.FormasPagoFacturaHonorarios(txtFactura.Text.Trim().Replace("-", string.Empty));
                        if (PagoHonorario.Rows.Count > 0)
                        {
                            foreach (DataRow item in PagoHonorario.Rows)
                            {
                                if (item[1].ToString().Contains("EFECTIVO"))
                                {
                                    lblComision.Text = "0.00 %";
                                    break;
                                }
                            }
                        }
                    }
                    dtpCaducidad.Value = Convert.ToDateTime(row["FEC_CAD_FACTURA"].ToString());
                    if (row["AUTORIZACION_SRI"].ToString().Trim() != "")
                    {
                        txtAutSri.Text = row["AUTORIZACION_SRI"].ToString().Trim();
                    }
                    if (row["HON_CUBIERTO"].ToString() != "")
                    {
                        txtcubierto.Text = row["HON_CUBIERTO"].ToString();
                    }
                }
            }
        }

        private void bttnCancelar_Click(object sender, EventArgs e)
        {
            grp2.Visible = true; grp3.Visible = true; grpCP.Visible = false;
        }

        private void ultraButton2_Click_3(object sender, EventArgs e)
        {
            if (txtAtencion.Text.Trim() != "")
            {
                string vales = "('0')";
                string facturas = "('0')";
                MedicoCP = false; //DEJO EN FALSO PORQUE AUN NO SE ELIGE UN MEDICO DE CUENTA PACIENTE
                if (ultraGrid.Rows.Count > 0)
                {
                    if (ultraGrid.Rows.Count == 1)
                    {
                        vales = "(";
                        facturas = "(";
                        for (int i = 0; i < ultraGrid.Rows.Count; i++)
                        {

                            vales += "'" + ultraGrid.Rows[i].Cells["VALE"].Value + "'";
                            facturas += "'" + ultraGrid.Rows[i].Cells["FACTURA"].Value + "'";
                            facturas = facturas.Trim();
                        }
                        int arreglo = vales.Length;
                        vales = vales.Substring(0, arreglo - 1);
                        vales += "')";

                        arreglo = facturas.Length;
                        facturas = facturas.Substring(0, arreglo - 1);
                        facturas += "')";
                        if (facturas.Length < 15)
                        {
                            facturas = "('0')";
                        }
                    }
                    else
                    {
                        vales = "(";
                        facturas = "(";
                        for (int i = 0; i < ultraGrid.Rows.Count; i++)
                        {

                            vales += "'" + ultraGrid.Rows[i].Cells["VALE"].Value + "',";
                            facturas += "'" + ultraGrid.Rows[i].Cells["FACTURA"].Value + "',";
                            facturas = facturas.Trim();

                        }
                        int arreglo = vales.Length;
                        vales = vales.Substring(0, arreglo - 1);
                        vales += ")";

                        arreglo = facturas.Length;
                        facturas = facturas.Substring(0, arreglo - 1);
                        facturas += ")";
                        if (facturas.Length < 15)
                        {
                            facturas = "('0')";
                        }
                    }
                }

                grp2.Visible = false; grp3.Visible = false; grpCP.Visible = true;
                gridCP.DataSource = NegDietetica.getDataTable("getHMCtasPctes", txtAtencion.Text.Trim(), vales, null, facturas);
                gridCP.DisplayLayout.Bands[0].Columns[1].Width = 300;
                gridCP.DisplayLayout.Bands[0].Columns[4].Width = 150;
                gridCP.DisplayLayout.Bands[0].Columns[5].Width = 400;
            }
        }

        private void gridCP_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
        {

        }
        public int id_usuario = 0;
        public string value_neto = "0.00";
        public bool MedicoCP = false;
        private void gridCP_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            try
            {
                int tIngreso = NegTipoIngreso.RecuperarporAtencion(Convert.ToInt64(txtAtencion.Text));

                DataTable FormaPagoxDentro = new DataTable();
                string numfac = txtFactura.Text;
                numfac = numfac.Replace("-", String.Empty);
                if (numfac.Trim() != "")
                {
                    FormaPagoxDentro = NegHonorariosMedicos.HMDentroPago(numfac);
                    //SE AGREGA LA FORMA DE PAGO SIEMPRE Y CUANDO EL PACIENTE TENGA FACTURA
                    if (FormaPagoxDentro.Rows.Count > 0) //Valida que tenga factura
                    {
                        int index1 = Convert.ToInt32(FormaPagoxDentro.Rows[0]["forpag"].ToString()); //Codigo de la FORMA DE PAGO
                        int index2 = Convert.ToInt32(FormaPagoxDentro.Rows[0]["claspag"].ToString()); //Codigo de la EMPRESA

                        cmb_empresas.SelectedValue = index2;
                        cboFiltroTipoFormaPago.SelectedValue = index1;
                    }
                }
                grp2.Visible = true; grp3.Visible = true; grpCP.Visible = false;
                int codHonorario = Convert.ToInt32(gridCP.ActiveRow.Cells["MED_CODIGO"].Value);
                ch_vale.Checked = true;
                //MessageBox.Show(codHonorario.ToString());
                txtCodMedico.Text = Convert.ToString(gridCP.ActiveRow.Cells["MED_CODIGO"].Value);
                TXTHOMCOD.Text = Convert.ToString(gridCP.ActiveRow.Cells["MED_CODIGO"].Value);
                txtNombreMedico.Text = Convert.ToString(gridCP.ActiveRow.Cells["MEDICO"].Value);
                dateTimePickerFecha.Value = Convert.ToDateTime(gridCP.ActiveRow.Cells["FECHA"].Value);
                value_neto = Convert.ToString(gridCP.ActiveRow.Cells["VALOR"].Value);
                txtValorNeto.Text = Convert.ToString(gridCP.ActiveRow.Cells["VALOR"].Value);
                txt_vale.Text = Convert.ToString(gridCP.ActiveRow.Cells["FACT_MEDICO"].Value);
                id_usuario = Convert.ToInt32(gridCP.ActiveRow.Cells["ID_USUARIO"].Value);
                ch_factura.Checked = false;
                ch_vale.Checked = true;
                // Convert.ToString(gridCP.ActiveRow.Cells["SEGURO"].Value);
                if (tIngreso == 4)
                    txtObservacion.Text = "CONSULTA EXTERNA";
                else
                    txtObservacion.Text = "";
                cargarMedico(codHonorario);
                chkXFuera.Enabled = false;
                chkXFuera.Checked = false;
                dtpCaducidad.Visible = false;
                MedicoCP = true; //INDICO QUE SE ELIGIO EL MEDICO DE CUENTA PACIENTE
                if (FormaPagoxDentro.Rows.Count > 0) //Valida que tenga factura
                {
                    formaPago = new FORMA_PAGO();
                    DataTable montos = new DataTable();
                    montos = obj_atencion.buscar_forma_pagos(cboFiltroFormaPago.Text);
                    if (montos.Rows.Count > 0)
                    {
                        lblAporte.Text = montos.Rows[0][2].ToString();
                        lblComision.Text = montos.Rows[0][1].ToString();
                        foreach (DataRow item in montos.Rows)
                        {
                            formaPago.FOR_CODIGO = Convert.ToInt16(item[0].ToString());
                        }
                        lblAporte.Visible = true;
                        lblComision.Visible = true;
                        cargarComision(formaPago.FOR_CODIGO);
                        Calculos();

                    }
                }
                //txtNumControl.Text = grid.ActiveRow.Cells[columnacontrol].Value.ToString();
                //txtNumControl.Tag = true;
                //if (grid.ActiveRow.Cells["fecha1"].Value.ToString() == "")
                //    fechafacturacion.Value = Convert.ToDateTime("01/01/1900");
                //else
                //    fechafacturacion.Value = Convert.ToDateTime(grid.ActiveRow.Cells["fecha1"].Value);

                //if (grid.ActiveRow.Cells["FECHA_ALTA"].Value.ToString() == "")
                //    fechaalta.Value = Convert.ToDateTime("01/01/1900");
                //else
                //    fechaalta.Value = Convert.ToDateTime(grid.ActiveRow.Cells["FECHA_ALTA"].Value);

                //if (grid.ActiveRow.Cells["FECHA_INGRESO"].Value.ToString() == "")
                //    fechaingreso.Value = Convert.ToDateTime("01/01/1900");
                //else
                //    fechaingreso.Value = Convert.ToDateTime(grid.ActiveRow.Cells["FECHA_INGRESO"].Value);

                //string refer = grid.ActiveRow.Cells[columnaReferido].Value.ToString();
                //if (refer.Trim() == "INSTITUCIONAL")
                //{
                //    campoReferido.Checked = true;
                //}
                //if (refer.Trim() == "PRIVADO")
                //{
                //    campoReferido.Checked = false;
                //}
                //this.Close();

                //this.Close();
            }
            catch (Exception es)
            {
                Console.WriteLine(es.Message);
            }
        }



        private void cboFiltroTipoFormaPago_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cmb_empresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_empresas.Text != "" && cmb_empresas.Text != "System.Data.DataRowView")
            {
                DataSet dts_combos = new DataSet();
                dts_combos = obj_atencion.combo_fomapago(cmb_empresas.Text);

                cboFiltroTipoFormaPago.DataSource = dts_combos.Tables[0];
                cboFiltroTipoFormaPago.DisplayMember = dts_combos.Tables[0].Columns["TE_DESCRIPCION"].ColumnName.ToString();
                cboFiltroTipoFormaPago.ValueMember = dts_combos.Tables[0].Columns["TE_CODIGO"].ColumnName.ToString();
                //cboFiltroTipoFormaPago.Text = "";
                //cboFiltroTipoFormaPago.SelectedIndex = -1;
                cboFiltroFormaPago.Text = "";
                cboFiltroFormaPago.SelectedIndex = -1;
                lblAporte.Text = "10";
                lblComision.Text = "10";
                lblAporte.Visible = false;
                lblComision.Visible = false;

                if (cmb_empresas.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    if (cmb_empresas.SelectedValue.ToString() == "2" && chkXFuera.Checked)
                    {
                        lblcubierto.Visible = false;
                        txtcubierto.Visible = false;
                        txtcubierto.Text = "0.00";
                    }
                    else
                    {
                        lblcubierto.Visible = true;
                        txtcubierto.Visible = true;
                    }
                }
                //cargarComision(Convert.ToInt32(dts_combos.Tables[0].Columns["TE_CODIGO"].ColumnName.ToString()));
                //    //MessageBox.Show(cmb_empresas.GetItemText(cmb_empresas.SelectedItem));

                //    //  DataSet dts_combos = new DataSet();
                //    // dts_combos = obj_atencion.combo_fomapago(cmb_empresas.GetItemText(cmb_empresas.SelectedItem));

                //    //   this.cboFiltroFormaPago.DataSource = dts_combos.Tables[0];
                //    //  cboFiltroFormaPago.DisplayMember = dts_combos.Tables[0].Columns["TE_DESCRIPCION"].ColumnName.ToString();
                //    ///  cboFiltroFormaPago.ValueMember = dts_combos.Tables[0].Columns["TE_CODIGO"].ColumnName.ToString();
                //    ///  
            }
        }
        private void cboFiltroTipoFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (cboFiltroTipoFormaPago.DataSource != null && cboFiltroTipoFormaPago.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                formaPago = new FORMA_PAGO();
                plazoPago = NegFactura.PlazoPagoSic(Convert.ToInt32(cboFiltroTipoFormaPago.SelectedValue.ToString()));
                if (plazoPago.Rows.Count > 0)
                {

                    cboFiltroFormaPago.Enabled = true;
                    cboFiltroFormaPago.DataSource = plazoPago;
                    cboFiltroFormaPago.DisplayMember = plazoPago.Columns[1].ToString();
                    cboFiltroFormaPago.ValueMember = plazoPago.Columns[0].ToString();

                }
                else
                {
                    cboFiltroFormaPago.SelectedIndex = -1;
                    cboFiltroFormaPago.Text = "";
                    MessageBox.Show("No Hay Plazo Para Esta Forma De Pago", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cboFiltroFormaPago.Enabled = false;
                }
            }
            else
            {
                txtAporteMedLLam.Text = "0.00";
                txtComisionClinica.Text = "0.00";
                txtRetencion.Text = "0.00";
            }
            //else
            //{
            //    if (cboFiltroTipoFormaPago.DataSource != null && cboFiltroTipoFormaPago.Text != "System.Data.DataRowView")
            //    {
            //        formaPago = new FORMA_PAGO();
            //        plazoPago = NegFactura.PlazoPagoSic(Convert.ToInt32(cboFiltroTipoFormaPago.SelectedValue.ToString()));
            //        if (plazoPago.Rows.Count > 0)
            //        {
            //            cboFiltroFormaPago.Enabled = true;
            //            cboFiltroFormaPago.DataSource = plazoPago;
            //            cboFiltroFormaPago.DisplayMember = plazoPago.Columns[1].ToString();
            //            cboFiltroFormaPago.ValueMember = plazoPago.Columns[0].ToString();
            //            foreach (DataRow item in plazoPago.Rows)
            //            {

            //                formaPago.FOR_CODIGO = Convert.ToInt16(item[0].ToString());
            //            }

            //            cargarComision(formaPago.FOR_CODIGO);
            //        }
            //    }
            //}
        }
        public string codAnticipo;
        public string valorAnticipo;
        public string nombreAnticipo; //Nombre del paciente
        private void cboFiltroFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFiltroFormaPago.Text != "System.Data.DataRowView" && cboFiltroFormaPago.DataSource != null)
            {
                formaPago = new FORMA_PAGO();
                DataTable montos = new DataTable();
                montos = obj_atencion.buscar_forma_pagos(cboFiltroFormaPago.Text);
                string porcentajeAPC = "";
                if (txtCodMedico.Text != "")
                {
                    porcentajeAPC = NegHonorariosMedicos.Porcentaje_MedicoAPC(Convert.ToInt64(txtCodMedico.Text));
                }

                if (montos.Rows.Count > 0)
                {
                    if (porcentajeAPC != "")
                        lblAporte.Text = Convert.ToString(Math.Round(Convert.ToDecimal(porcentajeAPC), 2));
                    else
                        lblAporte.Text = montos.Rows[0][2].ToString();
                    lblComision.Text = montos.Rows[0][1].ToString();

                    foreach (DataRow item in montos.Rows)
                    {
                        formaPago.FOR_CODIGO = Convert.ToInt16(item[0].ToString());
                    }
                    lblAporte.Visible = true;
                    lblComision.Visible = true;
                    cargarComision(formaPago.FOR_CODIGO);
                    //valido anticipo realizado con efectivo o tarjeta
                    if (cboFiltroFormaPago.SelectedValue.ToString() != "System.Data.DataRowView")
                    {
                        //CAMBIOS EDGAR PARA VALIDAR ANTICIPO
                        if (cboFiltroFormaPago.Text == "ANTICIPOS" && chkXFuera.Checked)
                        {
                            if (codAnticipo != null)
                            {
                                DataTable AnticipoFormaP = NegFormaPago.FormaPagoAnticipo(codAnticipo);
                                if (AnticipoFormaP.Rows.Count > 0)
                                {
                                    if (AnticipoFormaP.Rows[0][1].ToString().Contains("EFECTIVO"))
                                        lblComision.Text = "0.00 %";
                                }
                            }
                            btnAnticipos.Visible = true;
                        }
                        else if (cboFiltroFormaPago.Text == "ANTICIPOS")
                        {
                            btnAnticipos.Visible = false;
                            DataTable PagoHonorario = NegFactura.FormasPagoFacturaHonorarios(txtFactura.Text.Trim().Replace("-", string.Empty));
                            if (PagoHonorario.Rows.Count > 0)
                            {
                                foreach (DataRow item in PagoHonorario.Rows)
                                {
                                    if (item[1].ToString().Contains("EFECTIVO"))
                                    {
                                        lblComision.Text = "0.00 %";
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    Calculos();
                }
            }
        }

        private void txtAutSri_TextChanged(object sender, EventArgs e)
        {
            string autorizacion = txtAutSri.Text;

            if (autorizacion.Length == 49)
            {
                ch_factura.Checked = true;
                ch_vale.Checked = false;
                //txt_vale.Text = "";
                txt_vale.Enabled = false;
                txtFacturaMedico.Enabled = false;
                var fecha = autorizacion.Substring(0, 2) + "/" + autorizacion.Substring(2, 2) + "/" + autorizacion.Substring(4, 4);
                txtFacturaMedico.Text = autorizacion.Substring(24, 15);
                dateTimePickerFecha.Value = Convert.ToDateTime(fecha);
            }
            //else if(txtAutSri.Text.Length > 49)
            //{
            //    txtFacturaMedico.Enabled = true;
            //    ch_vale.Checked = true;
            //}
        }

        private void cmb_empresas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                cboFiltroTipoFormaPago.Focus();
            }
        }

        private void cboFiltroTipoFormaPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                cboFiltroFormaPago.Focus();
            }
        }

        private void cboFiltroFormaPago_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(GeneralPAR.TeclaTabular) || e.KeyChar == (char)(Keys.Enter))
            {
                if (txtLote.Visible == true)
                {
                    e.Handled = true;
                    txtLote.Focus();
                }
                else
                {
                    e.Handled = true;
                    txtObservacion.Focus();
                }
            }
        }
        public bool apcActivado = false;
        public void CalcularValorCubierto()
        {
            double valorAPagar;
            valorAPagar = Convert.ToDouble(txtValorTotal.Text);
            valorAPagar = (Convert.ToDouble(txtValorNeto.Text) - Convert.ToDouble(txtComisionClinica.Text) - Convert.ToDouble(txtAporteMedLLam.Text) - Convert.ToDouble(txtRetencion.Text));
            if (Convert.ToDouble(txtcubierto.Text) <= valorAPagar)
                txtValorTotal.Text = (Math.Round(valorAPagar - Convert.ToDouble(txtcubierto.Text), 2)).ToString();
            else
            {
                //Esta funcion se activara cuando se determine por cartera la aceptacion.
                sinAPC(true, false);
                //MessageBox.Show("Valor cubierto no puede superar a total a pagar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //txtcubierto.Text = "0.00";
            }
        }

        public void sinAPC(bool cubierto, bool recorte)
        {
            if (apcActivado == false)
            {
                if (MessageBox.Show("Valor no cubierto excede valor a pagar\r\n" +
               "¿Desea no tomar en cuenta el APC?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    apcActivado = true;
                    txtapc.Text = txtAporteMedLLam.Text;
                    txtapc.Visible = true;
                    txtAporteMedLLam.Text = "0.00";
                    txtAporteMedLLam.Visible = false;
                    txtValorTotal.Text = (Math.Round(Convert.ToDouble(txtValorNeto.Text) - Convert.ToDouble(txtcubierto.Text) - Convert.ToDouble(txtrecorte.Text), 2)).ToString();
                }
                else
                {
                    if (cubierto)
                    {
                        txtAporteMedLLam.Visible = true;
                        txtapc.Visible = false;
                        txtcubierto.Text = "0.00";
                    }
                    if (recorte)
                    {
                        txtAporteMedLLam.Visible = true;
                        txtapc.Visible = false;
                        txtrecorte.Text = "0.00";
                    }
                }
            }
            else
            {
                txtAporteMedLLam.Text = "0.00";
                txtValorTotal.Text = (Math.Round(Convert.ToDouble(txtValorNeto.Text) - Convert.ToDouble(txtcubierto.Text) - Convert.ToDouble(txtrecorte.Text), 2)).ToString();
            }

        }
        private void txtcubierto_TextChanged(object sender, EventArgs e)
        {
            if (cboFiltroTipoFormaPago.Text != "")
            {
                if (txtcubierto.Text.ToString() != string.Empty || txtcubierto.Text.ToString() != "0.00")
                {
                    if (Microsoft.VisualBasic.Information.IsNumeric(txtcubierto.Text))
                    {
                        CalcularValorCubierto();
                    }
                    else
                    {
                        txtcubierto.Text = string.Empty;
                    }
                }


                if (txtcubierto.Text == "0.00" || txtcubierto.Text.ToString() == string.Empty)
                {

                }
            }
            else
            {
                txtcubierto.Text = "0.00";
                return;
            }
        }

        private void txtcubierto_Enter(object sender, EventArgs e)
        {
            if (txtcubierto.Text == "0.00")
            {
                txtcubierto.Text = string.Empty;
            }
        }

        private void txtcubierto_Leave(object sender, EventArgs e)
        {
            if (txtcubierto.Text == string.Empty)
            {
                txtcubierto.Text = "0.00";
            }
        }

        private void txtrecorte_TextChanged(object sender, EventArgs e)
        {
            if (cboFiltroTipoFormaPago.Text != "")
            {
                if (txtrecorte.Text.ToString() != string.Empty || txtrecorte.Text.ToString() != "0.00")
                {
                    if (Microsoft.VisualBasic.Information.IsNumeric(txtrecorte.Text))
                    {
                        CalcularRecorte();
                    }
                    else
                    {
                        txtrecorte.Text = string.Empty;
                    }
                }

                if (txtrecorte.Text == "0.00" || txtrecorte.Text.ToString() == string.Empty)
                {

                }
            }
            else
            {
                txtrecorte.Text = "0.00";
                return;
            }
        }

        private void txtrecorte_Enter(object sender, EventArgs e)
        {
            if (txtrecorte.Text == "0.00")
            {
                txtrecorte.Text = string.Empty;
            }
        }

        private void txtrecorte_Leave(object sender, EventArgs e)
        {
            if (txtrecorte.Text == string.Empty)
            {
                txtrecorte.Text = "0.00";
            }
        }

        public void CalcularRecorte()
        {
            double valorAPagar;
            var aporte = (Convert.ToDouble(txtValorNeto.Text) - Convert.ToDouble(txtrecorte.Text)) * obtenerAporteLlamada();

            if (apcActivado == true)
                txtapc.Text = "" + aporte.ToString("N2");
            txtAporteMedLLam.Text = "" + aporte.ToString("N2");

            valorAPagar = Math.Round((Convert.ToDouble(txtValorNeto.Text) - Convert.ToDouble(txtComisionClinica.Text) - Convert.ToDouble(txtAporteMedLLam.Text) - Convert.ToDouble(txtRetencion.Text) - Convert.ToDouble(txtcubierto.Text)), 2);

            if (Convert.ToDouble(txtrecorte.Text) <= valorAPagar)
            {
                valorAPagar = Math.Round((Convert.ToDouble(txtValorNeto.Text) - Convert.ToDouble(txtComisionClinica.Text) - Convert.ToDouble(txtAporteMedLLam.Text) - Convert.ToDouble(txtRetencion.Text) - Convert.ToDouble(txtcubierto.Text) - Convert.ToDouble(txtrecorte.Text)), 2);
                txtValorTotal.Text = valorAPagar.ToString();
            }
            else
            {
                //Esta funcion se activara cuando se determine por cartera la aceptacion.
                sinAPC(false, true);
                //MessageBox.Show("Valor cubierto no puede superar a total a pagar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //txtcubierto.Text = "0.00";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (MessageBox.Show("¿Desea no tomar en cuenta el APC?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    PARAMETROS_DETALLE pd = NegParametros.RecuperaPorCodigo(53);
                    if ((bool)pd.PAD_ACTIVO)
                        txtapc.Text = "0.00";
                    else
                        txtapc.Text = txtAporteMedLLam.Text;
                    txtapc.Visible = true;
                    txtAporteMedLLam.Text = "0.00";
                    txtAporteMedLLam.Visible = false;
                    double valorAPagar = Math.Round((Convert.ToDouble(txtValorNeto.Text) - Convert.ToDouble(txtComisionClinica.Text) - Convert.ToDouble(txtAporteMedLLam.Text) - Convert.ToDouble(txtRetencion.Text) - Convert.ToDouble(txtcubierto.Text) - Convert.ToDouble(txtrecorte.Text)), 2);
                    txtValorTotal.Text = valorAPagar.ToString();
                }
            }
            else
            {
                txtAporteMedLLam.Visible = true;
                txtapc.Visible = false;
                Calculos();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cerrar el ingreso de honorarios?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                btncerrar.Enabled = false;
                ultraGrid.Enabled = false;
                ultraButton2.Enabled = false;
                ayudaMedicos.Enabled = false;
                try
                {
                    NegHonorariosMedicos.HonorariosCerrar(Convert.ToInt64(txtAtencion.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo cerrar los honorarios. Consulte con sistemas.\r\nMas en: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void CalcularComisionCheck()
        {
            double valorComision = Convert.ToDouble(lblComision.Text.ToString().TrimEnd('%')) / 100;
            txtComisionClinica.Text = Math.Round((Convert.ToDouble(txtValorNeto.Text) - Convert.ToDouble(txtcubierto.Text) - Convert.ToDouble(txtrecorte.Text)) * valorComision, 2).ToString();
            txtValorTotal.Text = Math.Round((Convert.ToDouble(txtValorNeto.Text) - Convert.ToDouble(txtComisionClinica.Text) - Convert.ToDouble(txtAporteMedLLam.Text) - Convert.ToDouble(txtcubierto.Text) - Convert.ToDouble(txtrecorte.Text) - Convert.ToDouble(txtRetencion.Text)), 2).ToString();
        }
        private void chkComision_CheckedChanged(object sender, EventArgs e)
        {
            if (chkComision.Checked)
                lblComision.Text = "5.75%";
            else
                lblComision.Text = "0.00%";
            CalcularComisionCheck();
        }

        private void asientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            His.Formulario.DSAsiento asiento = new Formulario.DSAsiento();
            DataRow cabecera;
            bool asientos = false;

            foreach (UltraGridRow item in ultraGrid.Rows)
            {
                USUARIOS users = new USUARIOS();
                try
                {
                    DataTable TCabecera = NegHonorariosMedicos.ImpresionAsientos(Convert.ToInt64(item.Cells["COD"].Value.ToString()), 0);

                    //DataTable TDetalle = NegHonorariosMedicos.ImpresionAsientos(Convert.ToInt64(item.Cells["COD"].Value.ToString()), 1);
                    if (TCabecera.Rows.Count > 0)
                    {
                        asientos = true;

                        if (TCabecera.Rows.Count > 0)
                        {
                            for (int i = 0; i < TCabecera.Rows.Count; i++)
                            {
                                cabecera = asiento.Tables["Cabecera"].NewRow();
                                cabecera["numdoc"] = TCabecera.Rows[i]["numdoc"].ToString();
                                users = NegUsuarios.RecuperaUsuario(Convert.ToInt16(TCabecera.Rows[i]["codrespon"].ToString()));
                                cabecera["usuario"] = users.USR;
                                cabecera["logo"] = NegUtilitarios.RutaLogo("General");
                                cabecera["fecha"] = TCabecera.Rows[i]["fechatran"].ToString();
                                cabecera["observacion"] = TCabecera.Rows[i]["observacion"].ToString();
                                cabecera["beneficiario"] = TCabecera.Rows[i]["beneficiario"].ToString();
                                cabecera["numCuenta"] = TCabecera.Rows[i]["CODIGO"].ToString();
                                cabecera["Cuenta"] = TCabecera.Rows[i]["nomcue_pc"].ToString();
                                cabecera["Auxiliar"] = TCabecera.Rows[i]["codigo_c"].ToString();
                                cabecera["numFac"] = TCabecera.Rows[i]["nocomp"].ToString();
                                cabecera["Debe"] = TCabecera.Rows[i]["debe"].ToString();
                                cabecera["Haber"] = TCabecera.Rows[i]["haber"].ToString();

                                asiento.Tables["Cabecera"].Rows.Add(cabecera);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (asientos)
            {
                Formulario.frmReportes x = new Formulario.frmReportes(1, "HonorariosAsiento", asiento);
                x.ShowDialog();
            }
            else
            {
                MessageBox.Show("No tiene asiento generados.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAnticipos_Click(object sender, EventArgs e)
        {
            try 
            {
                if (cboFiltroFormaPago.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    //CAMBIOS EDGAR PARA VALIDAR ANTICIPO
                    if (cboFiltroFormaPago.Text == "ANTICIPOS")
                    //if (Convert.ToInt32(cboFiltroFormaPago.SelectedValue.ToString()) == 15)
                    {
                        frmAnticiposSic form1 = new frmAnticiposSic(txtAtencion.Text);
                        if (form1.validador == 0)
                        {
                            form1.ShowDialog();
                            if (form1.cod_anticipo != null)
                            {
                                if (Convert.ToDouble(form1.valor_anticipo) > 0)
                                {
                                    valorAnticipo = form1.valor_anticipo;
                                    codAnticipo = form1.cod_anticipo;
                                    nombreAnticipo = form1.nombre_paciente;

                                    txtValorNeto.Text = valorAnticipo; //Agrego el valor del anticipo al valor por cobrar del honorario
                                }
                                else
                                {
                                    MessageBox.Show("Ese anticipo ya ha sido utilizado.\r\nIntente con otro mayor a cero.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                    }
                    //txtValorNeto.Text = value_neto;
                }
            }
            catch
            {
                MessageBox.Show("Seleccione el anticipo a liquidar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

            
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            UltraGridRow fila = ultraGrid.ActiveRow;
            if (fila.Activated)
            {

                if (NegHonorariosMedicos.existeCabmael(Convert.ToInt64(fila.Cells["COD"].Value.ToString())))
                {
                    MessageBox.Show("Este honorario no se puede habilitar Por favor reviselo en el modulo de Contabilidad", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    if (NegHonorariosMedicos.existeContabilidad(Convert.ToInt64(fila.Cells["COD"].Value.ToString())))
                    {
                        MessageBox.Show("Este honorario no se puede habilitar Por favor reviselo en el modulo de Contabilidad", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("¿Esta seguro que desea habilitar el honorario?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            NegHonorariosMedicos.cambiarEstadoHOMdatos(Convert.ToInt64(fila.Cells["COD"].Value.ToString()));
                            MessageBox.Show("Honorario habilitado correctamente.", "HIS300", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                }
            }
        }

        private void txtAutSri_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}