using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Emergencia
{
    public partial class frm_DescripcionSubSintoma : Form
    {
        public string descripcion;

        public frm_DescripcionSubSintoma()
        {
            InitializeComponent();
        }

        public frm_DescripcionSubSintoma(string subSintoma)
        {
            InitializeComponent();
            groupBox1.Name = subSintoma;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDescripcion.Text.ToString() == string.Empty || txtDescripcion.Text == null)
                {
                    AgregarError(txtDescripcion);                   
                }
                else 
                {
                    descripcion = txtDescripcion.Text;
                    this.Close();
                }                
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AgregarError(Control control)
        {
            error.SetError(control, "Campo Requerido");

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }



    }



}
