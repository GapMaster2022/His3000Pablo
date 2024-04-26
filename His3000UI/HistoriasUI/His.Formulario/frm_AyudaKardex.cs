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

namespace His.Formulario
{
    public partial class frm_AyudaKardex : Form
    {
        public string medicamento = "";
        public string cue_codigo = "";
        public string cantidad = "";
        public string producto = "";
        public string ateCodigo = "";
        public int frecuencia = 0;
        public int via = 0;
        public double dosis = 0;
        public int medida = 0;
        public Int64 idReseerva = 0;
        public List<KardexCompuesto> lista = new List<KardexCompuesto>();

        bool reserva = false;
        List<KardexEnfermeriaMEdicamentos> Lista = new List<KardexEnfermeriaMEdicamentos>();

        public frm_AyudaKardex(string ate_codigo, int rubro, int check)
        {
            InitializeComponent();
            Lista = NegFormulariosHCU.RecuperaMedicamentos(ate_codigo, rubro, check);
            dtgAyudaKardex.DataSource = Lista;
            //grid.DataSource = Lista;
            dtgAyudaKardex.Columns[0].Width = 300;
            dtgAyudaKardex.Columns[1].Width = 60;
            dtgAyudaKardex.Columns[2].Width = 40;
            dtgAyudaKardex.Columns[3].Visible = false;

            DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
            checkBoxColumn.HeaderText = "PRODUCTO COMPUESTO";
            checkBoxColumn.Name = "checkBoxColumn";
            checkBoxColumn.Visible = false;
            // Agregar la columna al DataGridView
            dtgAyudaKardex.Columns.Add(checkBoxColumn);
            dtgAyudaKardex.Columns[4].Visible = false;
            textBox1.Focus();
        }
        public frm_AyudaKardex(Int64 ate_codigo, bool _reserva = false)
        {
            InitializeComponent();
            List<RESERVA_KARDEX_MEDICAMENTO> list = new List<RESERVA_KARDEX_MEDICAMENTO>();
            list = NegFormulariosHCU.RecuperaReservas(ate_codigo);
            dtgAyudaKardex.DataSource = list;
            reserva = _reserva;
            //grid.DataSource = Lista;
            dtgAyudaKardex.Columns[0].Visible = false;
            dtgAyudaKardex.Columns[1].Visible = false;
            dtgAyudaKardex.Columns[2].Visible = false;
            dtgAyudaKardex.Columns[3].Visible = false;
            dtgAyudaKardex.Columns[4].Width = 300;
            dtgAyudaKardex.Columns[5].Visible = false;
            dtgAyudaKardex.Columns[6].Visible = false;
            dtgAyudaKardex.Columns[7].Visible = false;
            dtgAyudaKardex.Columns[8].Visible = false;
            dtgAyudaKardex.Columns[9].Width = 60;
            dtgAyudaKardex.Columns[10].Visible = false;
            dtgAyudaKardex.Columns[11].Width = 60;
            dtgAyudaKardex.Columns[12].Visible = false;
            dtgAyudaKardex.Columns[13].Visible = false;
            textBox1.Visible = false;
        }

        
        private void dtgAyudaKardex_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (!reserva)
            {
                
                for (int i = 0; i < dtgAyudaKardex.RowCount; i++)
                {
                    if (dtgAyudaKardex.Rows[i].Cells[0].Value == null)
                        dtgAyudaKardex.Rows[i].Cells[0].Value = false;
                    if (Convert.ToBoolean(dtgAyudaKardex.Rows[i].Cells[0].Value.ToString()) == true)
                    {
                        KardexCompuesto obj = new KardexCompuesto();
                        obj.medicamento = dtgAyudaKardex.Rows[i].Cells[1].Value.ToString();
                        obj.cue_codigo = dtgAyudaKardex.Rows[i].Cells[2].Value.ToString();
                        obj.cantidad = dtgAyudaKardex.Rows[i].Cells[3].Value.ToString();
                        obj.producto = dtgAyudaKardex.Rows[i].Cells[4].Value.ToString();
                        lista.Add(obj);
                    }
                }
                if(lista.Count > 0)
                {
                    this.Close();
                    return;
                }
                if (e.RowIndex != -1)
                {
                    medicamento = dtgAyudaKardex.Rows[e.RowIndex].Cells[1].Value.ToString();
                    cue_codigo = dtgAyudaKardex.Rows[e.RowIndex].Cells[2].Value.ToString();
                    cantidad = dtgAyudaKardex.Rows[e.RowIndex].Cells[3].Value.ToString();
                    producto = dtgAyudaKardex.Rows[e.RowIndex].Cells[4].Value.ToString();
                    this.Close();
                }
            }
            else
            {
                if (e.RowIndex != -1)
                {
                    idReseerva = Convert.ToInt64(dtgAyudaKardex.Rows[e.RowIndex].Cells[0].Value);
                    ateCodigo = dtgAyudaKardex.Rows[e.RowIndex].Cells[2].Value.ToString();
                    cue_codigo = dtgAyudaKardex.Rows[e.RowIndex].Cells[3].Value.ToString();
                    medicamento = dtgAyudaKardex.Rows[e.RowIndex].Cells[4].Value.ToString();
                    frecuencia = Convert.ToInt32(dtgAyudaKardex.Rows[e.RowIndex].Cells[5].Value);
                    via = Convert.ToInt32(dtgAyudaKardex.Rows[e.RowIndex].Cells[6].Value);
                    dosis = Convert.ToInt32(dtgAyudaKardex.Rows[e.RowIndex].Cells[7].Value);
                    medida = Convert.ToInt32(dtgAyudaKardex.Rows[e.RowIndex].Cells[8].Value);
                    cantidad = dtgAyudaKardex.Rows[e.RowIndex].Cells[9].Value.ToString();
                    this.Close();
                }

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            var q = from x in Lista
                    where x.Producto.Contains(textBox1.Text.Trim())
                    select x;
            dtgAyudaKardex.DataSource = q.ToList();
            //grid.DataSource = q.ToList();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // e.Handled = e.KeyChar == Convert.ToChar(Keys.Space);
            if ((int)e.KeyChar == (int)Keys.Escape)
                this.Close();
            if ((int)e.KeyChar == (int)Keys.Enter)
                dtgAyudaKardex.Focus();
        }

