using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using His.Entidades; 

namespace TarifariosUI
{
    public partial class frmBusquedaTarifario : Form
    {
        private HIS3000BDEntities  conexion;
        public frmBusquedaTarifario()
        {
            InitializeComponent();
            conexion = new HIS3000BDEntities(ConexionEntidades.ConexionEDM); 
            cargarFiltros();
            limpiarHonorariosList();
        }

        private void frmBusquedaTarifario_Load(object sender, EventArgs e)
        {

        }
        public void cargarFiltros()
        {
            var tarifarioQuery = from t in conexion.TARIFARIOS.Include("ESPECIALIDADES_TARIFARIOS")
                                 orderby t.TAR_NOMBRE
                                 select t;
            var aseguradorasQuery = from a in conexion.ASEGURADORAS_EMPRESAS
                                    orderby a.ASE_NOMBRE
                                    select a;
            try
            {
                // lleno la lista de tarifarios
                this.tarifarioList.DataSource = tarifarioQuery;
                this.tarifarioList.DisplayMember = "TAR_NOMBRE";
                // lleno la lista de aseguradoras
                this.aseguradoraList.DataSource = aseguradorasQuery;
                this.aseguradoraList.DisplayMember = "ASE_NOMBRE";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void cargarEspecialidadesTarifario()
        {
            tvEspecialidades.Nodes.Clear();
            try
            {
                // Añado la Raiz del treeview
                TreeNode raizNode = new TreeNode();
                raizNode.Text = "Todos";
                raizNode.Tag = 0;
                this.tvEspecialidades.Nodes.Add(raizNode);
                TARIFARIOS tarifario = (TARIFARIOS)this.tarifarioList.SelectedItem;
                //int codigoPadre = Convert.ToInt32(this.tvEspecialidades.SelectedNode.Tag.ToString());
                cargarNodosHijos(tarifario.TAR_CODIGO, 0, raizNode);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }    
  
        }

        private void cargarNodosHijos(Int64 codigoTarifario,Int64 nodoPadre,TreeNode nodePadre)
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

        public void cargarDetalleTarifario(string filtro,string tipo)
        {
            TARIFARIOS tarifario = (TARIFARIOS)tarifarioList.SelectedItem;

            if (tipo.Equals("referencia"))
            {
                var tarifarioDetalleQuery = from t in conexion.TARIFARIOS_DETALLE
                                            where t.ESPECIALIDADES_TARIFARIOS.TARIFARIOS.TAR_CODIGO == tarifario.TAR_CODIGO
                                            && t.TAD_REFERENCIA.Contains(filtro)
                                            orderby t.TAD_CODIGO
                                            select new {t.TAD_CODIGO,t.TAD_REFERENCIA,t.TAD_NOMBRE,t.TAD_UVR,t.TAD_ANESTESIA};
                try
                {
                    // lleno la lista de tarifarios
                    this.tarifariosDetalleGrid.DataSource = tarifarioDetalleQuery;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (tipo.Equals("descripcion"))
            {
                var tarifarioDetalleQuery = from t in conexion.TARIFARIOS_DETALLE
                                            where t.ESPECIALIDADES_TARIFARIOS.TARIFARIOS.TAR_CODIGO == tarifario.TAR_CODIGO
                                            && t.TAD_DESCRIPCION.Contains(filtro)
                                            orderby t.TAD_CODIGO
                                            select new { t.TAD_CODIGO, t.TAD_REFERENCIA, t.TAD_NOMBRE, t.TAD_UVR, t.TAD_ANESTESIA };
                try
                {
                    // lleno la lista de tarifarios
                    this.tarifariosDetalleGrid.DataSource = tarifarioDetalleQuery;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void cargarDetalleTarifario()
        {
            TARIFARIOS tarifario = (TARIFARIOS)tarifarioList.SelectedItem;

            var tarifarioDetalleQuery = from t in conexion.TARIFARIOS_DETALLE
                                        where t.ESPECIALIDADES_TARIFARIOS.TARIFARIOS.TAR_CODIGO == tarifario.TAR_CODIGO
                                        orderby t.TAD_CODIGO
                                        select new { t.TAD_CODIGO, t.TAD_REFERENCIA, t.TAD_NOMBRE, t.TAD_UVR, t.TAD_ANESTESIA };
            try
            {
                // lleno la lista de tarifarios
                this.tarifariosDetalleGrid.DataSource = tarifarioDetalleQuery;
                //this.tarifariosDetalleGrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void cargarDetalleTarifario(int codigoEspecialidad)
        {
            TARIFARIOS tarifario = (TARIFARIOS)tarifarioList.SelectedItem;

            var tarifarioDetalleQuery = from t in conexion.TARIFARIOS_DETALLE
                                        where t.ESPECIALIDADES_TARIFARIOS.TARIFARIOS.TAR_CODIGO == tarifario.TAR_CODIGO
                                            && t.ESPECIALIDADES_TARIFARIOS.EST_CODIGO == codigoEspecialidad 
                                        orderby t.TAD_CODIGO
                                        select new {t.TAD_CODIGO,t.TAD_REFERENCIA,t.TAD_NOMBRE,t.TAD_UVR,t.TAD_ANESTESIA};
            try
            {
                // lleno la lista de tarifarios
                this.tarifariosDetalleGrid.DataSource = tarifarioDetalleQuery;
                //this.tarifariosDetalleGrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tarifarioList_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarEspecialidadesTarifario();
            cargarDetalleTarifario();
        }
        private void limpiarHonorariosList()
        {
            lvwHonorarios.Items.Clear();
            lvwHonorarios.View = View.Details;
            lvwHonorarios.FullRowSelect = true;
            lvwHonorarios.GridLines = true;
            lvwHonorarios.LabelEdit = false;
            lvwHonorarios.HideSelection = false;
            lvwHonorarios.Columns.Clear();
            //lvwHonorarios.Columns.Add("CODIGO (CPT/TARIFARIO)", 120, HorizontalAlignment.Left);
            //lvwHonorarios.Columns.Add("DETALLE/DESCRIPCION", 350, HorizontalAlignment.Left);
            //lvwHonorarios.Columns.Add("COSTO UNITARIO", 100, HorizontalAlignment.Left);
            //lvwHonorarios.Columns.Add("CANTIDAD", 100, HorizontalAlignment.Left);
            //lvwHonorarios.Columns.Add("COSTO TOTAL", 140, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Cod", 40, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Codigo", 80, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Procedimiento", 350, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Cantidad", 60, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("U. Uvr", 70, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("U. Anestesia", 70, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Valor Uvr", 80, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Valor Anestesia", 80, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Subtotal", 100, HorizontalAlignment.Left);
        }


        //private void añadirHonorario(int codigoEspecialidad, string referencia,string procedimiento,int cantidad,double uvr,double anes,bool conUvr)
        //{
        //    HisModelo.ASEGURADORAS aseguradora = (HisModelo.ASEGURADORAS)aseguradoraList.SelectedItem;
        //    DataGridViewRow fila = tarifariosDetalleGrid.CurrentRow;
        //    HisModelo.CONVENIOS_TARIFARIOS convenios = conexion.CONVENIOS_TARIFARIOS.FirstOrDefault(
        //                                    c => c.ASEGURADORAS.ASE_CODIGO == aseguradora.ASE_CODIGO 
        //                                        && c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO == codigoEspecialidad);

        //    //realizo los calculos para obtener el costo unitario
        //    double costoU=0;
        //    double costoT;
        //    if (conUvr)
        //    {
        //        if (convenios != null)
        //        {
        //            costoU = (Convert.ToDouble(convenios.CON_VALOR_UVR) * uvr);
        //        }
        //    }
        //    else
        //    {
        //        if (convenios != null)
        //        {
        //            costoU = (Convert.ToDouble(convenios.CON_VALOR_ANESTESIA) * anes);
        //        }
        //    }
        //    costoT=cantidad*costoU;
        //    //Agrego una fila al ListView
        //    ListViewItem lista;
        //    lista = lvwHonorarios.Items.Add(referencia);
        //    lista.SubItems.Add(procedimiento);
        //    lista.SubItems.Add(costoU.ToString());
        //    lista.SubItems.Add(cantidad.ToString());
        //    lista.SubItems.Add(costoT.ToString());
        //}

        private void tvEspecialidades_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.tvEspecialidades.SelectedNode.Text != "Todos")
            {
                if (tvEspecialidades.SelectedNode.Nodes.Count == 0)
                { 
                    cargarDetalleTarifario(Convert.ToInt32(tvEspecialidades.SelectedNode.Tag.ToString()));  
                }
            }
            else
            { 
                cargarDetalleTarifario();
            }
        }

        private void tarifariosDetalleGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Convert.ToInt32(tarifariosDetalleGrid.SelectedRows[0].ToString())
            //DataGridViewRow fila = tarifariosDetalleGrid.CurrentRow;
            //añadirHonorario(7,
            //    fila.Cells[1].Value.ToString(),
            //    fila.Cells[2].Value.ToString(),
            //    1,
            //    Convert.ToDouble(fila.Cells[5].Value),
            //    Convert.ToDouble(fila.Cells[6].Value),
            //    true);
            addHonorario();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if(Convert.ToBoolean(this.optCodigo.Checked.ToString()))   
            {
                cargarDetalleTarifario(this.txtBuscar.Text,"referencia");
            }
            else if (Convert.ToBoolean(this.optDescripcion.Checked.ToString()))
            {
                cargarDetalleTarifario(this.txtBuscar.Text, "descripcion");
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            conexion = new HIS3000BDEntities(ConexionEntidades.ConexionEDM); 
            cargarFiltros();
            limpiarHonorariosList();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xla= new Microsoft.Office.Interop.Excel.Application();
 
            xla.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook wb = xla.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)xla.ActiveSheet;
            int i = 1;
            int j = 1;
            foreach (ListViewItem comp in this.lvwHonorarios.Items)
            {
                ws.Cells[i, j] = comp.Text.ToString();
                //MessageBox.Show(comp.Text.ToString());
                foreach (ListViewItem.ListViewSubItem drv in comp.SubItems)
                {
                    ws.Cells[i, j] = drv.Text.ToString();
                    j++;
                }
                j = 1;
                i++;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tarifariosDetalleGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void aseguradoraList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addHonorario()
        {
            bool uvr = false;
            if (Convert.ToBoolean(this.optUvr.Checked.ToString()))
                uvr = true;

            DataGridViewRow fila = tarifariosDetalleGrid.CurrentRow;
            int codigoDetalle = Convert.ToInt32(fila.Cells[0].Value.ToString());
            TARIFARIOS_DETALLE tarifarioDetalle = conexion.TARIFARIOS_DETALLE.FirstOrDefault(
                                                        t => t.TAD_CODIGO == codigoDetalle);
            Int64 codigo = tarifarioDetalle.ESPECIALIDADES_TARIFARIOS.EST_CODIGO;

            addHonorarioDetalle(codigoDetalle , codigo ,
                fila.Cells[1].Value.ToString(),
                fila.Cells[2].Value.ToString(),
                Convert.ToInt32(txtCantidad.Text),
                Convert.ToDouble(fila.Cells[3].Value),
                Convert.ToDouble(fila.Cells[4].Value),
                uvr);
        }
        private void addHonorarioDetalle(int codigoDetalle, Int64 codigoEspecialidad, string referencia, string procedimiento, int cantidad, double uvr, double anes, bool conUvr)
        {
            ASEGURADORAS_EMPRESAS aseguradora = (ASEGURADORAS_EMPRESAS)aseguradoraList.SelectedItem;
            //DataGridViewRow fila = tarifariosDetalleGrid.CurrentRow;
            CONVENIOS_TARIFARIOS convenios = conexion.CONVENIOS_TARIFARIOS.FirstOrDefault(
                                            c => c.ASEGURADORAS_EMPRESAS.ASE_CODIGO == aseguradora.ASE_CODIGO
                                                && c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO == codigoEspecialidad);

            //realizo los calculos para obtener el costo unitario
            double costoU = 0;
            double unidadesUvr = 0;
            double unidadesAnes = 0;
            decimal costoT;
            if (conUvr)
            {
                if (convenios != null)
                {
                    costoU = (Convert.ToDouble(convenios.CON_VALOR_UVR) * uvr);
                }
                else
                {
                    MessageBox.Show("No existe un Convenio", MessageBoxIcon.Information.ToString());
                }
                unidadesUvr = uvr;
            }
            else
            {
                if (convenios != null)
                {
                    costoU = (Convert.ToDouble(convenios.CON_VALOR_ANESTESIA) * anes);
                }
                else
                {
                    MessageBox.Show("No existe un Convenio", MessageBoxIcon.Information.ToString());
                }
                unidadesAnes = anes;
            }
            costoT = Decimal.Round(Convert.ToDecimal(cantidad * costoU), 2);
            //Agrego una fila al ListView
            ListViewItem lista;
            lista = lvwHonorarios.Items.Add(codigoDetalle.ToString());
            lista.SubItems.Add(referencia);
            lista.SubItems.Add(procedimiento);
            lista.SubItems.Add(cantidad.ToString());
            lista.SubItems.Add(unidadesUvr.ToString());
            lista.SubItems.Add(unidadesAnes.ToString());
            if (convenios != null)
            {
                lista.SubItems.Add(convenios.CON_VALOR_UVR.ToString());
                lista.SubItems.Add(convenios.CON_VALOR_ANESTESIA.ToString());
            }
            else
            {
                lista.SubItems.Add("0");
                lista.SubItems.Add("0");
            }
            lista.SubItems.Add(costoT.ToString());
        }

        private void btnAniadir_Click(object sender, EventArgs e)
        {
            addHonorario();
        }

    }
}
