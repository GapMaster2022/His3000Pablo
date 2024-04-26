using His.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades.Clases;
using Infragistics.Win.UltraWinGrid;

namespace His.Honorarios
{
    public partial class frm_FacturaCambiar : Form
    {
        ATENCIONES ultimaAtencion;
        PACIENTES paciente;
        PACIENTES_DATOS_ADICIONALES pda;
        public string facturaAnterior = "";
        public frm_FacturaCambiar()
        {
            InitializeComponent();
        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            frm_AyudaFacturas frm = new frm_AyudaFacturas();
            frm.ShowDialog();

            if(frm.ate_codigo != null)
            {
                ultimaAtencion = new ATENCIONES();
                paciente = new PACIENTES();
                pda = new PACIENTES_DATOS_ADICIONALES();
                ultimaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(frm.ate_codigo.Trim()));
                paciente = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(frm.ate_codigo.Trim()));
                pda = NegPacienteDatosAdicionales.RecuperarDatosPacientesID(paciente.PAC_CODIGO);
                this.facturaAnterior = frm.facturaAnte;
                Limpiar();
                lblAnterior.Text = lblAnterior.Text + facturaAnterior;
                txtObservacion.Text = frm.obs;
                CargarDatosPaciente();
                Bloquear(true, true, true, true, true);
            }
        }

        public void Limpiar()
        {
            txtPaciente.Text = "";
            txtCedula.Text = "";
            txtTelefono.Text = "";
            txtHc.Text = "";
            txtHab.Text = "";
            txtAtencion.Text = "";
            txtFactura.Text = "";
            dtpIngreso.Value = DateTime.Now;
            dtpAlta.Value = DateTime.Now;
            dtpFactura.Value = DateTime.Now;
            txtDireccion.Text = "";

            cmbEstado.SelectedIndex = -1;
            txtFacturaNueva.Text = "";
            txtObservacion.Text = "";
            errorProvider1.Clear();
            lblAnterior.Text = "FAC. ANTERIOR: ";
        }

        public void CargarDatosPaciente()
        {
            try
            {
                txtPaciente.Text = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO + " " + paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                txtCedula.Text = paciente.PAC_IDENTIFICACION;
                txtHc.Text = paciente.PAC_HISTORIA_CLINICA.Trim();
                txtHab.Text = ultimaAtencion.HABITACIONES.hab_Numero;
                txtAtencion.Text = ultimaAtencion.ATE_NUMERO_ATENCION;
                txtFactura.Text = ultimaAtencion.ATE_FACTURA_PACIENTE;
                txtFacturaNueva.Text = "001" + ultimaAtencion.ATE_FACTURA_PACIENTE;
                dtpIngreso.Value = (DateTime)ultimaAtencion.ATE_FECHA_INGRESO;
                dtpAlta.Value = (DateTime)ultimaAtencion.ATE_FECHA_ALTA;
                dtpFactura.Value = (DateTime)ultimaAtencion.ATE_FACTURA_FECHA;
                if(pda != null)
                {
                    txtDireccion.Text = pda.DAP_DIRECCION_DOMICILIO;
                    txtTelefono.Text = pda.DAP_TELEFONO1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio al cargar datos del paciente. Mas detalle: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_FacturaCambiar_Load(object sender, EventArgs e)
        {
            Bloquear(false, false, false, false, false);
            CargarTabla();
        }

        public void Bloquear(bool guardar, bool cancelar, bool estado, bool nueva, bool observacion) 
        {
            btnGuardar.Enabled = guardar;
            btnCancelar.Enabled = cancelar;
            cmbEstado.Enabled = estado;
            txtFacturaNueva.Enabled = nueva;
            txtObservacion.Enabled = observacion;
        }
        public bool validaFacturaIngresada(string numfac)
        {
            DataTable Tabla = NegHonorariosMedicos.FacturaAnuladaExiste(numfac);
            if (Tabla.Rows.Count > 0)
            {
                MessageBox.Show("Factura cambiada ya ha sido registrada.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
                return true;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validaFacturaIngresada(facturaAnterior))
            {
                if (Sesion.codDepartamento == 1 || Sesion.codDepartamento == 3)//SEGURIDAD PARA SISTEMAS Y CONTABILIDAD
                {
                    errorProvider1.Clear();
                    if (cmbEstado.SelectedIndex != -1)
                    {
                        if (cmbEstado.SelectedIndex == 0)
                        {
                            if (txtFacturaNueva.Text.Trim() != "")
                            {
                                if (txtFacturaNueva.Text.Length >= 15)
                                {
                                    if (txtObservacion.Text.Trim() != "")
                                    {
                                        //GUARDO PRIMERO LA ANTERIOR POR LA NUEVA FACTURA, NOTA DE CREDITO
                                        try
                                        {
                                            NegHonorariosMedicos.CambiarFactura(ultimaAtencion.ATE_CODIGO, cmbEstado.Text, facturaAnterior,
                                                txtFacturaNueva.Text.Trim().Substring(3), txtcredito.Text.Trim(), Sesion.codUsuario, txtObservacion.Text.Trim());
                                            CargarTabla();
                                            MessageBox.Show("Datos almacenados con éxito.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            Limpiar();
                                            Bloquear(false, false, false, false, false);
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Algo ocurrio al guardar. Mas detalles: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                        errorProvider1.SetError(txtObservacion, "Observacion es obligatoria.");
                                }
                                else
                                    errorProvider1.SetError(txtFacturaNueva, "La nueva factura debe tener 15 digitos.");
                            }
                            else
                                errorProvider1.SetError(txtFacturaNueva, "La nueva factura debe tener 15 digitos.");
                        }
                        else
                        {
                            if (txtcredito.Text.Length >= 15 && txtFacturaNueva.Text.Length >= 15)//DEBE TENER NECESARIAMENTE LOS DOS PARA CONTINUAR
                            {
                                if (txtObservacion.Text.Trim() != "")
                                {
                                    //GUARDO PRIMERO LA ANTERIOR POR LA NUEVA FACTURA, NOTA DE CREDITO
                                    try
                                    {
                                        NegHonorariosMedicos.CambiarFactura(ultimaAtencion.ATE_CODIGO, cmbEstado.Text, facturaAnterior,
                                            txtFacturaNueva.Text.Trim().Substring(3), txtcredito.Text.Trim(), Sesion.codUsuario, txtObservacion.Text.Trim());
                                        CargarTabla();
                                        MessageBox.Show("Datos almacenados con éxito.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        Limpiar();
                                        Bloquear(false, false, false, false, false);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Algo ocurrio al guardar. Mas detalles: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                    errorProvider1.SetError(txtObservacion, "Observacion es obligatoria.");
                            }
                            else
                                errorProvider1.SetError(txtcredito, "La factura ó nota de credito debe tener 15 digitos.");
                        }
                    }
                    else
                        errorProvider1.SetError(cmbEstado, "Debe elegir una opcion valida.");
                }
                else
                    MessageBox.Show("No tiene permiso para cambiar facturas. Consulte con sistemas.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Bloquear(false, false, false, false, false);
        }

        private void txtFacturaNueva_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbEstado.SelectedIndex == 0) //ES ANULADA
            {
                lblDocumento.Text = "N° FACTURA(*):";
                lbldoc.Visible = true;
                //txtFacturaNueva.Enabled = true;
                txtcredito.Enabled = false;
                txtcredito.Text = "0";
                //txtFacturaNueva.Text = "";
            }
            else if (cmbEstado.SelectedIndex == 1)//FACTURA NUEVA
            {
                txtFacturaNueva.Enabled = true;
                //txtFacturaNueva.Text = "0";
                txtcredito.Text = "";
                txtcredito.Enabled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
        }
        public void CargarTabla()
        {
            try
            {
                FacturasAnuladas.DataSource = NegHonorariosMedicos.FacturasAnuladas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio al cargar las facturas con cambios. Mas detalles: " + ex.Message,
                    "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarTabla();
        }

        private void FacturasAnuladas_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {
                UltraGridBand bandUno = FacturasAnuladas.DisplayLayout.Bands[0];

                FacturasAnuladas.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                FacturasAnuladas.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                FacturasAnuladas.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                FacturasAnuladas.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                FacturasAnuladas.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                FacturasAnuladas.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                FacturasAnuladas.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Caracteristicas de Filtro en la grilla
                FacturasAnuladas.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                FacturasAnuladas.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                FacturasAnuladas.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                FacturasAnuladas.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                FacturasAnuladas.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                FacturasAnuladas.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                FacturasAnuladas.DisplayLayout.Bands[0].Columns[0].Width = 100;
                FacturasAnuladas.DisplayLayout.Bands[0].Columns[1].Width = 260;
                FacturasAnuladas.DisplayLayout.Bands[0].Columns[2].Width = 80;
                FacturasAnuladas.DisplayLayout.Bands[0].Columns[3].Width = 80;
                FacturasAnuladas.DisplayLayout.Bands[0].Columns[4].Width = 120;
                FacturasAnuladas.DisplayLayout.Bands[0].Columns[5].Width = 120;
                FacturasAnuladas.DisplayLayout.Bands[0].Columns[6].Width = 120;
                FacturasAnuladas.DisplayLayout.Bands[0].Columns[7].Width = 180;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtFacturaNueva_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (txtFacturaNueva.Text.Length >= 15)
                {
                    if (!FacturaValida(txtFacturaNueva.Text.Trim()))
                    {
                        MessageBox.Show("Factura no encontrada ó no registrada", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                        txtcredito.Focus();
                }
                else
                    MessageBox.Show("Nº Factura no registrada", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool FacturaValida(string numfac)
        {
            bool valida = true;
            try
            {
                numfac = numfac.Substring(3);
                DataTable FacturaValida = NegHonorariosMedicos.FacturaValida(numfac);
                if (FacturaValida.Rows.Count > 0)
                {
                    if (FacturaValida.Rows[0][0].ToString() != "")
                    {
                        if (FacturaValida.Rows[0][0].ToString() != ultimaAtencion.ATE_CODIGO.ToString())
                        {
                            MessageBox.Show("Factura valida pero no corresponde al paciente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtFacturaNueva.Text = "";
                            return false;
                        }
                        else
                        {
                            DataTable ValidaAnulada = NegHonorariosMedicos.ValidaFacturasAnuladas(numfac);
                            if(ValidaAnulada.Rows.Count > 0)
                            {
                                MessageBox.Show("Factura ya esta anulada.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtFacturaNueva.Text = "";
                                return false;
                            }
                            else
                                return valida;
                        }
                    }
                    return valida;
                }
                else
                {
                    txtFacturaNueva.Text = "";
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void txtFacturaNueva_Leave(object sender, EventArgs e)
        {
            if(txtFacturaNueva.Text.Trim().Length >= 15)
            {
                if (!FacturaValida(txtFacturaNueva.Text.Trim()))
                {
                    MessageBox.Show("Factura no encontrada ó no registrada", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    txtcredito.Focus();
            }
        }
    }
}
