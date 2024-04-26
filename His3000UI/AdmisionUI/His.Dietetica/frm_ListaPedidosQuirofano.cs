using His.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Dietetica
{
    public partial class frm_ListaPedidosQuirofano : Form
    {
        public int ate_codigo;
        public string ped_codigo;
        public frm_ListaPedidosQuirofano()
        {
            InitializeComponent();
        }

        private void frm_ListaPedidosQuirofano_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable ListaPedidosQ = new DataTable();
                ListaPedidosQ = NegQuirofano.ListaPedidosQuirofano(ate_codigo);
                ListaPedidos.DataSource = ListaPedidosQ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar lista de pedidos. Más detalles: " + ex.Message, "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListaPedidos_DoubleClick(object sender, EventArgs e)
        {
            if(ListaPedidos.SelectedRows.Count > 0 && ListaPedidos.SelectedRows.Count == 1)
            {
                ped_codigo = ListaPedidos.CurrentRow.Cells["COD. PEDIDO"].Value.ToString();
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe elegir solo un pedido", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
