using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using System.Data.SqlClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using His.Entidades;
using Recursos;
using System.Xml.XPath;

namespace TarifariosUI
{
    public partial class frmAdministrarConvenios : Form
    {
        private HIS3000BDEntities conexion;
        private string  estado = "nuevo";
        private Int64 codigoConvenio = 0; 
        public frmAdministrarConvenios()
        {
            InitializeComponent();
            //menu.Enabled = His.Parametros.AccesosModuloTarifario.ConveniosCRUD;
            conexion = new HIS3000BDEntities(ConexionEntidades.ConexionEDM); 
            cargarFiltros();
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
                this.tarifarioLis.DataSource = tarifarioQuery;
                this.tarifarioLis.DisplayMember = "TAR_NOMBRE";
                //// lleno la lista de aseguradoras
                //this.aseguradoraList.DataSource = aseguradorasQuery;
                //this.aseguradoraList.DisplayMember = "ASE_NOMBRE";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //public void cargarEspecialidadesTarifario()
        //{
        //    tvEspecialidades.Nodes.Clear();
        //    try
        //    {
        //        // Añado la Raiz del treeview
        //        TreeNode raizNode = new TreeNode();
        //        raizNode.Text = "Todos";
        //        raizNode.Tag = 0;
        //        this.tvEspecialidades.Nodes.Add(raizNode);
        //        TARIFARIOS tarifario = (TARIFARIOS)this.tarifarioLis.SelectedItem;
        //        //int codigoPadre = Convert.ToInt32(this.tvEspecialidades.SelectedNode.Tag.ToString());
        //        cargarNodosHijos(tarifario.TAR_CODIGO, 0, raizNode);
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }

        //}
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

