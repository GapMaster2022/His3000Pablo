using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades;
using System.Globalization;
using Infragistics.Win.UltraWinGrid;

namespace His.Garantia
{
    public partial class Form1 : Form
    {
        NegPacienteGarantia garantia = new NegPacienteGarantia();
        //private static bool valor = false;
        private static bool Editar = false;
        private static string TG;
        private static string codigoGarantia;
        PACIENTES PacienteActual = null;
        internal static string codigoAtencion;
        private static string usuario;
        //PACIENTES_DATOS_ADICIONALES DatosPacienteActual = null;
        ATENCIONES UltimaAtencion = null;
        private static bool valido = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bloquear();
            RecuperarUsuario();
            CargarGarantias();
        }
        public void RecuperarUsuario()
        {
            usuario = Convert.ToString(His.Entidades.Clases.Sesion.codUsuario);
        }
        public void Bloquear()
        {
            btnguardar.Enabled = false;
            //btnActualizar.Enabled = false;
            ayudaPacientes.Enabled = false;
            btnmodificar.Enabled = false;
            btnimprimir.Enabled = false;
            txt_Historia_Pc.Enabled = false;
            txt_ApellidoH1.Enabled = false;
            txt_ApellidoH2.Enabled = false;
            txt_NombreH1.Enabled = false;
            txt_NombreH2.Enabled = false;
            txtatencion.Enabled = false;
            txthabitacion.Enabled = false;
            txtseguro.Enabled = false;
            txtfechai.Enabled = false;
            txtcedula.Enabled = false;
            combotipo.Enabled = false;
            txtfechaga.Enabled = false;
            txtvalor.Enabled = false;
            txtdiasve.Enabled = false;
            txtbeneficiario.Enabled = false;
            txtnumcb.Enabled = false;
            txtfechave.Enabled = false;
            combobt.Enabled = false;
            txtnumtarjeta.Enabled = false;
            txtcodseguridad.Enabled = false;
            txtestable.Enabled = false;
            txtautorizacion.Enabled = false;
            txtlote.Enabled = false;
            txtfechaa.Enabled = false;
            txtpersona.Enabled = false;
            checkBox1.Enabled = false;
            ckbCedula.Enabled = false;
            txtidentificacion.Enabled = false;
            txttelefono.Enabled = false;
            cbBanco.Enabled = false;
            txtcaducidad.Enabled = false;
        }
        public void Desbloquear()
        {
            btnguardar.Enabled = true;
            ayudaPacientes.Enabled = true;
            //btnActualizar.Enabled = true;
            btnmodificar.Enabled = true;
            btnimprimir.Enabled = true;
            //btnbuscar.Enabled = true;
            txt_Historia_Pc.Enabled = true;
            txt_ApellidoH1.Enabled = true;
            txt_ApellidoH2.Enabled = true;
            txt_NombreH1.Enabled = true;
            txt_NombreH2.Enabled = true;
            txtatencion.Enabled = true;
            txthabitacion.Enabled = true;
            txtseguro.Enabled = true;
            txtfechai.Enabled = true;
            txtcedula.Enabled = true;
            combotipo.Enabled = true;
            txtfechaga.Enabled = true;
            txtvalor.Enabled = true;
            txtdiasve.Enabled = true;
            txtbeneficiario.Enabled = true;
            txtnumcb.Enabled = true;
            txtfechave.Enabled = true;
            combobt.Enabled = true;
            txtnumtarjeta.Enabled = true;
            txtcodseguridad.Enabled = true;
            txtestable.Enabled = true;
            txtautorizacion.Enabled = true;
            txtlote.Enabled = true;
            txtfechaa.Enabled = true;
            txtpersona.Enabled = true;
            checkBox1.Enabled = true;
            txtidentificacion.Enabled = true;
            txttelefono.Enabled = true;
            ckbCedula.Enabled = true;
            cbBanco.Enabled = true;
            txtcaducidad.Enabled = true;
        }
        public void Limpiar()
        {
            txt_Historia_Pc.Text = "";
            txt_ApellidoH1.Text = "";
            txt_ApellidoH2.Text = "";
            txt_NombreH1.Text = "";
            txt_NombreH2.Text = "";
            txtatencion.Text = "";
            txthabitacion.Text = "";
            txtseguro.Text = "";
            txtfechai.Text = "";
            txtcedula.Text = "";
            combotipo.SelectedValue = "1";
            //txtfechagaa.Text = "";
            txtvalor.Text = "";
            txtdiasve.Text = "";
            txtbeneficiario.Text = "";
            txtnumcb.Text = "";
            txtfechave.Text = "";
            combobt.SelectedValue = "1";
            cbBanco.SelectedValue = "1";
            txtnumtarjeta.Text = "";
            txtcodseguridad.Text = "";
            txtestable.Text = "";
            txtautorizacion.Text = "";
            txtlote.Text = "";
            //txtfechaa.Text = "";
            txtpersona.Text = "";
            checkBox1.Checked = false;
            ckbCedula.Checked = false;
            txtidentificacion.Text = "";
            txtcaducidad.Text = "";
            txttelefono.Text = "";
        }

