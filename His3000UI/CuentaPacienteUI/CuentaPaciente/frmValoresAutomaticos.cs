using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Negocio;  

namespace CuentaPaciente
{
    public partial class frmValoresAutomaticos : Form
    {
        public frmValoresAutomaticos()
        {
            InitializeComponent();
        }

        private void uCboListaCategorias_ValueChanged(object sender, EventArgs e)
        {

        }

        private void frmValoresAutomaticos_Load(object sender, EventArgs e)
        {
            //cargo categorias
            List<CATALOGO_COSTOS_TIPO> categorias = NegCatalogoCostosTipo.RecuperaCctipo();
            uCboListaCategorias.DataSource = categorias;
            uCboListaCategorias.DataMember = "CCT_NOMBRE";
            uCboListaCategorias.ValueMember = "CCT_CODIGO";
            uCboListaCategorias.SelectedIndex = 0; 
        }
        private void cargarServiciosPorCategoria()
        { 

        }
    }
}
