using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Negocio;
using His.Entidades.Clases;
using Recursos;

using Infragistics.Win.UltraWinGrid;

namespace His.Facturacion
{
    public partial class frmFactura : Form
    {
        FACTURA nuevaFactura = new FACTURA();
        Boolean accionPaciente = false;
        Boolean accionFactura = false;
        public bool pacienteNuevo = false;
        int codigoFactura;
        PACIENTES pacienteFactura = new PACIENTES();
        ATENCIONES ultimaAtencion = null;
        List<FORMA_PAGO> listaFormaPago = new List<FORMA_PAGO>();
        DataTable dtFormasPagos = new DataTable();
        public frmFactura()
        {
            InitializeComponent();
            btnNuevo.Image = Archivo.imgBtnAdd2;
            btnBuscar.Image = Archivo.imgBtnRestart;            
            btnGuardar.Image = Archivo.imgBtnFloppy;
            btnCancelar.Image = Archivo.imgBtnStop;
            btnSalir.Image = Archivo.imgBtnSalir32;
            btnImprimir.Image = Archivo.imgBtnImprimir32;
            listaFormaPago = NegFormaPago.listaFormasPago();
            cbx_FormaPago.DataSource = listaFormaPago;
            cbx_FormaPago.ValueMember = "FOR_CODIGO";
            cbx_FormaPago.DisplayMember = "FOR_DESCRIPCION";
            cbx_FormaPago.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbx_FormaPago.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbx_FormaPago.SelectedIndex = -1;
            //    habilitarBotones(false, false, false, false, false, false, true); 
        }

        private void limpiarCampos()
        {
            txt_Historia_P.Tag = false;
            txt_ApellidoH1.Text = string.Empty;
            txt_ApellidoH2.Text = string.Empty;
            txt_NombreH1.Text = string.Empty;
            txt_NombreH2.Text = string.Empty;
            
            txt_Direccion_P.Text = string.Empty;
            txt_Telef_P.Text = string.Empty;
            txt_Historia_P.Text = string.Empty;
            txt_Habitacion_P.Text = string.Empty;
            txt_Dias_P.Text = string.Empty;
            txt_Ruc.Text = string.Empty;
            txt_Direc_Cliente.Text = string.Empty;
            txt_Cliente.Text = string.Empty;
            txt_telef_Cliente.Text = string.Empty;
        }

        private void habiltarBotones(Boolean nuevo, Boolean guardar, Boolean buscar, Boolean imprimir, Boolean cancelar, Boolean salir)
        {
            btnNuevo.Enabled = nuevo;
            btnGuardar.Enabled = guardar;
            btnBuscar.Enabled = buscar;
            btnImprimir.Enabled = imprimir;
            btnCancelar.Enabled = cancelar;
            btnSalir.Enabled = salir;
        }

        private void habiltarCampos()
        {
            txt_Ruc.Enabled = false;
            txt_Direc_Cliente.Enabled = false;
            txt_Cliente.Enabled = false;
            txt_telef_Cliente.Enabled = false;
        }

