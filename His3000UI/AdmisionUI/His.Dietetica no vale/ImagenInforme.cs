using His.Entidades;
using His.Formulario;
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
    public partial class ImagenInforme : Form
    {
        private int id_agendamiento;
        public ImagenInforme( int id_agenda=0)
        {
            InitializeComponent();
            id_agendamiento = id_agenda;
            CargaDatos();
        }

        private void CargaDatos()
        {
            if (id_agendamiento != 0)
            {
                DataTable ag = NegImagen.getAgendamiento(id_agendamiento.ToString());
              
                txtPaciente.Text = ag.Rows[0]["Paciente"].ToString();
                txtCODRadiologo.Text = ag.Rows[0]["med_radiologo"].ToString();
                cargarMedico(Convert.ToInt32(txtCODRadiologo.Text.ToString()), txtRadiologo, txtCODRadiologo);
                txtCODTecnologo.Text = ag.Rows[0]["med_tecnologo"].ToString();
                cargarMedico(Convert.ToInt32(txtCODTecnologo.Text.ToString()), txtTecnologo, txtCODTecnologo);
                txtMedicoCOD.Text = ag.Rows[0]["MED_CODIGO"].ToString();
                cargarMedico(Convert.ToInt32(txtMedicoCOD.Text.ToString()), txtMedico, txtMedicoCOD);
                dtpEntrega.Text = (ag.Rows[0]["fecha_agendamiento"].ToString());
                dtpInforme.Text = (ag.Rows[0]["fecha_agendamiento"].ToString());

                DataTable est = NegImagen.getAgendamientoEstudios(id_agendamiento.ToString());
                foreach (DataRow row in est.Rows)
                {
                    gridEstudios.Rows.Add(row["CUE_CODIGO"].ToString(), row["PRO_DESCRIPCION"].ToString());
                }


                DataTable aest = NegDietetica.getDataTable("getPlacasImagen",id_agendamiento.ToString());

                txt30x40.Text = aest.Rows[0]["30X40"].ToString();
                txt8x10.Text = aest.Rows[0]["8x10"].ToString();
                txt14x14.Text = aest.Rows[0]["14x14"].ToString();
                txt14x17.Text = aest.Rows[0]["14x17"].ToString();
                txt18x24.Text = aest.Rows[0]["18x24"].ToString();
                txtPOdonto.Text = aest.Rows[0]["ODONT"].ToString();
                txtPDanada.Text = aest.Rows[0]["DANADAS"].ToString();
                txtPEnviadas.Text = aest.Rows[0]["ENVIADAS"].ToString();
                if (aest.Rows[0]["MEDIO_CONTRASTE"].ToString() != "0")
                {
                    chkMedioContraste.Checked = true;
                }

            }
        }

        private void cargarMedico(int codMedico, TextBox txtNombre, TextBox txtCOD)
        {
            MEDICOS medico = NegMedicos.MedicoID(codMedico);
            if (medico != null)
            {
                txtNombre.Text = medico.MED_APELLIDO_PATERNO + " " + medico.MED_APELLIDO_MATERNO + " " + medico.MED_NOMBRE1 + " " + medico.MED_NOMBRE2;
                txtCOD.Text = Convert.ToString(medico.MED_CODIGO);
            }

        }

        private void ultraGroupBox8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButtonSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddDiagnotico_Click(object sender, EventArgs e)
        {
            frm_ImagenAyuda ayuda = new frm_ImagenAyuda("DIAGNOSTICOS");
            ayuda.ShowDialog();
            if (ayuda.codigo != string.Empty)
            {
                if (!BuscarItem(ayuda.codigo, gridDiagnosticos))
                    this.gridDiagnosticos.Rows.Add(ayuda.codigo, ayuda.producto, "PRESUNTIVO");
                else
                    MessageBox.Show("El item ya fue ingresado.");
            }
        }
        private bool BuscarItem(string searchValue, DataGridView grid)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(searchValue))
                {
                    return true;
                }
            }
            return false;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (txtMedicoCOD.Text.Trim()!=string.Empty)
            {
                if (gridDiagnosticos.RowCount>0)
                {
                    NegImagen.saveInformeAgendamiento(empaquetar(), empaquetarDx());
                    this.Close();
                }else
                    MessageBox.Show("Por favor ingrese al menos un diagnostico.");

            }
            else
            {
                MessageBox.Show("Por favor ingrese el medico solicitante.");
            }
            

        }

        private List<PedidoImagen_diagnostico> empaquetarDx(){
             List<PedidoImagen_diagnostico> ListDiagnosticos = new List<PedidoImagen_diagnostico>();
                foreach (DataGridViewRow row in gridDiagnosticos.Rows)
                {
                    PedidoImagen_diagnostico estudio = new PedidoImagen_diagnostico();
        estudio.CIE_CODIGO = Convert.ToString(row.Cells["CIE10"].Value);
                   
                    ListDiagnosticos.Add(estudio);
                }
            return ListDiagnosticos;
}

        private string[] empaquetar()
        {
            string prioridad="1";
            if (rdUrgente.Checked)
                prioridad = "3";
            if (rbControl.Checked)
                prioridad = "1";
            if (rbNormal.Checked)
                prioridad = "2";

            string[] x = new string[] {
                id_agendamiento.ToString()
           ,txtMedicoCOD.Text
           ,prioridad
           ,dtpInforme.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
           ,dtpEntrega.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss")
           ,txt_diagnosticoMedico.Text
           ,txt_dbv.Text
           ,txt_lfv.Text
           ,txt_pav.Text
           ,txt_dbeg.Text
           ,txt_lfeg.Text
           ,txt_paeg.Text
           ,txt_dbp.Text
           ,txt_lfp.Text
           ,txt_pap.Text
           , (chkFundica.Checked==true?"1":"0")
           , (chkMarginal.Checked==true?"1":"0")
           , (chkPrevia.Checked==true?"1":"0")
           , (chkMasculino.Checked==true?"1":"0")
           , (chkFemenino.Checked==true?"1":"0")
           , (chkMultiple.Checked==true?"1":"0")
           , txt_gradomadurez.Text
           , (chkAnteversion.Checked==true?"1":"0")
           , (chkRetroversion.Checked==true?"1":"0")
           , (chkDiu.Checked==true?"1":"0")
           , (chkFibroma.Checked==true?"1":"0")
           , (chkMioma.Checked==true?"1":"0")
           , (chkAusente.Checked==true?"1":"0")
           , (chkHidrosalpix.Checked==true?"1":"0")
           , (chkQuiste.Checked==true?"1":"0")
           , (chkVacia.Checked==true?"1":"0")
           , (chkOcupada.Checked==true?"1":"0")
           , txt_sacoDouglas.Text
           , txtRecomendaciones.Text
           , (txtPEnviadas.Text.Trim()==string.Empty?"0":txtPEnviadas.Text.Trim())
           ,(txt30x40.Text.Trim()==string.Empty?"0":txt30x40.Text.Trim())
           ,(txt8x10.Text.Trim() == string.Empty ? "0" : txt8x10.Text.Trim())
           ,(txt14x14.Text.Trim() == string.Empty ? "0" : txt14x14.Text.Trim())
            ,(txt14x17.Text.Trim() == string.Empty ? "0" : txt14x17.Text.Trim())
          ,(txt18x24.Text.Trim() == string.Empty ? "0" : txt18x24.Text.Trim())
           ,(txtPOdonto.Text.Trim() == string.Empty ? "0" : txtPOdonto.Text.Trim())
           ,(txtPDanada.Text.Trim() == string.Empty ? "0" : txtPDanada.Text.Trim())
           ,(chkMedioContraste.Checked==true?"1":"0")
        };
            return x;
        }

        private void btnAddMedico_Click(object sender, EventArgs e)
        {
            List<MEDICOS> listaMedicos = NegMedicos.listaMedicosIncTipoMedico();
            frm_AyudaMedicos ayuda = new frm_AyudaMedicos(listaMedicos, "MEDICOS", "CODIGO");
            ayuda.ShowDialog();
            if (ayuda.campoPadre.Text != string.Empty)
                cargarMedico(Convert.ToInt32(ayuda.campoPadre.Text.ToString()), txtMedico, txtMedicoCOD);
        }
    }


}
