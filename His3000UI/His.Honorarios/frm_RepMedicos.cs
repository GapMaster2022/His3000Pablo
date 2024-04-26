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
    public partial class frm_RepMedicos : Form
    {
        #region Variables
        #endregion

        #region Constructor
        public frm_RepMedicos()
        {
            InitializeComponent();
        }
        #endregion

        #region Eventos
        private void frm_RepMedicos_Load(object sender, EventArgs e)
        {
            try
            {
                //carga los tipos forma de pago en el combobox
                var tipoformap = NegEspecialidades.ListaEspecialidades();
                cmb_especialidad.DataSource = tipoformap;
                cmb_especialidad.ValueMember = "ESP_CODIGO";
                cmb_especialidad.DisplayMember = "ESP_NOMBRE";

                var tipomedico = NegTipoMedico.RecuperaTipoMedicos();
                cmb_tipomedico.DataSource = tipomedico;
                cmb_tipomedico.ValueMember = "TIM_CODIGO";
                cmb_tipomedico.DisplayMember = "TIM_NOMBRE";

                var tipohonorario = NegTipoHonorario.RecuperaTipoHonorarios();
                cmb_tipohonorario.DataSource = tipohonorario;
                cmb_tipohonorario.ValueMember = "TIH_CODIGO";
                cmb_tipohonorario.DisplayMember = "TIH_NOMBRE";

                rbn_todos.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void rbn_todos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbn_todos.Checked == true)
            {
                ResetearControles();
                cmb_especialidad.Enabled = false;
                cmb_tipohonorario.Enabled = false;
                cmb_tipomedico.Enabled = false;
               
            }
        }
        private void rbn_especialidad_CheckedChanged(object sender, EventArgs e)
        {
            if (rbn_especialidad.Checked == true)
            {
                ResetearControles();
                cmb_especialidad.Enabled = true;
                cmb_tipohonorario.Enabled = false;
                cmb_tipomedico.Enabled = false;
                
            }
        }
        private void rbn_tipomedico_CheckedChanged(object sender, EventArgs e)
        {
            if (rbn_tipomedico.Checked == true)
            {
                ResetearControles();    
                cmb_especialidad.Enabled = false;
                cmb_tipohonorario.Enabled = false;
                cmb_tipomedico.Enabled = true;
                
            }
        }
        private void rbn_tipohonorario_CheckedChanged(object sender, EventArgs e)
        {
            if (rbn_tipohonorario.Checked == true)
            {
                ResetearControles();
                cmb_especialidad.Enabled = false;
                cmb_tipohonorario.Enabled = true;
                cmb_tipomedico.Enabled = false;
                
            }
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                frmReportes frm = new frmReportes();
                if (rbn_todos.Checked == true)
                    frm.reporte = "rMedicosTodos";
                else if (rbn_especialidad.Checked == true)
                {
                    frm.reporte = "rMedicosEspecialidad";
                    if (cmb_especialidad.SelectedValue == null)
                        frm.campo1 = "0";
                    else
                        frm.campo1 = cmb_especialidad.SelectedValue.ToString();
                }
                else if (rbn_tipomedico.Checked == true)
                {
                    frm.reporte = "rMedicosTipoM";
                    if (cmb_tipomedico.SelectedValue == null)
                        frm.campo1 = "0";
                    else
                        frm.campo1 = cmb_tipomedico.SelectedValue.ToString();
                }
                else if (rbn_tipohonorario.Checked == true)
                {
                    frm.reporte = "rMedicosTipoH";
                    if (cmb_tipohonorario.SelectedValue == null)
                        frm.campo1 = "0";
                    else
                        frm.campo1 = cmb_tipohonorario.SelectedValue.ToString();
                }
                //frm.campo1 = txt_medcodigo.Text;
                frm.Show();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            rbn_todos.Checked = true;
        }
        #endregion

        

        #region Metodos Privados
        private void ResetearControles()
        {
            cmb_especialidad.SelectedValue = -1;
            cmb_tipomedico.SelectedValue = -1;
            cmb_tipohonorario.SelectedValue = -1;
            //cmb_tipohonorario.Text = string.Empty;
            //cmb_especialidad.Text = string.Empty;
            //cmb_tipomedico.Text =string.Empty;  
        }
        #endregion

        
        

        
    }
}
