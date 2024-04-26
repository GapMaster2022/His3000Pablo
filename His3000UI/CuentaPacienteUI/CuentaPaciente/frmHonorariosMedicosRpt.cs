using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Entidades;
using His.Entidades.Pedidos;
using His.Formulario;
using His.Negocio;
using His.Parametros;
using Infragistics.Win.UltraWinGrid;
using Recursos;
using frm_Ayudas = His.Admision.frm_Ayudas;

namespace CuentaPaciente
{
    public partial class frmHonorariosMedicosRpt : Form
    {

        private MEDICOS medico;

        public Int32 CodigoMedico=0;
        public string Fecha1;
        public string Fecha2;
        public Int64 Tramite = 0;

        public frmHonorariosMedicosRpt()
        {
            InitializeComponent();
        }

        private void frmHonorariosMedicosRpt_Load(object sender, EventArgs e)
        {

        }

        private void ayudaMedicos_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos;

            listaMedicos = NegMedicos.listaMedicosIncTipoMedico();

            frm_Ayudas ayuda = new frm_Ayudas(listaMedicos, "MEDICOS", "CODIGO", "");
            ayuda.campoPadre = txtCodMedico;
            ayuda.ShowDialog();

            if (ayuda.campoPadre.Text != string.Empty)
                CargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));

            ayuda.Dispose();
        }

        private void btnMedico_Click(object sender, EventArgs e)
        {
            //His.Admision.
            List<MEDICOS> listaMedicos = NegMedicos.listaMedicosIncTipoMedico();

            His.Admision.frm_Ayudas ayuda = new frm_Ayudas(listaMedicos, "MEDICOS", "CODIGO","");
            ayuda.campoPadre = txtCodMedico;
            ayuda.ShowDialog();

            if (ayuda.campoPadre.Text != string.Empty)
                CargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
        }
        private void CargarMedico(int codMedico)
        {
            
            medico = NegMedicos.MedicoID(codMedico);
            if (medico != null)
            {
                DataTable med = NegMedicos.MedicoIDValida(codMedico);
                if (med.Rows[0][0].ToString() == "7")
                {
                    MessageBox.Show("MEDICO SUSPENDIDO", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                txtNombreMedico.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + "  " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;

            }
            else
                txtNombreMedico.Text = string.Empty;
        }

        private void txtCodMedico_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtCodMedico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode ==(Keys.F1))
            {
                List<MEDICOS> listaMedicos = NegMedicos.listaMedicosIncTipoMedico();

                His.Admision.frm_Ayudas ayuda = new frm_Ayudas(listaMedicos, "MEDICOS", "CODIGO", "");
                ayuda.campoPadre = txtCodMedico;
                ayuda.ShowDialog();

                if (ayuda.campoPadre.Text != string.Empty)
                    CargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()));
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            //frmReporteDesgloseFactura(string NumeroFactura, string Cliente,string Ruc,string Telefono,Int32 CodigoAtencion,string Tipo,string CadenaAtenciones )

            if (txtCodMedico.Text != "")
            {
                CodigoMedico = Convert.ToInt32(txtCodMedico.Text);
            }
            else
            {
                CodigoMedico = 0;
            }
            Fecha1=dtpFecha1.Text;
            Fecha2=dtpFecha2.Text;

            if (txtTramite.Text == "")
            {
                Tramite = 0;
            }
            else
            {
                Tramite = Convert.ToInt64(txtTramite.Text);
            }


            frmReporteDesgloseFactura Forma = new frmReporteDesgloseFactura("", "", "", "","", 0, "HONORARIOS", "", CodigoMedico, Fecha1, Fecha2, Tramite,0,"","","","", "", "", "", "");
            Forma.Show();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
