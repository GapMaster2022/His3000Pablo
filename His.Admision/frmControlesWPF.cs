using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace His.Admision
{
    public partial class frmControlesWPF : Form
    {
        int xTip;
        public string tip_codigo; //codigo del tipo de ingreso para validar el PROCE
        public frmControlesWPF(int x =0)
        {
            xTip = x;
            InitializeComponent();
          
        }

        private void frmControlesWPF_Load(object sender, EventArgs e)
        {
            
                ElementHost host = new ElementHost();
                //His.HabitacionesUI.ucAyudaHabitaciones habitaciones = new His.HabitacionesUI.ucAyudaHabitaciones();
                frmAyudaHabitaciones habitaciones = new frmAyudaHabitaciones(this, xTip);
                host.Child = habitaciones;
                host.Dock = DockStyle.Fill;
                //host.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);  
                panelContenedor.Controls.Add(host);
            
        }

        public  void Cerrar()
        {
            this.Close(); 

        }
    }
}
