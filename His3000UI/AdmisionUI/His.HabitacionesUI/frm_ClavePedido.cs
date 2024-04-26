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
using His.Entidades.Clases;

namespace His.HabitacionesUI
{
    public partial class frm_ClavePedido : Form
    {
        public int usuarioActual;
        public bool validador = false;
        public frm_ClavePedido()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                MessageBox.Show("Ingrese un Usuario, Para continuar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUsuario.Focus();                
            }
            else if(txtPassword.Text == "")
            {
                MessageBox.Show("Ingrese Contraseña, Para continuar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPassword.Focus();                
            }
            else
            {
                USUARIOS usuario = NegUsuarios.ValidarUsuario(txtUsuario.Text, txtPassword.Text);
                if (usuario != null)
                {
                    validador = true;
                    usuarioActual = usuario.ID_USUARIO;
                    this.Close();
                }
                else
                    MessageBox.Show("Usuario y/o Contraseña mal ingresados", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Si continua perdera todo el pedido ingresado anteriormente,\r\n¿DESEA CONTINUAR?","HIS3000",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation)==DialogResult.Yes)
            {
                validador = false;
                usuarioActual = 0;
                this.Close();
            }
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(txtUsuario.Text.Trim() != "")
                {
                    txtPassword.Focus();
                }
                else
                {
                    txtUsuario.Text = "";
                    txtUsuario.Focus();
                }
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(txtPassword.Text.Trim() != "")
                {
                    if (txtUsuario.Text == "")
                    {
                        MessageBox.Show("Ingrese un Usuario, Para continuar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtUsuario.Focus();
                    }
                    else if (txtPassword.Text == "")
                    {
                        MessageBox.Show("Ingrese Contraseña, Para continuar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtPassword.Focus();
                    }
                    else
                    {
                        USUARIOS usuario = NegUsuarios.ValidarUsuario(txtUsuario.Text, txtPassword.Text);
                        if (usuario != null)
                        {
                            validador = true;
                            usuarioActual = usuario.ID_USUARIO;
                            this.Close();
                        }
                        else
                            MessageBox.Show("Usuario y/o Contraseña mal ingresados", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    txtPassword.Text = "";
                    txtPassword.Focus();
                }
            }
        }
    }
}
