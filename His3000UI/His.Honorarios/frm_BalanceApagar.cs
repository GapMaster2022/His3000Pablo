using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using His.Negocio;

namespace His.Honorarios
{
    public partial class frm_BalanceApagar : Form
    {
        #region Variables
        #endregion

        #region Constructor
        public frm_BalanceApagar()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frm_BalanceApagar_Load(object sender, EventArgs e)
        {
            try
            {
                //carga los tipos forma de pago en el combobox
                var medicosl = NegMedicos.MedicosConsulta().Where(cod => cod.MED_ESTADO == true).ToList();
                cmb_medicos.DataSource = medicosl.OrderBy(nom => nom.MED_NOMBRE).ToList();
                cmb_medicos.ValueMember = "MED_CODIGO";
                cmb_medicos.DisplayMember = "MED_NOMBRE";
                txt_fecfin.Text = DateTime.Now.ToString();

                cmb_medicos.SelectedValue = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ResetearControles();
        }
        private void txt_fecini_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsDigit(e.KeyChar))
                    e.Handled = false;
                else if (Char.IsControl(e.KeyChar))
                {
                    if (e.KeyChar == (char)(Keys.Enter))
                    {

                        if (txt_fecini.Text.Replace(" ", "").Replace("/", "") != string.Empty)
                        {
                            DateTime fec = DateTime.Parse(txt_fecini.Text);
                            txt_fecini.Text = fec.Date.ToString();
                        }
                        e.Handled = true;
                        txt_fecfin.Focus();
                    }
                }
                else if (Char.IsSeparator(e.KeyChar))
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fecha mal ingresada", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void txt_fecini_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txt_fecini.Text.Replace(" ", "").Replace("/", "") != string.Empty)
                {
                    DateTime fec = DateTime.Parse(txt_fecini.Text);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fecha mal ingresada", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void txt_fecfin_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsDigit(e.KeyChar))
                    e.Handled = false;
                else if (Char.IsControl(e.KeyChar))
                {
                    if (e.KeyChar == (char)(Keys.Enter))
                    {
                        DateTime fec = DateTime.Parse(txt_fecfin.Text);
                        e.Handled = true;
                        cmb_medicos.Focus();
                    }
                }
                else if (Char.IsSeparator(e.KeyChar))
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fecha mal ingresada", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void txt_fecfin_Leave(object sender, EventArgs e)
        {
            try
            {
                DateTime fec = DateTime.Parse(txt_fecfin.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fecha mal ingresada", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                frmReportes frm = new frmReportes();
                frm.reporte = "rBalanceApagarOpagado";
                if (txt_fecini.Text.Replace(" ", "").Replace("/", "") == string.Empty)
                    frm.campo1 = "01/01/0001";
                else
                    frm.campo1 = txt_fecini.Text;
                frm.campo2 = txt_fecfin.Text;
                if (cmb_medicos.SelectedValue == null)
                    frm.campo3 = "0";
                else
                {
                    //var med = NegMedicos.listaMedicos().Where(cod => cod.MED_CODIGO == int.Parse(cmb_medicos.SelectedValue.ToString())).FirstOrDefault();
                    frm.campo3 = cmb_medicos.SelectedValue.ToString();
                }
                frm.campo4 = "false";
                frm.campo5 = "BALANCE DE HONORARIOS PENDIENTES DE PAGO";
                frm.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        #endregion

        #region Metodos Privados
        private void ResetearControles()
        {
            txt_fecfin.Text = DateTime.Now.ToString();
            cmb_medicos.SelectedValue = -1;
            txt_fecini.Text = string.Empty;
            
        }
        #endregion

        

        

        

        
    }
}