        private void cargarDatosPaciente(PACIENTES pacienteActual)
        {
            txt_ApellidoH1.Text = pacienteActual.PAC_APELLIDO_PATERNO;
            txt_ApellidoH2.Text = pacienteActual.PAC_APELLIDO_MATERNO;
            txt_NombreH1.Text = pacienteActual.PAC_NOMBRE1;
            txt_NombreH2.Text = pacienteActual.PAC_NOMBRE2;

            //txt_PacienteN.Text = pacienteActual.PAC_NOMBRE1 + " " + pacienteActual.PAC_NOMBRE2 + " " + pacienteActual.PAC_APELLIDO_PATERNO + " " + pacienteActual.PAC_APELLIDO_MATERNO;
            txt_Direccion_P.Text = pacienteActual.PAC_REFERENTE_DIRECCION;
            txt_Telef_P.Text = pacienteActual.PAC_REFERENTE_TELEFONO;
            txt_Historia_P.Text = pacienteActual.PAC_HISTORIA_CLINICA;
            txt_Habitacion_P.Text = "302";
            txt_Dias_P.Text = "3";
            ultimaAtencion = NegAtenciones.RecuperarUltimaAtencion(pacienteActual.PAC_CODIGO);
            MessageBox.Show(ultimaAtencion.ATE_FACTURA_NOMBRE);
            //cbx_FacturaNombre.SelectedItem = ultimaAtencion.ATE_FACTURA_NOMBRE;
            if (cbx_FacturaNombre.SelectedIndex > -1)
            {
                if (ultimaAtencion.ATE_FACTURA_NOMBRE == "PACIENTE")
                {
                    txt_Ruc.Text = pacienteActual.PAC_IDENTIFICACION;
                    txt_Direc_Cliente.Text = pacienteActual.PAC_REFERENTE_DIRECCION;
                    txt_Cliente.Text = pacienteActual.PAC_NOMBRE1 + " " + pacienteActual.PAC_NOMBRE2 + " " + pacienteActual.PAC_APELLIDO_PATERNO + " " + pacienteActual.PAC_APELLIDO_MATERNO;
                    txt_telef_Cliente.Text = pacienteActual.PAC_REFERENTE_TELEFONO;
                    ////cmb_tipopago.SelectedItem = tipoPago.FirstOrDefault(f => f.TIF_CODIGO == ultimaAtencion.TIF_CODIGO);                    
                }
                else
                {
                    if (ultimaAtencion.ATE_FACTURA_NOMBRE == "ACOMPAÑANTE")
                    {
                        txt_Ruc.Text = ultimaAtencion.ATE_ACOMPANANTE_CEDULA;
                        txt_Direc_Cliente.Text = ultimaAtencion.ATE_ACOMPANANTE_DIRECCION;
                        txt_Cliente.Text = ultimaAtencion.ATE_ACOMPANANTE_NOMBRE;
                        txt_telef_Cliente.Text = ultimaAtencion.ATE_ACOMPANANTE_TELEFONO; ;

                    }
                    else
                    {
                        if (ultimaAtencion.ATE_FACTURA_NOMBRE == "GARANTE")
                        {

                        }
                        else
                        {
                            if (ultimaAtencion.ATE_FACTURA_NOMBRE == "EMPRESA")
                            {

                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
            //txt_Ruc.Text = pacienteActual.PAC_IDENTIFICACION;
            //txt_Direc_Cliente.Text = pacienteActual.PAC_REFERENTE_DIRECCION;
            //txt_Cliente.Text = pacienteActual.PAC_NOMBRE1 + " " + pacienteActual.PAC_NOMBRE2 + " " + pacienteActual.PAC_APELLIDO_PATERNO + " " + pacienteActual.PAC_APELLIDO_MATERNO;
            //txt_telef_Cliente.Text = pacienteActual.PAC_REFERENTE_TELEFONO;            
        }

        private void cargarDatosFactura()
        {
            if(accionFactura == true)
            {
                nuevaFactura = new FACTURA();
                int codigoFactura = NegFactura.recuperaMaximoFactura();                
                //nuevaFactura.fac_c
            }
        }

        private void txt_historiaclinica_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (pacienteNuevo == false)
                {
                    if (e.KeyCode == Keys.F1)
                    {
                        frmAyudaPaciente form = new frmAyudaPaciente();
                        form.campoPadre = txt_Historia_P;
                        form.ShowDialog();
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                if (pacienteNuevo == false)
                {
                    frmAyudaPaciente form = new frmAyudaPaciente();
                    form.campoPadre = txt_Historia_P;
                    form.ShowDialog();
                    if (txt_Historia_P.Text != null)
                    {
                        pacienteFactura = NegPacientes.RecuperarPacienteID(txt_Historia_P.Text.Trim());
                        cargarDatosPaciente(pacienteFactura);
                        //cargarFormasPagos();
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_historiaclinica_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //string historia = txt_historiaclinica.Text.ToString();               
                if (pacienteNuevo == false)
                {
                    if (Convert.ToBoolean(txt_Historia_P.Tag) == true)
                    {
                        cargarPaciente(txt_Historia_P.Text.ToString());
                        txt_Historia_P.Tag = false;
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }


        private void cargarFormasPagos(FORMA_PAGO formaP)
        {
            try
            {
                DataRow drFormasPagos;
                gridFormaPago.DataSource = dtFormasPagos;
                if (gridFormaPago.Rows.Count == 0)
                {
                    dtFormasPagos.Columns.Add("NRO.", Type.GetType("System.String"));
                    dtFormasPagos.Columns.Add("FORMA DE PAGO", Type.GetType("System.String"));
                    dtFormasPagos.Columns.Add("TOTAL", Type.GetType("System.String"));
                }
                if (gridFormaPago.Rows.Count >= 0)
                {
                    for (int i = 0; i < gridFormaPago.Rows.Count; i++)
                    {
                        //string pago = gridFormaPago.Rows(i);
                    }

                    drFormasPagos = dtFormasPagos.NewRow();
                    drFormasPagos["NRO."] = gridFormaPago.Rows.Count;
                    drFormasPagos["FORMA DE PAGO"] = formaP.FOR_DESCRIPCION;
                    drFormasPagos["TOTAL"] = "";
                    dtFormasPagos.Rows.Add(drFormasPagos);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void cargarPaciente(string historia)
        {
            try
            {
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (e.InnerException != null)
                    MessageBox.Show(e.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            habiltarBotones(false, true, false, false, true, true);
            accionPaciente = false;
            accionFactura = true;            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            guardarFactura();
        }

        private void guardarFactura()
        {

        }

        private void guardarDatos()
        {
            try
            {
                if (!validarFormulario())
                {
                    DialogResult resultado;
                    resultado = MessageBox.Show("Desea guardar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultado == DialogResult.Yes)
                    {
                        if (accionPaciente == false)
                        {
                            //Ingreso Nueva Factura   
                            codigoFactura = NegFactura.recuperaMaximoFactura();
                            codigoFactura++;
                            //nuevaFactura.COD_FACTURA = codigoFactura;
                            nuevaFactura.ATENCIONESReference.EntityKey = pacienteFactura.EntityKey;
                            agregarDatosFactura();

                            NegFactura.crearFactura(nuevaFactura);
                            MessageBox.Show("Datos Almacenados Correctamente", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            habiltarBotones(true, true, true, true, true, true);
                            //buscarHistorias.Enabled = true;
                            accionPaciente = true;
                            nuevaFactura = new FACTURA();
                        }

                        //ultraTabPageControl1.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese Campos obligatorios");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error en el ingreso de datos: \n" + e.Message);
                if (e.InnerException != null)
                    MessageBox.Show("Error en el ingreso de datos: \n" + e.InnerException);
            }
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        private bool validarFormulario()
        {
            errorFactura.Clear();
            bool valido = false;
            //
            if (gridDetalleFactura.Rows.Count == 0)
            {
                AgregarError(gridDetalleFactura);
                valido = true;
            }
            if (gridFormaPago.Rows.Count == 0)
            {
                AgregarError(gridFormaPago);
                valido = true;
            }
            if (txt_Cliente.Text.Trim() == string.Empty)
            {
                AgregarError(txt_Cliente);
                valido = true;
            }
            if (txt_Ruc.Text.Trim() == string.Empty)
            {
                AgregarError(txt_Ruc);
                valido = true;
            }
            if (txt_Direc_Cliente.Text.Trim() == string.Empty)
            {
                AgregarError(txt_Direc_Cliente);
                valido = true;
            }
            if (txt_Factura_Cod1.Text.Trim() == string.Empty)
            {
                AgregarError(txt_Factura_Cod1);
                valido = true;
            }
            if (txt_Factura_Cod2.Text.Trim() == string.Empty)
            {
                AgregarError(txt_Factura_Cod2);
                valido = true;
            }
            if (txt_Factura_Cod3.Text.Trim() == string.Empty)
            {
                AgregarError(txt_Factura_Cod3);
                valido = true;
            }
            return valido;
        }
        private void AgregarError(Control control)
        {
            errorFactura.SetError(control, "Campo Requerido");
        }

        private void agregarDatosFactura()
        {
            try
            {
                //nuevaFactura.NUM_FACTURA = txt_Factura_Cod1.Text.Trim() + "-" + txt_Factura_Cod2.Text.Trim() + " " + txt_Factura_Cod3.Text.Trim();
                //nuevaFactura.FECHA = dtpFechaFacturacion.Value;
                nuevaFactura.CLI_NOMBRE = txt_Cliente.Text.Trim();
                nuevaFactura.CLI_RUC = txt_Ruc.Text.Trim();
                nuevaFactura.CLI_TELEFONO = txt_telef_Cliente.Text.Trim();
                gridDetalleFactura.DataSource = nuevaFactura;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error en el ingreso de datos: \n" + e.Message);
                if (e.InnerException != null)
                    MessageBox.Show("Error en el ingreso de datos: \n" + e.InnerException);
            }
        }


        private void crearFactura() 
        {
            txt_Factura_Cod1.Text = "001";
            txt_Factura_Cod2.Text = "001";
            txt_Factura_Cod3.Text = Convert.ToString(NegFactura.recuperaMaximoFactura());
            //txt_Autorizacion.Text = Convert.ToString(Parametros.FacturaPAR.serieFacturasAutorizacion);          
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cbx_FormaPago.SelectedIndex > -1)
            {
                FORMA_PAGO formaP = new FORMA_PAGO();
                formaP = (FORMA_PAGO)cbx_FormaPago.SelectedItem;
                cargarFormasPagos(formaP);
            }
            else
            {
                MessageBox.Show("Error debe Seleccionar una Forma de Pago: \n");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            frmDetalleCuenta form = new frmDetalleCuenta();
            form.Show();
        }

        private void frmFactura_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void gridDetalleFactura_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }

        private void gridFormaPago_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {

        }

        private void txt_Ruc_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
