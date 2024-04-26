using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;

namespace His.Dietetica
{
    public partial class frmAuxViewer : Form
    {
        private string xcodigo;
        public frmAuxViewer(string codigo)
        {
            InitializeComponent();
            grid.DataSource = NegDietetica.getDataTable("HistorialDietas", codigo);
            DataTable rs = NegDietetica.getDataTable("getObservacionAtencion",codigo);
            try { textBox1.Text = rs.Rows[0][0].ToString(); }
            catch {}
            xcodigo = codigo;
            this.Text = "Detalle de Alimentacion - Atencion Nro." + codigo;
            button1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (xcodigo!= textBox1.Text.ToString())
            {
                object[] x = new object[] { textBox1.Text.ToString() };
                NegDietetica.setROW("setObservacionAtencion", x, xcodigo);
            }
            this.Close();
        }
    }
}
