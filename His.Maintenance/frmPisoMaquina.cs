using His.Negocio;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Datos;

namespace His.Maintenance
{
    public partial class frmPisoMaquina : Form
    {
        public bool Editar = false;
        public string temp = "";
        PAIS paises;
        public frmPisoMaquina()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Editar = false;
            errorProvider1.Clear();
            Bloquear();
            btnNuevo.Enabled = true;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            UltraGridRow fila = UltraGridNacionalidad.ActiveRow;
            if (UltraGridNacionalidad.Selected.Rows.Count > 0)
            {
                if (MessageBox.Show("¿Esta seguro de eliminar: " + fila.Cells[1].Value.ToString() + "?", "HIS3000",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    try
                    {
                        NegMaintenance.EliminarPisoMaquina(txtdescripcion.Text);
                        MessageBox.Show("Eliminado Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnBorrar.Enabled = false;
                        btnNuevo.Enabled = true;
                        Bloquear();
                        Limpiar();
                        CargarGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtdescripcion.Text.Trim() == "")
            {
                errorProvider1.SetError(txtdescripcion, "Campo Obligatorio");
                return;
            }
            if (txtnacionalidad.Text.Trim() == "")
            {
                errorProvider1.SetError(txtnacionalidad, "Campo Obligatorio");
                return;
            }
            //if (txtBodega.Text.Trim() == "")
            //{
            //    errorProvider1.SetError(txtBodega, "Campo Obligatorio");
            //    return;
            //}
            else
            {
                try
                {
                    if (Editar == false)
                    {
                        if (NegMaintenance.existePiso(txtdescripcion.Text))
                        {
                            MessageBox.Show("La IP " + txtdescripcion.Text + " ya existe", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        NegMaintenance.CrearPisoMaquina(Convert.ToInt32(cmbNivel.SelectedValue), txtdescripcion.Text, txtnacionalidad.Text, Convert.ToInt32(cmb_bodega.SelectedValue), Convert.ToInt32(cmbDespacho.SelectedValue));
                        MessageBox.Show("Datos Almacenados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                        Bloquear();
                        CargarGrid();
                        btnNuevo.Enabled = true;
                    }
                    else
                    {
                        NegMaintenance.EditarPisoMaquina(Convert.ToInt32(cmbNivel.SelectedValue), txtdescripcion.Text, txtnacionalidad.Text, temp,Convert.ToInt32(cmb_bodega.SelectedValue), Convert.ToInt32(cmbDespacho.SelectedValue));
                        MessageBox.Show("Datos Actualizados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                        Bloquear();
                        CargarGrid();
                        Editar = false;
                        btnNuevo.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void cargarcombo()
        {

            cmbNivel.DataSource = NegMaintenance.cargarComboNivelPiso().OrderBy(a => a.NIV_NOMBRE).ToList();
            cmbNivel.ValueMember = "NIV_CODIGO";
            cmbNivel.DisplayMember = "NIV_NOMBRE";
            cmbNivel.SelectedIndex = 0;
            cmb_bodega.DataSource = NegMaintenance.cargarBodega();
            cmb_bodega.ValueMember = "codlocal";
            cmb_bodega.DisplayMember = "nomlocal";
            cmb_bodega.SelectedIndex = 0;

            cmbDespacho.DataSource = NegMaintenance.cargarBodega();
            cmbDespacho.ValueMember = "codlocal";
            cmbDespacho.DisplayMember = "nomlocal";
            cmbDespacho.SelectedIndex = 0;
            ////using (MiEntity db = new MiEntity())
            //using (var db = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
            //{
            //    cmbNivel.DataSource = db.NIVEL_PISO.OrderBy(d => d.NIV_NOMBRE).ToList();
            //    //campo que vera el usuario
            //    cmbNivel.DisplayMember = "NIV_NOMBRE";
            //    //campo que es el valor real
            //    cmbNivel.ValueMember = "NIV_CODIGO";

            //}
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Desbloquear();
            btnNuevo.Enabled = false;
        }
        public void Bloquear()
        {
            txtcodigo.Enabled = false;
            txtdescripcion.Enabled = false;
            txtnacionalidad.Enabled = false;
            //txtBodega.Enabled = false;
            cmb_bodega.Enabled = false;
            cmbDespacho.Enabled = false;
            cmbNivel.Enabled = false;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnBorrar.Enabled = false;
        }
        public void Desbloquear()
        {
            txtcodigo.Enabled = true;
            txtdescripcion.Enabled = true;
            txtnacionalidad.Enabled = true;
            //txtBodega.Enabled = true;
            cmb_bodega.Enabled = true;
            cmbDespacho.Enabled = true;
            cmbNivel.Enabled = true;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }
        public void Limpiar()
        {
            txtcodigo.Text = "";
            txtdescripcion.Text = "";
            txtnacionalidad.Text = "";
            txtBodega.Text = "";
        }
        public void CargarGrid()
        {
            try
            {
                UltraGridNacionalidad.DataSource = NegMaintenance.GetPisoMaquina();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UltraGridNacionalidad_DoubleClick(object sender, EventArgs e)
        {
            UltraGridRow fila = UltraGridNacionalidad.ActiveRow;
            if (UltraGridNacionalidad.Selected.Rows.Count > 0)
            {
                NIVEL_PISO piso = NegMaintenance.recuperaPiso(Convert.ToInt64(fila.Cells["NIV_CODIGO"].Value.ToString()));
                if (piso == null)
                {
                    cmbNivel.SelectedItem = 99;
                    cmbNivel.Text = "TODOS";
                }
                else
                {
                    cmbNivel.SelectedItem = fila.Cells[0].Value.ToString();
                    cmbNivel.Text = piso.NIV_NOMBRE;
                }
                try
                {
                    DataTable bodegaP = NegMaintenance.recuperaBodega(Convert.ToInt64(fila.Cells["Nº BODEGA PEDIDO"].Value.ToString()));
                    cmb_bodega.SelectedItem = fila.Cells["Nº BODEGA PEDIDO"].Value.ToString();
                    cmb_bodega.Text = bodegaP.Rows[0][0].ToString();
                }
                catch (Exception ex)
                {
                    cmb_bodega.SelectedItem = 10;
                    cmb_bodega.Text = "FARMACIA";
                    //throw;
                }
                DataTable bodegaD = NegMaintenance.recuperaBodega(Convert.ToInt64(fila.Cells["Nº BODEGA REPOSICION"].Value.ToString()));
                cmbDespacho.SelectedItem = fila.Cells["Nº BODEGA REPOSICION"].Value.ToString();
                cmbDespacho.Text = bodegaD.Rows[0][0].ToString();

                txtdescripcion.Text = fila.Cells["IP_MAQUINA"].Value.ToString();
                txtnacionalidad.Text = fila.Cells["DESCRIPCION"].Value.ToString();
                txtBodega.Text = fila.Cells["BODEGA PEDIDO"].Value.ToString();
                temp = fila.Cells["IP_MAQUINA"].Value.ToString();
                Editar = true;
                errorProvider1.Clear();
                Desbloquear();
                btnNuevo.Enabled = false;
                btnBorrar.Enabled = true;
            }
        }

        private void UltraGridNacionalidad_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = UltraGridNacionalidad.DisplayLayout.Bands[0];

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;

            UltraGridNacionalidad.DisplayLayout.Bands[0].Columns["PISO"].Width = 200;
            UltraGridNacionalidad.DisplayLayout.Bands[0].Columns["IP_MAQUINA"].Width = 200;
            UltraGridNacionalidad.DisplayLayout.Bands[0].Columns["DESCRIPCION"].Width = 350;
            UltraGridNacionalidad.DisplayLayout.Bands[0].Columns["BODEGA PEDIDO"].Width = 200;
            // se retiran campos que no son Fundamentales
            UltraGridNacionalidad.DisplayLayout.Bands[0].Columns["NIV_CODIGO"].Hidden = true;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            UltraGridNacionalidad.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;
            UltraGridNacionalidad.DisplayLayout.Override.FilterEvaluationTrigger = FilterEvaluationTrigger.OnCellValueChange;
            UltraGridNacionalidad.DisplayLayout.Override.FilterOperatorLocation = FilterOperatorLocation.WithOperand;
            UltraGridNacionalidad.DisplayLayout.Override.FilterClearButtonLocation = FilterClearButtonLocation.RowAndCell;
            //dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro";  
            UltraGridNacionalidad.DisplayLayout.Override.SpecialRowSeparator = SpecialRowSeparator.FilterRow;

        }

        private void frmPisoMaquina_Load(object sender, EventArgs e)
        {
            CargarGrid();
            Bloquear();
            cargarcombo();
        }
    }
}
