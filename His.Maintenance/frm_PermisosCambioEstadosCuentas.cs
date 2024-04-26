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
    public partial class frm_PermisosCambioEstadosCuentas : Form
    {
        Datos.CambioEstadosCuentas OBJ = new Datos.CambioEstadosCuentas();

        DataTable Usuarios = new DataTable();
        DataTable EstadoDesde = new DataTable();
        DataTable EstadoHasta = new DataTable();
        DataTable ListadoPermisosUsuarios = new DataTable();

        public frm_PermisosCambioEstadosCuentas()
        {
            InitializeComponent();
        }

        private void frm_PermisosCambioEstadosCuentas_Load(object sender, EventArgs e)
        {
            Usuarios = OBJ.CargaUsuarios(1);

            cmbUsuario.DataSource = Usuarios;
            cmbUsuario.DisplayMember = "Usuario";
            cmbUsuario.ValueMember = "Codigo";
            cmbUsuario.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbUsuario.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbUsuario.SelectedIndex = -1;

            EstadoDesde=OBJ.CargaEstadosCuentas();

            cmbCuentaDesde.DataSource = EstadoDesde;
            cmbCuentaDesde.DisplayMember = "Nombre";
            cmbCuentaDesde.ValueMember = "Codigo";
            cmbCuentaDesde.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbCuentaDesde.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbCuentaDesde.SelectedIndex = -1;

            EstadoHasta = OBJ.CargaEstadosCuentas();

            cmbCuentaHasta.DataSource = EstadoHasta;
            cmbCuentaHasta.DisplayMember = "Nombre";
            cmbCuentaHasta.ValueMember = "Codigo";
            cmbCuentaHasta.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbCuentaHasta.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbCuentaHasta.SelectedIndex = -1;

            //ListadoPermisosUsuarios = OBJ.CargaListadoPermisosCuentas(1);
            //this.dgrEstadoUsuario.DataSource = ListadoPermisosUsuarios;

            //dgrEstadoUsuario.Columns["IdUsuario"].Visible = false;
            //dgrEstadoUsuario.Columns["EstadoDesde"].Visible = false;
            //dgrEstadoUsuario.Columns["EstadoHacia"].Visible = false;


        }


        private void cmbUsuario_SelectedValueChanged(object sender, EventArgs e)
        {
            //Int32 Codigo=0;
            //Codigo = Convert.ToInt32( this.cmbUsuario.SelectedValue);
            //ListadoPermisosUsuarios = OBJ.CargaListadoPermisosCuentas(Codigo);
            //this.dgrEstadoUsuario.DataSource = ListadoPermisosUsuarios;

            //dgrEstadoUsuario.Columns["IdUsuario"].Visible = false;
            //dgrEstadoUsuario.Columns["EstadoDesde"].Visible = false;
            //dgrEstadoUsuario.Columns["EstadoHacia"].Visible = false;
        }

        private void cmbUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsuario.SelectedIndex > 0)
            {

                ListadoPermisosUsuarios = OBJ.CargaListadoPermisosCuentas(Convert.ToInt32(cmbUsuario.SelectedValue.ToString()));
                this.dgrEstadoUsuario.DataSource = ListadoPermisosUsuarios;

                dgrEstadoUsuario.Columns["IdUsuario"].Visible = false;
                dgrEstadoUsuario.Columns["EstadoDesde"].Visible = false;
                dgrEstadoUsuario.Columns["EstadoHacia"].Visible = false;

            }
            else
            {

                ListadoPermisosUsuarios = OBJ.CargaListadoPermisosCuentas(1);
                this.dgrEstadoUsuario.DataSource = ListadoPermisosUsuarios;

                dgrEstadoUsuario.Columns["IdUsuario"].Visible = false;
                dgrEstadoUsuario.Columns["EstadoDesde"].Visible = false;
                dgrEstadoUsuario.Columns["EstadoHacia"].Visible = false;

            }
    }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                OBJ.PermisosActualizacionCuentas(Convert.ToInt16(this.cmbUsuario.SelectedValue), Convert.ToInt16(this.cmbCuentaDesde.SelectedValue), Convert.ToInt16(this.cmbCuentaHasta.SelectedValue));

                ListadoPermisosUsuarios = OBJ.CargaListadoPermisosCuentas(Convert.ToInt32(cmbUsuario.SelectedValue));
                this.dgrEstadoUsuario.DataSource = ListadoPermisosUsuarios;

                dgrEstadoUsuario.Columns["IdUsuario"].Visible = false;
                dgrEstadoUsuario.Columns["EstadoDesde"].Visible = false;
                dgrEstadoUsuario.Columns["EstadoHacia"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            DialogResult reply = MessageBox.Show("Realmente Desea Eliminar el Registro ?", "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (reply == DialogResult.No)
            {
                return;
            }


            Int32 codigo = 0;
            Int32 EstadoDesde = 0;
            Int32 EstadoHasta = 0;

            codigo = Convert.ToInt32(dgrEstadoUsuario.CurrentRow.Cells["IdUsuario"].Value.ToString());
            EstadoDesde = Convert.ToInt32(dgrEstadoUsuario.CurrentRow.Cells["EstadoDesde"].Value.ToString());
            EstadoHasta = Convert.ToInt32(dgrEstadoUsuario.CurrentRow.Cells["EstadoHacia"].Value.ToString());

            try
            {
                OBJ.BorraPermisosCuentasUsuarios(codigo, EstadoDesde, EstadoHasta);

                ListadoPermisosUsuarios = OBJ.CargaListadoPermisosCuentas(Convert.ToInt32(cmbUsuario.SelectedValue));
                this.dgrEstadoUsuario.DataSource = ListadoPermisosUsuarios;

                dgrEstadoUsuario.Columns["IdUsuario"].Visible = false;
                dgrEstadoUsuario.Columns["EstadoDesde"].Visible = false;
                dgrEstadoUsuario.Columns["EstadoHacia"].Visible = false;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
            
        }


    }

