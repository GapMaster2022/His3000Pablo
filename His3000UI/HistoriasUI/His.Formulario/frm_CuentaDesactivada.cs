using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades.Clases;
using His.Negocio;

namespace His.Formulario
{
    public partial class frm_CuentaDesactivada : Form
    {
        public int verificador=1;
        public Int64 atencion;
        public frm_CuentaDesactivada(string atenciontxt)
        {
            InitializeComponent();
            atencion = Convert.ToInt64(atenciontxt);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtMotivo.Text != "")
            {
                if (MessageBox.Show("Usted Va Desactivar Esta ATENCIÓN, reversión de estado sera solo con autorizacion del jefe del departamento. \r\n¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        txtMotivo.Text += " \r\nUSUARIO: " + Sesion.nomUsuario;
                        NegAtenciones.IngresaDesactivacionAtencion(atencion, txtMotivo.Text);                        
                        verificador = 0;
                        MessageBox.Show("Atención Desactivada con éxito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo desactivar la atención" + ex.ToString(), "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Describa el Motivo por el que esta desactivando la cuenta", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
