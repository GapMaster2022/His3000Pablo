using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;

namespace His.HabitacionesUI
{
    public partial class CalculoOxigeno : Form
    {
        public string totalCalculo;
        public CalculoOxigeno()
        {
            InitializeComponent();
        }

        private void CalculoOxigeno_Load(object sender, EventArgs e)
        {
            DataTable horas = new DataTable();
            horas = NegParametros.RecuepraHorasyLitros(1);
            cmbHora.DisplayMember = "desc";
            cmbHora.ValueMember = "id";
            cmbHora.DataSource = horas;
            horas = null;
            horas = NegParametros.RecuepraHorasyLitros(2);
            cmbLitos.DisplayMember = "desc";
            cmbLitos.ValueMember = "id";
            cmbLitos.DataSource = horas;
        }
        Int64 total;
        private void btnGenraTotal_Click(object sender, EventArgs e)
        {            
            
            if (label4.Visible)
            {

                if (MessageBox.Show("VA AGREGAR OTRO CÁLCULO SOBRE EL YA EXISTENTE\r\n¿DESEA CONTINUAR?", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    total += Convert.ToInt64(cmbHora.Text) * (Convert.ToInt64(cmbLitos.Text) * 60);
                    label4.Text = total.ToString();
                }
            }
            else
            {
                label4.Visible = true;
                total = Convert.ToInt64(cmbHora.Text) * (Convert.ToInt64(cmbLitos.Text) * 60);
                label4.Text = total.ToString();
            }
        }

        private void btnCargaOxigeno_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Va salir del Calculo de Oxigeno\r\n¿Desea Continuar?","HIS3000",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes)
            {
                totalCalculo = label4.Text;
                this.Close();
            }
        }
    }
}
