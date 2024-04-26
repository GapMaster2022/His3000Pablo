using System;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Negocio;
using His.Parametros;
using Infragistics.Win.UltraWinGrid;
using Recursos;
using frmImpresionPedidos=His.HabitacionesUI.frmImpresionPedidos;

namespace His.Pedidos
{
    public partial class frmConsultaPedido : Form
    {
        public frmConsultaPedido()
        {
            InitializeComponent();
        }

        private void btn_Buscar_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = NegPedidos.ListaPedidos(dateTimePicker1.Value, dateTimePicker2.Value);
            dataGridView1.DataSource = dt;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Int32 Pedido=0;
            Int32 codigoArea=0;
            Int32 fila=0;
            fila=dataGridView1.CurrentRow.Index;
            Pedido = Convert.ToInt32(dataGridView1.Rows[fila].Cells[0].Value.ToString());
            codigoArea = Convert.ToInt32(dataGridView1.Rows[fila].Cells[7].Value.ToString());
            frmImpresionPedidos frmPedidos = new frmImpresionPedidos(Pedido, codigoArea,1,0);
            frmPedidos.Show();
        }
    }
}
