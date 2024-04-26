using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Pedidos
{
    public partial class frmAyudaDespacho : Form
    {
        public string observacion = "";
        public int despachado = 0;
        public frmAyudaDespacho()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            observacion = txtObservacion.Text;
            this.Close();
        }

        private void frmAyudaDespacho_Load(object sender, EventArgs e)
        {

        }
    }
}
