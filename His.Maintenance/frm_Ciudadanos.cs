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
using Infragistics.Win.UltraWinGrid;

namespace His.Maintenance
{
    public partial class frm_Ciudadanos : Form
    {
        public int consecutivo = 0;
        public bool Editar = false;
        TIPO_CIUDADANO ciudadano;
        public frm_Ciudadanos()
        {
            InitializeComponent();
        }

        private void frm_Ciudadanos_Load(object sender, EventArgs e)
        {
            Limpiar();
            Bloquear();
            CargarDatos();
        }
        public void CargarDatos()
        {
            try
            {
                UltraGridTipos.DataSource = NegMaintenance.GetCiudadanos();
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
            btnGuardar.Enabled = false;
            btnCancelar.Enabled = false;
            btnBorrar.Enabled = false;
        }
        public void Desbloquear()
        {
            txtcodigo.Enabled = true;
            txtdescripcion.Enabled = true;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }
        public void Limpiar()
        {
            txtcodigo.Text = "";
            txtdescripcion.Text = "";
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
                txtcodigo.Text = Convert.ToString(NegMaintenance.ultimoCodigoTipoCiudadano() + 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ciudadano = new TIPO_CIUDADANO();
            if(txtdescripcion.Text.Trim() == "")
            {
                errorProvider1.SetError(txtdescripcion, "Campo Obligatorio");
            }
            else
            {
                try
                {
                    if (Editar == false)
                    {
                        ciudadano.TC_CODIGO = Convert.ToInt16(txtcodigo.Text);
                        ciudadano.TC_DESCRIPCION = txtdescripcion.Text;
                        ciudadano.TC_ESTADO = true;
                        NegMaintenance.CrearTipoCiudadano(ciudadano);
                        MessageBox.Show("Datos Almacenados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                        Bloquear();
                        CargarDatos();
                        btnNuevo.Enabled = true;
                    }
                    else
                    {
                        NegMaintenance.ModificarTipoCiudadano(txtcodigo.Text, txtdescripcion.Text);
                        MessageBox.Show("Datos Actualizados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                        Bloquear();
                        CargarDatos();
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UltraGridTipos_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = UltraGridTipos.DisplayLayout.Bands[0];

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;

            UltraGridTipos.DisplayLayout.Bands[0].Columns[0].Width = 100;
            UltraGridTipos.DisplayLayout.Bands[0].Columns[1].Width = 400;
        }

        private void UltraGridTipos_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
        {
            UltraGridRow Fila = UltraGridTipos.ActiveRow;
            txtcodigo.Text = Fila.Cells[0].Value.ToString();
            txtdescripcion.Text = Fila.Cells[1].Value.ToString();
            Editar = true;
            errorProvider1.Clear();
            Desbloquear();
            btnNuevo.Enabled = false;
            btnBorrar.Enabled = true;
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
            if(txtdescripcion.Text.Trim() != "")
            {
                try
                {
                    NegMaintenance.EliminarTipoCiudadano(txtcodigo.Text.Trim());
                    MessageBox.Show("Registro eliminado correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnBorrar.Enabled = false;
                    btnNuevo.Enabled = true;
                    Bloquear();
                    Limpiar();
                    CargarDatos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}
