using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using His.Admision;
using System.Windows.Forms;
using His.Negocio;

namespace His.ConsultaExterna
{
    public partial class frm_Triaje : Form
    {
        public int obstetrica = 1;
        public TextBox txthistoria = new TextBox();
        public TextBox txtatecodigo = new TextBox();
        public TextBox txtaseguradora = new TextBox();
        public frm_Triaje()
        {
            InitializeComponent();
        }

        #region FUNCIONES Y OBJETOS
        Boolean permitir = true;
        public bool txtKeyPress(TextBox textbox, int code)
        {

            bool resultado;

            if (code == 46 && textbox.Text.Contains("."))//se evalua si es punto y si es punto se rebiza si ya existe en el textbox
            {
                resultado = true;
            }
            else if ((((code >= 48) && (code <= 57)) || (code == 8) || code == 46)) //se evaluan las teclas validas
            {
                resultado = false;
            }
            else if (!permitir)
            {
                resultado = permitir;
            }
            else
            {
                resultado = true;
            }

            return resultado;

        }
        public void HabilitarBotones(bool nuevo)
        {                     
            if (!nuevo)
            {
                
                btnGuarda.Enabled = true;                
                btnBuscar.Enabled = true;
            }
            else
            {                
                btnNuevo.Enabled = true;
                btnBuscar.Enabled = false;
            }
            btnCancela.Enabled = true;
        }