        public void cargarEspecialidadesTarifario(TARIFARIOS tarifario)
        {
            tvEspecialidades.Nodes.Clear();
            try
            {
                // Añado la Raiz del treeview
                TreeNode raizNode = new TreeNode();
                raizNode.Text = "Todos";
                raizNode.Tag = 0;
                this.tvEspecialidades.Nodes.Add(raizNode);
                //int codigoPadre = Convert.ToInt32(this.tvEspecialidades.SelectedNode.Tag.ToString());
                HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
                List<ESPECIALIDADES_TARIFARIOS> medicosQuery;
                medicosQuery = contexto.ESPECIALIDADES_TARIFARIOS.Include("TARIFARIOS").Where(es =>
                                es.TARIFARIOS.TAR_CODIGO == tarifario.TAR_CODIGO).ToList();

                cargarNodosHijos(tarifario.TAR_CODIGO, 0, raizNode, medicosQuery);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void cargarNodosHijos(Int64 codigoTarifario, Int64 nodoPadre, TreeNode nodePadre, List<ESPECIALIDADES_TARIFARIOS> especialidades)
        {
            List<ESPECIALIDADES_TARIFARIOS> medicosQuery;
            medicosQuery = especialidades.Where(e => e.EST_PADRE == nodoPadre).ToList();
            try
            {
                foreach (var especialidad in medicosQuery)
                {
                    TreeNode node = new TreeNode();
                    //node.Name = especialidad.EST_CODIGO.ToString();
                    node.Text = especialidad.EST_NOMBRE;
                    node.Tag = especialidad.EST_CODIGO;
                    nodePadre.Nodes.Add(node);
                    cargarNodosHijos(codigoTarifario, especialidad.EST_CODIGO, node, especialidades);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void cargarAseguradoras(Int64 codigoTarifario)
        {
            try
            {
                HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
                List<ASEGURADORAS_EMPRESAS> aseguradorasQuery;
                DateTime fechaActual = DateTime.Now.Date;   

                if (rbtTodos.Checked == true)
                {
                    aseguradorasQuery = (from a in contexto.ASEGURADORAS_EMPRESAS
                                             join c in contexto.CONVENIOS_TARIFARIOS on a.ASE_CODIGO equals c.ASEGURADORAS_EMPRESAS.ASE_CODIGO
                                             join e in contexto.ESPECIALIDADES_TARIFARIOS on c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO equals e.EST_CODIGO
                                             where a.ASE_ESTADO == true && a.ASE_CONVENIO ==true && e.TARIFARIOS.TAR_CODIGO == codigoTarifario
                                             select a).Distinct().OrderBy(a => a.ASE_NOMBRE).ToList();
                }
                else if (rbtVigentes.Checked == true)
                {
                    aseguradorasQuery = (from a in contexto.ASEGURADORAS_EMPRESAS
                                             join c in contexto.CONVENIOS_TARIFARIOS on a.ASE_CODIGO equals c.ASEGURADORAS_EMPRESAS.ASE_CODIGO
                                             join e in contexto.ESPECIALIDADES_TARIFARIOS on c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO equals e.EST_CODIGO
                                             where a.ASE_ESTADO == true && a.ASE_CONVENIO ==true && e.TARIFARIOS.TAR_CODIGO == codigoTarifario && a.ASE_FIN_CONVENIO >= fechaActual   
                                             select a).Distinct().OrderBy(a => a.ASE_NOMBRE).ToList();
                }
                else 
                {
                    aseguradorasQuery = (from a in contexto.ASEGURADORAS_EMPRESAS
                                             join c in contexto.CONVENIOS_TARIFARIOS on a.ASE_CODIGO equals c.ASEGURADORAS_EMPRESAS.ASE_CODIGO
                                             join e in contexto.ESPECIALIDADES_TARIFARIOS on c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO equals e.EST_CODIGO
                                         where a.ASE_ESTADO == true && a.ASE_CONVENIO ==true && e.TARIFARIOS.TAR_CODIGO == codigoTarifario && a.ASE_FIN_CONVENIO < fechaActual  
                                             select a).Distinct().OrderBy(a => a.ASE_NOMBRE).ToList();
                }
                // lleno la lista de aseguradoras
                this.aseguradoraList.DataSource = null;
                this.aseguradoraList.DataSource = aseguradorasQuery;
                this.aseguradoraList.DisplayMember = "ASE_NOMBRE";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmAdministrarConvenios_Load(object sender, EventArgs e)
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
                adminMenu("guardar");
                txtUvr.Enabled = false;
                txtAnestesia.Enabled = false;
                txtAyudantia.Enabled = false;
                int codigoEspecialidad = Convert.ToInt32(tvEspecialidades.SelectedNode.Tag.ToString());
                ASEGURADORAS_EMPRESAS aseguradora = (ASEGURADORAS_EMPRESAS)aseguradoraList.SelectedItem;
                ESPECIALIDADES_TARIFARIOS especialidad = conexion.ESPECIALIDADES_TARIFARIOS.FirstOrDefault(
                                                                    t => t.EST_CODIGO == codigoEspecialidad);
                if (estado == "nuevo")
                {
                    CONVENIOS_TARIFARIOS convenio = new CONVENIOS_TARIFARIOS();
                    Int64 nuevoCodigo;
                    if (conexion.CONVENIOS_TARIFARIOS.Count() > 0)
                    {
                        nuevoCodigo = conexion.CONVENIOS_TARIFARIOS.Max(c => c.CON_CODIGO);
                    }
                    else
                    {
                        nuevoCodigo = 0;
                    }
                    convenio.CON_CODIGO = nuevoCodigo + 1;
                    convenio.CON_FECHA_CREACION = DateTime.Today.Date;
                    convenio.CON_NIVEL_ASEGURADO = 0;
                    if (txtAnestesia.Text != "")
                        convenio.CON_VALOR_ANESTESIA = Convert.ToDecimal(txtAnestesia.Text);
                    else
                        convenio.CON_VALOR_ANESTESIA = 0;
                    if (txtUvr.Text != "")
                        convenio.CON_VALOR_UVR = Convert.ToDecimal(txtUvr.Text);
                    else
                        convenio.CON_VALOR_UVR = 0;

                    if (txtAyudantia.Text != "")
                        convenio.CON_VALOR_AYUDANTIA = Convert.ToDecimal(txtAyudantia.Text);
                    else
                        convenio.CON_VALOR_AYUDANTIA = 0;
                    
                    convenio.ASEGURADORAS_EMPRESASReference.EntityKey  = aseguradora.EntityKey;
                    convenio.ESPECIALIDADES_TARIFARIOSReference.EntityKey = especialidad.EntityKey;
                    conexion.AddToCONVENIOS_TARIFARIOS(convenio);
                    conexion.SaveChanges();

                    guardarInformacionNodos(tvEspecialidades.SelectedNode, aseguradora);
                }
                else if (estado == "actualizar")
                {
                    using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                    {
                        CONVENIOS_TARIFARIOS convenio = contexto.CONVENIOS_TARIFARIOS.FirstOrDefault(c => c.CON_CODIGO == codigoConvenio);

                        convenio.CON_VALOR_ANESTESIA = Convert.ToDecimal(txtAnestesia.Text);
                        convenio.CON_VALOR_UVR = Convert.ToDecimal(txtUvr.Text);
                        convenio.CON_VALOR_AYUDANTIA = Convert.ToDecimal(txtAyudantia.Text);
                        //convenio.ESPECIALIDADES_TARIFARIOS = especialidad;
                        //conexion.AddToCONVENIOS_TARIFARIOS(convenio);
                        contexto.SaveChanges();
                    }
                    guardarInformacionNodos(tvEspecialidades.SelectedNode, aseguradora);
                }
                MessageBox.Show("la información se guardo correctamente", "guardado", MessageBoxButtons.OK, MessageBoxIcon.Information );
                
            }
            catch (Exception err)
            {
                MessageBox.Show("No a elegido una categoria a la cual cambiar los valores", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);        
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            adminMenu("eliminar");
            txtUvr.Enabled = false;
            txtAnestesia.Enabled = false;
            txtAyudantia.Enabled = false;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            adminMenu("actualizar");
            txtUvr.Enabled = true;
            txtAnestesia.Enabled = true;
            txtAyudantia.Enabled = true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {

                    var aseguradorasConConvenio = (from a in contexto.ASEGURADORAS_EMPRESAS
                                                   join c in contexto.CONVENIOS_TARIFARIOS on a.ASE_CODIGO equals c.ASEGURADORAS_EMPRESAS.ASE_CODIGO
                                                   join et in contexto.ESPECIALIDADES_TARIFARIOS on c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO equals et.EST_CODIGO
                                                   orderby a.ASE_NOMBRE ascending
                                                   select a).Distinct();
                    var aseguradoras = (from a in contexto.ASEGURADORAS_EMPRESAS
                                        where a.ASE_ESTADO == true
                                        orderby a.ASE_NOMBRE ascending
                                        select a).Except(aseguradorasConConvenio).ToList();
                    /*select a.ASE_RUC
                        from ASEGURADORAS_EMPRESAS a
                        where a.ASE_ESTADO = 1
                        and a.ASE_RUC not in (select distinct(a.ASE_RUC)
                        from ASEGURADORAS_EMPRESAS a
                        join CONVENIOS_TARIFARIOS c on c.ASE_CODIGO = a.ASE_CODIGO
                        )
                        order by a.ASE_NOMBRE asc */
                    /*var aseguradorasConConvenio = from b in contexto.ASEGURADORAS_EMPRESAS
                                                            join c in contexto.CONVENIOS_TARIFARIOS on b.ASE_CODIGO equals c.ASEGURADORAS_EMPRESAS.ASE_CODIGO
                                                            select b.ASE_RUC;

                    
                    var aseguradoras = (from a in contexto.ASEGURADORAS_EMPRESAS
                                        where a.ASE_ESTADO == true 
                                        select a).Except(aseguradorasConConvenio).ToList();*/

                    //var aseguradoras = (from a in contexto.ASEGURADORAS_EMPRESAS
                    //                    orderby a.ASE_NOMBRE ascending
                    //                    select a.ASE_NOMBRE).ToList();
                    //int x = aseguradorasConConvenio.Count();
                    if (aseguradoras.Count > 0)
                    {
                        aseguradoraList.DataSource = aseguradoras;
                        aseguradoraList.DisplayMember = "ASE_NOMBRE";
                        
                        adminMenu("nuevo");
                        txtUvr.Enabled = true;
                        txtAnestesia.Enabled = true;
                        txtAyudantia.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("No quedan por crearse convenios por favor ", "error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);      
            }
        }
        private  void adminMenu(String control)
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

        private void aseguradoraList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tarifarioLis_SelectedIndexChanged(object sender, EventArgs e)
        {
            TARIFARIOS tarifario = (TARIFARIOS)this.tarifarioLis.SelectedItem;
            cargarEspecialidadesTarifario(tarifario);
            cargarAseguradoras(tarifario.TAR_CODIGO); 
        }

        private void cargarGrilla()
        {
            try
            {
                var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
                List<List<string>> detalleConvenios = new List<List<string>>();
                TARIFARIOS tarifario = (TARIFARIOS)tarifarioLis.SelectedItem;
                ASEGURADORAS_EMPRESAS aseguradora = (ASEGURADORAS_EMPRESAS)aseguradoraList.SelectedItem;

                foreach (TreeNode  item in tvEspecialidades.SelectedNode.Nodes)
                {

                    //Int16[] codigosEspecialidades = new Int16[tvEspecialidades.SelectedNode.Nodes.Count];
                    //for (Int16 i = 0; i < tvEspecialidades.SelectedNode.Nodes.Count; i++)
                    //{
                    //    codigosEspecialidades[i] = Convert.ToInt16(tvEspecialidades.SelectedNode.Nodes[i].Tag.ToString());
                    //}
                    //dgdbDetalleUvr.DataSource = codigosEspecialidades;
                    //using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                    //{
                    //    //var especialidadesTarifario = (from e in contexto.ESPECIALIDADES_TARIFARIOS
                    //    //                              select e).Where(Extensions.BuildContainsExpression<EspecialidadesTarifario, int>(e => e., regIds));
                    //    ASEGURADORAS_EMPRESAS aseguradora = (ASEGURADORAS_EMPRESAS)aseguradoraList.SelectedItem;
                    //    TARIFARIOS tarifario = (TARIFARIOS)tarifarioLis.SelectedItem;    
                    //    var convenios = from c in contexto.CONVENIOS_TARIFARIOS 
                    //                    join a in contexto.ASEGURADORAS_EMPRESAS on c.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO 
                    //                    join ep in contexto.ESPECIALIDADES_TARIFARIOS on c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO equals ep.EST_CODIGO
                    //                    where a.ASE_CODIGO == aseguradora.ASE_CODIGO && ep.TARIFARIOS.TAR_CODIGO == tarifario.TAR_CODIGO
                    //                    select c; 

                    //}

                    List<string> columnas = new List<string>();
                    columnas.Add(item.Tag.ToString()) ;
                    columnas.Add(item.Text); 
                    

                    Int64 codigoEspecialidad = (Int64)item.Tag;

                    //int codigoEspecialidad = Convert.ToInt32(tvEspecialidades.SelectedNode.Tag.ToString());
                    //CONVENIOS_TARIFARIOS convenios = contexto.CONVENIOS_TARIFARIOS.FirstOrDefault(
                    //                                c => c.ASEGURADORAS_EMPRESAS.ASE_CODIGO == aseguradora.ASE_CODIGO
                    //                                    && c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO == codigoEspecialidad);

                    var convenios = (from c in contexto.CONVENIOS_TARIFARIOS
                                    join a in contexto.ASEGURADORAS_EMPRESAS on c.ASEGURADORAS_EMPRESAS.ASE_CODIGO equals a.ASE_CODIGO
                                    join ep in contexto.ESPECIALIDADES_TARIFARIOS on c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO equals ep.EST_CODIGO
                                    where a.ASE_CODIGO == aseguradora.ASE_CODIGO && ep.TARIFARIOS.TAR_CODIGO == tarifario.TAR_CODIGO
                                        && ep.EST_CODIGO == codigoEspecialidad
                                    select c).FirstOrDefault() ; 

                    if (convenios != null)
                    {
                        //codigoConvenio = convenios.CON_CODIGO;
                        //txtUvr.Text = convenios.CON_VALOR_UVR.Value.ToString();
                        //txtAnestesia.Text = convenios.CON_VALOR_ANESTESIA.Value.ToString();
                        columnas.Add (convenios.CON_VALOR_UVR.Value.ToString());
                        columnas.Add(convenios.CON_VALOR_ANESTESIA.Value.ToString());
                        columnas.Add(convenios.CON_VALOR_AYUDANTIA.Value.ToString());
                        //estado = "actualizar";
                    }
                    else
                    {
                        //txtUvr.Text = "";
                        //txtAnestesia.Text = "";
                        //estado = "nuevo";
                        columnas.Add ("S/N");
                        columnas.Add  ("S/N");
                        columnas.Add("S/N");
                    }
                    detalleConvenios.Add(columnas);  
                }

                if (detalleConvenios.Count > 0)
                    dgdbDetalleUvr.DataSource = detalleConvenios.Select(c => new { espCodigo = c[0], espNombre = c[1],valorUvr=c[2],valorAnest=c[3] , valorAyudantia=c[4]}).ToList(); 
                //
                if (dgdbDetalleUvr.Columns.Count > 0)
                {
                    dgdbDetalleUvr.Columns[0].HeaderText = "CODIGO";
                    dgdbDetalleUvr.Columns[1].HeaderText = "CATEGORIA";
                    dgdbDetalleUvr.Columns[2].HeaderText = "VALOR FCM";
                    dgdbDetalleUvr.Columns[3].HeaderText = "VALOR ANESTECIA";
                    dgdbDetalleUvr.Columns[4].HeaderText = "% AYUDANTIA";

                    dgdbDetalleUvr.Columns[0].Width = 50;
                    dgdbDetalleUvr.Columns[1].Width = 250;
                    dgdbDetalleUvr.Columns[2].Width = 75;
                    dgdbDetalleUvr.Columns[3].Width = 75;
                    dgdbDetalleUvr.Columns[4].Width = 75;
                }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);  
            }
        }
        private void cargarConvenio()
        {
            try
            {
                using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                {
                    ASEGURADORAS_EMPRESAS aseguradora = (ASEGURADORAS_EMPRESAS)aseguradoraList.SelectedItem;
                    int codigoEspecialidad = Convert.ToInt32(tvEspecialidades.SelectedNode.Tag.ToString());
                    CONVENIOS_TARIFARIOS convenios = contexto.CONVENIOS_TARIFARIOS.FirstOrDefault(
                                                    c => c.ASEGURADORAS_EMPRESAS.ASE_CODIGO == aseguradora.ASE_CODIGO
                                                        && c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO == codigoEspecialidad);

                    if (convenios != null)
                    {
                        codigoConvenio = convenios.CON_CODIGO;
                        lbCategoria.Text = "CONVENIO PARA " + tvEspecialidades.SelectedNode.Text; 
                        txtUvr.Text = convenios.CON_VALOR_UVR.Value.ToString();
                        txtAnestesia.Text = convenios.CON_VALOR_ANESTESIA.Value.ToString();
                        txtAyudantia.Text = convenios.CON_VALOR_AYUDANTIA.Value.ToString();
                        estado = "actualizar";
                    }
                    else
                    {
                        txtUvr.Text = "";
                        txtAnestesia.Text = "";
                        txtAyudantia.Text = "";
                        estado = "nuevo";
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);   
            }

        }

        private void tvEspecialidades_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (Convert.ToInt64(tvEspecialidades.SelectedNode.Tag) > 0)
            {//tvEspecialidades.SelectedNode.Checked = true;
                //tvEspecialidades.SearbollectedNode.ImageKey = 
                cargarConvenio();
                
            }
            cargarGrilla();
        }

        //private void tvEspecialidades_AfterCheck(object sender, TreeViewEventArgs e)
        //{
        //    if (tvEspecialidades.SelectedNode.Checked)
        //    {
        //        cargarConvenio();

        //    }
        //}

        private void tvEspecialidades_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (tvEspecialidades.SelectedNode != null)
            {
                if (Convert.ToInt16(tvEspecialidades.SelectedNode.Tag) > 0)
                    tvEspecialidades.SelectedNode.Checked = false;
            }
        }

        private void guardarInformacionNodos(TreeNode nodePadre, ASEGURADORAS_EMPRESAS aseguradora)
        {
            try
            {
                if (estado == "nuevo")
                {
                    foreach (TreeNode nodo in nodePadre.Nodes)
                    {
                        int codigoEspecialidad = Convert.ToInt32(nodo.Tag.ToString());
                        ESPECIALIDADES_TARIFARIOS especialidad = conexion.ESPECIALIDADES_TARIFARIOS.FirstOrDefault(
                                                        t => t.EST_CODIGO == codigoEspecialidad);
                        CONVENIOS_TARIFARIOS convenio = new CONVENIOS_TARIFARIOS();
                        Int64 nuevoCodigo = conexion.CONVENIOS_TARIFARIOS.Max(c => c.CON_CODIGO);
                        convenio.CON_CODIGO = nuevoCodigo + 1;
                        convenio.CON_FECHA_CREACION = DateTime.Today.Date;
                        convenio.CON_NIVEL_ASEGURADO = 0;
                        if (txtAnestesia.Text != "")
                            convenio.CON_VALOR_ANESTESIA = Convert.ToDecimal(txtAnestesia.Text);
                        else
                            convenio.CON_VALOR_ANESTESIA = 0;

                        if (txtUvr.Text != "")
                            convenio.CON_VALOR_UVR = Convert.ToDecimal(txtUvr.Text);
                        else
                            convenio.CON_VALOR_UVR = 0;

                        if (txtAyudantia.Text != "")
                            convenio.CON_VALOR_AYUDANTIA = Convert.ToDecimal(txtAyudantia.Text);
                        else
                            convenio.CON_VALOR_AYUDANTIA = 0;
                        convenio.ASEGURADORAS_EMPRESASReference.EntityKey   = aseguradora.EntityKey;
                        convenio.ESPECIALIDADES_TARIFARIOSReference.EntityKey   = especialidad.EntityKey;
                        conexion.AddToCONVENIOS_TARIFARIOS(convenio);

                        conexion.SaveChanges();
                        guardarInformacionNodos(nodo, aseguradora);
                    }
                }
                else if (estado == "actualizar")
                {
                    foreach (TreeNode nodo in nodePadre.Nodes)
                    {
                        using (var contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM))
                        {
                            int codigoEspecialidad = Convert.ToInt32(nodo.Tag.ToString());
                            ESPECIALIDADES_TARIFARIOS especialidad = contexto.ESPECIALIDADES_TARIFARIOS.FirstOrDefault(
                                                            t => t.EST_CODIGO == codigoEspecialidad);
                            CONVENIOS_TARIFARIOS convenio = contexto.CONVENIOS_TARIFARIOS.FirstOrDefault(c => c.ASEGURADORAS_EMPRESAS.ASE_CODIGO == aseguradora.ASE_CODIGO && c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO== especialidad.EST_CODIGO );

                            if (convenio == null)
                            {
                                CONVENIOS_TARIFARIOS convenioNuevo = new CONVENIOS_TARIFARIOS();
                                convenioNuevo.ASEGURADORAS_EMPRESASReference.EntityKey = aseguradora.EntityKey;
                                convenioNuevo.CON_FECHA_CREACION = DateTime.Now.Date;
                                convenioNuevo.CON_NIVEL_ASEGURADO = 0;
                                convenioNuevo.CON_VALOR_ANESTESIA = Convert.ToDecimal(txtAnestesia.Text);
                                convenioNuevo.CON_VALOR_UVR = Convert.ToDecimal(txtUvr.Text);
                                convenioNuevo.CON_VALOR_AYUDANTIA = Convert.ToDecimal(txtAyudantia.Text);
                                convenioNuevo.ESPECIALIDADES_TARIFARIOSReference.EntityKey = especialidad.EntityKey; 

                            }
                            else
                            {
                                convenio.CON_VALOR_ANESTESIA = Convert.ToDecimal(txtAnestesia.Text);
                                convenio.CON_VALOR_UVR = Convert.ToDecimal(txtUvr.Text);
                                convenio.CON_VALOR_AYUDANTIA = Convert.ToDecimal(txtAyudantia.Text);
                            }
                            contexto.SaveChanges();
                        }
                        guardarInformacionNodos(nodo, aseguradora);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            TARIFARIOS tarifario = (TARIFARIOS)this.tarifarioLis.SelectedItem;
            cargarAseguradoras(tarifario.TAR_CODIGO); 
        }

        private void rbtVigentes_CheckedChanged(object sender, EventArgs e)
        {
            TARIFARIOS tarifario = (TARIFARIOS)this.tarifarioLis.SelectedItem;
            cargarAseguradoras(tarifario.TAR_CODIGO); 
        }

        private void rbtTodos_CheckedChanged(object sender, EventArgs e)
        {
            TARIFARIOS tarifario = (TARIFARIOS)this.tarifarioLis.SelectedItem;
            cargarAseguradoras(tarifario.TAR_CODIGO); 
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                TARIFARIOS tarifario = (TARIFARIOS)tarifarioLis.SelectedItem;
                ASEGURADORAS_EMPRESAS aseguradora = (ASEGURADORAS_EMPRESAS)aseguradoraList.SelectedItem;
                frmReportes ventana = new frmReportes(tarifario.TAR_CODIGO);
                ventana.parametro = tarifario.TAR_CODIGO;
                ventana.parametro2 = aseguradora.ASE_CODIGO;
                ventana.reporte = "convenio";
                ventana.ShowDialog();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);   
            }
        }

        private void txtUvr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
                SendKeys.SendWait("{TAB}");
        }

        private void txtAnestesia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
                SendKeys.SendWait("{TAB}");
        }

        private void tarifarioLis_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
                SendKeys.SendWait("{TAB}");
        }

        private void aseguradoraList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
                SendKeys.SendWait("{TAB}");
        }

        private void dgdbDetalleUvr_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
