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

namespace His.Admision
{
    public partial class frm_ClavePedido : Form
    {
        public int usuarioActual;
        public bool aceptado = false;
        bool _alta = false;
        public frm_ClavePedido(bool alta = false)
        {
            InitializeComponent();
            _alta = alta;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
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
                    if (Sesion.codDepartamento == 10 || Sesion.codDepartamento == 1 || Sesion.codDepartamento == 6)
                    {
                        aceptado = true;
                        usuarioActual = usuario.ID_USUARIO;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No tiene permiso de Revertir/Editar, Comuniquese con el Administrador.",
                         "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        aceptado = false;
                        this.Close();
                    }
                }
                else
                    MessageBox.Show("Usuario y/o Contraseña mal ingresados", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (_alta)
            {
                if (MessageBox.Show("Si continua no de dara el alta al paciente,\r\n¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    usuarioActual = 0;
                    this.Close();
                }
            }
            else
            {
                if (MessageBox.Show("Si continua perdera todo el pedido ingresado anteriormente,\r\n¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    usuarioActual = 0;
                    this.Close();
                }
            }
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtUsuario.Text.Trim() != "")
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
            if (e.KeyCode == Keys.Enter)
            {
                if (txtPassword.Text.Trim() != "")
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
                            var dep = usuario.DEPARTAMENTOSReference.EntityKey.EntityKeyValues[0].Value;
                            int departamento = Convert.ToInt32(dep);
                            if (departamento == 10 || departamento == 1 || departamento == 6)
                            {
                                aceptado = true;
                                usuarioActual = usuario.ID_USUARIO;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("No tiene permiso de Revertir/Editar, Comuniquese con el Administrador.",
                                    "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                aceptado = false;
                                this.Close();
                            }
                            //if (Sesion.codDepartamento == 10 || Sesion.codDepartamento == 1)
                            //{
                            //    aceptado = true;
                            //    usuarioActual = usuario.ID_USUARIO;
                            //    this.Close();
                            //}
                            //else
                            //{
                            //    MessageBox.Show("No tiene permiso de Revertir/Editar, Comuniquese con el Administrador.",
                            //        "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    aceptado = false;
                            //    this.Close();
                            //}

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
