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

namespace His.Maintenance
{
    public partial class frm_Zona : Form
    {
        #region Variables
        List<ZONAS> zonas = new List<ZONAS>();
        List<DtoLocales> locales = new List<DtoLocales>();
        #endregion
        
        #region Contructores
        public frm_Zona()
        {
            InitializeComponent();
           
        }

        #endregion

        #region Metodos Privados
        private void CargaArbol()
        {
            //inicia adiriendo treeview node base
            treeView1.Nodes.Clear();
            TreeNode NodoPrincipal = new TreeNode();
            NodoPrincipal.Name = "NodoPrincipal";
            NodoPrincipal.Text = "Zonas";
            this.treeView1.Nodes.Add(NodoPrincipal);
            treeView1.ImageIndex = 0;
            treeView1.SelectedImageIndex = 1;
            treeView1.SelectedNode = NodoPrincipal;
            
            //zonas = NegZonas.RecuperaZonas();
            //if (zonas.Count > 0)
            //{
            //    foreach (var zona in zonas)
            //    {
            //        this.treeView1.SelectedNode.Nodes.Add(zona.CODZONA.ToString(), zona.NOMZONA.ToString(), 3, 2);
            //    }

            //}

             
        }
        #endregion

        #region Eventos
        private void frm_Zona_Load(object sender, EventArgs e)
        {
            CargaArbol();
            treeView1.ExpandAll();
        }
        private void btn_salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (treeView1.SelectedNode.Name == "NodoPrincipal")
                {
                    mnuContext.Show(treeView1, new Point(e.X, e.Y));


                }
                else
                {
                    string t = treeView1.SelectedNode.Index.ToString();
                    t = treeView1.SelectedNode.Parent.Index.ToString();
                    mnuContxtLocal.Show(treeView1, new Point(e.X, e.Y));
                }

            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Name != "NodoPrincipal")
            {
                if (treeView1.SelectedNode.Level == 1)
                {
                    treeView1.SelectedNode.Nodes.Clear();
                    locales = NegLocales.RecuperaLocales().Where(codloc => codloc.CODZONA == Int16.Parse(treeView1.SelectedNode.Name)).ToList();
                    if (locales.Count > 0)
                    {
                        foreach (var locl in locales)
                        {
                            this.treeView1.SelectedNode.Nodes.Add(locl.LOC_CODIGO.ToString(), locl.LOC_NOMBRE.ToString(), 3, 2);

                        }

                    }

                    //encera el grid
                    grid.DataSource = null;
                    grid.Columns.Clear();
                    grid.Rows.Clear();
                    // Carga los datos de los accesos que tiene cada modulo desplegado en el cmb_modulo
                    grid.DataSource = locales;
                    grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    grid.Columns["LOC_CODIGO"].HeaderText = "CODIGO";
                    grid.Columns["LOC_NOMBRE"].HeaderText = "NOMBRE";
                    grid.Columns["LOC_DIRECCION"].HeaderText = "DIRECCION";
                    grid.Columns["LOC_TELEFONO"].HeaderText = "TELEFONO";
                    grid.Columns["LOC_RUC"].HeaderText = "RUC";
                    grid.Columns["LOC_PRINCIPAL"].HeaderText = "PRINCIPAL";
                    grid.Columns["LOC_MATRIZ"].HeaderText = "MATRIZ";
                    //Desahbilitamos las demas columnas
                    grid.Columns["CODZONA"].Visible = false;
                    grid.Columns["CODTIPNEG"].Visible = false;
                    grid.Columns["LOC_AREA"].Visible = false;
                    grid.Columns["CODCIUDAD"].Visible = false;
                    grid.Columns["ID_USUARIO"].Visible = false;
                    grid.Columns["LOC_TEL1"].Visible = false;
                    grid.Columns["LOC_TEL2"].Visible = false;
                    grid.Columns["LOC_FAX"].Visible = false;
                    grid.Columns["LOC_NUMEMPLE"].Visible = false;
                    grid.Columns["LOC_BODEGA"].Visible = false;
                    grid.Columns["LOC_PRIORIDAD"].Visible = false;
                    grid.Columns["LOC_PORCENTAJE_DIS"].Visible = false;
                    grid.Columns["NOMZONA"].Visible = false;
                    grid.Columns["ENTITYSETNAME"].Visible = false;
                    grid.Columns["ENTITYID"].Visible = false;
                }
            }
            else
            {

                treeView1.SelectedNode.Nodes.Clear();
                //nodos secundarios+
                //TreeNode NodoSecundario = new TreeNode();
                zonas = NegZonas.RecuperaZonas();
                if (zonas.Count > 0)
                {
                    foreach (var zona in zonas)
                    {
                        this.treeView1.SelectedNode.Nodes.Add(zona.CODZONA.ToString(), zona.NOMZONA.ToString(), 3, 2);
                    }

                }
                grid.DataSource = null;
                grid.Columns.Clear();
                grid.Rows.Clear();

            }
        }
        private void nuevaZonaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frm_ZonaEdicion();
            frm.Show();
        }
        private void nuevoLocalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZONAS zona = zonas.Where(zon => zon.CODZONA.ToString() == treeView1.SelectedNode.Name).FirstOrDefault();
            Form frm = new frm_local(zona);
            frm.Show();
        }
        private void editaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frm_ZonaEdicion();
            frm.Show();
        }
        private void rToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                //DialogResult resultado;

                //resultado = MessageBox.Show("Desea eliminar Zona?", "Zona", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (resultado == DialogResult.Yes)
                //{
                //    ZONA zonaModificada = new ZONA();
                //    zonaModificada = zonas.Where(zon => zon.CODZONA == Int16.Parse(treeView1.SelectedNode.Name)).FirstOrDefault();

                //    NegZonas.EliminarZona(zonaModificada);
                //    treeView1.CollapseAll();
                //    MessageBox.Show("Datos Eliminados Correctamente");
                //}
            }
            catch (Exception ex)
            {

                MessageBox.Show("Operación Invalida");
            }


        }
        #endregion

        private void frm_Zona_Fill_Panel_PaintClient(object sender, PaintEventArgs e)
        {

        }

        

        
    }
}
