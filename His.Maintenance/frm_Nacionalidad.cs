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
    public partial class frm_Nacionalidad : Form
    {
        public bool Editar = false;
        PAIS paises;
        public frm_Nacionalidad()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Desbloquear();
            CargarConsecutivo();
            btnNuevo.Enabled = false;
        }
        public void CargarConsecutivo()
        {
            try
            {
                txtcodigo.Text = Convert.ToString(NegMaintenance.ultimaNacionalidad() + 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Bloquear()
        {
            txtcodigo.Enabled = false;
            txtdescripcion.Enabled = false;
            txtnacionalidad.Enabled = false;
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnBorrar.Enabled = false;
        }
        public void Desbloquear()
        {
            txtcodigo.Enabled = true;
            txtdescripcion.Enabled = true;
            txtnacionalidad.Enabled = true;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }
        public void Limpiar()
        {
            txtcodigo.Text = "";
            txtdescripcion.Text = "";
            txtnacionalidad.Text = "";
        }

        private void frm_Nacionalidad_Load(object sender, EventArgs e)
        {
            CargarGrid();
            Bloquear();
        }
        public void CargarGrid()
        {
            try
            {
                UltraGridNacionalidad.DataSource = NegMaintenance.GetNacionalidad();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            paises = new PAIS();
            if (txtdescripcion.Text.Trim() == "")
            {
                errorProvider1.SetError(txtdescripcion, "Campo Obligatorio");
            }
            if(txtnacionalidad.Text.Trim() == "")
            {
                errorProvider1.SetError(txtnacionalidad, "Campo Obligatorio");
            }
            else
            {
                try
                {
                    if (Editar == false)
                    {
                        paises.CODPAIS = Convert.ToInt16(txtcodigo.Text);
                        paises.NOMPAIS = txtdescripcion.Text;
                        paises.NACIONALIDAD = txtnacionalidad.Text;
                        paises.CODAREA = 0;
                        NegMaintenance.CrearNacionalidad(paises);
                        MessageBox.Show("Datos Almacenados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                        Bloquear();
                        CargarGrid();
                        btnNuevo.Enabled = true;
                    }
                    else
                    {
                        NegMaintenance.EditarNacionalidad(Convert.ToInt16(txtcodigo.Text), txtdescripcion.Text, txtnacionalidad.Text);
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

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            UltraGridRow fila = UltraGridNacionalidad.ActiveRow;
            if (UltraGridNacionalidad.Selected.Rows.Count > 0)
            {
                if(MessageBox.Show("¿Esta seguro de eliminar: " + fila.Cells[1].Value.ToString() + "?", "HIS3000",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    try
                    {
                        NegMaintenance.EliminarNacionalidad(txtcodigo.Text);
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

        private void UltraGridNacionalidad_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = UltraGridNacionalidad.DisplayLayout.Bands[0];

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;

            UltraGridNacionalidad.DisplayLayout.Bands[0].Columns[0].Width = 100;
            UltraGridNacionalidad.DisplayLayout.Bands[0].Columns[1].Width = 300;
            UltraGridNacionalidad.DisplayLayout.Bands[0].Columns[2].Width = 300;
        }

        private void UltraGridNacionalidad_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            UltraGridRow fila = UltraGridNacionalidad.ActiveRow;
            if (UltraGridNacionalidad.Selected.Rows.Count > 0)
            {
                txtcodigo.Text = fila.Cells[0].Value.ToString();
                txtdescripcion.Text = fila.Cells[1].Value.ToString();
                txtnacionalidad.Text = fila.Cells[2].Value.ToString();
                Editar = true;
                errorProvider1.Clear();
                Desbloquear();
                btnNuevo.Enabled = false;
                btnBorrar.Enabled = true;
            }
        }
    }
}
