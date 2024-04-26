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

namespace His.HabitacionesUI
{
    /// <summary>
    /// Lógica de interacción para frmAsignacionHabitaciones.xaml
    /// </summary>
    public partial class frmAsignacionHabitaciones : Window
    {
        private USUARIOS listaUsuarios;

        public frmAsignacionHabitaciones()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        
    }
}
