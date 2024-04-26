using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using System.Data.Objects;
using System.Data.Objects.DataClasses; 

namespace TarifariosUI
{
    public partial class frmListas : Form
    {
        TextBox campoPadre;
        string tipo;
        public frmListas()
        {
            InitializeComponent();
        }

        public frmListas(string lista,TextBox campo)
        //    public frmListas(List<HisModelo.PACIENTES> lista,TextBox campo)
        {
            InitializeComponent();
            campoPadre = campo;
            cargarLista(lista);
            tipo = lista;
        }

        private void cargarLista(string tipo)
        {
            HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
            if (tipo == "pacientes")
            {
                var QueryPacientes = from p in contexto.PACIENTES
                                     where p.PAC_ESTADO == true
                                     orderby p.PAC_APELLIDO_PATERNO
                                     select (new { p.PAC_CODIGO, p.PAC_HISTORIA_CLINICA, PAC_APELLIDO_PATERNO = (p.PAC_APELLIDO_PATERNO + " " + p.PAC_APELLIDO_MATERNO), PAC_NOMBRE1 = (p.PAC_NOMBRE1 + " " + p.PAC_NOMBRE2) });
                dataGridViewLista.DataSource = QueryPacientes;
                for (int i = 2; i < dataGridViewLista.Columns.Count; i++)
                    dataGridViewLista.Columns[i].Visible = false;
                dataGridViewLista.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridViewLista.Columns["PAC_CODIGO"].HeaderText = "COD";
                dataGridViewLista.Columns["PAC_CODIGO"].Width = 40;
                dataGridViewLista.Columns["PAC_HISTORIA_CLINICA"].HeaderText = "H. CLINICA";
                dataGridViewLista.Columns["PAC_HISTORIA_CLINICA"].Width = 100;
                dataGridViewLista.Columns["PAC_APELLIDO_PATERNO"].Visible = true;
                dataGridViewLista.Columns["PAC_NOMBRE1"].Visible = true;
                dataGridViewLista.Columns["PAC_APELLIDO_PATERNO"].HeaderText = "NOMBRES";
                dataGridViewLista.Columns["PAC_NOMBRE1"].HeaderText = "APELLIDOS";
                dataGridViewLista.Columns["PAC_APELLIDO_PATERNO"].Width = 120;
                dataGridViewLista.Columns["PAC_NOMBRE1"].Width = 120;
            }
            else if (tipo == "ciudades")
            {
                var QueryCiudades = from c in contexto.CIUDAD
                                    select c.NOMCIU ;
                dataGridViewLista.DataSource = QueryCiudades;
                dataGridViewLista.Columns["NOMCIU"].HeaderText = "CIUDAD";
                dataGridViewLista.Columns["NOMCIU"].Width = 240;
            }
        }

        private void frmListas_Load(object sender, EventArgs e)
        {

        }

        private void dataGridViewLista_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            enviarCodigo();
        }

        private void dataGridViewLista_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void enviarCodigo()
        {
            if (dataGridViewLista.RowCount>0)
            {
                DataGridViewRow fila = dataGridViewLista.CurrentRow;
                campoPadre.Text = fila.Cells[0].Value.ToString();
                //campoPadre.Text = dataGridViewLista.CurrentRow.Cells[0].Value.ToString() ;
            }
            this.Close();
        }

        private void dataGridViewLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewLista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataGridViewRow fila = dataGridViewLista.CurrentRow;
                e.Handled = true;
                enviarCodigo();
            }
        }

        private void dataGridViewLista_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            { 
            }
        }
    }
}
