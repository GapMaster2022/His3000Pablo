using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using His.Negocio;
using System.Windows.Forms;

namespace His.Maintenance
{
    public partial class frmTiposAtenciones : Form
    {
        public frmTiposAtenciones()
        {
            InitializeComponent();
            refresh();
            cmbTipo.DataSource = NegMaintenance.getDataTable("TiposIngresos");
            cmbTipo.ValueMember = "TIP_CODIGO";
            cmbTipo.DisplayMember = "TIP_DESCRIPCION";
            cmbTipo.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbTipo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }


        public void refresh()
        {
            grid.DataSource=NegMaintenance.getDataTable("TiposAtenciones");
        }

        private void grid_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            try
            {
                if (grid.ActiveRow.Index > -1)
                {
                    txtCod.Text = grid.ActiveRow.Cells["CODIGO"].Value.ToString();
                    txtDesc.Text = grid.ActiveRow.Cells["DESCRIPCION"].Value.ToString();
                    cmbTipo.Text = grid.ActiveRow.Cells["INGRESO"].Value.ToString();
                    grpDatos.Enabled = false;
                    btnNuevo.Enabled = false;
                    btnGuardar.Enabled = false;
                    btnCancelar.Enabled = true;
                    btnModificar.Enabled = true;
                    btnBorrar.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void btnCancelar_Click(object sender, EventArgs e) { 
            resetCampos();
        }

        private void resetCampos()
        {
            txtCod.Text = string.Empty;
            txtDesc.Text = string.Empty;
            //refresco objetos con datos
            refresh();
            //habilitando controles
            grpDatos.Enabled = false;
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnModificar.Enabled = false;
            btnBorrar.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtDesc.Text.Trim() != string.Empty)
            {
                if (Guardar())
                {
                    resetCampos();
                }
            }
        }

        private bool Guardar() //devolver si se guardo o no si hubo excepcion
        {

            try
            {
                int cod;
                if (txtCod.Text.Trim() == string.Empty)
                    cod = -1;
                else
                    cod = Convert.ToInt32(txtCod.Text.Trim());

                NegMaintenance.save_tipoAtencion(cod, Convert.ToInt32(cmbTipo.SelectedValue.ToString()), txtDesc.Text.Trim());

                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;

            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            grpDatos.Enabled = true;
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnModificar.Enabled = false;
            btnBorrar.Enabled = false;
            txtDesc.Focus();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grpDatos.Enabled = true;
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnModificar.Enabled = false;
            btnBorrar.Enabled = false;
            cmbTipo.Focus();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (!NegMaintenance.delete("TipoAtencion", Convert.ToInt32(txtCod.Text.Trim())))
            {
                MessageBox.Show("No ha sido posible eliminar, es probable que este asociado a otros registros.");
            }
            resetCampos();
        }
    }
}