        public void BloqueaBotones()
        {
            btnHistoriasClinicas.Visible = false;
            btnGuarda.Enabled = false;            
            btnNuevo.Enabled = false;
            btnBuscar.Enabled = true;
            gbSignos.Enabled = false;
            gbTriajeObservaciones.Enabled = false;
            gbObstetrica.Enabled = false;
            gbPaciente.Visible = false;
            gbDetalle.Visible = false;
            LimpiaControles();
        }
        public void LimpiaControles()
        {
            rdb_NoUrgente.Checked = false;
            rdb_Emergencia.Checked = false;
            rdb_Critico.Checked = false;
            rdb_Muerto.Checked = false;
            chbAlcoholC.Checked = false;
            chbDrogasC.Checked = false;
            chbOtrasC.Checked = false;
            txtOtrasActual.Text = "";
            txtObserEnfer.Text = "";
            txt_PresionA1.Text = "0";
            txt_PresionA2.Text = "0";
            txt_FCardiaca.Text = "0";
            txt_FResp.Text = "0";
            txt_TBucal.Text = "0";
            txt_TAxilar.Text = "0";
            txt_SaturaO.Text = "0";
            txt_TotalG.Text = "0";
            txt_Glicemia.Text = "";
            txt_PesoKG.Text = "0";
            txtIMCorporal.Text = "0";
            txt_Talla.Text = "0";
            txt_PerimetroC.Text = "0";
            txt_DiamPDV.Text = "3";
            txt_DiamPIV.Text = "3";
            txt_Gesta.Text = "0";
            txt_Partos.Text = "0";
            txt_Abortos.Text = "0";
            txt_Cesareas.Text = "0";
            txt_SemanaG.Text = "0";
            txt_FrecCF.Text = "0";
            txt_Tiempo.Text = "";
            txt_AltU.Text = "0";
            txt_Presentacion.Text = "";
            txt_Dilatacion.Text = "0";
            txt_Borramiento.Text = "0";
            txt_Plano.Text = "";
            txt_Contracciones.Text = "";
            chk_PelvisU.Checked = false;
            chk_MovimientoF.Checked = false;
            chk_MembranaS.Checked = false;
            chk_SangradoV.Checked = false;
            this.cmb_Ocular.SelectedItem = null;
            this.cmb_Verbal.SelectedItem = null;
            this.cmb_Motora.SelectedItem = null;
            this.cmb_ReacPDValor.SelectedItem = null;
            this.cmb_ReacPIValor.SelectedItem = null;
        }
        public void PacienteMuerto(bool muerto)
        {
            if(muerto)
            {
                rdb_Critico.ForeColor = Color.White;
                rdb_Emergencia.ForeColor = Color.White;
                rdb_NoUrgente.ForeColor = Color.White;
                rdb_Muerto.ForeColor = Color.White;
                gbTriaje.ForeColor = Color.White;
            }
            else
            {
                rdb_Critico.ForeColor = Color.Black;
                rdb_Emergencia.ForeColor = Color.Black;
                rdb_NoUrgente.ForeColor = Color.Black;
                rdb_Muerto.ForeColor = Color.Black;
                gbTriaje.ForeColor = Color.Black;
            }
        }
        private void txtKeyPress(KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
              if (Char.IsControl(e.KeyChar)) 
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public bool Valida()
        {
            int gbtriaje = 0, cheket=0;
            if (!rdb_Critico.Checked && !rdb_Emergencia.Checked && !rdb_Muerto.Checked && !rdb_NoUrgente.Checked)
                gbtriaje = 1;
            if (!chbAlcoholC.Checked && !chbDrogasC.Checked && !chbOtrasC.Checked)
                cheket = 1;

            if (gbtriaje==1)
            {
                ValidaError(gbTriaje, "Debe Marcar Por Lo Menos Uno");
                return false;
            }
            error.Clear();
            if (cheket == 1)
            {
                ValidaError(gbCircunstancias, "Debe Marcar Por Lo Menos Uno");
                return false;
            }
            error.Clear();
            if (chbOtrasC.Checked && txtOtrasActual.Text=="")
            {
                ValidaError(txtOtrasActual, "Ingrese Cual Es La Circunstancia");
                return false;
            }
            error.Clear();
            if (chbOtrasC.Checked && txtOtrasActual.Text == "")
            {
                ValidaError(txtOtrasActual, "Ingrese Cual Es La Circunstancia");
                return false;
            }
            error.Clear();
            if (txtObserEnfer.Text == "")
            {
                ValidaError(txtObserEnfer, "Ingrese La Observación");
                return false;
            }
            error.Clear();
            if (txt_PresionA1.Text == "0")
            {
                ValidaError(txt_PresionA1, "Campo Requerido");
                return false;
            }
            error.Clear();
            if (txt_PresionA2.Text == "0")
            {
                ValidaError(txt_PresionA2, "Campo Requerido");
                return false;
            }
            error.Clear();
            if (txt_FCardiaca.Text == "0")
            {
                ValidaError(txt_FCardiaca, "Campo Requerido");
                return false;
            }
            error.Clear();
            if (txt_FResp.Text == "0")
            {
                ValidaError(txt_FResp, "Campo Requerido");
                return false;
            }
            error.Clear();
            if (txt_TBucal.Text == "0")
            {
                ValidaError(txt_TBucal, "Campo Requerido");
                return false;
            }
            error.Clear();
            if (txt_TAxilar.Text == "0")
            {
                ValidaError(txt_TAxilar, "Campo Requerido");
                return false;
            }
            error.Clear();
            if (txt_SaturaO.Text == "0")
            {
                ValidaError(txt_SaturaO, "Campo Requerido");
                return false;
            }
            error.Clear();
            if (cmb_Ocular.Text == "")
            {
                ValidaError(cmb_Ocular, "Campo Requerido");
                return false;
            }
            error.Clear();
            if (cmb_Verbal.Text == "")
            {
                ValidaError(cmb_Verbal, "Campo Requerido");
                return false;
            }
            error.Clear();
            if (cmb_Motora.Text == "")
            {
                ValidaError(cmb_Motora, "Campo Requerido");
                return false;
            }
            error.Clear();
            if (txt_Glicemia.Text == "")
            {
                ValidaError(txt_Glicemia, "Campo Requerido");
                return false;
            }
            error.Clear();            
            if (txt_PesoKG.Text == "0")
            {
                ValidaError(txt_PesoKG, "Campo Requerido");
                return false;
            }
            error.Clear();
            if (txt_Talla.Text == "0")
            {
                ValidaError(txt_Talla, "Campo Requerido");
                return false;
            }
            error.Clear();
            if (txt_PerimetroC.Text == "0")
            {
                ValidaError(txt_PerimetroC, "Campo Requerido");
                return false;
            }
            error.Clear();
            if (cmb_ReacPDValor.Text == "")
            {
                ValidaError(cmb_ReacPDValor, "Campo Requerido");
                return false;
            }
            error.Clear();
            if (cmb_ReacPIValor.Text == "")
            {
                ValidaError(cmb_ReacPIValor, "Campo Requerido");
                return false;
            }
            error.Clear();
            return true;
        }
        public void ValidaError(Control control, string campo)
        {
            error.SetError(control, campo);
        }

        public void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            Form formulario;
            
            formulario = P_Central.Controls.OfType<MiForm>().FirstOrDefault();            
            formulario = new MiForm();
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            P_Central.Controls.Add(formulario);
            P_Central.Tag = formulario;
            formulario.Show();
            formulario.BringToFront();
            


        }


