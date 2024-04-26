using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Core.Datos;

using Recursos;
using His.Parametros;
using His.Negocio;

namespace TarifariosUI
{
    public partial class ConsultaHonorarios : Form
    {
        //private HIS3000BDEntities  conexion;
        private int codigoHonorario = 0;
        private Boolean guardado = false;
        private string tipomedico = "principal";
        private string procedimiento = "procedimiento1";
        MEDICOS medicoActual;
        PORCENTAJES porcentajeActual;
        double porcentajeFacturar;
        public ConsultaHonorarios()
        {
            InitializeComponent();
            //conexion = new HIS3000BDEntities(ConexionEntidades.ConexionEDM); 
            cargarFiltros();
            limpiarHonorariosList();
            //cargarEncabezadosGrid();
            if (His.Entidades.Clases.Sesion.codMedico <= 0)
            {
                btnGuardar.Enabled = false;
            }
            //inicializar campos
            inicializarCampos();
        }

        private void inicializarCampos()
        {
            porcentajeFacturar = 100;
        }

        public void cargarFiltros()
        {

            HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
            var tarifarioQuery = from t in contexto.TARIFARIOS.Include("ESPECIALIDADES_TARIFARIOS")
                                 orderby t.TAR_CODIGO
                                 select t;

            try
            {
                // lleno la lista de tarifarios
                this.tarifarioList.DataSource = tarifarioQuery;
                this.tarifarioList.DisplayMember = "TAR_NOMBRE";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            medicoActual = contexto.MEDICOS.FirstOrDefault(m => m.MED_CODIGO == His.Entidades.Clases.Sesion.codMedico);//1);//

            if (medicoActual != null)
                lbl_medPrincipal.Text = medicoActual.MED_APELLIDO_PATERNO.Trim() + " " + medicoActual.MED_APELLIDO_MATERNO.Trim() +
                                    " " + medicoActual.MED_NOMBRE1.Trim();
            tarifariosDetalleGrid.DataSource = null;
            tarifariosDetalleGrid.ClearSelection();
            tarifariosDetalleGrid.DataMember = null;
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
            lvwHonorarios.Columns.Add("Cod", 40, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Codigo", 80, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Procedimiento", 350, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Cantidad", 60, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Unitario", 40, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("U. Uvr", 70, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("U. Anestesia", 70, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Valor Uvr", 80, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Valor Anestesia", 80, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Subtotal", 80, HorizontalAlignment.Left);
            lvwHonorarios.Columns.Add("Ayudantia", 80, HorizontalAlignment.Left);
        }


        private void addHonorario()
        {
            try
            {
                HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
                int cant = 0;
                foreach (ListViewItem item in lvwHonorarios.Items)
                {
                    if (item.Text == tarifariosDetalleGrid.CurrentRow.Cells[0].Value.ToString())
                    {
                        cant++;
                    }
                }
                if (cant > 0)
                {
                    if (procedimiento == "procedimiento1")
                        MessageBox.Show("El procedimiento ya fue añadido a los Honorarios Medicos", "Item añadido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        if (procedimiento.Equals("mismaVia"))
                        {
                            int nivel = 0;
                            if (cant == 1)
                                nivel = Convert.ToInt16(porcentajeActual.POR_PORC1);
                            else
                                if (cant == 2)
                                    nivel = Convert.ToInt16(porcentajeActual.POR_PORC2);

                            enviardatosDV(contexto, nivel);
                            return;
                        }
                        else //doble via
                        {
                            int nivel = 0;
                            nivel = Convert.ToInt16(porcentajeActual.POR_DOBLEVIA);
                            enviardatosDV(contexto, nivel);
                        }
                    }
                }
                else
                    enviardatos(contexto);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void tarifariosDetalleGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            addHonorario();
        }

        private void addHonorarioDetalle(int codigoDetalle, Int64 codigoEspecialidad, string referencia, string procedimiento, int cantidad, double uvr, double anes, bool conUvr)
        {
            try
            {
                HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
                ASEGURADORAS_EMPRESAS aseguradora = (ASEGURADORAS_EMPRESAS)aseguradoraList.SelectedItem;
                //DataGridViewRow fila = tarifariosDetalleGrid.CurrentRow;
                CONVENIOS_TARIFARIOS convenios = contexto.CONVENIOS_TARIFARIOS.FirstOrDefault(
                                                c => c.ASEGURADORAS_EMPRESAS.ASE_CODIGO == aseguradora.ASE_CODIGO
                                                    && c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO == codigoEspecialidad);

                //realizo los calculos para obtener el costo unitario
                double costoU = 0;
                decimal unidadesUvr = 0;
                decimal unidadesAnes = 0;
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
                        return;
                    }
                    unidadesUvr = Convert.ToDecimal(uvr);
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
                        return;
                    }
                    unidadesAnes = Convert.ToDecimal(anes);
                }
                //redondear valores

                unidadesUvr = Decimal.Round(unidadesUvr, 2);
                unidadesAnes = Decimal.Round(unidadesAnes, 2);

                Double tempTotal = costoU * (porcentajeFacturar / 100);
                costoT = Decimal.Round((Decimal.Round(Convert.ToDecimal(tempTotal), 2) * cantidad), 2);
                //Agrego una fila al ListView
                ListViewItem lista = new ListViewItem();
                lista = lvwHonorarios.Items.Add(codigoDetalle.ToString(), 0);
                lista.SubItems.Add(referencia);
                lista.SubItems.Add(procedimiento);
                lista.SubItems.Add(cantidad.ToString());
                lista.SubItems.Add(Convert.ToString(tempTotal));
                lista.SubItems.Add(unidadesUvr.ToString());
                lista.SubItems.Add(unidadesAnes.ToString());
                if (convenios != null)
                {
                    if ((tipomedico == "ayudante1"))
                    {
                        convenios.CON_VALOR_UVR = convenios.CON_VALOR_UVR;
                        convenios.CON_VALOR_ANESTESIA = convenios.CON_VALOR_ANESTESIA;
                        convenios.CON_VALOR_AYUDANTIA = (convenios.CON_VALOR_AYUDANTIA * costoT) / 100;
                        lista.SubItems.Add(convenios.CON_VALOR_UVR.ToString());
                        lista.SubItems.Add(convenios.CON_VALOR_ANESTESIA.ToString());
                        //lista.SubItems.Add(convenios.CON_VALOR_AYUDANTIA.ToString());
                    }
                    else
                    {
                        if ((tipomedico == "principal") || (tipomedico == "secundario"))
                        {
                            lista.SubItems.Add(convenios.CON_VALOR_UVR.ToString());
                            lista.SubItems.Add(convenios.CON_VALOR_ANESTESIA.ToString());
                            //lista.SubItems.Add("0");
                        }
                        else
                        {
                            convenios.CON_VALOR_UVR = convenios.CON_VALOR_UVR;
                            convenios.CON_VALOR_ANESTESIA = convenios.CON_VALOR_ANESTESIA;
                            convenios.CON_VALOR_AYUDANTIA2 = (convenios.CON_VALOR_AYUDANTIA2 * costoT) / 100;
                            lista.SubItems.Add(convenios.CON_VALOR_UVR.ToString());
                            lista.SubItems.Add(convenios.CON_VALOR_AYUDANTIA2.ToString());
                            //lista.SubItems.Add(convenios.CON_VALOR_AYUDANTIA.ToString());

                        }

                    }
                }
                else
                {
                    lista.SubItems.Add("0");
                    lista.SubItems.Add("0");
                    lista.SubItems.Add("0");
                }


                Decimal total = 0;
                if ((tipomedico == "principal") || (tipomedico == "secundario"))
                {
                    lista.SubItems.Add(costoT.ToString());
                    lista.SubItems.Add("0");
                    total = (Convert.ToDecimal(txtTotal.Text) + costoT);
                }
                else
                {
                    if (tipomedico == "ayudante1")
                    {
                        lista.SubItems.Add(convenios.CON_VALOR_AYUDANTIA.ToString());
                        lista.SubItems.Add(convenios.CON_VALOR_AYUDANTIA.ToString());
                        total = ((Convert.ToDecimal(txtTotal.Text)) + Convert.ToDecimal(convenios.CON_VALOR_AYUDANTIA));
                    }
                    else
                    {
                        lista.SubItems.Add(convenios.CON_VALOR_AYUDANTIA2.ToString());
                        lista.SubItems.Add(convenios.CON_VALOR_AYUDANTIA2.ToString());
                        total = ((Convert.ToDecimal(txtTotal.Text)) + Convert.ToDecimal(convenios.CON_VALOR_AYUDANTIA2));
                    }
                }
                txtTotal.Text = total.ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addHonorarioDetalle(int codigoDetalle, int codigoEspecialidad, string referencia, string procedimiento, int cantidad, double uvr, double anes, bool conUvr, int idseguro)
        {
            try
            {
                HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
                ASEGURADORAS_EMPRESAS aseguradora = (ASEGURADORAS_EMPRESAS)aseguradoraList.SelectedItem;
                //DataGridViewRow fila = tarifariosDetalleGrid.CurrentRow;
                CONVENIOS_TARIFARIOS convenios = contexto.CONVENIOS_TARIFARIOS.FirstOrDefault(
                                                c => c.ASEGURADORAS_EMPRESAS.ASE_CODIGO == aseguradora.ASE_CODIGO
                                                    && c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO == codigoEspecialidad);

                //realizo los calculos para obtener el costo unitario
                double costoU = 0;
                decimal unidadesUvr = 0;
                decimal unidadesAnes = 0;
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
                    unidadesUvr = Convert.ToDecimal(uvr);
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
                    unidadesAnes = Convert.ToDecimal(anes);
                }
                //redondear valores

                unidadesUvr = Decimal.Round(unidadesUvr, 2);
                unidadesAnes = Decimal.Round(unidadesAnes, 2);

                costoT = Decimal.Round(Convert.ToDecimal(cantidad * costoU), 2);
                //Agrego una fila al ListView
                ListViewItem lista;
                lista = lvwHonorarios.Items.Add(codigoDetalle.ToString(), 0);
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
                Decimal total = 0;
                total = (Convert.ToDecimal(txtTotal.Text) + costoT);
                txtTotal.Text = total.ToString();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void tvEspecialidades_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.tvEspecialidades.SelectedNode.Text != "Todos")
            {
                //if (tvEspecialidades.SelectedNode.Nodes.Count == 0)
                //{
                cargarDetalleTarifario(Convert.ToInt32(tvEspecialidades.SelectedNode.Tag.ToString()));
                //}
            }
            else
            {
                cargarDetalleTarifario();
            }
        }

        private void tarifarioList_SelectedIndexChanged(object sender, EventArgs e)
        {
            TARIFARIOS tarifario = (TARIFARIOS)this.tarifarioList.SelectedItem;
            cargarEspecialidadesTarifario(tarifario);
            cargarAseguradoras(tarifario.TAR_CODIGO);
            if (this.Visible == true)
            {
                cargarDetalleTarifario();
            }
        }


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
                                es.TARIFARIOS.TAR_CODIGO == tarifario.TAR_CODIGO).OrderBy(o => o.EST_NOMBRE).ToList();

                cargarNodosHijos(tarifario.TAR_CODIGO, 0, raizNode, medicosQuery);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ValorPadre(int CodEspecialidad, List<ESPECIALIDADES_TARIFARIOS> especialidades)
        {
            List<ESPECIALIDADES_TARIFARIOS> medicosQuery;
            medicosQuery = especialidades.Where(e => e.EST_CODIGO == CodEspecialidad).ToList();
            try
            {
                foreach (var especialidad in medicosQuery)
                {
                   string  codigo = especialidad.EST_PADRE.ToString();
                   if (codigo.Trim() == "0")                   
                   {
                       MessageBox.Show(especialidad.EST_NOMBRE.ToString());
                   } 
                    //ValorPadre(Convert.ToInt16(codigo.Trim()),especialidad);
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cargarNodosHijos(int codigoTarifario, Int64 nodoPadre, TreeNode nodePadre, List<ESPECIALIDADES_TARIFARIOS> especialidades)
        {
            List<ESPECIALIDADES_TARIFARIOS> medicosQuery;
            medicosQuery = especialidades.Where(e => e.EST_PADRE == nodoPadre).ToList();
            //var medicosQuery = from es in conexion.ESPECIALIDADES_TARIFARIOS.Include("TARIFARIOS")
            //                   where es.EST_PADRE == nodoPadre && es.TARIFARIOS.TAR_CODIGO == codigoTarifario
            //                   orderby es.EST_NOMBRE
            //                   select es;
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

        public void cargarDetalleTarifario(string filtro, string tipo)
        {
            HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
            TARIFARIOS tarifario = (TARIFARIOS)tarifarioList.SelectedItem;
            int cant = 100;

            if (tipo.Equals("referencia"))
            {

                if ((this.comboBox1.Text != "Todos") && (this.comboBox1.Text != ""))
                    cant = Convert.ToInt32(this.comboBox1.Text);

                var tarifarioDetalleQuery = (from t in contexto.TARIFARIOS_DETALLE
                                             where t.ESPECIALIDADES_TARIFARIOS.TARIFARIOS.TAR_CODIGO == tarifario.TAR_CODIGO
                                             && t.TAD_REFERENCIA.Contains(filtro)
                                             orderby t.TAD_CODIGO
                                             select new { t.TAD_CODIGO, t.TAD_REFERENCIA, t.TAD_NOMBRE, t.TAD_UVR, t.TAD_ANESTESIA }).Take(cant).ToList();
                try
                {
                    // lleno la lista de tarifarios
                    this.tarifariosDetalleGrid.DataSource = tarifarioDetalleQuery;
                    cargarEncabezadosGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (tipo.Equals("descripcion"))
            {
                var tarifarioDetalleQuery = (from t in contexto.TARIFARIOS_DETALLE
                                             where t.ESPECIALIDADES_TARIFARIOS.TARIFARIOS.TAR_CODIGO == tarifario.TAR_CODIGO
                                             && t.TAD_DESCRIPCION.Contains(filtro)
                                             orderby t.TAD_CODIGO
                                             select new { t.TAD_CODIGO, t.TAD_REFERENCIA, t.TAD_NOMBRE, t.TAD_UVR, t.TAD_ANESTESIA }).Take(cant).ToList();
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

        public void cargarEncabezadosGrid()
        {
            try
            {
                this.tarifariosDetalleGrid.Columns["TAD_CODIGO"].Visible = false;
                //this.tarifariosDetalleGrid.Columns["TAD_CODIGO"].HeaderText = "CODIGO";
                //this.tarifariosDetalleGrid.Columns["TAD_CODIGO"].Width = 80;
                this.tarifariosDetalleGrid.Columns["TAD_REFERENCIA"].HeaderText = "REFERENCIA";
                this.tarifariosDetalleGrid.Columns["TAD_REFERENCIA"].Width = 100;
                this.tarifariosDetalleGrid.Columns["TAD_NOMBRE"].HeaderText = "DESCRIPCION";
                this.tarifariosDetalleGrid.Columns["TAD_NOMBRE"].Width = 560;
                this.tarifariosDetalleGrid.Columns["TAD_UVR"].HeaderText = "UVR";
                this.tarifariosDetalleGrid.Columns["TAD_UVR"].Width = 60;
                this.tarifariosDetalleGrid.Columns["TAD_ANESTESIA"].HeaderText = "ANESTESIA";
                this.tarifariosDetalleGrid.Columns["TAD_ANESTESIA"].Width = 60;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        public void cargarDetalleTarifario()
        {
            try
            {
                HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
                TARIFARIOS tarifario = (TARIFARIOS)tarifarioList.SelectedItem;
                int cant = 100;
                if ((this.comboBox1.Text != "Todos") && (this.comboBox1.Text != ""))
                    cant = Convert.ToInt32(this.comboBox1.Text);
                var tarifarioDetalleQuery = (from t in contexto.TARIFARIOS_DETALLE
                                             where t.ESPECIALIDADES_TARIFARIOS.TARIFARIOS.TAR_CODIGO == tarifario.TAR_CODIGO
                                             orderby t.TAD_CODIGO
                                             select new { t.TAD_CODIGO, t.TAD_REFERENCIA, t.TAD_NOMBRE, t.TAD_UVR, t.TAD_ANESTESIA }).Take(cant).ToList();

                // lleno la lista de tarifarios
                this.tarifariosDetalleGrid.DataSource = tarifarioDetalleQuery;
                //this.tarifariosDetalleGrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                cargarEncabezadosGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void cargarDetalleTarifario(int codigoEspecialidad)
        {
            HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
            TARIFARIOS tarifario = (TARIFARIOS)tarifarioList.SelectedItem;
            int cant = 100;
            if ((this.comboBox1.Text != "Todos") && (this.comboBox1.Text != ""))
                cant = Convert.ToInt32(this.comboBox1.Text);
            var tarifarioDetalleQuery = (from t in contexto.TARIFARIOS_DETALLE
                                         where t.ESPECIALIDADES_TARIFARIOS.TARIFARIOS.TAR_CODIGO == tarifario.TAR_CODIGO
                                             && t.ESPECIALIDADES_TARIFARIOS.EST_CODIGO == codigoEspecialidad
                                         orderby t.TAD_CODIGO
                                         select new { t.TAD_CODIGO, t.TAD_REFERENCIA, t.TAD_NOMBRE, t.TAD_UVR, t.TAD_ANESTESIA }).Take(cant).ToList();
            try
            {
                // lleno la lista de tarifarios
                this.tarifariosDetalleGrid.DataSource = tarifarioDetalleQuery;
                //this.tarifariosDetalleGrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                cargarEncabezadosGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void aseguradoraList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtCodigoPaciente_KeyDown(object sender, KeyEventArgs e)
        {
            //HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);  
            if (e.KeyCode == Keys.F1)
            {
                //var QueryPacientes = from p in contexto.PACIENTES
                //                     where p.PAC_ESTADO == true 
                //                     orderby p.PAC_APELLIDO_PATERNO
                //                     select(new {p.PAC_CODIGO,p.PAC_HISTORIA_CLINICA,PAC_APELLIDO_PATERNO = (p.PAC_APELLIDO_PATERNO + " "+ p.PAC_APELLIDO_MATERNO),p.PAC_NOMBRE1}) ;
                //List<HisModelo.PACIENTES>    pacientes;
                //pacientes = QueryPacientes.ToList();
                //frmListas lista = new frmListas("pacientes",txtCodigoPaciente);
                //lista.Show(); 
                frm_AyudaPacientes frm = new frm_AyudaPacientes();
                frm.campoPadre = txtCodigoPaciente;
                frm.ShowDialog();
            }
        }
        public void cargarPaciente(int codigo)
        {
            HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
            //PACIENTES paciente = contexto.PACIENTES.FirstOrDefault(
            //                        p => p.PAC_CODIGO == codigo);
            var infPaciente = (from p in contexto.PACIENTES
                               join d in contexto.PACIENTES_DATOS_ADICIONALES on p.PAC_CODIGO equals d.PACIENTES.PAC_CODIGO
                               where d.DAP_ESTADO == true && p.PAC_CODIGO == codigo
                               select new { p.PAC_HISTORIA_CLINICA, p.PAC_APELLIDO_PATERNO, p.PAC_APELLIDO_MATERNO, p.PAC_NOMBRE1, p.PAC_NOMBRE2, d.DAP_DIRECCION_DOMICILIO, p.PAC_IDENTIFICACION }).FirstOrDefault();
            txtPacienteHCL.Text = infPaciente.PAC_HISTORIA_CLINICA;
            txtFacturaMedico.Text = "0";
            txtPacienteNombre.Text = infPaciente.PAC_APELLIDO_PATERNO + " " +
                                     infPaciente.PAC_APELLIDO_MATERNO + " " + infPaciente.PAC_NOMBRE1 + " " + infPaciente.PAC_NOMBRE2;
            txtPacienteDireccion.Text = infPaciente.DAP_DIRECCION_DOMICILIO;
            txtCedula.Text = infPaciente.PAC_IDENTIFICACION;
        }

        private void txtCodigoPaciente_TextChanged(object sender, EventArgs e)
        {
            if (txtCodigoPaciente.Text != "")
            {
                cargarPaciente(Convert.ToInt32(txtCodigoPaciente.Text.ToString()));
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCrearHonorarios_Load(object sender, EventArgs e)
        {

            try
            {
                His.Parametros.ArchivoIni archivo = new ArchivoIni(Application.StartupPath + @"\his3000.ini");
                string codParametro = archivo.IniReadValue("parametros", "cod");
                var usuarioporcentaje = NegPorcentajes.RecuperaPorcentaje().Where(cod => cod.POR_CODIGO.ToString() == codParametro).FirstOrDefault();

                if (porcentajeActual != null)
                    porcentajeActual = usuarioporcentaje;
                else
                    porcentajeActual = NegPorcentajes.RecuperaPorcentaje().Where(cod => cod.POR_CODIGO == 1).FirstOrDefault();

                //cargo valores por defecto
                this.WindowState = FormWindowState.Maximized;
                btnNuevo.Image = Archivo.imgBtnAdd2;
                btnGuardar.Image = Archivo.imgBtnGoneSave48;
                btnImprimir.Image = Archivo.imgBtnGonePrint48;
                btnExportarExcel.Image = Archivo.imgOfficeExcel;
                btnBuscarHonorarios.Image = Archivo.imgBtnSearch;
                btnSalir.Image = Archivo.imgBtnGoneExit48;
                btnParametros.Image = Archivo.imgBtnGoneEmblem48;
                splitContainer1.SplitterDistance = 350;

                //cambio el fondo del menu dependiendo de la resolucion de pantalla
                int deskHeight = Screen.PrimaryScreen.Bounds.Height;
                int deskWidth = Screen.PrimaryScreen.Bounds.Width;

                if (deskWidth >= 1024 && deskWidth < 1280)
                    panelFiltros.Appearance.ImageBackground = Archivo.fondoA1x1024x73;
                else if (deskWidth >= 1280 && deskWidth < 1360)
                    panelFiltros.Appearance.ImageBackground = Archivo.fondoA1x1280x92;
                else if (deskWidth >= 1360 && deskWidth < 1600)
                    panelFiltros.Appearance.ImageBackground = Archivo.fondoA1x1360x97;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(this.optCodigo.Checked.ToString()))
            {
                cargarDetalleTarifario(this.txtBuscar.Text, "referencia");
            }
            else if (Convert.ToBoolean(this.optDescripcion.Checked.ToString()))
            {
                cargarDetalleTarifario(this.txtBuscar.Text, "descripcion");
            }
        }

        private void btnAniadir_Click(object sender, EventArgs e)
        {

            if (tipomedico == String.Empty)
                MessageBox.Show("Escoga el tipo de Medico", "Alerta");
            else
                addHonorario();
        }

        private void panelMarcoo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lvwHonorarios_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    foreach (ListViewItem items in lvwHonorarios.SelectedItems)
                    {
                        lvwHonorarios.Items.Remove(items);
                    }
                    Decimal total = 0;
                    foreach (ListViewItem fila in lvwHonorarios.Items)
                    {
                        total += Convert.ToDecimal(fila.SubItems[8].Text);
                    }
                    txtTotal.Text = total.ToString();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (His.Entidades.Clases.Sesion.codMedico != 0)
            {
                try
                {
                    HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
                    if (txtPacienteHCL.Text.Trim() == "" || txtPacienteNombre.Text.Trim() == "")
                    {
                        MessageBox.Show("Por favor ingrese la información del paciente antes de guardar", "Seleeción de Paciente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //int codPaciente = Convert.ToInt32(txtCodigoPaciente.Text);
                    //PACIENTES paciente = contexto.PACIENTES.FirstOrDefault(p => p.PAC_CODIGO == codPaciente);
                    ASEGURADORAS_EMPRESAS aseguradora = (ASEGURADORAS_EMPRESAS)aseguradoraList.SelectedItem;
                    MEDICOS medico = contexto.MEDICOS.FirstOrDefault(m => m.MED_CODIGO == His.Entidades.Clases.Sesion.codMedico);
                    //int cod = His.Entidades.Clases.Sesion.codMedico;
                    var codigoHonTarifario = 1;
                    if (contexto.HONORARIOS_TARIFARIO.Count() > 0)
                    {
                        codigoHonTarifario = contexto.HONORARIOS_TARIFARIO.Max(h => h.HON_CODIGO);
                    }
                    HONORARIOS_TARIFARIO honTarifario = new HONORARIOS_TARIFARIO();
                    honTarifario.HON_CODIGO = codigoHonTarifario + 1;
                    honTarifario.HON_DIAGNOSTICO = txtDiagnostico.Text;
                    //honTarifario.HON_ESTADO = false;
                    honTarifario.HON_FECHA = DateTime.Now;

                    //ayudantes (0) - medico principal o 2do cirujano (1)
                    if ((tipomedico == "principal") || (tipomedico == "secundario"))
                        honTarifario.HON_TIPO = 0;
                    else
                        honTarifario.HON_TIPO = 1;

                    honTarifario.HON_TOTAL = Convert.ToDecimal(txtTotal.Text.ToString());
                    honTarifario.HON_FACTURA_MEDICO = txtFacturaMedico.Text;
                    honTarifario.HON_HISTORIA_CLINICA = txtPacienteHCL.Text; //txtFacturaMedico.Text;
                    honTarifario.HON_PACIENTE = txtPacienteNombre.Text;
                    honTarifario.ASEGURADORAS_EMPRESASReference.EntityKey = aseguradora.EntityKey;
                    //honTarifario.PACIENTESReference.EntityKey = paciente.EntityKey ;
                    honTarifario.MEDICOSReference.EntityKey = medico.EntityKey;
                    contexto.AddToHONORARIOS_TARIFARIO(honTarifario);
                    contexto.SaveChanges();
                    foreach (ListViewItem items in lvwHonorarios.Items)
                    {
                        int codigoDT = Convert.ToInt32(items.Text);
                        Int64 codigoHonTarifarioDetalle = 1;
                        if (contexto.HONORARIOS_TARIFARIO_DETALLE.Count() > 0)
                        {
                            codigoHonTarifarioDetalle = contexto.HONORARIOS_TARIFARIO_DETALLE.Max(h => h.HOD_CODIGO);
                        }
                        TARIFARIOS_DETALLE tarifarioDetalle = contexto.TARIFARIOS_DETALLE.FirstOrDefault(t => t.TAD_CODIGO == codigoDT);
                        HONORARIOS_TARIFARIO_DETALLE honTarifarioDetalle = new HONORARIOS_TARIFARIO_DETALLE();
                        honTarifarioDetalle.HOD_ANESTESIA = Convert.ToInt32(items.SubItems[5].Text);
                        honTarifarioDetalle.HOD_CANTIDAD = Convert.ToInt16(items.SubItems[3].Text);
                        honTarifarioDetalle.HOD_CODIGO = codigoHonTarifarioDetalle + 1;
                        honTarifarioDetalle.HOD_DESCRIPCION = items.SubItems[2].Text.Length > 120 ? items.SubItems[2].Text.Substring(0, 119) : items.SubItems[2].Text.ToString();
                        honTarifarioDetalle.HOD_SUBTOTAL = Convert.ToDouble(items.SubItems[8].Text);
                        honTarifarioDetalle.HOD_UVR = float.Parse(items.SubItems[4].Text);
                        honTarifarioDetalle.HOD_VALOR_ANESTESIA = float.Parse(items.SubItems[7].Text);
                        honTarifarioDetalle.HOD_VALOR_UVR = float.Parse(items.SubItems[6].Text);
                        honTarifarioDetalle.TARIFARIOS_DETALLEReference.EntityKey = tarifarioDetalle.EntityKey;
                        honTarifarioDetalle.HONORARIOS_TARIFARIOReference.EntityKey = honTarifario.EntityKey;
                        contexto.AddToHONORARIOS_TARIFARIO_DETALLE(honTarifarioDetalle);
                        contexto.SaveChanges();
                    }
                    codigoHonorario = honTarifario.HON_CODIGO;
                    guardado = true;
                    MessageBox.Show("La información se guardo exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnGuardar.Enabled = false;
                    btnImprimir.Enabled = true;
                }
                catch (Exception err)
                {
                    if (err.InnerException != null)
                        MessageBox.Show(err.InnerException.Message);
                    else
                        MessageBox.Show(err.Message);
                }
                panelPaciente.Enabled = false;
            }
            else
            {
                MessageBox.Show("Usuario No Médico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            frmReportes reporte = new frmReportes(codigoHonorario);
            reporte.Show();
            //Reportes.frmReportesTarifario reportes = new Reportes.frmReportesTarifario();
            //reportes.Show();  
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //conexion = new HisModelo.HIS3000BDEntities();
            panelPaciente.Enabled = true;
            cargarFiltros();
            limpiarHonorariosList();
            limpiarTextBox();
            btnGuardar.Enabled = true;
            txtTotal.Text = "0";
            defaulttrips();

        }
        private void defaulttrips()
        {
            tpprincipal.Checked = true;
            tpuvr.Checked = true;
            tpProcedimiento1.Checked = true;
            tpsecundario.Checked = false;
            tpayudante1.Checked = false;
            tpayudante2.Checked = false;
            tpanestesia.Checked = false;
            tpMismaVia.Checked = false;
            tpDobleVia.Checked = false;
        }

        private void limpiarTextBox()
        {
            // hace un chequeo por todos los textbox del formulario
            foreach (Control oControls in panelPaciente.Controls)
            {
                if (oControls is TextBox)
                {
                    oControls.Text = ""; // eliminar el texto
                }
            }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();

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

        private void tarifariosDetalleGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void tarifariosDetalleGrid_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                panelInf.Visible = true;
                DataGridViewCell celda = tarifariosDetalleGrid.CurrentCell;
                Rectangle rect = tarifariosDetalleGrid.GetCellDisplayRectangle(celda.ColumnIndex, celda.RowIndex, true);
                panelInf.Top = rect.Top + tarifariosDetalleGrid.ColumnHeadersHeight;
                rtbInf.Text = tarifariosDetalleGrid.CurrentRow.Cells[2].Value.ToString();
                panelInf.Focus();
            }
        }

        private void rtbInf_MouseLeave(object sender, EventArgs e)
        {
            panelInf.Visible = false;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtPacienteHCL_TextChanged(object sender, EventArgs e)
        {

        }

        private void lvwHonorarios_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void cargarAseguradoras(Int16 codigoTarifario)
        {
            try
            {
                HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
                var aseguradorasQuery = (from a in contexto.ASEGURADORAS_EMPRESAS
                                         join c in contexto.CONVENIOS_TARIFARIOS on a.ASE_CODIGO equals c.ASEGURADORAS_EMPRESAS.ASE_CODIGO
                                         join e in contexto.ESPECIALIDADES_TARIFARIOS on c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO equals e.EST_CODIGO
                                         where a.ASE_ESTADO == true && e.TARIFARIOS.TAR_CODIGO == codigoTarifario
                                         select a).Distinct().OrderBy(a => a.ASE_NOMBRE).ToList();

                // lleno la lista de aseguradoras
                this.aseguradoraList.DataSource = null;
                this.aseguradoraList.DataSource = aseguradorasQuery;
                this.aseguradoraList.DisplayMember = "ASE_NOMBRE";
                //string x = this.aseguradoraList.SelectedText;
                //if (codigoTarifario == 1)
                //{
                //    object x = this.aseguradoraList.Items[25];
                //    this.aseguradoraList.ValueMember = "ASE_NOMBRE";
                //    this.aseguradoraList.SelectedValue = "IESS";
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCodigoPaciente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtPacienteHCL.Focus();
            }
        }

        private void txtPacienteHCL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtPacienteNombre.Focus();
            }
        }

        private void txtPacienteNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtPacienteDireccion.Focus();
            }
        }

        private void txtPacienteDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtFacturaMedico.Focus();
            }
        }

        private void txtFacturaMedico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtDiagnostico.Focus();
            }
        }

        private void txtDiagnostico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                tarifarioList.Focus();
            }
        }

        private void tarifarioList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                aseguradoraList.Focus();
            }
        }

        private void aseguradoraList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter) || e.KeyChar == (char)(Keys.Tab))
            {
                e.Handled = true;
                txtBuscar.Focus();
            }
        }

        private void btnBuscarHonorarios_Click(object sender, EventArgs e)
        {

            try
            {

                frmBuscarHonorario bh = new frmBuscarHonorario();
                bh.cargarAyuda(medicoActual.MED_CODIGO);
                bh.lblmedico.Text = medicoActual.MED_APELLIDO_PATERNO.Trim() + " " +
                                    medicoActual.MED_APELLIDO_MATERNO.Trim() + " " +
                                    medicoActual.MED_NOMBRE1.Trim();
                bh.ShowDialog();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string teclas = "";
        private void tarifariosDetalleGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //solo se aceptan digitos y letras
                if (Char.IsLetterOrDigit(e.KeyChar))
                {
                    //capturo las teclas presionadas mientras el control timer este Activo (500 milisegundos)
                    if (timerCapturaTeclas.Enabled == true)
                    {
                        teclas += e.KeyChar.ToString();
                    }
                    //si el control esta inactivo, paso el control a activo y vuelvo a iniciar la captura
                    else
                    {
                        teclas = e.KeyChar.ToString();
                        timerCapturaTeclas.Enabled = true;
                    }
                    //celda seleccionada
                    DataGridViewCell celda = tarifariosDetalleGrid.CurrentCell;
                    int ini;
                    //valido si esta en la ultima celda
                    if (tarifariosDetalleGrid.CurrentRow.Index == (tarifariosDetalleGrid.Rows.Count - 1))
                    {
                        ini = 0;    //si esta en la ultima celda, inicia la busqueda desde la primera celda 
                    }
                    else
                    {
                        ini = tarifariosDetalleGrid.CurrentRow.Index + 1;    //inicia la busqueda desde la siguiente celda
                    }
                    //verifico si existen coincidencias desde la celda actual hasta la final
                    for (int i = ini; i < tarifariosDetalleGrid.Rows.Count; i++)
                    {
                        if (tarifariosDetalleGrid.Rows[i].Cells[celda.ColumnIndex].Value.ToString().ToUpper().StartsWith(teclas.ToUpper()))
                        {
                            tarifariosDetalleGrid.CurrentCell = tarifariosDetalleGrid.Rows[i].Cells[celda.ColumnIndex];
                            return;
                        }
                    }
                    //verifico si existen coincidencias desde la primera celda hasta la actual
                    for (int i = 0; i < ini; i++)
                    {
                        if (tarifariosDetalleGrid.Rows[i].Cells[celda.ColumnIndex].Value.ToString().ToUpper().StartsWith(teclas.ToUpper()))
                        {
                            tarifariosDetalleGrid.CurrentCell = tarifariosDetalleGrid.Rows[i].Cells[celda.ColumnIndex];
                            return;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timerCapturaTeclas_Tick(object sender, EventArgs e)
        {

        }

        private void txtDiagnostico_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frmBusquedaCIE10 busqueda = new frmBusquedaCIE10();
                busqueda.ShowDialog();
                txtDiagnostico.Text = busqueda.resultado;
            }
        }



        private void enviardatos(HIS3000BDEntities contexto)
        {
            bool uvr = false;
            //if (Convert.ToBoolean(this.optUvr.Checked.ToString()))
            if (Convert.ToBoolean(this.tpuvr.Checked.ToString()))
                uvr = true;

            DataGridViewRow fila = tarifariosDetalleGrid.CurrentRow;
            int codigoDetalle = Convert.ToInt32(fila.Cells[0].Value.ToString());
            TARIFARIOS_DETALLE tarifarioDetalle = contexto.TARIFARIOS_DETALLE.Include("ESPECIALIDADES_TARIFARIOS").FirstOrDefault(
                                                        t => t.TAD_CODIGO == codigoDetalle);


            Int64 codigo = tarifarioDetalle.ESPECIALIDADES_TARIFARIOS.EST_CODIGO;

            //if (rdb_medico.Checked == true)
            if ((tipomedico == "principal") || (tipomedico == "secundario"))
                addHonorarioDetalle(codigoDetalle, codigo,
                    fila.Cells[1].Value.ToString(),
                    fila.Cells[2].Value.ToString(),
                    Convert.ToInt32(txtCantidad.Text),
                    Convert.ToDouble(fila.Cells[3].Value),
                    Convert.ToDouble(fila.Cells[4].Value),
                    uvr);
            if ((tipomedico == "ayudante1") || (tipomedico == "ayudante2"))
            {
                //int cod = Convert.ToInt16(aseguradoraList.SelectedValue.ToString());
                ASEGURADORAS_EMPRESAS aseguradora = (ASEGURADORAS_EMPRESAS)aseguradoraList.SelectedItem;
                int cod = aseguradora.ASE_CODIGO;
                addHonorarioDetalle(codigoDetalle, codigo,
                    fila.Cells[1].Value.ToString(),
                    fila.Cells[2].Value.ToString(),
                    Convert.ToInt32(txtCantidad.Text),
                    Convert.ToDouble(fila.Cells[3].Value),
                    Convert.ToDouble(fila.Cells[4].Value),
                    uvr);
            }
        }

        private void enviardatosDV(HIS3000BDEntities contexto, int nivel)
        {
            bool uvr = false;
            if (Convert.ToBoolean(this.tpuvr.Checked.ToString()))
                uvr = true;

            DataGridViewRow fila = tarifariosDetalleGrid.CurrentRow;
            int codigoDetalle = Convert.ToInt32(fila.Cells[0].Value.ToString());
            TARIFARIOS_DETALLE tarifarioDetalle = contexto.TARIFARIOS_DETALLE.Include("ESPECIALIDADES_TARIFARIOS").FirstOrDefault(
                                                        t => t.TAD_CODIGO == codigoDetalle);


            Int64 codigo = tarifarioDetalle.ESPECIALIDADES_TARIFARIOS.EST_CODIGO;


            addHonorarioDetalleDV(codigoDetalle, codigo,
                fila.Cells[1].Value.ToString(),
                fila.Cells[2].Value.ToString(),
                Convert.ToInt32(txtCantidad.Text),
                Convert.ToDouble(fila.Cells[3].Value),
                Convert.ToDouble(fila.Cells[4].Value),
                uvr, nivel);

        }

        private void addHonorarioDetalleDV(int codigoDetalle, Int64 codigoEspecialidad,
            string referencia,
            string procedimiento,
            int cantidad,
            double uvr,
            double anes,
            bool conUvr,
            int nivel)
        {
            try
            {
                HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
                ASEGURADORAS_EMPRESAS aseguradora = (ASEGURADORAS_EMPRESAS)aseguradoraList.SelectedItem;
                //DataGridViewRow fila = tarifariosDetalleGrid.CurrentRow;
                CONVENIOS_TARIFARIOS convenios = contexto.CONVENIOS_TARIFARIOS.FirstOrDefault(
                                                c => c.ASEGURADORAS_EMPRESAS.ASE_CODIGO == aseguradora.ASE_CODIGO
                                                    && c.ESPECIALIDADES_TARIFARIOS.EST_CODIGO == codigoEspecialidad);

                //realizo los calculos para obtener el costo unitario
                double costoU = 0;
                decimal unidadesUvr = 0;
                decimal unidadesAnes = 0;
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
                    unidadesUvr = Convert.ToDecimal(uvr);
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
                    unidadesAnes = Convert.ToDecimal(anes);
                }
                //redondear valores

                unidadesUvr = Decimal.Round(unidadesUvr, 2);
                unidadesAnes = Decimal.Round(unidadesAnes, 2);
                double d = Convert.ToDouble(nivel) / 100;
                costoT = Decimal.Round(Convert.ToDecimal((cantidad * costoU) * d), 2);
                //Agrego una fila al ListView
                ListViewItem lista;

                lista = lvwHonorarios.Items.Add(codigoDetalle.ToString(), 0);
                lista.SubItems.Add(referencia);
                lista.SubItems.Add(procedimiento);
                lista.SubItems.Add(cantidad.ToString());
                lista.SubItems.Add(Convert.ToString(costoU*d));
                lista.SubItems.Add(unidadesUvr.ToString());
                lista.SubItems.Add(unidadesAnes.ToString());
                if (convenios != null)
                {
                    if ((tipomedico == "ayudante1"))
                    {
                        convenios.CON_VALOR_UVR = convenios.CON_VALOR_UVR;
                        convenios.CON_VALOR_ANESTESIA = convenios.CON_VALOR_ANESTESIA;
                        convenios.CON_VALOR_AYUDANTIA = (convenios.CON_VALOR_AYUDANTIA * costoT) / 100;
                        lista.SubItems.Add(convenios.CON_VALOR_UVR.ToString());
                        lista.SubItems.Add(convenios.CON_VALOR_ANESTESIA.ToString());
                        //lista.SubItems.Add(convenios.CON_VALOR_AYUDANTIA.ToString());
                    }
                    else
                    {
                        if ((tipomedico == "principal") || (tipomedico == "secundario"))
                        {
                            lista.SubItems.Add(convenios.CON_VALOR_UVR.ToString());
                            lista.SubItems.Add(convenios.CON_VALOR_ANESTESIA.ToString());
                            //lista.SubItems.Add("0");
                        }
                        else
                        {
                            convenios.CON_VALOR_UVR = convenios.CON_VALOR_UVR;
                            convenios.CON_VALOR_ANESTESIA = convenios.CON_VALOR_ANESTESIA;
                            convenios.CON_VALOR_AYUDANTIA2 = (convenios.CON_VALOR_AYUDANTIA2 * costoT) / 100;
                            lista.SubItems.Add(convenios.CON_VALOR_UVR.ToString());
                            lista.SubItems.Add(convenios.CON_VALOR_ANESTESIA.ToString());
                        }
                    }
                }
                else
                {
                    lista.SubItems.Add("0");
                    lista.SubItems.Add("0");
                    lista.SubItems.Add("0");
                }


                lista.SubItems.Add(costoT.ToString());
                Decimal total = 0;
                if ((tipomedico == "principal") || (tipomedico == "secundario"))
                {
                    lista.SubItems.Add("0");
                    total = (Convert.ToDecimal(txtTotal.Text) + costoT);
                }
                else
                {
                    if (tipomedico == "ayudante1")
                    {
                        lista.SubItems.Add(convenios.CON_VALOR_AYUDANTIA.ToString());
                        total = (Convert.ToDecimal(txtTotal.Text)) + Convert.ToDecimal(convenios.CON_VALOR_AYUDANTIA);
                    }
                    else
                    {
                        lista.SubItems.Add(convenios.CON_VALOR_AYUDANTIA2.ToString());
                        total = (Convert.ToDecimal(txtTotal.Text)) + Convert.ToDecimal(convenios.CON_VALOR_AYUDANTIA2);
                    }
                }
                txtTotal.Text = total.ToString();
                this.lvwHonorarios.Sort();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnParametros_Click(object sender, EventArgs e)
        {
            frmPorcentajes frmp = new frmPorcentajes();
            frmp.ShowDialog();

            porcentajeActual = NegPorcentajes.RecuperaPorcentaje().Where(cod => cod.POR_CODIGO == frmp.codigo).FirstOrDefault();
        }

        private void lvwHonorarios_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            /*
    ' ==================================================
    ' Usando la clase ListViewColumnSort con constructor
    ' ==================================================
    '
    ' Crear una instancia de la clase que realizará la comparación
    ' indicando la columna en la que se ha pulsado
    */
            ListViewColumnSort oCompare = new ListViewColumnSort(e.Column);
            /*
            ' La columna en la que se ha pulsado
            ' esto sólo es necesario si no se utiliza el contructor parametrizado
            'oCompare.ColumnIndex = e.Column
            */
            /*
            Asignar el orden de clasificación
            */
            if (lvwHonorarios.Sorting == SortOrder.Ascending)
                oCompare.Sorting = SortOrder.Descending;
            else
                oCompare.Sorting = SortOrder.Ascending;
            lvwHonorarios.Sorting = oCompare.Sorting;
            /*
            El tipo de datos de la columna en la que se ha pulsado
            */
            switch (e.Column)
            {
                case 0:
                    oCompare.CompararPor = ListViewColumnSort.TipoCompare.Cadena;
                    break;
                case 1:
                    oCompare.CompararPor = ListViewColumnSort.TipoCompare.Numero;
                    break;
                case 2:
                    oCompare.CompararPor = ListViewColumnSort.TipoCompare.Fecha;
                    break;
            }
            // Asignar la clase que implementa IComparer
            // y que se usará para realizar la comparación de cada elemento
            lvwHonorarios.ListViewItemSorter = oCompare;
            //
        }

        private void lvwHonorarios_ColumnClick_1(object sender, ColumnClickEventArgs e)
        {

            // Crear una instancia de la clase que realizará la comparación
            // indicando la columna en la que se ha pulsado

            ListViewColumnSort oCompare = new ListViewColumnSort(e.Column);
            /*
            ' La columna en la que se ha pulsado
            ' esto sólo es necesario si no se utiliza el contructor parametrizado
            'oCompare.ColumnIndex = e.Column
            */
            /*
            Asignar el orden de clasificación
            */
            if (lvwHonorarios.Sorting == SortOrder.Ascending)
                oCompare.Sorting = SortOrder.Descending;
            else
                oCompare.Sorting = SortOrder.Ascending;
            lvwHonorarios.Sorting = oCompare.Sorting;
            /*
            El tipo de datos de la columna en la que se ha pulsado
            */
            switch (e.Column)
            {
                case 0:
                    oCompare.CompararPor = ListViewColumnSort.TipoCompare.Cadena;
                    break;
                case 1:
                    oCompare.CompararPor = ListViewColumnSort.TipoCompare.Numero;
                    break;
                case 2:
                    oCompare.CompararPor = ListViewColumnSort.TipoCompare.Fecha;
                    break;
            }
            // Asignar la clase que implementa IComparer
            // y que se usará para realizar la comparación de cada elemento
            lvwHonorarios.ListViewItemSorter = oCompare;
            //
        }

        private void toolStripMedicos_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void optAnestesia_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void uVRToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tpprincipal_Click(object sender, EventArgs e)
        {

        }

        private void tpsecundario_Click(object sender, EventArgs e)
        {

        }

        private void tpayudante1_Click(object sender, EventArgs e)
        {

        }

        private void tpayudante2_Click(object sender, EventArgs e)
        {

        }

        private void tpanestesia_Click(object sender, EventArgs e)
        {

        }

        private void tpProcedimiento1_Click(object sender, EventArgs e)
        {

        }

        private void tpMismaVia_Click(object sender, EventArgs e)
        {

        }

        private void tpDobleVia_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void limpiarTools()
        {
            tpprincipal.Enabled = true;
            tpsecundario.Enabled = true;
            tpayudante1.Enabled = true;
            tpayudante2.Enabled = true;
            tpuvr.Enabled = true;
            tpanestesia.Enabled = true;
            tpProcedimiento1.Enabled = true;
            tpMismaVia.Enabled = true;
            tpDobleVia.Enabled = true;
            aseguradoraList.Enabled = true;
        }

        private void ayudaPacientes_Click(object sender, EventArgs e)
        {
            frm_AyudaPacientes frm = new frm_AyudaPacientes();
            frm.campoPadre = txtCodigoPaciente;
            frm.ShowDialog();
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            frmBusquedaCIE10 busqueda = new frmBusquedaCIE10();
            busqueda.ShowDialog();
            txtDiagnostico.Text = busqueda.resultado;
        }

        private void tsmiSetentaCincoPor_Click(object sender, EventArgs e)
        {

        }

        private void tsmiCincuentaPor_Click(object sender, EventArgs e)
        {

        }

        private void tsmiVeinteCincoPor_Click(object sender, EventArgs e)
        {

        }

        private void tsmiCienPor_Click(object sender, EventArgs e)
        {

        }

        private void tsTxtPorcentajeCobrar_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void tsmiOtro_Click(object sender, EventArgs e)
        {

        }

        private void tsTxtPorcentajeCobrar_TextChanged(object sender, EventArgs e)
        {

        }

        private void tsTxtPorcentajeCobrar_Click(object sender, EventArgs e)
        {

        }

        private void tarifarioList_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            TARIFARIOS tarifario = (TARIFARIOS)this.tarifarioList.SelectedItem;
            cargarEspecialidadesTarifario(tarifario);
            cargarAseguradoras(tarifario.TAR_CODIGO);
            if (this.Visible == true)
            {
                cargarDetalleTarifario();
            }
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(this.optCodigo.Checked.ToString()))
            {
                cargarDetalleTarifario(this.txtBuscar.Text, "referencia");
            }
            else if (Convert.ToBoolean(this.optDescripcion.Checked.ToString()))
            {
                cargarDetalleTarifario(this.txtBuscar.Text, "descripcion");
            }
        }

        private void btnAniadir_Click_1(object sender, EventArgs e)
        {
            if (tipomedico == String.Empty)
                MessageBox.Show("Escoga el tipo de Medico", "Alerta");
            else
                addHonorario();
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tarifarioList.Text != "")
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    if (Convert.ToBoolean(this.optCodigo.Checked.ToString()))
                    {
                        cargarDetalleTarifario(this.txtBuscar.Text, "referencia");
                    }
                    else if (Convert.ToBoolean(this.optDescripcion.Checked.ToString()))
                    {
                        cargarDetalleTarifario(this.txtBuscar.Text, "descripcion");
                    }
                }
            }
        }

        private void tpprincipal_Click_1(object sender, EventArgs e)
        {
            tpsecundario.Checked = false;
            tpayudante1.Checked = false;
            tpayudante2.Checked = false;
            tpprincipal.Checked = true;
            tipomedico = "principal";
            txtValorMedico.Visible = false;
        }

        private void tpsecundario_Click_1(object sender, EventArgs e)
        {
            tpprincipal.Checked = false;
            tpayudante1.Checked = false;
            tpayudante2.Checked = false;
            tpsecundario.Checked = true;
            tipomedico = "secundario";
            txtValorMedico.Visible = true;
        }

        private void tpayudante1_Click_1(object sender, EventArgs e)
        {
            tpprincipal.Checked = false;
            tpsecundario.Checked = false;
            tpayudante2.Checked = false;
            tpayudante1.Checked = true;
            tipomedico = "ayudante1";
            txtValorMedico.Visible = true;
        }

        private void tpayudante2_Click_1(object sender, EventArgs e)
        {
            tpprincipal.Checked = false;
            tpsecundario.Checked = false;
            tpayudante1.Checked = false;
            tpayudante2.Checked = true;
            tipomedico = "ayudante2";
            txtValorMedico.Visible = true;
        }

        private void tpuvr_Click(object sender, EventArgs e)
        {
            tpanestesia.Checked = false;
            tpuvr.Checked = true;
        }

        private void tpanestesia_Click_1(object sender, EventArgs e)
        {

            tpuvr.Checked = false;
            tpanestesia.Checked = true;
        }

        private void tpProcedimiento1_Click_1(object sender, EventArgs e)
        {

            tpProcedimiento1.Checked = true;
            tpMismaVia.Checked = false;
            tpDobleVia.Checked = false;
            procedimiento = "procedimiento1";
        }

        private void tpMismaVia_Click_1(object sender, EventArgs e)
        {
            tpDobleVia.Checked = false;
            tpMismaVia.Checked = true;
            tpProcedimiento1.Checked = false;
            procedimiento = "mismaVia";
        }

        private void tpDobleVia_Click_1(object sender, EventArgs e)
        {
            tpProcedimiento1.Checked = false;
            tpMismaVia.Checked = false;
            tpDobleVia.Checked = true;
            procedimiento = "dobleVia";
        }

        private void tsmiCienPor_Click_1(object sender, EventArgs e)
        {
            tsmiSetentaCincoPor.Checked = false;
            tsmiCienPor.Checked = true;
            tsmiCincuentaPor.Checked = false;
            tsmiVeinteCincoPor.Checked = false;
            tsmiOtro.Checked = false;
            porcentajeFacturar = Convert.ToInt16(tsmiCienPor.Tag);
            tsTxtPorcentajeCobrar.Visible = false;
        }

        private void tsmiSetentaCincoPor_Click_1(object sender, EventArgs e)
        {
            tsmiSetentaCincoPor.Checked = true;
            tsmiCienPor.Checked = false;
            tsmiCincuentaPor.Checked = false;
            tsmiVeinteCincoPor.Checked = false;
            tsmiOtro.Checked = false;
            porcentajeFacturar = Convert.ToInt16(tsmiSetentaCincoPor.Tag);
            tsTxtPorcentajeCobrar.Visible = false; 
        }

        private void tsmiCincuentaPor_Click_1(object sender, EventArgs e)
        {
            tsmiSetentaCincoPor.Checked = false;
            tsmiCienPor.Checked = false;
            tsmiCincuentaPor.Checked = true;
            tsmiVeinteCincoPor.Checked = false;
            tsmiOtro.Checked = false;
            porcentajeFacturar = Convert.ToInt16(tsmiCincuentaPor.Tag);
            tsTxtPorcentajeCobrar.Visible = false; 
        }

        private void tsmiVeinteCincoPor_Click_1(object sender, EventArgs e)
        {
            tsmiSetentaCincoPor.Checked = false;
            tsmiCienPor.Checked = false;
            tsmiCincuentaPor.Checked = false;
            tsmiVeinteCincoPor.Checked = true;
            tsmiOtro.Checked = false;
            porcentajeFacturar = Convert.ToInt16(tsmiVeinteCincoPor.Tag);
            tsTxtPorcentajeCobrar.Visible = false; 
        }

        private void tsmiOtro_Click_1(object sender, EventArgs e)
        {
            tsmiSetentaCincoPor.Checked = false;
            tsmiCienPor.Checked = false;
            tsmiCincuentaPor.Checked = false;
            tsmiVeinteCincoPor.Checked = false;
            tsmiOtro.Checked = true;
            porcentajeFacturar = Convert.ToInt16(tsTxtPorcentajeCobrar.Text);
            tsTxtPorcentajeCobrar.Visible = true; 
        }

        private void tarifariosDetalleGrid_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                panelInf.Visible = true;
                DataGridViewCell celda = tarifariosDetalleGrid.CurrentCell;
                Rectangle rect = tarifariosDetalleGrid.GetCellDisplayRectangle(celda.ColumnIndex, celda.RowIndex, true);
                panelInf.Top = rect.Top + tarifariosDetalleGrid.ColumnHeadersHeight;
                rtbInf.Text = tarifariosDetalleGrid.CurrentRow.Cells[2].Value.ToString();
                panelInf.Focus();
            }
        }

        private void tarifariosDetalleGrid_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                //solo se aceptan digitos y letras
                if (Char.IsLetterOrDigit(e.KeyChar))
                {
                    //capturo las teclas presionadas mientras el control timer este Activo (500 milisegundos)
                    if (timerCapturaTeclas.Enabled == true)
                    {
                        teclas += e.KeyChar.ToString();
                    }
                    //si el control esta inactivo, paso el control a activo y vuelvo a iniciar la captura
                    else
                    {
                        teclas = e.KeyChar.ToString();
                        timerCapturaTeclas.Enabled = true;
                    }
                    //celda seleccionada
                    DataGridViewCell celda = tarifariosDetalleGrid.CurrentCell;
                    int ini;
                    //valido si esta en la ultima celda
                    if (tarifariosDetalleGrid.CurrentRow.Index == (tarifariosDetalleGrid.Rows.Count - 1))
                    {
                        ini = 0;    //si esta en la ultima celda, inicia la busqueda desde la primera celda 
                    }
                    else
                    {
                        ini = tarifariosDetalleGrid.CurrentRow.Index + 1;    //inicia la busqueda desde la siguiente celda
                    }
                    //verifico si existen coincidencias desde la celda actual hasta la final
                    for (int i = ini; i < tarifariosDetalleGrid.Rows.Count; i++)
                    {
                        if (tarifariosDetalleGrid.Rows[i].Cells[celda.ColumnIndex].Value.ToString().ToUpper().StartsWith(teclas.ToUpper()))
                        {
                            tarifariosDetalleGrid.CurrentCell = tarifariosDetalleGrid.Rows[i].Cells[celda.ColumnIndex];
                            return;
                        }
                    }
                    //verifico si existen coincidencias desde la primera celda hasta la actual
                    for (int i = 0; i < ini; i++)
                    {
                        if (tarifariosDetalleGrid.Rows[i].Cells[celda.ColumnIndex].Value.ToString().ToUpper().StartsWith(teclas.ToUpper()))
                        {
                            tarifariosDetalleGrid.CurrentCell = tarifariosDetalleGrid.Rows[i].Cells[celda.ColumnIndex];
                            return;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rtbInf_MouseLeave_1(object sender, EventArgs e)
        {
            panelInf.Visible = false; 
        }

        private void lvwHonorarios_ColumnClick_2(object sender, ColumnClickEventArgs e)
        {

            // Crear una instancia de la clase que realizará la comparación
            // indicando la columna en la que se ha pulsado

            ListViewColumnSort oCompare = new ListViewColumnSort(e.Column);
            /*
            ' La columna en la que se ha pulsado
            ' esto sólo es necesario si no se utiliza el contructor parametrizado
            'oCompare.ColumnIndex = e.Column
            */
            /*
            Asignar el orden de clasificación
            */
            if (lvwHonorarios.Sorting == SortOrder.Ascending)
                oCompare.Sorting = SortOrder.Descending;
            else
                oCompare.Sorting = SortOrder.Ascending;
            lvwHonorarios.Sorting = oCompare.Sorting;
            /*
            El tipo de datos de la columna en la que se ha pulsado
            */
            switch (e.Column)
            {
                case 0:
                    oCompare.CompararPor = ListViewColumnSort.TipoCompare.Cadena;
                    break;
                case 1:
                    oCompare.CompararPor = ListViewColumnSort.TipoCompare.Numero;
                    break;
                case 2:
                    oCompare.CompararPor = ListViewColumnSort.TipoCompare.Fecha;
                    break;
            }
            // Asignar la clase que implementa IComparer
            // y que se usará para realizar la comparación de cada elemento
            lvwHonorarios.ListViewItemSorter = oCompare;
            //

        }

        private void lvwHonorarios_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    foreach (ListViewItem items in lvwHonorarios.SelectedItems)
                    {
                        lvwHonorarios.Items.Remove(items);
                    }
                    Decimal total = 0;
                    foreach (ListViewItem fila in lvwHonorarios.Items)
                    {
                        total += Convert.ToDecimal(fila.SubItems[8].Text);
                    }
                    txtTotal.Text = total.ToString();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void tvEspecialidades_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            if (this.tvEspecialidades.SelectedNode.Text != "Todos")
            {
                //if (tvEspecialidades.SelectedNode.Nodes.Count == 0)
                //{
                cargarDetalleTarifario(Convert.ToInt32(tvEspecialidades.SelectedNode.Tag.ToString()));
                //}
            }
            else
            {
                cargarDetalleTarifario();
            }
        }

        private void ConsultaHonorarios_Load(object sender, EventArgs e)
        {

            try
            {
                His.Parametros.ArchivoIni archivo = new ArchivoIni(Application.StartupPath + @"\his3000.ini");
                string codParametro = archivo.IniReadValue("parametros", "cod");
                var usuarioporcentaje = NegPorcentajes.RecuperaPorcentaje().Where(cod => cod.POR_CODIGO.ToString() == codParametro).FirstOrDefault();

                if (porcentajeActual != null)
                    porcentajeActual = usuarioporcentaje;
                else
                    porcentajeActual = NegPorcentajes.RecuperaPorcentaje().Where(cod => cod.POR_CODIGO == 1).FirstOrDefault();

                //cargo valores por defecto
                this.WindowState = FormWindowState.Maximized;
                btnNuevo.Image = Archivo.imgBtnAdd2;
                btnGuardar.Image = Archivo.imgBtnGoneSave48;
                btnImprimir.Image = Archivo.imgBtnGonePrint48;
                btnExportarExcel.Image = Archivo.imgOfficeExcel;
                btnBuscarHonorarios.Image = Archivo.imgBtnSearch;
                btnSalir.Image = Archivo.imgBtnGoneExit48;
                btnParametros.Image = Archivo.imgBtnGoneEmblem48;
                splitContainer1.SplitterDistance = 350;

                //cambio el fondo del menu dependiendo de la resolucion de pantalla
                int deskHeight = Screen.PrimaryScreen.Bounds.Height;
                int deskWidth = Screen.PrimaryScreen.Bounds.Width;

                if (deskWidth >= 1024 && deskWidth < 1280)
                    panelFiltros.Appearance.ImageBackground = Archivo.fondoA1x1024x73;
                else if (deskWidth >= 1280 && deskWidth < 1360)
                    panelFiltros.Appearance.ImageBackground = Archivo.fondoA1x1280x92;
                else if (deskWidth >= 1360 && deskWidth < 1600)
                    panelFiltros.Appearance.ImageBackground = Archivo.fondoA1x1360x97;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
