using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Negocio;

namespace His.Maintenance
{
    public partial class Habitaciones : Form
    {

        public Int32 codigoHabitacion = 0;
        public Habitaciones()
        {
            InitializeComponent();
            DataTable obj = new DataTable();
            obj = NegHabitaciones.listaHabitacionesActivas();
            dgv_habitaciones.DataSource = obj;
            dgv_habitaciones.Columns[0].Width = 200;
            dgv_habitaciones.Columns[1].Width = 300;
            dgv_habitaciones.Columns[2].Width = 200;
            List<HABITACIONES_ESTADO> estado = new List<HABITACIONES_ESTADO>();
            estado = NegHabitaciones.ListaEstadosdeHabitacion();
            comboBox1.DisplayMember = "HES_NOMBRE";
            comboBox1.ValueMember = "HES_CODIGO";
            comboBox1.DataSource = estado;
            comboBox1.SelectedValue = -1;
        }

        private void dgv_habitaciones_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int fila = dgv_habitaciones.CurrentRow.Index;
            codigoHabitacion = Convert.ToInt32(dgv_habitaciones.Rows[fila].Cells[0].Value);
            textBox1.Text = dgv_habitaciones.Rows[fila].Cells[1].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if( textBox1.Text == "")
            {
                MessageBox.Show("Seleccione una habitacion para modificar su estado actual", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if( comboBox1.Text == "")
            {
                MessageBox.Show("Seleccione un estado para modificar la habitacion", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if(MessageBox.Show("Esta seguro de cambiar el estado de la habitación???", "HIS3000", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if(NegHabitaciones.CambiaEstadoHabitacionMantenimiento(codigoHabitacion, Convert.ToInt32(comboBox1.SelectedValue)))
                {
                    MessageBox.Show("Habitacion Actualizada con exito", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Habitacion no se pudo actualizar, vuelva a intentar", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
    }
}
