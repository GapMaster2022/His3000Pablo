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
using Recursos;
using His.Parametros;
using Infragistics.Win.UltraWinGrid;

namespace His.Farmacia
{
    public partial class frm_ExploradorPedidos : Form
    {
        Infragistics.Win.UltraWinTree.UltraTreeNode raiz;
        PEDIDOS pedido = null;
        public static string tagPedidos = "pedidos";
        public static string tagPacientes = "pacientes";
        public static string tagDepartamentos = "departamentos";
        public static string tagAtenciones = "atenciones";

        public frm_ExploradorPedidos()
        {
            InitializeComponent();
            cargarRecursos();
            inicializarForma();
        }

        public void cargarRecursos()
        {
            //this.tssMedicos.Image  = Recursos.Archivo.btnOrganigrama;  
            //imagenes del menu principal
            toolStripSplitButtonNuevo.Image = Archivo.imgBtnAdd2;
            toolStripButtonCancelar.Image = Archivo.imgBtnStop;
            toolStripButtonGuardar.Image = Archivo.imgBtnFloppy;
            toolStripButtonActualizar.Image = Archivo.imgBtnRestart;
            toolStripButtonExportar.Image = Archivo.imgOfficeExcel;
            toolStripButtonImprimir.Image = Archivo.imgBtnImprimir32;
            toolStripButtonSalir.Image = Archivo.imgBtnSalir32;
            tssMedicos.Image = Archivo.imgSptOrganizar;
        }

        public void inicializarForma()
        {

            txtNumPedido.ReadOnly = true;
            txtNumAtencion.ReadOnly = true;
            txtPaciente.ReadOnly = true;
            txtHCL.ReadOnly = true;
            txtUsuario.ReadOnly = true;
            dtpFechaPedido.ReadOnly = true;

            cboEstadoPedidos.Items.Add(new KeyValuePair<int, string>(FarmaciaPAR.PedidoPendiente, "Pendientes"));
            cboEstadoPedidos.Items.Add(new KeyValuePair<int, string>(FarmaciaPAR.PedidoEntregado, "Entregados"));
            cboEstadoPedidos.Items.Add(new KeyValuePair<int, string>(FarmaciaPAR.PedidoAnulado, "Anulados"));
            cboEstadoPedidos.DisplayMember = "Value";
            cboEstadoPedidos.ValueMember = "Key";
            cboEstadoPedidos.SelectedIndex = 0;

            dtpDesde.Value = DateTime.Now;
            dtpHasta.Value = DateTime.Now;

            ultraTree.Nodes.Clear();
            ultraTree.Override.CellClickAction = Infragistics.Win.UltraWinTree.CellClickAction.SelectNodeOnly;

            raiz = new Infragistics.Win.UltraWinTree.UltraTreeNode();
            raiz.Tag = "TODOS";
            raiz.Key = "0";
            raiz.Text = "TODOS";
            raiz.Override.NodeAppearance.Image = Archivo.mundO32X32;
            raiz.Override.NodeAppearance.ForeColor = Color.DarkBlue;
            ultraTree.Nodes.Add(raiz);
            
        }

