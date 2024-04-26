using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace His.Maintenance
{
    public partial class frm_Estaciones : Form
    {
        Datos.Estaciones estaciones = new Datos.Estaciones();
        DataTable datos = new DataTable();
        string codigo = "0";
        public frm_Estaciones()
        {
            InitializeComponent();
        }

        private void frm_Estaciones_Load(object sender, EventArgs e)
        {
            cargar_grid();
            chkActivo.Checked = true;
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
            datos = estaciones.cargar_estaciones();
            grv_estaciones.DataSource = datos;
            grv_estaciones.Columns["codigo"].Visible = false;
            grv_estaciones.Columns["Activo"].ReadOnly = false;
          
         
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            datos = estaciones.UltimoCodigo();
            codigo = datos.Rows[0]["maximo"].ToString();
            txtnombre.Enabled = true;
            txtnombre.Text = "";
            txtdescripcion.Enabled = true;
            txtdescripcion.Text = "";
            btnGuardar.Enabled = true;
            btnEliminar.Enabled = false;
            btnActualizar.Enabled = false;
        }
        public void grabar()
        {
            string estado = "0";
            if (chkActivo.Checked == true)
            estado = "1";       
            else          
            estado = "0";

            if (estaciones.grabar(codigo,txtnombre.Text,txtdescripcion.Text, estado))
            {
                MessageBox.Show("Registro  guardado exitosamente");
                cargar_grid();
                txtnombre.Enabled = false;
                txtnombre.Text = "";
                txtdescripcion.Enabled = false;
                txtdescripcion.Text = "";
                btnGuardar.Enabled = false;
              }
            else
            {
                MessageBox.Show("No  se pudo  guardar registro ");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try{grabar(); }catch (Exception ex) { }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            txtnombre.Enabled = false;
            btnEliminar.Enabled = false;
            btnActualizar.Enabled = false;
            txtnombre.Text = "";
            txtdescripcion.Text = "";
            txtdescripcion.Enabled = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult resultado;

            resultado = MessageBox.Show("Desea eliminar los Datos?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                if (estaciones.eliminar(codigo)) MessageBox.Show("Registro  eliminado ");
                btnEliminar.Enabled = false;
                cargar_grid();
            }   
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string estado = "0";
            if (chkActivo.Checked == true)          
                estado = "1";          
            else         
                estado = "0";

         
            if (estaciones.modificar(codigo,txtnombre.Text, txtdescripcion.Text, estado))
            {
                MessageBox.Show("Registro  guardado exitosamente");
                cargar_grid();
                txtnombre.Enabled = false;
                txtdescripcion.Text = "";
                txtdescripcion.Enabled = false;
                txtnombre.Text = "";
                btnActualizar.Enabled = false;
            }
            else
            {
                MessageBox.Show("No  se pudo  guardar registro ");
            }
        }

        private void grv_estaciones_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            btnGuardar.Enabled = false;
            txtnombre.Enabled = true;
            txtdescripcion.Enabled = true;
            codigo = grv_estaciones.CurrentRow.Cells["codigo"].Value.ToString();
            txtnombre.Text = grv_estaciones.CurrentRow.Cells["Nombre"].Value.ToString();
            txtdescripcion.Text = grv_estaciones.CurrentRow.Cells["Descripcion"].Value.ToString();
           string estado = grv_estaciones.CurrentRow.Cells["Activo"].Value.ToString();
            if (estado == "True") chkActivo.Checked = true;
            else chkActivo.Checked = false;
            btnActualizar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void txtnombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                txtdescripcion.Focus();
            }
        }

        private void txtdescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                e.Handled = true;
                chkActivo.Focus();
            }
        }
    }
}
