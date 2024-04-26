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
    public partial class frm_PedidosCuenta : Form
    {
        Datos.PedidosAreas area = new Datos.PedidosAreas();
        DataTable datos = new DataTable();
        string codigo = "0";
        public frm_PedidosCuenta()
        {
            InitializeComponent();
        }

        private void btn_Nuevo_Click(object sender, EventArgs e)
        {
            Form frm = new frm_Pedidos();
            frm.Show();
        }



        private void frm_PedidosCuenta_Load(object sender, EventArgs e)
        {
            cargar_grid();
            chkEstado.Checked = true;
            inicio_controles();

        }
        private void inicio_controles()
        {
            btnActualizar.Enabled = false;
            btnCancelar.Enabled = true;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
            btnActualizar.Enabled = false;
        }

        public void cargar_grid()
        {


            datos = area.cargar_areas();
            grv_areas.DataSource = datos;
            grv_areas.Columns["codigo"].Visible = false;
            grv_areas.Columns["tipo"].Visible = false;
            grv_areas.Columns["Activo"].ReadOnly = false;

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            datos = area.UltimoCodigo();
            codigo = datos.Rows[0]["maximo"].ToString();
            txt_nombre.Enabled = true;
            txt_nombre.Text = "";
            btnGuardar.Enabled = true;
            btnEliminar.Enabled = false;
            btnActualizar.Enabled = false;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try { grabar(); }
            catch (Exception ex) { }

        }
        public void grabar()
        {
            string estado = "0";
            if (chkEstado.Checked == true)
            {
                estado = "1";
            }
            else
            {
                estado = "0";

            }

            if (area.grabar(codigo, codigo, txt_nombre.Text, estado))
            {
                MessageBox.Show("Registro  guardado exitosamente");
                cargar_grid();
                txt_nombre.Enabled = false;
                txt_nombre.Text = "";
                btnGuardar.Enabled = false;

            }
            else
            {
                MessageBox.Show("No  se pudo  guardar registro ");


            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            txt_nombre.Enabled = false;
            btnEliminar.Enabled = false;
            btnActualizar.Enabled = false;
            txt_nombre.Text = "";
        }

        private void grv_areas_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnGuardar.Enabled = false;
            txt_nombre.Enabled = true;
            codigo= grv_areas.CurrentRow.Cells["codigo"].Value.ToString();
            txt_nombre.Text = grv_areas.CurrentRow.Cells["Nombre"].Value.ToString();
       string estado = grv_areas.CurrentRow.Cells["Activo"].Value.ToString();
       if (estado == "True") chkEstado.Checked = true;
       else chkEstado.Checked = false;
        btnActualizar.Enabled = true;
        btnEliminar.Enabled = true;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {  string estado = "0";
            if (chkEstado.Checked == true)
            {
                estado = "1";
            }
            else
            {
                estado = "0";

            }
            if (area.modificar(codigo, codigo, txt_nombre.Text, estado))
            {
                MessageBox.Show("Registro  guardado exitosamente");
                cargar_grid();
                txt_nombre.Enabled = false;
                txt_nombre.Text = "";
                btnActualizar.Enabled = false;
            }
            else
            {
                MessageBox.Show("No  se pudo  guardar registro ");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            
                 DialogResult resultado;

                resultado = MessageBox.Show("Desea eliminar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    if (area.eliminar(codigo)) MessageBox.Show("Registro  eliminado ");
                    btnEliminar.Enabled = false;
                    cargar_grid();
                }   
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_nombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                chkEstado.Focus();
            }
        }
    }
}

