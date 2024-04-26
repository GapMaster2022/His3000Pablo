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
using His.Entidades;
using His.Negocio;

namespace AccesoUI
{
    /// <summary>
    /// Lógica de interacción para CambiarClave.xaml
    /// </summary>
    public partial class CambiarClave : Window
    {
        USUARIOS usuario;
		public CambiarClave()
        {
            InitializeComponent();
        }
		
		public CambiarClave(USUARIOS parUsuario)
        {
            InitializeComponent();
			usuario = parUsuario;
        }
		

        private void txtClave_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtClaveVerificar_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (txtClave.Password.Equals(txtClaveVerificar.Password))
            {
                if (txtClave.Password.Length >= 5)
                {
                    usuario.PWD = txtClave.Password;
                    NegUsuarios.ActualizarUsuario(usuario);
                    MessageBox.Show("La contraseña se cambio exitosamente");  
                    this.Close();
                    return; 
                }
            }
            MessageBox.Show("La contraseña escrita debe ser la misma en los dos campos");  
            txtClave.Password = "";
            txtClaveVerificar.Password = "";

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
			this.Close();
        }
    }
}
