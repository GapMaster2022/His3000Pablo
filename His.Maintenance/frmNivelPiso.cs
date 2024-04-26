using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades;
using Infragistics.Win.UltraWinGrid;

namespace His.Maintenance
{
    public partial class frmNivelPiso : Form
    {
        public bool Editar = false;
        public string temp = "";
        NIVEL_PISO paises;
        public frmNivelPiso()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Desbloquear();
            CargarConsecutivo();
            btnNuevo.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            paises = new NIVEL_PISO();
            temp = txtNomble.Text;
            if (txtNomble.Text.Trim() == "")
            {
                errorProvider1.SetError(txtNomble, "Campo Obligatorio");
                return;
            }
            if (txtNumeroPiso.Text.Trim() == "")
            {
                errorProvider1.SetError(txtNumeroPiso, "Campo Obligatorio");
                return;
            }
            else
            {
                try
                {
                    if (Editar == false)
                    {
                        if (NegMaintenance.existeNivelPiso(temp))
                        {
                            MessageBox.Show("El Piso " + temp + " ya existe", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        paises.NIV_CODIGO = Convert.ToInt16(txtcodigo.Text);
                        paises.NIV_NOMBRE = txtNomble.Text;
                        paises.NIV_NUMERO_PISO = Convert.ToInt16(txtNumeroPiso.Text);
                        NegMaintenance.CrearPiso(paises);
                        MessageBox.Show("Datos Almacenados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                        Bloquear();
                        CargarGrid();
                        btnNuevo.Enabled = true;
                    }
                    else
                    {
                        NegMaintenance.EditarPiso(Convert.ToInt16(txtcodigo.Text), txtNomble.Text,Convert.ToInt16(txtNumeroPiso.Text));
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Editar = false;
            errorProvider1.Clear();
            Bloquear();
            btnNuevo.Enabled = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
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
                        NegMaintenance.EliminarPiso(txtcodigo.Text);
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

        private void frmNivelPiso_Load(object sender, EventArgs e)
        {
            CargarGrid();
            Bloquear();
        }
        public void CargarGrid()
        {
            try
            {
                UltraGridNacionalidad.DataSource = NegMaintenance.GetPiso();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CargarConsecutivo()
        {
            try
            {
                txtcodigo.Text = Convert.ToString(NegMaintenance.ultimPiso() + 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Bloquear()
        {
            txtcodigo.Enabled = false;
            txtNumeroPiso.Enabled = false;
            txtNomble.Enabled = false;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnBorrar.Enabled = false;
        }
        public void Desbloquear()
        {
            txtcodigo.Enabled = true;
            txtNumeroPiso.Enabled = true;
            txtNomble.Enabled = true;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }
        public void Limpiar()
        {
            txtcodigo.Text = "";
            txtNumeroPiso.Text = "";
            txtNomble.Text = "";
        }

        private void UltraGridNacionalidad_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = UltraGridNacionalidad.DisplayLayout.Bands[0];

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;

            UltraGridNacionalidad.DisplayLayout.Bands[0].Columns[0].Width = 100;
            UltraGridNacionalidad.DisplayLayout.Bands[0].Columns[1].Width = 300;
            UltraGridNacionalidad.DisplayLayout.Bands[0].Columns[2].Width = 300;

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            UltraGridNacionalidad.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;
            UltraGridNacionalidad.DisplayLayout.Override.FilterEvaluationTrigger = FilterEvaluationTrigger.OnCellValueChange;
            UltraGridNacionalidad.DisplayLayout.Override.FilterOperatorLocation = FilterOperatorLocation.WithOperand;
            UltraGridNacionalidad.DisplayLayout.Override.FilterClearButtonLocation = FilterClearButtonLocation.RowAndCell;
            //dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro";  
            UltraGridNacionalidad.DisplayLayout.Override.SpecialRowSeparator = SpecialRowSeparator.FilterRow;
        }

        private void UltraGridNacionalidad_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            UltraGridRow fila = UltraGridNacionalidad.ActiveRow;
            if (UltraGridNacionalidad.Selected.Rows.Count > 0)
            {
                txtcodigo.Text = fila.Cells[0].Value.ToString();
                txtNomble.Text = fila.Cells[1].Value.ToString();
                txtNumeroPiso.Text = fila.Cells[2].Value.ToString();
                Editar = true;
                errorProvider1.Clear();
                Desbloquear();
                btnNuevo.Enabled = false;
                btnBorrar.Enabled = true;
            }
        }
    }
}