        public void CargarArbol()
        {
            txtNumPedido.Text = string.Empty;
            raiz.Nodes.Clear();
            KeyValuePair<int, string> sitem = (KeyValuePair<int, string>)cboEstadoPedidos.SelectedItem;
            int estadoPedidos = sitem.Key;

            string desde, hasta;
            desde = null;
            hasta = null;

            if (optPorFecha.Checked == true)
            {
                desde = new DateTime(dtpDesde.Value.Year, dtpDesde.Value.Month, dtpDesde.Value.Day, 0, 0, 0).ToString();
                hasta = new DateTime(dtpHasta.Value.Year, dtpHasta.Value.Month, dtpHasta.Value.Day, 23, 59, 59).ToString();
            }

            if (tipoHonorarioToolStripMenuItem.Checked == true)
            {
                List<PEDIDOS> hijos = NegPedidos.recuperarListaPedidos(estadoPedidos,txtBusqPedido.Text.Trim(),desde,hasta);
                foreach (var item in hijos)
                {
                    Infragistics.Win.UltraWinTree.UltraTreeNode nodo = new Infragistics.Win.UltraWinTree.UltraTreeNode();
                    nodo.Key = item.PED_CODIGO.ToString();
                    nodo.Text = "Pedido Nº " + item.PED_CODIGO.ToString();
                    nodo.Tag = tagPedidos;
                    nodo.Override.NodeAppearance.ForeColor = Color.FromArgb(30, 30, 30);
                    nodo.Override.NodeAppearance.Image = Archivo.ubicacion_16x16;
                    raiz.Nodes.Add(nodo);
                }
            }
            if (pacientesToolStripMenuItem.Checked == true)
            {
                List<PACIENTES> hijos = NegPacientes.recuperarListaPacientesPedidos(estadoPedidos,txtBusqPedido.Text.Trim(),desde,hasta);
                foreach (var item in hijos)
                {
                    Infragistics.Win.UltraWinTree.UltraTreeNode nodo = new Infragistics.Win.UltraWinTree.UltraTreeNode();
                    nodo.Key = item.PAC_CODIGO.ToString();
                    nodo.Text = item.PAC_APELLIDO_PATERNO + " " + item.PAC_APELLIDO_MATERNO + " " + item.PAC_NOMBRE1 + " " + item.PAC_NOMBRE2;
                    nodo.Tag = tagPacientes;
                    nodo.Override.NodeAppearance.ForeColor = Color.FromArgb(30, 30, 30);
                    nodo.Override.NodeAppearance.Image = Archivo.ubicacion_16x16;
                    raiz.Nodes.Add(nodo);
                }
            }
            if (usuariosToolStripMenuItem.Checked == true)
            {
                //List<PACIENTES> hijos = NegPacientes.recuperarListaPacientesPedidos(FarmaciaPAR.PedidoPendiente);
                //foreach (var item in hijos)
                //{
                //    Infragistics.Win.UltraWinTree.UltraTreeNode nodo = new Infragistics.Win.UltraWinTree.UltraTreeNode();
                //    nodo.Key = item.PAC_CODIGO.ToString();
                //    nodo.Text = item.PAC_APELLIDO_PATERNO + " " + item.PAC_APELLIDO_MATERNO + " " + item.PAC_NOMBRE1 + " " + item.PAC_NOMBRE2;
                //    nodo.Tag = tagPacientes;
                //    nodo.Override.NodeAppearance.ForeColor = Color.FromArgb(30, 30, 30);
                //    nodo.Override.NodeAppearance.Image = Archivo.ubicacion_16x16;
                //    raiz.Nodes.Add(nodo);
                //}
            }
            raiz.ExpandAll();

        }

        public void CargarArbol(Infragistics.Win.UltraWinTree.UltraTreeNode nodoRaiz, int codigo)
        {
            //txtNumPedido.Text = string.Empty;
            nodoRaiz.Nodes.Clear();
            KeyValuePair<int, string> sitem = (KeyValuePair<int, string>)cboEstadoPedidos.SelectedItem;
            int estadoPedidos = sitem.Key;

            string desde, hasta;
            desde = null;
            hasta = null;

            if (optPorFecha.Checked == true)
            {
                desde = new DateTime(dtpDesde.Value.Year, dtpDesde.Value.Month, dtpDesde.Value.Day, 0, 0, 0).ToString();
                hasta = new DateTime(dtpHasta.Value.Year, dtpHasta.Value.Month, dtpHasta.Value.Day, 23, 59, 59).ToString();
            }

            if (nodoRaiz.Tag == tagPacientes)
            {
                List<ATENCIONES> atenciones = NegAtenciones.listaAtencionesPacienteConPedidos(codigo,estadoPedidos,txtBusqPedido.Text.Trim(),desde,hasta);

                foreach (var item in atenciones)
                {
                    Infragistics.Win.UltraWinTree.UltraTreeNode nodo = new Infragistics.Win.UltraWinTree.UltraTreeNode();
                    nodo.Key = item.ATE_CODIGO.ToString();
                    nodo.Text = "Atencion Nº " + item.ATE_NUMERO_ATENCION;
                    nodo.Tag = tagAtenciones;
                    nodo.Override.NodeAppearance.ForeColor = Color.FromArgb(30, 30, 30);
                    nodo.Override.NodeAppearance.Image = Archivo.ubicacion_16x16;
                    nodoRaiz.Nodes.Add(nodo);
                }
            }
            if (nodoRaiz.Tag == tagAtenciones)
            {
                List<PEDIDOS> pedidos = NegPedidos.recuperarListaPedidosAtencion(codigo,estadoPedidos,txtBusqPedido.Text.Trim(),desde,hasta);

                foreach (var item in pedidos)
                {
                    Infragistics.Win.UltraWinTree.UltraTreeNode nodo = new Infragistics.Win.UltraWinTree.UltraTreeNode();
                    nodo.Key = item.PED_CODIGO.ToString();
                    nodo.Text = "Pedido Nº " + item.PED_CODIGO;
                    nodo.Tag = tagPedidos;
                    nodo.Override.NodeAppearance.ForeColor = Color.FromArgb(30, 30, 30);
                    nodo.Override.NodeAppearance.Image = Archivo.ubicacion_16x16;
                    nodoRaiz.Nodes.Add(nodo);
                }
            }
        }

