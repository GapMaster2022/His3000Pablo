using His.Entidades;
using His.Entidades.Clases;
using His.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Formulario
{
    public partial class frmAnulaCertificado : Form
    {
        internal static bool Eliminar;
        Int32 codigoCertificado = 0;
        MaskedTextBox codMedico;
        MEDICOS medico = null;
        public string tipoCer = "";
        public bool certificado = true;
        public frmAnulaCertificado(Int32 codCertificado, string medico, string _tipoCer = "", bool _certificado = true)
        {
            InitializeComponent();
            codigoCertificado = codCertificado;
            txtmedico.Text = medico;
            tipoCer = _tipoCer;
            certificado = _certificado;
        }

        private void btnBuscaMedico_Click(object sender, EventArgs e)
        {
            List<MEDICOS> medicos = NegMedicos.listaMedicos();
            frm_Ayudas x = new frm_Ayudas(medicos);
            x.ShowDialog();
            if (x.campoPadre.Text != string.Empty)
            {
                codMedico = (x.campoPadre2);
                string cod = x.campoPadre.Text;
                medico = NegMedicos.RecuperaMedicoId(Convert.ToInt32(cod));
                agregarMedico(medico);
            }
        }

        private void agregarMedico(MEDICOS medicoTratante)
        {
            if ((medicoTratante != null))
            {
                txtmedico.Text = medicoTratante.MED_APELLIDO_PATERNO.Trim() + " " + medicoTratante.MED_APELLIDO_MATERNO.Trim()
                    + " " + medicoTratante.MED_NOMBRE1.Trim() + " " + medicoTratante.MED_NOMBRE2.Trim();
            }
        }

        private void btnGuarda_Click(object sender, EventArgs e)
        {
            if (txtmedico.Text == "")
            {
                MessageBox.Show("Se necesita el médico que solicita la inhabilitación del certificado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMotivo.Text == "")
            {
                MessageBox.Show("Se necesita el motivo de inhabilitación del certificado", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmLogin x = new frmLogin();
            x.ShowDialog();
            if (Eliminar == true)
            {
                if (certificado)
                {
                    if (tipoCer == "CM")
                    {
                        if (NegCertificadoMedico.InhabilitaCertificado(txtMotivo.Text + " Usuario que anula: " + Sesion.nomUsuario, txtmedico.Text, codigoCertificado))
                        {
                            MessageBox.Show("Certificado Inhabilitado con exito.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                            MessageBox.Show("Certificado no se pudo Inhabilitar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (tipoCer == "CME")
                    {
                        if (NegCertificadoMedico.InhabilitaCertificadoIESS(txtMotivo.Text + " Usuario que anula: " + Sesion.nomUsuario, txtmedico.Text, codigoCertificado))
                        {
                            MessageBox.Show("Certificado Inhabilitado con exito.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                            MessageBox.Show("Certificado no se pudo Inhabilitar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (tipoCer == "CA")
                    {
                        if (NegCertificadoMedico.InhabilitaCertificadoPrecentacion(txtMotivo.Text + " Usuario que anula: " + Sesion.nomUsuario, txtmedico.Text, codigoCertificado))
                        {
                            MessageBox.Show("Certificado Inhabilitado con exito.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                            MessageBox.Show("Certificado no se pudo Inhabilitar.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    Eliminar = false;
                }
                else
                {
                    RECETAS_ANULADAS obj = new RECETAS_ANULADAS();
                    obj.detalle = txtMotivo.Text;
                    obj.RM_CODIGO = codigoCertificado;
                    obj.Usuario = Sesion.codUsuario;
                    obj.fecha = (DateTime.Now);
                    if (NegCertificadoMedico.InhabilitaRecetamedica(obj))
                    {
                        MessageBox.Show("Receta anulada con exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Receta no se pudo anular vuela a intentar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    Eliminar = false;
                }
            }
            else
            {
                MessageBox.Show("No tienes permitido Inhabilitar ni certificados ni recetas", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancela_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
