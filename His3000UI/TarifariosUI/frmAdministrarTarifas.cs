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
    public partial class frmAdministrarTarifas : Form
    {
        private HIS3000BDEntities conexion;
        private bool estadoIni = false;
        private List<TARIFARIOS_DETALLE> tarifarioDetalleQuery;
        private ESPECIALIDADES_TARIFARIOS especialidad ;
        public frmAdministrarTarifas()
        {
            InitializeComponent();
            tarifarioDetalleQuery = new List<TARIFARIOS_DETALLE>();
            conexion = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
            especialidad =new ESPECIALIDADES_TARIFARIOS();
            cargarListaTarifario();
            limpiarTextBox();
        }

        private void cargarListaTarifario()
        {
            try
            {
                var tarifarioQuery = from t in conexion.TARIFARIOS.Include("ESPECIALIDADES_TARIFARIOS")
                                     orderby t.TAR_NOMBRE
                                     select t;
                // lleno la lista de tarifarios
                this.tarifarioList.DataSource = tarifarioQuery;
                this.tarifarioList.DisplayMember = "TAR_NOMBRE";
                this.tarifarioList.SelectedIndex = 0;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);       
            }
        }
        public void cargarControles()
        {
            try
            {
                tvEspecialidades.Nodes.Clear();
                // Añado la Raiz del treeview
                TreeNode raizNode = new TreeNode();
                raizNode.Text = "Todos";
                raizNode.Tag = 0;
                this.tvEspecialidades.Nodes.Add(raizNode);
                TARIFARIOS tarifario = (TARIFARIOS)this.tarifarioList.SelectedItem;
                
                List<ESPECIALIDADES_TARIFARIOS> medicosQuery;
                medicosQuery = conexion.ESPECIALIDADES_TARIFARIOS.Include("TARIFARIOS").Where(es => 
                                es.TARIFARIOS.TAR_CODIGO == tarifario.TAR_CODIGO).ToList();
                cargarNodosHijos(tarifario.TAR_CODIGO, 0,raizNode ,medicosQuery );
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);     
            }
        }

        //private void cargarNodosHijos(int codigoTarifario, int nodoPadre, TreeNode nodePadre)
        //{
        //    var medicosQuery = from es in conexion.ESPECIALIDADES_TARIFARIOS.Include("TARIFARIOS")
        //                       where es.EST_PADRE == nodoPadre && es.TARIFARIOS.TAR_CODIGO == codigoTarifario
        //                       orderby es.EST_NOMBRE
        //                       select es;
        //    try
        //    {
        //        foreach (var especialidad in medicosQuery)
        //        {
        //            TreeNode node = new TreeNode();
        //            //node.Name = especialidad.EST_CODIGO.ToString();
        //            node.Text = especialidad.EST_NOMBRE;
        //            node.Tag = especialidad.EST_CODIGO;
        //            nodePadre.Nodes.Add(node);
        //            cargarNodosHijos(codigoTarifario, especialidad.EST_CODIGO, node);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void cargarNodosHijos(int codigoTarifario, Int64 nodoPadre, TreeNode nodePadre, List<ESPECIALIDADES_TARIFARIOS> especialidades)
        {
            List<ESPECIALIDADES_TARIFARIOS> medicosQuery;
            medicosQuery = especialidades.Where(e => e.EST_PADRE == nodoPadre).ToList();

            try
            {
                foreach (var especialidad in medicosQuery)
                {
                    TreeNode node = new TreeNode();
                    node.Text = especialidad.EST_NOMBRE;
                    node.Tag = especialidad;
                    nodePadre.Nodes.Add(node);
                    cargarNodosHijos(codigoTarifario, especialidad.EST_CODIGO, node, especialidades);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        protected virtual bool eliminarItem()
        {
            return true;
        }

        protected virtual bool guardarItem()
        {
            return true;
        }


        private void frmAdministrarTarifas_Load(object sender, EventArgs e)
        {
      
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                tvEspecialidades.Focus();
                conexion.SaveChanges();
                guardarItem();
                MessageBox.Show("La información se ha guardado exitosamente");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tvEspecialidades_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.tvEspecialidades.SelectedNode.Text != "Todos")
            {
                if (tvEspecialidades.SelectedNode.Nodes.Count == 0)
                {
                    especialidad = (ESPECIALIDADES_TARIFARIOS)tvEspecialidades.SelectedNode.Tag;    
                    cargarDetalleTarifario(especialidad.EST_CODIGO);
                }
            }
            else
            {
                cargarDetalleTarifario();
            }
        }
        public void cargarDetalleTarifario(Int64 codigoEspecialidad)
        {
            TARIFARIOS tarifario = (TARIFARIOS)tarifarioList.SelectedItem;

            tarifarioDetalleQuery = (from t in conexion.TARIFARIOS_DETALLE
                                        where t.ESPECIALIDADES_TARIFARIOS.TARIFARIOS.TAR_CODIGO == tarifario.TAR_CODIGO
                                            && t.ESPECIALIDADES_TARIFARIOS.EST_CODIGO == codigoEspecialidad
                                        orderby t.TAD_CODIGO
                                        select t).ToList();
            try
            {
                // lleno la lista de tarifarios
                this.tarifariosDetalleGrid.DataSource = tarifarioDetalleQuery;
                SetearGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetearGrid()
        {
            tarifariosDetalleGrid.Columns["ESPECIALIDADES_TARIFARIOS"].Visible = false;
            tarifariosDetalleGrid.Columns["HONORARIOS_TARIFARIO_DETALLE"].Visible = false;
            tarifariosDetalleGrid.Columns["TAD_CODIGO"].HeaderText = "CODIGO";
            tarifariosDetalleGrid.Columns["TAD_NOMBRE"].HeaderText = "NOMBRE";
            tarifariosDetalleGrid.Columns["TAD_RESUMEN"].HeaderText = "RESUMEN";
            tarifariosDetalleGrid.Columns["TAD_DESCRIPCION"].HeaderText = "DESCRIPCION";
            tarifariosDetalleGrid.Columns["TAD_UVR"].HeaderText = "VALOR UVR";
            tarifariosDetalleGrid.Columns["TAD_ANESTESIA"].HeaderText = "VALOR ANEST.";
            tarifariosDetalleGrid.Columns["TAD_REFERENCIA"].HeaderText = "REFERENCIA";
        }

        public void cargarDetalleTarifario()
        {
            TARIFARIOS tarifario = (TARIFARIOS)tarifarioList.SelectedItem;

            var tarifarioDetalleQuery = from t in conexion.TARIFARIOS_DETALLE
                                        where t.ESPECIALIDADES_TARIFARIOS.TARIFARIOS.TAR_CODIGO == tarifario.TAR_CODIGO
                                        orderby t.TAD_CODIGO
                                        select t;
            try
            {
                // lleno la lista de tarifarios
                this.tarifariosDetalleGrid.DataSource = tarifarioDetalleQuery;
                //this.tarifariosDetalleGrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                this.tarifariosDetalleGrid.Columns["ESPECIALIDADES_TARIFARIOS"].Visible = false;
                this.tarifariosDetalleGrid.Columns["HONORARIOS_TARIFARIO_DETALLE"].Visible = false;

                this.tarifariosDetalleGrid.Columns["TAD_CODIGO"].HeaderText = "CODIGO";
                this.tarifariosDetalleGrid.Columns["TAD_NOMBRE"].HeaderText = "NOMBRE";
                this.tarifariosDetalleGrid.Columns["TAD_RESUMEN"].HeaderText = "RESUMEN";
                this.tarifariosDetalleGrid.Columns["TAD_DESCRIPCION"].HeaderText = "DESCRIPCION";
                this.tarifariosDetalleGrid.Columns["TAD_UVR"].HeaderText = "VALOR UVR";
                this.tarifariosDetalleGrid.Columns["TAD_ANESTESIA"].HeaderText = "VALOR ANEST.";
                this.tarifariosDetalleGrid.Columns["TAD_REFERENCIA"].HeaderText = "REFERENCIA";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tarifarioList_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarControles();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
            if (especialidad.EST_CODIGO > 0)
            {
                    var nuevoDetalle = new TARIFARIOS_DETALLE();
                    Int64 nuevoCodigo;
                    if (conexion.TARIFARIOS_DETALLE.Count() > 0)
                    {
                        nuevoCodigo = conexion.TARIFARIOS_DETALLE.Max(t => t.TAD_CODIGO);
                    }
                    else
                    {
                        nuevoCodigo = 1;
                    }
                    nuevoDetalle.TAD_CODIGO = nuevoCodigo+1;
                    nuevoDetalle.TAD_NOMBRE = "";
                    nuevoDetalle.TAD_REFERENCIA = "";
                    nuevoDetalle.TAD_DESCRIPCION = "";
                    nuevoDetalle.TAD_RESUMEN = "";
                    nuevoDetalle.TAD_UVR = 0;
                    nuevoDetalle.ESPECIALIDADES_TARIFARIOSReference.EntityKey = especialidad.EntityKey;
                    conexion.AddToTARIFARIOS_DETALLE(nuevoDetalle);
                    conexion.SaveChanges();
                    cargarDetalleTarifario(especialidad.EST_CODIGO);
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