        private void toolStripButtonActualizar_Click(object sender, EventArgs e)
        {
            CargarArbol();
        }

        private void ultraTree_AfterActivate(object sender, Infragistics.Win.UltraWinTree.NodeEventArgs e)
        {
            //if (e.TreeNode.HasNodes == false)
            //    CargarHijos(e.TreeNode);
            //if(e.TreeNode.Tag == )
            txtNumPedido.Text = string.Empty;
            if (e.TreeNode.Tag.ToString() != "TODOS")
            {
                if (e.TreeNode.Tag.ToString() == tagPedidos)
                {
                    txtNumPedido.Text = e.TreeNode.Key.ToString();
                }
                else
                {

                    CargarArbol(e.TreeNode, Convert.ToInt32(e.TreeNode.Key));
                }
            }

        }

        private void txtNumPedido_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtNumPedido.Text.Trim() != string.Empty)
                {
                    int codPedido = Convert.ToInt32(txtNumPedido.Text.Trim());
                    pedido = NegPedidos.recuperarPedidoID(codPedido);
                    ATENCIONES at = NegAtenciones.AtencionID((Int32)pedido.ATE_CODIGO);
                    PACIENTES paciente = NegPacientes.recuperarPacientePorAtencion(at.ATE_CODIGO);
                    txtNumAtencion.Text = at.ATE_NUMERO_ATENCION;
                    txtHCL.Text = paciente.PAC_HISTORIA_CLINICA;
                    txtPaciente.Text = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO + " " + paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                    USUARIOS usuario = NegUsuarios.RecuperarUsuarioID((Int32)pedido.ID_USUARIO);
                    txtUsuario.Text = usuario.NOMBRES + " " + usuario.APELLIDOS;
                    if (pedido.PED_FECHA != null)
                        dtpFechaPedido.Text = pedido.PED_FECHA.ToString();
                    else
                        dtpFechaPedido.Text = string.Empty;

                    ultraGridDetalle.DataSource = NegPedidos.RecuperarDetallePedido(pedido.PED_CODIGO).Select(p => new
                    {
                        PDD_CODIGO = p.PDD_CODIGO,
                        CODIGO = p.PRODUCTOReference.EntityKey.EntityKeyValues[0].Value,
                        DESCRIPCION = p.PRO_DESCRIPCION,
                        CANTIDAD = p.PDD_CANTIDAD,
                        VALOR = p.PDD_VALOR,
                        IVA = p.PDD_IVA,
                        TOTAL = p.PDD_TOTAL,
                        ESTADO = p.PDD_ESTADO
                    }).ToList();
                }
                else
                {
                    pedido = null;
                    //txtNumPedido.Text = string.Empty;
                    txtNumAtencion.Text = string.Empty;
                    txtPaciente.Text = string.Empty;
                    txtHCL.Text = string.Empty;
                    txtUsuario.Text = string.Empty;
                    dtpFechaPedido.Text = string.Empty;
                    ultraGridDetalle.DataSource = null;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Error");  
            }
            
        }

        private void ultraGridDetalle_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            try
            {

                Infragistics.Win.UltraWinGrid.UltraGridBand bandUno = ultraGridDetalle.DisplayLayout.Bands[0];

                ultraGridDetalle.DisplayLayout.GroupByBox.Prompt = "Arrastrar la columna que desea agrupar";
                ultraGridDetalle.DisplayLayout.Override.RowSelectorHeaderStyle = Infragistics.Win.UltraWinGrid.RowSelectorHeaderStyle.ColumnChooserButton;
                ultraGridDetalle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                bandUno.Override.HeaderAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                ultraGridDetalle.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;

                ultraGridDetalle.DisplayLayout.Override.SpecialRowSeparator = Infragistics.Win.UltraWinGrid.SpecialRowSeparator.FilterRow;

                if (!ultraGridDetalle.DisplayLayout.Bands[0].Columns.Exists("ESTADO2"))
                {
                    //UltraGridColumn columna = new UltraGridColumn("");

                    bandUno.Columns.Add("ESTADO2", "");
                    bandUno.Columns["ESTADO2"].DataType = typeof(bool);
                    bandUno.Columns["ESTADO2"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
                    bandUno.Columns["ESTADO2"].ButtonDisplayStyle = ButtonDisplayStyle.Always;
                    //bandUno.Columns["ESTADO"].IsReadOnly = false;
                }

                //if (!ultraGridDetalle.DisplayLayout.Bands[0].Columns.Exists("edit"))
                //{
                //    bandUno.Columns.Add("edit", "");
                //    bandUno.Columns["edit"].DataType = typeof(string);
                //    bandUno.Columns["edit"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
                //    bandUno.Columns["edit"].ButtonDisplayStyle = ButtonDisplayStyle.Always;
                //}

                //bandUno.Columns["delete"].Width = 20;
                //bandUno.Columns["edit"].Width = 20;

                //bandUno.Columns["delete"].MaxWidth = 20;
                //bandUno.Columns["delete"].MinWidth = 20;


                //bandUno.Columns["edit"].MaxWidth = 20;
                //bandUno.Columns["edit"].MinWidth = 20;


                //bandUno.Columns["delete"].Header.VisiblePosition = 0;
                //bandUno.Columns["edit"].Header.VisiblePosition = 1;
                ////bandUno.Columns["COD"].Header.VisiblePosition = 2;
                //bandUno.Columns["MEDICO"].Header.VisiblePosition = 2;
                //bandUno.Columns["FACTURA"].Header.VisiblePosition = 3;
                //bandUno.Columns["FECHA"].Header.VisiblePosition = 4;
                //bandUno.Columns["F_PAGO"].Header.VisiblePosition = 5;
                //bandUno.Columns["VALOR_NETO"].Header.VisiblePosition = 6;
                //bandUno.Columns["RETENCION"].Header.VisiblePosition = 7;
                //bandUno.Columns["COMISION_CLINICA"].Header.VisiblePosition = 8;
                //bandUno.Columns["APORTE_LLAMADA"].Header.VisiblePosition = 9;
                //bandUno.Columns["VALOR_TOTAL"].Header.VisiblePosition = 10;
                //bandUno.Columns["PEDIDOS"].Hidden = true;
                bandUno.Columns["PDD_CODIGO"].Hidden = true;
                bandUno.Columns["ESTADO"].Hidden = true;
                //DataGridViewButtonCell

                 for (int i = 0; i < ultraGridDetalle.Rows.Count; i++)
                 {
                     UltraGridCell cell = ultraGridDetalle.Rows[i].Cells["ESTADO"];
                     ultraGridDetalle.Rows[i].Cells["ESTADO2"].Value = cell.Value;
                 }

            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tssMedicos_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            foreach (ToolStripMenuItem item in tssMedicos.DropDownItems)
            {
                item.Checked = false;
            }
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (pedido != null)
                {
                    bool band = true;

                    for (int i = 0; i < ultraGridDetalle.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(ultraGridDetalle.Rows[i].Cells["ESTADO2"].Value) == true)
                        {
                            int codDetalle = Convert.ToInt32(ultraGridDetalle.Rows[i].Cells["PDD_CODIGO"].Value);
                            NegPedidos.actulizarEstadoDetallePedido(codDetalle, true);
                        }
                        if (Convert.ToBoolean(ultraGridDetalle.Rows[i].Cells["ESTADO"].Value) == false 
                            && Convert.ToBoolean(ultraGridDetalle.Rows[i].Cells["ESTADO2"].Value) == false)
                        {
                            band = false;
                        }
                    }
                    if (band == true)
                    {
                        NegPedidos.actualizarEstadoPedido(pedido, FarmaciaPAR.PedidoEntregado);
                        MessageBox.Show("Pedido Entregado");
                    }
                    else
                    {
                        MessageBox.Show("Pedido Entregado (Incompleto)");
                    }
                    CargarArbol();
                    
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                if (er.InnerException != null)
                    MessageBox.Show(er.InnerException.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void frm_ExploradorPedidos_Load(object sender, EventArgs e)
        {

        }
    }
}
