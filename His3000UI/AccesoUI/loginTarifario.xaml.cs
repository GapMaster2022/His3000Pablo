using System;
using System.Windows;
using System.Windows.Forms;
//using TarifariosUI;
using His.Negocio;
using His.Entidades;

namespace AccesoUI
{
    /// <summary>
    /// Lógica de interacción para loginTarifario.xaml
    /// </summary>
    public partial class loginTarifario : Window
    {
        public loginTarifario()
        {
            InitializeComponent();
			recuperarInfEmpresa();
        }

		private void recuperarInfEmpresa() 
        {
            //EMPRESA  empresa = (from e in contexto.EMPRESA
            //              select e).First() ;
            EMPRESA empresa = NegEmpresa.RecuperaEmpresa();  
        }
		
        private void txtUsuario_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
        	
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (txtClave.Focusable)
                    txtClave.Focus();
            }
        }

        private void txtClave_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key==System.Windows.Input.Key.Enter)
            {
                if( btnAceptar.Focusable )
                    btnAceptar.Focus();  
            }
        }

        private void btnAceptar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	try
            {

                USUARIOS usuario = NegUsuarios.ValidarUsuario(txtUsuario.Text.Trim(), txtClave.Password.ToString());
                if (usuario!=null )
                {
                    //guardo los cambios en el archivo de configuracion
                    IniFile archivo = new IniFile(Environment.CurrentDirectory + "\\his3000.ini");
                    archivo.IniWriteValue("Usuario", "usr", txtUsuario.Text.Trim());
                    archivo.IniWriteValue("Usuario", "pwd", txtClave.Password.ToString());
                    //Recupero el codigo del medico si es usuario lo es
                    MEDICOS medico = NegMedicos.RecuperaMedicoIdUsuario(usuario.ID_USUARIO);
                    if (medico != null)
                        His.Entidades.Clases.Sesion.codMedico = medico.MED_CODIGO;
                    His.Entidades.Clases.Sesion.codUsuario = usuario.ID_USUARIO;
					//Form tarifario = new frmMainTarifario();
     //               tarifario.ShowDialog();
                    txtUsuario.Text = "";
                    txtClave.Password = "";
                    //this.Hide(); 
                    //this.Close();
                }
                else
                {
                    txtClave.Password = "";
                    System.Windows.Forms.MessageBox.Show("Usuario incorrecto, por favor ingrese un usuario activo", "Login incorrecto");
                }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message);      
            }
        }

        private void btnSalir_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Desea salir de la aplicación?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
    }
}
