using His.Negocio;
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

    public partial class frmAyudaBancos : Form
    {

        public String bancoElegido;

        public frmAyudaBancos()
        {
            InitializeComponent();
            CargarTiposDesuento();
        }

  

        private void CargarTiposDesuento()
        {
            listBox1.Items.Clear();
            DataTable auxDT = NegFactura.Bancos();

            foreach (DataRow row in auxDT.Rows)
            {
                listBox1.Items.Add(row[0].ToString());
            }
            listBox1.SetSelected(0, true);
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bancoElegido = listBox1.SelectedItem.ToString();
            this.Dispose();
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                bancoElegido = listBox1.SelectedItem.ToString();
                this.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bancoElegido = "CANCELA ACCION";
            this.Dispose();
        }
    }
}
