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
using Recursos;
using His.Parametros;
using Infragistics.Win.UltraWinGrid;
using System.IO;
using System.Windows.Forms.Integration;
using Infragistics.Win.UltraWinTabControl;
using His.Entidades.General;

namespace His.Admision
{
    public partial class FrmTarifario : Form
    {
        public FrmTarifario()
        {
            InitializeComponent();
            cargardatos();
        }
        public void cargardatos()
        {
            //this.tssMedicos.Image  = Recursos.Archivo.btnOrganigrama;  
            //imagenes del menu principal
            //toolStripnuevo.Image = Archivo.imgBtnAdd;
            //toolStripActualizar.Image = Archivo.imgBtnRefresh; 
            //toolStripGuardar.Image = Archivo.imgBtnGuardar32;
            //toolStripEliminar.Image = Archivo.imgBtnDelete;
            //toolStripCancelar.Image = Archivo.imgCancelar;
            //toolStripSalir.Image = Archivo.imgBtnSalir32;
            ultraGridIess.DataSource = NegIess.RecuperarTarifario();
            
        }

        private void toolStripSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
                
    }
}
