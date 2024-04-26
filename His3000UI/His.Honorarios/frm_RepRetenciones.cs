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

namespace His.Honorarios
{
    public partial class frm_RepRetenciones : Form
    {
        #region VARIAbles
        public bool tipdocok;
        #endregion

        #region Constructores
        public frm_RepRetenciones()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frm_RepRetenciones_Load(object sender, EventArgs e)
        {
            try
            {
                tipdocok = true;
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
                tipdocok = true;
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
        private void txt_proceso_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsDigit(e.KeyChar))
                    e.Handled = false;
                else if (Char.IsControl(e.KeyChar))
                {
                    if (e.KeyChar == (char)(Keys.Enter))
                    {
                        e.Handled = true;
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
        private void chk_automaticas_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_automaticas.Checked == true)
                cmb_medicos.Enabled = false;
            else
                cmb_medicos.Enabled = true;
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                chk_automaticas.Focus();
                if (tipdocok == true)
                {
                    frmReportes frm = new frmReportes();
                    if (chk_automaticas.Checked != true)
                    {
                        frm.reporte = "rRetencionGeneral";
                        if (txt_fecini.Text.Replace(" ", "").Replace("/", "") == string.Empty)
                            frm.campo1 = "01/01/0001";
                        else
                            frm.campo1 = txt_fecini.Text;
                        frm.campo2 = txt_fecfin.Text;
                        if (cmb_medicos.SelectedValue == null)
                        {
                            frm.campo3 = "0";
                        }
                        else
                        {
                            //frm.campo4 = cmb_medicos.SelectedValue.ToString();
                            var med = NegMedicos.listaMedicos().Where(cod => cod.MED_CODIGO == int.Parse(cmb_medicos.SelectedValue.ToString())).FirstOrDefault();
                            frm.campo3 = med.MED_RUC;
                        }
                        frm.Show();
                    }
                    else
                    {
                        frmReportes frm1 = new frmReportes();
                        if (txt_proceso.Text == string.Empty)
                        {
                            frm1.reporte = "rRetencionAutReimpresion";
                            if (txt_fecini.Text.Replace(" ", "").Replace("/", "") == string.Empty)
                                frm1.campo1 = "01/01/0001";
                            else
                                frm1.campo1 = txt_fecini.Text;
                            frm1.campo2 = txt_fecfin.Text;
                            frm1.campo3 = "4";
                            frm1.Show();
                        }
                        else
                        {
                            frm1.reporte = "rRetencionesAutomaticas";
                            frm1.campo1 = txt_proceso.Text;
                            frm1.Show();

                            frmReportes frm2 = new frmReportes();
                            frm2.reporte = "rRetencionesAInforme";
                            frm2.campo1 = txt_proceso.Text;
                            frm2.Show();

                        }


                    }

                    //frm.campo1 = txt_medcodigo.Text;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void txt_proceso_Leave(object sender, EventArgs e)
        {
            tipdocok = true;
            if (txt_proceso.Text != string.Empty)
            {
                List<GENERACIONES_AUTOMATICAS> generatipodoc = new List<GENERACIONES_AUTOMATICAS>();
                var genera = NegGeneracionesAutomaticas.ListaGeneracionesAutomaticas().Where(cod => cod.GEN_CODIGO == int.Parse(txt_proceso.Text)).ToList();
                if (genera.Count == 0)
                {
                    MessageBox.Show("Proceso no existente", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tipdocok = false;
                    txt_proceso.Focus();
                }
                else
                {
                    generatipodoc = genera.Where(cod => cod.TIPO_DOCUMENTO.TID_CODIGO == 4).ToList();
                    if (generatipodoc.Count == 0)
                    {
                        MessageBox.Show("Proceso no existente en el tipo de documento", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tipdocok = false;
                        txt_proceso.Focus();
                    }
                }
            }
        }
        #endregion

        #region Metodos privados
        private void ResetearControles()
        {
            cmb_medicos.SelectedValue = -1;
            txt_fecfin.Text = DateTime.Now.ToString();
            cmb_medicos.SelectedValue = -1;
            txt_fecini.Text = string.Empty;
            txt_proceso.Text = string.Empty;
            chk_automaticas.Checked = false;

        }
        #endregion

        

        

        

        

        

       

       

        

        
    }
}
