using His.Entidades;
using His.Entidades.Clases;
using His.Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace His.Dietetica
{
    public partial class ImagenExamenes : Form
    {
        USUARIOS usuario = new USUARIOS();
        object[] x;

        public ImagenExamenes(object[] xz)
        {
            usuario = NegUsuarios.RecuperaUsuario(Sesion.codUsuario);
            x = xz;
            InitializeComponent();
            txtPaciente.Text = x[2].ToString();
            txtHC.Text = x[3].ToString();
            txtAtencion.Text = x[4].ToString();
            txtIngreso.Text = x[5].ToString();
            if (Convert.ToDateTime(x[6].ToString())== Convert.ToDateTime("1900-01-01T00:00:00"))
            {
                dtpDesde.Value = DateTime.Now;
            }else
                dtpDesde.Value = Convert.ToDateTime(x[6].ToString());
            txt30x40.Text = x[7].ToString();
            txt8x10.Text = x[8].ToString();
            txt14x14.Text = x[9].ToString();
            txt14x17.Text = x[10].ToString();
            txt18x24.Text = x[11].ToString();
            txtPOdonto.Text = x[12].ToString();
            txtPDanada.Text = x[13].ToString();
            txtPEnviadas.Text = x[14].ToString();
            if (x[15].ToString() == "1")
                chkMedioContraste.Checked = true;
            x[16] = usuario.ID_USUARIO.ToString();

        }

        private void ultraGroupBox10_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ValidaVacios();
            x[6] = dtpDesde.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss");
            x[7] = txt30x40.Text;
            x[8] = txt8x10.Text ;
            x[9] = txt14x14.Text;
            x[10] = txt14x17.Text;
            x[11] = txt18x24.Text;
            x[12] = txtPOdonto.Text;
            x[13] = txtPDanada.Text;
            x[14] = txtPEnviadas.Text;
            if (chkMedioContraste.Checked)
                    x[15] = "1";
            else
                    x[15] = "0";
            x[16] = usuario.ID_USUARIO.ToString();

            Negocio.NegDietetica.setROW("SetAgendamientoImagen", x);
            this.Close();
        }

        private void ValidaVacios()
        {
            if (txt30x40.Text == "")
                txt30x40.Text = "0";
            if (txt8x10.Text == "")
                txt8x10.Text = "0";
            if (txt14x14.Text == "")
                txt14x14.Text = "0";
            if (txt14x17.Text == "")
                txt14x17.Text = "0";
            if (txt18x24.Text == "")
                txt18x24.Text = "0";
            if (txtPOdonto.Text == "")
                txtPOdonto.Text = "0";
            if (txtPDanada.Text == "")
                txtPDanada.Text = "0";
            if (txtPEnviadas.Text == "")
                txtPEnviadas.Text = "0"; 
        }
    }
}
