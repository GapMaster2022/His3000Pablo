using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;


namespace CuentaPaciente
{
    public partial class frmRecuperaClienteSIC : Form
    {
        #region VARIABLES GLOBALES

        public TextBox IDENTIFICADOR = new TextBox();

        #endregion
        public frmRecuperaClienteSIC()
        {
            InitializeComponent();
        }       

        #region OBJETOS DE FORMULARIO

        public void BuscaClienteSIC()
        {
            DataTable clientesCedula = new DataTable();
            clientesCedula = NegDetalleCuenta.RecuperaClienteSIC(txt_Cliente.Text);
            if (clientesCedula.Rows.Count >= 1)
            {
                dgvClienteSic.DataSource = clientesCedula;
                dgvClienteSic.Columns[1].Width = 300;
            }
            else
                MessageBox.Show("No Hay Clientes Con Ese Detalle", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void dgvClienteSic_DoubleClick(object sender, EventArgs e)
        {
            Enviar();
        }
        private void dgvClienteSic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Enviar();
            }
        }

        #endregion

        private void btnBurcar_Click(object sender, EventArgs e)
        {
            if (txt_Cliente.Text != "")
            {
                BuscaClienteSIC();
            }
            else
            {
                MessageBox.Show("Ingrese Un Nombre Ó Razon Social Del Cliente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Enviar()
        {
            int pos = dgvClienteSic.CurrentRow.Index;
            IDENTIFICADOR.Text = dgvClienteSic.Rows[pos].Cells[0].Value.ToString();
            this.Close();
        }

        
    }
}
