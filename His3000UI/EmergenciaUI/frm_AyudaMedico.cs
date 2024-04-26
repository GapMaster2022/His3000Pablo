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
    public partial class frm_AyudaMedico : Form
    {
        public frm_AyudaMedico()
        {
            InitializeComponent();
        }
        public string cod = "";
        public string nombre = "";
        private void frm_AyudaMedico_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hIS3000BDDataSet.MEDICOS' table. You can move, or remove it, as needed.
            this.mEDICOSTableAdapter.Fill(this.hIS3000BDDataSet.MEDICOS);

        }

        private void grid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            cod = grid.Rows[e.RowIndex].Cells[0].Value.ToString();
            nombre = grid.Rows[e.RowIndex].Cells[1].Value.ToString().Trim()
                + " " + grid.Rows[e.RowIndex].Cells[2].Value.ToString().Trim()
                + " " + grid.Rows[e.RowIndex].Cells[3].Value.ToString().Trim()
                + " " + grid.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();
            this.Close();
        }
    }
}
