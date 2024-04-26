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

namespace His.HabitacionesUI
{
    /// <summary>
    /// Lógica de interacción para frmRadiografiaAyudas.xaml
    /// </summary>
    public partial class frmRadiografiaAyudas : Window
    {
        public frmRadiografiaAyudas()
        {
            InitializeComponent();
        }

        private void xamDataPresenterRadiografiasSolicitados_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
    }
}
