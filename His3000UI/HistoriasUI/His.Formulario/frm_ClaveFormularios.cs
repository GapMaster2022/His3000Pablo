using His.Entidades;
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
    public partial class frm_ClaveFormularios : Form
    {
        public int usuarioActual;
        public bool aceptado = false;
        public int codDepar;
        public bool modificar = false;
        string _formulario = "";
        public frm_ClaveFormularios()
        {
            InitializeComponent();
        }
        public frm_ClaveFormularios(bool habilitar = false)
        {
            InitializeComponent();
            modificar = habilitar;
        }
        public frm_ClaveFormularios(string formulario = "")
        {
            InitializeComponent();
            _formulario = formulario;
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
                    var dep = usuario.DEPARTAMENTOSReference.EntityKey.EntityKeyValues[0].Value;
                    //codDepar = (int)usuario.DEPARTAMENTOSReference.EntityKey.EntityKeyValues[0].Value;
                    int departamento = Convert.ToInt32(dep);
                    if (!modificar)
                    {
                        if (departamento == 34 || departamento == 1 || departamento == 6 || departamento == 23 || departamento == 27)
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
                    {
                        if (departamento == 34 || departamento == 1 || departamento == 6 || departamento == 23 || departamento == 27)
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



                }
                else
                    MessageBox.Show("Usuario y/o Contraseña mal ingresados", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            switch (_formulario)
            {
                case "Signos":
                    if (MessageBox.Show("Si continua no grabara la informacion,\r\n¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        usuarioActual = 0;
                        this.Close();
                    }
                    break;
                case "Ingesta":
                    if (MessageBox.Show("Si continua no grabara la informacion,\r\n¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        usuarioActual = 0;
                        this.Close();
                    }
                    break;
                case "interB":
                    if (MessageBox.Show("Si continua no grabara la informacion,\r\n¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        usuarioActual = 0;
                        this.Close();
                    }
                    break;
                case "protocolo":
                    if (MessageBox.Show("Si continua no grabara la informacion,\r\n¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        usuarioActual = 0;
                        this.Close();
                    }
                    break;
                case "PerLab":
                    if (MessageBox.Show("Si continua no grabara la informacion,\r\n¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        usuarioActual = 0;
                        this.Close();
                    }
                    break;
                default:
                    if (MessageBox.Show("Si continua no se dara el alta al paciente,\r\n¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        usuarioActual = 0;
                        this.Close();
                    }
                    break;
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
                            if (departamento == 34 || departamento == 1 || departamento == 6 || departamento == 23 || departamento == 27)
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
                else
                {
                    txtPassword.Text = "";
                    txtPassword.Focus();
                }
            }
        }
    }
}
