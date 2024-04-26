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


namespace His.Honorarios
{
    public partial class frmAnticiposSic : Form
    {
        public string cod_anticipo;
        public string valor_anticipo;
        public string nombre_paciente;
        public int validador = 0;
        string _ate_codigo;
        public frmAnticiposSic(string ate_codigo)
        {
            _ate_codigo = ate_codigo;
            InitializeComponent();
            CargaAnticipos();
        }
        public void CargaAnticipos()
        {
            DataTable aux = new DataTable();
            aux = NegFactura.AnticiposSic_sp(_ate_codigo);
            if (aux.Rows.Count > 0)
            {
                ultraGridFormasPago.DataSource = aux;
                //if (ultraGridFormasPago.Rows.Count == 0)
                //{
                //    this.Close();
                //    MessageBox.Show("NO EXISTEN ANTICIPOS PARA ESTE PACIENTE", "HIS3000");
                //    validador = 1;
                //}
            }
            else
            {
                this.Close();
                MessageBox.Show("NO EXISTEN ANTICIPOS PARA ESTE PACIENTE", "HIS3000");
                validador = 1;
            }
        }

        private void ultraGridFormasPago_DoubleClick(object sender, System.EventArgs e)
        {
            cod_anticipo = ultraGridFormasPago.Rows[ultraGridFormasPago.ActiveRow.Index].Cells[0].Value.ToString();
            valor_anticipo = ultraGridFormasPago.Rows[ultraGridFormasPago.ActiveRow.Index].Cells[1].Value.ToString();
            nombre_paciente = ultraGridFormasPago.Rows[ultraGridFormasPago.ActiveRow.Index].Cells[2].Value.ToString();
            if (cod_anticipo != null)
                this.Close();
        }

        private void ultraGridFormasPago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cod_anticipo = ultraGridFormasPago.Rows[ultraGridFormasPago.ActiveRow.Index].Cells[0].Value.ToString();
                valor_anticipo = ultraGridFormasPago.Rows[ultraGridFormasPago.ActiveRow.Index].Cells[1].Value.ToString();
                nombre_paciente = ultraGridFormasPago.Rows[ultraGridFormasPago.ActiveRow.Index].Cells[2].Value.ToString();
                if (cod_anticipo != null)
                    this.Close();
            }
        }

    }
}