        private void facturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Desbloquear();
            btnmodificar.Enabled = false;
            Editar = false;
            Limpiar();
            TipoGarantia();
            txtfechaga.Text = DateTime.Now.ToShortDateString();
        }
        public void TipoGarantia()
        {
            combotipo.DataSource = garantia.TipoGarantia();
            combotipo.DisplayMember = "TG_NOMBRE";
            combotipo.ValueMember = "TG_CODIGO";
        }
        public void Banco()
        {
            cbBanco.DataSource = garantia.TipoBanco();
            cbBanco.DisplayMember = "BAN_NOMBRE";
            cbBanco.ValueMember = "BAN_CODIGO";
        }
        public void TipoTarjeta()
        {
            combobt.DataSource = garantia.TipoTarjeta();
            combobt.DisplayMember = "name";
            combobt.ValueMember = "codigo";
        }
        private static void Numbers(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789," + Convert.ToChar(8);
            }
            if (aceptados.Contains("" + e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtvalor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Numbers(e, false);
        }

        private void txtdiasve_KeyPress(object sender, KeyPressEventArgs e)
        {
            Numbers(e, false);
        }

        private void txtnumcb_KeyPress(object sender, KeyPressEventArgs e)
        {
            Numbers(e, false);
        }

        private void txtnumtarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Numbers(e, false);
        }

        private void txtcodseguridad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Numbers(e, false);
        }
        private void CerrarAyuda(object sender, FormClosedEventArgs e)
        {
            txt_Historia_Pc.Text = frmAyuda.campoPadre.Text;
        }
        public void CargarGarantias()
        {
            DateTime fechas;
            string cod;
            try
            {
                TablaGarantia.DataSource = garantia.CargarGarantias();
                UltraGridBand bandUno = TablaGarantia.DisplayLayout.Bands[0];

                TablaGarantia.DisplayLayout.ViewStyle = ViewStyle.SingleBand;
                //grid.DisplayLayout.Override.Allow

                TablaGarantia.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
                //grid.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                TablaGarantia.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                TablaGarantia.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;

                bandUno.Override.CellClickAction = CellClickAction.RowSelect;
                bandUno.Override.CellDisplayStyle = CellDisplayStyle.PlainText;

                TablaGarantia.DisplayLayout.AutoFitStyle = AutoFitStyle.ExtendLastColumn;
                TablaGarantia.DisplayLayout.Override.RowSizing = RowSizing.AutoFree;
                TablaGarantia.DisplayLayout.Override.RowSizingArea = RowSizingArea.EntireRow;

                //Caracteristicas de Filtro en la grilla
                TablaGarantia.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.FilterRow;
                TablaGarantia.DisplayLayout.Override.FilterEvaluationTrigger = Infragistics.Win.UltraWinGrid.FilterEvaluationTrigger.OnCellValueChange;
                TablaGarantia.DisplayLayout.Override.FilterOperatorLocation = Infragistics.Win.UltraWinGrid.FilterOperatorLocation.WithOperand;
                TablaGarantia.DisplayLayout.Override.FilterClearButtonLocation = Infragistics.Win.UltraWinGrid.FilterClearButtonLocation.Row;
                TablaGarantia.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;
                //
                TablaGarantia.DisplayLayout.UseFixedHeaders = true;

                //Dimension los registros
                TablaGarantia.DisplayLayout.Bands[0].Columns[0].Width = 120;
                TablaGarantia.DisplayLayout.Bands[0].Columns[1].Width = 60;
                TablaGarantia.DisplayLayout.Bands[0].Columns[2].Width = 60;
                TablaGarantia.DisplayLayout.Bands[0].Columns[3].Width = 260;
                TablaGarantia.DisplayLayout.Bands[0].Columns[4].Width = 140;
                TablaGarantia.DisplayLayout.Bands[0].Columns[5].Width = 80;
                TablaGarantia.DisplayLayout.Bands[0].Columns[6].Width = 120;
                TablaGarantia.DisplayLayout.Bands[0].Columns[7].Width = 220;
                foreach (Infragistics.Win.UltraWinGrid.UltraGridRow item in TablaGarantia.Rows)
                {
                    fechas = Convert.ToDateTime(item.Cells["ADG_FECHA_AUT"].Value.ToString());
                    fechas = fechas.AddDays(Convert.ToInt32(item.Cells["ADG_DIASVENCIMIENTO"].Value.ToString()));
                    if (fechas < DateTime.Now)
                    {
                        cod = item.Cells["ADG_Codigo"].Value.ToString();
                        garantia.Caduca(cod, Convert.ToString(DateTime.Now));
                    }
                }
                //Ocultar columnas, que son fundamentales al levantar informacion
                TablaGarantia.DisplayLayout.Bands[0].Columns[9].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[10].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[11].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[12].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[13].Hidden = false;
                TablaGarantia.DisplayLayout.Bands[0].Columns[14].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[15].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[16].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[17].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[18].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[19].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[20].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[21].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[22].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[23].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[24].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[25].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[26].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[27].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[28].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[29].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[30].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[31].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[32].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[33].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            VerificarCampos();
            if (Editar == true)
            {
                if (valido == true)
                {
                    try
                    {
                        if (combotipo.SelectedValue.ToString() == "1")
                        {

                            garantia.ModificarGarantia(combotipo.SelectedValue.ToString(), txtbeneficiario.Text, txtnumcb.Text,
                            txtvalor.Text, txtfechaga.Text + " " + DateTime.Now.ToShortTimeString(),
                            cbBanco.GetItemText(cbBanco.SelectedItem), "", txtcodseguridad.Text, txtdiasve.Text, txtlote.Text,
                            txtautorizacion.Text, txtpersona.Text, txtfechaa.Text + " " + DateTime.Now.ToShortTimeString(),
                            txtestable.Text, codigoGarantia, txtnumtarjeta.Text, usuario, txtidentificacion.Text, txttelefono.Text, txtcaducidad.Text);
                            MessageBox.Show("Datos Actualizados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarGarantias();
                            btnmodificar.Enabled = true;
                            Editar = false;
                            Limpiar();
                            Bloquear();
                        }
                        if (combotipo.SelectedValue.ToString() == "2")
                        {
                            garantia.ModificarGarantia(combotipo.SelectedValue.ToString(), txtbeneficiario.Text, txtnumcb.Text,
                            txtvalor.Text, txtfechaga.Text + " " + DateTime.Now.ToShortTimeString(), cbBanco.GetItemText(cbBanco.SelectedItem), combobt.GetItemText(combobt.SelectedItem),
                            txtcodseguridad.Text, txtdiasve.Text, txtlote.Text, txtautorizacion.Text, txtpersona.Text, txtfechaa.Text + " " + DateTime.Now.ToShortTimeString(),
                            txtestable.Text, codigoGarantia, txtnumtarjeta.Text, usuario, txtidentificacion.Text, txttelefono.Text, txtcaducidad.Text);
                            MessageBox.Show("Datos Actualizados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarGarantias();
                            btnmodificar.Enabled = true;
                            Editar = false;
                            Limpiar();
                            Bloquear();
                        }
                        if (combotipo.SelectedValue.ToString() == "7")
                        {
                            garantia.ModificarGarantia(combotipo.SelectedValue.ToString(), "", "",
                            txtvalor.Text, txtfechaga.Text + " " + DateTime.Now.ToShortTimeString(), "", "", "", "0", "", "", "", txtfechaa.Text + " " + DateTime.Now.ToShortTimeString(),  "", 
                            codigoGarantia, "", usuario, txtidentificacion.Text, txttelefono.Text, txtcaducidad.Text);
                            MessageBox.Show("Datos Actualizados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarGarantias();
                            btnmodificar.Enabled = true;
                            Editar = false;
                            Limpiar();
                            Bloquear();
                        }
                        if (combotipo.SelectedValue.ToString() == "3" || combotipo.SelectedValue.ToString() == "4" ||
                            combotipo.SelectedValue.ToString() == "5" || combotipo.SelectedValue.ToString() == "6")
                        {
                            garantia.ModificarGarantia(combotipo.SelectedValue.ToString(), txtbeneficiario.Text, txtnumcb.Text,
                                txtvalor.Text, txtfechaga.Text + " " + DateTime.Now.ToShortTimeString(), cbBanco.GetItemText(cbBanco.SelectedItem), combobt.GetItemText(combobt.SelectedItem),
                                txtcodseguridad.Text, "0", txtlote.Text, txtautorizacion.Text, txtpersona.Text, txtfechaa.Text + " " + DateTime.Now.ToShortTimeString(),
                                txtestable.Text, codigoGarantia, txtnumtarjeta.Text, usuario, txtidentificacion.Text, txttelefono.Text, txtcaducidad.Text);
                            MessageBox.Show("Datos Actualizados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarGarantias();
                            btnmodificar.Enabled = true;
                            Editar = false;
                            Limpiar();
                            Bloquear();
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrio Algo al Editar Datos por: " + ex, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Debe LLenar todos los Campos Habilitados", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                if (valido == true)
                {
                    try
                    {
                        if (combotipo.SelectedValue.ToString() == "1")
                        {
                            garantia.InsertarGarantia(combotipo.SelectedValue.ToString(), codigoAtencion, txtbeneficiario.Text,
                            txtnumcb.Text, txtvalor.Text, txtfechaga.Text + " " + DateTime.Now.ToShortTimeString(), cbBanco.GetItemText(cbBanco.SelectedItem), "", txtcodseguridad.Text,
                            txtdiasve.Text, txtlote.Text, txtautorizacion.Text, txtpersona.Text, txtfechaa.Text + " " + DateTime.Now.ToShortTimeString(), txtestable.Text, 
                            txtnumtarjeta.Text, usuario, txtidentificacion.Text, txttelefono.Text, "");
                            MessageBox.Show("Datos Ingresados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarGarantias();
                            Bloquear();
                            Limpiar();
                        }
                        else if (combotipo.SelectedValue.ToString() == "2")
                        {
                            garantia.InsertarGarantia(combotipo.SelectedValue.ToString(),  codigoAtencion, txtbeneficiario.Text,
                            txtnumcb.Text, txtvalor.Text, txtfechaga.Text + " " + DateTime.Now.ToShortTimeString(), cbBanco.GetItemText(cbBanco.SelectedItem),
                            combobt.GetItemText(combobt.SelectedItem), txtcodseguridad.Text, txtdiasve.Text, txtlote.Text, txtautorizacion.Text,
                            txtpersona.Text, txtfechaa.Text + " " + DateTime.Now.ToShortTimeString(), txtestable.Text, txtnumtarjeta.Text,
                            usuario,txtidentificacion.Text, txttelefono.Text, txtcaducidad.Text);
                            MessageBox.Show("Datos Ingresados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarGarantias();
                            Bloquear();
                            Limpiar();
                        }
                        else if (combotipo.SelectedValue.ToString() == "7")
                        {
                            garantia.InsertarGarantia(combotipo.SelectedValue.ToString(), codigoAtencion, ""
                                , "", txtvalor.Text, txtfechaga.Text + " " + DateTime.Now.ToShortTimeString(), "", "", "",
                                "0", "", "", "", txtfechaa.Text + " " + DateTime.Now.ToShortTimeString(), "", "", usuario, "", "","");
                            MessageBox.Show("Datos Ingresados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarGarantias();
                            Bloquear();
                            Limpiar();
                        }
                        else if (combotipo.SelectedValue.ToString() == "3" || combotipo.SelectedValue.ToString() == "4" ||
                            combotipo.SelectedValue.ToString() == "5" || combotipo.SelectedValue.ToString() == "6")
                        {
                            garantia.InsertarGarantia(combotipo.SelectedValue.ToString(), codigoAtencion, txtbeneficiario.Text,
                            txtnumcb.Text, txtvalor.Text, txtfechaga.Text + " " + DateTime.Now.ToShortTimeString(), cbBanco.GetItemText(cbBanco.SelectedItem), combobt.GetItemText(combobt.SelectedItem), txtcodseguridad.Text,
                            "0", txtlote.Text, txtautorizacion.Text, txtpersona.Text, txtfechaa.Text, txtestable.Text, txtnumtarjeta.Text, 
                            usuario, txtidentificacion.Text, txttelefono.Text, txtcaducidad.Text);
                            MessageBox.Show("Datos Ingresados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarGarantias();
                            Bloquear();
                            Limpiar();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Debe Llenar todos los Campos Habilitados", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        public void VerificarTG()
        {
            if (TG == "EFECTIVO (Anticipo)")
            {
                combotipo.SelectedValue = "7";
            }
            if (TG == "VOUCHER")
            {
                combotipo.SelectedValue = "2";
                preauto.Enabled = true;
                lblfecha.Text = "Fecha:";
                lblnumcb.Text = "Nº de Váucher:";
                combobt.Enabled = true;
                lblbt.Text = "Tipo de Tarjeta:";
                txtfechave.Enabled = false;
                txtnumcb.Enabled = true;
                txtfechave.Enabled = false;
                txtbeneficiario.Enabled = true;
                combobt.Enabled = true;
                txtdiasve.Enabled = true;
                txtcodseguridad.Enabled = true;
            }
            if (TG == "CHEQUE")
            {
                combotipo.SelectedValue = "1";
                preauto.Enabled = false;
                combobt.Enabled = true;
                txtfechave.Enabled = true;
                txtnumcb.Enabled = true;
                txtfechave.Enabled = false;
                txtbeneficiario.Enabled = true;
                combobt.Enabled = false;
                txtdiasve.Enabled = true;
                txtnumtarjeta.Enabled = false;
                txtcodseguridad.Enabled = false;
                txtidentificacion.Enabled = false;
                txtcaducidad.Enabled = false;
                txttelefono.Enabled = false;
                ckbCedula.Enabled = false;
                lblnumcb.Text = "Nº de Cheque:";
                txtnumtarjeta.Text = "";
            }
        }
        private void combotipo_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            combobt.DataSource = null;
            if (combotipo.SelectedValue.ToString() == "1")
            {
                preauto.Enabled = false;
                lblfecha.Text = "Fecha:";
                lblnumcb.Text = "Nº de Cheque:";
                combobt.Enabled = true;
                txtfechave.Enabled = true;
                txtnumcb.Enabled = true;
                txtfechave.Enabled = false;
                txtbeneficiario.Enabled = true;
                combobt.Enabled = false;
                txtdiasve.Enabled = true;
                txtnumtarjeta.Enabled = false;
                txtcodseguridad.Enabled = false;
                ckbCedula.Enabled = false;
                txtidentificacion.Enabled = false;
                txttelefono.Enabled = false;
                txtcaducidad.Enabled = false;
                Banco();
            }
            else if (combotipo.SelectedValue.ToString() == "7")
            {
                preauto.Enabled = false;
                txtdiasve.Enabled = false;
                txtbeneficiario.Enabled = false;
                txtnumcb.Enabled = false;
                txtfechave.Enabled = false;
                combobt.Enabled = false;
                cbBanco.Enabled = false;
                txtcaducidad.Enabled = false;
                txtidentificacion.Enabled = false;
                txttelefono.Enabled = false;
                txtnumtarjeta.Enabled = false;
                txtcodseguridad.Enabled = false;
                txtidentificacion.Enabled = false;
                txttelefono.Enabled = false;
                txtcaducidad.Enabled = false;
                ckbCedula.Enabled = false;
            }
            else if (combotipo.SelectedValue.ToString() == "2")
            {
                txtnumtarjeta.Enabled = true;
                preauto.Enabled = true;
                lblfecha.Text = "Fecha:";
                lblnumcb.Text = "Nº de Váucher:";
                combobt.Enabled = true;
                cbBanco.Enabled = true;
                lblbt.Text = "Tipo de Tarjeta:";
                txtfechave.Enabled = false;
                txtnumcb.Enabled = true;
                txtfechave.Enabled = false;
                txtbeneficiario.Enabled = true;
                txtdiasve.Enabled = true;
                txtcodseguridad.Enabled = true;
                txtcaducidad.Enabled = true;
                txtidentificacion.Enabled = true;
                txttelefono.Enabled = true;
                ckbCedula.Enabled = true;
                TipoTarjeta();
            }
            else if (combotipo.SelectedValue.ToString() == "3" || combotipo.SelectedValue.ToString() == "4" ||
                            combotipo.SelectedValue.ToString() == "5" || combotipo.SelectedValue.ToString() == "6")
            {
                preauto.Enabled = false;
                txtdiasve.Enabled = false;
                txtbeneficiario.Enabled = false;
                txtnumcb.Enabled = false;
                txtfechave.Enabled = false;
                combobt.Enabled = false;
                txtnumtarjeta.Enabled = false;
                txtcodseguridad.Enabled = false;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarGarantias();
        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
            Desbloquear();
            Editar = true;
            btnmodificar.Enabled = false;
            btnguardar.Enabled = true;
            VerificarTG();
        }

        private void txt_Historia_Pc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CargarPaciente(txt_Historia_Pc.Text);
                txt_Historia_Pc.Tag = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CargarPaciente(string historia)
        {
            DateTime fechai;
            try
            {
                if (historia.Trim() != string.Empty)
                {
                    PacienteActual = NegPacientes.RecuperarPacienteID(historia);
                }
                else
                {
                    PacienteActual = null;
                }
                if (PacienteActual != null)
                {
                    txt_Historia_Pc.ReadOnly = false;
                    //if(PacienteActual.PAC_DATOS_INCOMPLETOS == true)
                    //{
                    //  MessageBox.Show("Datos Incompletos", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //}
                    //else
                    //{

                    //}
                    txt_NombreH1.Text = PacienteActual.PAC_NOMBRE1;
                    txt_NombreH2.Text = PacienteActual.PAC_NOMBRE2;
                    txt_ApellidoH1.Text = PacienteActual.PAC_APELLIDO_PATERNO;
                    txt_ApellidoH2.Text = PacienteActual.PAC_APELLIDO_MATERNO;
                    txtcedula.Text = PacienteActual.PAC_IDENTIFICACION;
                    txtseguro.Text = garantia.Aseguradora(txtcedula.Text);
                    fechai = (DateTime)PacienteActual.PAC_FECHA_CREACION;
                    txtfechai.Text = fechai.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                    //CargarDatosAdicionalesPaciente(PacienteActual.PAC_CODIGO);
                    CargarUltimaAtencion(PacienteActual.PAC_CODIGO);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (ex.InnerException != null)
                {
                    MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void CargarUltimaAtencion(int keyPaciente)
        {
            UltimaAtencion = NegAtenciones.RecuperarUltimaAtencion(keyPaciente);
            CargarAtencion();
        }
        public void CargarAtencion()
        {
            try
            {
                if (UltimaAtencion != null)
                {
                    txtatencion.Text = UltimaAtencion.ATE_NUMERO_ATENCION;
                    codigoAtencion = Convert.ToString(UltimaAtencion.ATE_CODIGO);
                    HABITACIONES Hab = NegHabitaciones.listaHabitaciones().FirstOrDefault(h => h.EntityKey == UltimaAtencion.HABITACIONESReference.EntityKey);
                    if (Hab != null)
                    {
                        His.Entidades.Clases.Sesion.codHabitacion = Hab.hab_Codigo;
                        txthabitacion.Text = Hab.hab_Numero;

                    }
                    else
                    {
                        Hab = NegHabitaciones.RecuperarHabitacionId(Convert.ToInt16(UltimaAtencion.HABITACIONESReference.EntityKey.EntityKeyValues[0].Value));
                        if (Hab != null)
                        {
                            His.Entidades.Clases.Sesion.codHabitacion = Hab.hab_Codigo;
                            txthabitacion.Text = Hab.hab_Numero;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtvalor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtdiasve.Enabled == true)
                {
                    txtdiasve.Focus();
                }
            }
        }

        private void txtbeneficiario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtnumcb.Enabled == true)
                {
                    txtnumcb.Focus();
                }
            }
        }

        private void txtnumcb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (combobt.Enabled == true)
                {
                    cbBanco.Focus();
                }
            }
        }

        private void combobt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtnumtarjeta.Enabled == true)
                {
                    txtnumtarjeta.Focus();
                }
            }
        }

        private void txtnumtarjeta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtcodseguridad.Enabled == true)
                {
                    txtcaducidad.Focus();
                }
            }
        }

        private void txtcodseguridad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtestable.Enabled == true)
                {
                    txtidentificacion.Focus();
                }
            }
        }

        private void txtestable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtautorizacion.Enabled == true)
                {
                    txtautorizacion.Focus();
                }
            }
        }

        private void txtautorizacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtfechaa.Focus();
            }
        }

        private void txtfechaa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtpersona.Enabled == true)
                {
                    txtpersona.Focus();
                }
            }
        }

        private void txtpersona_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtlote.Enabled == true)
                {
                    txtlote.Focus();
                }
            }
        }

        private void txtdiasve_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtbeneficiario.Enabled == true)
                {
                    txtbeneficiario.Focus();
                }
            }
        }

        private void txtdiasve_TextChanged(object sender, EventArgs e)
        {
            DateTime fechave, fechaga;
            if (txtdiasve.Text == "")
            {
                txtdiasve.Focus();
            }
            else
            {
                fechaga = Convert.ToDateTime(txtfechaga.Text);
                fechave = fechaga.AddDays(Convert.ToInt32(txtdiasve.Text));
                txtfechave.Text = fechave.ToShortDateString();
            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtpersona.Enabled == true)
                {
                    txtpersona.Focus();
                }
            }
        }

        private void txtfechaa_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechave, fechaga;
            fechaga = Convert.ToDateTime(txtfechaa.Text);
            fechave = fechaga.AddDays(Convert.ToInt32(txtdiasve.Text));
            txtfechave.Text = fechave.ToShortDateString();
        }

        private void combotipo_ValueMemberChanged(object sender, EventArgs e)
        {
            combobt.DataSource = null;
            cbBanco.DataSource = null;
            if (combotipo.SelectedValue.ToString() == "1")
            {
                preauto.Enabled = false;
                lblfecha.Text = "Fecha:";
                lblnumcb.Text = "Nº de Cheque:";
                combobt.Enabled = false;
                txtfechave.Enabled = true;
                txtnumcb.Enabled = true;
                txtfechave.Enabled = false;
                txtbeneficiario.Enabled = true;
                combobt.Enabled = false;
                txtdiasve.Enabled = true;
                txtnumtarjeta.Enabled = false;
                txtcodseguridad.Enabled = false;
                txtidentificacion.Enabled = false;
                txttelefono.Enabled = false;
                txtcaducidad.Enabled = false;
                cbBanco.Enabled = true;
                Banco();
            }
            else if (combotipo.SelectedValue.ToString() == "7")
            {
                preauto.Enabled = false;
                txtdiasve.Enabled = false;
                txtbeneficiario.Enabled = false;
                txtnumcb.Enabled = false;
                txtfechave.Enabled = false;
                combobt.Enabled = false;
                txtnumtarjeta.Enabled = false;
                txtcodseguridad.Enabled = false;
            }
            else if (combotipo.SelectedValue.ToString() == "2")
            {
                txtnumtarjeta.Enabled = true;
                preauto.Enabled = true;
                lblfecha.Text = "Fecha:";
                lblnumcb.Text = "Nº de Váucher:";
                combobt.Enabled = true;
                txtfechave.Enabled = false;
                txtnumcb.Enabled = true;
                txtfechave.Enabled = false;
                txtbeneficiario.Enabled = true;
                combobt.Enabled = true;
                txtdiasve.Enabled = true;
                txtcodseguridad.Enabled = true;
                txtidentificacion.Enabled = true;
                txttelefono.Enabled = true;
                txtcaducidad.Enabled = true;
                cbBanco.Enabled = true;
                TipoTarjeta();
            }
            else if (combotipo.SelectedValue.ToString() == "3" || combotipo.SelectedValue.ToString() == "4" ||
                            combotipo.SelectedValue.ToString() == "5" || combotipo.SelectedValue.ToString() == "6")
            {
                preauto.Enabled = false;
                txtdiasve.Enabled = false;
                txtbeneficiario.Enabled = false;
                txtnumcb.Enabled = false;
                txtfechave.Enabled = false;
                combobt.Enabled = false;
                txtnumtarjeta.Enabled = false;
                txtcodseguridad.Enabled = false;
                cbBanco.Enabled = false;
                txtidentificacion.Enabled = false;
                txttelefono.Enabled = false;
                txtcaducidad.Enabled = false;
            }
        }
        public bool VerificarCampos()
        {
            if (combotipo.SelectedValue.ToString() == "1")
            {
                valido = false;
                if (txtvalor.Text != "")
                {
                    if (txtdiasve.Text != "")
                    {
                        if (txtbeneficiario.Text != "")
                        {
                            if (txtnumcb.Text != "")
                            {
                                valido = true;
                                return valido;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Llenar el Campo \"Nom. Beneficiario\" ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Llenar el Campo \"Dias de Vencimiento\" ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Llenar el Campo \"Valor\" ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return valido;
            }
            if (combotipo.SelectedValue.ToString() == "2")
            {
                valido = false;
                if (txtvalor.Text != "")
                {
                    if (txtdiasve.Text != "")
                    {
                        if (txtbeneficiario.Text != "")
                        {
                            if (txtnumcb.Text != "")
                            {
                                if (txtnumtarjeta.Text != "")
                                {
                                    if (txtcodseguridad.Text != "")
                                    {
                                        if (txtestable.Text != "")
                                        {
                                            if (txtautorizacion.Text != "")
                                            {
                                                if (txtpersona.Text != "")
                                                {
                                                    valido = true;
                                                    return valido;
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Llenar el Campo \"Persona Autoriza\" ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Llenar el Campo \"Autorización\" ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Llenar el Campo \"Establecimiento\" ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Llenar el Campo \"Cod. Seguridad\" ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Llenar el Campo \"Num. de Tarjeta\" ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Llenar el Campo \"Num. de Váucher\" ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Llenar el Campo \"Nom. Beneficiario\" ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Llenar el Campo \"Dias de Vencimiento\" ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Llenar el Campo \"Valor\" ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            if (combotipo.SelectedValue.ToString() == "7")
            {
                valido = false;
                if (txtvalor.Text != "")
                {
                    valido = true;
                    return valido;
                }
                else
                {
                    MessageBox.Show("Llenar el Campo \"Valor\" ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            return valido;
        }

        private void txtusuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtbeneficiario.Enabled == true)
                {
                    txtbeneficiario.Focus();
                }
            }
        }

        private void combotipo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtfechaga.Focus();
            }
        }

        private void txtfechaga_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtvalor.Focus();
            }
        }

        private void TablaGarantia_DoubleClick_1(object sender, EventArgs e)
        {
            int index, index1;
            TipoGarantia();
            DateTime fechai, fechave, fechaga, fechaa;
            UltraGridRow Fila = TablaGarantia.ActiveRow;
            index = combotipo.FindString(Fila.Cells[8].Value.ToString());
            combotipo.SelectedIndex = index;
            txt_ApellidoH1.Text = Fila.Cells[9].Value.ToString();
            txt_ApellidoH2.Text = Fila.Cells[10].Value.ToString();
            txt_NombreH1.Text = Fila.Cells[11].Value.ToString();
            txt_NombreH2.Text = Fila.Cells[12].Value.ToString();
            txt_Historia_Pc.Text = Fila.Cells[2].Value.ToString();
            txtatencion.Text = Fila.Cells[1].Value.ToString();
            txthabitacion.Text = Fila.Cells[5].Value.ToString();
            fechai = Convert.ToDateTime(Fila.Cells[6].Value.ToString());
            txtfechai.Text = fechai.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            txtseguro.Text = Fila.Cells[7].Value.ToString();
            fechaga = Convert.ToDateTime(Fila.Cells[0].Value.ToString());
            txtfechaga.Text = fechaga.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            TG = Fila.Cells[8].Value.ToString();
            VerificarTG();
            txtvalor.Text = Fila.Cells[13].Value.ToString();
            txtdiasve.Text = Fila.Cells[14].Value.ToString();
            txtbeneficiario.Text = Fila.Cells[15].Value.ToString();
            txtnumcb.Text = Fila.Cells[16].Value.ToString();
            fechaa = Convert.ToDateTime(Fila.Cells[17].Value.ToString());
            txtfechaa.Text = fechaa.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            if (index == 0)
            {
                Banco();
                index1 = cbBanco.FindString(Fila.Cells[27].Value.ToString());
                cbBanco.SelectedIndex = index1;
                txtcodseguridad.Enabled = false;
            }
            else if (index == 1)
            {
                TipoTarjeta();
                Banco();
                index1 = combobt.FindString(Fila.Cells[18].Value.ToString());
                combobt.SelectedIndex = index1;
                index1 = cbBanco.FindString(Fila.Cells[27].Value.ToString());
                cbBanco.SelectedIndex = index1;
                txtcodseguridad.Enabled = true;
            }
            //combobt.SelectedItem = TablaGarantia.CurrentRow.Cells[18].Value.ToString();
            txtcodseguridad.Text = Fila.Cells[19].Value.ToString();
            txtestable.Text = Fila.Cells[20].Value.ToString();
            txtautorizacion.Text = Fila.Cells[21].Value.ToString();
            txtpersona.Text = Fila.Cells[22].Value.ToString();
            txtlote.Text = Fila.Cells[23].Value.ToString();
            txtcedula.Text = Fila.Cells[4].Value.ToString();
            txtnumtarjeta.Text = Fila.Cells[25].Value.ToString();
            txtidentificacion.Text = Fila.Cells[31].Value.ToString();
            txttelefono.Text = Fila.Cells[32].Value.ToString();
            txtcaducidad.Text = Fila.Cells[33].Value.ToString();
            //usuario = TablaGarantia.CurrentRow.Cells[26].Value.ToString(); se oculto orque ya no se necesita levatar ese dato
            if (txtdiasve.Text == "" || txtdiasve.Text == " ")
            {
                txtdiasve.Text = "0";
            }
            fechave = fechaa.AddDays(Convert.ToUInt32(txtdiasve.Text));
            txtfechave.Text = fechave.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            Bloquear();
            codigoGarantia = Fila.Cells[24].Value.ToString();
            Editar = true;
            btnmodificar.Enabled = true;
        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            Limpiar();
            frmAyuda x = new frmAyuda();
            x.Show();
            x.FormClosed += CerrarAyuda;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtbeneficiario.Text = txt_ApellidoH1.Text + " " + txt_NombreH1.Text;
            }
            else
            {
                txtbeneficiario.Text = "";
            }
        }

        private void btncancelar_Click(object sender, EventArgs e)
        {
            combobt.DataSource = null;
            combotipo.DataSource = null;
            cbBanco.DataSource = null;
            Editar = false;
            Bloquear();
            Limpiar();
        }

        private void txtcaducidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtcodseguridad.Focus();
            }
        }

        private void ckbCedula_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbCedula.Checked == true)
            {
                txtidentificacion.Text = txtcedula.Text;
            }
            else
            {
                txtidentificacion.Text = "";
            }
        }

        private void txtidentificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtestable.Focus();
            }
        }

        private void txttelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtidentificacion.Focus();
            }
        }

        private void txtcaducidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            Numbers(e, false);
        }

        private void txttelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            Numbers(e, false);
        }

        private void ckbCedula_CheckedChanged_1(object sender, EventArgs e)
        {
            if(ckbCedula.Checked == true)
            {
                txtidentificacion.Text = txtcedula.Text;
            }
            else
            {
                txtidentificacion.Text = "";
            }
        }
    }
}
