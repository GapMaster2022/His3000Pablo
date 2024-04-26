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
using His.Parametros;
using Recursos;

namespace His.Farmacia
{
    public partial class frm_RealizarPedidos : Form
    {
        PEDIDOS pedido = null;
        ATENCIONES atencion = null;
        PACIENTES paciente = null;

        public static bool inicio = true;
        public frm_RealizarPedidos(ATENCIONES natencion)
        {
            atencion = natencion;
            int codigoPedido = NegPedidos.ultimoCodigoPedidos()+1;
            InitializeComponent();
            txtNumPedido.Text = codigoPedido.ToString();
            txtNumAtencion.Text = Convert.ToString(atencion.ATE_CODIGO);
            cargarRecursos();
        }
        public frm_RealizarPedidos()
        {
            InitializeComponent();
            cargarRecursos();
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

            DateTime max = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day, 23, 59, 59);

            toolStripButtonActualizar.Visible = false;
            toolStripButtonExportar.Visible = false;

            txtNumPedido.ReadOnly = true;
            txtHCL.ReadOnly = true;
            txtPaciente.ReadOnly = true;
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dataGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (gridPedidos.CurrentCell.ColumnIndex == 2)
                {
                    frm_AyudaProductos form = new frm_AyudaProductos("PRODUCTOS");
                    form.campoPadre = gridPedidos.CurrentRow;
                    form.ShowDialog();
                    if (form.campoPadre.Cells[2].Value != null)
                        gridPedidos.CurrentCell = form.campoPadre.Cells[6];
                }
                //MessageBox.Show(gridPedidos.CurrentCell.ColumnIndex.ToString());
            }
        }

        private void gridPedidos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 6)
                {
                    if (gridPedidos.Rows[e.RowIndex].Cells[6].Value != null)
                    {
                        if (!Microsoft.VisualBasic.Information.IsDBNull(gridPedidos.Rows[e.RowIndex].Cells[6].Value))
                        {
                            decimal valor = 0;
                            decimal iva = 0;
                            int cantidad = 0;
                            decimal total = 0;


                            valor = Convert.ToDecimal(gridPedidos.Rows[e.RowIndex].Cells[4].Value);
                            iva = Convert.ToDecimal(gridPedidos.Rows[e.RowIndex].Cells[5].Value);
                            cantidad = Convert.ToInt32(gridPedidos.Rows[e.RowIndex].Cells[6].Value);
                            total = (valor + iva) * cantidad;
                            gridPedidos.Rows[e.RowIndex].Cells[7].Value = total;
                            gridPedidos.CurrentCell = gridPedidos.Rows[e.RowIndex+1].Cells[2];
                        }
                        else
                        {
                            gridPedidos.Rows[e.RowIndex].Cells[6].Value = 0;
                            gridPedidos.Rows[e.RowIndex].Cells[7].Value = 0;
                        }
                    }
                    else
                    {
                        gridPedidos.Rows[e.RowIndex].Cells[6].Value = 0;
                        gridPedidos.Rows[e.RowIndex].Cells[7].Value = 0;
                    }

                }
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                if(r.InnerException != null)
                    MessageBox.Show(r.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridPedidos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gridPedidos_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            gridPedidos.Rows[e.RowIndex].Cells[6].ReadOnly = true;
        }

        private void toolStripSplitButtonNuevo_Click(object sender, EventArgs e)
        {
            habilitarCampos();
            limpiarCampos();
            txtNumPedido.Text = (NegPedidos.ultimoCodigoPedidos() + 1).ToString();
        }

        public void GuardarDatos()
        {
            try
            {
                if (pedido == null)
                {
                    pedido = new PEDIDOS();
                    pedido.PED_CODIGO = NegPedidos.ultimoCodigoPedidos() + 1;
                    pedido.ATE_CODIGO = atencion.ATE_CODIGO;
                    //pedido.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    pedido.ID_USUARIO = 1;
                    pedido.PED_ESTADO = FarmaciaPAR.PedidoPendiente;
                    pedido.PED_FECHA = DateTime.Now;
                    pedido.TIP_PEDIDO = FarmaciaPAR.PedidoMedicamentos;
                    NegPedidos.crearPedido(pedido);
                }
                for (Int16 i = 0; i < gridPedidos.Rows.Count - 1; i++)
                {
                    DataGridViewRow fila = gridPedidos.Rows[i];
                    PEDIDOS_DETALLE nuevaPrescripcion = new PEDIDOS_DETALLE();
                    nuevaPrescripcion.PEDIDOSReference.EntityKey = pedido.EntityKey;

                    if (fila.Cells["PDD_CODIGO"].Value == null)
                    {
                        nuevaPrescripcion.PDD_CODIGO = NegPedidos.ultimoCodigoPedidosDetalles() + 1;
                        int codProd = Convert.ToInt32(fila.Cells["PRO_CODIGO"].Value);
                        PRODUCTO producto = NegProducto.RecuperarProductoID(codProd);
                        nuevaPrescripcion.PRODUCTOReference.EntityKey = producto.EntityKey;

                        if (fila.Cells["PRO_DESCRIPCION"].Value != null)
                        nuevaPrescripcion.PRO_DESCRIPCION = fila.Cells["PRO_DESCRIPCION"].Value.ToString();
                        if (fila.Cells["PDD_CANTIDAD"].Value != null)
                            nuevaPrescripcion.PDD_CANTIDAD = Convert.ToInt32(fila.Cells["PDD_CANTIDAD"].Value);
                        if (fila.Cells["PDD_VALOR"].Value != null)
                            nuevaPrescripcion.PDD_VALOR = Convert.ToDecimal(fila.Cells["PDD_VALOR"].Value);
                        if (fila.Cells["PDD_IVA"].Value != null)
                            nuevaPrescripcion.PDD_IVA = Convert.ToDecimal(fila.Cells["PDD_IVA"].Value);
                        if (fila.Cells["PDD_TOTAL"].Value != null)
                            nuevaPrescripcion.PDD_TOTAL = Convert.ToDecimal(fila.Cells["PDD_TOTAL"].Value);
                        nuevaPrescripcion.PDD_ESTADO = false;

                        //Método Cambiado y da error hay que revisar 
                        //NegPedidos.crearDetallePedido(nuevaPrescripcion);
                    }
                    //else
                    //{
                    //    //int codPresc = (Int32)fila.Cells["PRES_CODIGO"].Value;
                    //    //nuevaPrescripcion = NegPrescripciones.recuperarPrescripcionID(codPresc);

                    //    //if (fila.Cells["PRES_ESTADO"].Value == null)
                    //    //    nuevaPrescripcion.PRES_ESTADO = false;
                    //    //else
                    //    //    nuevaPrescripcion.PRES_ESTADO = Convert.ToBoolean(fila.Cells["PRES_ESTADO"].Value);

                    //    //if (nuevaPrescripcion.PRES_ESTADO == true)
                    //    //{
                    //    //    nuevaPrescripcion.ID_USUARIO = His.Entidades.Clases.Sesion.codUsuario;
                    //    //    nuevaPrescripcion.NOM_USUARIO = Entidades.Clases.Sesion.nomUsuario;
                    //    //    nuevaPrescripcion.PRES_FARMACOS_INSUMOS = fila.Cells["PRES_FARMACOS_INSUMOS"].Value.ToString();
                    //    //    nuevaPrescripcion.PRES_FECHA_ADMINISTRACION = Convert.ToDateTime(fila.Cells["PRES_FECHA"].Value.ToString());
                    //    //}

                    //    //NegPrescripciones.editarPrescripcion(nuevaPrescripcion);
                    //}
                }
                MessageBox.Show("Información almacenada correctamente","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                if(e.InnerException != null)
                    MessageBox.Show(e.InnerException.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNumAtencion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frm_AyudaProductos lista = new frm_AyudaProductos("ATENCIONES");
                lista.campoPadre2 = txtNumAtencion;
                lista.ShowDialog();
            }


        }

        private void txtNumAtencion_KeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void txtNumAtencion_TextChanged(object sender, EventArgs e)
        {
            string numAtencion = txtNumAtencion.Text.Trim();
            if (numAtencion != string.Empty)
            {
                atencion = NegAtenciones.RecuperarAtencionPorNumero(numAtencion);
                if (atencion != null)
                {
                    paciente = NegPacientes.recuperarPacientePorAtencion(atencion.ATE_CODIGO);
                    txtHCL.Text = paciente.PAC_HISTORIA_CLINICA;
                    txtPaciente.Text = paciente.PAC_APELLIDO_PATERNO + " " + paciente.PAC_APELLIDO_MATERNO + " " + paciente.PAC_NOMBRE1 + " " + paciente.PAC_NOMBRE2;
                }
                else
                {
                    txtHCL.Text = string.Empty;
                    txtPaciente.Text = string.Empty;
                }
            }
            else
            {
                txtHCL.Text = string.Empty;
                txtPaciente.Text = string.Empty;
            }
        }
        public void habilitarCampos()
        {
            toolStripSplitButtonNuevo.Enabled = false;
            toolStripButtonCancelar.Enabled = true;
            toolStripButtonGuardar.Enabled = true;
            //toolStripButtonActualizar.Enabled = false;
            //toolStripButtonImprimir.Enabled = false;
        }
        public void deshabilitarCampos()
        {
            toolStripSplitButtonNuevo.Enabled = true;
            toolStripButtonCancelar.Enabled = false;
            toolStripButtonGuardar.Enabled = false;
            //toolStripButtonActualizar.Enabled = true;
            //toolStripButtonImprimir.Enabled = true;
        }
        public void limpiarCampos()
        {
            txtNumPedido.Text = string.Empty;
            txtNumAtencion.Text = string.Empty;
            txtPaciente.Text = string.Empty;
            txtHCL.Text = string.Empty;
            gridPedidos.Rows.Clear();
        }

        private void toolStripButtonGuardar_Click(object sender, EventArgs e)
        {
            deshabilitarCampos();
            GuardarDatos();
            //limpiarCampos();
        }

        private void toolStripButtonCancelar_Click(object sender, EventArgs e)
        {
            deshabilitarCampos();
            limpiarCampos();
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
