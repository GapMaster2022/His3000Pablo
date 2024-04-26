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

namespace TarifariosUI
{
    public partial class MasHonorarios : Form
    {
        string atencion = "";
        Int32 codUsuario = 0;
        public List<ListaHonorariosTeam> lista = new List<ListaHonorariosTeam>();
        public MasHonorarios(string _atencion, string _total, Int32 cod_Usuario)
        {
            InitializeComponent();
            atencion = _atencion;
            txtTotalMed1.Text = _total;
            codUsuario = cod_Usuario;
        }

        private void ultraButton2_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Medicos lista = new frm_Medicos(NegMedicos.listaMedicos());
                lista.tabla = "MEDICOS";
                lista.campoPadre = txtmedico;
                lista.ShowDialog();
                HIS3000BDEntities contexto = new HIS3000BDEntities(ConexionEntidades.ConexionEDM);
                int codigo = Convert.ToInt16(txtmedico.Text.Trim());
                MEDICOS medicoActual = contexto.MEDICOS.FirstOrDefault(m => m.MED_CODIGO == codigo);//1);//

                if (medicoActual != null)
                    lbl_medPrincipal.Text = medicoActual.MED_APELLIDO_PATERNO.Trim() + " " + medicoActual.MED_APELLIDO_MATERNO.Trim() +
                                        " " + medicoActual.MED_NOMBRE1.Trim();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtTotal.Text != "")
            {
                NegTarifario.GuardaHonorarioCuentaPaciente(atencion, txtTotal.Text, txtmedico.Text, codUsuario, DateTime.Now);
                MessageBox.Show("La información se guardo exitosamente", "Inf", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListaHonorariosTeam obj = new ListaHonorariosTeam();
                obj.nombre = lbl_medPrincipal.Text;
                obj.porcentaje = txtPorcentaje.Text;
                obj.valor = txtTotal.Text;
                lista.Add(obj);
                txtmedico.Text = "";
                txtPorcentaje.Text = "";
                txtTotal.Text = "";
                lbl_medPrincipal.Text = "...";               

            }
            else
            {
                MessageBox.Show("Genere un valor para cargar al médico", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnGenera_Click(object sender, EventArgs e)
        {
            if (txtPorcentaje.Text != "")
            {
                decimal medTotal = Convert.ToDecimal(txtTotalMed1.Text);
                decimal porcentaje = Convert.ToDecimal(txtPorcentaje.Text);
                txtTotal.Text = (medTotal * (porcentaje / 100)).ToString();
                
            }
            else
            {
                MessageBox.Show("Ingrese un porcentaje para generar el pago del Médico", "HIS3000", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPorcentaje.Focus();
            }
        }

        
    }
}
