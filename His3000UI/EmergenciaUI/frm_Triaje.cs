using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using His.Admision;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades.Clases;
using His.Entidades;
using His.Formulario;

namespace His.Emergencia
{
    public partial class frm_Triaje : Form
    {
        public int obstetrica = 1;
        public TextBox txthistoria = new TextBox();
        public TextBox txtatecodigo = new TextBox();
        public TextBox txtaseguradora = new TextBox();
        public bool Editar = false;
        bool emergecia = true;

        public bool cambios = false;
        public frm_Triaje(bool _emergecnia=true)
        {
            InitializeComponent();
            HBotones(true, false, false, false, false);
            emergecia = _emergecnia;
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
        public void HBotones(bool busca, bool nuevo, bool guardar, bool editar, bool cancelar)
        {
            btnBuscar.Enabled = busca;
            btnNuevo.Enabled = nuevo;
            btnGuarda.Enabled = guardar;
            btmodificar.Enabled = editar;
            btnCancela.Enabled = cancelar;
        }
        public void HabilitarBotones(bool nuevo)
        {
            if (!nuevo)
            {
                btnGuarda.Enabled = true;
                btnBuscar.Enabled = false;
                btmodificar.Enabled = false;
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
            txtPerimetro.Text = "0";
            txtGlucosa.Text = "0";
            txtHemoglobina.Text = "0";

            txtIMCorporal.Text = "0";
            txt_Talla.Text = "0";
            txt_PerimetroC.Text = "0";
            txt_DiamPDV.Text = "";
            txt_DiamPIV.Text = "";
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
            chk_SangradoV5.Checked = false;
            this.cmb_Ocular.SelectedItem = null;
            this.cmb_Verbal.SelectedItem = null;
            this.cmb_Motora.SelectedItem = null;
            this.cmb_ReacPDValor.SelectedItem = null;
            this.cmb_ReacPIValor.SelectedItem = null;
        }
        public void PacienteMuerto(bool muerto)
        {
            if (muerto)
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
            ATENCIONES atencion = new ATENCIONES();
            atencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(lblAteCodigo.Text));
            error.Clear();
            bool Valida = true;
            int gbtriaje = 0, cheket = 0;
            if (!rdb_Critico.Checked && !rdb_Emergencia.Checked && !rdb_Muerto.Checked && !rdb_NoUrgente.Checked && !radioButton1.Checked)
                gbtriaje = 1;
            if (!chbAlcoholC.Checked && !chbDrogasC.Checked && !chbOtrasC.Checked)
                cheket = 1;

            if (gbtriaje == 1)
            {
                ValidaError(gbTriaje, "Debe Marcar Por Lo Menos Uno");
                Valida = false;
            }
            if (cheket == 1)
            {
                ValidaError(gbCircunstancias, "Debe Marcar Por Lo Menos Uno");
                Valida = false;
            }
            if (chbOtrasC.Checked && txtOtrasActual.Text == "")
            {
                ValidaError(txtOtrasActual, "Ingrese Cual Es La Circunstancia");
                Valida = false;
            }
            if (chbOtrasC.Checked && txtOtrasActual.Text == "")
            {
                ValidaError(txtOtrasActual, "Ingrese Cual Es La Circunstancia");
                Valida = false;
            }
            if (txtObserEnfer.Text == "")
            {
                ValidaError(txtObserEnfer, "Ingrese La Observación");
                Valida = false;
            }
            //if (txt_PresionA1.Text == "0")
            //{
            //    ValidaError(txt_PresionA1, "Campo Requerido");
            //    Valida = false;
            //}
            //if (txt_PresionA2.Text == "0")
            //{
            //    ValidaError(txt_PresionA2, "Campo Requerido");
            //    Valida = false;
            //}
            //if (txt_FCardiaca.Text == "0")
            //{
            //    ValidaError(txt_FCardiaca, "Campo Requerido");
            //    Valida = false;
            //}
            //if (txt_FResp.Text == "0")
            //{
            //    ValidaError(txt_FResp, "Campo Requerido");
            //    Valida = false;
            //}
            //if (txt_TBucal.Text == "0")
            //{
            //    ValidaError(txt_TBucal, "Campo Requerido");
            //    Valida = false;
            //}
            //if (txt_TAxilar.Text == "0")
            //{
            //    ValidaError(txt_TAxilar, "Campo Requerido");
            //    Valida = false;
            //}
            //if (txt_SaturaO.Text == "0")
            //{
            //    ValidaError(txt_SaturaO, "Campo Requerido");
            //    Valida = false;
            //}
            //if (cmb_Ocular.Text == "")
            //{
            //    ValidaError(cmb_Ocular, "Campo Requerido");
            //    Valida = false;
            //}
            //if (cmb_Verbal.Text == "")
            //{
            //    ValidaError(cmb_Verbal, "Campo Requerido");
            //    Valida = false;
            //}
            //if (cmb_Motora.Text == "")
            //{
            //    ValidaError(cmb_Motora, "Campo Requerido");
            //    Valida = false;
            //}
            //if (txt_Glicemia.Text == "")
            //{
            //    ValidaError(txt_Glicemia, "Campo Requerido");
            //    Valida = false;
            //}
            if(Convert.ToInt32(atencion.TIPO_INGRESOReference.EntityKey.EntityKeyValues[0].Value) == 4)
            {
                if (txt_PesoKG.Text == "0")
                {
                    ValidaError(txt_PesoKG, "Campo Requerido");
                    Valida = false;
                }
                if (txt_Talla.Text == "0")
                {
                    ValidaError(txt_Talla, "Campo Requerido");
                    Valida = false;
                }
            }
            //if (txt_PerimetroC.Text == "0")
            //{
            //    ValidaError(txt_PerimetroC, "Campo Requerido");
            //    Valida = false;
            //}
            //if (cmb_ReacPDValor.Text == "")
            //{
            //    ValidaError(cmb_ReacPDValor, "Campo Requerido");
            //    Valida = false;
            //}
            //if (cmb_ReacPIValor.Text == "")
            //{
            //    ValidaError(cmb_ReacPIValor, "Campo Requerido");
            //    Valida = false;
            //}
            return Valida;
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

            btnNuevo.Enabled = false;
            btnBuscar.Enabled = false;
            btnGuarda.Enabled = false;
            btmodificar.Enabled = true;
            btnBrazzalete.Visible = true;
            if (triaje.Rows[0][3].ToString() == "1")
            {
                rdb_NoUrgente.Checked = true;
                rdb_Emergencia.Checked = false;
                rdb_Critico.Checked = false;
                rdb_Muerto.Checked = false;
                radioButton1.Checked = false;
                gbTriaje.BackColor = Color.Red;
                PacienteMuerto(false);
            }
            else if (triaje.Rows[0][4].ToString() == "1")
            {
                rdb_NoUrgente.Checked = false;
                rdb_Emergencia.Checked = true;
                rdb_Critico.Checked = false;
                rdb_Muerto.Checked = false;
                radioButton1.Checked = false;
                gbTriaje.BackColor = Color.Orange;
                PacienteMuerto(false);
            }
            else if (triaje.Rows[0][5].ToString() == "1")
            {
                rdb_NoUrgente.Checked = false;
                rdb_Emergencia.Checked = false;
                rdb_Critico.Checked = true;
                rdb_Muerto.Checked = false;
                radioButton1.Checked = false;
                gbTriaje.BackColor = Color.Yellow;
                PacienteMuerto(false);
            }
            else if (triaje.Rows[0][6].ToString() == "1")
            {
                rdb_NoUrgente.Checked = false;
                rdb_Emergencia.Checked = false;
                rdb_Critico.Checked = false;
                rdb_Muerto.Checked = true;
                radioButton1.Checked = false;
                gbTriaje.BackColor = Color.Green;
                PacienteMuerto(true);
            }
            else if (triaje.Rows[0][12].ToString() == "1")
            {
                rdb_NoUrgente.Checked = false;
                rdb_Emergencia.Checked = false;
                rdb_Critico.Checked = false;
                rdb_Muerto.Checked = false;
                radioButton1.Checked = true;
                gbTriaje.BackColor = Color.LightSkyBlue;
                PacienteMuerto(false);
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
            string[] valores = signos.Rows[0][3].ToString().Split('.');
            txt_PresionA1.Text = valores[0];
            valores = signos.Rows[0][4].ToString().Split('.');
            txt_PresionA2.Text = valores[0];
            valores = signos.Rows[0][5].ToString().Split('.');
            txt_FCardiaca.Text = valores[0];
            valores = signos.Rows[0][6].ToString().Split('.');
            txt_FResp.Text = valores[0];
            txt_TBucal.Text = signos.Rows[0][7].ToString().Substring(0,4);
            txt_TAxilar.Text = signos.Rows[0][8].ToString().Substring(0, 4);
            valores = signos.Rows[0][9].ToString().Split('.');
            txt_SaturaO.Text = valores[0];
            txt_PesoKG.Text = signos.Rows[0][10].ToString();
            txt_Talla.Text = signos.Rows[0][11].ToString();
            txtIMCorporal.Text = signos.Rows[0][12].ToString();
            txt_PerimetroC.Text = signos.Rows[0][13].ToString();
            valores = signos.Rows[0][14].ToString().Split('.');
            txt_Glicemia.Text = valores[0];
            txt_TotalG.Text = signos.Rows[0][15].ToString();
            cmb_Ocular.Text = signos.Rows[0][16].ToString();
            cmb_Verbal.Text = signos.Rows[0][17].ToString();
            cmb_Motora.Text = signos.Rows[0][18].ToString();
            if(signos.Rows[0][19].ToString()!="-1")
                txt_DiamPDV.Text = signos.Rows[0][19].ToString();
            else
                txt_DiamPDV.Text = "";
            cmb_ReacPDValor.Text = signos.Rows[0][20].ToString();
            if (signos.Rows[0][21].ToString() != "-1")
                txt_DiamPIV.Text = signos.Rows[0][21].ToString();
            else
                txt_DiamPIV.Text = "";
            

            cmb_ReacPIValor.Text = signos.Rows[0][22].ToString();
            txtPerimetro.Text = signos.Rows[0][23].ToString();
            txtHemoglobina.Text = signos.Rows[0][24].ToString();
            txtGlucosa.Text = signos.Rows[0][26].ToString();
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
            dtp_ultimaMenst1.Value = Convert.ToDateTime(obstet.Rows[0][7].ToString());
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
                chk_SangradoV5.Checked = true;
            txt_Contracciones.Text = obstet.Rows[0][20].ToString();

        }

        #endregion

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de salir del Triaje?", "HIS3000", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DataTable paciente = new DataTable();
            frm_AyudaPacientes ayuda = new frm_AyudaPacientes();
            ayuda.campoPadre = txthistoria;
            ayuda.campoAtencion = txtatecodigo;
            //ayuda.campoAseguradora = txtaseguradora;
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
                lblMedico.Text = paciente.Rows[0][4].ToString();
                txtDiagnostico.Text = paciente.Rows[0][5].ToString();
                txtObservacion.Text = paciente.Rows[0][6].ToString();
                HabilitarBotones(true);
                gbPaciente.Visible = true;
                gbDetalle.Visible = true;
                gbSignos.Visible = true;
                P_SignosVitales.Visible = true;
                P_Obstetrica.Visible = true;
                //btnHistoriasClinicas.Visible = true;
                RecuperaDatos(obstetrica);
                gbTriajeObservaciones.Enabled = false;
                gbSignos.Enabled = false;
            }

        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            gbTriajeObservaciones.Enabled = true;
            gbSignos.Enabled = true;
            btnNuevo.Enabled = false;
            btnGuarda.Enabled = true;
            btnBuscar.Enabled = false;
            btmodificar.Enabled = false;
            Editar = false;
            if (obstetrica == 0)
                gbObstetrica.Enabled = true;
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Usted Va A Borrar Toda la Informacion Ingresada.\r\n¿Desea Continuar?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                BloqueaBotones();
                gbTriaje.BackColor = Color.Transparent;
                btnBuscar.Enabled = true;
                btnNuevo.Enabled = false;
                btmodificar.Enabled = false;
                btnGuarda.Enabled = false;
            }

        }