        public void RecuperaDatos(int obstetrica)
        {
            DataTable triaje = new DataTable();
            DataTable signos = new DataTable();
            DataTable obstet = new DataTable();
            triaje = NegConsultaExterna.RecuperaTriaje(Convert.ToInt64(lblAteCodigo.Text));
            signos = NegConsultaExterna.RecuperaSignos(Convert.ToInt64(lblAteCodigo.Text));
            if (triaje.Rows.Count == 0)
                return;
            
            if (triaje.Rows[0][3].ToString() == "1")
            {
                rdb_NoUrgente.Checked = true;
                rdb_Emergencia.Checked = false;
                rdb_Critico.Checked = false;
                rdb_Muerto.Checked = false;
                gbTriaje.BackColor = Color.LightGreen;
                PacienteMuerto(false);
            }
            else if (triaje.Rows[0][4].ToString() == "1")
            {
                rdb_NoUrgente.Checked = false;
                rdb_Emergencia.Checked = true;
                rdb_Critico.Checked = false;
                rdb_Muerto.Checked = false;
                gbTriaje.BackColor = Color.LightSalmon;
                PacienteMuerto(false);
            }
            else if (triaje.Rows[0][5].ToString() == "1")
            {
                rdb_NoUrgente.Checked = false;
                rdb_Emergencia.Checked = false;
                rdb_Critico.Checked = true;
                rdb_Muerto.Checked = false;
                gbTriaje.BackColor = Color.DarkRed;
                PacienteMuerto(true);
            }
            else
            {
                rdb_NoUrgente.Checked = false;
                rdb_Emergencia.Checked = false;
                rdb_Critico.Checked = false;
                rdb_Muerto.Checked = true;
                gbTriaje.BackColor = Color.Black;
                PacienteMuerto(true);
            }
            if (triaje.Rows[0][7].ToString() == "1")
            {
                chbAlcoholC.Checked = true;
            }
            if (triaje.Rows[0][8].ToString() == "1")
            {
                chbDrogasC.Checked = true;
            }
            if (triaje.Rows[0][9].ToString() == "1")
            {
                chbOtrasC.Checked = true;
            }
            txtOtrasActual.Text = triaje.Rows[0][10].ToString();
            txtObserEnfer.Text = triaje.Rows[0][11].ToString();

            /////////SIGNOS VITALES\\\\\\\\\\\\\

            txt_PresionA1.Text = signos.Rows[0][3].ToString();
            txt_PresionA2.Text = signos.Rows[0][4].ToString();
            txt_FCardiaca.Text = signos.Rows[0][5].ToString();
            txt_FResp.Text = signos.Rows[0][6].ToString();
            txt_TBucal.Text = signos.Rows[0][7].ToString();
            txt_TAxilar.Text = signos.Rows[0][8].ToString();
            txt_SaturaO.Text = signos.Rows[0][9].ToString();
            txt_PesoKG.Text = signos.Rows[0][10].ToString();
            txt_Talla.Text = signos.Rows[0][11].ToString();
            txtIMCorporal.Text = signos.Rows[0][12].ToString();
            txt_PerimetroC.Text = signos.Rows[0][13].ToString();
            txt_Glicemia.Text = signos.Rows[0][14].ToString();
            txt_TotalG.Text = signos.Rows[0][15].ToString();
            cmb_Ocular.Text = signos.Rows[0][16].ToString();
            cmb_Verbal.Text = signos.Rows[0][17].ToString();
            cmb_Motora.Text = signos.Rows[0][18].ToString();
            txt_DiamPDV.Text = signos.Rows[0][19].ToString();
            cmb_ReacPDValor.Text = signos.Rows[0][20].ToString();
            txt_DiamPIV.Text = signos.Rows[0][21].ToString();
            cmb_ReacPIValor.Text = signos.Rows[0][22].ToString();

            /////////OBSTETRICIA\\\\\\\\\\\\\

            if (obstetrica == 0)
            {
                obstet = NegConsultaExterna.RecuperaObstetrica(Convert.ToInt64(lblAteCodigo.Text));
            }
            else
                return;

            txt_Gesta.Text = obstet.Rows[0][3].ToString();
            txt_Partos.Text = obstet.Rows[0][4].ToString();
            txt_Abortos.Text = obstet.Rows[0][5].ToString();
            txt_Cesareas.Text = obstet.Rows[0][6].ToString();
            dtp_ultimaMenst1.Value= Convert.ToDateTime(obstet.Rows[0][7].ToString());
            txt_SemanaG.Text = obstet.Rows[0][8].ToString();
            if (obstet.Rows[0][9].ToString() == "1")
                chk_MovimientoF.Checked = true;
            txt_FrecCF.Text = obstet.Rows[0][10].ToString();
            if (obstet.Rows[0][11].ToString() == "1")
                chk_MembranaS.Checked = true;
            txt_Tiempo.Text = obstet.Rows[0][12].ToString();
            txt_AltU.Text = obstet.Rows[0][13].ToString();
            txt_Presentacion.Text = obstet.Rows[0][14].ToString();
            txt_Dilatacion.Text = obstet.Rows[0][15].ToString();
            txt_Borramiento.Text = obstet.Rows[0][16].ToString();
            txt_Plano.Text = obstet.Rows[0][17].ToString();
            if (obstet.Rows[0][18].ToString() == "1")
                chk_PelvisU.Checked = true;
            if (obstet.Rows[0][19].ToString() == "1")
                chk_SangradoV.Checked = true;
            txt_Contracciones.Text = obstet.Rows[0][20].ToString();
        }

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¡Si Sale Perdera Los Cambios Realizados!", "HIS3000", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DataTable paciente = new DataTable();
            frmAyudaPacientesFacturacion ayuda = new frmAyudaPacientesFacturacion();
            ayuda.campoPadre = txthistoria;
            ayuda.campoAtencion = txtatecodigo;
            ayuda.campoAseguradora = txtaseguradora;
            ayuda.ShowDialog();
            if (txthistoria.Text != "")
            {
                paciente = NegConsultaExterna.RecuperaPaciente(Convert.ToInt64(txtatecodigo.Text));
                lblHistoria.Text = txthistoria.Text;
                lblAteCodigo.Text = txtatecodigo.Text;
                lblAseguradora.Text = txtaseguradora.Text;
                lblNombre.Text = paciente.Rows[0][0].ToString();
                lblApellido.Text = paciente.Rows[0][1].ToString();
                lblEdad.Text = paciente.Rows[0][2].ToString();
                if (paciente.Rows[0][3].ToString().Trim() == "M")
                {
                    lblGenero.Text = "Masculino";
                    obstetrica = 1;
                }
                else
                {
                    lblGenero.Text = "Femenino";
                    obstetrica = 0;
                }
                lblMedico.Text= paciente.Rows[0][4].ToString();
                txtDiagnostico.Text = paciente.Rows[0][5].ToString();
                txtObservacion.Text = paciente.Rows[0][6].ToString();
                HabilitarBotones(true);
                gbPaciente.Visible = true;
                gbDetalle.Visible = true;
                P_SignoT.Visible = true;
                P_SignosVitales.Visible = true;
                P_Obstetrica.Visible = true;
                btnHistoriasClinicas.Visible = true;
                RecuperaDatos(obstetrica);                
            }
           
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            gbTriajeObservaciones.Enabled = true;
            gbSignos.Enabled = true;
            btnNuevo.Enabled = false;
            btnGuarda.Enabled = true;
            if (obstetrica == 0)
                gbObstetrica.Enabled = true;
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Usted Va A Borrar Toda la Informacion Ingresada.\r\n¿Desea Continuar?","HIS3000",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                BloqueaBotones();
            }
                    
        }

        private void rdb_NoUrgente_CheckedChanged(object sender, EventArgs e)
        {
            rdb_Critico.Checked = false;
            rdb_Emergencia.Checked = false;
            rdb_Muerto.Checked = false;
            gbTriaje.BackColor = Color.LightGreen;
            PacienteMuerto(false);
        }

        private void rdb_Emergencia_CheckedChanged(object sender, EventArgs e)
        {
            rdb_Critico.Checked = false;
            rdb_NoUrgente.Checked = false;
            rdb_Muerto.Checked = false;
            gbTriaje.BackColor = Color.LightSalmon;
            PacienteMuerto(false);
        }

        private void rdb_Critico_CheckedChanged(object sender, EventArgs e)
        {
            rdb_Emergencia.Checked = false;
            rdb_NoUrgente.Checked = false;
            rdb_Muerto.Checked = false;
            gbTriaje.BackColor = Color.DarkRed;
            PacienteMuerto(false);
        }

        private void rdb_Muerto_CheckedChanged(object sender, EventArgs e)
        {
            rdb_Emergencia.Checked = false;
            rdb_NoUrgente.Checked = false;
            rdb_Critico.Checked = false;
            gbTriaje.BackColor = Color.Black;
            PacienteMuerto(true);
        }

        private void txt_PresionA1_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
            if (e.KeyChar == (char)09)
            {
                txt_PresionA2.Focus();
            }
        }

        private void txt_PresionA2_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
            txtKeyPress(e);
            if (e.KeyChar == (char)09)
            {
                txt_FCardiaca.Focus();
            }
        }

        private void txt_FCardiaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
            if (e.KeyChar == (char)09)
            {
                txt_FResp.Focus();
            }
        }

        private void txt_FResp_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
            if (e.KeyChar == (char)09)
            {
                txt_TBucal.Focus();
            }
        }

        private void txt_TBucal_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txt_TAxilar_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender; // Convierto el sender a TextBox
            e.Handled = txtKeyPress(textbox, Convert.ToInt32(e.KeyChar));

            if (e.KeyChar == (char)09)
            {
                txt_SaturaO.Focus();
            }
        }

        private void txt_SaturaO_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
            if (e.KeyChar == (char)09)
            {
                txt_PesoKG.Focus();
            }
        }

        private void txt_PesoKG_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender; // Convierto el sender a TextBox
            e.Handled = txtKeyPress(textbox, Convert.ToInt32(e.KeyChar));
            if (!string.IsNullOrEmpty(txt_PesoKG.Text))
            {
                decimal valorIngresado;
                if (decimal.TryParse(txt_PesoKG.Text, out valorIngresado))
                {
                    if (valorIngresado < 0 || valorIngresado > 635)
                    {
                        MessageBox.Show("El valor debe estar entre 0 y 635.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        txt_PesoKG.Text = "0";
                    }
                }
                else
                {
                    MessageBox.Show("Ingresa un numero decimal valido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            if (e.KeyChar == (char)09)
            {
                txt_Talla.Focus();
            }
        }

        private void txt_Talla_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
            if (e.KeyChar == (char)09)
            {
                txtIMCorporal.Focus();
            }
        }

        private void txtIMCorporal_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
            if (e.KeyChar == (char)09)
            {
                txt_PerimetroC.Focus();
            }
        }

        private void txt_PerimetroC_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
            if (e.KeyChar == (char)09)
            {
                txt_Glicemia.Focus();
            }
        }

        private void txt_Glicemia_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txt_DiamPDV_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txt_DiamPIV_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void btnGuarda_Click(object sender, EventArgs e)
        {
            if(Valida())
            {
                if(MessageBox.Show("Usted Va A Grabar Esta Información\r\n¿DESEA CONTINUAR?","HIS3000",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes)
                {
                    int alcohol=0, drogas=0, otros=0, nourgente=0, urgente=0, critico=0, muerto=0, movFetal=0,memRotas=0,pelvis=0,sangrado=0, urgente2 = 0;
                    if (chbAlcoholC.Checked)
                        alcohol = 1;
                    if (chbDrogasC.Checked)
                        drogas = 1;
                    if (chbOtrasC.Checked)
                        otros = 1;
                    if (rdb_NoUrgente.Checked)
                        nourgente = 1;
                    else if (rdb_Emergencia.Checked)
                        urgente = 1;
                    else if (rdb_Critico.Checked)
                        critico = 1;
                    else if (rdb_Muerto.Checked)
                        muerto = 1;
                    else
                        urgente2 = 1;

                    if (chk_MovimientoF.Checked)
                        movFetal = 1;
                    if (chk_MembranaS.Checked)
                        memRotas = 1;
                    if (chk_PelvisU.Checked)
                        pelvis = 1;
                    if (chk_SangradoV.Checked)
                        sangrado = 1;

                    if (NegConsultaExterna.GuardaTriajeSignosVitales(lblHistoria.Text, Convert.ToInt64(lblAteCodigo.Text), nourgente, urgente, critico, muerto, alcohol, drogas, otros, txtOtrasActual.Text, txtObserEnfer.Text, Convert.ToDecimal(txt_PresionA1.Text), Convert.ToDecimal(txt_PresionA2.Text), Convert.ToDecimal(txt_FCardiaca.Text), Convert.ToDecimal(txt_FResp.Text), Convert.ToDecimal(txt_TBucal.Text), Convert.ToDecimal(txt_TAxilar.Text), Convert.ToDecimal(txt_SaturaO.Text), Convert.ToDecimal(txt_PesoKG.Text), Convert.ToDecimal(txt_Talla.Text), Convert.ToDecimal(txtIMCorporal.Text), Convert.ToDecimal(txt_PerimetroC.Text), Convert.ToDecimal(txt_Glicemia.Text), Convert.ToDecimal(txt_TotalG.Text), Convert.ToInt16(cmb_Motora.Text), Convert.ToInt16(cmb_Verbal.Text), Convert.ToInt16(cmb_Ocular.Text), Convert.ToInt16(txt_DiamPDV.Text), cmb_ReacPDValor.Text, Convert.ToInt16(txt_DiamPIV.Text), cmb_ReacPIValor.Text, Convert.ToInt16(txt_Gesta.Text), Convert.ToInt16(txt_Partos.Text), Convert.ToInt16(txt_Abortos.Text), Convert.ToInt16(txt_Cesareas.Text), dtp_ultimaMenst1.Value, Convert.ToInt16(txt_SemanaG.Text), movFetal, Convert.ToInt16(txt_FrecCF.Text), memRotas, txt_Tiempo.Text, Convert.ToInt16(txt_AltU.Text), Convert.ToInt16(txt_Presentacion.Text), Convert.ToInt16(txt_Dilatacion.Text), Convert.ToInt16(txt_Borramiento.Text), txt_Plano.Text, pelvis, sangrado, txt_Contracciones.Text, urgente2,0,0,0, His.Entidades.Clases.Sesion.codUsuario))
                        MessageBox.Show("Información Almacenada Con Exito!!!", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Información No Almacenada, Consulte Con Sistemas!!!", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHistoriasClinicas_Click(object sender, EventArgs e)
        {
            frm_ExploradorFormularios formulario = new frm_ExploradorFormularios();
            formulario.txt_historiaclinica.Text = lblHistoria.Text;
            formulario.Show();

        }

        private void txtOtrasActual_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)09)
            //{
            //    txtObserEnfer.Focus();
            //}
        }

        private void txtObserEnfer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                txt_PresionA1.Focus();
            }
        }

        private void txt_TBucal_Validating(object sender, CancelEventArgs e)
        {
            if (txt_TBucal.Text.Trim() != ".")
            {
                if (txt_TBucal.Text.Trim() != "")
                {
                    if (NegUtilitarios.ValidaTemperatura(Convert.ToDecimal(txt_TBucal.Text)))
                    {
                        txt_SaturaO.Focus();
                    }
                    else
                        txt_TBucal.Text = "0";
                }
                else
                    txt_TBucal.Text = "";
            }
            else
                txt_TBucal.Text = "0";
        }

        private void txt_TAxilar_Validating(object sender, CancelEventArgs e)
        {
            if (txt_TAxilar.Text.Trim() != ".")
            {
                if (txt_TAxilar.Text.Trim() != "")
                {

                    if (NegUtilitarios.ValidaTemperatura(Convert.ToDecimal(txt_TAxilar.Text)))
                    {
                        //if (e.KeyChar == (char)09)
                        //{
                        //    txt_SaturaO.Focus();
                        //}
                    }
                    else
                        txt_TAxilar.Text = "0";
                }
                else
                    txt_TAxilar.Text = "";
            }
            else
                txt_TAxilar.Text = "0";
        }

        private void txt_TBucal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                //txt_TAxilar.Focus();
                if (txt_TBucal.Text != "0")
                {
                    if (txt_TBucal.Text.Trim() != "")
                    {
                        if (txt_TBucal.Text.Trim().Substring(txt_TBucal.Text.Length - 1, 1) == ".")
                            txt_TBucal.Text = txt_TBucal.Text.Remove(txt_TBucal.Text.Length - 1);
                        txt_TAxilar.Enabled = false;
                        txt_SaturaO.Focus();
                    }
                    else
                    {
                        txt_TBucal.Text = "0";
                        txt_TAxilar.Enabled = true;
                        txt_TAxilar.Focus();
                    }

                }
                else
                {
                    txt_TBucal.Text = "0";
                    txt_TBucal.Enabled = false;
                    txt_TAxilar.Enabled = true;
                    txt_TAxilar.Focus();
                }
            }
        }

        private void txt_TAxilar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (txt_TAxilar.Text != "0")
                {
                    if (txt_TAxilar.Text.Trim() != "")
                    {
                        if (txt_TAxilar.Text.Trim().Substring(txt_TAxilar.Text.Length - 1, 1) == ".")
                            txt_TAxilar.Text = txt_TAxilar.Text.Remove(txt_TAxilar.Text.Length - 1);
                        txt_TBucal.Enabled = false;
                        txt_SaturaO.Focus();
                    }

                    else
                    {
                        txt_TAxilar.Text = "0";
                        txt_TBucal.Enabled = true;
                        txt_SaturaO.Focus();
                    }
                }
                else
                {
                    txt_TAxilar.Text = "0";
                    txt_TBucal.Enabled = true;
                    txt_SaturaO.Focus();
                }
            }
        }
    }
}
