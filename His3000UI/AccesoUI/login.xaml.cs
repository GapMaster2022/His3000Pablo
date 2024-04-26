using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using His.Negocio;
using His.Entidades;
using Core.Datos;
//using GeneralApp;
using Core.Utilitarios;
using System.Net;
using System.Data;

namespace AccesoUI
{
    /// <summary>
    /// Lógica de interacción para login.xaml
    /// </summary>
    public partial class login : Window
    {
        //HIS3000BDEntities conexion;
        //GeneralApp.ControlesWPF.LoadingAnimation animacion; 
        public login()
        {
            InitializeComponent();
            BaseContextoDatos Ip = new BaseContextoDatos();
            IconoPruebas.Visibility = Visibility.Visible;
            string conexion = Ip.ObtenerIP();

            if (conexion.Trim() == "192.168.20.100")
                IconoPruebas.Visibility = Visibility.Collapsed;


        }

        private void recuperarInfEmpresa()
        {
            try
            {
                EMPRESA empresa = NegEmpresa.RecuperaEmpresa();
                lblEmpresa.Text = empresa.EMP_NOMBRE;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)

        {
            try
            {
                marco.Cursor = Cursors.Wait;

                USUARIOS usuario = NegUsuarios.ValidarUsuario(txtUsuario.Text.Trim(), txtClave.Password.ToString());
                //

                if (usuario != null)
                {
                    //guardo los cambios en el archivo de configuracion
                    IniFile archivo = new IniFile(Environment.CurrentDirectory + "\\his3000.ini");
                    archivo.IniWriteValue("Usuario", "usr", txtUsuario.Text.Trim());
                    archivo.IniWriteValue("Usuario", "pwd", EncriptadorUTF.Encriptar(txtClave.Password.ToString()));
                    //Recupero el codigo del medico si es usuario lo es
                    MEDICOS medico = NegMedicos.RecuperaMedicoIdUsuario(usuario.ID_USUARIO);
                    if (medico != null)
                        His.Entidades.Clases.Sesion.codMedico = medico.MED_CODIGO;

                    His.Entidades.Clases.Sesion.codUsuario = usuario.ID_USUARIO;
                    His.Entidades.Clases.Sesion.nomUsuario = usuario.NOMBRES + " " + usuario.APELLIDOS;
                    //datos del correo electronico
                    His.Parametros.HonorariosPAR.SmtpCorreo = usuario.EMAIL != null ? usuario.EMAIL : His.Parametros.HonorariosPAR.SmtpCorreo;
                    //
                    EMPRESA empresa = NegEmpresa.RecuperaEmpresa();
                    His.Entidades.Clases.Sesion.nomEmpresa = empresa.EMP_NOMBRE;
                    His.Entidades.Clases.Sesion.unicodigo = empresa.EMP_UNICODIGO;
                    DEPARTAMENTOS departamento = NegDepartamentos.RecuperarDepartamento(usuario.ID_USUARIO);
                    His.Entidades.Clases.Sesion.codDepartamento = departamento.DEP_CODIGO;
                    His.Entidades.Clases.Sesion.nomDepartamento = departamento.DEP_NOMBRE;
                    
                    Window1 acceso = new Window1();
                    acceso.Show();
                    this.Close();
                }
                else
                {
                    txtClave.Password = "";
                    MessageBox.Show("Usuario incorrecto, por favor ingrese un usuario activo", "Login incorrecto", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //añado la animacion de procesamiento
            //animacion = new GeneralApp.ControlesWPF.LoadingAnimation();
            //marco.Children.Add(animacion);
            //DockPanel.SetDock(animacion, Dock.Top);
            //recupero información de la empresa
            recuperarInfEmpresa();
            txtUsuario.Focus();
            //marco.Children.Remove(animacion); 
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            //MessageBoxResult result = System.Windows.MessageBox.Show("Desea salir de la aplicación?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //if (result == MessageBoxResult.Yes)
            //{
            System.Windows.Application.Current.Shutdown();
            //}
        }

        private void txtClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                btnAceptar_Click(null, null);
            }
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (txtClave.Focusable)
                    txtClave.Focus();
            }
        }




        private void btnCambiarClave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                USUARIOS usuario = NegUsuarios.ValidarUsuario(txtUsuario.Text.Trim(), txtClave.Password.ToString());
                if (usuario != null)
                {
                    CambiarClave ventana = new CambiarClave(usuario);
                    txtClave.Password = "";
                    ventana.ShowDialog();
                }
                else
                {
                    txtClave.Password = "";
                    MessageBox.Show("Usuario incorrecto, por favor ingrese un usuario activo", "Login incorrecto", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message);
            }


        }

    }
}