        private void dtgAyudaKardex_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataGridViewRow Item = null;
            Item = dtgAyudaKardex.CurrentRow;

            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                //medicamento.Text = Item.Cells[0].Value.ToString();
                //cue_codigo.Text = Item.Cells[1].Value.ToString();
                //cantidad.Text = Item.Cells[2].Value.ToString();
                this.Close();


            }
            if ((int)e.KeyChar == (int)Keys.Escape)
                this.Close();
        }

        private void dtgAyudaKardex_CellEnter(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            //if ((int)e.KeyChar == (int)Keys.Enter)
            //{


            //    medicamento.Text = dtgAyudaKardex.CurrentRow.Cells[0].Value.ToString();
            //    cue_codigo.Text = dtgAyudaKardex.CurrentRow.Cells[1].Value.ToString();
            //    cantidad.Text = dtgAyudaKardex.CurrentRow.Cells[2].Value.ToString();
            //    //this.Close();

            //}
        }

        private void grid_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {

        }

        private void frm_AyudaKardex_Load(object sender, EventArgs e)
        {

        }

        private void dtgAyudaKardex_CurrentCellChanged(object sender, EventArgs e)
        {
            //DataGridViewRow Item = null;
            //Item = dtgAyudaKardex.CurrentRow;
            //if (Item != null)
            //{
            //    try
            //    {
            //        //Console.WriteLine(Item.Cells[0].Value.ToString() + "  " + Item.Cells[1].Value.ToString() + "  " + Item.Cells[2].Value.ToString());
            //        medicamento = Item.Cells[0].Value.ToString();
            //        cue_codigo = Item.Cells[1].Value.ToString();
            //        cantidad = Item.Cells[2].Value.ToString();
            //    }
            //    catch (Exception ex)
            //    {

            //        //throw;
            //    }
            //}           

        }

        private void dtgAyudaKardex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataGridViewRow row = ((DataGridView)sender).CurrentRow;
                string valorPr = Convert.ToString(row.Cells[0].Value);
                e.Handled = true;
            }
        }
    }
}
