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
using System.Text.RegularExpressions;

namespace His.Admision
{
    public partial class frm_Fallecido : Form
    {
        public int pac_codigo;
        public bool Validador = false;
        DtoPacienteDatosAdicionales2 datosPaciente2 = new DtoPacienteDatosAdicionales2();
        public frm_Fallecido()
        {
            InitializeComponent();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Fallecido_Load(object sender, EventArgs e)
        {
            if(His.Entidades.Clases.Sesion.codDepartamento == 10 || Entidades.Clases.Sesion.codDepartamento == 1)
            {
                CargarDatosFallecido(pac_codigo);
                Bloquear();
            }
            else
            {
                BloquearTodo();
                CargarDatosFallecido(pac_codigo);
            }
        }
        public void BloquearTodo()
        {
            btnmodificar.Enabled = false;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;

            txtfolio.Enabled = false;
            txtfechafallecio.Enabled = false;
            txtmotivo.Enabled = false;
            txtdiagnostico.Enabled = false;

            txtidentificacion.Enabled = false;
            txtnombreapellido.Enabled = false;
            txttelefono1.Enabled = false;
            txtcelular.Enabled = false;
            txtemail.Enabled = false;
            dtpEntrega.Enabled = false;
        }

        public void Bloquear()
        {
            btnmodificar.Enabled = true;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;

            txtfolio.Enabled = false;
            txtfechafallecio.Enabled = false;
            dtpHora.Enabled = false;
            txtmotivo.Enabled = false;
            txtdiagnostico.Enabled = false;

            txtidentificacion.Enabled = false;
            txtnombreapellido.Enabled = false;
            txttelefono1.Enabled = false;
            txtcelular.Enabled = false;
            txtemail.Enabled = false;
            dtpEntrega.Enabled = false;

            ckbCedula.Enabled = false;
            ckbPasaporte.Enabled = false;
        }
        public void Desbloquear()
        {
            btnmodificar.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;

            txtfolio.Enabled = true;
            txtfechafallecio.Enabled = true;
            dtpHora.Enabled = true;
            txtmotivo.Enabled = true;
            txtdiagnostico.Enabled = true;

            txtidentificacion.Enabled = true;
            txtnombreapellido.Enabled = true;
            txttelefono1.Enabled = true;
            txtcelular.Enabled = true;
            txtemail.Enabled = true;
            dtpEntrega.Enabled = true;

            if (ckbPasaporte.Checked)
            {
                ckbCedula.Checked = false;
                ckbPasaporte.Enabled = true;
            }
            else if (ckbCedula.Checked)
            {
                ckbPasaporte.Checked = false;
                ckbCedula.Enabled = true;
            }
            if (!ckbCedula.Checked && !ckbPasaporte.Checked)
            {
                ckbCedula.Enabled = true;
                ckbPasaporte.Enabled = true;
            }
        }
        public void CargarDatosFallecido(int pacCodigo)
        {
            try
            {
                datosPaciente2 = NegPacienteDatosAdicionales.PDA2_find(Convert.ToInt16(pacCodigo));
                //Cargamos los datos en los cuadros de texto

                //Datos de defuncion
                if(datosPaciente2 != null)
                {
                    txtfolio.Text = datosPaciente2.FOLIO;
                    if (datosPaciente2.FEC_FALLECIDO != "")
                    {
                        txtfechafallecio.Value = Convert.ToDateTime(datosPaciente2.FEC_FALLECIDO);
                        dtpHora.Value = Convert.ToDateTime(datosPaciente2.FEC_FALLECIDO);
                    }
                    else
                    {
                        txtfechafallecio.Value = DateTime.Now;
                        dtpHora.Value = DateTime.Now;
                    }

                    txtmotivo.Text = datosPaciente2.motivo;
                    txtdiagnostico.Text = datosPaciente2.diagnostico;

                    //Datos de acompañante que tramita
                    if (datosPaciente2.id_persona_tramita.Length == 10)
                    {
                        ckbCedula.Checked = true;
                    }
                    else
                    {
                        ckbPasaporte.Checked = true;
                    }
                    txtidentificacion.Text = datosPaciente2.id_persona_tramita;
                    txtnombreapellido.Text = datosPaciente2.nombre_apellido_tramita;
                    txttelefono1.Text = datosPaciente2.telf_convencional;
                    txtcelular.Text = datosPaciente2.REF_TELEFONO_2;
                    txtemail.Text = datosPaciente2.email;
                    if (datosPaciente2.fecha_entrega_documento == Convert.ToDateTime("01/01/0001 00:00:00"))
                    {
                        dtpEntrega.Value = DateTime.Now;
                    }
                    else
                        dtpEntrega.Value = datosPaciente2.fecha_entrega_documento;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if(btnGuardar.Enabled == true)
            {
                if(MessageBox.Show("¿Está seguro de \"Cancelar\" los cambios realizado?", "HIS3000",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    ckbCedula.Checked = false;
                    ckbPasaporte.Checked = false;
                    CargarDatosFallecido(pac_codigo);
                    Bloquear();
                } 
            }

        }

        private void btnmodificar_Click(object sender, EventArgs e)
        {
            Desbloquear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarCampos())
                {
                    if (Valido())
                    {
                        errorProvider1.Clear();
                        DtoPacienteDatosAdicionales2 datosPaciente23 = new DtoPacienteDatosAdicionales2();
                        datosPaciente23.COD_PACIENTE = pac_codigo;
                        datosPaciente23.FALLECIDO = true;
                        string fechaDef = txtfechafallecio.Value.ToShortDateString() + " " + dtpHora.Value.ToShortTimeString();
                        datosPaciente23.FEC_FALLECIDO = fechaDef;
                        datosPaciente23.FOLIO = txtfolio.Text.ToString().Trim();
                        datosPaciente23.REF_TELEFONO_2 = txtcelular.Text.Trim();
                        datosPaciente23.email = txtemail.Text.Trim();
                        datosPaciente23.motivo = txtmotivo.Text.Trim();
                        datosPaciente23.diagnostico = txtdiagnostico.Text.Trim();
                        datosPaciente23.id_persona_tramita = txtidentificacion.Text.Trim();
                        datosPaciente23.nombre_apellido_tramita = txtnombreapellido.Text.Trim();
                        datosPaciente23.telf_convencional = txttelefono1.Text.Trim();
                        datosPaciente23.fecha_entrega_documento = dtpEntrega.Value;
                        datosPaciente23.id_usuario = His.Entidades.Clases.Sesion.codUsuario;
                        NegPacienteDatosAdicionales.PDA2_save(datosPaciente23);
                        MessageBox.Show("Datos Almacenados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Validador = true;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("No se puede guardar, Datos incompletos.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Validador = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public bool Valido()
        {
            //bool valido = false;
            //frm_ClavePedido x = new frm_ClavePedido();
            //x.ShowDialog();
            //if(x.aceptado == true)
            //{
            //    valido = true;
            //}
            //return valido;
            return true;
        }
        public bool ValidarCampos()
        {
            errorProvider1.Clear();
            bool valido = true;
            //if(ckbCedula.Checked == false && ckbPasaporte.Checked == false)
            //{
            //    errorProvider1.SetError(txtidentificacion, "Debe elegir el tipo de identificación");
            //    valido = false;
            //}
            //if(txtfolio.Text.Trim() == "")
            //{
            //    errorProvider1.SetError(txtfolio, "Debe asignar Nro del IEDG");
            //    valido = false;
            //}
            if(txtmotivo.Text.Trim() == "")
            {
                errorProvider1.SetError(txtmotivo, "Debe asignar el motivo de fallecimiento");
                valido = false;
            }
            if(txtdiagnostico.Text.Trim() == "")
            {
                errorProvider1.SetError(txtdiagnostico, "Debe asignar el diagnostico de fallecimiento");
                valido = false;
            }
            //if(txtidentificacion.Text.Trim() == "")
            //{
            //    errorProvider1.SetError(txtidentificacion, "Debe asignar el nro de identificacion del tramitante.");
            //    valido = false;
            //}
            if(txtfechafallecio.Value.Date > DateTime.Now.Date)
            {
                errorProvider1.SetError(txtfechafallecio, "La Fecha no puede ser mayor a la fecha actual.");
                valido = false;
                if(dtpHora.Value.Hour > DateTime.Now.Hour)
                {
                    errorProvider1.SetError(dtpHora, "La hora no puede ser mayor a la hora actual.");
                    valido = false;
                }
            }
            //else
            //{
            //    if(ckbCedula.Checked == true)
            //    {
            //        if (NegValidaciones.esCedulaValida(txtidentificacion.Text) != true)
            //        {
            //            txtidentificacion.Focus();
            //            errorProvider1.SetError(txtidentificacion, "Cédula no es válida.");
            //            valido = false;
            //        }
            //    }
            //}
            //if(txtnombreapellido.Text.Trim() == "")
            //{
            //    errorProvider1.SetError(txtnombreapellido, "Debe asignar nombre y apellido del tramitante");
            //    valido = false;
            //}
            //if(txttelefono1.Text.Trim() == "")
            //{
            //    errorProvider1.SetError(txttelefono1, "Debe asignar el nro convencional del tramitante");
            //    valido = false;
            //}
            //if(txtcelular.Text.Trim() == "")
            //{
            //    errorProvider1.SetError(txtcelular, "Debe asignar el nro móvil del tramitante");
            //    valido = false;
            //}
            //if(txtemail.Text.Trim() == "")
            //{
            //    errorProvider1.SetError(txtemail, "Debe asignar el email del tramitante");
            //    valido = false;
            //}
            //else
            //{
            //    if (ComprobarFormatoEmail(txtemail.Text.Trim()))
            //    {
            //        txtemail.Focus();
            //    }
            //    else
            //    {
            //        errorProvider1.SetError(txtemail, "Email no cumple con el formato.");
            //        txtemail.Focus();
            //        valido = false;
            //    }
            //}
            return valido;
        }
        private static void OnlyNumber(KeyPressEventArgs e, bool isdecimal)
        {
            String aceptados = null;
            if (!isdecimal)
            {
                aceptados = "0123456789" + Convert.ToChar(8);
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

        private void txttelefono1_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }

        private void txtcelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnlyNumber(e, false);
        }

        public static bool ComprobarFormatoEmail(string seMailAComprobar)
        {
            String sFormato;
            sFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(seMailAComprobar, sFormato))
            {
                if (Regex.Replace(seMailAComprobar, sFormato, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbCedula.Checked == true)
            {
                txtidentificacion.Text = "";
                ckbPasaporte.Enabled = false;
                txtidentificacion.MaxLength = 10;
            }
            else
            {
                ckbPasaporte.Enabled = true;
                txtidentificacion.MaxLength = 15;
            }
        }

        private void ckbPasaporte_CheckedChanged(object sender, EventArgs e)
        {
            if(ckbPasaporte.Checked == true)
            {
                txtidentificacion.Text = "";
                ckbCedula.Enabled = false;
                txtidentificacion.MaxLength = 15;
            }
            else
            {
                ckbCedula.Enabled = true;
                txtidentificacion.MaxLength = 10;
            }
        }

        private void txtidentificacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(ckbCedula.Checked == true)
            {
                OnlyNumber(e, false);
            }
        }

        private void txtfolio_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtmotivo.Focus();
            }
        }

        private void txtmotivo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtdiagnostico.Focus();
            }
        }

        private void txtdiagnostico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tabulador2.SelectedTab = tabulador2.Tabs[1];
                txtidentificacion.Focus();
            }
        }

        private void txtidentificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(ckbCedula.Checked == true)
                {
                    if (!NegValidaciones.esCedulaValida(txtidentificacion.Text))
                    {
                        errorProvider1.SetError(txtidentificacion, "Cédula no válida");
                        txtidentificacion.Focus();
                    }
                    else
                    {
                        txtnombreapellido.Focus();
                    }
                }
                else
                {
                    txtnombreapellido.Focus();
                }
            }
        }

        private void txtnombreapellido_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txttelefono1.Focus();
            }
        }

        private void txttelefono1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtcelular.Focus();
            }
        }

        private void txtcelular_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtemail.Focus();
            }
        }

        private void txtemail_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                dtpEntrega.Focus();
            }
        }
    }
}
