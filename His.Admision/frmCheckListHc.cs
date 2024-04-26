using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;

namespace His.Admision
{
    public partial class frmCheckListHc : Form
    {
        NegControlHc Negocio = new NegControlHc();
        private static bool Editar;
        public frmCheckListHc()
        {
            InitializeComponent();
        }

        private void frmCheckListHc_Load(object sender, EventArgs e)
        {
            Bloquear();
            CargarTabla();
        }
        public void Bloquear()
        {
            btnCancelar.Enabled = false;
            btnGuardar.Enabled = false;
            panelDatos.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Bloquear();
        }
        public void Limpiar()
        {
            txt_codigo.Text = "";
            txt_descripcion.Text = "";
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Desbloquear();
            txt_codigo.Text = Convert.ToString(Convert.ToInt32(Negocio.Ultimo()) + 1);
            Editar = false;
        }
        public void Desbloquear()
        {
            btnEliminar.Enabled = true;
            btnCancelar.Enabled = true;
            btnGuardar.Enabled = true;
            panelDatos.Enabled = true;
            btnNuevo.Enabled = true;
        }
        public void CargarTabla()
        {
            TablaControl.DataSource = Negocio.TablaControl();
        }

        private void TablaControl_DoubleClick(object sender, EventArgs e)
        {
            if(TablaControl.SelectedRows.Count > 0)
            {
                if(MessageBox.Show("Desea Editar El Registro Seleccionado?", "Warning", MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation) ==
                    DialogResult.Yes)
                {
                    txt_codigo.Text = TablaControl.CurrentRow.Cells[0].Value.ToString();
                    txt_descripcion.Text = TablaControl.CurrentRow.Cells[1].Value.ToString();
                    Editar = true;
                    Desbloquear();
                }
            }
            else
            {

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(Editar == true)
            {
                if(txt_descripcion.Text != null || txt_descripcion.Text != "")
                {
                    try
                    {
                        Negocio.InsertarActualizar(txt_codigo.Text, txt_descripcion.Text);
                        MessageBox.Show("Los Datos ha Sido Actualizados Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Bloquear();
                        CargarTabla();
                        Editar = false;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                if(txt_descripcion.Text != null || txt_descripcion.Text != "")
                {
                    try
                    {
                        Negocio.InsertarActualizar(txt_codigo.Text, txt_descripcion.Text);
                        MessageBox.Show("Se ha Guardado Correctamente.", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Bloquear();
                        CargarTabla();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string codigo;
            if(TablaControl.SelectedRows.Count > 0 && TablaControl.SelectedRows.Count == 1)
            {
                codigo = TablaControl.CurrentRow.Cells[0].Value.ToString();
                if(MessageBox.Show("¿Esta Seguro de Eliminar Registro?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                    == DialogResult.Yes)
                {
                    try
                    {
                        Negocio.ControlEliminar(codigo);
                        MessageBox.Show("Se ha Eliminado Correctamente", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Bloquear();
                        CargarTabla();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe Seleccionar la Fila ha Eliminar", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
