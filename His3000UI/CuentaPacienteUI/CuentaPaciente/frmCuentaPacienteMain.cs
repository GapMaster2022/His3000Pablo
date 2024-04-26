using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CuentaPaciente
{
    public partial class frmCuentaPacienteMain : Form
    {
        public frmCuentaPacienteMain()
        {
            InitializeComponent();
        }

        private void valoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmValoresAutomaticos valoresAutomaticos = new frmValoresAutomaticos();
            //valoresAutomaticos.MdiParent = this;
            valoresAutomaticos.Show();  
        }

    }
}
