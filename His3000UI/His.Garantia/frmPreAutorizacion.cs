using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using His.Negocio;
using System.Windows.Forms;
using System.Globalization;
using Infragistics.Win.UltraWinGrid;

namespace His.Garantia
{
    public partial class frmPreAutorizacion : Form
    {
        NegPacienteGarantia garantia = new NegPacienteGarantia();
        private static bool Editar = false;
        private static string user;
        private static string TG;
        internal static bool vacio = true;
        #region Variables 
        private static int index;
        private static int index1;
        internal static string hc;
        internal static string atencion;
        internal static string habitacion;
        internal static string segu;
        internal static string fechaing;
        internal static string cedula;
        internal static string ape1;
        internal static string ape2;
        internal static string nom1;
        internal static string nom2;
        internal static string codtipo;
        internal static string fechagar;
        internal static string valor;
        internal static string usuario;
        internal static string beneficiario;
        internal static string diasve;
        internal static string numcb;
        internal static string numtarjeta;
        internal static string codseguridad;
        internal static string establecimiento;
        internal static string autoriza;
        internal static string fechaau;
        internal static string persona;
        internal static string lote;
        internal static string banco;
        internal static string vaucher;
        internal static string codigogarantia;
        #endregion
        public frmPreAutorizacion()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPreAutorizacion_Load(object sender, EventArgs e)
        {
            Bloquear();
            RecuperarUsuario();
            CargarGarantiasFecha();
        }
        public void RecuperarUsuario()
        {
            user = Convert.ToString(His.Entidades.Clases.Sesion.codUsuario);
        }
        private void btnmodificar_Click(object sender, EventArgs e)
        {
            if(txtonserva.Text == "")
            {
                txtonserva.Focus();
            }
            else
            {
                if (MessageBox.Show("¿Está Seguro que Desea Aprobar Pre-Autorizacion?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        garantia.Aprobacion(Convert.ToString(DateTime.Now), user, codigogarantia, txtonserva.Text);
                        MessageBox.Show("Pre-Autorización Aprobada Exitosamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                        CargarGarantias();
                        txtonserva.Visible = false;
                        lblobserva.Visible = false;
                        lblfca.Visible = false;
                        txtfca.Visible = false;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void txt_Historia_Pc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                
            }
        }
        private void CerrarAyuda(object sender, FormClosedEventArgs e)
        {
            if(vacio == false)
            {
                Limpiar();
                frmAyuda.autorizacion = false;
                //DATOS GENERALES
                txt_Historia_Pc.Text = hc;
                txtatencion.Text = atencion;
                txthabitacion.Text = habitacion;
                txtseguro.Text = segu;
                txtfechai.Text = fechaing;
                txtcedula.Text = cedula;
                txt_ApellidoH1.Text = ape1;
                txt_ApellidoH2.Text = ape2;
                txt_NombreH1.Text = nom1;
                txt_NombreH2.Text = nom2;
                groupgenerales.Enabled = false;
                vacio = true;
            }
            else
            {
                Bloquear();
                //CargarGarantiasFecha();
            }
        }
        public void Bloquear()
        {
            btnguardar.Enabled = false;
            //btnActualizar.Enabled = false;
            btnmodificar.Enabled = false;
            btnanular.Enabled = false;
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
            ayudaPacientes.Enabled = false;
            checkrepetir.Enabled = false;
            ckbCedula.Enabled = false;
            txtcaducidad.Enabled = false;
            txtidentificacion.Enabled = false;
            txttelefono.Enabled = false;
            cbBanco.Enabled = false;
        }
        public void Desbloquear()
        {
            btnguardar.Enabled = true;
            //btnActualizar.Enabled = true;
            btnmodificar.Enabled = true;
            btnanular.Enabled = true;
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
            ayudaPacientes.Enabled = true;
            checkrepetir.Enabled = true;
            txtcaducidad.Enabled = true;
            txtidentificacion.Enabled = true;
            txttelefono.Enabled = true;
            ckbCedula.Enabled = true;
            cbBanco.Enabled = true;
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
            cbBanco.SelectedValue = "1";
            //txtfechagaa.Text = "";
            txtvalor.Text = "";
            txtdiasve.Text = "";
            txtbeneficiario.Text = "";
            txtnumcb.Text = "";
            txtfechave.Text = "";
            combobt.SelectedValue = "1";
            txtnumtarjeta.Text = "";
            txtcodseguridad.Text = "";
            txtestable.Text = "";
            txtautorizacion.Text = "";
            txtlote.Text = "";
            //txtfechaa.Text = "";
            txtpersona.Text = "";
            checkrepetir.Checked = false;
            txtonserva.Text = "";
            txtfca.Text = "";
            txtcaducidad.Text = "";
            txtidentificacion.Text = "";
            txttelefono.Text = "";
            ckbCedula.Checked = false;
        }

        private void facturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(txt_Historia_Pc.Text == "" || btnanular.Enabled == false)
            {
                Editar = false;
                MessageBox.Show("Debe Seleccionar una Garantia: VIGENTE, CADUCADO O POR CADUCAR", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Desbloquear();
                btnanular.Enabled = false;
                btnmodificar.Enabled = false;
                txtfechaga.Text = DateTime.Now.ToShortDateString();
                VerificarTG();
            }
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

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarGarantiasFecha();
        }
        public void CargarGarantiasFecha()
        {
            DateTime fechas;
            DateTime date = DateTime.Now;
            string fechainicio, fechafin;
            //Asi obtenemos el primer dia del mes actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            DateTime oUltimoDiaDelMes = oPrimerDiaDelMes.AddMonths(1).AddDays(-1);
            fechainicio = Convert.ToString(oPrimerDiaDelMes);
            fechafin = Convert.ToString(oUltimoDiaDelMes);
            string cod;
            
            try
            {
                TablaGarantia.DataSource = garantia.FechaTodo(fechainicio, fechafin);
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

                //Dimension las celdas
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
                    if (fechas < DateTime.Now && item.Cells[31].Value.ToString() != "APROBADO" && item.Cells[31].Value.ToString() != "ANULADO")
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
                TablaGarantia.DisplayLayout.Bands[0].Columns[32].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[33].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[34].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[35].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CargarGarantias()
        {
            DateTime fechas;
            string cod;
            try
            {
                TablaGarantia.DataSource = garantia.CargarGarantiasTodo();
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

                //Dimension las celdas
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
                TablaGarantia.DisplayLayout.Bands[0].Columns[32].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[33].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[34].Hidden = true;
                TablaGarantia.DisplayLayout.Bands[0].Columns[35].Hidden = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void VerificarTG()
        {
            if (TG == "EFECTIVO (Anticipo)")
            {
                combotipo.SelectedValue = "7";
            }
            else if (TG == "VOUCHER")
            {
                combotipo.SelectedValue = "2";
                txtfechave.Enabled = false;
                txtnumcb.Enabled = true;
                txtfechave.Enabled = false;
                txtbeneficiario.Enabled = true;
                combobt.Enabled = true;
                txtdiasve.Enabled = true;
                txtcodseguridad.Enabled = true;
                txtestable.Enabled = true;
                txtautorizacion.Enabled = true;
                txtfechaa.Enabled = true;
                txtpersona.Enabled = true;
                txtlote.Enabled = true;
                txtidentificacion.Enabled = true;
                txtcaducidad.Enabled = true;
                txttelefono.Enabled = true;
                ckbCedula.Enabled = true;
            }
            else if (TG == "CHEQUE")
            {
                combotipo.SelectedValue = "1";
                combobt.Enabled = false;
                cbBanco.Enabled = true;
                txtfechave.Enabled = true;
                txtnumcb.Enabled = true;
                txtfechave.Enabled = false;
                txtbeneficiario.Enabled = true;
                txtdiasve.Enabled = true;
                txtnumtarjeta.Enabled = false;
                txtcodseguridad.Enabled = false;
                txtestable.Enabled = false;
                txtautorizacion.Enabled = false;
                txtfechaa.Enabled = false;
                txtpersona.Enabled = false;
                txtlote.Enabled = false;
                txtidentificacion.Enabled = false;
                txtcaducidad.Enabled = false;
                txttelefono.Enabled = false;
                ckbCedula.Enabled = false;
            }
        }


        private void combotipo_ValueMemberChanged_1(object sender, EventArgs e)
        {
            combobt.DataSource = null;
            cbBanco.DataSource = null;
            if (combotipo.SelectedValue.ToString() == "1")
            {
                lblfecha.Text = "Fecha:";
                lblnumcb.Text = "Nº de Cheque:";
                combobt.Enabled = false;
                cbBanco.Enabled = true;
                txtfechave.Enabled = true;
                txtnumcb.Enabled = true;
                txtfechave.Enabled = false;
                txtbeneficiario.Enabled = true;
                txtdiasve.Enabled = true;
                txtnumtarjeta.Enabled = false;
                txtcodseguridad.Enabled = false;
                txtestable.Enabled = false;
                txtautorizacion.Enabled = false;
                txtfechaa.Enabled = false;
                txtpersona.Enabled = false;
                txtlote.Enabled = false;
                txtidentificacion.Enabled = false;
                txtcaducidad.Enabled = false;
                txttelefono.Enabled = false;
                ckbCedula.Enabled = false;
                Banco();
            }
            if (combotipo.SelectedValue.ToString() == "7")
            {
                txtdiasve.Enabled = false;
                txtbeneficiario.Enabled = false;
                txtnumcb.Enabled = false;
                txtfechave.Enabled = false;
                combobt.Enabled = false;
                cbBanco.Enabled = false;
                txtnumtarjeta.Enabled = false;
                txtcodseguridad.Enabled = false;
                txtestable.Enabled = false;
                txtautorizacion.Enabled = false;
                txtfechaa.Enabled = false;
                txtpersona.Enabled = false;
                txtlote.Enabled = false;
                txtidentificacion.Enabled = false;
                txtcaducidad.Enabled = false;
                txttelefono.Enabled = false;
                ckbCedula.Enabled = false;
            }
            if (combotipo.SelectedValue.ToString() == "2")
            {
                txtnumtarjeta.Enabled = true;
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
                txtestable.Enabled = true;
                txtautorizacion.Enabled = true;
                txtfechaa.Enabled = true;
                txtpersona.Enabled = true;
                txtlote.Enabled = true;
                txtidentificacion.Enabled = true;
                txtcaducidad.Enabled = true;
                txttelefono.Enabled = true;
                ckbCedula.Enabled = true;
                TipoTarjeta();
            }
            if (combotipo.SelectedValue.ToString() == "3" || combotipo.SelectedValue.ToString() == "4" ||
                            combotipo.SelectedValue.ToString() == "5" || combotipo.SelectedValue.ToString() == "6")
            {
                txtdiasve.Enabled = false;
                txtbeneficiario.Enabled = false;
                txtnumcb.Enabled = false;
                txtfechave.Enabled = false;
                combobt.Enabled = false;
                cbBanco.Enabled = false;
                txtnumtarjeta.Enabled = false;
                txtcodseguridad.Enabled = false;
                txtestable.Enabled = false;
                txtautorizacion.Enabled = false;
                txtfechaa.Enabled = false;
                txtpersona.Enabled = false;
                txtlote.Enabled = false;
                txtidentificacion.Enabled = false;
                txtcaducidad.Enabled = false;
                txttelefono.Enabled = false;
                ckbCedula.Enabled = false;
            }
        }

        private void combotipo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            combobt.DataSource = null;
            cbBanco.DataSource = null;
            if (combotipo.SelectedValue.ToString() == "1")
            {
                lblfecha.Text = "Fecha:";
                lblnumcb.Text = "Nº de Cheque:";
                combobt.Enabled = false;
                cbBanco.Enabled = true;
                txtfechave.Enabled = true;
                txtnumcb.Enabled = true;
                txtfechave.Enabled = false;
                txtbeneficiario.Enabled = true;
                txtdiasve.Enabled = true;
                txtnumtarjeta.Enabled = false;
                txtcodseguridad.Enabled = false;
                txtestable.Enabled = false;
                txtautorizacion.Enabled = false;
                txtfechaa.Enabled = false;
                txtpersona.Enabled = false;
                txtlote.Enabled = false;
                txtidentificacion.Enabled = false;
                txtcaducidad.Enabled = false;
                txttelefono.Enabled = false;
                ckbCedula.Enabled = false;
                Banco();
            }
            if (combotipo.SelectedValue.ToString() == "7")
            {
                txtdiasve.Enabled = false;
                txtbeneficiario.Enabled = false;
                txtnumcb.Enabled = false;
                txtfechave.Enabled = false;
                combobt.Enabled = false;
                cbBanco.Enabled = false;
                txtnumtarjeta.Enabled = false;
                txtcodseguridad.Enabled = false;
                txtestable.Enabled = false;
                txtautorizacion.Enabled = false;
                txtfechaa.Enabled = false;
                txtpersona.Enabled = false;
                txtlote.Enabled = false;
                txtidentificacion.Enabled = false;
                txtcaducidad.Enabled = false;
                txttelefono.Enabled = false;
                ckbCedula.Enabled = false;
            }
            if (combotipo.SelectedValue.ToString() == "2")
            {
                txtnumtarjeta.Enabled = true;
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
                txtestable.Enabled = true;
                txtautorizacion.Enabled = true;
                txtfechaa.Enabled = true;
                txtpersona.Enabled = true;
                txtlote.Enabled = true;
                txtidentificacion.Enabled = true;
                txtcaducidad.Enabled = true;
                txttelefono.Enabled = true;
                ckbCedula.Enabled = true;
                TipoTarjeta();
            }
            if (combotipo.SelectedValue.ToString() == "3" || combotipo.SelectedValue.ToString() == "4" ||
                            combotipo.SelectedValue.ToString() == "5" || combotipo.SelectedValue.ToString() == "6")
            {
                txtdiasve.Enabled = false;
                txtbeneficiario.Enabled = false;
                txtnumcb.Enabled = false;
                txtfechave.Enabled = false;
                combobt.Enabled = false;
                txtnumtarjeta.Enabled = false;
                txtcodseguridad.Enabled = false;
                txtestable.Enabled = false;
                txtautorizacion.Enabled = false;
                txtfechaa.Enabled = false;
                txtpersona.Enabled = false;
                txtlote.Enabled = false;
                txtidentificacion.Enabled = false;
                txtcaducidad.Enabled = false;
                txttelefono.Enabled = false;
                ckbCedula.Enabled = false;
                cbBanco.Enabled = false;
            }
        }

        private void btnanular_Click(object sender, EventArgs e)
        {
            if (txtonserva.Text == "")
            {
                txtonserva.Focus();
            }
            else
            {
                if (MessageBox.Show("¿Está Seguro que Desea Anular Pre-Autorizacion?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    try
                    {
                        garantia.Anular(Convert.ToString(DateTime.Now), user, codigogarantia, txtonserva.Text);
                        MessageBox.Show("Pre-Autorización Anulada Exitosamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                        CargarGarantias();
                        txtonserva.Visible = false;
                        lblobserva.Visible = false;
                        lblfca.Visible = false;
                        txtfca.Visible = false;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            if (Editar == true)
            {
                try
                {
                    if (combotipo.SelectedValue.ToString() == "1")
                    {

                        garantia.ModificarGarantia(combotipo.SelectedValue.ToString(), txtbeneficiario.Text, txtnumcb.Text,
                        txtvalor.Text, txtfechaga.Text + " " + DateTime.Now.ToShortTimeString(), cbBanco.GetItemText(cbBanco.SelectedItem), "",
                        txtcodseguridad.Text, txtdiasve.Text, txtlote.Text, txtautorizacion.Text, txtpersona.Text, txtfechaa.Text + " " + DateTime.Now.ToShortTimeString(),
                        txtestable.Text, codigogarantia, txtnumtarjeta.Text, user, "", "", "");
                        MessageBox.Show("Datos Actualizados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarGarantiasFecha();
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
                        txtestable.Text, codigogarantia, txtnumtarjeta.Text, user, txtidentificacion.Text, txttelefono.Text, txtcaducidad.Text);
                        MessageBox.Show("Datos Actualizados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarGarantiasFecha();
                        btnmodificar.Enabled = true;
                        Editar = false;
                        Limpiar();
                        Bloquear();
                    }
                    if (combotipo.SelectedValue.ToString() == "7")
                    {
                        garantia.ModificarGarantia(combotipo.SelectedValue.ToString(), "", "",
                        txtvalor.Text, txtfechaga.Text + " " + DateTime.Now.ToShortTimeString(), "", "", "", "0", "", "", "", "", "",
                        codigogarantia, "", user, "", "", "");
                        MessageBox.Show("Datos Actualizados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarGarantiasFecha();
                        btnmodificar.Enabled = true;
                        Editar = false;
                        Limpiar();
                        Bloquear();
                    }
                    if (combotipo.SelectedValue.ToString() == "3" || combotipo.SelectedValue.ToString() == "4" ||
                            combotipo.SelectedValue.ToString() == "5" || combotipo.SelectedValue.ToString() == "6")
                    {
                        garantia.ModificarGarantia(combotipo.SelectedValue.ToString(), txtbeneficiario.Text, txtnumcb.Text,
                            txtvalor.Text, txtfechaga.Text + " " + DateTime.Now.ToShortTimeString(), combobt.GetItemText(combobt.SelectedItem), combobt.GetItemText(combobt.SelectedItem),
                            txtcodseguridad.Text, "0", txtlote.Text, txtautorizacion.Text, txtpersona.Text, txtfechaa.Text + " " + DateTime.Now.ToShortTimeString(),
                            txtestable.Text, codigogarantia, txtnumtarjeta.Text, user, txtidentificacion.Text, txttelefono.Text, txtcaducidad.Text);
                        MessageBox.Show("Datos Actualizados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarGarantiasFecha();
                        btnmodificar.Enabled = true;
                        Editar = false;
                        Limpiar();
                        Bloquear();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrio Algo al Editar Datos por: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    if (combotipo.SelectedValue.ToString() == "1")
                    {
                        garantia.InsertarGarantia(combotipo.SelectedValue.ToString(), atencion, txtbeneficiario.Text,
                        txtnumcb.Text, txtvalor.Text, txtfechaga.Text + " " + DateTime.Now.ToShortTimeString(), cbBanco.GetItemText(cbBanco.SelectedItem), "", txtcodseguridad.Text,
                        txtdiasve.Text, txtlote.Text, txtautorizacion.Text, txtpersona.Text, txtfechaa.Text + " " + DateTime.Now.ToShortTimeString(), txtestable.Text, 
                        txtnumtarjeta.Text, user, txtidentificacion.Text, txttelefono.Text, "");
                        MessageBox.Show("Datos Ingresados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarGarantiasFecha();
                        Bloquear();
                        Limpiar();
                    }
                    if (combotipo.SelectedValue.ToString() == "2")
                    {
                        garantia.InsertarGarantia(combotipo.SelectedValue.ToString(), atencion, txtbeneficiario.Text,
                        txtnumcb.Text, txtvalor.Text, txtfechaga.Text + " " + DateTime.Now.ToShortTimeString(), cbBanco.GetItemText(cbBanco.SelectedItem), combobt.GetItemText(combobt.SelectedItem), txtcodseguridad.Text,
                        txtdiasve.Text, txtlote.Text, txtautorizacion.Text, txtpersona.Text, txtfechaa.Text + " " + DateTime.Now.ToShortTimeString(), txtestable.Text, 
                        txtnumtarjeta.Text, user, txtidentificacion.Text, txttelefono.Text, txtcaducidad.Text);
                        MessageBox.Show("Datos Ingresados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarGarantiasFecha();
                        Bloquear();
                        Limpiar();
                    }
                    if (combotipo.SelectedValue.ToString() == "7")
                    {
                        garantia.InsertarGarantia(combotipo.SelectedValue.ToString(), atencion, ""
                            , "", txtvalor.Text, txtfechaga.Text + " " + DateTime.Now.ToShortTimeString(), "", "", "",
                            "0", "", "", "", "", "", "", user, "", "","");
                        MessageBox.Show("Datos Ingresados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarGarantiasFecha();
                        Bloquear();
                        Limpiar();
                    }
                    if (combotipo.SelectedValue.ToString() == "3" || combotipo.SelectedValue.ToString() == "4" ||
                            combotipo.SelectedValue.ToString() == "5" || combotipo.SelectedValue.ToString() == "6")
                    {
                        garantia.InsertarGarantia(combotipo.SelectedValue.ToString(), atencion, txtbeneficiario.Text,
                        txtnumcb.Text, txtvalor.Text, txtfechaga.Text + " " + DateTime.Now.ToShortTimeString(), cbBanco.GetItemText(cbBanco.SelectedItem), combobt.GetItemText(combobt.SelectedItem), txtcodseguridad.Text,
                        "0", txtlote.Text, txtautorizacion.Text, txtpersona.Text, txtfechaa.Text + " " + DateTime.Now.ToShortTimeString(), txtestable.Text, txtnumtarjeta.Text, 
                        user, txtidentificacion.Text, txttelefono.Text, txtcaducidad.Text);
                        MessageBox.Show("Datos Ingresados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarGarantiasFecha();
                        Bloquear();
                        Limpiar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void combotipo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtfechaga.Enabled == true)
                {
                    txtfechaga.Focus();
                }
            }
        }

        private void txtfechaga_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtvalor.Enabled == true)
                {
                    txtvalor.Focus();
                }
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
                if (txtcaducidad.Enabled == true)
                {
                    txtcaducidad.Focus();
                }
            }
        }

        private void txtcodseguridad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txttelefono.Enabled == true)
                {
                    txttelefono.Focus();
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
                if (txtfechaa.Enabled == true)
                {
                    txtfechaa.Focus();
                }
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

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            Limpiar();
            frmAyuda.autorizacion = true;
            frmAyuda x = new frmAyuda();
            x.Show();
            x.FormClosed += CerrarAyuda;
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

        private void checkrepetir_CheckedChanged(object sender, EventArgs e)
        {
            if(checkrepetir.Checked == true)
            {
                txtbeneficiario.Text = txt_ApellidoH1.Text + " " + txt_NombreH1.Text;
            }
            else
            {
                txtbeneficiario.Text = "";
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

        private void txtfechaa_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechave, fechaga;
            fechaga = Convert.ToDateTime(txtfechaa.Text);
            fechave = fechaga.AddDays(Convert.ToInt32(txtdiasve.Text));
            txtfechave.Text = fechave.ToShortDateString();
        }

        private void TablaGarantia_DoubleClick(object sender, EventArgs e)
        {
            Limpiar();
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
            }
            if (index == 1)
            {
                Banco();
                TipoTarjeta();
                index1 = combobt.FindString(Fila.Cells[18].Value.ToString());
                combobt.SelectedIndex = index1;
                index1 = cbBanco.FindString(Fila.Cells[27].Value.ToString());
                cbBanco.SelectedIndex = index1;
            }
            //combobt.SelectedItem = TablaGarantia.CurrentRow.Cells[18].Value.ToString();
            txtcodseguridad.Text = Fila.Cells[19].Value.ToString();
            txtestable.Text = Fila.Cells[20].Value.ToString();
            txtautorizacion.Text = Fila.Cells[21].Value.ToString();
            txtpersona.Text = Fila.Cells[22].Value.ToString();
            txtlote.Text = Fila.Cells[23].Value.ToString();
            txtcedula.Text = Fila.Cells[4].Value.ToString();
            txtnumtarjeta.Text = Fila.Cells[25].Value.ToString();
            string valor = Fila.Cells[31].Value.ToString();
            fechave = fechaa.AddDays(Convert.ToUInt32(txtdiasve.Text));
            txtfechave.Text = fechave.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            Bloquear();
            codigogarantia = Fila.Cells[24].Value.ToString();
            txtcaducidad.Text = Fila.Cells[33].Value.ToString();
            txtidentificacion.Text = Fila.Cells[34].Value.ToString();
            txttelefono.Text = Fila.Cells[35].Value.ToString();
            if (Fila.Cells[31].Value.ToString() == "APROBADO" || Fila.Cells[31].Value.ToString() == "ANULADO")
            {
                txtonserva.Visible = true;
                lblobserva.Visible = true;
                lblfca.Visible = true;
                txtfca.Visible = true;
                txtonserva.Text = Fila.Cells[32].Value.ToString();
                txtfca.Text = Fila.Cells[29].Value.ToString();
                txtonserva.Enabled = false;
                txtfca.Enabled = false;
                btnnuevo.Enabled = false;
            }else
            {
                txtonserva.Visible = true;
                lblobserva.Visible = true;
                txtonserva.Enabled = true;
                btnanular.Enabled = true;
                lblfca.Visible = false;
                txtfca.Visible = false;
                btnmodificar.Enabled = true;
                btnnuevo.Enabled = true;
            }
            //usuario = TablaGarantia.CurrentRow.Cells[26].Value.ToString(); se oculto orque ya no se necesita levatar ese dato
            if (txtdiasve.Text == "" || txtdiasve.Text == " ")
            {
                txtdiasve.Text = "0";
            }
            Editar = true;
        }

        private void TablaGarantia_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            string estado;
            string codigo;
            DateTime hoy;
            hoy = DateTime.Now.Date;
            foreach (Infragistics.Win.UltraWinGrid.UltraGridRow item in TablaGarantia.Rows)
            {
                estado = item.Cells[31].Value.ToString();
                if(estado == "CADUCADO")
                {
                    e.Layout.Override.CellAppearance.BackColor = Color.Orange;
                    codigo = item.Cells["ADG_Codigo"].Value.ToString();
                    garantia.Caduca(codigo, Convert.ToString(DateTime.Now));
                }
                if(estado == "ANULADO")
                {
                    e.Layout.Override.CellAppearance.BackColor = Color.Red;
                }
                if(estado == "APROBADO")
                {
                    e.Layout.Override.CellAppearance.BackColor = Color.FromArgb(70, 168, 67);
                }
                if(estado == "VIGENTE")
                {
                    e.Layout.Override.CellAppearance.BackColor = Color.White;
                }
            }
        }

        private void TablaGarantia_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            string estado;
            DateTime fechas, hoy;
            hoy = DateTime.Now.Date;
            fechas = Convert.ToDateTime(e.Row.Cells["ADG_FECHA_AUT"].Value.ToString());
            fechas = fechas.AddDays(Convert.ToInt32(e.Row.Cells["ADG_DIASVENCIMIENTO"].Value.ToString())); // dias de vencimiento
            estado = e.Row.Cells[31].Value.ToString();

            if (estado == "CADUCADO")
            {
                e.Row.Appearance.BackColor = Color.Orange;
            }
            else if (estado == "ANULADO")
            {
                e.Row.Appearance.BackColor = Color.Red;
            }
            else if (estado == "APROBADO")
            {
                e.Row.Appearance.BackColor = Color.FromArgb(70, 168, 67);
            }
            else if (estado == "VIGENTE")
            {
                e.Row.Appearance.BackColor = Color.White;
            }
            if (fechas.AddDays(-3) == hoy)
            {
                e.Row.Appearance.BackColor = Color.Yellow; //Faltan 3 dias de Caducar
            }
        }

        private void txtcaducidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtcodseguridad.Enabled == true)
                {
                    txtcodseguridad.Focus();
                }
            }
        }

        private void txttelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtidentificacion.Enabled == true)
                {
                    txtidentificacion.Focus();
                }
            }
        }

        private void txtidentificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txtestable.Enabled == true)
                {
                    txtestable.Focus();
                }
            }
        }
    }
}