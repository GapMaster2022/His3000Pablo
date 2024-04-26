using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Negocio;
using His.Entidades.Clases;
using Recursos;
using Infragistics.Win.UltraWinGrid;
using His.Maintenance;
using His.Parametros;
using His.DatosReportes;
using His.Entidades.Reportes;
using His.Formulario;
using Core.Utilitarios;
//using GeneralApp;
using System.Runtime.InteropServices;
using System.Data.OleDb;

namespace CuentaPaciente
{
    public partial class frmAnticipos : Form
    {
        string _IdCliente="";

        public frmAnticipos(string IdCliente)
        {
            _IdCliente = IdCliente;
            InitializeComponent();
        }

        private void frmAnticipos_Load(object sender, EventArgs e)
        {
            this.dgvAnticiposCliente.DataSource = NegFactura.AnticiposCliente(_IdCliente); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
