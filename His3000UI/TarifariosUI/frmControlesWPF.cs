using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace TarifariosUI
{
    public partial class frmControlesWPF : Form
    {
        public frmControlesWPF()
        {
            InitializeComponent();
            //cambio el tamaño de los contenedores
            this.Height = 300;
            this.Width = 375;

            panelContenedor.Height = 300;
            panelContenedor.Width = 375;
        }

        private void frmControlesWPF_Load(object sender, EventArgs e)
        {
            ElementHost host = new ElementHost();
            //His.HabitacionesUI.ucAyudaHabitaciones habitaciones = new His.HabitacionesUI.ucAyudaHabitaciones();
            frmAcerca habitaciones = new frmAcerca();
            host.Dock = DockStyle.Fill;
            host.Child = habitaciones;
            panelContenedor.Controls.Add(host);
            panelContenedor.Dock = DockStyle.Top;
           
        }
    }
}
