using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades; 

namespace TarifariosUI
{
    public partial class frmAdministrarEstructura : Form
    {
        private HIS3000BDEntities conexion;
 
        public frmAdministrarEstructura()
        {
            InitializeComponent();
            menu.Enabled = His.Parametros.AccesosModuloTarifario.EstructuraEspecialidades;
            adminMenu("");
            limpiarTextBox();
            conexion = new HIS3000BDEntities(ConexionEntidades.ConexionEDM); 
            cargarControles();
        }

        public void cargarControles()
        {
            tvEspecialidades.Nodes.Clear();
            var tarifarioQuery = from t in conexion.TARIFARIOS.Include("ESPECIALIDADES_TARIFARIOS")
                                 orderby t.TAR_NOMBRE
                                 select t;
            try
            {
                // lleno la lista de tarifarios
                this.tarifarioList.DataSource = tarifarioQuery;
                this.tarifarioList.DisplayMember = "TAR_NOMBRE";
                // Añado la Raiz del treeview
                TreeNode raizNode = new TreeNode();
                raizNode.Text = "Todos";
                raizNode.Tag = 0;
                this.tvEspecialidades.Nodes.Add(raizNode);
                TARIFARIOS tarifario = (TARIFARIOS)this.tarifarioList.SelectedItem;
                //int codigoPadre = Convert.ToInt32(this.tvEspecialidades.SelectedNode.Tag.ToString());
                cargarNodosHijos(tarifario.TAR_CODIGO, 0, raizNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cargarNodosHijos(Int64 codigoTarifario, Int64 nodoPadre, TreeNode nodePadre)
        {
            var medicosQuery = from es in conexion.ESPECIALIDADES_TARIFARIOS.Include("TARIFARIOS")
                               where es.EST_PADRE == nodoPadre && es.TARIFARIOS.TAR_CODIGO == codigoTarifario
                               orderby es.EST_NOMBRE
                               select es;
            try
            {
                foreach (var especialidad in medicosQuery)
                {
                    TreeNode node = new TreeNode();
                    //node.Name = especialidad.EST_CODIGO.ToString();
                    node.Text = especialidad.EST_NOMBRE;
                    node.Tag = especialidad.EST_CODIGO;
                    nodePadre.Nodes.Add(node);
                    cargarNodosHijos(codigoTarifario, especialidad.EST_CODIGO, node);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmAdministrarEstructura_Load(object sender, EventArgs e)
        {

        }
        // metodo que limpia los textbox
        protected virtual void limpiarTextBox()
        {
            // hace un chequeo por todos los textbox del formulario
            foreach (Control oControls in this.Controls)
            {
                if (oControls is TextBox)
                {
                    oControls.Text = ""; // eliminar el texto
                }
            }
        }

        protected virtual void adminMenu(String control)
        {
            btnNuevo.Enabled = false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
            btnCerrar.Enabled = false;
            switch (control)
            {
                case "nuevo":
                case "actualizar":
                    btnGuardar.Enabled = true;
                    btnCerrar.Enabled = true;
                    break;
                case "eliminar":
                    btnNuevo.Enabled = false;
                    btnCerrar.Enabled = true;
                    break;
                case "guardar":
                default:
                    btnNuevo.Enabled = true;
                    btnActualizar.Enabled = true;
                    btnEliminar.Enabled = true;
                    btnCerrar.Enabled = true;
                    break;
            }
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            adminMenu("nuevo");
            limpiarTextBox();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            adminMenu("actualizar");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            adminMenu("eliminar");
            limpiarTextBox();
            eliminarItem();
        }

        protected virtual bool eliminarItem()
        {
            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            adminMenu("guardar");
            guardarItem();
        }

        protected virtual bool guardarItem()
        {
            return true;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
                
    }
}