        private void rdb_NoUrgente_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_NoUrgente.Checked == true)
            {
                rdb_Critico.Checked = false;
                rdb_Emergencia.Checked = false;
                rdb_Muerto.Checked = false;
                radioButton1.Checked = false;
                gbTriaje.BackColor = Color.Red;
                PacienteMuerto(false);
            }
        }

        private void rdb_Emergencia_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Emergencia.Checked == true)
            {
                rdb_Critico.Checked = false;
                rdb_NoUrgente.Checked = false;
                rdb_Muerto.Checked = false;
                radioButton1.Checked = false;
                gbTriaje.BackColor = Color.Orange;
                PacienteMuerto(false);
            }
        }

        private void rdb_Critico_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Critico.Checked == true)
            {
                rdb_Emergencia.Checked = false;
                rdb_NoUrgente.Checked = false;
                rdb_Muerto.Checked = false;
                radioButton1.Checked = false;
                gbTriaje.BackColor = Color.Yellow;
                PacienteMuerto(false);
            }
        }

        private void rdb_Muerto_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_Muerto.Checked == true)
            {
                rdb_Emergencia.Checked = false;
                rdb_NoUrgente.Checked = false;
                rdb_Critico.Checked = false;
                radioButton1.Checked = false;
                gbTriaje.BackColor = Color.Green;
                PacienteMuerto(true);
            }
        }

        private void txt_PresionA1_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
            txtKeyPress(e);
            if (e.KeyChar == (char)09)
            {
                txt_PresionA2.Focus();
            }
        }

        private void txt_PresionA2_KeyPress(object sender, KeyPressEventArgs e)
        {
            NegUtilitarios.OnlyNumber(e, false);
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
            TextBox textbox = (TextBox)sender; // Convierto el sender a TextBox
            e.Handled = txtKeyPress(textbox, Convert.ToInt32(e.KeyChar));
            //if (txt_TBucal.Text.Trim() != ".")
            //{
            //    if (txt_TBucal.Text.Trim() != "")
            //    {
            //        if (NegUtilitarios.ValidaTemperatura(Convert.ToDecimal(txt_TBucal.Text)))
            //        {
            //            if (e.KeyChar == (char)09)
            //            {
            //                txt_TAxilar.Focus();
            //            }
            //        }
            //        else
            //            txt_TBucal.Text = "";
            //    }
            //    else
            //        txt_TBucal.Text = "";
            //}
            //else
            //    txt_TBucal.Text = "0";
        }

        private void txt_TAxilar_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender; // Convierto el sender a TextBox
            e.Handled = txtKeyPress(textbox, Convert.ToInt32(e.KeyChar));
            //if(txt_TAxilar.Text.Trim() != ".")
            //{
            //    if(txt_TAxilar.Text.Trim() != "")
            //    {
                    
            //        if (NegUtilitarios.ValidaTemperatura(Convert.ToDecimal(txt_TAxilar.Text)))
            //        {
            //            if (e.KeyChar == (char)09)
            //            {
            //                txt_SaturaO.Focus();
            //            }
            //        }
            //        else
            //            txt_TAxilar.Text = "";
            //    }
            //    else
            //        txt_TAxilar.Text = "";
            //}
            //else
            //    txt_TAxilar.Text = "0";
        }

        private void txt_SaturaO_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
            if (e.KeyChar == (char)09)
            {
                cmb_Ocular.Focus();
            }
        }

        private void txt_PesoKG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '.')
            {
                // Permitir solo un punto decimal y asegurarse de que no esté al inicio
                if (e.KeyChar == '.' && ((TextBox)sender).Text.IndexOf('.') > -1)
                {
                    e.Handled = true; // Bloquear la entrada
                }
                else if (e.KeyChar == '.' && ((TextBox)sender).Text.Length == 0)
                {
                    e.Handled = true; // Bloquear la entrada
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                e.Handled = true; // Bloquear la entrada
            }
        }

        private void txt_Talla_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
            if (e.KeyChar == (char)09)
            {
                txtIMCorporal.Focus();
            }
        }
        private static void OnlyNumber(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789." + Convert.ToChar(8);
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
            TextBox textbox = (TextBox)sender; // Convierto el sender a TextBox
            e.Handled = txtKeyPress(textbox, Convert.ToInt32(e.KeyChar));
            if (e.KeyChar == (char)09)
            {
                txt_Glicemia.Focus();
            }
        }

        private void txt_Glicemia_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
            if (e.KeyChar == (char)09)
            {
                txt_DiamPDV.Focus();
            }
        }

        private void txt_DiamPDV_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
            if (e.KeyChar == (char)09)
            {
                cmb_ReacPDValor.Focus();
            }
        }

        private void txt_DiamPIV_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
            if (e.KeyChar == (char)09)
            {
                cmb_ReacPIValor.Focus();
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
            if (e.KeyChar == (char)09)
            {
                txtObserEnfer.Focus();
            }
        }

        private void txtObserEnfer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                txt_PresionA1.Focus();
            }
        }

        private void txt_PesoKG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Talla.Focus();
            }
        }

        private void txt_Talla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtIMCorporal.Focus();
            }
        }

        private void txt_PesoKG_TextChanged(object sender, EventArgs e)
        {
            if (txt_PesoKG.Text != "0" && txt_PesoKG.Text.Trim() != "")
            {
                if (txt_Talla.Text != "0" && txt_Talla.Text.Trim() != "")
                {
                    double valor = (Convert.ToDouble(txt_PesoKG.Text) / Math.Pow(Convert.ToDouble(txt_Talla.Text), 2));
                    txtIMCorporal.Text = (Math.Round(valor, 2)).ToString();
                }
            }
            else
            {
                txtIMCorporal.Text = "0";
            }
        }

        private void txt_Talla_TextChanged(object sender, EventArgs e)
        {
            double talla1 = 0;
            if (txt_Talla.Text.Length>0)
            talla1 = Convert.ToDouble(txt_Talla.Text);
            if (talla1 > 2.50)
            {
                MessageBox.Show("La talla no puede ser mayor a 2.50 m", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_Talla.Text = "0";
                return;
            }
            if (txt_PesoKG.Text != "0" && txt_PesoKG.Text.Trim() != "")
            {
                if (txt_Talla.Text != "0" && txt_Talla.Text.Trim() != "")
                {
                    double valor = (Convert.ToDouble(txt_PesoKG.Text) / Math.Pow(Convert.ToDouble(txt_Talla.Text), 2));
                    txtIMCorporal.Text = (Math.Round(valor, 2)).ToString();
                }
            }
            else
            {
                txtIMCorporal.Text = "0";
            }
        }

        private void txt_PresionA1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_PresionA2.Focus();
            }
        }

        private void txt_PresionA2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (Convert.ToInt32(txt_PresionA1.Text) < Convert.ToInt32(txt_PresionA2.Text))
                {
                    txt_PresionA2.Text = "0";
                    txt_PresionA2.Focus();
                }
                else
                {
                    txt_FCardiaca.Focus();
                }
            }
        }

        private void txt_FCardiaca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_FResp.Focus();
            }
        }

        private void txt_FResp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_TBucal.Focus();
            }
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

        private void txt_SaturaO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_Ocular.Focus();
            }
        }

        private void cmb_Ocular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_Verbal.Focus();
            }
        }

        private void cmb_Verbal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_Motora.Focus();
            }
        }

        private void cmb_Motora_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Glicemia.Focus();
            }
        }

        private void txt_TotalG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Glicemia.Focus();
            }
        }

        private void txt_Glicemia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_DiamPDV.Focus();
            }
        }

        private void txt_DiamPDV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_ReacPDValor.Focus();
            }
        }

        private void cmb_ReacPDValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_DiamPIV.Focus();
            }
        }

        private void txt_DiamPIV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmb_ReacPIValor.Focus();
            }
        }

        private void cmb_ReacPIValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_PesoKG.Focus();
            }
        }

        private void txtIMCorporal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_PerimetroC.Focus();
            }
        }

        private void txt_PerimetroC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               txtPerimetro.Focus();
            }
        }

        private void txt_Gesta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Partos.Focus();
            }
        }

        private void txt_Partos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Abortos.Focus();
            }
        }

        private void txt_Abortos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Cesareas.Focus();
            }
        }

        private void txt_Cesareas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtp_ultimaMenst1.Focus();
            }
        }

        private void cmb_Ocular_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Ocular.SelectedIndex != -1)
            {
                if (cmb_Verbal.SelectedIndex != -1)
                {
                    if (cmb_Motora.SelectedIndex != -1)
                    {
                        txt_TotalG.Text = Convert.ToString(Convert.ToInt32(cmb_Ocular.Text) + Convert.ToInt32(cmb_Verbal.Text) + Convert.ToInt32(cmb_Motora.Text));
                    }
                    else
                    {
                        txt_TotalG.Text = "0";
                    }
                }
            }
        }

        private void cmb_Verbal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Ocular.SelectedIndex != -1)
            {
                if (cmb_Verbal.SelectedIndex != -1)
                {
                    if (cmb_Motora.SelectedIndex != -1)
                    {
                        txt_TotalG.Text = Convert.ToString(Convert.ToInt32(cmb_Ocular.Text) + Convert.ToInt32(cmb_Verbal.Text) + Convert.ToInt32(cmb_Motora.Text));
                    }
                    else
                    {
                        txt_TotalG.Text = "0";
                    }
                }
            }
        }

        private void cmb_Motora_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Ocular.SelectedIndex != -1)
            {
                if (cmb_Verbal.SelectedIndex != -1)
                {
                    if (cmb_Motora.SelectedIndex != -1)
                    {

                        txt_TotalG.Text = Convert.ToString(Convert.ToInt32(cmb_Ocular.Text) + Convert.ToInt32(cmb_Verbal.Text) + Convert.ToInt32(cmb_Motora.Text));
                    }
                    else
                    {
                        txt_TotalG.Text = "0";
                    }
                }
            }
        }

        private void dtp_ultimaMenst1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_SemanaG.Focus();
            }
        }

        private void txt_SemanaG_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Enter)
            {
                chk_MovimientoF.Focus();
            }
        }

        private void chk_MovimientoF_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txt_FrecCF.Focus();
            }
        }

        private void txt_FrecCF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chk_MembranaS.Focus();
            }
        }

        private void chk_MembranaS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Tiempo.Focus();
            }
        }

        private void txt_Tiempo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_AltU.Focus();
            }
        }

        private void txt_AltU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Presentacion.Focus();
            }
        }

        private void txt_Presentacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Dilatacion.Focus();
            }
        }

        private void txt_Dilatacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Borramiento.Focus();
            }
        }

        private void txt_Borramiento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Plano.Focus();
            }
        }

        private void txt_Plano_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chk_PelvisU.Focus();
            }
        }

        private void chk_PelvisU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Contracciones.Focus();
            }
        }

        private void chk_SangradoV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_Contracciones.Focus();
            }
        }

        private void txt_Gesta_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txt_Partos_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txt_Abortos_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txt_Cesareas_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txt_SemanaG_KeyPress(object sender, KeyPressEventArgs e)
        {
            //txtKeyPress(e);
            NegUtilitarios.OnlyNumberDecimal(e, false);
        }

        private void txt_FrecCF_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
            
        }

        private void txt_AltU_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txt_Dilatacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txt_Borramiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void txt_Presentacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtKeyPress(e);
        }

        private void btmodificar_Click(object sender, EventArgs e)
        {
            Editar = true;
            btnBuscar.Enabled = false;
            btnNuevo.Enabled = false;
            btmodificar.Enabled = false;
            btnGuarda.Enabled = true;
            gbTriajeObservaciones.Enabled = true;
            gbSignos.Enabled = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                rdb_Emergencia.Checked = false;
                rdb_NoUrgente.Checked = false;
                rdb_Critico.Checked = false;
                rdb_Muerto.Checked = false;
                gbTriaje.BackColor = Color.LightSkyBlue;
                PacienteMuerto(false);
            }
        }

        private void btnCancelar1_Click(object sender, EventArgs e)
        {
            if (cambios == true)
            {
                if (MessageBox.Show("Usted Va A Borrar Toda la Informacion Ingresada.\r\n¿Desea Continuar?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    BloqueaBotones();
                    gbTriaje.BackColor = Color.Transparent;
                    btnBuscar.Enabled = true;
                    btnNuevo.Enabled = false;
                    btmodificar.Enabled = false;
                    btnGuarda.Enabled = false;
                    btnBrazzalete.Visible = false;
                }
            }
            else
            {
                BloqueaBotones();
                gbTriaje.BackColor = Color.Transparent;
                btnBuscar.Enabled = true;
                btnNuevo.Enabled = false;
                btmodificar.Enabled = false;
                btnGuarda.Enabled = false;
                btnBrazzalete.Visible = false;
            }
        }

        private void btnNuevo1_Click(object sender, EventArgs e)
        {
            gbTriajeObservaciones.Enabled = true;
            gbSignos.Enabled = true;
            btnNuevo.Enabled = false;
            btnGuarda.Enabled = true;
            btnBuscar.Enabled = false;
            btmodificar.Enabled = false;
            Editar = false;
            cambios = true;
            if (obstetrica == 0)
                gbObstetrica.Enabled = true;
        }
        public string fechaNacimimiento;
        public string fechaAtencion;
        public string id;
        private void btnBuscar1_Click(object sender, EventArgs e)
        {
            DataTable paciente = new DataTable();
            if (mushuñan)
                emergecia = true;
            frm_AyudaPacientes ayuda = new frm_AyudaPacientes(emergecia);
            ayuda.campoPadre = txthistoria;
            ayuda.campoAtencion = txtatecodigo;
            ayuda.campoFecha = fechaNacimimiento;
            ayuda.campoFechaAtencion = fechaAtencion;
            ayuda.campoId = id;
            ayuda.mushunia = mushuñan;
            if(!mushuñan)
                ayuda.triaje = true;
            if (Sesion.codDepartamento == 1)
                ayuda.sistemas = true;
            //ayuda.campoAseguradora = txtaseguradora;
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
                lblMedico.Text = paciente.Rows[0][4].ToString();
                txtDiagnostico.Text = paciente.Rows[0][5].ToString();
                txtObservacion.Text = paciente.Rows[0][6].ToString();
                HabilitarBotones(true);
                gbPaciente.Visible = true;
                gbDetalle.Visible = true;
                gbSignos.Visible = true;
                P_SignosVitales.Visible = true;
                P_Obstetrica.Visible = true;
                //btnHistoriasClinicas.Visible = true;
                RecuperaDatos(obstetrica);
                gbTriajeObservaciones.Enabled = false;
                gbSignos.Enabled = false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Valida())
            {
                if (Editar == false)
                {
                    if (MessageBox.Show("Usted Va A Grabar Esta Información\r\n¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        int alcohol = 0, drogas = 0, otros = 0, nourgente = 0, urgente = 0, critico = 0, muerto = 0, movFetal = 0, memRotas = 0, pelvis = 0, sangrado = 0, urgente2 = 0;

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
                        if (chk_SangradoV5.Checked)
                            sangrado = 1;
                        if (txt_Glicemia.Text == "")
                            txt_Glicemia.Text = "0";
                        if (txt_Plano.Text == "")
                        {
                            txt_Plano.Text = ".";
                        }
                        if (txt_Presentacion.Text == "")
                            txt_Presentacion.Text = "0";

                        if (cmb_Ocular.SelectedIndex == -1)
                        {
                            cmb_Ocular.Text = "0";
                        }
                        if (cmb_Motora.SelectedIndex == -1)
                        {
                            cmb_Motora.Text = "0";
                        }
                        if (cmb_Verbal.SelectedIndex == -1)
                        {
                            cmb_Verbal.Text = "0";
                        }
                        string valor1 = "";
                        string valor2 = "";
                        if (txt_DiamPDV.Text == "")
                        {
                            valor1 = "-1";
                        }
                        else
                            valor1 = txt_DiamPDV.Text;
                        if (txt_DiamPIV.Text == "")
                        {
                            valor2 = "-1";
                        }
                        else
                            valor2 = txt_DiamPIV.Text;

                        if (NegConsultaExterna.GuardaTriajeSignosVitales(lblHistoria.Text, Convert.ToInt64(lblAteCodigo.Text), nourgente, urgente, critico, muerto, alcohol, drogas, otros, txtOtrasActual.Text, txtObserEnfer.Text, Convert.ToDecimal(txt_PresionA1.Text), Convert.ToDecimal(txt_PresionA2.Text), Convert.ToDecimal(txt_FCardiaca.Text), Convert.ToDecimal(txt_FResp.Text), Convert.ToDecimal(txt_TBucal.Text), Convert.ToDecimal(txt_TAxilar.Text), Convert.ToDecimal(txt_SaturaO.Text), Convert.ToDecimal(txt_PesoKG.Text), Convert.ToDecimal(txt_Talla.Text), Convert.ToDecimal(txtIMCorporal.Text), Convert.ToDecimal(txt_PerimetroC.Text), Convert.ToDecimal(txt_Glicemia.Text), Convert.ToDecimal(txt_TotalG.Text), Convert.ToInt16(cmb_Motora.Text), Convert.ToInt16(cmb_Verbal.Text), Convert.ToInt16(cmb_Ocular.Text), Convert.ToInt16(valor1), cmb_ReacPDValor.Text, Convert.ToInt16(valor2), cmb_ReacPIValor.Text, Convert.ToInt16(txt_Gesta.Text), Convert.ToInt16(txt_Partos.Text), Convert.ToInt16(txt_Abortos.Text), Convert.ToInt16(txt_Cesareas.Text), dtp_ultimaMenst1.Value, Convert.ToDecimal(txt_SemanaG.Text), movFetal, Convert.ToInt16(txt_FrecCF.Text), memRotas, txt_Tiempo.Text, Convert.ToInt16(txt_AltU.Text), Convert.ToInt16(txt_Presentacion.Text), Convert.ToInt16(txt_Dilatacion.Text), Convert.ToInt16(txt_Borramiento.Text), txt_Plano.Text, pelvis, sangrado, txt_Contracciones.Text, urgente2, Convert.ToDecimal(txtPerimetro.Text), Convert.ToDecimal(txtHemoglobina.Text), Convert.ToDecimal(txtGlucosa.Text),Sesion.codUsuario))
                        {
                            MessageBox.Show("Información Almacenada Con Exito!!!", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            HabilitarBotones(true);
                            cambios = false;
                            gbTriajeObservaciones.Enabled = false;
                            gbSignos.Enabled = false;
                            Editar = false;
                            HBotones(false, false, false, true, true);
                            btnBrazzalete.Visible = true;
                        }

                        else
                            MessageBox.Show("Información No Almacenada, Consulte Con Sistemas!!!", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (MessageBox.Show("Usted Va A Actualizar Esta Información\r\n¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        int alcohol = 0, drogas = 0, otros = 0, nourgente = 0, urgente = 0, critico = 0, muerto = 0, movFetal = 0, memRotas = 0, pelvis = 0, sangrado = 0, urgente2 = 0;
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
                        if (chk_SangradoV5.Checked)
                            sangrado = 1;
                        if (txt_Plano.Text == "")
                        {
                            txt_Plano.Text = ".";
                        }
                        if (txt_Glicemia.Text == "")
                            txt_Glicemia.Text = "0";
                        try
                        {
                            if (NegConsultaExterna.EditarTriajeSignosVitales(lblHistoria.Text, Convert.ToInt64(lblAteCodigo.Text), nourgente, urgente, critico, muerto, alcohol, drogas, otros, txtOtrasActual.Text, txtObserEnfer.Text, Convert.ToDecimal(txt_PresionA1.Text), Convert.ToDecimal(txt_PresionA2.Text), Convert.ToDecimal(txt_FCardiaca.Text), Convert.ToDecimal(txt_FResp.Text), Convert.ToDecimal(txt_TBucal.Text), Convert.ToDecimal(txt_TAxilar.Text), Convert.ToDecimal(txt_SaturaO.Text), Convert.ToDecimal(txt_PesoKG.Text), Convert.ToDecimal(txt_Talla.Text), Convert.ToDecimal(txtIMCorporal.Text), Convert.ToDecimal(txt_PerimetroC.Text), Convert.ToDecimal(txt_Glicemia.Text), Convert.ToDecimal(txt_TotalG.Text), Convert.ToInt16(cmb_Motora.Text), Convert.ToInt16(cmb_Verbal.Text), Convert.ToInt16(cmb_Ocular.Text), Convert.ToInt16(txt_DiamPDV.Text), cmb_ReacPDValor.Text, Convert.ToInt16(txt_DiamPIV.Text), cmb_ReacPIValor.Text, Convert.ToInt16(txt_Gesta.Text), Convert.ToInt16(txt_Partos.Text), Convert.ToInt16(txt_Abortos.Text), Convert.ToInt16(txt_Cesareas.Text), dtp_ultimaMenst1.Value, Convert.ToInt16(txt_SemanaG.Text), movFetal, Convert.ToInt16(txt_FrecCF.Text), memRotas, txt_Tiempo.Text, Convert.ToInt16(txt_AltU.Text), Convert.ToInt16(txt_Presentacion.Text), Convert.ToInt16(txt_Dilatacion.Text), Convert.ToInt16(txt_Borramiento.Text), txt_Plano.Text, pelvis, sangrado, txt_Contracciones.Text, urgente2, Convert.ToDecimal(txtPerimetro.Text), Convert.ToDecimal(txtHemoglobina.Text), Convert.ToDecimal(txtGlucosa.Text)))
                            {
                                MessageBox.Show("Información Actualizada Con Exito!!!", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                HabilitarBotones(true);
                                cambios = false;
                                gbTriajeObservaciones.Enabled = false;
                                gbSignos.Enabled = false;
                                Editar = false;
                                HBotones(false, false, false, true, true);
                            }

                            else
                                MessageBox.Show("Información No Actualizada, Consulte Con Sistemas!!!", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }
                }
            }


        }

        private void btnModificar1_Click(object sender, EventArgs e)
        {
            Editar = true;
            cambios = true;
            btnBuscar.Enabled = false;
            btnNuevo.Enabled = false;
            btmodificar.Enabled = false;
            btnGuarda.Enabled = true;
            gbTriajeObservaciones.Enabled = true;
            gbSignos.Enabled = true;
            if (lblGenero.Text == "Femenino")
            {
                gbObstetrica.Enabled = true;
            }
            else
                gbObstetrica.Enabled = false;
        }

        private void cmb_Ocular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                cmb_Verbal.Focus();
            }
        }

        private void cmb_Motora_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                txt_Glicemia.Focus();
            }
        }

        private void cmb_Verbal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                cmb_Motora.Focus();
            }
        }

        private void cmb_ReacPDValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                txt_DiamPIV.Focus();
            }
        }

        private void cmb_ReacPIValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)09)
            {
                txt_PesoKG.Focus();
            }
        }
        ATENCIONES ultimaAtencion = null;
        private void btnBrazzalete_Click(object sender, EventArgs e)
        {
            ultimaAtencion = NegAtenciones.RecuperarAtencionID(Convert.ToInt64(lblAteCodigo.Text));
            PACIENTES pac = NegPacientes.recuperarPacientePorAtencion(Convert.ToInt32(lblAteCodigo.Text));
            try
            {
                string HC = lblHistoria.Text;
                string ATE_NUM = lblAteCodigo.Text;
                string xgenero = lblGenero.Text;

                var now = DateTime.Now;
                var birthday = Convert.ToDateTime(pac.PAC_FECHA_NACIMIENTO);
                var yearsOld = now - birthday;

                int years = (int)(yearsOld.TotalDays / 365.25);
                int months = (int)(((yearsOld.TotalDays / 365.25) - years) * 12);

                TimeSpan age = now - birthday;
                DateTime totalTime = new DateTime(age.Ticks);

                #region BRAZZALETES
                Formulario.DSBarCode BC = new Formulario.DSBarCode();
                DataRow Brazzalete;

                Brazzalete = BC.Tables["BarCode"].NewRow();
                Brazzalete["Logo"] = NegUtilitarios.RutaLogo("Brazzalete");
                Brazzalete["Barras"] = pac.PAC_IDENTIFICACION;
                Brazzalete["Empresa"] = Sesion.nomEmpresa;
                Brazzalete["Paciente"] = "PAC.: " + lblApellido.Text + ' ' + lblNombre.Text;
                Brazzalete["Edad"] = "EDAD: " + years + " AÑOS, " + months + " MESES, " + totalTime.Day + " DIAS";
                Brazzalete["Sexo"] = "SEXO: " + xgenero;
                Brazzalete["HC"] = " - HC : " + HC;
                Brazzalete["Identificacion"] = "CEDULA:" + pac.PAC_IDENTIFICACION;
                Brazzalete["F_Ingreso"] = "INGRESO: " + ultimaAtencion.ATE_FECHA_INGRESO;
                if (rdb_NoUrgente.Checked)
                    Brazzalete["Triaje"] = "1";
                else if (rdb_Emergencia.Checked)
                    Brazzalete["Triaje"] = "2";
                else if (rdb_Critico.Checked)
                    Brazzalete["Triaje"] = "3";
                else if (rdb_Muerto.Checked)
                    Brazzalete["Triaje"] = "4";
                else if (radioButton1.Checked)
                    Brazzalete["Triaje"] = "5";
                Brazzalete["Atencion"] = "Nº ATENCIÓN: " + ultimaAtencion.ATE_NUMERO_ATENCION + " HAB.: " + ultimaAtencion.HABITACIONES.hab_Numero;
                Brazzalete["Medico"] = "MÉD.: " + lblMedico.Text;

                //Brazzalete["Referido"] = "REFERIDO: " + ultimaAtencion.TIPO_REFERIDO.TIR_DESCRIPCION;
                string convenios = lblAseguradora.Text;
                //foreach (DataGridViewRow row in gridAseguradoras.Rows)
                //{
                //    convenios += (row.Cells["nomCategoria"].Value.ToString()) + ". ";
                //}
                //Brazzalete["Convenio"] = " - CONVENIO: " + convenios;


                BC.Tables["BarCode"].Rows.Add(Brazzalete);
                frmReportes reporte = new frmReportes(1, "Brazzalete", BC);
                reporte.Show();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se ha podido generar el ticket.\r\nMás detalle: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txt_PresionA1_TextChanged(object sender, EventArgs e)
        {
            if (txt_PresionA1.Text == "" || !NegUtilitarios.ValidaPrecion1(Convert.ToDouble(txt_PresionA1.Text)))
            {
                txt_PresionA1.Text = "0";
            }
        }

        private void txt_PresionA2_TextChanged(object sender, EventArgs e)
        {
            if (txt_PresionA2.Text == "" || !NegUtilitarios.ValidaPrecion2(Convert.ToDouble(txt_PresionA2.Text)))
            {
                txt_PresionA2.Text = "0";
            }
        }

        private void txt_PresionA2_Leave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txt_PresionA1.Text) < Convert.ToInt32(txt_PresionA2.Text))
            {
                txt_PresionA2.Text = "0";
                txt_PresionA2.Focus();
            }
        }

        private void txt_TBucal_Leave(object sender, EventArgs e)
        {
            if (txt_TBucal.Text != "0")
            {
                if (txt_TBucal.Text.Trim() != "")
                {
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

        private void txt_TAxilar_Leave(object sender, EventArgs e)
        {
            if (txt_TAxilar.Text != "0")
            {
                if (txt_TAxilar.Text.Trim() != "")
                {
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

        private void txt_SaturaO_Leave(object sender, EventArgs e)
        {
            int satura = 0;
            if (txt_SaturaO.Text == "")
            {
                satura = 0;
            }
            else
            {
                satura = Convert.ToInt16(txt_SaturaO.Text);
            }
            if (satura < 30 || satura > 100)
            {
                txt_SaturaO.Focus();
                txt_SaturaO.Text = "";
                MessageBox.Show("Saturación de oxigeno no puede ser menor a 30 ni mayor a 100", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void nada_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txt_PresionA1_Enter(object sender, EventArgs e)
        {
            if (txt_PresionA1.Text == "0")
            {
                txt_PresionA1.Text = string.Empty;
            }
        }

        private void txt_PresionA1_Leave(object sender, EventArgs e)
        {
            if (txt_PresionA1.Text.Trim() == string.Empty)
            {
                txt_PresionA1.Text = "0";
            }
        }

        private void txt_FCardiaca_Enter(object sender, EventArgs e)
        {
            if (txt_FCardiaca.Text == "0")
            {
                txt_FCardiaca.Text = string.Empty;
            }
        }

        private void txt_FCardiaca_Leave(object sender, EventArgs e)
        {
            if (txt_FCardiaca.Text.Trim() == string.Empty)
            {
                txt_FCardiaca.Text = "0";
            }
        }

        private void txt_FResp_Enter(object sender, EventArgs e)
        {
            if (txt_FResp.Text == "0")
            {
                txt_FResp.Text = string.Empty;
            }
        }

        private void txt_FResp_Leave(object sender, EventArgs e)
        {
            if (txt_FResp.Text.Trim() == string.Empty)
            {
                txt_FResp.Text = "0";
            }
        }
        public bool mushuñan = false;
        private void frm_Triaje_Load(object sender, EventArgs e)
        {
            List<PERFILES> perfilUsuario = new NegPerfil().RecuperarPerfil(His.Entidades.Clases.Sesion.codUsuario);
            foreach (var item in perfilUsuario)
            {
                List<ACCESO_OPCIONES> accop = NegUtilitarios.ListaAccesoOpcionesPorPerfil(item.ID_PERFIL, 7);
                foreach (var items in accop)
                {
                    if (items.ID_ACCESO == 71110)// se cambia del perfil  29 a opcion 71110// Mario Valencia 14/11/2023 // cambio en seguridades.
                    {
                        mushuñan = true;
                    }
                }
                //if (item.ID_PERFIL == 29)
                //{
                //    if (item.DESCRIPCION.Contains("SUCURSALES")) //se debe tomar en cuenta que si es 29 en otra empresa no actuara de la forma como en la pasteur.
                //        mushuñan = true;
                //}
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

        private void txtPerimetro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtHemoglobina.Focus();
            }
        }

        private void txtHemoglobina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               txtGlucosa.Focus();
            }
        }

        private void txtGlucosa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
        }

        private void txtPerimetro_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender; // Convierto el sender a TextBox
            e.Handled = txtKeyPress(textbox, Convert.ToInt32(e.KeyChar));

            if (e.KeyChar == (char)09)
            {
                txtHemoglobina.Focus();
            }
        }

        private void txtHemoglobina_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender; // Convierto el sender a TextBox
            e.Handled = txtKeyPress(textbox, Convert.ToInt32(e.KeyChar));

            if (e.KeyChar == (char)09)
            {
                    txtGlucosa.Focus();
            }
        }

        private void txtGlucosa_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = (TextBox)sender; // Convierto el sender a TextBox
            e.Handled = txtKeyPress(textbox, Convert.ToInt32(e.KeyChar));

            if (e.KeyChar == (char)09)
            {

            }
        }

        private void txt_FCardiaca_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (txt_FCardiaca.Text == "" || !NegUtilitarios.ValidaFcardiaca(Convert.ToDouble(txt_FCardiaca.Text.Trim())))
            {
                txt_FCardiaca.Text = NegParametros.RecuperaValorParSvXcodigo(56).ToString();
                return;
            }
        }

        private void txt_FResp_Validating(object sender, CancelEventArgs e)
        {
            if (txt_FResp.Text == "" || !NegUtilitarios.ValidaFrespiratoria(Convert.ToDouble(txt_FResp.Text.Trim())))
            {
                txt_FResp.Text = NegParametros.RecuperaValorParSvXcodigo(58).ToString();
            }
        }

        private void txt_PesoKG_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_PesoKG.Text))
            {
                decimal valorIngresado;
                if (decimal.TryParse(txt_PesoKG.Text, out valorIngresado))
                {
                    if (valorIngresado < 0 || valorIngresado > 635)
                    {
                        MessageBox.Show("El valor debe estar entre 0 y 635.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_PesoKG.Text = "0";
                        txt_PesoKG.Focus();
                    }

                }
                else
                {
                    txt_PesoKG.Text = "0";
                    txt_PesoKG.Focus();
                    MessageBox.Show("Ingresa un numero decimal valido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}