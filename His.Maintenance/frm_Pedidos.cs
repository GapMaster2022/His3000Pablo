using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Maintenance
{
    public partial class frm_Pedidos : Form
    {
        public frm_Pedidos()
        {
            InitializeComponent();
            cargarDatos();
        }


        private void btn_Guardar_Click(object sender, EventArgs e)
        {

        }

        private void cargarDatos() 
        {
            try
            {     
                cbm_Estado.Items.Add("Activado");
                cbm_Estado.Items.Add("Desactivado");
                cbm_Estado.SelectedIndex = 0;
                cbm_Tipo.Items.Add("Institucinal");
                cbm_Tipo.Items.Add("Externo");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    MessageBox.Show(ex.InnerException.Message);
                else
                    MessageBox.Show(ex.Message);
            }


        }
        public Boolean validarDatos()
        {
            bool valido = true;
            if (txt_Nombre.Text == null || txt_Nombre.Text == string.Empty)
            {
                MessageBox.Show("Campo Obligatorio", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_Nombre.Focus();
                //AgregarError(txt_Nombre);
                valido = false;
            }
            else
            {
                cbm_Estado.Focus();
                if (cbm_Estado.SelectedIndex.Equals(-1))
                {
                }
                else
                {
                    //catalogoModificado.HCC_NOMBRE = txt_Nombre.Text;
                    if (cbm_Estado.SelectedItem.ToString() == "Activado")
                    {
                        //catalogoModificado.HCC_ESTADO = true;
                    }
                    else
                    {
                        //catalogoModificado.HCC_ESTADO = false;
                    }
                }
            }
            return valido;

        }
    }
}
