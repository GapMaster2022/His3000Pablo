using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Emergencia
{
    public partial class frm_AyudaMedicamentos : Form
    {
        public frm_AyudaMedicamentos()
        {
            InitializeComponent();
        }
        public string desc = "";
        public string cod = "";

        private void frm_AyudaMedicamentos_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hIS3000BDDataSetProductos.PRODUCTOS' table. You can move, or remove it, as needed.
            this.pRODUCTOSTableAdapter.Fill(this.hIS3000BDDataSetProductos.PRODUCTOS);

        }

        private void grid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            cod = grid.Rows[e.RowIndex].Cells[0].Value.ToString();
            desc = grid.Rows[e.RowIndex].Cells[1].Value.ToString();
            this.Close();
        }
    }
}
