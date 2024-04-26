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
using His.Formulario;

namespace His.HabitacionesUI
{
    /// <summary>
    /// Lógica de interacción para frmHistorisClinica.xaml
    /// </summary>
    public partial class frmHistorisClinica : Window
    {
        public frmHistorisClinica()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host =
                new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the MaskedTextBox control.
            frm_Anemnesis anemeasis = new frm_Anemnesis(); 

            // Assign the MaskedTextBox control as the host control's child.
            anemeasis.Show(); 
 

            // Add the interop host control to the Grid
            // control's collection of child controls.
            this.gridContenedor.Children.Add(host);

        }

    }
}
