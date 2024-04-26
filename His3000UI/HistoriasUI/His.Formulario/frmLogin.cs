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

namespace His.Formulario
{
    public partial class frmLogin : Form
    {

        bool kardex = false;
        public USUARIOS user;
        public frmLogin()
        {
            InitializeComponent();
        }
        public frmLogin(bool _Kardex = false)
        {
            InitializeComponent();
            kardex = _Kardex;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void txtusuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtclave.Focus();
            }
        }

        private void txtclave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (kardex)
                {
                    ValidaKardex();
                }
                else
                {
                    Aprobar();
                }
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            frm_Evolucion.Eliminar = false;
            this.Close();
        }

        private void btnaceptar_Click(object sender, EventArgs e)
        {
            if (kardex)
            {
                ValidaKardex();
            }
            else
            {
                Aprobar();
            }
        }

        public void Aprobar()
        {
            if (txtusuario.Text == "" || txtusuario.Text == " ")
            {
                MessageBox.Show("Por Favor, ingrese su usuario.", "HI3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtusuario.Focus();
            }
            else if (txtclave.Text == "" || txtclave.Text == " ")
            {
                MessageBox.Show("Por Favor, ingrese la clave.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtclave.Focus();
            }
            else
            {
                USUARIOS usuario = NegUsuarios.ValidarUsuario(txtusuario.Text, txtclave.Text);
                if (usuario != null)
                {
                    DataTable usu = new DataTable();
                    usu = NegUsuarios.ConsultaUsuarioDep(usuario.IDENTIFICACION);
                    if (usu.Rows[0][0].ToString() == "1" || usu.Rows[0][0].ToString() == "5" || usu.Rows[0][0].ToString() == "7" || usu.Rows[0][0].ToString() == "10")
                    {
                        frm_Evolucion.Eliminar = true;
                        frmAnulaCertificado.Eliminar = true;
                        this.Close();
                    }
                    else
                    {
                        frm_Evolucion.Eliminar = false;
                        frmAnulaCertificado.Eliminar = false;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Usuario y/o Contraseña mal ingresados", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void ValidaKardex()
        {
            if (txtusuario.Text == "" || txtusuario.Text == " ")
            {
                MessageBox.Show("Por Favor, ingrese su usuario.", "HI3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtusuario.Focus();
            }
            else if (txtclave.Text == "" || txtclave.Text == " ")
            {
                MessageBox.Show("Por Favor, ingrese la clave.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtclave.Focus();
            }
            else
            {
                USUARIOS usuario = NegUsuarios.ValidarUsuario(txtusuario.Text, txtclave.Text);
                if (usuario != null)
                {
                    user = usuario;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario y/o Contraseña mal ingresados", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
