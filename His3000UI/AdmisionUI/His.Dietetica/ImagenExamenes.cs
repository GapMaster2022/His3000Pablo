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
            dtpDesde.Enabled = false;
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
            if(x[17].ToString() == "True")
            {
                ckbCD.Checked = true;
            }
            if(x[18].ToString() == "True")
            {
                ckbDVD.Checked = true;
            }

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
            DateTime Hoy = DateTime.Now;
            string fecha = Hoy.ToShortDateString();
            string horanoche = "19:00:00";
            string horamañana = "06:00:00";
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
            if(ckbCD.Checked == true)
            {
                x[17] = "1";
            }
            else
            {
                x[17] = "0";
            }
            if (ckbDVD.Checked == true)
            {
                x[18] = "1";
            }
            else
            {
                x[18] = "0";
            }
            Negocio.NegDietetica.setROW("SetAgendamientoImagen", x);
            try
            {
                NegDietetica die = new NegDietetica();
                bool ok = false;
                //ok = NegDietetica.DiasFestivos(Hoy);                
                if (ok)
                {
                    if (ok || DayOfWeek.Saturday == Hoy.DayOfWeek || DayOfWeek.Sunday == Hoy.DayOfWeek
                        || Hoy >= Convert.ToDateTime(fecha + " " + horanoche) || Hoy <= Convert.ToDateTime(fecha + " " + horamañana))
                    {
                        string cue_codigo = x[1].ToString();
                        die.RecargoImagen(cue_codigo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
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

        private void ckbCD_CheckedChanged(object sender, EventArgs e)
        {
            //if(ckbCD.Checked == true)
            //{
            //    ckbDVD.Checked = false;
            //    ckbDVD.Enabled = false;
            //}else
            //{
            //    ckbDVD.Enabled = true;
            //}
        }

        private void ckbDVD_CheckedChanged(object sender, EventArgs e)
        {
            //if(ckbDVD.Checked == true)
            //{
            //    ckbCD.Checked = false;
            //    ckbCD.Enabled = false;
            //}
            //else
            //{
            //    ckbCD.Enabled = true;
            //}
        }

        private void dtpDesde_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void ckbCD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void ckbDVD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                txtPEnviadas.Focus();
                txtPEnviadas.SelectAll();
            }
//                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txtPEnviadas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                txtPDanada.Focus();
                txtPDanada.SelectAll();
            }
            //System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txtPDanada_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                txtPOdonto.Focus();
                txtPOdonto.SelectAll();
            }
            //System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txtPOdonto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void chkMedioContraste_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                txt30x40.Focus();
                txt30x40.SelectAll();
            }
            //System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txt30x40_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                txt8x10.Focus();
                txt8x10.SelectAll();
            }
        }

        private void txt8x10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                txt14x14.Focus();
                txt14x14.SelectAll();
            }
        }

        private void txt14x14_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                txt14x17.Focus();
                txt14x17.SelectAll();
            }
        }

        private void txt14x17_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                txt18x24.Focus();
                txt18x24.SelectAll();
            }
        }

        private void txt18x24_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txtPaciente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txtHC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txtAtencion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }

        private void txtIngreso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                System.Windows.Forms.SendKeys.Send("{TAB}");
        }
    }
}
