using His.Negocio;
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
    public partial class frm_TipoIngreso : Form
    {
        
        
        #region Eventos
        public frm_TipoIngreso()
        {
            InitializeComponent();
            refreshObject();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            grpDatos.Enabled = true;
            btnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
            btnModificar.Enabled = false;
            btnBorrar.Enabled = false;
            txtDesc.Focus();
            chkEstado.Checked = true;
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
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                if (Guardar())
                {
                    resetCampos();
                }
            }
        }
        #endregion

        #region Metodos

        private bool Guardar() //devolver si se guardo o no si hubo excepcion
        {

            try
            {
                int cod;
                if (txtCod.Text.Trim() == string.Empty)
                    cod = -1;
                else
                    cod = Convert.ToInt32(txtCod.Text.Trim());
                int estado;
                if (chkEstado.Checked)
                    estado = 1;
                else
                    estado = 0;

                string[] x = new string[] { cod.ToString(), txtDesc.Text.Trim(), estado.ToString() };
                
                NegMaintenance.setROW("SetTipoIngreso", x);
                

                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;

            }
        }

        private bool Validar() //Validar SI, si los campos necesarios estan bien llenos, else NO
        {
            if (txtDesc.Text.Trim() == string.Empty)
            {
                errorProvider1.SetError(txtDesc, "Campo Obligatorio");
                return false;
            }
            else
                return true;
        }
        private void resetCampos()
        {
            clsCampos();
            //refresco objetos con datos
            refreshObject();
            //habilitando controles
            grpDatos.Enabled = false;
            btnNuevo.Enabled = true;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnModificar.Enabled = false;
            btnBorrar.Enabled = false;
        }
        private void clsCampos() //vaciar campos necesarios
        {
            txtCod.Text = string.Empty;
            txtDesc.Text = string.Empty;
        }
        private void refreshObject() //actualizar grid, combos, etc 
        {
            grid.DataSource = NegMaintenance.getDataTable("TipoIngreso");


        }



        #endregion

        private void grid_DoubleClickRow(object sender, Infragistics.Win.UltraWinGrid.DoubleClickRowEventArgs e)
        {
            try
            {
                if (grid.ActiveRow.Index > -1)
                {
                    txtCod.Text = grid.ActiveRow.Cells["TIP_CODIGO"].Value.ToString();
                    txtDesc.Text = grid.ActiveRow.Cells["TIP_DESCRIPCION"].Value.ToString();
                    if (grid.ActiveRow.Cells["TIP_ESTADO"].Value.ToString().Trim() == "True" | grid.ActiveRow.Cells["TIP_ESTADO"].Value.ToString().Trim() == "1")
                        chkEstado.Checked = true;
                    else
                        chkEstado.Checked = false;



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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            resetCampos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (!NegMaintenance.delete("TipoIngreso", Convert.ToInt32(txtCod.Text.Trim())))
            {
                MessageBox.Show("No ha sido posible eliminar, es probable que este asociado a otros registros.", "HIS3000");
            }
            resetCampos();
        }
    }
}
