using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;
using His.Entidades;
using Infragistics.Win.UltraWinGrid;
using System.Threading;

namespace His.Maintenance
{
    public partial class frm_HonorarioConsultaExterna : Form
    {
        DataTable producto = new DataTable();
        public frm_HonorarioConsultaExterna()
        {
            InitializeComponent();
            cargarHCEX();
            cargarConboBox();
            bloquear(true, false, false, false);
            cmbProductos.SelectedIndex = -1;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            bloquear(false, true, false, true);
            cmbProductos.SelectedIndex = -1;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            HONORARIOS_CONSULTA_EXTERNA hcex = NegHonorarios.existeProductoHonorarrio(Convert.ToString(cmbProductos.SelectedValue));
            if (hcex == null)
            {
                hcex = new HONORARIOS_CONSULTA_EXTERNA();
                hcex.PRO_CODIGO = Convert.ToString(cmbProductos.SelectedValue);
                hcex.PRO_DESCRIPCION = cmbProductos.Text.Trim();
                if (NegHonorarios.insertarHCEX(hcex))
                {
                    bloquear(true, false, false, false);
                    cmbProductos.SelectedIndex = -1;
                    cargarHCEX();
                    MensajeGuardarConPausa();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el Producto \r\n Comuniquese con el administrador", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
                MessageBox.Show("El producto ya ha sido agregado", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bloquear(true, false, false, false);
            cmbProductos.SelectedIndex = -1;
            cmbProductos.Text = "";
        }
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (NegHonorarios.eliminarHCEX(producto.Rows[0][0].ToString()))
            {
                bloquear(true, false, false, false);
                cmbProductos.SelectedIndex = -1;
                cargarHCEX();
                MensajeEliminarConPausa();
            }
            else
            {
                MessageBox.Show("No se pudo eliminar el Producto \r\n Comuniquese con el administrador", "His3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UltraGridHCEX_DoubleClick(object sender, EventArgs e)
        {
            UltraGridRow fila = UltraGridHCEX.ActiveRow;
            if (UltraGridHCEX.Selected.Rows.Count > 0)
            {
                if (MessageBox.Show("Esta seguro de eliminar el producto" + fila.Cells["DESCRIPCION"].Value.ToString(), "His3000", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bloquear(false, false, true, false);
                    producto = NegProducto.recuperaProductoSicXcodpro(fila.Cells["COD_PRODUCTO"].Value.ToString());
                    cmbProductos.SelectedItem = Convert.ToInt64(producto.Rows[0][0].ToString());
                    cmbProductos.Text = producto.Rows[0][1].ToString();
                }
            }
        }

        private void UltraGridHCEX_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            UltraGridBand bandUno = UltraGridHCEX.DisplayLayout.Bands[0];

            bandUno.Override.CellClickAction = CellClickAction.RowSelect;


            bandUno.Override.CellClickAction = CellClickAction.RowSelect;
            UltraGridHCEX.DisplayLayout.Override.FilterUIType = FilterUIType.FilterRow;
            UltraGridHCEX.DisplayLayout.Override.FilterEvaluationTrigger = FilterEvaluationTrigger.OnCellValueChange;
            UltraGridHCEX.DisplayLayout.Override.FilterOperatorLocation = FilterOperatorLocation.WithOperand;
            UltraGridHCEX.DisplayLayout.Override.FilterClearButtonLocation = FilterClearButtonLocation.RowAndCell;
            //dbgrPagosFacMedicos.DisplayLayout.Override.FilterRowPrompt = "Filtro";  
            UltraGridHCEX.DisplayLayout.Override.SpecialRowSeparator = SpecialRowSeparator.FilterRow;

            e.Layout.PerformAutoResizeColumns(true, PerformAutoSizeType.AllRowsInBand);
        }

        private void frm_HonorarioConsultaExterna_Load(object sender, EventArgs e)
        {
            cargarHCEX();
            cargarConboBox();
            bloquear(true, false, false, false);
            cmbProductos.SelectedIndex = -1;
        }
        public void cargarHCEX()
        {
            UltraGridHCEX.DataSource = NegHonorarios.listaHCEX();
        }
        public void cargarConboBox()
        {
            PARAMETROS_DETALLE pd = NegParametros.RecuperaPorCodigo(67);

            cmbProductos.DataSource = NegProducto.listaproductosXdescripcion(pd.PAD_VALOR);
            cmbProductos.ValueMember = "codpro";
            cmbProductos.DisplayMember = "despro";
            cmbProductos.SelectedIndex = -1;
        }
        public void bloquear(bool nuevo, bool guardar, bool eliminar, bool cmb)
        {
            btnGuardar.Enabled = guardar;
            btnBorrar.Enabled = eliminar;
            btnNuevo.Enabled = nuevo;
            cmbProductos.Enabled = cmb;
        }
        private void MensajeGuardarConPausa()
        {
            lblMensaje.Text = "Producto Guardado";

            // Iniciar un hilo para la pausa en segundo plano
            Thread thread = new Thread(() =>
            {
                // Pausa de 3 segundos
                Thread.Sleep(2000);

                // Limpiar el texto después de la pausa
                lblMensaje.Invoke((MethodInvoker)delegate
                {
                    lblMensaje.Text = "";
                });
            });

            // Iniciar el hilo
            thread.Start();
        }
        private void MensajeEliminarConPausa()
        {
            lblMensaje.Text = "Producto Eliminado";

            // Iniciar un hilo para la pausa en segundo plano
            Thread thread = new Thread(() =>
            {
                // Pausa de 3 segundos
                Thread.Sleep(2000);

                // Limpiar el texto después de la pausa
                lblMensaje.Invoke((MethodInvoker)delegate
                {
                    lblMensaje.Text = "";
                });
            });

            // Iniciar el hilo
            thread.Start();
        }
    }
}