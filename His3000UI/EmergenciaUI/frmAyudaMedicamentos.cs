using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Reportes;
using His.Formulario;
using His.Negocio;
using Recursos;
using GeneralApp.ControlesWinForms;
using His.Parametros;
using His.General;
using His.Entidades.Clases;
using His.DatosReportes;
using System.Windows.Forms.VisualStyles;
using His.Formulario;

namespace His.Emergencia
{
    public partial class frmAyudaMedicamentos : Form
    {
        DataTable dtListaMedicamentos = new DataTable();
        public string Codigo = "";
        public string Descripcion = "";
        public string CodigoMSP = "";

        string _TipoBusqueda = "";

        public frmAyudaMedicamentos(string TipoBusqueda)
        {
            _TipoBusqueda = TipoBusqueda;
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            if (_TipoBusqueda == "MEDICAMENTOS")
            {
                dtListaMedicamentos = NegAtenciones.ListaMedicamentos(this.txtFiltro.Text);
                gResultado.DataSource = dtListaMedicamentos;
                gResultado.Columns[1].Width = 300;
            }

            if (_TipoBusqueda == "MEDICOS")
            {
                dtListaMedicamentos = NegAtenciones.ListaMedicos("9999989");
                gResultado.DataSource = dtListaMedicamentos;
                gResultado.Columns[1].Width = 300;
            }
            

        }

        private void gResultado_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewRow Fila = null;
            //Fila = gResultado.CurrentRow;

            //if (_TipoBusqueda == "MEDICAMENTOS")
            //{
            //    Codigo = Fila.Cells["CODIGO"].Value.ToString();
            //    Descripcion = Fila.Cells["NOMBRE"].Value.ToString();
            //}

            //if (_TipoBusqueda == "MEDICOS")
            //{
            //    Codigo = Fila.Cells["CODIGO"].Value.ToString();
            //    Descripcion = Fila.Cells["NOMBRE"].Value.ToString();
            //    CodigoMSP = Fila.Cells["MSP"].Value.ToString();
            //}

        }

        private void gResultado_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewRow Fila = null;
            Fila = gResultado.CurrentRow;

            if (Fila != null)
            {

                if (_TipoBusqueda == "MEDICAMENTOS")
                {
                    Codigo = Fila.Cells["CODIGO"].Value.ToString();
                    Descripcion = Fila.Cells["NOMBRE"].Value.ToString();
                }

                if (_TipoBusqueda == "MEDICOS")
                {
                    Codigo = Fila.Cells["CODIGO"].Value.ToString();
                    Descripcion = Fila.Cells["NOMBRE"].Value.ToString();
                    CodigoMSP = Fila.Cells["MSP"].Value.ToString();
                }
            }
            this.Close();
        }

        private void gResultado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataGridViewRow Fila = null;
                Fila = gResultado.CurrentRow;

                if (Fila != null)
                {

                    if (_TipoBusqueda == "MEDICAMENTOS")
                    {
                        Codigo = Fila.Cells["CODIGO"].Value.ToString();
                        Descripcion = Fila.Cells["NOMBRE"].Value.ToString();
                    }

                    if (_TipoBusqueda == "MEDICOS")
                    {
                        Codigo = Fila.Cells["CODIGO"].Value.ToString();
                        Descripcion = Fila.Cells["NOMBRE"].Value.ToString();
                        CodigoMSP = Fila.Cells["MSP"].Value.ToString();
                    }
                }

                this.Close();
            }
        }

        private void frmAyudaMedicamentos_Load(object sender, EventArgs e)
        {
            this.txtFiltro.Focus();

            if (_TipoBusqueda == "MEDICAMENTOS")
            {
                dtListaMedicamentos = NegAtenciones.ListaMedicamentos(this.txtFiltro.Text);
                gResultado.DataSource = dtListaMedicamentos;
                gResultado.Columns[1].Width = 300;
            }

            if (_TipoBusqueda == "MEDICOS")
            {
                dtListaMedicamentos = NegAtenciones.ListaMedicos("9999989");
                gResultado.DataSource = dtListaMedicamentos;
                gResultado.Columns[1].Width = 300;
            }
        }

        private void gResultado_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
